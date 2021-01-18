using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchOfEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        Vector3 dir = GameManager.Instance.playerController.transform.position - transform.position;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up.normalized * 5 * Time.fixedDeltaTime;
    }


}
