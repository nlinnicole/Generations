
using UnityEngine;

using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class RobotTrigger : MonoBehaviour
{
    //public Material inactiveMaterial;
    //public Material gazedAtMaterial;
    private float timer;
    public float gazeTime = 2f;
    public GameObject player;
    public GameObject robot;
    public bool isGazedAt = false;

    public GameObject[] animalEvents = new GameObject[4];
    public Transform[] robotLocations = new Transform[4];
    public GameObject[] speechCanvas = new GameObject[5];

    private int eventIndex = -1;
    private Vector3 playerPos = new Vector3(0, 20, 0);

    public Transform destination;
    public float journeyTime = 2F;

    private Vector3 start;
    private float startTime;
    private float speechTime;

    void Start()
    {
        for (int i = 0; i < speechCanvas.Length; ++i){
            speechCanvas[i].GetComponent<Canvas>().enabled = false;
        }

        //Disable animals at start
        SetGazedAt(false);
        for (int i = 0; i < animalEvents.Length; ++i) {
            animalEvents[i].SetActive(false);
        }

        start = transform.position;
        startTime = Time.time;

        //enable the first canvas
        speechCanvas[4].GetComponent<Canvas>().enabled = true;
        Speak();
    }

    private void Update()
    {
        if (!robot.GetComponent<AudioSource>().mute)
        {
            speechTime++;
        }
        if (isGazedAt && (eventIndex == -1 || robotLocations[eventIndex].position == transform.position))
        {
          
            timer += Time.deltaTime;
            if (timer >= gazeTime)
            {
                for (int i = 0; i < speechCanvas.Length; ++i)
                {
                    speechCanvas[i].GetComponent<Canvas>().enabled = false;
                }
                robot.GetComponent<AudioSource>().mute = true;
                //teleports player at beginning
                if (player.transform.position != playerPos) {
                    player.transform.position = playerPos;
                }
                timer = 0;
                ++eventIndex;
                if(eventIndex >= robotLocations.Length)
                {
                    Debug.Log("!");
                    eventIndex = -1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }
                Debug.Log(eventIndex);
                //activate speech
                if (eventIndex >=0 && eventIndex < speechCanvas.Length)
                {
                     speechCanvas[eventIndex].GetComponent<Canvas>().enabled = true;
                     Speak();
                }
                //Activate animal

                if (eventIndex >= 0 && eventIndex < animalEvents.Length)
                {
                            animalEvents[eventIndex].SetActive(true);                   
                }

                setNewDestination(robotLocations[eventIndex]);
            }
        }
        else
        {
            timer = 0;
        }
        if (speechTime == 250)
        {
            robot.GetComponent<AudioSource>().mute = true;
            speechTime = 0;
        }

        //Controls events
        if (eventIndex == 0)
        {
            player.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
            player.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
            transform.position = robotLocations[eventIndex].position;
        }
        else if (eventIndex > 0 && eventIndex < robotLocations.Length)
        {
            
            Vector3 center = (destination.position + start) * 0.5F;
            center -= new Vector3(0, 0, 1);
            Vector3 riseRelCenter = destination.position - center;
            Vector3 setRelCenter = start - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(setRelCenter, riseRelCenter, fracComplete);
            transform.position += center;
            transform.LookAt(player.transform);
        }
    }

    public void setNewDestination(Transform newDest) {
        start = transform.position;
        startTime = Time.time;
        destination = newDest;
    }

    public void SetGazedAt(bool gazedAt)
    {
        isGazedAt = gazedAt;
    }

    public void Recenter()
    {
#if !UNITY_EDITOR
    GvrCardboardHelpers.Recenter();
#else
        GvrEditorEmulator emulator = FindObjectOfType<GvrEditorEmulator>();
        if (emulator == null)
        {
            return;
        }
        emulator.Recenter();
#endif  // !UNITY_EDITOR
    }

    public void movePlayerAnimation()
    {
        Debug.Log("move");
        player.transform.position = new Vector3(0, 20, 0);

    }
    public void Speak()
    {
        robot.GetComponent<AudioSource>().mute = false;
 
    }
}
