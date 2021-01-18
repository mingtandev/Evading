using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject coin;

    const string _SpawnEnemy = "SpawEnemy";
    const string _SpawnCoin = "SpawnCoin";
    const string _SpawnFastEnemy = "SpawFastEnemy";
    const string _SpawnBunchOfEnemy = "SpawBunchOfCar";



    GameObject player;
    Camera cam;
    float heighCam;
    float widthCam;

    [Header("Time Spawn")]
    [SerializeField] float timeSpawnEnemy;
    [SerializeField] float timeSpawnCoin;
    [SerializeField] float timeSpawnFastEnemy;
    [SerializeField] float timeSpawnBunchOfEnemy;




    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        heighCam = 2f * cam.orthographicSize;
        widthCam = heighCam * cam.aspect;

    }
    void Start()
    {
        StartCoroutine(_SpawnEnemy);
        StartCoroutine(_SpawnCoin);
        StartCoroutine(_SpawnBunchOfEnemy);
        StartCoroutine(_SpawnFastEnemy);
    }



    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawEnemy()
    {
        //Random in 4 area
        //Instantiate(enemy, posSpawn, Quaternion.identity);
        yield return new WaitForSeconds(timeSpawnEnemy);
        Vector2 posSpawn = getPosSpawn();
        ObjectPool.Instance.Spawn("Enemy", posSpawn);
        StartCoroutine(_SpawnEnemy);
    }

    IEnumerator SpawnCoin()
    {
        //Random in 4 area
        Vector2 posSpawn = getPosSpawn();
        Instantiate(coin, posSpawn, Quaternion.identity);
        yield return new WaitForSeconds(timeSpawnCoin);
        StartCoroutine(_SpawnCoin);
    }

    IEnumerator SpawFastEnemy()
    {
        //Random in 4 area
        //Instantiate(enemy, posSpawn, Quaternion.identity);
        yield return new WaitForSeconds(timeSpawnFastEnemy);
        Vector2 posSpawn = getPosSpawn();
        ObjectPool.Instance.Spawn("FastEnemy", posSpawn);
        StartCoroutine(_SpawnFastEnemy);
    }

    IEnumerator SpawBunchOfCar()
    {
        //Random in 4 area
        //Instantiate(enemy, posSpawn, Quaternion.identity);
        yield return new WaitForSeconds(timeSpawnBunchOfEnemy);
        Vector2 posSpawn = posSpawn2Dimension();

        ObjectPool.Instance.Spawn("BunchOfEnemy", posSpawn);


        StartCoroutine(_SpawnBunchOfEnemy);
    }

    Vector2 getPosSpawn()
    {
        int area = Random.Range(0, 4);
        Vector2 posSpawn = Vector2.zero;
        switch (area)
        {
            //top
            case 0:
                posSpawn = new Vector2(Random.Range(cam.MiddleLeftPoint().x - 10, cam.MiddleLeftPoint().x + 10), cam.TopMiddlePoint().y + 10f);
                break;
            //right
            case 1:
                posSpawn = new Vector2(cam.MiddleRightPoint().x + 10, Random.Range(-cam.BottomMiddlePoint().y - 10, cam.TopMiddlePoint().y + 10));
                break;
            //bottom
            case 2:
                posSpawn = new Vector2(Random.Range(cam.MiddleLeftPoint().x - 10, cam.MiddleLeftPoint().x + 10), cam.BottomMiddlePoint().y - 10);
                break;
            //left
            case 3:
                posSpawn = new Vector2(cam.MiddleLeftPoint().x - 10f, Random.Range(-cam.BottomMiddlePoint().y - 10, cam.TopMiddlePoint().y + 10));
                break;
            //
            default:
                break;
        }
        return posSpawn;
    }

    Vector2 posSpawn2Dimension()
    {
        int area = Random.Range(0, 2);
        if (area == 0)
        {
            return cam.TopLeftPoint() + new Vector2(-5, 5);
        }
        if (area == 1)
        {
            return cam.TopRightPoint() + new Vector2(5, 5);
        }
        return new Vector2(0, 0);
    }


}