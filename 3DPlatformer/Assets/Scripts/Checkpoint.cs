using System.Collections;
using System.Collections.Generic;
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
        if (other.tag == "Player")
        {
            //Obtiene todos los checkpoints del nivel
            Checkpoint[] allCheckpoints = FindObjectsOfType<Checkpoint>();
            //Recorre todos los checkpoints del nivel para desactivarlos y activar el actual
            for (int i = 0; i < allCheckpoints.Length; i++)
            {
                allCheckpoints[i].checkPointOn.SetActive(false);
                allCheckpoints[i].checkPointOff.SetActive(true);
            }

            checkPointOn.SetActive(true);
            checkPointOff.SetActive(false);

            GameManager.instance.SetSpawnPosition(PlayerController.instance.transform.position);
        }
    }
}
