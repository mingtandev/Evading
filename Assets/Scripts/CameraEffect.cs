using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraEffect instance;
    Camera cam;
    public bool isShake;
    public bool isZoom;
    float zoomSpeed = 1f;
    //
    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }
    public IEnumerator CamShake(float duration, float power)
    {
        Vector3 originPos = transform.position;

        float temp = 0f;
        while (temp < duration)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;

            transform.position += new Vector3(x, y, originPos.z);
            temp += Time.deltaTime;

            yield return null;
        }

        transform.position = originPos;
    }

    void CamZoom(float src, float des, float time)
    {
        if (isZoom)
        {
            cam.orthographicSize += time * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, src, des);
        }
        else
        {
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, src, des);
        }
    }

    private void Update()
    {
        if (isShake || Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(CamShake(0.05f, 0.1f));
            isShake = false;
        }

        CamZoom(5f, 5.2f, Time.deltaTime);
        

    }

}
