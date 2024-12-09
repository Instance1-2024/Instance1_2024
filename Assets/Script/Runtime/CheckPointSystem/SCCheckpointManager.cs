using UnityEngine;
using UnityEngine.Events;

public class SCCheckpointManager : MonoBehaviour
{
    public UnityEvent ReachCheckpoint;
    public static SCCheckpointManager Instance;


    /// <summary>
    /// Initialize Instance of the Checkpoint Manager
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    /// <summary>
    /// Initialize ReachCheckpoint Event
    /// </summary>
    void Start()
    {
        if (ReachCheckpoint == null)
            ReachCheckpoint = new UnityEvent();
    }

}
