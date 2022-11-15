using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static int currentLevel = 1;
    public static int highestLevel = 2;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Reset() {
        currentLevel = 1;
        LoadLevel();
    }
    public void IncreaseLevel() {
        if (currentLevel < highestLevel) {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }
        LoadLevel();
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + currentLevel);
        PlayerManager.instance.SetPosition(new Vector3(0,1,0));
    }
}
