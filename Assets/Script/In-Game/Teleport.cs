using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLevel;
    public int NextLevel;
    public bool SceneSwitch;

    //if it is banner, then send the notification to save the game.
    public bool IsItBanner;
    public string SceneName;
    UserData save =null;

    //save notify.
    public delegate void SaveAction();
    public static event SaveAction saveGame;

    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
    }

    /**
    void OnEnable(){
        //Control.Tele +=Save;
    }

    void OnDisable(){
        //Control.Tele -=Save;
    }**/

    void OnTriggerStay2D(Collider2D other){
        if(Input.GetKeyDown(KeyCode.E)){
            //save the game.
            if(IsItBanner){
                if(saveGame !=null){
                    saveGame();
                }
            }

            //switch the scene.
            if(SceneSwitch ==false){
                Transform teleportDestination =GameObject.Find(NextLevel.ToString()).transform;
                Player.transform.position =teleportDestination.position;
                //when player using a teleport, save the game.
                if(!save.level.Contains(CurrentLevel) && !save.level.Contains(NextLevel)){
                    save.currentLevel =NextLevel;
                    save.level.Add(CurrentLevel);
                    save.level.Add(NextLevel);
                }
            }else{

            }
        }
    }
}
