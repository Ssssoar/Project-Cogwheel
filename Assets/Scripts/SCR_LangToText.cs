using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_LangToText : MonoBehaviour{
    public TMP_Text textComp;
    public string text;
    public string enText;

    bool toUpdate = false;

    void Start(){
        toUpdate = true;
        UpdateText();
    }

    void OnEnable(){
        toUpdate = true;
        UpdateText();
    }

    void Update(){
        if (toUpdate){
            UpdateText();
        }
    }

    public void UpdateText(){
        if (SCR_Language.instance == null) return;
        textComp.text = (SCR_Language.instance.GetLang() == Lang.español)? text : enText;
        toUpdate = false;
    }
}
