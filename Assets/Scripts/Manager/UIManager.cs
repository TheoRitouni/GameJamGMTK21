using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // FIELDS

    // PUBLIC FIELDS
    public int scoreNumber = 0;

    // SERIALIZE FIELDS
    [SerializeField] private int guardPoint = 50;
    [SerializeField] private int birdPoint = 100;
    [SerializeField] private int letterBirdPoint = 150;

    // REFERENCES
    [SerializeField] private TextMeshProUGUI score;

    
    public void UpdateScore(string enemyType)
    {
        switch (enemyType)
        {
            case "Guard":
                scoreNumber += guardPoint;
                break;
            case "Bird":
                scoreNumber += birdPoint;
                break;
            case "LetterBird":
                scoreNumber += letterBirdPoint;
                break;
        }
    }

    private void Update()
    {
        score.text = scoreNumber.ToString();
    }
}
