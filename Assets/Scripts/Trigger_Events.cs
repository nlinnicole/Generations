
using UnityEngine;

using System.Collections;

[RequireComponent(typeof(Collider))]
public class Trigger_Events : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    public GameObject player;

    public Texture2D fadeTexture;
    [Range(0.1f, 1f)]
    public float fadespeed;
    public int drawDepth = -1000;

    private float alpha = 1f;
    private float fadeDir = -1f;

    void Start()
    {
        SetGazedAt(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
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

    public void movePlayerAnimation() {
        Debug.Log("move");
        player.transform.position = new Vector3(0, 20, 0);

    }

}
