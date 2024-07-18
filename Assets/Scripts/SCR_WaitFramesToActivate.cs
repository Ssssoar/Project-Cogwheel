using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_WaitFramesToActivate : MonoBehaviour{
    public int framesToWait;
    public Image[] imgComps;
    public GameObject[] objs;
    
    void Update(){
        framesToWait--;
        if (framesToWait <= 0){
            foreach (Image imgComp in imgComps){
                imgComp.enabled = true;
            }
            foreach(GameObject obj in objs){
                obj.SetActive(true);
            }
            Destroy(this);
        }
    }
}
