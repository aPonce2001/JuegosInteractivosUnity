using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    private Vector3 _respawnPosition;

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _respawnPosition = new Vector3
        (
            PlayerController.Instance.transform.position.x,
            PlayerController.Instance.transform.position.y,
            PlayerController.Instance.transform.position.z
        );
    }

    void Update()
    {

    }

    public void Respawn()
    {
        PlayerController.Instance.transform.position = new Vector3
        (
            _respawnPosition.x,
            _respawnPosition.y,
            _respawnPosition.z
        );
    }
}
