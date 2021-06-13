using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Manager
{
    private static GameManager instance;

    public UnityAction<int> onScore;
    public UnityAction onGameOver;
    public UnityAction onWin;
    public bool isGameEnd = false;

    private int _currentScore;
    public float currentTimer;

    void Awake()
    {
        if (instance != null)
            Destroy(this);

        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            SetParent();
        }
    }

    private void Start()
    {
        LevelManager.getInstance().Init();
        currentTimer = Level.getCurrentLevel().levelTimerMax;
    }

    private void Update()
    {
        if(currentTimer > 0)
            currentTimer -= Time.deltaTime;

        else if(!LevelManager.getInstance().isLoading)
            LevelManager.getInstance().NextLevel();
    }

    public void IncreaseScore(int pAmount)
    {
        _currentScore += pAmount;
        onScore?.Invoke(_currentScore);
    }

    public static GameManager getInstance()
    {
        if (instance == null)
            new GameObject("GameManager").AddComponent<GameManager>();

        return instance;
    }

    public float currentScore => _currentScore;

    public void GameOver()
    {
        isGameEnd = true;
        Time.timeScale = 0;
        onGameOver?.Invoke();
    }

    public void Win()
    {
        isGameEnd = true;
        Time.timeScale = 0;
        onWin?.Invoke();
    }
}