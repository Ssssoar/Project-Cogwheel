using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SequenceNotePlayer : MonoBehaviour{
    public SCR_BeatKeeper beatScript;
    public SCR_NotePicker noteScript;
    public bool activate;
    public float timer;
    float maxTime;
    int nums = 10;
    // Start is called before the first frame update
    void Start(){
        if(activate){
            beatScript.bassBeatInterval = nums;
            maxTime = timer;
            timer = maxTime / 10;
            noteScript.overrideChord = true;
        }
    }

    // Update is called once per frame
    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0f){
            nums--;
            beatScript.bassBeatInterval = nums;
            timer += maxTime / 10;
            if (nums == 5)
                beatScript.chordChance = true;
            if (nums%2 == 1)
                noteScript.chordOverride = Chord.tonic;
            else
                noteScript.chordOverride = Chord.dominant;

        }
    }
}
