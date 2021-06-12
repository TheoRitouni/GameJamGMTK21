using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// Singleton class in charge of screen shakes
public class ShakeManager : Manager
{
    private static ShakeManager instance;
    private Transform cam;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        if (instance != null)
            Destroy(this);

        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            cam = Camera.main.transform;
            SetParent();
        }
    }

    private IEnumerator ScreenShake(ShakeData pData)
    {
        float lTimer = 0;

        while (lTimer < pData.duration)
        {
            float lTimeScaler = pData.isTimeScaled ? Time.deltaTime : Time.fixedDeltaTime;
            float lProgress = lTimer / pData.duration;
            Vector3 lNextPosition = Random.insideUnitSphere * pData.magnitude * pData.curve.Evaluate(lProgress);

            cam.localPosition = lNextPosition;
            lTimer += lTimeScaler;
            yield return new WaitForEndOfFrame();
        }

        cam.localPosition = Vector3.zero;
    }

    /// Triggers a screen shake 
    /// <param name="pData">Associated parameters</param>
    public void Shake(ShakeData pData)
    {
        if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
        shakeCoroutine = StartCoroutine(ScreenShake(pData));
    }

    /// <summary>
    /// Stop every ScreenShakes currently running
    /// </summary>
    public void StopShake()
    {
        StopAllCoroutines();
        cam.localPosition = Vector3.zero;
    }

    /// Returns ShakeManager unique instance and create one if it doesn't exist
    /// <returns> ShakeManager instance </returns>
    public static ShakeManager getInstance()
    {
        if (instance == null)
            new GameObject("ShakeManager").AddComponent<ShakeManager>();

        return instance;
    }
}
