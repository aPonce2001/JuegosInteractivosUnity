using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float jumpoForce;
    public float moveSpeed = 5f;
    public float gravityScale = 5f;
    public CharacterController characterController;
    private Vector3 moveDirection;

    private Camera theCamera;
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator animator;


    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;  //Arreglo de piezas del jugador

    public float bounceForce = 8f;
    public bool stopMove;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
        stopMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMove)
        {

            if (!isKnocking)
            {
                //El siguiente codigo es para que el personaje se mueva en el eje x y z
                // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")

                float yStore = moveDirection.y;
                // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
                //Calcula la magnitud del vector y que tenga una magnitud de 1 independientemente de la direccion
                moveDirection.Normalize();
                moveDirection = moveDirection * moveSpeed;
                moveDirection.y = yStore;

                if (characterController.isGrounded)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        //El siguiente codigo es para que el personaje salte si esta en el suelo y no en el aire
                        moveDirection.y = jumpoForce;
                    }
                }

                // El siguiente codigo es para que el personaje caiga si no esta en el suelo
                else
                {
                    moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
                }
                //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);
                characterController.Move(moveDirection * Time.deltaTime);

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    //Sirve para que el personaje mire hacia donde se mueve
                    transform.rotation = Quaternion.Euler(0.0f, theCamera.transform.rotation.eulerAngles.y, 0.0f);
                    //Sirve para que el personaje mire hacia donde se mueve
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                    // playerModel.transform.rotation = newRotation;
                    //Sirve para el suavizado de la rotacion
                    playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                    //Permite imprimir en consola la magnitud del vector
                    //Cantidad de pixeles por unidad de tiempo
                    // Debug.Log(moveDirection.magnitude);
                }
            }

            if (isKnocking)
            {

                knockBackCounter -= Time.deltaTime;
                float yStore = moveDirection.y;
                // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                //moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
                moveDirection = playerModel.transform.forward * -knockBackPower.x;
                //Calcula la magnitud del vector y que tenga una magnitud de 1 independientemente de la direccion
                moveDirection.Normalize();
                moveDirection = moveDirection * moveSpeed;
                moveDirection.y = yStore;

                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

                characterController.Move(moveDirection * Time.deltaTime);
                if (knockBackCounter <= 0)
                {
                    isKnocking = false;
                }
            }
        }
        else
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            characterController.Move(moveDirection);
        }
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", characterController.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockBackPower.y;
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
