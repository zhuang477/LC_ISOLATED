using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    private string saveFolderName ="LCI";
    string[] savingNames;

    public List<GameObject> buttons;
    public GameManager gameManager;

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
                    saveName = inputField.text,
                    level = new List<int>(),
                    XP = 0,
                    hitpoint = 50,
                    stanima = 50,

                    weapon_id = 0,
                    armor_id = 1,

                    perks_unlocked = new List<int>()
                };
                //2. convert the UserData file into JSON.
                string JSONfile =JsonUtility.ToJson(newSave);
                string gameSavingsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", saveFolderName);

                string fileName = inputField.text+".json"; // Specify the desired file name
                string filePath = Path.Combine(gameSavingsFolderPath, fileName);
                File.WriteAllText(filePath, JSONfile);
                    
                //persist the data, enter the game(change scene)
                gameManager.currentSaving =newSave;
        }
    }

    /*
    *
                //no duplicate files.
                else{
                    //create a new save file.
                    //1. create a new UserData file.
                    UserData newSave = new UserData
                    {
                        saveName = inputField.text,
                        level = new List<int>(),
                        XP = 0,
                        hitpoint = 50,
                        stanima = 50,

                        weapon_id = 0,
                        armor_id = 1,

                        perks_unlocked = new List<int>()
                    };
                    //2. convert the UserData file into JSON.
                        string JSONfile =JsonUtility.ToJson(newSave);
                        Debug.Log(JSONfile);
                        //get the path to the "My Documents" folder.
                        string documentsPath =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //get the path to LCI document.
                        string gameSavingsFolderPath =Path.Combine(documentsPath,"My Games",saveFolderName);
                        File.WriteAllText(gameSavingsFolderPath,JSONfile);
                    
                    //persist the data, enter the game(change scene)
                }
    */

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
