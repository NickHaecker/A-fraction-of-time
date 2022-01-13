using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Timeline
{
    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private List<Timeline> _children = new List<Timeline>();
    [SerializeField]
    private float _startTimestamp;
    [SerializeField]
    private CharacterData _player;
    [SerializeField]
    private Timeline _parent = null;
    [SerializeField]
    private Shadow _ghost = null;
    [SerializeField]
    private string _ID;
    [SerializeField]
    private float[] _startPosition = new float[3];

    public Timeline(int level,float timestamp,CharacterData playerData,Timeline timeline, Transform startPosition)
    {
        _level = level;
        _startTimestamp = timestamp;
        _player = playerData;
        _parent = timeline;
        string parentId = "";
        if(timeline != null)
        {
            parentId = timeline.GetId();
        }
        _ID = level.ToString() + parentId + "_" + _player.NAME;
        _startPosition[0] = startPosition.position.x;
        _startPosition[1] = startPosition.position.y;
        _startPosition[2] = startPosition.position.z;
    }

    public int GetLevel()
    {
        return _level;
    }
    public List<Timeline> GetChildren()
    {
        return _children;
    }
    public float GetStartTimestamp()
    {
        return _startTimestamp;
    }
    public CharacterData GetPlayer()
    {
        return _player;
    }
    public Timeline GetParent()
    {
        return _parent;
    }
    public void InsertChild(Timeline child)
    {
        if(!_children.Contains(child))
        {
            _children.Add(child);
        }
        else
        {
            _children.Insert(_children.FindIndex(a => a.GetId() == child.GetId()),child);
        }
    }
    public void InsertGhost(Shadow ghost)
    {
        _ghost = ghost;
        _ghost.DestroyShadow += DeleteGhost;
    }
    public Shadow GetGhost()
    {
        return _ghost;
    }
    public string GetId()
    {
        return _ID;
    }
    private void DeleteGhost()
    {
        _ghost.DestroyShadow -= DeleteGhost;
        _ghost = null;

    }
    public bool IsTimestampStillValid(float timestamp)
    {
        SavePlayerData shadowData = StateManager.LoadPlayer(_player.NAME);
        if(shadowData != null)
        {
            if(shadowData.Interactions.Count > 0)
            {
                return shadowData.Interactions[shadowData.Interactions.Count - 1].TimeStamp >= timestamp;
            }
        }
        return false;
    }
    public float[] GetPosition()
    {
        return _startPosition;
    }

}
