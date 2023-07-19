using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private UnityEngine.Material[] materials;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        materials = new UnityEngine.Material[renderers.Length];
        for (int i = 0; i< renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }
    }

    private IEnumerator Flash(Color color, float duration)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_FlashColor", color);
        }
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime/duration));

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_FlashAmount", currentFlashAmount);
            }

            yield return null;
        }
    }

    public void FlashStart(Color color, float duration)
    {
        currentCoroutine = StartCoroutine(Flash(color, duration));
    }
}