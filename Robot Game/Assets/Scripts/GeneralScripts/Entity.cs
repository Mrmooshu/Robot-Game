using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int facingDirection { get; protected set; }

    public virtual void Start()
    {
        facingDirection = (int)transform.localScale.x;
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public void Flip()
    {
        facingDirection *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetDirection(int direction)
    {
        Vector3 theScale = transform.localScale;
        if (direction >= 0)
        {
            facingDirection = 1;
            theScale.x = 1;
        }
        else
        {
            facingDirection = -1;
            theScale.x = -1;
        }
        transform.localScale = theScale;
    }
}
