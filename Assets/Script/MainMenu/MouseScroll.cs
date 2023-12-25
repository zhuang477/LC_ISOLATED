using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseScroll : MonoBehaviour
{
    private ScrollRect scrollRect;

    void Start()
    {
        // Get a reference to the ScrollRect component
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        // Allow scrolling only with the mouse scroll wheel
        float scrollWheelDelta = Input.mouseScrollDelta.y;
        if(scrollWheelDelta !=0f){
            float scrollSpeed = 0.1f; // Adjust this value based on your preference

            // Modify the vertical scroll position based on the mouse scroll wheel input
            scrollRect.verticalNormalizedPosition += scrollWheelDelta * scrollSpeed;

            // Clamp the verticalNormalizedPosition between 0 and 1
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }
}
