using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;
    public float gravityScale = 5f;
    public CharacterController characterController;
    private Vector3 _moveDirection;
    private Camera theCamera;
    public GameObject playerModel;
    public Animator animator;

    void Start()
    {
        theCamera = Camera.main;
    }

    void Update()
    {
        float yStore = _moveDirection.y;
        _moveDirection = Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right;
        _moveDirection = _moveDirection.normalized;
        _moveDirection *= moveSpeed;
        _moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            _moveDirection.y = jumpForce;
        }

        _moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        characterController.Move(_moveDirection * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCamera.transform.rotation.eulerAngles.y, 0f);
            Quaternion modelRotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0f, _moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, modelRotation, Time.deltaTime * 10f);
        }

        animator.SetFloat("Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
    }
}
