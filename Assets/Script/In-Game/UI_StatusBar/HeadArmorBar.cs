using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadArmorBar : MonoBehaviour
{
    public Slider healthbar;
    double Max_Armor;
    UserData save =null;
    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        if(save !=null){
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
