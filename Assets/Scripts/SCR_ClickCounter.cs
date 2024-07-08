using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_ClickCounter : MonoBehaviour{
    public static SCR_ClickCounter instance;
    private void Awake(){
        if (SCR_ClickCounter.instance != null) Destroy(SCR_ClickCounter.instance.gameObject);
        instance = this;
    }

    [Header("References")]
    public TMP_Text clickIndicator;

    [Header("Variables")]
    public int count = 0;
    public string text;

    void Start(){
        ResetCount();
    }

    public void Count(){
        count++;
        clickIndicator.text = text + System.Environment.NewLine + count;
    }

    public void ResetCount(){
        count = 0;
        clickIndicator.text = text + System.Environment.NewLine + count;
    }
}
