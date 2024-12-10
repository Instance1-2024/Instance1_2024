using JetBrains.Annotations;
using System;
using UnityEngine;
using static Script.Runtime.SCEnum;


public struct PlayerDataRespawn
{
    Vector3 _lastCheckpoint;
    Quaternion _rotation;
    EColor _color;
    bool isHandle;
}
public class SCPlayerDeath : MonoBehaviour
{
    private void Start()
    {
        SCCheckpointManager.Instance.ReachCheckpoint.AddListener(OnDeath);
        
    }

    private void OnDeath()
    {
        throw new NotImplementedException();
    }
}
