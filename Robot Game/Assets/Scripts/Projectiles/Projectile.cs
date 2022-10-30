using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    Entity origin;
    public Vector2 direction;
    public float speed;

    public void Initialize(Vector2 initalDirecton, float speed)
    {
        direction = initalDirecton;
        this.speed = speed;
    }

    public override void Update()
    {
        transform.position += (Vector3)(direction * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
