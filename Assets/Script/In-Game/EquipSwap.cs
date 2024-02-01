using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EquipSwap : MonoBehaviour
{
    public UserData save=null;
    public GameObject Player;

    public List<GameObject> Armor_Sprite;

    public GameObject weapon;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
    }

    // Update is called once per frame
    void Update()
    {
        ArmorSwap();
        WeaponSwap();
        ShieldSwap();
    }

    void ArmorSwap(){
        if(save !=null){
            int Armor_id =save.armor_id;
            //holy crap, a coding hell.
            if(Armor_id ==2){
                foreach (GameObject child in Armor_Sprite){
                    string partName =child.name;
                    child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName,"GladFlax");
                }
            }
            if(Armor_id ==5){
                foreach (GameObject child in Armor_Sprite){
                    string partName =child.name;
                    child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName,"Relos_Hoplite");
                }
            }
        }
    }

    void WeaponSwap(){
        if(save !=null){
            int Weapon_id =save.weapon_id;
            if(Weapon_id ==1){
                weapon.GetComponent<SpriteResolver>().SetCategoryAndLabel(weapon.name,"GladSword");
            }
        }
    }

    void ShieldSwap(){
        if(save !=null){
            int Shield_id =save.shield_id;
            if(Shield_id ==3){
                shield.GetComponent<SpriteResolver>().SetCategoryAndLabel(shield.name,"GladShield");
            }
        }
    }
}
