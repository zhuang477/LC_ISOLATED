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

    //Key management.
        public delegate void Action();

        //Attack.
        public static event Action Attack;

        //Spine Rotate
        public static event Action SpineRotating;
        public static event Action StopSpineRotating;

        //Squat
        public static event Action Squatting;
        public static event Action StopSquatting;

        //Teleport(not in animation, is in teleport) <- not implement yet.
        //public static event Action Tele;
        public static event Action IsPicked;

        public static event Action Phan_Attack;
        public static event Action Stop_Phan_Attack;

        public delegate void DialogueRelate();
        public static event DialogueRelate EnableDialogueEvent;

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

                //when rigidbody is assigned.
                }else{
                    Aim();
                    AttackOnClicked();
                    SpineRotate();
                    Squat();
                    //Teleport();
                    PickUpItem();
                    StartDialogue();
                }
            
            }
    }


    void OnEnable(){
        //see Pickable.cs, it identifies items that can be picked.
        Pickable.IsPickable +=PickUpItem;
    }

    void OnDisable(){
        //see Pickable.cs, it identifies items that can be picked.
        Pickable.IsPickable -=PickUpItem;
    }

    //Attack
    public void AttackOnClicked(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Attack !=null){
                Attack();
            }
        }
    }

    //Spine Rotate
    public void SpineRotate(){
        if(Input.GetKey(KeyCode.Mouse1)){
            if(SpineRotating !=null){
                SpineRotating();
            }
            if(Phan_Attack !=null){
                Phan_Attack();
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)){
            if(StopSpineRotating !=null){
                StopSpineRotating();
            }
            if(Stop_Phan_Attack !=null){
                Stop_Phan_Attack();
            }
        }
    }

    //Squat
    public void Squat(){
        if(Input.GetKey(KeyCode.S)){
            if(Squatting !=null){
                Squatting();
            }
        }
        if(Input.GetKeyUp(KeyCode.S)){
            if(StopSquatting !=null){
                StopSquatting();
            }
        }
    }

    //Teleport
    /**
    public void Teleport(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(Tele !=null){
                Tele();
            }
        }
    }**/

    //this is a DOUBLE-EVENT! When an item is pickable, Pickable.cs will send event to Control.cs
    //And when Control.cs press E, Pickable.cs will receive the input.
    public void PickUpItem(){
        if(Input.GetKeyDown(KeyCode.E)){
            save.itemInBackpack ++;
            IsPicked();
        }
    }

    void FixedUpdate(){
        PlayerPos.x =Input.GetAxisRaw("Horizontal");
        Move();
    }

    public float moveSpeed;

    void Move(){
        if(PlayerRB !=null){
            moveSpeed =save.moveSpeed;
            PlayerRB.MovePosition(PlayerRB.position + PlayerPos * moveSpeed * Time.fixedDeltaTime);
        }
    }

    //use playerRB to detect conversation collider.
    void StartDialogue(){
        if(save.current_Dialogue !=null){
            if(Input.GetKeyDown(KeyCode.G)){
                if(EnableDialogueEvent !=null){
                    EnableDialogueEvent();
                }
            }
        }
        //disable dialogue will be done by continue button in dialogue box.
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
        //
    }
}
