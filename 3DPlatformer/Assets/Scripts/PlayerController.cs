using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;
    public float gravityScale = 5f;
    public CharacterController characterController;
    private Vector3 _moveDirection;

    void Start()
    {
        return;
    }

    void Update()
    {
        float yStore = _moveDirection.y;
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _moveDirection *= moveSpeed;
        _moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            _moveDirection.y = jumpForce;
        }

        _moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        characterController.Move(_moveDirection * Time.deltaTime);
    }
}
