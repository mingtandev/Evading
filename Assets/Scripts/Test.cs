using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TestExe();
        }
    }

    void TestExe()
    {

        // transform.DORotate(new Vector3(0,0,270) , 2f , RotateMode.FastBeyond360).SetLoops(-1 , LoopType.YoYo);   
        //transform.DOMove(new Vector3(10,0,0) , 2f).SetLoops(-1 , LoopType.Incremental);  // Incremental : hieu ung di chuyen tang dan
        // transform.DORotate(new Vector3(0,0,360) , 2f , RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.InElastic);  //Ease giong nhu curve trong animation , linear de khong bi delay

        // transform.DOKill();  //true => chi kill nhung ham da hoan thanh
        // transform.DORotate(new Vector3(0, 0, Random.Range(0, 360)), 2f, RotateMode.FastBeyond360).OnComplete(() =>
        // {
        //     Debug.Log("TEST");
        // });  //Bi loi van de da luong



        //Noi cac animatiobn
        //LUU Y : Khi tao mot sequence , no se chay o frame tiep theo ( cac xu ly la cache lai khi khoi tao)
        Sequence sequence = DOTween.Sequence();

        //Append : noi cac animation
        //Join thuc hien cac animation co trong list cua Append

        sequence.Join(transform.DORotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360));
        sequence.Join(transform.DOMoveY(10, 2f));
        sequence.Append(transform.DOMoveX(10, 2f));
        sequence.Join(transform.DOMoveX(20, 2f));
        
        //Reseach : Reuse sequence : khong kill , cached.SetAutoKill(false); , sau do restart 

    }
}
