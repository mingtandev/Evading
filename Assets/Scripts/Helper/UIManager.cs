using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoSingleton<UIManager>
{
    //BUTTON PAUSE GAME

    const string FADE = "fade";
    const string TRANSLATE = "Translate";
    const string STARTGAME = "StartGame";

    [Header("Panel Pause")]
    [SerializeField] GameObject PanelPause;
    [SerializeField] Button btnGoHome;
    [SerializeField] Button btnPauseSetting;
    Animator a_PannelPause;

    [Header("Panel Result")]
    [SerializeField] GameObject PanelResults;
    [SerializeField] Button btnResultSetting;
    [SerializeField] TextMeshProUGUI rsBestScore;
    [SerializeField] TextMeshProUGUI rsCurrentScore;
    [SerializeField] TextMeshProUGUI rsCoin;
    Animator a_PanelResults;

    [Header("Panel Home")]
    [SerializeField] GameObject PanelHome;
    [SerializeField] Button btnHomeSetting;
    Animator a_PanelHome;


    [Header("Panel Game")]
    [SerializeField] GameObject PanelGame;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI pointGame;
    Animator a_PanelGame;

    [Header("Panel Setting")]
    [SerializeField] GameObject PanelSetting;
    [SerializeField] Toggle toggleSound;
    [SerializeField] Toggle toggleMusic;
    [SerializeField] Button quit;
    [SerializeField] Sprite offToggle;
    [SerializeField] Sprite onToggle;
    [SerializeField] Image spToggleSound;
    [SerializeField] Image spToggleMusic;

    new void Awake()
    {
        Time.timeScale = 0;
        GetAnimator();
        AddEventListener();
    }

    private void OnEnable()
    {
        GameManager.e_coinGamePlay += SetTempCoin;
        GameManager.e_coinGamePlay += SetResultCoin;
        GameManager.e_resultPoint += SetResultPoint;
        GameManager.e_resultPoint += SetHighSocre;
        SetTempCoin(GameManager.Instance.CoinGame);
        ToggleMusicChangeValue(SoundManager.onMusic != 0);
        ToggleSoundChangeValue(SoundManager.onSound != 0);
        toggleSound.isOn = (SoundManager.onSound != 0);
        toggleMusic.isOn = (SoundManager.onMusic != 0);
    }

    private void OnDisable()
    {
        GameManager.e_coinGamePlay -= SetTempCoin;
        GameManager.e_coinGamePlay -= SetResultCoin;
        GameManager.e_resultPoint -= SetResultPoint;
        GameManager.e_resultPoint -= SetHighSocre;


    }

    void AddEventListener()
    {
        btnGoHome.onClick.AddListener(GoHomeScene);
        AddOnclickSetting();
        toggleSound.onValueChanged.AddListener(ToggleSoundChangeValue);
        toggleMusic.onValueChanged.AddListener(ToggleMusicChangeValue);
        quit.onClick.AddListener(QuitSetting);

    }

    void AddOnclickSetting()
    {
        btnHomeSetting.onClick.AddListener(SettingShow);
        btnPauseSetting.onClick.AddListener(SettingShow);
        btnResultSetting.onClick.AddListener(SettingShow);
    }

    void GetAnimator()
    {
        a_PannelPause = PanelPause.GetComponent<Animator>();
        a_PanelHome = PanelHome.GetComponent<Animator>();
        a_PanelGame = PanelGame.GetComponent<Animator>();
        a_PanelResults = PanelResults.GetComponent<Animator>();
    }

    public void TogglePause()
    {
        Time.timeScale = 1 - Time.timeScale;
        PanelPause.SetActive(true);
        a_PanelGame.SetBool(FADE, !a_PanelGame.GetBool(FADE));
        a_PannelPause.SetBool(FADE, !a_PannelPause.GetBool(FADE));
        if (Time.timeScale == 1) SoundManager.Instance.AwakeAllLoop();
        else SoundManager.Instance.StopAllLoop();
    }

    public void TapToPlay()
    {
        a_PanelHome.SetBool(TRANSLATE, !a_PanelHome.GetBool(TRANSLATE));
        a_PanelHome.SetTrigger(STARTGAME);
        PanelGame.SetActive(true);
        Time.timeScale = 1;
        SoundManager.Instance.PlayLoop("bg_loop");
        SoundManager.Instance.Play("engine_veh");
    }

    public void GoHomeScene()
    {
        Time.timeScale = 0;
        GameManager.Instance.ResetStateGame();
        PanelPause.SetActive(false);
        PanelGame.SetActive(false);
        PanelResults.SetActive(false);

        PanelHome.SetActive(true);
        a_PanelResults.SetBool(FADE, !a_PanelResults.GetBool(FADE));
        a_PanelHome.SetBool(TRANSLATE, !a_PanelHome.GetBool(TRANSLATE));
    }

    public void PlayAgain()
    {
        Time.timeScale = 1 - Time.timeScale;
        GameManager.Instance.ResetStateGame(); //CHECKED

        a_PanelResults.SetBool(FADE, !a_PanelResults.GetBool(FADE));
        a_PanelGame.SetBool(FADE, !a_PanelGame.GetBool(FADE));
        SoundManager.Instance.AwakeAllLoop();

    }

    public void ResultSceneShow()
    {
        Time.timeScale = 0;
        PanelResults.SetActive(true);
        a_PanelResults.SetBool(FADE, !a_PanelResults.GetBool(FADE));
        a_PanelGame.SetBool(FADE, !a_PanelGame.GetBool(FADE));
        SoundManager.Instance.StopAllLoop();
    }

    public void SettingShow()
    {
        PanelSetting.SetActive(true);
    }

    public void QuitSetting()
    {
        PanelSetting.SetActive(false);
    }

    void ToggleSoundChangeValue(bool check)
    {
        toggleSound.isOn = check;
        SoundManager.Instance.CheckToggleSound(check);
        if (check) spToggleSound.sprite = onToggle;
        else spToggleSound.sprite = offToggle;
    }

    void ToggleMusicChangeValue(bool check)
    {

        toggleMusic.isOn = check;
        SoundManager.Instance.CheckToggleMusic(check);
        if (check) spToggleMusic.sprite = onToggle;
        else spToggleMusic.sprite = offToggle;
    }

    public void SetTempCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SetResultCoin(int coin)
    {
        rsCoin.text = coin.ToString();
    }

    public void SetResultPoint(int point)
    {
        StartCoroutine(AnimationCurrentScore(point));
    }

    //Animation for Result Point
    IEnumerator AnimationCurrentScore(int p)
    {
        yield return new WaitForSecondsRealtime(1f);
        int temp = 0;
        int Point = p;
        while (temp < Point)
        {
            temp += 1;
            rsCurrentScore.text = temp.ToString();
            yield return new WaitForSecondsRealtime(0.0001f);
        }
    }

    public void SetHighSocre(int point)
    {
        if (point > Int32.Parse(rsBestScore.text))
        {
            rsBestScore.text = point.ToString();
        }
    }

    public void SetPointGame(int point)
    {
        pointGame.text = point.ToString();
    }
}
