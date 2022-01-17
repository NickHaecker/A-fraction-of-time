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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject Cam = GameObject.Find("----CAM----");
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
            Cursor.lockState = CursorLockMode.None;
        }
        if(!Input.GetKeyDown(KeyCode.G) && ShiftIsPressed)
        {
            ShiftIsPressed = false;
        }
    }
}
