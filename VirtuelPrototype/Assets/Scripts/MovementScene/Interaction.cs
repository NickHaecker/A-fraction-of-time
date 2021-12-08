using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public InteractionType type;
    public Player actor;
    public GameObject interactedObject;
    public Transform interactionPosition;
    public int timestamp;

    public Interaction Copy(InteractionType i, Player p, GameObject g, Transform t, int ti)
    {
        Interaction interaction = new Interaction();
        interaction.type = i;
        interaction.actor = p;
        interaction.interactedObject = g;
        interaction.interactionPosition = t;
        interaction.timestamp = ti;

        return interaction;
    }
}
