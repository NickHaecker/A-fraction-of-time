using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractionObject
{
    [SerializeField]
    private GameObject _target = null;

    public override void CharacterNearbyAction()
    {
        _target.SetActive(false);
    }

    public override void CharacterNotNearbyAction()
    {
        _target.SetActive(true);
    }
}
