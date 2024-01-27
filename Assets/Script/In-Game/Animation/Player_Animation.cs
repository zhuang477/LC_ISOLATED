using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public Animator animator;
    public GameObject spineBone;
    ItemDatabase itemDatabase =null;
    UserData save=null;
    Control control =null;
    Quaternion DefaultSpineAngle;
    // Start is called before the first frame update
    void Start()
    {
        DefaultSpineAngle =spineBone.transform.rotation;
        itemDatabase =GameManager.Instance.itemDatabase;
        save =GameManager.Instance.currentSaving;
        control =GameManager.Instance.control;
    }

    // Update is called once per frame
    void Update()
    {
        LeftHand();
        RightHand();
        BodyAndLeg();
    }

    void LeftHand(){
        if(save !=null && itemDatabase !=null){
            if(itemDatabase.getItem(save.weapon_id).stats_stringTostring["Weapon Hold"].Equals("1H")){
                animator.SetBool("1H",true);
                animator.SetBool("2H",false);
            }
            if(itemDatabase.getItem(save.weapon_id).stats_stringTostring["Weapon Hold"].Equals("2H")){
                animator.SetBool("2H",true);
                animator.SetBool("1H",false);
            }
        }
        if(itemDatabase.getItem(save.weapon_id).stats_stringTostring["Weapon Type"].Equals("Sword")){
            float attackspeed =(float)itemDatabase.getItem(save.weapon_id).stats_stringTodouble["Attack_Speed"];
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                animator.SetFloat("AttackSpeed",attackspeed);
                //head
                if((int)GameManager.Instance.combatSystem.attackPos ==0){
                    animator.SetTrigger("Sword_UpAttack");
                }
                //torso
                if((int)GameManager.Instance.combatSystem.attackPos ==1){
                    animator.SetTrigger("Sword_MidAttack");
                }
                //leg
                if((int)GameManager.Instance.combatSystem.attackPos ==2){
                    animator.SetTrigger("Sword_DownAttack");
                }
            }
        }
    }

    void RightHand(){
        if(Input.GetKey(KeyCode.Mouse1)){
            spineBone.transform.rotation =Quaternion.Euler(0,0,GameManager.Instance.control.rotZ+90);
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)){
            spineBone.transform.rotation =DefaultSpineAngle;
        }
    }

    void BodyAndLeg(){
        if(control !=null){
            //moving towards right.
            if(control.PlayerPos.x >0){
                //looking at right.
                if((control.rotZ <90.00 && control.rotZ >0)|| (control.rotZ <0 && control.rotZ >-90.00) || control.rotZ ==0){
                    animator.SetFloat("AnimationSpeed",1f);
                    animator.SetBool("IsWalking",true);
                }
                //looking at left.
                if((control.rotZ >90.00 && control.rotZ <180.00) ||(control.rotZ <-90.00 && control.rotZ >-180.00) || control.rotZ ==-180){
                    animator.SetBool("IsWalking",true);
                }
            }
            //moving towards left.
            if(control.PlayerPos.x <0){
                //looking at right.
                if((control.rotZ <90.00 && control.rotZ >0)|| (control.rotZ <0 && control.rotZ >-90.00) || control.rotZ ==0){
                    animator.SetFloat("AnimationSpeed",-1f);
                    animator.SetBool("IsWalking",true);
                }
                //looking at left.
                if((control.rotZ >90.00 && control.rotZ <180.00) ||(control.rotZ <-90.00 && control.rotZ >-180.00) || control.rotZ ==-180){
                    animator.SetBool("IsWalking",true);
                }
            }
            //standing.
            if(control.PlayerPos.x ==0){
                animator.SetBool("IsWalking",false);
            }
        }
    }
}

/**

//rotate by mouse position.
        //face the right
        if((rotZ <90.00 && rotZ >0)|| (rotZ <0 && rotZ >-90.00) || rotZ ==0){
            Player.GetComponent<SpriteRenderer>().flipX =false;
        }
        //face the left
        if((rotZ >90.00 && rotZ <180.00) ||(rotZ <-90.00 && rotZ >-180.00) || rotZ ==-180){
            Player.GetComponent<SpriteRenderer>().flipX =true;
        }
*/