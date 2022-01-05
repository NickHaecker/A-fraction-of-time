using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RingHandler : MonoBehaviour
{
    public List<RingButton> RingButtons = new List<RingButton>();
    public List<Sprite> Icons = new List<Sprite>();
    private List<CharacterData> Data;
    

    public void setCharacterData(List<CharacterData> data, SplitSelectionController sSC)
    {
        this.Data = data;
        for(int i = 0; i < Data.Count; i++ )
        {
            RingButtons[i].GetComponent<RingButton>().setData(Data[i]);
        }

        foreach (RingButton button in RingButtons)
        {
            if(button.Data != null)
            {
                switch (button.Data.NAME)
                {
                    case "Character":
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[0];
                        break;
                    case "Kaengu":
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[1];
                        break;
                    default:
                        button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[5];
                        break;
                }
                button.GetComponentInChildren<Button>().onClick.RemoveListener(() => sSC.HandleCharacterSelection(button.Data.NAME));
                button.GetComponentInChildren<Button>().onClick.AddListener(() => sSC.HandleCharacterSelection(button.Data.NAME));
                
            } else
            {
                button.GetComponentInChildren<RectTransform>().GetChild(0).GetChild(0).GetComponent<Image>().sprite = Icons[5];
            }
            
        }
    }
}
