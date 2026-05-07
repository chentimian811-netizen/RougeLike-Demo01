using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//场景退出
public class SceneExit : MonoBehaviour
{
    [Tooltip("需要过渡新场景的名称")]
    public string NewSceneName;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionInternal();
        }
    }

   public void TransitionInternal()
    {
        SceneLoader.Instance.TransitionToScene(NewSceneName);
    }
}
