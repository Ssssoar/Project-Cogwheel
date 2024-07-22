using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayLibSFXOnAwake : MonoBehaviour{
    public LibreriaDeSonidos lib;
    void Start(){
        SoundFXManager.instance.ReproducirSFX(lib);
    }
}
