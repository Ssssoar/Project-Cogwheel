using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WiggleTimer : MonoBehaviour{
    public static SCR_WiggleTimer instance;
    private void Awake(){
        if (SCR_WiggleTimer.instance != null) Destroy(SCR_LevelGenerator.instance.gameObject);
        instance = this;
    }


    public float quickTime;
    public float slowTime;

    float slowTimer;
    float quickTimer;
    List<SCR_RandomSprite> quickWiggle = new List <SCR_RandomSprite>();
    List<SCR_RandomSprite> slowWiggle = new List <SCR_RandomSprite>();

    public void Update(){
        slowTimer -= Time.deltaTime;
        quickTimer -= Time.deltaTime;
        if (slowTimer <= 0f){
            slowTimer += slowTime;
            foreach (SCR_RandomSprite script in slowWiggle) script.Wiggle();
        }
        if (quickTimer <= 0f){
            quickTimer += quickTime;
            foreach (SCR_RandomSprite script in quickWiggle) script.Wiggle();
        }
    }

    public void NewWiggle(WiggleType wiggle,SCR_RandomSprite spriteScript){
        switch(wiggle){
            case(WiggleType.quick):
                quickWiggle.Add(spriteScript);
            break;
            case(WiggleType.slow):
                slowWiggle.Add(spriteScript);
            break;
        }
    }

    public void RemoveWiggle(WiggleType wiggle, SCR_RandomSprite toRemove){
        switch(wiggle){
            case(WiggleType.quick):
                quickWiggle.Remove(toRemove);
            break;
            case(WiggleType.slow):
                slowWiggle.Remove(toRemove);
            break;
        }
    }
}
