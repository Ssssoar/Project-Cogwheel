using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SCR_OpenURL : MonoBehaviour{
    public void Open(){
        Application.OpenURL("https://ssoar.itch.io/");
    }
}
