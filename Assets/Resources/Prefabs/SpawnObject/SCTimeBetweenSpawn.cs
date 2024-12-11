using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class TimeBetweenSpawn : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    public Transform SpawnPos;
    public float TimeBS;
    private float _time;
    public float SpawnForce ;
    public Vector3 Direction ;

    void Start() 
    {
        _time = TimeBS ;

    }

    void Update() {
        _time -= Time.deltaTime ;

        if (_time <= 0) {
            _time = TimeBS ;

            GameObject prefab = Instantiate ( PrefabToSpawn ,  SpawnPos.position , Quaternion.identity );
            prefab.GetComponent<Rigidbody>().AddForce(Direction.normalized * SpawnForce) ;

            // si la prefab a une durée de vie alors le spawner définie cette durée
            if (prefab.TryGetComponent(out SCLifeSpanOfThisObject lifeComp))
            {
                lifeComp.LifeSpanObject = TimeBS;
                lifeComp.canDespawn = true;
            }
        }
    }
}
