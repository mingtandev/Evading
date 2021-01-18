using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class GameManager : MonoSingleton<GameManager>
{
    Camera cam;
    [SerializeField] UIManager UIManager;
    public PlayerController playerController;
    [SerializeField] PlayerPoint playerPoint;
    [SerializeField] InfinityBG bg;


    public static Action<int> e_coinGamePlay;
    public static Action<int> e_resultPoint;



    public static string COIN_KEY = "COIN_KEY";
    public static string COIN_GAME = "COIN_GAME";
    public static string BEST_SCORE = "BEST_SCORE";

    int _coin;
    int _gameCoin = 0;
    public int Coin
    {
        get => _coin;
        set
        {
            _coin = value;
            PlayerPrefs.SetInt(COIN_KEY, value);
        }
    }
    public int CoinGame
    {
        get => _gameCoin;
        set
        {
            _gameCoin = value;
            e_coinGamePlay?.Invoke(value);
        }
    }


    public float FPoint;
    int _point;
    int _hightPoint;

    public int Point
    {
        get => _point;
        set => _point = value;
    }

    public int HightScorePoint
    {
        get => _hightPoint;
        set
        {
            if (value > HightScorePoint)
            {
                _hightPoint = value;
                PlayerPrefs.SetInt(BEST_SCORE, _hightPoint);
            }
        }
    }

    private new void Awake()
    {
        Coin = PlayerPrefs.GetInt(COIN_KEY, 0);
        HightScorePoint = PlayerPrefs.GetInt(BEST_SCORE, 0);

        
        cam = Camera.main;
    }



 
    public void ResetStateGame()
    {
        FPoint = 0;
        cam.transform.position = new Vector3(0, 0, 0);
        playerController.ResetState();
        CoinGame = 0;
        bg.ResetPos();
        ObjectPool.Instance.ResetAllObject();
    }

    public void ResultState()
    {
        HightScorePoint = Point;
        Coin += CoinGame;
        e_resultPoint?.Invoke(Point);
        UIManager.Instance.ResultSceneShow();
    }

    public void IncreasePoint(int point)
    {
        GameManager.Instance.FPoint += point;
    }
}
