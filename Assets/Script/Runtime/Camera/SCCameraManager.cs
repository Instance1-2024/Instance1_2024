using UnityEngine;
using UnityEngine.Events;
public class SCCameraManager : MonoBehaviour
{
    public static SCCameraManager Instance;
    [HideInInspector] public UnityEvent <float, float, float, float> ChangeCamera;
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }
    
}
