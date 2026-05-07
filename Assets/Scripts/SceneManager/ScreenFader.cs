using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class ScreenFader : MonoBehaviour
{
   //单列模式
   public static ScreenFader Instance { get; private set; }

    public CanvasGroup faderCanvasGroup;

    public float faderDuration = 1.0f;  

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //将当前的游戏对象设置为不销毁
        }
        else 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //将当前的游戏对象设置为不销毁
    }

    //淡入场景
    public IEnumerator FaderScreenIn()
    {
        yield return StartCoroutine(Fade(0f, faderCanvasGroup)); 
        faderCanvasGroup.gameObject.SetActive(false);
    }
    //淡出场景
    public IEnumerator FaderScrennOut()
    {
       
        faderCanvasGroup.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1f, faderCanvasGroup));
    }

    public IEnumerator Fade(float fianlAlpha, CanvasGroup canvasGroup)
    {
        //使用DG完成淡入淡出的效果
        yield return canvasGroup.DOFade(fianlAlpha, faderDuration).WaitForCompletion();
    }
}   

