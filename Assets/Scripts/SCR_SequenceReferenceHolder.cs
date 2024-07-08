using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SCR_SequenceReferenceHolder : MonoBehaviour{
    public static SCR_SequenceReferenceHolder instance;
    private void Awake(){
        if (SCR_SequenceReferenceHolder.instance != null) Destroy(SCR_SequenceReferenceHolder.instance);
        instance = this;
    }
    public PlayableDirector nextLevelSequence;

    public void CheckRecord(){
        Debug.Log(SCR_RecordTracker.instance.SetRecord(SCR_ClickCounter.instance.count));
    }

    public void TriggerLoad(){
        SCR_ClickCounter.instance.ResetCount();
        SCR_LevelGenerator.instance.LoadLevel();
    }
}
