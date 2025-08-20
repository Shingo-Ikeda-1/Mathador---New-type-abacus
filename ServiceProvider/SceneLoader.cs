using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private MonoBehaviour _coroutineHost; // Whatever MonoBehaviour in current scene that can run coroutines

    public void LoadScene(string sceneName)
    {
        _coroutineHost = ServiceScopeProvider.Instance;
        if (_coroutineHost != null)
        {
            _coroutineHost.StartCoroutine(LoadSceneCoroutine(sceneName));
        }
        else
        {
            Debug.LogError("SceneLoader not initialized. Call Initialize() with a MonoBehaviour instance.");
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // You can add progress updates here if needed
            yield return null;
        }
    }

    public void UnloadScene(string sceneName)
    {
        _coroutineHost = ServiceScopeProvider.Instance;
        if (_coroutineHost != null)
        {
            _coroutineHost.StartCoroutine(UnloadSceneCoroutine(sceneName));
        }
        else
        {
            Debug.LogError("SceneLoader not initialized. Call Initialize() with a MonoBehaviour instance.");
        }
    }

    private IEnumerator UnloadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncUnload.isDone)
        {
            // You can add progress updates here if needed
            Debug.Log($"Unloading progress: {asyncUnload.progress * 100}%");
            yield return null;
        }
        Debug.Log($"Scene '{sceneName}' unloaded successfully!");
    }
}