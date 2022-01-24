using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractionObject
{
    [SerializeField]
    private List<GameObject> _targets = null;

    [SerializeField]
    private bool isDoor;

    public override void CharacterNearbyAction()
    {
        AudioPlayerScript.instance.playSpecificAudio(this.name);
        foreach (GameObject target in _targets)
        {
            if(isDoor)
                target.GetComponent<Door>().SetMoving(true);
            else
                target.GetComponent<MoveablePlatform>().SetMoving(true);
        }
        
    }

    public override void CharacterNotNearbyAction()
    {
        foreach (GameObject target in _targets)
        {
            if (isDoor)
                target.GetComponent<Door>().SetMoving(false);
            else
                target.GetComponent<MoveablePlatform>().SetMoving(false);
        }
    }
}
