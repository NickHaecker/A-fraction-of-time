using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractionObject
{
    [SerializeField]
    private List<GameObject> _targets = null;

    [SerializeField]
    private bool isDoor;

    [SerializeField]
    private bool hasBoth = false;

    public override void CharacterNearbyAction()
    {
        AudioPlayerScript.instance.playSpecificAudio(this.name);
        foreach (GameObject target in _targets)
        {
            if (hasBoth)
            {
                if(target.GetComponent<Door>() != null)
                {
                    target.GetComponent<Door>().SetMoving(true);
                }
                else if(target.GetComponent<MoveablePlatform>() != null)
                {
                    target.GetComponent<MoveablePlatform>().SetMoving(true);
                }
            }
            else if(isDoor)
                target.GetComponent<Door>().SetMoving(true);
            else
                target.GetComponent<MoveablePlatform>().SetMoving(true);
        }
        
    }

    public override void CharacterNotNearbyAction()
    {
        foreach (GameObject target in _targets)
        {
            if (hasBoth)
            {
                if (target.GetComponent<Door>() != null)
                {
                    target.GetComponent<Door>().SetMoving(false);
                }
                else if (target.GetComponent<MoveablePlatform>() != null)
                {
                    target.GetComponent<MoveablePlatform>().SetMoving(false);
                }
            }
            else if (isDoor)
                target.GetComponent<Door>().SetMoving(false);
            else
                target.GetComponent<MoveablePlatform>().SetMoving(false);
        }
    }
}
