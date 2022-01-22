using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Quaternion _startRotation;

    [SerializeField]
    private Quaternion _endRotation;

    [SerializeField]
    private float speed = 5f;

    private bool _isMoving;

    private void Start()
    {
        Debug.Log(transform.rotation);
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
