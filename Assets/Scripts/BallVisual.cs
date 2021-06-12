using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisual : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Transform player;

    [Header("ScreenShakes")]
    [SerializeField] ShakeData[] shakeDataList;

    [Header("Indicator")]
    [SerializeField] Transform launchIndicator;
    [SerializeField] float offset;

    [Header("Chain")]
    [SerializeField] LineRenderer chain;

    private void Start()
    {
        ball.onHitGround += OnHitGround;
        ball.onHitEnemy += OnHitEnemy;
    }
    private void Update()
    {
        IndicatorSetting();
        ChainSetting();
    }

    void OnHitGround()
    {
        ShakeManager.getInstance().Shake(shakeDataList[0]);
    }

    void OnHitEnemy()
    {
        ShakeManager.getInstance().Shake(shakeDataList[1]);
    }

    void IndicatorSetting()
    {
        Vector2 dirJoystickLeft = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (dirJoystickLeft.magnitude > 0.1f)
        {
            launchIndicator.localPosition = dirJoystickLeft.normalized * offset;
            launchIndicator.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(dirJoystickLeft.y, dirJoystickLeft.x)*Mathf.Rad2Deg)-90);

            if (!launchIndicator.gameObject.activeSelf)
                launchIndicator.gameObject.SetActive(true);
        }

        else if (launchIndicator.gameObject.activeSelf)
            launchIndicator.gameObject.SetActive(false);
    }

    void ChainSetting()
    {
        chain.SetPosition(0, transform.position);
        chain.SetPosition(1, player.position);
    }
}
