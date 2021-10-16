
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] Checkpoints;
    GameObject currentCheckpoint;
    [HideInInspector] public Transform currentLocationToSpawn;
    [HideInInspector] public bool firstCheckpoint = false;
    public void CheckpointSelected(string checkpointName)
    {
        firstCheckpoint = true;
        if(checkpointName != null)
        {
            currentCheckpoint = GameObject.Find(checkpointName);
            Checkpoints checkpointScript = currentCheckpoint.GetComponent<Checkpoints>();
            currentLocationToSpawn = checkpointScript.checkPointSpawn;
        }
    }
}
