using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class ManagerMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider sliderSfx;
    [SerializeField]
    private Slider sliderBgm;

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.SetFloat("sfxVolume", GlobalValues.sfxVolume); ;
        audioMixer.SetFloat("bgmVolume", GlobalValues.bgmVolume);

        sliderSfx.value = GlobalValues.sfxVolume;
        sliderBgm.value = GlobalValues.bgmVolume;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void EscapeGame()
    {
        Application.Quit();
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
