using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MouseAnims : MonoBehaviour{
    [Header("References")]
    public Animator buttonAnim1;
    public Animator buttonAnim2;

    [Header("Variables")]
    public KeyCode trigger1;
    public KeyCode trigger2;

    void Update(){
        if(Input.GetKeyDown(trigger1)){
            SendAnimations(buttonAnim1,true);
        }
        if(Input.GetKeyDown(trigger2)){
            SendAnimations(buttonAnim2,true);
        }
        if(Input.GetKeyUp(trigger1)){
            SendAnimations(buttonAnim1,false);
        }
        if(Input.GetKeyUp(trigger2)){
            SendAnimations(buttonAnim2,false);
        }
    }

    void SendAnimations(Animator buttonAnim,bool pressed){
        buttonAnim.SetBool("Pressed",pressed);
    }
}
