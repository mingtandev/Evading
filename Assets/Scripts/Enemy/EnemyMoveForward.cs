using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    Movement movement;
    public float speed;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    Quaternion quad;

    private void OnEnable()
    {
        Debug.Log("ONEABLE");
        Vector3 dir = GameManager.Instance.playerController.transform.position - transform.position;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        quad = Quaternion.Euler(0, 0, angle - 90);

        transform.rotation = quad;

    }


    // Update is called once per frame
    void Update()
    {
        movement.ObjectMovement(transform.up, speed);

    }


}
