using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{

    public int maxHealth = 1;
    private int currentHealth;

    public GameObject deathEffect;

    public GameObject objectDToDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Takedamage(){
        currentHealth--;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);

            PlayerController.instance.Bounce();

            Instantiate(deathEffect, transform.position + new Vector3(0f, 1.3f, 0f), transform.rotation);
            Instantiate(objectDToDrop, transform.position + new Vector3(0f, 1.3f, 0f), transform.rotation);
        }
    }
}
