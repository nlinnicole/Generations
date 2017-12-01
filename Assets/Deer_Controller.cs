using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Controller : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

    }
    void Update()
    {
        if (transform.position != endMarker.position)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
    }
}
