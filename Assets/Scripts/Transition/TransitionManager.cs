using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public string startSceneName = string.Empty;
    private CanvasGroup fadeCanvasGroup;
    private bool isFade;
    //Transition
    public const float fadeDuration = 1.5f;

    private void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        //LoadSaveDataScene("Home");
        //EventHandler.CallAfterSceneLoadedEvent();
        //GameObject.FindWithTag("BoundsConfiner").GetComponent<SwitchBounds>().SwitchConfinerShape(none);
    }


    private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
    {
        if (!isFade)
            StartCoroutine(Transition(sceneToGo, positionToGo));
    }

    /// <summary>
    /// 场景切换
    /// </summary>
    /// <param name="sceneName">目标场景</param>
    /// <param name="targetPosition">目标位置</param>
    /// <returns></returns>
    private IEnumerator Transition(string sceneName, Vector3 targetPosition)
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        yield return Fade(1);

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        yield return LoadSceneSetActive(sceneName);

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// 加载场景并设置为激活
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <returns></returns>
    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newScene);
            
    }
    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        //为真时遮挡鼠标
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        // 判断两个float值无限接近
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }

    private IEnumerator UnloadScene()
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        yield return Fade(1f);
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return Fade(0);
    }

    private IEnumerator LoadSaveDataScene(string sceneName)
    {
        //yield return Fade(1f);

        if (SceneManager.GetActiveScene().name != "Base")    //在游戏过程中 加载另外游戏进度
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        yield return LoadSceneSetActive(sceneName);
        EventHandler.CallAfterSceneLoadedEvent();
        //yield return Fade(0);
    }
}
