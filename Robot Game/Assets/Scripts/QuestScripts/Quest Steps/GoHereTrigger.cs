using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHereTrigger : MonoBehaviour
{
    public GoHereStep step;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerEntity>() != null)
            {
                step.Evaluate();
                Destroy(gameObject);
            }
        }
    }
}
