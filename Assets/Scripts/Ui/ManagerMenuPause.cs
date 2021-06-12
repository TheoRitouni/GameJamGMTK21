using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class ManagerMenuPause : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private GameObject menuPause;

    [SerializeField]
    private Slider sliderSfx;
    [SerializeField]
    private Slider sliderBgm;


    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        audioMixer.SetFloat("sfxVolume", GlobalValues.sfxVolume);
        audioMixer.SetFloat("bgmVolume", GlobalValues.bgmVolume);

        sliderSfx.value = GlobalValues.sfxVolume;
        sliderBgm.value = GlobalValues.bgmVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0;
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuPause.SetActive(false);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReturnToMainMenu()
    {

    }

    public void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("sfxVolume", value);
        GlobalValues.sfxVolume = value;
    }
    public void SetBgmVolume(float value)
    {
        audioMixer.SetFloat("bgmVolume", value);
        GlobalValues.bgmVolume = value;

    }
}
