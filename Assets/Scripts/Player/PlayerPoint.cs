using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.FPoint+=Time.deltaTime*10;
        GameManager.Instance.Point = Mathf.FloorToInt(GameManager.Instance.FPoint);
        UIManager.Instance.SetPointGame(GameManager.Instance.Point);
    }
}
