using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ScrollDetect : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float scrollWheelDelta = Input.mouseScrollDelta.y;
        if(scrollWheelDelta !=0f){
            Debug.Log(scrollWheelDelta);
        }
    }
}
