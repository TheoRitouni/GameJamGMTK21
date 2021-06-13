using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Manager
{
    private static UIManager instance;

    // REFERENCES
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;


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
        GameManager.getInstance().onScore += UpdateScore;
    }

    private void Update()
    {
        timerText.text = Mathf.Clamp(Mathf.FloorToInt(GameManager.getInstance().currentTimer),0,60).ToString(format: "00");
    }

    private void UpdateScore(int pScore)
    {
        scoreText.text = pScore.ToString(format:"00000");
    }

    public static UIManager getInstance()
    {
        if (instance == null)
            new GameObject("UIManager").AddComponent<UIManager>();

        return instance;
    }
}
