using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability")]
[Serializable]
public abstract class Ability : MonoBehaviour
{
    public String NAME;
    public String DESCRIPTION;
    protected abstract void HandleInput();
    protected abstract void HandleCollision(GameObject other);

}
