using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_NotePicker : MonoBehaviour{
    public static SCR_NotePicker instance;
    private void Awake(){
        if (SCR_NotePicker.instance != null) Destroy(gameObject);
        else instance = this;
    }

    public SCR_BeatKeeper beatScript;
    public LibreriaDeSonidos tonicInvHi;
    public LibreriaDeSonidos tonicInvLo;
    public LibreriaDeSonidos tonicHi;
    public LibreriaDeSonidos tonicLo;
    public LibreriaDeSonidos domInvHi;
    public LibreriaDeSonidos domInvLo;
    public LibreriaDeSonidos domHi;
    public LibreriaDeSonidos domLo;
    public AudioClip firstChord;
    public AudioClip otherChord;
    public AudioClip firstDomChord;
    public AudioClip otherDomChord;
    public GameObject victoryObj;
    public float minTime;
    public float maxTime;

    Inversion currentInv = Inversion.first;
    float timer; 

    void Start(){
        timer = Random.Range(minTime , maxTime);
    }

    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0f){
            timer = Random.Range(minTime , maxTime);
            SwitchInversion();
        }
    }

    void SwitchInversion(){
        currentInv = ((currentInv == Inversion.first)? Inversion.other : Inversion.first);
    }

    Chord CheckVictory(){
        if (victoryObj.activeSelf){
            return Chord.tonic;
        }else{
            return Chord.dominant;
        }
    }

    public void PlayRandomNote(){
        if(Random.Range(0,2) == 0)
            SCR_NotePicker.instance.PlayNote(Pitch.high);
        else
            SCR_NotePicker.instance.PlayNote(Pitch.low);
    }

    public void PlayChord(){
        switch(currentInv){
            case(Inversion.first):
                if (CheckVictory() == Chord.dominant)
                    SoundFXManager.instance.ReproducirSFX(firstDomChord);
                else
                    SoundFXManager.instance.ReproducirSFX(firstChord);
            break;
            case(Inversion.other):
                if (CheckVictory() == Chord.dominant)
                    SoundFXManager.instance.ReproducirSFX(otherDomChord);
                else
                    SoundFXManager.instance.ReproducirSFX(otherChord);
            break;
        }
    }

    public void AccelPlay(){
        beatScript.bassBeatInterval = 1;
    }

    public void PlayNote(Pitch pitch){
        LibreriaDeSonidos toPlay = null;
        Chord chord = CheckVictory();
        if(
            (currentInv == Inversion.first)&&
            (chord      == Chord.dominant )&&
            (pitch      == Pitch.high     )
        ){
            toPlay = domHi;
        }else if(
            (currentInv == Inversion.first)&&
            (chord      == Chord.dominant )&&
            (pitch      == Pitch.low      )
        ){
            toPlay = domLo;
        }else if(
            (currentInv == Inversion.first)&&
            (chord      == Chord.tonic    )&&
            (pitch      == Pitch.high     )
        ){
            toPlay = tonicHi;
        }else if(
            (currentInv == Inversion.first)&&
            (chord      == Chord.tonic    )&&
            (pitch      == Pitch.low      )
        ){
            toPlay = tonicLo;
        }else if(
            (currentInv == Inversion.other)&&
            (chord      == Chord.dominant )&&
            (pitch      == Pitch.high     )
        ){
            toPlay = domInvHi;
        }else if(
            (currentInv == Inversion.other)&&
            (chord      == Chord.dominant )&&
            (pitch      == Pitch.low      )
        ){
            toPlay = domInvLo;
        }else if(
            (currentInv == Inversion.other)&&
            (chord      == Chord.tonic    )&&
            (pitch      == Pitch.high     )
        ){
            toPlay = tonicInvHi;
        }else if(
            (currentInv == Inversion.other)&&
            (chord      == Chord.tonic    )&&
            (pitch      == Pitch.low      )
        ){
            toPlay = tonicInvLo;
        }
        SoundFXManager.instance.ReproducirSFX(toPlay);
    }
}
