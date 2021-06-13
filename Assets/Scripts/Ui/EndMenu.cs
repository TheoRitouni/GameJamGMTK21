using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private EndType type = EndType.win;

    [SerializeField] private GameObject endMenu;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        switch (type)
        {
            case EndType.gameover:
                GameManager.getInstance().onGameOver += Active;
                break;

            case EndType.win:
                GameManager.getInstance().onWin += Active;
                break;
        }
    }

    private void Active()
    {
        endMenu.SetActive(true);
        scoreText.text = "SCORE: "+ GameManager.getInstance().currentScore.ToString("00000");
    }

    public void OnRetry()
    {
        Scene lCurrentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(lCurrentScene.buildIndex);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public enum EndType
{
    gameover,
    win,
}
