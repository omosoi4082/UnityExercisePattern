
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ScreenFader _screenFader;
    private Scene _bootStrapScene;
    private Scene _lastLoadedScene;

    private List<Scene> _additiveScenes = new List<Scene>();

    public Scene bootStrapScene => _bootStrapScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bootStrapScene = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
        SceneEvents.SceneLoadedByPath += SceneEvents_LoadSceneByPath;
        SceneEvents.SceneUnloadedByPath += SceneEvents_UnloadSceneByPath;
        SceneEvents.SceneLoadedByIndex += SceneEvents_SceneIndexLoadeed;
        SceneEvents.LastSceneUnloaded += SceneEvents_LastSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneEvents.SceneLoadedByPath -= SceneEvents_LoadSceneByPath;
        SceneEvents.SceneUnloadedByPath -= SceneEvents_UnloadSceneByPath;
        SceneEvents.SceneLoadedByIndex -= SceneEvents_SceneIndexLoadeed;
        SceneEvents.LastSceneUnloaded -= SceneEvents_LastSceneUnloaded;
    }
    //이벤트 handling methods
    private void SceneEvents_LastSceneUnloaded()
    {
        UnloadLastLoadedScene();
    }
    private void SceneEvents_SceneIndexLoadeed(int sceneindex)
    {
        LoadSceneAdditively(sceneindex);
    }
    private void SceneEvents_LoadSceneByPath(string scenePath)
    {
        LoadSceneByPath(scenePath);
    }
    private void SceneEvents_UnloadSceneByPath(string scenePath)
    {
        UnloadSceneByPath(scenePath);
    }

    // Methods
    public void LoadSceneByPath(string scenePath)//Path로 로드
    {
        StartCoroutine(LoadScene(scenePath));
    }
    public void LoadSceneAdditively(int sceneindex)//index->Path로 로드
    {
        StartCoroutine(LoadAddtiveScene(sceneindex));
    }
    public void UnloadLastLoadedScene()
    {
        StartCoroutine(UnloadeLastScene());
    }
    public void UnloadSceneByPath(string scenePath)
    {
        Scene sceneToUnloade = SceneManager.GetSceneByPath(scenePath);
        if (sceneToUnloade.IsValid())
        {
            StartCoroutine(UnloadScene(sceneToUnloade));
        }
    }
    private IEnumerator LoadAddtiveScene(int sceneindex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneindex);
        yield return LoadAdditiveScene(scenePath);
    }


    //씬로드
    public IEnumerator LoadScene(string scenePath)
    {
        if (string.IsNullOrEmpty(scenePath))
        {
            yield break;
        }
        if (_screenFader != null)
        {
            _screenFader.FadeOut();
            yield return new WaitForSeconds(_screenFader.fadeDuration);
        }
        yield return UnloadeLastScene();
        yield return LoadAdditiveScene(scenePath);

        if (_screenFader != null)
        {
            _screenFader.FadeIn();
        }
    }
    private IEnumerator LoadAdditiveScene(string scenePath)
    {
        if (string.IsNullOrEmpty(scenePath))
        {
            yield break;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress;
            yield return null;
        }
        _lastLoadedScene = SceneManager.GetSceneByPath(scenePath);
        SceneManager.SetActiveScene(_lastLoadedScene);
    }
    public void LoadSceneAdditivelyByPath(string scenePath)
    {
        Scene sceneToLoad = SceneManager.GetSceneByPath(scenePath);
        if (!sceneToLoad.IsValid())
        {
            if (!_additiveScenes.Contains(sceneToLoad))
            {
                _additiveScenes.Add(sceneToLoad);
            }
            StartCoroutine(LoadAdditiveScene(scenePath));
        }
        else
        {
            Debug.LogWarning($"[SceneLoader]: Scene at path {scenePath} is already loaded.");
        }
    }

    // 씬 언로드 
    public IEnumerator UnloadeLastScene()
    {
        if (!_lastLoadedScene.IsValid())
            yield break;
        UnloadAllAdditiveScenes();
        if (_lastLoadedScene != _bootStrapScene)
            yield return UnloadScene(_lastLoadedScene);

    }

    public void UnloadAllAdditiveScenes()
    {
        foreach (Scene scene in _additiveScenes)
        {
            if (scene.IsValid() && scene != _bootStrapScene)
            {
                StartCoroutine(UnloadScene(scene));
            }
        }
        _additiveScenes.Clear();
    }

    private IEnumerator UnloadScene(Scene scene)
    {
        if (SceneManager.sceneCount <= 1)
        {
            Debug.Log("[SceneLoader: Cannot unload only loaded scene " + scene.name);
            yield break;
        }
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(scene);
        while (!asyncOperation.isDone) { yield return null; }
    }
   
    public void UnloadSceneImmediately(string scenePath)
    {
        Scene sceneToUnload=SceneManager.GetSceneByPath(scenePath); 
        if(sceneToUnload.IsValid())
        {
            StartCoroutine(UnloadSceneWrapper(sceneToUnload));
        }
        else
        {
            Debug.LogWarning($"Scene at path {scenePath} is not valid or already unloaded.");
        }
    }

    private IEnumerator UnloadSceneWrapper(Scene scene)
    {
        yield return StartCoroutine(UnloadScene(scene));
        _additiveScenes.Remove(scene);
    }   


    ///////////////
    //additively없이 단독 로드 
    public void LoadSceneNonAdditive(int sceneindex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneindex);
        if (string.IsNullOrEmpty(scenePath))
        {
            return;
        }
        SceneManager.LoadScene(scenePath);
    }
    
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadNextScene()
    {
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneindex + 1);//위험하지 않나?
    }
}

