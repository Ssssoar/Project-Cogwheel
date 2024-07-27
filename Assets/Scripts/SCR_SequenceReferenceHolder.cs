using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SCR_SequenceReferenceHolder : MonoBehaviour{
    public static SCR_SequenceReferenceHolder instance;
    private void Awake(){
        if (SCR_SequenceReferenceHolder.instance != null) Destroy(SCR_SequenceReferenceHolder.instance);
        instance = this;
    }

    public PlayableDirector nextLevelSequence;
    public PlayableDirector gameEndSequence;
    public Image newHiScoreGraphic;

    public void TriggerLoad(){
        Color color = Color.white;
        color.a = 0f;
        newHiScoreGraphic.color = color;
        SCR_ClickCounter.instance.ResetCount();
        SCR_LevelGenerator.instance.LoadLevel();   
    }
}
