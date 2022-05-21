using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    bool sceneLoadComplete;

    public bool skipBegining = false;

    public SceneFader sceneFaderPrefab;
    public bool useFader;
    public SceneFader currentFader;
    public float fadeInDuration;
    public float fadeOutDuration;

    public Scene CurrentScene { get { return SceneManager.GetActiveScene(); } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator LoadScene(string sceneName)
    {

        currentFader = Instantiate(sceneFaderPrefab);


        yield return StartCoroutine(currentFader.FadeOut(fadeOutDuration));


        yield return SceneManager.LoadSceneAsync(sceneName);


        yield return StartCoroutine(currentFader.FadeIn(fadeInDuration));

        yield break;
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene("Main Menu"));
    }

    public void ReLoadScene()
    {
        if (skipBegining == false)
        {
            skipBegining = true;
        }
        GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
        UIManager.Instance.gameOverPanel.SetActive(false);

        StartCoroutine(LoadScene(CurrentScene.name));
    }

    public void LoadEnding(GameManager.GameEnding gameEnding)
    {
        GameManager.Instance.gameEnding = gameEnding;
        StartCoroutine(LoadScene("Ending"));
    }

    #region ArnoClare
    private AsyncOperation _preloadAsync;
    public bool finishPreload => _preloadAsync.isDone;
    IEnumerator PreloadScene(string sceneName)
    {
        _preloadAsync = SceneManager.LoadSceneAsync(sceneName);
        _preloadAsync.allowSceneActivation = false;
        while (!_preloadAsync.isDone)
        {
            yield return null;
        }
    }
    public void PreloadLevel(string sceneName)
    {
        StartCoroutine(PreloadScene(sceneName));
    }
    public void LoadIn()
    {
        _preloadAsync.allowSceneActivation = true;
    }
    #endregion
}
