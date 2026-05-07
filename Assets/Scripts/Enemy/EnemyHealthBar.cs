using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image HpImg;
    public Image hpEffectImg;

    //[SerializeField] private EnemyController enemyController;
    private EnemyController enemyController;
    public float buffTime = 0.2f;


    private Coroutine updateCoroutine;

    private float maxHp;
    private float currentHp;
    // Start is called before the first frame update
    private void Start()
    {
        buffTime = 0.2f;
        // 获取父物体上的EnemyController

        enemyController = GetComponentInParent<EnemyController>();

        if (enemyController == null)
        {
            enemyController = GetComponentInParent<EnemyController>();
        }

        // 初始化血条
        if (enemyController != null)
        {
            UpdateHealthBar();
        }
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (enemyController == null) return;

        currentHp = enemyController.health;
        maxHp = enemyController.maxhealth;
        // 确保有最大值
        if (maxHp <= 0) maxHp = 1;

        // 立即更新当前血条
        float targetFillAmount = currentHp / maxHp;
        HpImg.fillAmount = targetFillAmount;
        //HpImg.fillAmount =currentHp / maxHp;

        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
        //updateCoroutine = StartCoroutine(UpdateHpEffect());
        updateCoroutine = StartCoroutine(UpdateHpEffect(targetFillAmount));



    }
    //private IEnumerator UpdateHpEffect()
    //{
    //    float effectLength = hpEffectImg.fillAmount - HpImg.fillAmount;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < buffTime && effectLength != 0)
    //    {
    //        elapsedTime += Time.deltaTime;  
    //        hpEffectImg.fillAmount = Mathf.Lerp(
    //           hpEffectImg.fillAmount + effectLength,
    //            HpImg.fillAmount,
    //            elapsedTime / buffTime);
    //        yield return null;
    //    }
    //    hpEffectImg.fillAmount = HpImg.fillAmount;
    //}

    private IEnumerator UpdateHpEffect(float targetAmount)
    {
        // 记录特效血条当前值
        float startAmount = hpEffectImg.fillAmount;
        float elapsedTime = 0f;

        // 如果已经相等，不需要缓冲
        if (Mathf.Approximately(startAmount, targetAmount))
        {
            hpEffectImg.fillAmount = targetAmount;
            yield break;
        }

        // 缓冲动画
        while (elapsedTime < buffTime)
        {
            elapsedTime += Time.deltaTime;

            // 使用插值让特效血条平滑过渡到目标值
            float t = elapsedTime / buffTime;

            // 可以选择不同的缓动函数
            // 这里使用平滑的插值
            t = Mathf.SmoothStep(0f, 1f, t);

            hpEffectImg.fillAmount = Mathf.Lerp(startAmount, targetAmount, t);

            yield return null;
        }

        // 确保最终值准确
        hpEffectImg.fillAmount = targetAmount;
    }

}
