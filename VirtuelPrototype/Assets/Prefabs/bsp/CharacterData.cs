using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
[Serializable]
public class CharacterData : ScriptableObject
{
    public string NAME;
    public string UID;
    public string DESCRIPTION;
    public GameObject PREFAB;
    public GameObject PREFAB_GHOST;
    // public List<Ability> ABILITYS = null;
    public bool IS_SPLIT_REALITY_ORIGIN = false;

}
