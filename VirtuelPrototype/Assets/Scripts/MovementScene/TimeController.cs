using UnityEngine;
using System;
[Serializable]
public class TimeController : Controller
{
    [SerializeField]
    private static TimeController _instance = null;

    [SerializeField]
    private GraphController _graphController = null;

    [SerializeField]
    private float _gametime;

    [SerializeField]
    private bool _isCounting = false;
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
    private void Start()
    {
        _graphController = this.gameObject.GetComponent<GraphController>();
        SetActive(true);
    }

    public void FixedUpdate()
    {
        if(_isCounting)
        {

            _graphController.HandleGameTime(_gametime);

            _gametime += Time.deltaTime;

        }
    }

    public void FirstSplit()
    {

        _isCounting = true;
    }

    public void SetActive(bool active)
    {
        _isCounting = active;
    }
    public void SetGameTime(float timestamp)
    {
        _gametime = timestamp;
    }

    public float GetGameTime()
    {
        return _gametime;
    }
}
