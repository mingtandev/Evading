using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject representObject;

    [Header("Options")]
    public LayerMask layer;
    public float distanceDetect;
    public bool followRotation;

    PlayerController player;
    Renderer rend;



    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rend = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        representObject.transform.eulerAngles = new Vector3(0, 0, 0);
        representObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        MakeIndicator();
        if (Vector2.Distance(representObject.transform.position, transform.position) > 20)
        {
            representObject.SetActive(false);
        }
    }


    void MakeIndicator()
    {
        CheckFollow();

        if (!rend.isVisible)
        {
            representObject.transform.eulerAngles = new Vector3(0, 0, 0);

            if (representObject.activeSelf == false)
            {
                representObject.SetActive(true);
            }

            Vector2 dir = player.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, distanceDetect, layer);
            if (ray.collider != null)
            {
                representObject.transform.position = ray.point;
            }
        }
        else
        {
            if (representObject.activeSelf)
            {
                IndicatorReset();
                representObject.SetActive(false);
            }
        }


    }


    void CheckFollow()
    {
        if (followRotation)
        {
            Vector3 dir = transform.position - representObject.transform.position;


            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion newQuat = Quaternion.Euler(0, 0, angle - 90 - 180);
            representObject.transform.rotation = Quaternion.Slerp(representObject.transform.rotation, newQuat, 5f * Time.fixedDeltaTime);
        }
        else
        {
            Quaternion newQuat = Quaternion.Euler(0, 0, 0);
        }
    }

    public void IndicatorReset()
    {
        representObject.transform.SetParent(gameObject.transform);
        representObject.transform.position = new Vector3(0, 0, 0);
        representObject.SetActive(false);
    }
}

