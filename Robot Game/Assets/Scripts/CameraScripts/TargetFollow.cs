using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] public Transform followTransform;
    [SerializeField] public float height = 0;

    void FixedUpdate()
    {
        if (followTransform != null)
        {
            transform.position = new Vector3(followTransform.position.x, followTransform.position.y + height, transform.position.z);
        }
    }
}
