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
    public AudioSource _walking;
    public AudioSource _splitting;
    public AudioSource _pressure;
    public AudioSource _pressure2;
    public AudioSource _pressure3;
    public AudioSource _pressure4;
    public AudioSource _pressureSmall;
    public AudioSource _door;
    public AudioSource _door2;
    public AudioSource _woodplank1;
    public AudioSource _woodplank2;
    public AudioSource _woodplank3;
    public AudioSource _woodplank4;
    public AudioSource _woodplank5;
    public AudioSource _woodplank6;
    public AudioSource _woodplank7;
    public AudioSource _woodplank8;
    public AudioSource _woodplank9;
    public AudioSource _woodplank10;


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
    }

    private void playAudio(AudioSource clip, bool ends)
    {
        AudioData newClip = new AudioData(clip, ends);
        newClip.setClipActive(false);
        newClip.setClipActive(true);
    }

    public void playSpecificAudio(string name)
    {
        Debug.Log("play audio for: " + name);
        
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
            case "walking":
                playAudio(_walking, true);
                break;
            case "splitting":
                playAudio(_splitting, true);
                break;
            case "Pressure Plate.001":
                playAudio(_pressure, true);
                break;
            case "Pressure Plate.002":
                playAudio(_pressure2, true);
                break;
            case "Pressure Plate.003":
                playAudio(_pressure3, true);
                break;
            case "Pressure Plate.004":
                playAudio(_pressure4, true);
                break;
            case "Pressure Plate Small":
                playAudio(_pressureSmall, true);
                break;
            case "Door":
                playAudio(_door, true);
                break;
            case "Door.001":
                playAudio(_door2, true);
                break;
            case "Woodplank":
                playAudio(_woodplank1, true);
                break;
            case "Woodplank.001":
                playAudio(_woodplank2, true);
                break;
            case "Woodplank.002":
                playAudio(_woodplank3, true);
                break;
            case "Woodplank.003":
                playAudio(_woodplank4, true);
                break;
            case "Woodplank.004":
                playAudio(_woodplank5, true);
                break;
            case "Woodplank.005":
                playAudio(_woodplank6, true);
                break;
            case "Woodplank.006":
                playAudio(_woodplank7, true);
                break;
            case "Woodplank.007":
                playAudio(_woodplank8, true);
                break;
            case "Woodplank.008":
                playAudio(_woodplank9, true);
                break;
            case "Woodplank.009":
                playAudio(_woodplank10, true);
                break;
        }
    }

    public void stopSpecificAudio(string name)
    {
        switch (name)
        {
            case "walking":
                _walking.enabled = false;
                break;
        }
    }

}
