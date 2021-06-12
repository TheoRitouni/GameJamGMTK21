using System.Collections;
using UnityEngine;

public class GameManager : Manager
{
    private static GameManager instance;
    
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
    }

    public static GameManager getInstance()
    {
        if (instance == null)
            new GameObject("GameManager").AddComponent<GameManager>();

        return instance;
    }
}
