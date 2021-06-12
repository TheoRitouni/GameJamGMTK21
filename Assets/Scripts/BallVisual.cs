using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisual : MonoBehaviour
{
    private Ball ball;

    [Header("ScreenShakes")]
    [SerializeField] ShakeData[] shakeDataList;

    private void Start()
    {
        ball = GetComponent<Ball>();
        ball.onHitGround += OnHitGround;
        ball.onHitEnemy += OnHitEnemy;
    }

    void OnHitGround()
    {
        ShakeManager.getInstance().Shake(shakeDataList[0]);
    }

    void OnHitEnemy()
    {
        ShakeManager.getInstance().Shake(shakeDataList[1]);
    }
}
