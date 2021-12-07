using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{

    public void OnHoverMainMenu()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 24;
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 255;
    }

    public void OnExitMainMenu()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 20;
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 180;
    }
}
