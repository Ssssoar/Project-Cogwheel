using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SCR_SpawnLevelList : MonoBehaviour{
    [Header("References")]
    public GameObject buttonPrefab;
    public GameObject firstLevel;
    public GameObject puff;
    public GameObject medal;
    public Transform attractor;
    public float timeBetweenSpawns;

    GameObject currentlySpawning = null;
    GameObject spawnedButton = null;
    float timer = 0f;
    bool spawning = false;
    int count = 0;
    public SCR_Medallion pendingMedal;
    public string pendingMedalName;

    [ContextMenu("SpawnButtons")]
    public void SpawnList(){
        count = 0;
        spawning = true;
        currentlySpawning = SpawnButton(firstLevel);
        timer = timeBetweenSpawns;
    }

    GameObject SpawnButton(GameObject toSpawn){
        if (toSpawn == null) return null;
        count++;
        spawnedButton = Instantiate(buttonPrefab,Vector3.zero,Quaternion.identity,transform);
        SCR_ColorSystem colorLister = toSpawn.GetComponent<SCR_ColorSystem>();
        TMP_Text textComp = spawnedButton.GetComponentInChildren<TMP_Text>();
        spawnedButton.GetComponent<SCR_LevelSender>().levelToSend = toSpawn;
        textComp.text = count.ToString();
        textComp.color = colorLister.highlight;
        spawnedButton.GetComponent<Image>().color = colorLister.background;
        SCR_LevelGenerator genScript = toSpawn.GetComponent<SCR_LevelGenerator>();
        spawnedButton.GetComponent<SCR_WorldPositioner>().positionOverride = attractor;
        if(genScript.levelName == pendingMedalName){
            pendingMedal.ChangeParentButton(spawnedButton);
            pendingMedal.transform.SetParent(transform.parent);
            pendingMedal = null;
            pendingMedalName = "";
            Destroy(textComp.gameObject);
        }else{
            if(PlayerPrefs.GetInt(genScript.levelName) == 1){
                Instantiate(medal , spawnedButton.transform.position , Quaternion.identity, transform.parent).GetComponent<SCR_Medallion>().ChangeParentButton(spawnedButton);
                Destroy(textComp.gameObject);
            }
        }
        if (PlayerPrefs.GetInt(genScript.levelName + "passed", int.MaxValue) == int.MaxValue)
            return null;
        else
            return genScript.nextLevel;
    }

    public void Update(){
        if (spawnedButton != null){
            Instantiate(puff , spawnedButton.transform.position , Quaternion.identity, transform.parent);
            spawnedButton = null;
        }
        if (spawning == false) return;
        timer -= Time.deltaTime;
        if (timer <= 0){
            timer += timeBetweenSpawns;
            currentlySpawning = SpawnButton(currentlySpawning);
            if (currentlySpawning == null) {
                spawning = false;
            }
        }
    }

    [ContextMenu("DestroyButtons")]
    public void KillButtons(){
        spawning = false;
        currentlySpawning = null;
        for(int i = 0; i < transform.childCount ;  i++){
            Transform toDestroy = transform.GetChild(i);
            Instantiate(puff , toDestroy.position , Quaternion.identity, transform.parent);
            Destroy(toDestroy.gameObject);
        }
    }
}
