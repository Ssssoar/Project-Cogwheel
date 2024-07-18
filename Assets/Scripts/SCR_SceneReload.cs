using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_SceneReload : MonoBehaviour{
    public void Reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
