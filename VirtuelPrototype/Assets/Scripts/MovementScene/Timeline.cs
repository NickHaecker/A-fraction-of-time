using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Timeline
{
    private int _level = 0;
    private List<Timeline> _children = new List<Timeline>();
    private float _startTimestamp;
    private CharacterData _player;
    private Timeline _parent = null;
    private Player _ghost = null;
    private string _ID;

    public Timeline(int level, float timestamp,CharacterData player, Timeline timeline)
    {
        _level = level;
        _startTimestamp = timestamp;
        _player = player;
        _parent = timeline;
        string parentId = "";
        if(timeline != null)
        {
            parentId = timeline.GetId();
        }
        _ID = level.ToString() + parentId +  "_" + player.NAME;
    }
    //public get level
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
    public void InsertGhost(Player ghost)
    {
        _ghost = ghost;
    }
    public Player GetGhost()
    {
        return _ghost;
    }
    public string GetId()
    {
        return _ID;
    }
}
