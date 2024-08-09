using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{



    public float fadeDuration = 1.0f;
    public RawImage fadePanel;
    public GameObject fadeOutCanvas;

    public BossTrigger bossTrigger;

    private void Start()
    {
        bossTrigger.OnBossKilled += StartFade;
    }

    private void StartFade()
    {
        StartCoroutine(FadeOutStart());
        fadeOutCanvas.SetActive(true);

    }

    private IEnumerator FadeOutStart()
    {
        float elapsedTime = 0.0f;
        Color panelColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeDuration);
            panelColor.a = alpha;
            fadePanel.color = panelColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ScenesManager.Instance.StartLoadScene(5);

    }
}
