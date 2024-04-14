using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    float a = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());

        IEnumerator timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                a *= -1;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Rotate(.5f,.5f,.5f);
        transform.Rotate(0, 0,a);
    }
}
