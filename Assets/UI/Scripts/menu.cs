using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menu : MonoBehaviour
{
    GameObject Base;

    GameObject men;
    GameObject levels;
    GameObject settings;

    public TMP_Text mt;
    public TMP_Text st;

    public Slider musv;
    public Slider sfxv;

    float soundEffects = 100;
    float musicV = 100;

    bool level = false;
    bool set = false;

    AudioSource sound;
    public AudioClip bip;


    void Start(){
        Base = GameObject.Find("Base");
        men = GameObject.Find("menu");
        levels = GameObject.Find("levels");
        settings = GameObject.Find("settingsMenu");

        DontDestroyOnLoad(transform.gameObject);
        sound = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!level){
            Base.transform.Rotate(new Vector3(0, 1, 0));
            musicV = musv.value;
            soundEffects = sfxv.value;
            mt.text = "Music: " + musicV.ToString();
            st.text = "SFX: " + soundEffects.ToString();
        }
        else{
            if (!set && GameObject.Find("Event") != null){
                GameObject.Find("Event").GetComponent<towerControl>().setV(soundEffects, musicV);
                set = true;
            }
        }
    }

    public void play(){
        men.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, -225);
        levels.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, -225);
        sound.PlayOneShot(bip, soundEffects / 100);
    }
    public void back(){
        men.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, -225);
        levels.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, -225);
        settings.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, -225);
        sound.PlayOneShot(bip, soundEffects / 100);
    }
    public void setting(){
        settings.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, -225);
        men.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, -225);
        sound.PlayOneShot(bip, soundEffects / 100);
    }
    public void quit(){
        sound.PlayOneShot(bip, soundEffects / 100);
        Application.Quit();
    }

    public void finalLevel(){
        SceneManager.LoadScene("TSE Project Tower Defence Final Level", LoadSceneMode.Single);
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        level = true;

    }
}
