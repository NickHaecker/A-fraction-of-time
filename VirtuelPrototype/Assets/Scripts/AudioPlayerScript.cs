using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerScript : MonoBehaviour
{
    public static AudioPlayerScript instance = null;

    public AudioSource _background;
    public AudioSource _openUI;
    public AudioSource _closeUI;
    public AudioSource _info1;


    private List<AudioData> _runningAudios;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playSpecificAudio("background");
    }

    // Update is called once per frame
    void Update()
    {
        /*foreach (AudioData audio in _runningAudios)
        {
            if (audio.GetShouldStop())
            {
                audio.setClipActive(false);
                _runningAudios.Remove(audio);
            }
        }*/
    }

    private void playAudio(AudioSource clip, bool ends)
    {
        AudioData newClip = new AudioData(clip, ends);
        newClip.setClipActive(false);
        newClip.setClipActive(true);
    }

    public void playSpecificAudio(string name)
    {
        switch(name)
        {
            case "background":
                playAudio(_background, false);
                break;
            case "openUI":
                playAudio(_openUI, true);
                break;
            case "closeUI":
                playAudio(_closeUI, true);
                break;
            case "info1":
                playAudio(_info1, true);
                break;
        }
    }
}
