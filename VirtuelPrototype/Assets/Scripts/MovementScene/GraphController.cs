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

        //hier noch todo iterativ
        TimeController.Instance.SetGameTime(_currentTimeline.GetStartTimestamp());
        _playerController.RemoveCharacter(_currentTimeline.GetPlayer());
        Shadow shadow = _playerController.CreateShadow(_currentTimeline.GetPlayer());
        _currentTimeline.InsertGhost(shadow);
        shadow.SetLastTimestamp(_currentTimeline.GetStartTimestamp());

        _currentTimeline = _currentTimeline.GetParent();



    }
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
            CheckForInteractions(gametime,_rootTimeline);
        }
    }

    public bool IsSelectionInTimelineYet(string selection)
    {
        return CheckValidation(_rootTimeline,selection);
    }

    private bool CheckValidation(Timeline timeline,string selection)
    {
        bool valid = false;
        CharacterData data = timeline.GetPlayer();
        if(data)
        {
            if(data.NAME == selection)
            {
                valid = true;
            }
            else
            {
                List<Timeline> children = timeline.GetChildren();

                if(children.Count > 0)
                {
                    foreach(Timeline child in children)
                    {
                        CheckValidation(child,selection);
                    }
                }
            }
        }
        return valid;
    }


    private void CheckForInteractions(float gametime,Timeline timeline)
    {
        if(timeline != null)
        {

            if(timeline.GetLevel() >= _currentTimeline.GetLevel())
            {
                if(!timeline.GetId().Equals(_currentTimeline.GetId()))
                {
                    Shadow ghost = timeline.GetGhost();
                    if(ghost != null)
                    {
                        ghost.ReconstructRecord(gametime);
                    }
                    else
                    {
                        if(timeline.IsTimestampStillValid(gametime))
                        {
                            Shadow shadow = _playerController.CreateShadow(timeline.GetPlayer());
                            timeline.InsertGhost(shadow);
                        }
                    }
                }
            }


            List<Timeline> children = timeline.GetChildren();
            if(children != null && children.Count > 0)
            {
                foreach(Timeline child in children)
                {
                    CheckForInteractions(gametime,child);
                }
            }
        }

    }

}
