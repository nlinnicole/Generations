
using UnityEngine;

using System.Collections;
using System.Timers;

[RequireComponent(typeof(Collider))]
public class RobotTrigger : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material gazedAtMaterial;
    private float timer;
    public float gazeTime = 2f;
    public GameObject player;
    public bool isGazedAt = false;

    public GameObject[] animalEvents = new GameObject[3];

    private int animalEventIndex = -2;

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
                player.transform.position = new Vector3(0, 20, 0);
                transform.position = new Vector3(-10, 20, -7);
                timer = 0;
                ++animalEventIndex;
                if (animalEventIndex >= 0) {
                    animalEvents[animalEventIndex].SetActive(true);
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
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
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
