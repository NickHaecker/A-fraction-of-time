using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelCompleteController : MonoBehaviour
{
    [SerializeField]
    private bool _isTriggerd = false;

    public Action ResetTimeline;

    private void OnTriggerEnter(Collider collider)
    {
        if(!_isTriggerd)
        {
            if(collider.gameObject.tag == "Player")
            {
                _isTriggerd = true;
                ResetTimeline?.Invoke();
            }
        }
    }
}
