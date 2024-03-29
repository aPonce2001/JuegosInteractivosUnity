using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthText;

    public Image healthImage;

    public Text coinText;

    //Singleton
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
        if (fadeToBlack){
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 
            Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f){
                fadeToBlack = false;
            }
        }else if (fadeFromBlack){
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 
            Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f){
                fadeFromBlack = false;
            }
        }
    }
}
