using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    // public SceneFader sceneFaderPrefab;
    // bool fadeInGameOver;  // 保证收到EndNotify只执行一次SceneFade
    bool sceneLoadComplete;

    public bool skipBegining = false;

    public Scene CurrentScene { get { return SceneManager.GetActiveScene(); } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // fadeInGameOver = true;
    }

    IEnumerator LoadScene(string sceneName)
    {
        // 开始加载画面渐出
        // SceneFader fade = Instantiate(sceneFaderPrefab);
        // yield return StartCoroutine(fade.FadeOut(1.7f));

        yield return SceneManager.LoadSceneAsync(sceneName);

        // 加载完毕画面渐进
        // yield return StartCoroutine(fade.FadeIn(2f));

        yield break;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene("Main Menu"));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadScene("Episode One"));
    }

    public void ReLoadScene()
    {
        if (skipBegining == false)
        {
            skipBegining = true;
            GameManager.Instance.gameMode = GameManager.GameMode.Normal;
            UIManager.Instance.gameOverPanel.SetActive(false);
        }

        StartCoroutine(LoadScene(CurrentScene.name));
    }

    public void LoadEndingOne()
    {
        StartCoroutine(LoadScene("Ending-1"));
    }
}
