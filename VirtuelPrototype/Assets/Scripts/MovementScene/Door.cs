using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Quaternion _addRotation;

    private Quaternion _startRotation;

    private Quaternion _endRotation;

    [SerializeField]
    private float speed = 5f;

    private bool _isMoving;

    private void Start()
    {
        _startRotation = transform.rotation;
        _endRotation = transform.rotation * _addRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_isMoving)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, speed * Time.deltaTime);
        }
        if (!_isMoving)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _startRotation, speed * Time.deltaTime);
        }
    }

    public void SetMoving(bool isMoving)
    {
        _isMoving = isMoving;
    }
}
