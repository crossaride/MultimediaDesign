using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lbText;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    private DialogueAutoplay dialogueAutoplay;
    private ResponseHandler responseHandler;

    public bool IsOpen { get; private set; }

    private string sceneName;

    private string isObj;


    private void Start()
    {
        dialogueAutoplay = GetComponent<DialogueAutoplay>();
        responseHandler  = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        OpenDialogueBox();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return dialogueAutoplay.Run(dialogue, lbText);

            if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.Responses != null && dialogueObject.Responses.Length > 0) 
            break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        }
        
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {   
            CloseDialogueBox();

            if(sceneName.Contains("Yes"))
            {
                if(isObj.Contains("Desk"))
                {
                    SceneManager.LoadScene("GoodEnd");
                }
                else if(isObj.Contains("Bed"))
                {
                    SceneManager.LoadScene("BadEnd");
                }
                else if(isObj.Contains("TV"))
                {
                    SceneManager.LoadScene("BadEnd");
                }
                else if(isObj.Contains("Outside"))
                {
                    SceneManager.LoadScene("BadEnd");
                }
            }
        }
    }

    public void OpenDialogueBox()
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        lbText.text = string.Empty;
    }

    public void SetSceneName(string newSceneName)
    {
        this.sceneName = newSceneName;
    } 

    public void SetObj(string newIsObj)
    {
        this.isObj = newIsObj;
    } 
}
