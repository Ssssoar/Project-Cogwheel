using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Language : MonoBehaviour{
    public static SCR_Language instance;
    private void Awake(){
        if (SCR_Language.instance != null) Destroy(SCR_Language.instance.gameObject);
        instance = this;
    }

    public Lang GetLang(){
        return (Lang)PlayerPrefs.GetInt("lang" , (int)Lang.español);
    }
    
    public void ToggleLang(){
        Lang current = GetLang();
        switch (current){
            case(Lang.español):
                PlayerPrefs.SetInt("lang" , (int)Lang.english);
            break;
            case(Lang.english):
                PlayerPrefs.SetInt("lang" , (int)Lang.español);
            break;
        }
    }
}
