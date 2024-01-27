using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public UserData save;
    public Transform SpawnPoint0;
    public Transform SpawnPoint1;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        //When start a new campaign.
        if(save.level.Count ==0){
            save.level.Add(1);
            save.currentLevel =1;
            Player.transform.position =SpawnPoint0.position;
            save.location =Player.transform.position;
        }
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
