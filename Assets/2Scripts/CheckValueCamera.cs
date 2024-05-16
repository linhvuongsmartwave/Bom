using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckValueCamera : MonoBehaviour
{
    public GameObject parentTransform;

    public void Awake() {
        Camera.main.transform.position = new Vector3(parentTransform.transform.position.x, parentTransform.transform.position.y, Camera.main.transform.position.z);
    }
}
