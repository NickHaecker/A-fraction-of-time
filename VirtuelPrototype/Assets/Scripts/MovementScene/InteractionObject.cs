using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    private GameObject triggerObj;

    private bool isCollision;

    private void FixedUpdate()
    {
        if (triggerObj == null && isCollision)
        {
            CharacterNotNearbyAction();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollision = true;
            triggerObj = collision.gameObject;
            CharacterNearbyAction();
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollision = false;
            CharacterNotNearbyAction();
        }

    }

    public abstract void CharacterNearbyAction();

    public abstract void CharacterNotNearbyAction();
}
