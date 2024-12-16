using UnityEngine;

public class SCCameraMouvement : MonoBehaviour
{
    Transform _transform;
    [SerializeField] Transform _playerTransform;
    SCCameraManager _cameraManager;
    public float SmoothSpeed = 0.125f;
    private float _minX, _maxX, _minY, _maxY ;
    void Start()
    {
        _transform = this.transform  ;
        _cameraManager = SCCameraManager.Instance ;
        SCCameraManager.Instance.ChangeCamera.AddListener(ChangeCameraMinMax);
    }
    void ChangeCameraMinMax(float minX ,float maxX ,float minY ,float maxY )
    {   
        _minX = minX;
        _maxX = maxX;
        _minY = minY;
        _maxY = maxY;

    }
    void Update()
    {

        // on garde le transform en Z car on ne veut pas que la camera soit au même niveau que le joueur
        Vector3 desiredPosition = new Vector3( Mathf.Clamp(_playerTransform.position.x, _minX, _maxX), Mathf.Clamp(_playerTransform.position.y, _minY, _maxY), _transform.position.z);
        
        // on créer le vecteur "Smooth" grace a Lerp et on ajoute * Time.deltatime pour toujours le faire a la même vitesse
        Vector3 smoothPosition = Vector3.Lerp(_transform.position , desiredPosition , SmoothSpeed * Time.deltaTime);
        _transform.position = smoothPosition ; 

    }
}
