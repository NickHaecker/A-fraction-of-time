using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineVisualiser : MonoBehaviour
{
    public float speed;

    public float seconds;

    public GameObject normalTimeline;
    public GameObject splittimeline;
    public GameObject mergetimeline;

    public float[] lineTwo = new float[] { 13, 22 };
    public bool twoIsSplitted = false;

    public float[] lineThree = new float[] { 17, 20 };
    public bool threeIsSplitted = false;

    private void Start()
    {
        speed = 3;
        seconds = 1;

        //splittimeline = Resources.Load("Assets/Prefabs/Timeline/split") as GameObject;
        //normalTimeline = Resources.Load("Assets/Prefabs/Timeline/normal") as GameObject;
        //mergetimeline = Resources.Load("Assets/Prefabs/Timeline/merge") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(-speed * Time.deltaTime, 0, 0);
        if(Time.realtimeSinceStartup > seconds)
        {
            Debug.Log(seconds);
            GameObject newClone = GameObject.Instantiate(normalTimeline, this.transform);
            newClone.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y, this.transform.position.z);
            if (twoIsSplitted && lineTwo[1] == seconds)
            {
                GameObject newClone3 = GameObject.Instantiate(mergetimeline, this.transform);
                newClone3.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y, this.transform.position.z);
                twoIsSplitted = false;
            }
            if (twoIsSplitted)
            {
                GameObject newClone2 = GameObject.Instantiate(normalTimeline, this.transform);
                newClone2.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y + 2.8f, this.transform.position.z);
            }
            if (lineTwo[0] == seconds && !twoIsSplitted)
            {
                GameObject newClone1 = GameObject.Instantiate(splittimeline, this.transform);
                newClone1.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y, this.transform.position.z);
                twoIsSplitted = true;
            }

            if (threeIsSplitted && lineThree[1] == seconds)
            {
                GameObject newClone3 = GameObject.Instantiate(mergetimeline, this.transform);
                newClone3.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y + 2.8f, this.transform.position.z);
                threeIsSplitted = false;
            }
            if (threeIsSplitted)
            {
                GameObject newClone2 = GameObject.Instantiate(normalTimeline, this.transform);
                newClone2.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y + 5.6f, this.transform.position.z);
            }
            if (lineThree[0] == seconds && !threeIsSplitted)
            {
                GameObject newClone1 = GameObject.Instantiate(splittimeline, this.transform);
                newClone1.transform.position = new Vector3(this.transform.position.x + seconds * 3, this.transform.position.y + 2.8f, this.transform.position.z);
                threeIsSplitted = true;
            }


            seconds++;
        }
        
    }
}
