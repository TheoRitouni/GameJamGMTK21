using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class ManagerMenuPause : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private GameObject menuPause;
    [SerializeField]
    private GameObject selectedObject;
    [SerializeField]
    private EventSystem eventSystem;

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
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.getInstance().isGameEnd && !LevelManager.getInstance().isLoading)
            {
                isPause = !isPause;
                menuPause.SetActive(isPause);
                eventSystem.SetSelectedGameObject(selectedObject);
                Time.timeScale = isPause? 0:1;
            }
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
