using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointTrack : MonoBehaviour
{
    UserData save;
    public GameObject ActionPoint;

    void Start(){
        save=GameManager.Instance.currentSaving;
        for(int i=0;i<save.action_point;i++){
            Instantiate(ActionPoint,this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
