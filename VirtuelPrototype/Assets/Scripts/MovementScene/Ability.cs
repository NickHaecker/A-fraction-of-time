using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability")]
[Serializable]
public abstract class Ability : ScriptableObject
{
    public String NAME;
    public String DESCRIPTION;
    protected abstract void HandleInput(Input input);
    protected abstract void HandleCollision(GameObject other);

}
