using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_PopUp : MonoBehaviour
{
    public GameObject icons;

    void Start()
    {
        icons = GameObject.FindGameObjectWithTag("Quest");
        icons.SetActive(true);
    }

}
