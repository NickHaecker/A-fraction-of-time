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
    //// Start is called before the first frame update
    void Start()
    {
        _playerController = this.gameObject.GetComponent<PlayerController>();
        _playerController.InitTimeline += HandleInitTimeline;
        _playerController.Split += HandleAddChild;
        _playerController.Merge += HandleMerge;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void HandleMerge()
    {
        TimeController.Instance.SetGameTime(_currentTimeline.GetStartTimestamp());
        _playerController.RemoveCharacter(_currentTimeline.GetPlayer());
        Player shadow = _playerController.CreateShadow(_currentTimeline.GetPlayer());
        _currentTimeline.InsertGhost(shadow);
        shadow.SetLastTimestamp(_currentTimeline.GetStartTimestamp());
        /*  foreach (var interaction in _currentTimeline.GetGhost().GetInteractions())
          {
              Debug.Log("time :" + interaction.timestamp);
          }*/
        _currentTimeline = _currentTimeline.GetParent();
        //shadow.StartShadowing(true);
        //shadow.InsertInteractions(Utils.ConvertInteractions(shadow.Interactions,_playableCharacter));
    }
    private void HandleAddChild(CharacterData player)
    {
        Timeline timeline = new Timeline(_currentTimeline.GetLevel() + 1,TimeController.Instance.GetGameTime(),player,_currentTimeline);
        _currentTimeline.InsertChild(timeline);
        _currentTimeline = timeline;
    }
    private void HandleInitTimeline(CharacterData player)
    {
        _rootTimeline = new Timeline(0,TimeController.Instance.GetGameTime(),player,null);
        _currentTimeline = _rootTimeline;
    }
    public void HandleGameTime(float gametime)
    {
        if(_currentTimeline != null)
        {
            //if(_currentTimeline.GetParent() != null && _currentTimeline.GetParent().GetChildren().Count > 1)
            //{

            //}
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
        //if()

    }
}
