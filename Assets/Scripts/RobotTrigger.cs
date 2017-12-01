
using UnityEngine;

using System.Collections;
using System.Timers;

[RequireComponent(typeof(Collider))]
public class RobotTrigger : MonoBehaviour
{
    //public Material inactiveMaterial;
    //public Material gazedAtMaterial;
    private float timer;
    public float gazeTime = 2f;
    public GameObject player;
    public bool isGazedAt = false;

    public GameObject[] animalEvents = new GameObject[4];
    public Transform[] robotLocations = new Transform[4];

    private int eventIndex = -1;
    private Vector3 playerPos = new Vector3(0, 20, 0);

    void Start()
    {
        //Disable animals at start
        SetGazedAt(false);
        for (int i = 0; i < animalEvents.Length; ++i) {
            animalEvents[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (isGazedAt)
        {
            timer += Time.deltaTime;
            if (timer >= gazeTime)
            {
                transform.position = new Vector3(-5, 20, -2);
                if (player.transform.position != playerPos) {
                    player.transform.position = playerPos;
                }
                timer = 0;
                ++eventIndex;
                if (eventIndex >= 0 && eventIndex < animalEvents.Length) {
                    animalEvents[eventIndex].SetActive(true);
                    transform.position = robotLocations[eventIndex].position;
                    transform.rotation = robotLocations[eventIndex].rotation;
                }

            }
        } else
        {
            timer = 0;
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        isGazedAt = gazedAt;
        //if (inactiveMaterial != null && gazedAtMaterial != null)
        //{
        //    GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        //    return;
        //}
        //GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
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

}
