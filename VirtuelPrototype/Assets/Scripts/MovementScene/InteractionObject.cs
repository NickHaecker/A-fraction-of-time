using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
            CharacterNearbyAction();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
            CharacterNotNearbyAction();
    }

    public abstract void CharacterNearbyAction();

    public abstract void CharacterNotNearbyAction();
}
