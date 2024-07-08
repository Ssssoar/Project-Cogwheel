using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MouseInputReceiver : MonoBehaviour{
    public static SCR_MouseInputReceiver instance;
    private void Awake(){
        if (SCR_MouseInputReceiver.instance != null) Destroy(gameObject);
        else instance = this;
    }

    [Header("References")]
    public Animator buttonAnim1;
    public Animator buttonAnim2;
    public GameObject arrowContainer1;
    public GameObject arrowContainer2;
    public SCR_ClickCounter clickCounter;

    [Header("Prefabs")]
    public GameObject upArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject downArrow;

    [Header("Variables")]
    public KeyCode trigger1;
    public KeyCode trigger2;
    public List<Command> commandList1;
    public List<Command> commandList2;
    public bool canMove;

    void Start(){
        UpdateCommands(null,null);
    }

    void Update(){
        if (!canMove) return;
        if(Input.GetKeyDown(trigger1)){
            SendCommands(commandList1,arrowContainer1);
            SendAnimations(buttonAnim1,true);
        }
        if(Input.GetKeyDown(trigger2)){
            SendCommands(commandList2,arrowContainer2);
            SendAnimations(buttonAnim2,true);
        }
        if(Input.GetKeyUp(trigger1)){
            SendAnimations(buttonAnim1,false);
        }
        if(Input.GetKeyUp(trigger2)){
            SendAnimations(buttonAnim2,false);
        }
    }

    void SendCommands(List<Command> commandList, GameObject arrowContainer){
        clickCounter.Count();
        int i = 0;
        foreach (Command comm in commandList){
            GameObject toClone = arrowContainer.transform.GetChild(i).gameObject;
            GameObject clone = Instantiate(toClone,toClone.transform.position,Quaternion.identity,SCR_Scheduler.instance.transform);
            SCR_Scheduler.instance.ReceiveCommand(comm,clone);
            i++;
        }
    }

    void SendAnimations(Animator buttonAnim,bool pressed){
        buttonAnim.SetBool("Pressed",pressed);
    }

    [ContextMenu("UpdateCommands")]
    void DebugUpdate(){
        UpdateCommands(null,null);
    }

    void UpdateCommands(List<Command> list1,List<Command> list2){
        if (list1 == null)
            list1 = commandList1;
        if (list2 == null)
            list2 = commandList2;
        UpdateContainer(arrowContainer1, list1);
        UpdateContainer(arrowContainer2, list2);

    }

    void UpdateContainer(GameObject container, List<Command> list){
        //destroy all the children
        List<Transform> childrenList = new List<Transform>();
        container.GetComponentsInChildren<Transform>(childrenList);
        childrenList.Remove(container.transform);
        foreach(Transform child in childrenList){
            Destroy(child.gameObject);
        }
        //create new children
        foreach(Command comm in list){
            GameObject toInstantiate = upArrow;
            switch(comm){
                case(Command.up):
                    toInstantiate = upArrow;
                break;
                case(Command.left):
                    toInstantiate = leftArrow;
                break;
                case(Command.right):
                    toInstantiate = rightArrow;
                break;
                case(Command.down):
                    toInstantiate = downArrow;
                break;
            }
            Instantiate(toInstantiate,container.transform);
        }
    }

    public void InputState(bool state){
        canMove = state;
    }
}
