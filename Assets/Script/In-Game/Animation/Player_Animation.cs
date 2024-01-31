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
        //swords and hammer can attack head and body, axe can only attack head(both 1H and 2H).
        //spears(both 1H and 2H) and polearms can attack all parts.
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
                    animator.SetTrigger("Sword_Up");
                }
                //torso
                if((int)GameManager.Instance.combatSystem.attackPos ==1){
                    animator.SetTrigger("Sword_Mid");
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

            //Squat
            if(Input.GetKey(KeyCode.S)){
                animator.SetBool("IsSquatting",true);
            }
            if(Input.GetKeyUp(KeyCode.S)){
                animator.SetBool("IsSquatting",false);
            }
        }
    }
}