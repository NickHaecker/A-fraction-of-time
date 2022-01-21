using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject ChooseCharacterUI;
    public GameObject RunTimeUI;
    private bool ShiftIsPressed = false;

    public GameObject StartMenu;

    private bool infoboxActive = true;
    private GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("----CAM----");
    }

    // Update is called once per frame
    void Update()
    {

        
        if (StartMenu.activeInHierarchy)
        {
            Cam.GetComponent<Transform>().GetChild(0).GetComponent<CinemachineFreeLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.G) && !ChooseCharacterUI.activeSelf && !ShiftIsPressed)
        {
            RunTimeUI.SetActive(false);
            ChooseCharacterUI.SetActive(true);
            ShiftIsPressed = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.G) && ChooseCharacterUI.activeSelf && !ShiftIsPressed)
        {
            RunTimeUI.SetActive(true);
            ChooseCharacterUI.SetActive(false);
            ShiftIsPressed = true;
        }
        if(!Input.GetKeyDown(KeyCode.G) && ShiftIsPressed)
        {
            ShiftIsPressed = false;
        }

        if(Time.realtimeSinceStartup > 20 && infoboxActive)
        {
            hideWelcomeText();
            infoboxActive = false; 
        }
    }

    private void hideWelcomeText()
    {
        RunTimeUI.GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
    }
}
