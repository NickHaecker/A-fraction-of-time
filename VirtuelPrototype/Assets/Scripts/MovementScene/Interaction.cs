using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public InteractionType type;
    public CharacterData actor;
    public GameObject interactedObject;
    public Transform interactionPosition;
    public Time timestamp;

    public Interaction Copy(InteractionType i, CharacterData c, GameObject g, Transform t, Time ti)
    {
        Interaction interaction = new Interaction();
        interaction.type = i;
        interaction.actor = c;
        interaction.interactedObject = g;
        interaction.interactionPosition = t;
        interaction.timestamp = ti;

        return interaction;
    }
}
