using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Trigger : MonoBehaviour
{
   Quest_PopUp qp;

   [SerializeField] private DialogueUI dialogueUI;
[SerializeField] private DialogueObject Quest_dialogue;

   private bool Trigger = false;


    void Start()
    {
        qp = new Quest_PopUp();
        qp.icons = GameObject.FindGameObjectWithTag("Quest");
    }

     void Update()
    {
        if (Trigger == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueUI.ShowDialogue(Quest_dialogue);
                qp.icons.SetActive(false);
                Trigger = false;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            Trigger = true;
        }
    }


    /// Sent when a collider on another object stops touching this object's collider (2D physics only).

    void OnCollisionExit2D(Collision2D collision2)
    {
        qp.icons.SetActive(false);
    }
    
}
