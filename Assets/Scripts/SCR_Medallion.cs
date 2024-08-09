using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SCR_Medallion : MonoBehaviour{
    public GameObject parentButton;
    public Image ribbon;
    public Vector3 targetScale;
    Color ribbonColor;
    public SCR_WorldPositioner posScript;
    bool hadButton = false;
    // Start is called before the first frame update
    void Start(){
        if (!hadButton)
            targetScale = transform.localScale;
        posScript.desiredWorldPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        if ((parentButton != null)&&(posScript.positionOverride != parentButton.transform))
            posScript.positionOverride = parentButton.transform;
        ribbon.color = Color.Lerp(ribbon.color, ribbonColor, 10f * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale,targetScale, 10f * Time.deltaTime);
        if((hadButton == true)&&(parentButton == null))
            Destroy(gameObject);
    }

    public void ChangeParentButton(GameObject button){
        hadButton = true;
        parentButton = button;
        ribbonColor = button.GetComponentInChildren<TMP_Text>().color;
        targetScale = transform.localScale/3f;
    }
}
