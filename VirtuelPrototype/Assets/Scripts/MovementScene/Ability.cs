using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public abstract class Ability : MonoBehaviour
{
    public Action<Interaction> CreateInteraction;

    private void FixedUpdate()
    {
        this.HandleInput();
    }
    protected abstract void HandleInput();

    private void OnTriggerEnter(Collider other)
    {
        HandleCollisionEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        HandleCollisionExit(other);
    }
    protected abstract void HandleCollisionEnter(Collider other);

    protected abstract void HandleCollisionExit(Collider other);
    protected void SubmitAction(InteractionType type, Player player, GameObject gameObject, Transform position, Time time)
    {
        Player current = this.gameObject.GetComponent<Player>();
        if (current.GetCharacterData().IS_SPLIT_REALITY_ORIGIN)
        {
            Interaction interaction = new Interaction().Copy(type, player, gameObject, position, time);
            CreateInteraction?.Invoke(interaction);
        }
    }
}
