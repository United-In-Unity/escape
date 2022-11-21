using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    CanvasGroup cg;

    public static int currentLevel = 1;
    public static int highestLevel = 2;

    public float timer = 0f;
    public float maxTime = 1.0f;
    public bool isLoading = false;

    int coins = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        cg = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
    }

    void Update() {
        if (isLoading) {
            timer += Time.deltaTime;
            if (timer > maxTime) {
                isLoading = false;
                timer = 0f;
                cg.alpha = 0f;
                return;
            }
            cg.alpha = 1f - Mathf.Abs(timer/maxTime);
        }
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

    public void LoadLevel(string name = "default") {
        if (name=="default") {
            SceneManager.LoadScene("Level" + currentLevel);
        }
        else {
            SceneManager.LoadScene(name);
        }
        isLoading = true;

        PlayerManager.instance.SetPosition(new Vector3(0,1,0));
    }

    public void CollectCoin() {
        coins += 1;
        print("Coins: " + coins);
    }
}
