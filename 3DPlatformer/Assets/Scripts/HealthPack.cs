using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public bool isFullHealth;
    public int healthAmmount;
    public GameObject particleSystem;
    public GameObject hearthAsset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Se desactiva el objeto
            // Destroy(gameObject);
            // if (isFullHealth)
            // {
            //     HealthManager.instance.ResetHealth();
            // }
            // else
            // {
            //     HealthManager.instance.AddHealth(healthAmmount);
            // }
            StartCoroutine(DestroyCoroutine());
        }
    }

    public IEnumerator DestroyCoroutine()
    {
        particleSystem.SetActive(true);
        hearthAsset.SetActive(false);

        if (isFullHealth)
        {
            HealthManager.instance.ResetHealth();
        }
        else
        {
            HealthManager.instance.AddHealth(healthAmmount);
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
