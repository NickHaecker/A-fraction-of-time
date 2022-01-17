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

    void Start()
    {
        _playerController = this.gameObject.GetComponent<PlayerController>();
        _playerController.InitTimeline += HandleInitTimeline;
        _playerController.Split += HandleAddChild;
        _playerController.Merge += HandleMerge;
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
            CheckForInteractions(TimeController.Instance.GetGameTime(),_rootTimeline);
        }



    }
    private void HandleRemoveOldCharacter(Timeline timeline)
    {
        Debug.Log("hier bin ich");

        if(timeline.GetLevel() >= _currentTimeline.GetLevel() && !timeline.GetId().Equals(_currentTimeline.GetId()))
        {
            Debug.Log("zweiter schritt " +timeline.GetId());
            CharacterData shadow = timeline.GetPlayer();

            int children = this.gameObject.transform.childCount;

            for(int i = 0 ; i < children ; i++)
            {
                GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                if(child.name.Contains(shadow.PREFAB_GHOST.name))
                {
                    Debug.Log("dritter schritt " + child.name);
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
        return HandleGetTimelimeOfNewPlayer(_rootTimeline,data);
    }
    private Timeline HandleGetTimelimeOfNewPlayer(Timeline timeline, CharacterData data)
    {
        Timeline t = null;
        if(timeline.GetPlayer().NAME.Equals(data.NAME))
        {
            t = timeline;
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

        return t;
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
        //if(_rootTimeline != null)
        //{
        //    CheckForInteractions(gametime,_rootTimeline);
        //}
        foreach(Timeline timeline in _timelinesToHandle)
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
                    if(timeline.GetStartTimestamp() == gametime)
                    {
                        Shadow shadow = _playerController.CreateShadow(timeline.GetPlayer());
                        shadow.gameObject.transform.position = new Vector3(timeline.GetPosition()[0],timeline.GetPosition()[1],timeline.GetPosition()[2]);
                        //Debug.Log(shadow.GetCharacterData().NAME + " sollte erstellt worden sein");
                        timeline.InsertGhost(shadow);
                        shadow.ReconstructRecord(gametime);
                    }
                }
            }
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
                    _timelinesToHandle.Add(timeline);
                    //Shadow ghost = timeline.GetGhost();
                    //if(ghost != null)
                    //{
                    //    ghost.ReconstructRecord(gametime);
                    //}
                    //else
                    //{
                    //    if(timeline.IsTimestampStillValid(gametime))
                    //    {
                    //        if(timeline.GetStartTimestamp() == gametime)
                    //        {
                    //            Shadow shadow = _playerController.CreateShadow(timeline.GetPlayer());
                    //            shadow.gameObject.transform.position = new Vector3(timeline.GetPosition()[0],timeline.GetPosition()[1],timeline.GetPosition()[2]);
                    //            //Debug.Log(shadow.GetCharacterData().NAME + " sollte erstellt worden sein");
                    //            timeline.InsertGhost(shadow);
                    //            //shadow.ReconstructRecord(gametime);
                    //        }
                    //    }
                    //}
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
