using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// Singleton class in charge of screen shakes
public class HitstopManager : Manager
{
    private static HitstopManager instance;
    private Coroutine coroutine;

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

    public void PlayHitStop(HitstopData pData)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(HitStop(pData));

        else return;
    }

    private IEnumerator HitStop(HitstopData pData)
    {
        Time.timeScale = pData.amount;
        yield return new WaitForSecondsRealtime(pData.duration);
        if(!GameManager.getInstance().isGameEnd)Time.timeScale = 1;
        coroutine = null;
    }

    /// Returns HitstopManager unique instance and create one if it doesn't exist
    /// <returns> HitstopManager instance </returns>
    public static HitstopManager getInstance()
    {
        if (instance == null)
            new GameObject("HitstopManager").AddComponent<HitstopManager>();

        return instance;
    }
}
