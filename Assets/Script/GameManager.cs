using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //This is use to store the player's current info and player control.
    public static GameManager Instance;

    //each scene, the player will always have character controller component.
    public CharacterController characterController =null;

    private void Awake(){
        Instance =this;
        DontDestroyOnLoad(this.gameObject);
    }

    //It is a transport only.
    public UserData currentSaving;

    //Player Control
    void Update(){
        //enter the game.
        if(currentSaving.evaluable ==1){
            //finds the CharacterController in each scene.
            if(characterController ==null){
                characterController =GameObject.Find("Player").GetComponent<CharacterController>();
            }
            
        }
    }
}
