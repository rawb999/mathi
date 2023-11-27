using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroller : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float _X;

    // Update is called once per delta Time
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(_X, 0) * Time.deltaTime,img.uvRect.size);
    }
}
