using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //put inside each character's dialogue collider.
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject DialogueTrigger;
    public GameObject DialogueBox;

    private Queue<string> sentences;

    UserData save;

    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        sentences =new Queue<string>();
    }

    void OnEnable(){
        Control.EnableDialogueEvent +=DialogueActivate;
    }

    void OnDisable(){
        Control.EnableDialogueEvent -=DialogueActivate;
    }

    void DialogueActivate(){
        DialogueBox.SetActive(true);
        StartDialogue(save.current_Dialogue.dialogue);
    }

    void DialogueDeactivate(){
        DialogueBox.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue){
        if(save.current_Dialogue !=null){
            nameText.text =save.current_Dialogue.dialogue.npc_name;
            sentences.Clear();

            foreach(string sentence in dialogue.sentences){
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence(){
        if(save.current_Dialogue !=null){
            //end of queue.
            if(sentences.Count ==0){
                EndDialogue();
                return;
            }
        }
    }

    //not sure how to use this one yet, maybe change the "countinue" to "end"?
    void EndDialogue(){

    }
}
