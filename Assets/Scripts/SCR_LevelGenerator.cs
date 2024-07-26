using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SCR_LevelGenerator : MonoBehaviour{
    public static SCR_LevelGenerator instance;
    private void Awake(){
        if (SCR_LevelGenerator.instance != null) Destroy(SCR_LevelGenerator.instance.gameObject);
        instance = this;
    }

    [Header("References")]
    public SCR_RowIndex[] rows;
    public SCR_ColorSystem colorScript;

    [Header("Prefabs")]
    public GameObject emptyPrefab;
    public GameObject puffPrefab;
    public GameObject silentPrefab;
    public GameObject nextLevel;
    public GameObject rowPrefab;

    [Header("Variables")]
    public string levelName;
    public string enLevelName;
    public List<Command> leftCommands;
    public List<Command> rightCommands;
    public bool clockWiseTurn;
    public int starScore;

    [HideInInspector]
    public Vector2Int maxSize;
    public List<SCR_WorldPositioner> buttonList;
    public List<Vector2Int> buttonPos;
    bool nameUpdated = false;
    bool recordFetched = false;

    void Update(){
        if (!nameUpdated){
            if (SCR_Scheduler.instance != null) { 
                SCR_Scheduler.instance.UpdateLevelName(
                    ((Lang)PlayerPrefs.GetInt("lang" , (int)Lang.español) == Lang.español)? levelName : enLevelName
                );
                nameUpdated = true;
            }
        }
        if(!recordFetched){
            if (SCR_RecordTracker.instance != null){
                SCR_RecordTracker.instance.NewLevel(levelName, starScore);
                recordFetched = true;
            }
        }
    }

    void Start(){
        maxSize = CalculateSize();
        GeneratePuffs();
        PositionRows();
        colorScript.SwitchAllColors(rows);
        ScanButtons();
        Vector2 middlePos = (Vector2)maxSize;
        middlePos.x -= 1f;
        middlePos.y -= 1f;
        middlePos /= 2;
        SCR_CameraFocuser.instance.SetFocus(middlePos);
        SCR_CameraFocuser.instance.SetSize(maxSize);
        SCR_CameraFocuser.instance.zRot = 0f;
        SCR_CameraFocuser.instance.transform.rotation = Quaternion.identity;
        SCR_Scheduler.instance.rotations = 0;
        SCR_MouseInputReceiver.instance.UpdateCommands(leftCommands,rightCommands);
        SCR_Scheduler.instance.blocked = false;
    }

    void ScanButtons(){
        for(int i = 0; i < rows.Length; i++){
            for(int j = 0; j < rows[i].slots.Length; j++){
                if(rows[i].slots[j].tag == "Button"){
                    buttonList.Add(rows[i].slots[j]);
                    Vector2Int pos = new Vector2Int(j,i);
                    buttonPos.Add(pos);
                    AddObject(pos, null);
                }
            }
        }
    }

    public bool SeekButton(Vector2Int toCheck){
        for(int i = 0; i < buttonPos.Count ; i++){
            Vector2Int coord = buttonPos[i];
            if (toCheck == coord){
                if(buttonList[i].transform.localScale.x < 0f)
                    clockWiseTurn = true;
                else
                    clockWiseTurn = false;
                return true;
            }
        }
        return false;
    }

    public void Rotate(){
        float rotNum = (clockWiseTurn)? 90f : -90f;
        SCR_CameraFocuser.instance.zRot += rotNum;
        SCR_Scheduler.instance.AddRotations((int)(-rotNum/90f));
        for(int i = 0; i < rows.Length; i++){
            for(int j = 0; j < rows[i].slots.Length; j++){
                rows[i].slots[j].SetZRot(rotNum);
            }
        }
    }

    void PositionRows(){
        int yPos = 0;
        foreach(SCR_RowIndex row in rows){
            row.transform.position = new Vector3(0f,(float)yPos,0f);
            row.PositionObjects();
            yPos++;
        }
    }

    void GeneratePuffs(){
        int soundLeft = 6;
        for (int i = 0; i < maxSize.x; i++){
            for (int j = 0; j < maxSize.y; j++){
                Vector3 pos = new Vector3( (float)i , (float)j , 0f);
                if (soundLeft > 0){
                    soundLeft--;
                    Instantiate(puffPrefab,pos,Quaternion.identity);
                }else{
                    Instantiate(silentPrefab,pos,Quaternion.identity);
                }
            }
        }
    }

    Vector2Int CalculateSize(){
        Vector2Int size = new Vector2Int(0,0);
        size.y = rows.Length;
        int maxX = 0;
        foreach (SCR_RowIndex row in rows){
            if (row.objects.Length > maxX)
                maxX = row.objects.Length;
        }
        size.x = maxX;
        return size;
    }

    public SCR_WorldPositioner GetElementAtCoord(Vector2Int coord){
        if (coord.x < 0) coord.x = 0;
        if (coord.y < 0) coord.y = 0;
        if (coord.x > maxSize.x-1) coord.x = maxSize.x-1;
        if (coord.y > maxSize.y-1) coord.y = maxSize.y-1;
        return rows[coord.y].slots[coord.x];
    }

    public Vector2Int DirectionToVect(Command direction){
        switch(direction){
            case(Command.up):    return new Vector2Int( 0, 1);
            case(Command.down):  return new Vector2Int( 0,-1);
            case(Command.left):  return new Vector2Int(-1, 0);
            case(Command.right): return new Vector2Int( 1, 0);
        }
        return new Vector2Int(0,0);
    }

    public Vector2 DirectionToFloatVect(Command direction){
        switch(direction){
            case(Command.up):    return new Vector2( 0f, 1f);
            case(Command.down):  return new Vector2( 0f,-1f);
            case(Command.left):  return new Vector2(-1f, 0f);
            case(Command.right): return new Vector2( 1f, 0f);
        }
        return new Vector2(0f,0f);
    }

    public void SwitchObjects(Vector2Int pos1, Vector2Int pos2){ //switch the position of two objects
        SCR_WorldPositioner obj1 = GetElementAtCoord(pos1);
        SCR_WorldPositioner obj2 = GetElementAtCoord(pos2);
        if (obj1 == obj2){
            obj1.SetDesiredWorldPosFromInt(pos1);
        }else{
            rows[pos1.y].slots[pos1.x] = obj2;
            rows[pos2.y].slots[pos2.x] = obj1;
            obj1.desiredWorldPos = pos2;
            obj2.desiredWorldPos = pos1;
            colorScript.SwitchColorSingle(pos1);
            colorScript.SwitchColorSingle(pos2);
        }
    }

    public void SwitchObjects(Vector2 pos1, Vector2 pos2){
        SwitchObjects(FloatVectToInt(pos1) , FloatVectToInt(pos2));
    }

    Vector2Int FloatVectToInt(Vector2 vect){
        return new Vector2Int((int)vect.x , (int)vect.y);
    }

    public void ReplaceObject(Vector2Int pos, GameObject prefab, Color color){ //unload the object in a position and load a different one
        if (prefab == null) prefab = emptyPrefab;
        Destroy(rows[pos.y].slots[pos.x].gameObject);
        rows[pos.y].RuntimeCreate(pos, prefab);
        colorScript.SwitchColorSingle(pos);
        Vector2 worldPos = new Vector2((float)pos.x , (float)pos.y);
        GameObject puff = Instantiate(puffPrefab,worldPos,Quaternion.identity,transform);
        puff.GetComponent<SCR_SendFourCorners>().colorSetter.color = (color != null)? color : Color.white;
    }

    public void AddObject(Vector2Int pos, GameObject prefab){ //adds an object to a position, not unloading the one previously there, but taking it's reference off of the grid.
        if (prefab == null) prefab = emptyPrefab;
        rows[pos.y].RuntimeCreate(pos, prefab);
        colorScript.SwitchColorSingle(pos);
    }

    [ContextMenu("TriggerNextLevel")]
    public void LoadLevel(){
        if (nextLevel != null){/*
            SCR_LevelReloader.instance.levelToReload = nextLevel;
            Instantiate(nextLevel,Vector3.zero,Quaternion.identity,transform.parent);*/
            GeneratePuffs();
            Destroy(gameObject);
        }else{
            DisappearAllButPlayer();
            SCR_SequenceReferenceHolder.instance.gameEndSequence.Play();
        }
    }

    void DisappearAllButPlayer(){
        for(int i = 0; i < rows.Length; i++){
            for(int j = 0; j < rows[i].slots.Length; j++){
                if (rows[i].slots[j].tag != "Player"){
                    ReplaceObject(new Vector2Int(j,i), null, Color.black);
                }
            }
        }
    }
}
