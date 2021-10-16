using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Transform checkPointSpawn;
    public GameObject checkPointManager;
    // Start is called before the first frame update
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckpointManager checkpointManagerScript = checkPointManager.GetComponent<CheckpointManager>();
            checkpointManagerScript.CheckpointSelected(this.gameObject.name);
        }
    }
}
