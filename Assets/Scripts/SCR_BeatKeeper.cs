using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BeatKeeper : MonoBehaviour{
    public LibreriaDeSonidos drums;
    public AudioClip bassBeat;
    public int chanceToDrop;
    public float beatTime;
    public int bassBeatInterval;
    public int highlightOffset;

    float timeToNextBeat;
    int beatsToNextBass;
    int beatsToNextHighlight;
    public bool chordChance = false;

    void Start(){
        timeToNextBeat = beatTime;
        beatsToNextBass = bassBeatInterval;
        beatsToNextHighlight = bassBeatInterval + highlightOffset;
    }

    void Update(){
        timeToNextBeat -= Time.deltaTime;
        if (timeToNextBeat <= 0){
            timeToNextBeat = beatTime;
            PlayDrumBeat();
            if(SCR_Scheduler.instance != null)
                SCR_Scheduler.instance.Beat();
        }
    }

    void PlayDrumBeat(){
        if(Random.Range(1 , chanceToDrop + 1) != chanceToDrop){
            SoundFXManager.instance.ReproducirSFX(drums);
        }
        beatsToNextBass--;
        if (beatsToNextBass == 0){
            SoundFXManager.instance.ReproducirSFX(bassBeat);
            beatsToNextBass = bassBeatInterval;
        }
        beatsToNextHighlight--;
        if(beatsToNextHighlight == 0){
            
            if((chordChance) && (Random.Range(0,6) == 0))
                SCR_NotePicker.instance.PlayChord();
            else{
                if(Random.Range(0,2) == 0)
                    SCR_NotePicker.instance.PlayNote(Pitch.high);
                else
                    SCR_NotePicker.instance.PlayNote(Pitch.low);
            }
            beatsToNextHighlight = bassBeatInterval;
        }
    }
}
