using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class GraphController : Controller
{
    [SerializeField]
    private Timeline _rootTimeline;
    [SerializeField]
    private Timeline _currentTimeline;

    [SerializeField]
    private PlayerController _playerController = null;

    void Start()
    {
        _playerController = this.gameObject.GetComponent<PlayerController>();
        _playerController.InitTimeline += HandleInitTimeline;
        _playerController.Split += HandleAddChild;
        _playerController.Merge += HandleMerge;
    }

    private void HandleMerge()
    {
        TimeController.Instance.SetGameTime(_currentTimeline.GetStartTimestamp());
        _playerController.RemoveCharacter(_currentTimeline.GetPlayer());
        Shadow shadow = _playerController.CreateShadow(_currentTimeline.GetPlayer());
        _currentTimeline.InsertGhost(shadow);
        shadow.SetLastTimestamp(_currentTimeline.GetStartTimestamp());

        _currentTimeline = _currentTimeline.GetParent();

    }
    //private void RemoveTimeline(GameObject current)
    //{

    //}
    private void HandleAddChild(CharacterData playerData)
    {
        Timeline timeline = new Timeline(_currentTimeline.GetLevel() + 1,TimeController.Instance.GetGameTime(),playerData,_currentTimeline);
        _currentTimeline.InsertChild(timeline);
        _currentTimeline = timeline;
    }
    private void HandleInitTimeline(CharacterData playerData)
    {
        _rootTimeline = new Timeline(0,TimeController.Instance.GetGameTime(),playerData,null);
        _currentTimeline = _rootTimeline;
    }
    public void HandleGameTime(float gametime)
    {
        if(_currentTimeline != null)
        {

            if(_currentTimeline.GetChildren().Count != 0)
            {
                foreach(Timeline child in _currentTimeline.GetChildren())
                {
                    if(child.GetGhost() != null)
                    {
                        child.GetGhost().ReconstructRecord(gametime);
                    }
                }
            }
        }


    }
}
