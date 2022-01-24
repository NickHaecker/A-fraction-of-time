using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 _startPosition;

    [SerializeField]
    private Vector3 _endPosition;

    [SerializeField]
    private float speed = 5f;

    private bool _isMoving;

    private void Start()
    {
        Debug.Log(transform.position + gameObject.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _endPosition, speed * Time.deltaTime);
        }
        if (!_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, speed * Time.deltaTime);
        }
    }

    public void SetMoving(bool isMoving)
    {
        _isMoving = isMoving;
        AudioPlayerScript.instance.playSpecificAudio(this.name);
    }
}
