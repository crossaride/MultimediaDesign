using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndUI : MonoBehaviour
{
   
    [SerializeField] private TMP_Text lbText;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    private DialogueAutoplay dialogueAutoplay;

    private void Start()
    {
        dialogueAutoplay = GetComponent<DialogueAutoplay>();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        openDialogueBox();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return dialogueAutoplay.Run(dialogue, lbText);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        
        CloseDialogueBox();
        SceneManager.LoadScene("BadEnding");
    }

    public void openDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        lbText.text = string.Empty;
    }
}
