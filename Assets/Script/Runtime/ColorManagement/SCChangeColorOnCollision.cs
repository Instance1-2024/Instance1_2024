using Script.Runtime;
using Script.Runtime.ColorManagement;
using UnityEngine;

public class SCChangeColorOnCollision : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    /// <summary>
    /// Destroy the gameObject on any collision and if this collision is with a black or white platform then it change it's actual color
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other) {
        

        if (other.gameObject.CompareTag("Platform")) {

            Debug.Log("Styx Drop hit");

            // to avoid having to GetComponent multiple time
            SCChangeColor plat = other.gameObject.GetComponentInParent<SCChangeColor>();
            Debug.Log(plat);

            if (plat.GetColor() == SCEnum.EColor.White)
                plat.ChangeColor(SCEnum.EColor.Black);

            else if (plat.GetColor() == SCEnum.EColor.Black)
                plat.ChangeColor(SCEnum.EColor.White);

        }

        Destroy(this.gameObject);
    }
}