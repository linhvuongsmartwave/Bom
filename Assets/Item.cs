using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveDistance = 0.5f; // Khoảng cách di chuyển
    public float moveDuration = 0.5f; // Thời gian di chuyển lên hoặc xuống

    void Start()
    {
        // Tạo một tween di chuyển lên xuống liên tục
        transform.DOMoveY(transform.position.y + moveDistance, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
