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

    public static void NextLevel() {
        instance.currentLevel++;
        if(instance.currentLevel < instance.levelManagerProfile.levels.Count) {
            SceneManager.LoadScene(instance.levelManagerProfile.levels[instance.currentLevel].sceneName);
        }

    }
}
