using UnityEngine;
using UnityEngine.Events;

public class SCChangeCameraOnTrigger : MonoBehaviour
{
    public float MinX, MaxX, MinY, MaxY ;
    private SCCameraManager _cameraManager;
    void Start() 
    {
        _cameraManager = SCCameraManager.Instance;
    }   
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player detected");
            _cameraManager.ChangeCamera.Invoke( MinX, MaxX, MinY, MaxY);
        }
    }
}
