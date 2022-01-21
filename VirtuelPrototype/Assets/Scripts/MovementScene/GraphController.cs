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
    [SerializeField]
    private List<Timeline> _timelinesToHandle = new List<Timeline>();


    private bool _splitValidationCheck = false;
    private Timeline _usedTimneline = null;

    void Start()
    {
        _playerController = this.gameObject.GetComponent<PlayerController>();
        _playerController.InitTimeline += HandleInitTimeline;
        _playerController.Split += HandleAddChild;
        _playerController.Merge += HandleMerge;
        _timelinesToHandle = new List<Timeline>();
        CheckForInteractions(_rootTimeline);
    }

    private void HandleMerge(Player player)
    {
        float startPoint = -1;
        Timeline timeline = GetTimelineOfNewPlayer(player.GetCharacterData());

        if(timeline != null)
        {
            List<Timeline> childrenOfNewTimeline = timeline.GetChildren();

            if(childrenOfNewTimeline.Count > 1)
            {
                foreach(Timeline child in childrenOfNewTimeline)
                {
                    if(child.GetId().Equals(_currentTimeline.GetId()))
                    {
                        startPoint = child.GetStartTimestamp();
                    }
                }
            }

            if(childrenOfNewTimeline.Count == 1)
            {
                foreach(Timeline child in childrenOfNewTimeline)
                {
                    startPoint = child.GetStartTimestamp();
                }
            }

            if(startPoint != -1)
            {
                TimeController.Instance.SetGameTime(startPoint);


                _currentTimeline = timeline;
            }

            HandleRemoveOldCharacter(_rootTimeline);
            _timelinesToHandle = new List<Timeline>();
            CheckForInteractions(_rootTimeline);
        }



    }
    private void HandleRemoveOldCharacter(Timeline timeline)
    {
        if(timeline.GetLevel() >= _currentTimeline.GetLevel() && !timeline.GetId().Equals(_currentTimeline.GetId()))
        {
            CharacterData shadow = timeline.GetPlayer();

            int children = this.gameObject.transform.childCount;

            for(int i = 0 ; i < children ; i++)
            {
                GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                if(child.name.Contains(shadow.PREFAB_GHOST.name))
                {
                    Destroy(child);
                }
            }
        }
        else
        {
            List<Timeline> children = timeline.GetChildren();
            if(children.Count > 0)
            {
                foreach(Timeline child in children)
                {
                    HandleRemoveOldCharacter(child);
                }
            }
        }
    }
    private Timeline GetTimelineOfNewPlayer(CharacterData data)
    {
        _usedTimneline = null;
        HandleGetTimelimeOfNewPlayer(_rootTimeline,data);
        return _usedTimneline;
    }
    private void HandleGetTimelimeOfNewPlayer(Timeline timeline,CharacterData data)
    {
        
        if(timeline.GetPlayer().NAME.Equals(data.NAME))
        {
            _usedTimneline = timeline;
        }
        else
        {
            List<Timeline> children = timeline.GetChildren();
            if(children.Count > 0)
            {
                foreach(Timeline child in children)
                {
                    HandleGetTimelimeOfNewPlayer(child,data);
                }
            }
        }
    }
    private void HandleAddChild(CharacterData playerData)
    {
        Timeline timeline = new Timeline(_currentTimeline.GetLevel() + 1,TimeController.Instance.GetGameTime(),playerData,_currentTimeline,_playerController.GetSpawn());
        _currentTimeline.InsertChild(timeline);
        _currentTimeline = timeline;
    }
    private void HandleInitTimeline(CharacterData playerData)
    {
        _rootTimeline = new Timeline(0,TimeController.Instance.GetGameTime(),playerData,null,_playerController.GetSpawn());
        _currentTimeline = _rootTimeline;
    }
    public void HandleGameTime(float gametime)
    {
        PurgeOldTimelines(gametime);

        if(_timelinesToHandle != null && _timelinesToHandle.Count > 0)
        {
            foreach(Timeline timeline in _timelinesToHandle.ToArray())
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
                        if(timeline.GetStartTimestamp() <= gametime)
                        {
                            Shadow shadow = _playerController.CreateShadow(timeline.GetPlayer());
                            Vector3 positionVector = new Vector3(timeline.GetPosition()[0], timeline.GetPosition()[1], timeline.GetPosition()[2]);
                            shadow.gameObject.transform.position = positionVector;
                            
                            timeline.InsertGhost(shadow);
                            shadow.ReconstructRecord(gametime);
                        }
                    }
                }

                if(!timeline.IsTimestampStillValid(gametime))
                {
                    Debug.Log("Zu spät zum tun");
                    _timelinesToHandle.Remove(timeline);

                }

            }
        }
    }

    private void PurgeOldTimelines(float gametime)
    {
        if(_currentTimeline.GetId().Equals(_rootTimeline.GetId()))
        {
            HandlePurge(gametime,_rootTimeline);
        }
    }

    private void HandlePurge(float gametime,Timeline timeline)
    {
        List<Timeline> children = timeline.GetChildren();
        if(children.Count > 0)
        {
            foreach(Timeline child in children.ToArray())
            {
                HandlePurge(gametime,child);
            }
        }
        else
        {
            if(!timeline.IsTimestampStillValid(gametime))
            {
                timeline.GetParent()?.GetChildren()?.Remove(timeline);
            }
        }
    }

    public bool IsSelectionInTimelineYet(string selection)
    {
        _splitValidationCheck = false;
        CheckValidation(_rootTimeline,selection);
        Debug.Log(_splitValidationCheck);
        return _splitValidationCheck;
    }

    private void CheckValidation(Timeline timeline,string selection)
    {
 
        if(selection.Equals(timeline.GetPlayer().NAME))
        {
            _splitValidationCheck = true;
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
    private void HandleResetDataInTimeline(Timeline timeline,string name)
    {
        if(name.Equals(timeline.GetPlayer().NAME))
        {
            timeline.ResetData();
        }
        else
        {
            List<Timeline> children = timeline.GetChildren();
            if(children.Count > 0)
            {
                foreach(Timeline child in children)
                {
                    CheckValidation(child,name);
                }
            }
        }
    }
    public void ResetDataInTimeline(Player player)
    {
        HandleResetDataInTimeline(_rootTimeline,player.GetCharacterData().NAME);
    }


    private void CheckForInteractions(Timeline timeline)
    {


        if(timeline.GetLevel() >= _currentTimeline.GetLevel())
        {
            if(!timeline.GetId().Equals(_currentTimeline.GetId()))
            {
                //Debug.Log("-----------------------------");
                timeline.ResetData();
                _timelinesToHandle.Add(timeline);
            }
        }

        List<Timeline> children = timeline.GetChildren();
        if(children.Count > 0)
        {
            foreach(Timeline child in children)
            {
                CheckForInteractions(child);
            }
        }
    }

}
