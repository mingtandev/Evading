using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnActiveAfterTime : MonoBehaviour
{

    public int time = 1;
    private void OnEnable()
    {
        StartCoroutine(unactive());
    }
    
    IEnumerator unactive()
    {
        yield return new WaitForSecondsRealtime(time);
        SetActiveFalse();
    }

    //use in keyframe
    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
