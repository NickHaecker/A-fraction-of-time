using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Interaction
{
    public InteractionType type;
    public Player actor;
    public GameObject interactedObject;
    public Transform interactionPosition;
    public float timestamp;

    public Interaction(InteractionType i, Player p, GameObject g, Transform t, float ti)
    {
        //Interaction interaction = new Interaction();
        type = i;
        actor = p;
        interactedObject = g;
        interactionPosition = t;
        timestamp = ti;

        //return interaction;
    }
}
