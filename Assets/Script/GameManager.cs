using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //This is use to store the player's current info and player control.
    public static GameManager Instance;

    private void Awake(){
        Instance =this;
        DontDestroyOnLoad(this.gameObject);
    }

    //It is a transport only.
    public UserData currentSaving;

    //Player Control
    public GameObject Player =null;
    //each scene, the player will always have character controller component.
    public Rigidbody2D PlayerRB =null;

    //check whether the player reaches the ground.
    public Transform groundCheck;

    //check whether the thing stepped in is ground by checking layer.
    public LayerMask groundLayer;

    private float horizontal;
    private float speed =8f;
    private float jumpingPower =16f;
    private bool isFacingRight =true;
    void Update(){
        //enter the game.
        if(currentSaving.evaluable ==1){
            Player =GameObject.Find("Player");

            //RigidBody needs to wait until Player be found, otherwise it will give an error
            //(it seems it can find the Rigidbody finally, but to avoid error shows, this if sentence is still essential)
            if(Player !=null){
                //finds the CharacterController in each scene.
                if(PlayerRB ==null){
                    PlayerRB =Player.GetComponent<Rigidbody2D>();
                    groundCheck =GameObject.Find("GroundCheck").transform;
                }else{
                    horizontal =Input.GetAxisRaw("Horizontal");

                    if(Input.GetKey("space") && IsGrounded()){
                        PlayerRB.velocity =new UnityEngine.Vector2(PlayerRB.velocity.x,jumpingPower);
                    }

                    if(Input.GetKeyUp("space") && PlayerRB.velocity.y >0f){
                        PlayerRB.velocity =new UnityEngine.Vector2(PlayerRB.velocity.x,PlayerRB.velocity.y*0.5f);
                    }
                    Flip();
                }
            
            }
        }
    }

    private void FixedUpdate(){
        if(PlayerRB !=null){
            PlayerRB.velocity =new UnityEngine.Vector2(horizontal *speed, PlayerRB.velocity.y);
        }
    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip(){
        if(isFacingRight && horizontal <0f || !isFacingRight && horizontal >0f){
            isFacingRight =!isFacingRight;
            UnityEngine.Vector3 localScale =Player.transform.localScale;
            localScale.x *= -1f;
            Player.transform.localScale =localScale;
        }
    }
}
