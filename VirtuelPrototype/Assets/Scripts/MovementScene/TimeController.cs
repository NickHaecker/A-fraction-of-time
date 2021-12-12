using UnityEngine;

public sealed class TimeController : MonoBehaviour
{
    private static TimeController _instance = null;
    private float _gametime;
    private bool _firstSplitHappenend = false;
    private bool _isActive = false;

    private TimeController()
    {
    }

    public static TimeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TimeController();
            }
            return _instance;
        }
    }
    // Update is called once per frame
    public void Update()
    {
        if(this._firstSplitHappenend && this._isActive)
        {
            _gametime += Time.deltaTime;
        }
    }

    public void FirstSplit()
    {
        this._firstSplitHappenend = true;
        this._isActive = true;
    }

    public void SetActive(bool active)
    {
        this._isActive = active;
    }
    public void SetGameTime(float timestamp) {
        _gametime = timestamp;
    }

    public float GetGameTime() {
        return _gametime;
    }
}
