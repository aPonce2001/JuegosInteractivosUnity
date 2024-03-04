using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    public static CoinPickup instance;

    public GameObject coinImage;
    public GameObject pickupEffect;

    public int coinValue;

    private void Awake()
    {
        instance = this;
    }

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
        if(other.tag == "Player")
        {
            StartCoroutine(DestroyCoinCoroutine());
            MusicManager.instance.PlaySFX(5);
        }
    }

    private IEnumerator DestroyCoinCoroutine()
    {
        GameManager.instance.AddCoins(coinValue);
        coinImage.SetActive(false);
        Instantiate(pickupEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
