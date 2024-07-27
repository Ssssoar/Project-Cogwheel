using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SCR_MedalCounter : MonoBehaviour{
    public int maxBadges;
    public TMP_Text textComp;
    public Button button;
    int currentBadges;

    public void ResetCount(){
        currentBadges = 0;
        UpdateText();
        button.interactable = false;
    }

    public void Count(){
        currentBadges++;
        UpdateText();
        if (currentBadges >= maxBadges){
            button.interactable = true;
        }
    }

    public void UpdateText(){
        textComp.text = currentBadges + "/" + maxBadges;
    }
}
