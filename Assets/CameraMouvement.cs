using UnityEngine;

public class CameraMouvement : MonoBehaviour
{
    Transform _transform;
    [SerializeField] Transform _playerTransform;

    public float minY, maxY , minX, maxX;

    public float SmoothSpeed = 0.125f;
    void Start()
    {
        _transform = this.transform  ;
    }
    void Update()
    {

        // on garde le transform en Z car on ne veut pas que la camera soit au même niveau que le joueur
        Vector3 desiredPosition = new Vector3( Mathf.Clamp(_playerTransform.position.x, minX, maxX) , Mathf.Clamp(_playerTransform.position.y, minY, maxY), _transform.position.z);
        
        // on créer le vecteur "Smooth" grace a Lerp et on ajoute * Time.deltatime pour toujours le faire a la même vitesse
        Vector3 smoothPosition = Vector3.Lerp(_transform.position , desiredPosition , SmoothSpeed * Time.deltaTime);
        _transform.position = smoothPosition ; 

    }
}
