using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisual : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Transform player;

    [Header("ScreenShakes")]
    [SerializeField] ShakeData[] shakeDataList;
    
    [Header("Hitstops")]
    [SerializeField] HitstopData[] hitstopDataList;

    [Header("Renderer")]
    [SerializeField] SpriteRenderer ballRenderer;

    [Header("Indicator")]
    [SerializeField] Indicator indicator;

    [Header("Chain")]
    [SerializeField] LineRenderer chain;

    [Header("Particles")]
    [SerializeField] ParticleSystem smokeParticles;

    [Header("Squash and Stretch")]
    [SerializeField] AnimationCurve squatchCurveX = new AnimationCurve(new Keyframe(0,1), new Keyframe(1, 1));
    [SerializeField] AnimationCurve squatchCurveY = new AnimationCurve(new Keyframe(0,1), new Keyframe(1, 1));
    private Vector3 initialScale;

    [Header("VFX")]
    [SerializeField] AudioSource audioSourcePropulsion;
    [SerializeField] AudioSource audioSourceHitWall;


    [Header("Camera")]
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

        ball.onHitGround += OnHitGround;
        ball.onHitEnemy += OnHitEnemy;
        ball.onChangeForce += OnChangeForce;
        ball.onPropulsion += OnPropulsion;

        indicator.indicatorScale = indicator.launchIndicator.transform.localScale;
        indicator.indicatorTrailTime = indicator.trailIndicator.time;
        initialScale = ballRenderer.transform.localScale;
    }

    private void Update()
    {
          if (Time.timeScale != 0)
        {
            IndicatorSetting();
            ChainSetting();
        }
    }

    private void LateUpdate()
    {
        SquashAndStretch();
    }

    void OnHitGround(Collision2D pCollision)
    {
        ParticleSystem lSmokeParticles = Instantiate(smokeParticles);
        lSmokeParticles.transform.position = pCollision.contacts[0].point;
        Vector2 lNormal = pCollision.contacts[0].normal;
        lSmokeParticles.transform.eulerAngles = new Vector3(0,0,(Mathf.Atan2(lNormal.y, lNormal.x) * Mathf.Rad2Deg) - 90);
        lSmokeParticles.Play();
        audioSourceHitWall.Play();

        ShakeManager.getInstance().Shake(shakeDataList[0]);
        HitstopManager.getInstance().PlayHitStop(hitstopDataList[2]);
    }

    void OnPropulsion()
    {
        if (!audioSourcePropulsion.isPlaying)
        {
            audioSourcePropulsion.Play();
        }
    }


    void SquashAndStretch()
    {
        Vector2 lVelocity = ball.velocity;
        float lRatio = lVelocity.magnitude/ball.maxForce;

        ballRenderer.transform.eulerAngles = new Vector3 (0,0,((Mathf.Atan2(lVelocity.normalized.y, lVelocity.normalized.x))*Mathf.Rad2Deg)-90);
        ballRenderer.transform.localScale = new Vector3 (squatchCurveX.Evaluate(lRatio)*initialScale.x, squatchCurveY.Evaluate(lRatio)*initialScale.y, initialScale.z);
    }

    void OnHitEnemy()
    {
        ShakeManager.getInstance().Shake(shakeDataList[1]);
        HitstopManager.getInstance().PlayHitStop(hitstopDataList[0]);
    }

    void IndicatorSetting()
    {
        Vector2 dirJoystickLeft;
        if (GlobalValues.switchControl)
            dirJoystickLeft = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
            dirJoystickLeft = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (dirJoystickLeft.magnitude > 0.1f)
        {
            indicator.launchIndicator.transform.localPosition = dirJoystickLeft.normalized * indicator.offset;
            indicator.launchIndicator.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(dirJoystickLeft.y, dirJoystickLeft.x)*Mathf.Rad2Deg)-90);

            if (!indicator.launchIndicator.gameObject.activeSelf)
                indicator.launchIndicator.gameObject.SetActive(true);
        }

        else if (indicator.launchIndicator.gameObject.activeSelf)
            indicator.launchIndicator.gameObject.SetActive(false);
    }

    void OnChangeForce(int pForce)
    {
        Color lColor = indicator.colorOverForce[pForce];
        Color lColorA = new Color(lColor.r, lColor.g, lColor.b, 1);

        indicator.trailIndicator.startColor = lColor;
        indicator.trailIndicator.time = indicator.indicatorTrailTime * indicator.timeOverForce[pForce];

        indicator.launchIndicator.color = lColorA;
        indicator.launchIndicator.transform.localScale = indicator.indicatorScale * indicator.sizeOverForce[pForce];
    }

    void ChainSetting()
    {
        chain.SetPosition(0, transform.position);
        chain.SetPosition(1, player.position);
    }
}

[System.Serializable]
public struct Indicator
{
    public SpriteRenderer launchIndicator;
    public TrailRenderer trailIndicator;
    public float offset;
    public List<Color> colorOverForce;
    public List<float> sizeOverForce;
    public List<float> timeOverForce;

    private float _indicatorTrailTime;
    private Vector3 _indicatorScale;

    //Getters
    public Vector3 indicatorScale
    {
        get => _indicatorScale;
        set { _indicatorScale = value; }
    }

    public float indicatorTrailTime
    {
        get => _indicatorTrailTime;
        set { _indicatorTrailTime = value; }
    }
}
