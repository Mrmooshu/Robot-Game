using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator[] animators;

    public void SelectScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        StartCoroutine(LoadScene(sceneName, mode));
    }

    public void SelectScene(string sceneName, int transitionIndex = 0, float transitionTime = 1, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        StartCoroutine(LoadScene(sceneName, transitionIndex, transitionTime, mode));
    }

    public void StartGame()
    {
        SelectScene("Core", LoadSceneMode.Single);
        SelectScene("World", LoadSceneMode.Additive);
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
        yield return null;
    }

    IEnumerator LoadScene(string sceneName, int transitionIndex, float transitionTime, LoadSceneMode mode)
    {
        animators[(transitionIndex)].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName, mode);
    }
}
