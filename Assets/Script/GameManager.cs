using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    public Rigidbody2D PlayerRB =null;

    //check whether the player reaches the ground.
    public Transform groundCheck;

    //check whether the thing stepped in is ground by checking layer.
    public LayerMask groundLayer;

    private float horizontal;
    private float speed =8f;
    private float jumpingPower =16f;
    //private bool isFacingRight =true;

    //aim function
    public GameObject mainCam_Object;
    //public Camera mainCam;
    private UnityEngine.Vector3 mousePos;
    private GameObject rotatePoint;
    //

    void Update(){
        //enter the game.
        if(currentSaving.evaluable ==1){
            Player =GameObject.Find("Player");

            //RigidBody needs to wait until Player be found, otherwise it will give an error
            //(it seems it can find the Rigidbody finally, but to avoid error shows, this if sentence is still essential)
            if(Player !=null){
                if(PlayerRB ==null){
                    //finds all other needed component in here.
                    PlayerRB =Player.GetComponent<Rigidbody2D>();
                    groundCheck =GameObject.Find("GroundCheck").transform;
                    mainCam_Object =GameObject.Find("Main Camera");
                    rotatePoint =GameObject.Find("RotatePoint");
                }else{
                    //This part is now implement all inputs.
                    horizontal =Input.GetAxisRaw("Horizontal");

                    if(Input.GetKey("space") && IsGrounded()){
                        PlayerRB.velocity =new UnityEngine.Vector2(PlayerRB.velocity.x,jumpingPower);
                    }

                    if(Input.GetKeyUp("space") && PlayerRB.velocity.y >0f){
                        PlayerRB.velocity =new UnityEngine.Vector2(PlayerRB.velocity.x,PlayerRB.velocity.y*0.5f);
                    }
                    //Flip();
                    Aim();
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

    //the facing direction will based on the mouse position, hence 
    //this function is disabled.
    /**
    private void Flip(){
        if(isFacingRight && horizontal <0f || !isFacingRight && horizontal >0f){
            isFacingRight =!isFacingRight;
            UnityEngine.Vector3 localScale =Player.transform.localScale;
            localScale.x *= -1f;
            Player.transform.localScale =localScale;
        }
    }**/

    //Make camera follow player
    public UnityEngine.Vector3 Cameraoffset;
    public float CameraSpeed;

    private void Aim(){
        //Make camera follow player
        UnityEngine.Vector3 correctPos =Player.transform.position +Cameraoffset;
        mainCam_Object.transform.position =UnityEngine.Vector3.Lerp(mainCam_Object.transform.position, correctPos, CameraSpeed*Time.deltaTime);
        //

        //RotatePoint will rotate since Player object cannot rotate due to the freeze of Z-axis.
        //hence the actual projectile will shot from CrossHair object.
        mousePos =mainCam_Object.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 rotation =mousePos -rotatePoint.transform.position;
        float rotZ =Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        rotatePoint.transform.rotation =UnityEngine.Quaternion.Euler(0,0,rotZ);
        //

        //rotate by mouse position.
        //face the right
        if((rotZ <90.00 && rotZ >0)|| (rotZ <0 && rotZ >-90.00) || rotZ ==0){
            Player.GetComponent<SpriteRenderer>().flipX =false;
        }
        //face the left
        if((rotZ >90.00 && rotZ <180.00) ||(rotZ <-90.00 && rotZ >-180.00) || rotZ ==-180){
            Player.GetComponent<SpriteRenderer>().flipX =true;
        }
    }
}
