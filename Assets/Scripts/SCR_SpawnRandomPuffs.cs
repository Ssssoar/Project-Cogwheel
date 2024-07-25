using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_SpawnRandomPuffs : MonoBehaviour{
    [Header("References")]
    public Image toCheck;

    [Header("Prefabs")]
    public GameObject toSpawn;
    public GameObject silentSpawn;

    [Header("Variables")]
    public Vector2 mins;
    public Vector2 maxs;
    public int numberToSpawn;
    public bool spawnHere = false;
    

    [ContextMenu("Spawn")] 
    public void Spawn(){
        if ((toCheck != null) && (toCheck.color.a == 0f)) return; //this to signal that there's been no high score
        for (int i = 0; i < numberToSpawn; i++){
            Vector3 spawnPos = Vector3.zero;
            if (spawnHere){
                spawnPos = transform.position;
            }else{
                spawnPos = new Vector3(
                    Random.Range(mins.x , maxs.x) ,
                    Random.Range(mins.y , maxs.y) , 
                    0f
                );
                spawnPos += transform.parent.position;
            }
            if (i < 6 )
                Instantiate(toSpawn, spawnPos, Quaternion.identity,transform.parent); 
            else
                Instantiate(silentSpawn, spawnPos, Quaternion.identity,transform.parent);
        }
    }
}
