using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    CanvasGroup cg;

    public static int currentLevel = 1;
    public static int highestLevel = 2;

    [SerializeField] private TextMeshProUGUI _gameTimer;
    private float timer = 0f;
    public float maxTime = 10.0f;
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
        updateTimer();
    }

    void updateTimer()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                isLoading = false;
                timer = 0f;
                cg.alpha = 0f;
            }
            cg.alpha = 1f - Mathf.Abs(timer / maxTime);
        }
        _gameTimer.text = "Time Left: " + System.Math.Round(timer, 3);
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
