using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    UserData save =null;
    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        //init set, since value will not automatically change when maxvalue change.
        if(save !=null){
            healthbar.maxValue =(float)save.hitpoint;
            healthbar.value =(float) save.current_hitpoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
