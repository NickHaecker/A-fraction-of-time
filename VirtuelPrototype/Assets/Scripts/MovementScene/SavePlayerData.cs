using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SavePlayerData
{
    public string Name;
    public string UID;
    public List<InteractionSaveData> Interactions = new List<InteractionSaveData>();
    public GameObjectSaveData Position;
}
