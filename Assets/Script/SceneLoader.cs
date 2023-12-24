using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string targetSceneName;

    public void LoadTargetScene()
    {
        StartCoroutine(LoadSceneAsyncCoroutine());
    }

    IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 is the completion threshold
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            yield return null;
        }

        Debug.Log("Scene loaded");
    }
}
