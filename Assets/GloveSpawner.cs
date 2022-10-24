using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GloveSpawner : MonoBehaviour
{
    public GameObject GlovePrefab;
    public GameObject GloveSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        SpawnGlove();
    }
    public void SpawnGlove()
    {
        GameObject TempGlove = Instantiate(GlovePrefab, GloveSpawnLocation.transform);
        TempGlove.GetComponent<XRGrabInteractable>().selectEntered.AddListener(delegate { SpawnGlove(); });
    }
}
