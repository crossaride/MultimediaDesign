using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private string tempName;

    GameObject obj;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        obj = GameObject.FindGameObjectWithTag("Option");
        obj.SetActive(false);
    }
    
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true); 
        obj.SetActive(true);

    }

    private void OnPickedResponse(Response response) //, int responseIndex
    {
        responseBox.gameObject.SetActive(false);

        string temp2 = response.ResponseText;
        Debug.Log("response: " + temp2);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        dialogueUI.SetSceneName(temp2);
        dialogueUI.ShowDialogue(response.DialogueObject);
    } 

    public void setName(string newName){
        this.tempName = newName;
    }
}