using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
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
        //si el objeto que entra en la zona de muerte es el jugador
        if (other.tag == "Player")
        {
            Debug.Log("Player has entered the death zone");
            GameManager.instance.Respawn();
            
        }
    }
}
