using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private static TimeController _instance = null;
    [SerializeField]
    private float _gametime;
    [SerializeField]
    private bool _firstSplitHappenend = false;
    [SerializeField]
    private bool _isActive = false;
    [SerializeField]

    public static TimeController Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
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
