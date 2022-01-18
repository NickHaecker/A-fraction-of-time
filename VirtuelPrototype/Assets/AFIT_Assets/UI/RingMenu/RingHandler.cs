using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RingHandler : MonoBehaviour
{
    public List<RingButton> RingButtons = new List<RingButton>();
    public List<Sprite> Icons = new List<Sprite>();
    private List<CharacterData> Data;

    //public GameObject PlayerObject;


    public void setCharacterData(List<CharacterData> data,SplitSelectionController sSC)
    {

        GameObject PlayerObject = GameObject.Find("----PLAYER----");
        Player activeCharacter = PlayerObject.GetComponent<PlayerController>().GetCurrentCharacter();
        string currentCharacterName = activeCharacter.GetCharacterData().NAME;
        switch(currentCharacterName)
        {
            case "Character":
                this.GetComponent<Transform>().GetChild(2).GetChild(0).GetComponent<Image>().sprite = Icons[0];
                this.GetComponent<Transform>().GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = currentCharacterName;
                break;
            case "Kaengu":
                this.GetComponent<Transform>().GetChild(2).GetChild(0).GetComponent<Image>().sprite = Icons[1];
                this.GetComponent<Transform>().GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = currentCharacterName;
                break;
            case "Paengu":
                this.GetComponent<Transform>().GetChild(2).GetChild(0).GetComponent<Image>().sprite = Icons[2];
                this.GetComponent<Transform>().GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = currentCharacterName;
                break;
            default:
                this.GetComponent<Transform>().GetChild(2).GetChild(0).GetComponent<Image>().sprite = Icons[5];
                break;
        }

        this.Data = data;
        for(int i = 0 ; i < Data.Count ; i++)
        {
            RingButtons[i].GetComponent<RingButton>().setData(Data[i]);
        }

        foreach(RingButton button in RingButtons)
        {
            button.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            if(button.Data != null)
            {
                switch(button.Data.NAME)
                {
                    case "Character":
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[0];
                        ButtonStyling(button,button.Data.NAME,currentCharacterName,sSC);
                        break;
                    case "Kaengu":
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[1];
                        ButtonStyling(button,button.Data.NAME,currentCharacterName,sSC);
                        break;
                    case "Paengu":
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[2];
                        ButtonStyling(button,button.Data.NAME,currentCharacterName,sSC);
                        break;
                    default:
                        ColorBlock cbBlocked = new ColorBlock();
                        cbBlocked.normalColor = new Color(1f,1f,1f,0.1f);
                        cbBlocked.highlightedColor = new Color(1f,1f,1f,0.1f);
                        cbBlocked.pressedColor = new Color(1f,1f,1f,0.1f);
                        cbBlocked.colorMultiplier = 1;
                        button.GetComponentInChildren<Button>().colors = cbBlocked;
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[5];
                        break;
                }
            }
            else
            {
                ColorBlock cbBlocked = new ColorBlock();
                cbBlocked.normalColor = new Color(1f,1f,1f,0.1f);
                cbBlocked.highlightedColor = new Color(1f,1f,1f,0.1f);
                cbBlocked.pressedColor = new Color(1f,1f,1f,0.1f);
                cbBlocked.colorMultiplier = 1;
                button.GetComponentInChildren<Button>().colors = cbBlocked;
                button.GetComponentInChildren<Button>().interactable = false;
                button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[5];
                button.GetComponentInChildren<Button>().onClick.RemoveListener(() => sSC.HandleCharacterSelection(button.Data.NAME));
            }
        }
    }

    private void ButtonStyling(RingButton button,string buttonName,string currentCharacterName,SplitSelectionController sSC)
    {
        if(currentCharacterName != buttonName)
        {
            ColorBlock cb = new ColorBlock();
            cb.normalColor = new Color(1f,1f,1f,0.6f);
            cb.highlightedColor = new Color(1f,1f,1f,0.9f);
            cb.pressedColor = new Color(1f,1f,1f,1f);
            cb.colorMultiplier = 1;
            button.GetComponentInChildren<Button>().colors = cb;
            button.GetComponentInChildren<Button>().onClick.AddListener(() => sSC.HandleCharacterSelection(button.Data.NAME));
            button.GetComponentInChildren<Button>().interactable = true;

        }
        else
        {
            ColorBlock cbBlocked = new ColorBlock();
            cbBlocked.normalColor = new Color(1f,1f,1f,0.1f);
            cbBlocked.highlightedColor = new Color(1f,1f,1f,0.1f);
            cbBlocked.pressedColor = new Color(1f,1f,1f,0.1f);
            cbBlocked.disabledColor = new Color(1f,1f,1f,0.1f);
            cbBlocked.colorMultiplier = 1;
            button.GetComponentInChildren<Button>().colors = cbBlocked;
            button.GetComponentInChildren<Button>().interactable = false;
        }
    }
}
