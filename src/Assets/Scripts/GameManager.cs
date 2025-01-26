using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField]
    private LevelManagerProfile levelManagerProfile;
    private int currentLevel = 0;

    void Start()
    {
        if(instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        }else if(instance != this) {
            Destroy(gameObject);
        }
    }

    void Update() {
        ResetLevel();
        if(Input.GetKey(KeyCode.Escape)) {
            Quit();
        }
        
    }

    public static void NextLevel() {
        instance.currentLevel++;
        if(instance.currentLevel < instance.levelManagerProfile.levels.Count) {
            SceneManager.LoadScene(instance.levelManagerProfile.levels[instance.currentLevel].sceneName);
        }

    }

    public static void FirstLevel() {
        instance.currentLevel = 0;
        if(instance.currentLevel < instance.levelManagerProfile.levels.Count) {
            SceneManager.LoadScene(instance.levelManagerProfile.levels[0].sceneName);
        }
    }

    public static void Quit() {
        Application.Quit();
    }

    static void ResetLevel() {
        if(!Input.GetKeyUp(KeyCode.R)) return;
        SceneManager.LoadScene(instance.levelManagerProfile.levels[instance.currentLevel].sceneName);
    }
}
