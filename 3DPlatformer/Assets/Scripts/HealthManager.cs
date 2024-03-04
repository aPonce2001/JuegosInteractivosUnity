using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;

    public int maxHealth, currentHealth;
    public float invisibleLength = 2f;
    private float invisibleCounter;

    public Sprite[] healthBarImages;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Si esta en el proceso de desaparecer entocnes se alterna entre desaparecer y aparecer
        if (invisibleCounter > 0)
        {
            invisibleCounter -= Time.deltaTime;

            if (Math.Floor(invisibleCounter*5f)%2 == 0)
            {
                // PlayerController.instance.playerModel.SetActive(true);
                SetActivePlayerPieces(true);
            }
            else
            {
                // PlayerController.instance.playerModel.SetActive(false);
                SetActivePlayerPieces(false);
            }
        }

        if (invisibleCounter <= 0)
        {
            // PlayerController.instance.playerModel.SetActive(true);
            SetActivePlayerPieces(true);
        }
    }

    private void SetActivePlayerPieces(bool flag)
    {
        for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
        {
            PlayerController.instance.playerPieces[i].SetActive(flag);
        }
    }

    //Metodo para restar vida al jugador
    public void Hurt()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.Respawn();
        }
        else
        {
            invisibleCounter = invisibleLength;
            // PlayerController.instance.playerModel.SetActive(false);
            SetActivePlayerPieces(false);
        }
        UpdateUI();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void AddHealth(int ammount)
    {
        currentHealth += ammount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();
        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.sprite = null;
                break;
        }

    }
}
