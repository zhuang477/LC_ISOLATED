using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    //this is for temp backpack and backpack items visualization.
    public static Backpack Instance;
    public GameObject backpack_grid;

    public Transform rectParent;

    //tempoary backpack, items will actually store after touch banner.
    public List<int> TempBackpack;

    UserData save =null;

    //fill the backpack grid only, if using for(int i), the grid will loop forever.
    int capacity =0;
    // Start is called before the first frame update
    void Start()
    {
        //subscribe to event.
        Teleport.saveGame +=AddBackpack;
    }

    // Update is called once per frame
    void Update()
    {

        save =GameManager.Instance.currentSaving;

        //backpack init.
        if(save.evaluable ==1){
            float posX =0.7f;
            float posY =-0.5f;
            //omg the time complexity is O(n^2) :(
            for(;capacity<save.backpack.Count;capacity++){
                //backpack grid first, items next.
                if(capacity %5 ==0 && capacity !=0){
                    //create a new row.
                    posY -=1f;
                    posX =0.7f;
                }
                GameObject grid =Instantiate(backpack_grid, rectParent);
                RectTransform rectTransform =grid.GetComponent<RectTransform>();
                rectTransform.anchoredPosition =new Vector2(posX,posY);
                posX +=1.1f;
            }
        }
        //
        
    }

    //send the item in temp backpack to backpack.
    void AddBackpack(){
        foreach(int item in TempBackpack){
            save.backpack.Add(item);
        }
        TempBackpack.Clear();
    }

    //unsubscribe to the event.
    private void OnDisable(){
        Teleport.saveGame -=AddBackpack;
    }
    
}
