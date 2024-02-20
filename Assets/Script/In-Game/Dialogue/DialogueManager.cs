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

    public GameObject DialogueBox;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences =new Queue<string>();
    }

    void OnEnable(){
        DialogueColliderDetect.EnableDialogueEvent +=DialogueActivate;
        DialogueColliderDetect.DisableDialogueEvent +=DialogueDeactivate;
    }

    void OnDisable(){
        DialogueColliderDetect.EnableDialogueEvent -=DialogueActivate;
        DialogueColliderDetect.DisableDialogueEvent -=DialogueDeactivate;
    }

    void DialogueActivate(){
        DialogueBox.SetActive(true);
    }

    void DialogueDeactivate(){
        DialogueBox.SetActive(false);
    }

    public void StartDialogue (Dialogue dialogue){
        nameText.text =dialogue.npc_name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count ==0){
            EndDialogue();
            return;
        }
        string sentence =sentences.Dequeue();
        dialogueText.text =sentence;
    }

    void EndDialogue(){

    }
}
