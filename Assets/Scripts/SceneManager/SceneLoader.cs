using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //单列模式，并且不销毁
    public static SceneLoader Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);//加载新的场景时，不要销毁此场景

    }

    //切换场景时过渡的函数
    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionStartCoroutine(sceneName));
    }
    
    //切换场景协程
    public IEnumerator TransitionStartCoroutine(string newSceneName)
    {
        //保存所有持久化的数据
        //淡出当前场景
        yield return StartCoroutine(ScreenFader.Instance.FaderScrennOut());
        //异步加载当前场景
        yield return SceneManager.LoadSceneAsync(newSceneName);
        //加载所有持久化数据
        //获得目标场景过渡的位置
        SceneEntrance entrance = FindObjectOfType<SceneEntrance>();

        //设置进入游戏对象的位置
        SetEnteringPosition(entrance); 

        //淡入新场景
        yield return StartCoroutine(ScreenFader.Instance.FaderScreenIn());
    }


    private void SetEnteringPosition(SceneEntrance entrance)
    {
        if (entrance == null)
            return;
        Transform entanceTransform = entrance.transform;
        
    }


    //public void LoadNextScene()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    int nextSceneIndex = currentSceneIndex + 1;

    //    if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    //    {
    //        SceneManager.LoadScene(nextSceneIndex);
    //    }
    //    else
    //    {
    //        Debug.Log("当前已是最后一关");
    //        //LoadMainScene();
    //    }
    //    Time.timeScale = 1.0f;
    //}



    //切换场景函数
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1.0f;
    }
}
    
  
 
