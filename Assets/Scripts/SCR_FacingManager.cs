using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FacingManager : MonoBehaviour{
    public GameObject facingUp;
    public GameObject facingLeft;
    public GameObject facingRight;
    public GameObject facingDown;
    public bool flip;

    public void ChangeFacing(Command direction){
        facingUp.SetActive(false);
        facingLeft.SetActive(false);
        facingRight.SetActive(false);
        facingDown.SetActive(false);
        switch(direction){
            case(Command.up):
                facingUp.SetActive(true);
            break;
            case(Command.left):
                facingLeft.SetActive(true);
                if (flip){
                    transform.localScale = new Vector3 (
                        Mathf.Abs(transform.localScale.x) , 
                        transform.localScale.y ,
                        transform.localScale.z
                    );
                }
            break;
            case(Command.right):
                facingRight.SetActive(true);
                if (flip){
                    transform.localScale = new Vector3 (
                        - Mathf.Abs(transform.localScale.x) , 
                        transform.localScale.y ,
                        transform.localScale.z
                    );
                }
            break;
            case(Command.down):
                facingDown.SetActive(true);
            break;
        }
    }
}
