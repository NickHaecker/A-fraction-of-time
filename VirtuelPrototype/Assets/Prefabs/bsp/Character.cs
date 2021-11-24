using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Character", menuName = "Data/Character")]
[Serializable]
public class Character : ScriptableObject
{
    public List<CharacterData> CHARACTER = new List<CharacterData>();
}
