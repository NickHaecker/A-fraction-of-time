using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameObjectSaveData
{
    public float[] Position = new float[3];
    public float[] Scale = new float[3];
    public float[] Rotation = new float[3];
    public string Name;
    public string Tag;
    public int Layer;
}
