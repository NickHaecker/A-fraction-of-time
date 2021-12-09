using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject ChooseCharacterUI;
    public GameObject RunTimeUI;
    private bool ShiftIsPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !ChooseCharacterUI.activeSelf && !ShiftIsPressed)
        {
            RunTimeUI.SetActive(false);
            ChooseCharacterUI.SetActive(true);
            ShiftIsPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && ChooseCharacterUI.activeSelf && !ShiftIsPressed)
        {
            RunTimeUI.SetActive(true);
            ChooseCharacterUI.SetActive(false);
            ShiftIsPressed = true;
        }
        if(!Input.GetKeyDown(KeyCode.LeftShift) && ShiftIsPressed)
        {
            ShiftIsPressed = false;
        }
    }
}
