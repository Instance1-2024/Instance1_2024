using UnityEngine;

public class SCCheckpointComponent : MonoBehaviour
{
    private SCCheckpointManager _checkpointManager;

    private void Start()
    {
        _checkpointManager = SCCheckpointManager.Instance;
    }

    /// <summary>
    /// Invoke ReachCheckpoint event when a checkpoint is Triggered
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _checkpointManager.ReachCheckpoint.Invoke();
            Debug.Log("Triggered");
        }
    }


}
