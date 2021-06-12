using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Manager
{
    private static LevelManager instance;
    private bool _isLoading = true;

    private int levelIndex = 0;
    [SerializeField] List<Level> levelList;

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


    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Init()
    {
        levelIndex = 0;
        Instantiate(levelList[levelIndex]);
        GameManager.getInstance().currentTimer = Level.getCurrentLevel().levelTimerMax;

        _isLoading = false;
    }

    public void NextLevel()
    {
        _isLoading = true;

        levelIndex++;
        if (levelIndex <= levelList.Count-1)
            StartCoroutine(LoadNextLevel(levelList[levelIndex]));

        else
        {
            Time.timeScale = 0;
            Debug.Log("you win !");
        }
    }

    public bool isLoading => _isLoading;

    IEnumerator LoadNextLevel(Level pLevel)
    {

        yield return new WaitForSecondsRealtime(Level.getCurrentLevel().UnLoad());

        Instantiate(pLevel);
        GameManager.getInstance().currentTimer = pLevel.levelTimerMax;

        _isLoading = false;
    }

    public static LevelManager getInstance()
    {
        if (instance == null)
            new GameObject("LevelManager").AddComponent<LevelManager>();

        return instance;
    }
}
