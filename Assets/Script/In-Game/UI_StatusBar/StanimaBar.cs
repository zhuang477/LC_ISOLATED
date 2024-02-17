using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanimaBar : MonoBehaviour
{
    UserData save =null;
    public Slider stanimabar;
    public Slider recoverybar;


    //

    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        if(save !=null){
            stanimabar.maxValue =(float)save.actual_stanima;
            stanimabar.value =(float)save.current_stanima;

            //maybe this recovery speed can be affect by armor's weight?
            //may implement in the future.
            recoverybar.maxValue =(float)save.recovery;
            recoverybar.value =(float)save.current_recovery;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set the bar's value
        stanimabar.value =(float)save.current_stanima;
        recoverybar.value =(float)save.current_recovery;


        //set the recovery
        if(save.current_recovery <save.recovery){
            save.current_recovery+= Time.deltaTime;
        }
        //when recovery is full, stanima recover.
        if(save.current_recovery >=save.recovery){
            save.current_recovery =2;
            if(save.current_stanima <save.actual_stanima){
                save.current_stanima +=5*Time.deltaTime;
            }
            if(save.current_stanima >=save.actual_stanima){
                save.current_stanima =save.actual_stanima;
                //Set ActionPoint.
            }
        }

    }

    void OnEnable(){
        AttackEvent.Attack +=ResetRecovery;
    }

    void OnDisable(){
        AttackEvent.Attack -=ResetRecovery;
    }

    void ResetRecovery(){
        save.current_recovery =0;
    }
}
