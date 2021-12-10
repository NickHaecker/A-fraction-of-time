using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class InteractionSaveData
{
    public string Type;
    public string Actor;
    public GameObjectSaveData Source;
    public GameObjectSaveData Target;
    public float TimeStamp;
}
