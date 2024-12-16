using Script.Runtime.Prophecy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SCProphecyManager : MonoBehaviour
{
    public static SCProphecyManager Instance;
    private List<int> _memoryFragmentsId = new List<int>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Returned an array of all the memory ids that the played picked up
    /// </summary>
    public List<int> GetMemoryIds()
    {
        return _memoryFragmentsId;
    }

    /// <summary>
    /// Add an id to the memory ids
    /// </summary>
    public void AddMemoryPiece(int id)
    {
        if(!_memoryFragmentsId.Contains(id))
            _memoryFragmentsId.Add(id);
    }

    /// <summary>
    /// Update the current level ProphecyUiManager with the saved ids
    /// </summary>
    public void UpdateProphecyUiManager()
    {
        if (_memoryFragmentsId.Count <= 0)
            return;

        foreach(int id in _memoryFragmentsId)
        {
            if(SCProphecyManager.Instance != null)
                SCProphecyUIManager.Instance.GetMemoryEvent.Invoke(id,true);
        }
    }
}
