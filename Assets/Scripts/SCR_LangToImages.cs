using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LangToImages : MonoBehaviour{
    public SCR_UI_RandomSprite randomerComp;
    public Sprite[] spriteList;
    public Sprite[] ENspriteList;

    bool toUpdate = false;

    void Start(){
        toUpdate = true;
        UpdateList();
    }

    void OnEnable(){
        toUpdate = true;
        UpdateList();
    }

    void Update(){
        if (toUpdate){
            UpdateList();
        }
    }

    public void UpdateList(){
        if (SCR_Language.instance == null) return;
        switch(SCR_Language.instance.GetLang()){
            case(Lang.español):
                randomerComp.img.sprite = spriteList[0];
                randomerComp.spriteList = spriteList;
            break;
            case(Lang.english):
                randomerComp.img.sprite = ENspriteList[0];
                randomerComp.spriteList = ENspriteList;
            break;
        }
        toUpdate = false;
    }
}