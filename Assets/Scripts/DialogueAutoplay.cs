using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueAutoplay : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 50f;

    public Coroutine Run(string textToType, TMP_Text lbText)
    {
 
        return StartCoroutine(TypeText(textToType, lbText));
    }

    private IEnumerator TypeText(string textToType, TMP_Text lbText){

        lbText.text = string.Empty; //reset any text before commencing.

        float time    = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length){

            time     += Time.deltaTime * typeWriterSpeed;

            charIndex = Mathf.FloorToInt(time);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            lbText.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        lbText.text = textToType;
    }
}

