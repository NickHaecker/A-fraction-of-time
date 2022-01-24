using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject ChooseCharacterUI;
    public GameObject RunTimeUI;
    private bool ShiftIsPressed = false;

    public GameObject StartMenu;
    public GameObject InfoMenu;

    public GameObject Player;

    public TextMeshProUGUI time;

    private bool infoboxActive = true;
    
    private float infoboxEndTime;

    private GameObject Cam;



    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("----CAM----");
        showRuntimeInfobox("Welcome to A Fraction of Time! The controls are: WASD for character movement, SPACE for jump, MOUSE for camera movement, G for splitting and merging and I for Controll-Informations. Have Fun!", 15);
        
        Player = GameObject.Find("----PLAYER----");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (StartMenu.activeInHierarchy)
        {
            Cam.GetComponent<Transform>().GetChild(0).GetComponent<CinemachineFreeLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if(Time.realtimeSinceStartup > infoboxEndTime && infoboxActive)
        {
            hideWelcomeText();
            infoboxActive = false; 
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            InfoMenu.SetActive(!InfoMenu.activeInHierarchy);
        }

        time.text = computeSeconds(Player.GetComponent<TimeController>().GetGameTime());
    }

    private void hideWelcomeText()
    {
        RunTimeUI.GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
    }

    public void showRuntimeInfobox(string text, float time)
    {
        float infoboxStartTime = Time.realtimeSinceStartup;
        infoboxEndTime = infoboxStartTime + time;
        infoboxActive = true;
        this.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
        this.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    public bool toggleSelectionUI()
    {
        ChooseCharacterUI.SetActive(!ChooseCharacterUI.activeSelf);
        RunTimeUI.SetActive(!RunTimeUI.activeSelf);
        if (!ChooseCharacterUI.activeSelf) return false;
        else return true;
    }

    private string computeSeconds(float seconds)
    {
        int s = Mathf.RoundToInt(seconds);

        int minutes = s / 60;
        int sec = s % 60;
        string secString = "";
        string minString = "";

        if (sec < 10) secString = "0" + sec;
        else secString = "" + sec;
        if (minutes < 10) minString = "0" + minutes;
        else minString = "" + minutes;

        return minString + ":" + secString;
    }
}
