using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLevel;
    public int NextLevel;
    public bool SceneSwitch;
    public string SceneName;
    UserData save =null;
    // Start is called before the first frame update
    void Start()
    {
        if(save ==null){
            save =GameManager.Instance.currentSaving;

        }else{

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other){
        if(Input.GetKeyDown(KeyCode.E)){
            if(SceneSwitch ==false){
                Transform teleportDestination =GameObject.Find(NextLevel.ToString()).transform;
                Player.transform.position =teleportDestination.position;
                //when player using a teleport, save the game.
                if(!save.level.Contains(CurrentLevel) && !save.level.Contains(NextLevel)){
                    save.currentLevel =NextLevel;
                    save.level.Add(CurrentLevel);
                    save.level.Add(NextLevel);
                    //save the game, means that tempsave's item will save into actual backapack now.
                    foreach(int item in GameManager.Instance.TempBackpack){
                        save.backpack.Add(item);
                    }
                    //clean the temp backpack.
                    GameManager.Instance.TempBackpack.Clear();
                }
            }else{

            }
        }
    }
}
