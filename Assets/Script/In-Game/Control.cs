using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Control : MonoBehaviour
{
    //Player Control
    public GameObject Player =null;
    public Rigidbody2D PlayerRB =null;
    //aim function
    public GameObject mainCam_Object;
    //public Camera mainCam;
    public Vector2 PlayerPos;
    private UnityEngine.Vector3 mousePos;
    private GameObject rotatePoint;
    UserData save;
    //

    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        Player =GameObject.Find("Player");
            if(Player !=null){
                
                if(PlayerRB ==null){
                    //finds all other needed component in here.
                    PlayerRB =Player.GetComponent<Rigidbody2D>();
                    mainCam_Object =GameObject.Find("Main Camera");
                    rotatePoint =GameObject.Find("RotatePoint");

                }else{
                    Aim();
                }
            
            }
    }

    void FixedUpdate(){
        PlayerPos.x =Input.GetAxisRaw("Horizontal");
        //PlayerPos.y = Input.GetAxisRaw("Vertical");
        Move();
    }

    public float moveSpeed;

    void Move(){
        if(PlayerRB !=null){
            moveSpeed =save.moveSpeed;
            PlayerRB.MovePosition(PlayerRB.position + PlayerPos * moveSpeed * Time.fixedDeltaTime);
        }
    }

    //Make camera follow player
    public UnityEngine.Vector3 Cameraoffset;
    public float CameraSpeed;

    public float rotZ;

    private void Aim(){
        //Make camera follow player
        UnityEngine.Vector3 correctPos =Player.transform.position +Cameraoffset;
        mainCam_Object.transform.position =UnityEngine.Vector3.Lerp(mainCam_Object.transform.position, correctPos, CameraSpeed*Time.deltaTime);
        //

        //RotatePoint will rotate since Player object cannot rotate due to the freeze of Z-axis.
        //hence the actual projectile will shot from CrossHair object.
        mousePos =mainCam_Object.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 rotation =mousePos -rotatePoint.transform.position;
        rotZ =Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        rotatePoint.transform.rotation =UnityEngine.Quaternion.Euler(0,0,rotZ);
        Debug.Log(rotZ);
        //
    }
}
