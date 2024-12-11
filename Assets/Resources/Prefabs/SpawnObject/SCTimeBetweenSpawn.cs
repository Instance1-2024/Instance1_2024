using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class TimeBetweenSpawn : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    public Transform SpawnPos;
    public float TimeBS;
    private float _time = 0;
    public float SpawnForce ;
    public Vector3 Direction ;
    public bool DespawnObjectOnDestroy = false;
    [HideInInspector] public bool CanSpawn = true;

    void Update() {
        
        if (!DespawnObjectOnDestroy )
        {
            Spawn1();
        }
        else if (DespawnObjectOnDestroy)
        {
            Spawn2();
        }
    }

    void Spawn1()
    {
        _time -= Time.deltaTime ;

        if (_time <= 0) {
            _time = TimeBS ;

            GameObject prefab = Instantiate ( PrefabToSpawn ,  SpawnPos.position , Quaternion.identity );
            prefab.GetComponent<Rigidbody>().AddForce(Direction.normalized * SpawnForce);

            
            if (prefab.TryGetComponent(out SCLifeSpanOfThisObject lifeComp))
            {
                lifeComp.LifeSpanObject = TimeBS;
                lifeComp.SpawnerScript = GetComponent<TimeBetweenSpawn>() ;
                lifeComp.canDespawn = true;
            }
        }
    }
    void Spawn2()
    {
        if (CanSpawn == true)
        {
            CanSpawn = false;
            GameObject prefab = Instantiate ( PrefabToSpawn ,  SpawnPos.position , Quaternion.identity );
            prefab.GetComponent<Rigidbody>().AddForce(Direction.normalized * SpawnForce);
            
            if (prefab.TryGetComponent(out SCLifeSpanOfThisObject lifeComp))
            {
                lifeComp.SpawnerScript = GetComponent<TimeBetweenSpawn>() ;
            }
        }

    }
}
