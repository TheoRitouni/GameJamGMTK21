using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Manager
{
    private static GameManager instance;

    public UnityAction<int> onScore;

    private int currentScore;
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
        currentScore += pAmount;
        onScore?.Invoke(currentScore);
    }

    public static GameManager getInstance()
    {
        if (instance == null)
            new GameObject("GameManager").AddComponent<GameManager>();

        return instance;
    }
}
