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
        
        //permet de choisir la manière de sapwn un objet
        if (!DespawnObjectOnDestroy )
        {
            Spawn1();
        }
        else if (DespawnObjectOnDestroy)
        {
            Spawn2();
        }
    }
    /// <summary>
    /// Ce spawn instantie un objet et le détruit toute les TimeBS
    /// </summary>
    void Spawn1()
    {
        //le timer qui commence a 0 par defauty ce qui fait spawn instantanément le premier objet
        _time -= Time.deltaTime ;

        if (_time <= 0) {
            //reset le timer
            _time = TimeBS ;

            //Instantie la prefab préciser dans l'éditeur
            GameObject prefab = Instantiate ( PrefabToSpawn ,  SpawnPos.position , Quaternion.identity );

            //ajoute une force au spawn de l'objet
            prefab.GetComponent<Rigidbody>().AddForce(Direction.normalized * SpawnForce);

            //ajoute le script de durée de vie au objet instantier
            SCLifeSpanOfThisObject lifeComp = prefab.AddComponent<SCLifeSpanOfThisObject>();
            
            //donne le temp et le fait que le l'objet peut despawn
            lifeComp.LifeSpanObject = TimeBS;
            lifeComp.canDespawn = true;
            
            //ajoute une référence a ce script pour l'objet instantier
            lifeComp.SpawnerScript = GetComponent<TimeBetweenSpawn>() ;

        }
    }
    /// <summary>
    /// Ce spawn instantie un objet quand CanSpawn == true ce qui est le cas au start et quand le précédent objet est détruit
    /// </summary>
    void Spawn2()
    {
        if (CanSpawn == true)
        {
            //empeche de faire spawn un objet tant que le précedent n'est pas détruit et donc renvoie dans OnDestroy : CanSpawn = true
            CanSpawn = false;

            //Instantie la prefab préciser dans l'éditeur
            GameObject prefab = Instantiate ( PrefabToSpawn ,  SpawnPos.position , Quaternion.identity );

            //ajoute une force au spawn de l'objet
            prefab.GetComponent<Rigidbody>().AddForce(Direction.normalized * SpawnForce);

            //ajoute le script de durée de vie au objet instantier
            SCLifeSpanOfThisObject lifeComp = prefab.AddComponent<SCLifeSpanOfThisObject>();
            
            //ajoute une référence a ce script pour l'objet instantier
            lifeComp.SpawnerScript = GetComponent<TimeBetweenSpawn>() ;
        }

    }
}
