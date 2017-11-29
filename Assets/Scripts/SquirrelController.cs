using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    public Transform midMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength1;
    private float journeyLength2;

    private float timer = 0;
    private float timerMax = 0;
    private bool reachedMid = false;
    private bool looked = false;

    public Animator anim;

    void Start()
    {
        startTime = Time.time;
        journeyLength1 = Vector3.Distance(startMarker.position, midMarker.position);
        journeyLength2 = Vector3.Distance(midMarker.position, endMarker.position);

        anim = GetComponent<Animator>();
    }
    void Update()
    {
         if (reachedMid == false)
        {
            if (transform.position != midMarker.position)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength1;
                transform.position = Vector3.Lerp(startMarker.position, midMarker.position, fracJourney);
            }
            else
            {
                if (looked == false) {
                    Debug.Log("Looking");
                    anim.SetTrigger("Look");
                    looked = true;
                }
                else
                {
                    anim.SetTrigger("Run");
                }
                if (!Waited(0.5f))
                {
                    return;
                }
                reachedMid = true;
                startTime = Time.time;
            }
        }
        else
        {
            anim.SetTrigger("Run");
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength2;
            transform.position = Vector3.Lerp(midMarker.position, endMarker.position, fracJourney);
        }
      
    }

    private bool Waited(float seconds) {
        timerMax = seconds;
        timer += Time.deltaTime;
        if (timer >= timerMax) {
            return true;
        }
        return false;
    }
}
