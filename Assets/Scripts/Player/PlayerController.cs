using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour, ICollidedWithPlayer
{


    public Rigidbody2D rb;
    Movement movement;

    [Header("Speed Config")]
    [SerializeField]
    float speed = 5f;
    float originSpeed;
    [SerializeField]
    float rotateSpeed = 25f;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float minSpeed;
    public float angularDrag;
    public TrailRenderer[] driftTrails;

    public bool isDrift;
    bool isRight;
    Camera Cammain;
    CarEffect CarEffect;

    void Awake()
    {
        originSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        Cammain = Camera.main;
        CarEffect = GetComponent<CarEffect>();
    }

    void Start()
    {

    }

    void Update()
    {
        CheckDrift();
    }

    void FixedUpdate()
    {
        MovementControllerV1();
    }



    void MovementControllerV1()
    {
        CarEffect.isDust = true;
        CarEffect.isDrift = isDrift;
        // movement.ObjectMovement(transform.up, speed);
        //AddForce khong lien tuc vi khong ton tai ma sat truot (he so ma sat truot = M)
        // rb.AddForce(transform.up * originSpeed);  //Khong quan ly duoc vel
        rb.AddForce(transform.up * originSpeed);
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                rb.angularVelocity = rotateSpeed;
                //rb.rotation += rotateSpeed * Time.fixedDeltaTime;
            }
            else if (Input.mousePosition.x > Screen.width / 2)
            {
                rb.angularVelocity = -rotateSpeed;
                //rb.rotation -= rotateSpeed * Time.fixedDeltaTime;
            }
            CameraEffect.instance.isZoom = true;
            CarEffect.isDust = true;
            isDrift = true;
        }
        else
        {
            isDrift = false;
            CameraEffect.instance.isZoom = false;
            rb.angularVelocity = 0;

        }
    }

    //Drift

    void CheckDrift()
    {
        if (isDrift)
        {
            DriftStartEmit();
        }
        else
        {
            DriftStopEmit();

        }
    }

    void DriftStartEmit()
    {
        foreach (TrailRenderer trail in driftTrails)
        {
            trail.emitting = true;
        }
    }

    void DriftStopEmit()
    {
        foreach (TrailRenderer trail in driftTrails)
        {
            trail.emitting = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var collision = other.transform.GetComponent<ICollidedWithPlayer>();
        if (collision != null)
        {
            other.transform.GetComponent<ICollidedWithPlayer>().Collided();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var collision = other.transform.GetComponent<ICollidedWithPlayer>();
        if (collision != null)
        {
            other.transform.GetComponent<ICollidedWithPlayer>().Collided();
        }
    }

    public void Collided()
    {
        GameManager.Instance.ResultState();
    }


    public void ResetState()
    {
        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }


}
