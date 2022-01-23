using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour
{
    private AudioSource _audio;
    private bool _ends;
    public AudioData(AudioSource audio, bool ends)
    {
        this._audio = audio;
        this._ends = ends;
    }

    public AudioSource GetClip()
    {
        return this._audio;
    }

    public bool GetShouldStop()
    {
        return this._ends;
    }

    public void setClipActive(bool b)
    {
        this._audio.enabled = b;
    }
}
