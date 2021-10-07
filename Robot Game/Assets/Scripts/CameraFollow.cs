using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private float height;

    void FixedUpdate()
    {
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y + height, this.transform.position.z);
    }
}
