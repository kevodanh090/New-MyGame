using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _imgBackGround;
    [SerializeField] private float _x;

    // Update is called once per frame
    void Update()
    {
        _imgBackGround.uvRect = new Rect (_imgBackGround.uvRect.position + new Vector2(_x, 0) * Time.deltaTime, _imgBackGround.uvRect.size);
    }
}
