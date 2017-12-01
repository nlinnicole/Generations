using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    public float delay;
    private float startTime;
    private float journeyLength;

    public Animator anim;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time - delay >= 0)
        {
            if (transform.position != endMarker.position)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            }
            else
            {
                anim.SetTrigger("Look");
            }
        }
    }
}
