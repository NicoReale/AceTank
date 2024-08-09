using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene
{
    [SerializeField] Slider LoadingBarFill;

    public Action UIUpdate;

    public LoadScene(Slider LoadingBar)
    {
        LoadingBarFill = LoadingBar;
    }
    public IEnumerator LoadSceneAsync(int sceneId)
    {
        float timer = 2f;
        UIUpdate?.Invoke();
        yield return new WaitForSeconds(timer);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);


        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            LoadingBarFill.value = progress;
            yield return null;
        }      
    }

}
