using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkPointOn, checkPointOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !checkPointOn.activeSelf)
        {
            checkPointOff.SetActive(false);
            checkPointOn.SetActive(true);
        }
    }
}
