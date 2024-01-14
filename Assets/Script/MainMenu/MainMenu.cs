using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    private string saveFolderName ="LCI";
    string[] savingNames;

    public List<GameObject> buttons;
    public SceneLoader sceneLoader;

    //For Load interface.
    public GameObject SaveView;
    public Transform Content;
    //

    void Start(){
        //when the game starts, read the saving documents in advance.
        string GameSavingPath =GetSavingPath();
        LoadFromFile(GameSavingPath);

        //showing Main Buttons Only.
        foreach(GameObject button in buttons){
            string ButtonNames =button.name;
            if(ButtonNames.Equals("MainButton")){
                button.SetActive(true);
            }else{
                button.SetActive(false);
            }
        }
    }

    public void NewCampaign_PlayButton(){
        bool duplicateDetect =false;
        //check whether the input field has any inputs.
        if(inputField.text.Equals("")){
            //If the save name is empty, do something.
        
        //if there exist characters in textfield.
        }else{
            //check whether the file name is duplicated.
            string CurrentSaveName =inputField.text+".json";
            //Debug.Log(CurrentSaveName);
            foreach(string savingName in savingNames){
                //Duplicated.
                string filename =Path.GetFileName(savingName);
                if(CurrentSaveName.Equals(filename)){
                    Debug.Log("Duplicated File Detect");
                    duplicateDetect =true;
                }
            }
        }

        if(!duplicateDetect){
            //create a new save file.
                //1. create a new UserData file.
                UserData newSave = new UserData
                {
                    evaluable =1,
                    saveName = inputField.text,
                    level = new List<int>(),
                    //current level and nextlevel should be assign by individual scene script.
                    player_Avatar =AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Asset/Player/Avatar/Player.png"),
                    XP = 0,
                    hitpoint = 50,
                    stanima = 50,

                    weapon_id = 1,
                    armor_id = 2,

                    perks_unlocked = new List<int>(),
                    backpack = Enumerable.Repeat(0, 40).ToList(),
                    debuff =new List<int>()
                };
                //2. convert the UserData file into JSON.
                string JSONfile =JsonUtility.ToJson(newSave);
                string gameSavingsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", saveFolderName);

                string fileName = inputField.text+".json"; // Specify the desired file name
                string filePath = Path.Combine(gameSavingsFolderPath, fileName);
                File.WriteAllText(filePath, JSONfile);
                    
                //persist the data, keep itemdatabase running, enter the game(change scene)
                GameManager.Instance.currentSaving =newSave;
                sceneLoader.LoadTargetScene();
        }
    }

    public void LoadCampaign(){
        // Path to the "LCI" folder
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "LCI");
        // Get all files in the "LCI" folder
        string[] saveFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (string save in saveFiles){
            GameObject InteractiveButton =Instantiate(SaveView, Content);
            // Get a reference to the TMP component of the button's text
            TextMeshProUGUI buttonText = InteractiveButton.GetComponentInChildren<TextMeshProUGUI>();
            // Set the text of the TMP component to the name of the file (without extension)
            buttonText.text = Path.GetFileNameWithoutExtension(save);
        }
    }

    //Delete all InteractiveButton after exit to avoid duplicate save choice appeared.
    public void Exit_Choose_Save(){
        foreach(Transform child in Content){
            Destroy(child.gameObject);
        }
    }

    public void QuitGame(){
        Application.Quit();
    }

    //------------------WHEN THE GAME STARTS---------------------
    private string GetSavingPath(){
        //get the path to the "My Documents" folder.
        string documentsPath =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //get the path to LCI document.
        string gameSavingsFolderPath =Path.Combine(documentsPath,"My Games",saveFolderName);

        //create LCI if it dosen't exist.
        if(!Directory.Exists(gameSavingsFolderPath)){
            Directory.CreateDirectory(gameSavingsFolderPath);
        }
        return gameSavingsFolderPath;
    }

    private void LoadFromFile(string path){
        try{
            savingNames =Directory.GetFiles(path);
            foreach(string savingName in savingNames){
                //Debug.Log(Path.GetFileName(savingName));
            }
        }catch(Exception e){
            Debug.LogError("Saving Check Error: "+e.Message);
        }
    }
    //----------------------------------------------------------
}
