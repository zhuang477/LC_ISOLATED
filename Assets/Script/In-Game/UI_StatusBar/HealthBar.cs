using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    public TMP_Text healthNumber;
    double Max_Health;
    UserData save =null;
    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        Max_Health =save.hitpoint;
        //init set, since value will not automatically change when maxvalue change.
        if(save !=null){
            //healthbar UI
            healthbar.maxValue =(int)Max_Health;
            healthbar.value =(int)save.hitpoint;
            //healthbar number show
            int Health =(int)save.hitpoint;
            healthNumber.text =Health.ToString()+" | "+Max_Health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
