using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SourceManage : MonoBehaviour
{
    private static SourceManage instance;

    public string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        if (instance != null && instance != this )
        {
            Destroy(gameObject);
            return;
        }
         
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if(scene.name  == mainMenuSceneName)
        {
            DestroySingleton();
        }
    }

    private void DestroySingleton()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(gameObject);

        instance = null;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
