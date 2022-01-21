using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPreview : MonoBehaviour
{
    public List<Sprite> Icons;
    private Color activeColor = new Color(1f,1f,1f,0.8f);
    private Color activeFontColor = new Color(0f, 0f, 0f, 1f);

    private Color notActiveColor = new Color(1f, 1f, 1f, 0.2f);
    private Color notActiveFontColor = new Color(1f, 1f, 1f, 1f);
    public void ActualizePreview()
    {
        GameObject PlayerObject = GameObject.Find("----PLAYER----");
        List<CharacterData>data = PlayerObject.GetComponent<PlayerController>().GetCharacterData();
        Player player = PlayerObject.GetComponent<PlayerController>().GetCurrentCharacter();

        for (int i = 0; i < this.GetComponent<Transform>().GetChild(0).childCount; i++)
        {
            
            if(i < data.Count)
            {
                if(player.GetCharacterData().NAME == data[i].name)
                {
                    Debug.Log(player.GetCharacterData().NAME);
                    this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().color = activeColor;
                    this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().color = activeFontColor;
                } else
                {
                    //Debug.Log(player.GetCharacterData().NAME);
                    this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().color = notActiveColor;
                    this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().color = notActiveFontColor;
                }
                this.editPreviewPart(data[i], i);
                this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = data[i].name;

            } else  
            {
                this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(1).GetComponent<Image>().sprite = Icons[5];
            }
        }
    }

    private void editPreviewPart(CharacterData d, int i) 
    {
        switch (d.NAME)
        {
            case "Character":
                this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(1).GetComponent<Image>().sprite = Icons[0];
                break;
            case "Kaengu":
                this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(1).GetComponent<Image>().sprite = Icons[1];
                break;
            case "Paengu":
                this.GetComponent<Transform>().GetChild(0).GetChild(i).GetChild(1).GetComponent<Image>().sprite = Icons[2];
                break;
        }
        
    }
}
