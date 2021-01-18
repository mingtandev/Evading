using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 Offset;

    Transform transShadow;
    SpriteRenderer spriteShadow;
    public Material shadowMaterial;
    void Start()
    {
        transShadow = new GameObject().transform;
        transShadow.localScale = transform.localScale;
        transShadow.SetParent(transform);
        transShadow.name = "Shadow";
        transShadow.gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer sprite = transShadow.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer Renderer = transform.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = Renderer.sortingOrder - 1;
        sprite.sprite = transform.GetComponent<SpriteRenderer>().sprite;
        sprite.material = shadowMaterial;
        sprite.sortingLayerName = Renderer.sortingLayerName;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transShadow.transform.position = new Vector2(transform.position.x + Offset.x , transform.position.y  + Offset.y);
    }
}
