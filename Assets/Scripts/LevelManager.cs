using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string sceneName;

    public void setSceneName(string newSceneName){
        this.sceneName = newSceneName;  
    }

    public void changeScene(){
        SceneManager.LoadScene(sceneName);
    }
}
