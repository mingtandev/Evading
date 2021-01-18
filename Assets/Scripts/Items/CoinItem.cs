using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinItem : MonoBehaviour , ICollidedWithPlayer
{
    WaitForSeconds wait = new WaitForSeconds(10f);


    public void Collided()
    {
        SoundManager.Instance.PlayOneShot("take_coin");
        GameManager.Instance.CoinGame++;
        Debug.Log(GameManager.Instance.CoinGame);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimeLieft());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimeLieft()
    {
        yield return wait;
        Destroy(gameObject.GetComponent<Indicator>().representObject);
        Destroy(gameObject);
    }
}
