using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_LevelSender : MonoBehaviour{
    public GameObject levelToSend;
    public Image imgComp;
    public void SendLevel(){
        SCR_LevelSpawner.instance.levelToSpawn = levelToSend;
        GameObject instantiated = Instantiate(gameObject,transform.position,Quaternion.identity,transform.parent.parent);
        instantiated.GetComponent<SCR_WorldPositioner>().enabled = true;
        SCR_LevelSpawner.instance.buttonToKill = instantiated;
        SCR_LevelSpawner.instance.bgColor = imgComp.color;
        SCR_LevelSpawner.instance.sequence.Play();
    }
}
