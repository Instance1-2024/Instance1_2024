using UnityEngine;

public class SCLifeSpanOfThisObject : MonoBehaviour
{
    public float LifeSpanObject = 10f;
    private float _time;
    public bool canDespawn = false;
    public TimeBetweenSpawn SpawnerScript;
    void Start() 
    {
        _time = LifeSpanObject ;
    }
   void Update() 
   {
        _time -= Time.deltaTime ;

        if (_time <= 0  && canDespawn == true) 
        {
            Destroy(gameObject);
        }
   }

   void OnDestroy() 
   {
    SpawnerScript.CanSpawn = true;
   }
}
