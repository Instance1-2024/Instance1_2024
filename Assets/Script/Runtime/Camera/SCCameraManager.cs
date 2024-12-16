using UnityEngine;
using UnityEngine.Events;
public class SCCameraManager : MonoBehaviour
{
    public static SCCameraManager Instance;
    public float MinX, MaxX, MinY, MaxY ;
    [HideInInspector] public UnityEvent <float, float, float, float> ChangeCamera;
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }
    void Start()
    {
        ChangeCamera.AddListener(ChangeCameraMinMax);
    }
    void ChangeCameraMinMax(float minX ,float maxX ,float minY ,float maxY )
    {
        Debug.Log(minX + maxX + minY + maxY);
        
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;

    }
}
