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
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _gameHelper;

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
        updateScore();
        updateHelper();
    }

    void updateTimer()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            resetTimer();
            cg.alpha = 1f - Mathf.Abs(timer / maxTime);
        }
        _gameTimer.text = "Elapsed Time: " + System.Math.Round(timer, 3);
    }

    void updateScore()
    {
        _gameScore.text = "Score: " + coins;
    }

    void resetTimer(){
        if (timer > maxTime)
        {
            isLoading = false;
            timer = 0f;
            cg.alpha = 0f;
        }
    }

    void updateHelper(){
        if(PlayerManager.instance.PlayerCanPushBox() && !PlayerManager.instance.PlayerHasPushBox()){
            _gameHelper.text = "Hint: If you come in contact with boxes, press the 'E' key to push the box.";
        }
        else if(PlayerManager.instance.PlayerCanPushButton() && !PlayerManager.instance.PlayerHasPushButton()){
            _gameHelper.text = "Hint: If you come in contact with a door, press the 'E' key to open the door.";
        }
        else{
            _gameHelper.text = "";
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
