using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void ObjectMovement(Vector2 direct , float speed)
    {
        rb.velocity = direct.normalized * speed;
    }   
}
