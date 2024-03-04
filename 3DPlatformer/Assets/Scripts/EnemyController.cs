using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking
    }

    public AIState currentState;

    public float waitApoint;
    private float waitCounter;

    public float chaseRange;

    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;


    // Start is called before the first frame update
    void Start()
    {
        currentPatrolPoint = 0;
        waitCounter = waitApoint;
    }

    // Update is called once per frame
    void Update()
    {

        float distancePlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                anim.SetBool("isMoving", false);
                if(waitCounter >= 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.isPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }
                if(distancePlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }
            break;

            case AIState.isPatrolling:
                //agent.SetDestination(patrolPoints[currentPatrolPoint].position);

                if(agent.remainingDistance <= 0.2f)
                {
                    currentPatrolPoint++;
                    if(currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }                    
                    currentState = AIState.isIdle;
                    waitCounter = waitApoint;
                }

                if(distancePlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                anim.SetBool("isMoving", true);
            break;

            case AIState.isChasing:
                anim.SetBool("isMoving", true);
                agent.SetDestination(PlayerController.instance.transform.position);

                if(distancePlayer < attackRange)
                {
                    currentState = AIState.isAttacking;
                    anim.SetTrigger("Attack");
                    anim.SetBool("isMoving", false);

                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    attackCounter = timeBetweenAttacks;
                }

                if(distancePlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitApoint;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
            break;

            case AIState.isAttacking:

                //El siguiente codigo sirve para que el enemigo siempre mire hacia el jugador
                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                //El siguiente codigo sirve para que el enemigo no rote en el eje Y antes de atacar
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if(distancePlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }else
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitApoint;

                        agent.isStopped = false;
                    }
                }
            break;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player ha entrado en el rango de ataque");
            HealthManager.instance.Hurt();
        }
    }
}
