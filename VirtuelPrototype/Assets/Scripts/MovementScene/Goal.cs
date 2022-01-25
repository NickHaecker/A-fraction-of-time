using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject _uI = null;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _uI.GetComponent<UIHandler>().ShowEndscreen();
        }
    }
}
