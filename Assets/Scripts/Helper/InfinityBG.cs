using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBG : MonoBehaviour
{
    //reference
    Camera mainCamera;
    PlayerController player;


    public GameObject[] bgTitle;
    public List<Transform> QTrans;
    public SpriteRenderer[] listSprite;


    int curTitle;
    int newTitle;
    bool isRegen;
    private void Awake()
    {

        newTitle = curTitle;
        Initial();
        //BOUNDS
    }

    // Update is called once per frame
    void Update()
    {
        // test.position = new Vector2(test.position.x  , test.position.y - test.GetComponent<SpriteRenderer>().bounds.size.y/2);

        for (int i = 0; i < bgTitle.Length; i++)
        {
            //Check outside
            if (listSprite[i].bounds.CointainPoint(player.transform.position))
            {
                if (QTrans.Contains(bgTitle[i].transform)) QTrans.Remove(bgTitle[i].transform);


                newTitle = i;

                if (newTitle != curTitle)
                {
                    if (!QTrans.Contains(bgTitle[curTitle].transform)) QTrans.Add(bgTitle[curTitle].transform);

                    isRegen = true;
                    curTitle = newTitle;
                    Regen();
                }
            }
            else
            {
                if (!QTrans.Contains(bgTitle[i].transform))
                {
                    QTrans.Add(bgTitle[i].transform);
                }
            }
        }

        if (isRegen)
        {
            isRegen = false;
        }
    }


    void Initial()
    {
        mainCamera = Camera.main;

        bgTitle = GameObject.FindGameObjectsWithTag("Background");
        player = GameObject.FindObjectOfType<PlayerController>();
        QTrans = new List<Transform>();
        listSprite = new SpriteRenderer[bgTitle.Length];
        for (int i = 0; i < listSprite.Length; i++)
        {
            listSprite[i] = bgTitle[i].GetComponent<SpriteRenderer>();
            QTrans.Add(bgTitle[i].transform);
        }
    }


    public void Regen()
    {

        //Lay toa do tai goc , tru di phan thua(scale)
        QTrans[0].transform.position = new Vector2(listSprite[curTitle].bounds.TopLeftPoint().x - listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.TopLeftPoint().y + listSprite[curTitle].bounds.extents.y);
        QTrans[1].transform.position = new Vector2(listSprite[curTitle].bounds.TopMiddlePoint().x, listSprite[curTitle].bounds.TopMiddlePoint().y + listSprite[curTitle].bounds.extents.y);
        QTrans[2].transform.position = new Vector2(listSprite[curTitle].bounds.TopRightPoint().x + listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.TopRightPoint().y + listSprite[curTitle].bounds.extents.y);
        QTrans[3].transform.position = new Vector2(listSprite[curTitle].bounds.MiddleLeftPoint().x - listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.MiddleLeftPoint().y);
        QTrans[4].transform.position = new Vector2(listSprite[curTitle].bounds.MiddleRightPoint().x + listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.MiddleRightPoint().y);
        QTrans[5].transform.position = new Vector2(listSprite[curTitle].bounds.BottomLeftPoint().x - listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.BottomLeftPoint().y - listSprite[curTitle].bounds.extents.y);
        QTrans[6].transform.position = new Vector2(listSprite[curTitle].bounds.BottomMiddlePoint().x, listSprite[curTitle].bounds.BottomMiddlePoint().y - listSprite[curTitle].bounds.extents.y);
        QTrans[7].transform.position = new Vector2(listSprite[curTitle].bounds.BottomRightPoint().x + listSprite[curTitle].bounds.extents.x, listSprite[curTitle].bounds.BottomRightPoint().y - listSprite[curTitle].bounds.extents.y);
    }

    public void ResetPos()
    {
        QTrans[0].transform.position = new Vector2(0f,0f);
        QTrans[1].transform.position = new Vector2(20f,0f);
        QTrans[2].transform.position = new Vector2(39.5f,0f);
        QTrans[3].transform.position = new Vector2(39.5f,-20f);
        QTrans[4].transform.position = new Vector2(19.5f,-20f);
        QTrans[5].transform.position = new Vector2(0.1f,-20f);
        QTrans[6].transform.position = new Vector2(0.1f,-40f);
        QTrans[7].transform.position = new Vector2(20.1f,-40f);
        QTrans[7].transform.position = new Vector2(39.5f,-40f);

    }

}
