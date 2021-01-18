using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour, ICollidedWithEnemy
{
    //Reference
    Transform player;
    Movement movement;
    Indicator indicator;
    CarEffect carEffect;
    bool isDrift;
    public bool isDamage;  //Detect conlision only once

    [SerializeField] float speed = 5f;
    float originSpeed;
    [SerializeField] float speedRotation = 2f;
    [SerializeField] bool isStatic;



    private void Awake()
    {
        originSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        movement = GetComponent<Movement>();
        indicator = GetComponent<Indicator>();
        carEffect = GetComponent<CarEffect>();
    }

    private void OnEnable()
    {
        isDamage = false;
    }

    private void FixedUpdate()
    {
        if(!isStatic)
            MoveToPlayer();
    }

    void MoveToPlayer()
    {
        carEffect.isDust = true;
        carEffect.isDrift = isDrift;

        movement.ObjectMovement(transform.up, speed);
        Vector3 dir = player.position - transform.position;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion newQuat = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, newQuat, speedRotation * Time.fixedDeltaTime);




        Vector2 directionToPlayer = player.transform.position - transform.position;
        float rotate = Vector2.Dot(directionToPlayer.normalized, transform.up);
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            if (rotate < Mathf.Cos(Mathf.PI / 6))
            {
                if (speed > originSpeed / 2)
                    speed -= originSpeed * Time.fixedDeltaTime;
                isDrift = true;
            }
            else
            {
                if (speed < originSpeed)
                    speed += originSpeed * Time.fixedDeltaTime;
                isDrift = false;
            }
        }
        else
        {
            if (speed < originSpeed)
                speed += originSpeed * Time.fixedDeltaTime;
            isDrift = false;
        }



    }




    private void OnCollisionEnter2D(Collision2D other)
    {

        var collisionWithEnemy = other.transform.GetComponent<ICollidedWithEnemy>();
        var collisionWithPlayer = other.transform.GetComponent<ICollidedWithPlayer>();

        if (collisionWithEnemy != null)
        {
            if (!isDamage && !other.transform.GetComponent<EnemyMove>().isDamage)
            {
                isDamage = true;
                ObjectPool.Instance.Spawn("PopupPoint", transform.position);
            }
            GameManager.Instance.IncreasePoint(25);
            SoundManager.Instance.PlayOneShot("police_crash");
            CameraEffect.instance.isShake = true;
            collisionWithEnemy.Collided();
            Collided();

        }

        if (collisionWithPlayer != null)
        {
            SoundManager.Instance.PlayOneShot("police_crash");
            collisionWithPlayer.Collided();
        }
    }

    public void Collided()
    {
        GameObject explosion = ObjectPool.Instance.Spawn("Explosion", transform.position);
        indicator.IndicatorReset();
        gameObject.SetActive(false);
    }





}
