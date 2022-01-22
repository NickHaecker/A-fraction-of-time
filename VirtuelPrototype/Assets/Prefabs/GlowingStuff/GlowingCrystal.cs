using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingCrystal : MonoBehaviour
{
    public Renderer renderer;

    public Material mat;

    public bool isFlickering = true;

    public Color baseColor; 
    public float minemission = 0.9f;
    public float maxemission = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer> ();
        //Material ownMat = renderer.material;
        mat = renderer.material;
        baseColor = mat.color;
        
    }

    void Update () {

        if (isFlickering)
        {
            StartCoroutine(Flickering());
        }
    }

    IEnumerator Flickering()
    {
        isFlickering = false;
        
        float emission = minemission + Mathf.PingPong (Time.time, maxemission - minemission);
        float timeDelay = Random.Range(0.001f, 0.01f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = true;
 
        Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
        renderer.material.SetColor ("_EmissionColor", finalColor);
    }
}
