using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using UnityEngine.SceneManagement; //Libreria para cargar escenas en Unity

public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager instance;
    private Vector3 respawnPosition;
    public GameObject deathEffect;

    public int currentCoins;

    public int levelEndMusic = 5;

    public string levelToLoad;


    //Awake se ejecuta antes que el start y sirve para inicializar variables
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Esconde el cursor y lo bloquea en el centro de la pantalla
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Guarda la posicion inicial del jugador
        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        //Reinicia la posicion del jugador
        StartCoroutine(RespawnCoroutine());
    }

    //Corrutina para el respawn
    public IEnumerator RespawnCoroutine()
    {
        //Desactiva el jugador
        PlayerController.instance.gameObject.SetActive(false);
        //Desactiva la camara
        CameraControler.instance.cmBrain.enabled = false;
        //Crea el efecto de muerte
        Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
        //Activa el fade
        UIManager.instance.fadeToBlack = true;


        //Retorna un valor despues de un tiempo y luego continua con el codigo
        yield return new WaitForSeconds(2f);
        //Reinicia la posicion del jugador
        PlayerController.instance.transform.position = respawnPosition;


        //Reactiva el jugador
        PlayerController.instance.gameObject.SetActive(true);
        //Reactiva la camara
        CameraControler.instance.cmBrain.enabled = true;
        //Desactiva el fade
        UIManager.instance.fadeFromBlack = true;

        //Reinicia la vida
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = this.currentCoins.ToString();
    }

    public IEnumerator LevelEndCoroutine()
    {
        yield return new WaitForSeconds(2f);

        MusicManager.instance.PlaySFX(levelEndMusic);
        SceneManager.LoadScene(levelToLoad);
        Debug.Log("Level End");
    }
}
