using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameSound gs;

    CanvasGroup cg;

    public static int currentLevel = 1;
    public static int highestLevel = 1;

    [SerializeField] private TextMeshProUGUI _gameTimer;
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _gameHelper;

    private float timer = 0f;
    public float maxTime = 1000.0f;
    private float loadingTimer = 0f;
    private float loadingTime = 1f;
    public bool isLoading = true;

    int coins = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        cg = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
        gs = GetComponent<GameSound>();
    }

    void Update()
    {
        updateTimer();
        updateScore();
        updateHelper();
        updateLoading();
    }

    void updateTimer()
    {
        timer += Time.deltaTime;
        _gameTimer.text = "Elapsed Time: " + System.Math.Round(timer, 3);
    }
    void updateLoading() {
        loadingTimer += Time.deltaTime;
        if (isLoading)
        {
            if (loadingTimer > loadingTime)
            {
                isLoading = false;
                loadingTimer = 0f;
                cg.alpha = 0f;
                return;
            }
            cg.alpha = 1f - loadingTimer / loadingTime;
        }
    }

    void updateScore()
    {
        _gameScore.text = "Score: " + coins;
    }

    void updateHelper()
    {
        if (PlayerManager.instance.PlayerCanPushBox() && !PlayerManager.instance.PlayerHasPushBox())
        {
            _gameHelper.text = "Hint: If you come in contact with boxes, press the 'E' key to push the box.";
        }
        else if (PlayerManager.instance.PlayerCanPushButton() && !PlayerManager.instance.PlayerHasPushButton())
        {
            _gameHelper.text = "Hint: If you come in contact with a door, press the 'E' key to open the door.";
        }
        // else if(PlayerManager.instance.PlayerHasDied()){
        //     _gameHelper.text = ""
        // }
        else
        {
            _gameHelper.text = "";
        }

    }

    public void Reset()
    {
        currentLevel = 1;
        LoadLevel();
    }
    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            currentLevel++;
            LoadLevel("Level" + currentLevel);
        }
    }

    public void LoadLevel(string name = "default")
    {
        SceneManager.LoadScene(name);
        if (name == "Credits") return;
        isLoading = true;
        timer = 0f;
        loadingTimer = 0f;
        PlayerManager.instance.SetPosition(new Vector3(0, 1, 0));
    }

    public void CollectCoin()
    {
        coins += 1;
        gs.CollectCoin();
        print("Coins: " + coins);
    }
}
