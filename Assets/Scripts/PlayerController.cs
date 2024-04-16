using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{
    
    [SerializeField] float moveSpeed = 3.8F; 
    [SerializeField] private Animator animator;
    [SerializeField] private DialogueObject PC_dialogue;
    [SerializeField] private DialogueObject BED_dialogue;
    [SerializeField] private DialogueObject TV_dialogue;
    [SerializeField] private DialogueObject EXIT_dialogue;
    private Vector2 movement;
    private new Rigidbody2D rigidbody2D;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public GameObject icon, icon2, icon3;

    private bool deskTrigger = false;
    private bool bedTrigger = false;
    private bool tvTrigger = false;
    private bool exitTrigger = false;


    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator    = GetComponent<Animator>();

        rigidbody2D.freezeRotation = true; //prevent player from auto-rotate when collide with gameObjects

        //Hide Tooltips
        icon  = GameObject.FindGameObjectWithTag("Icon_PC");
        icon.SetActive(false);
        
        icon2 = GameObject.FindGameObjectWithTag("Icon_TV");
        icon2.SetActive(false);
        
        icon3 = GameObject.FindGameObjectWithTag("Icon_BED");
        icon3.SetActive(false);
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking",false);
        }
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        if(dialogueUI.IsOpen == true) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //prevent diagonal movements
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            movement.y = 0;
        }
        else
        {
            movement.x = 0;
        }

        if (deskTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueUI.SetObj("Desk");
                dialogueUI.ShowDialogue(PC_dialogue);
                icon.SetActive(false);
            }
        }

        if (tvTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueUI.SetObj("TV");
                dialogueUI.ShowDialogue(TV_dialogue);
                icon2.SetActive(false);
            }
        }

         if (bedTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueUI.SetObj("Bed");
                dialogueUI.ShowDialogue(BED_dialogue);
                icon3.SetActive(false);
            }
        }

        if (exitTrigger == true)
        {
            dialogueUI.SetObj("Outside");
            dialogueUI.ShowDialogue(EXIT_dialogue);  
            exitTrigger = false;    
        }

    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /// Sent when an incoming collider makes contact with this object's collider (2D physics only).
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Desk")
        {
            icon.SetActive(true);
            deskTrigger = true;
        }
        else if (collision.gameObject.name == "TV")
        {  
            icon2.SetActive(true);
            tvTrigger = true;
        }
        else if (collision.gameObject.name == "Bed")
        {
            icon3.SetActive(true);
            bedTrigger = true;
        }else if (collision.gameObject.name == "Exit")
        {
            exitTrigger = true;
        }

        Debug.Log("Object that collided with me: " + collision.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D collision2)
    {
        deskTrigger = false;
        tvTrigger   = false;
        bedTrigger  = false;   
        icon.SetActive(false);
        icon2.SetActive(false);
        icon3.SetActive(false);
    }

}
