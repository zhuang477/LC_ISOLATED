using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    public string tagname;
    public delegate void Action();
    public static event Action HitTheTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals(tagname)){
            if(HitTheTarget !=null){
                HitTheTarget();
            }
        }
    }
}
