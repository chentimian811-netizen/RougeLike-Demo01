using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [Header("UI组件")]
    public GameObject GameOverPanel;
    public GameObject GameaPassPanel;

    public Button btnRestart;//重新开始
    public Button btnContinue;//继续游戏
    // Start is called before the first frame update
    void Start()
    {
        btnRestart.onClick.AddListener(OnRestartButtonClick);
        btnContinue.onClick.AddListener(OnContinueButtonClick);
    }

    // Update is called once per frame
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    //void Update()
    //{
    //    //通过条件，最后一波并且敌人死完就通过
    //    if (EnemyManager.Instance.GetLastWave() && EnemyManager.Instance.enemyCount == 0)
    //    {
    //        GameaPassPanel.SetActive(true);//显示通过面板
    //    }
    //}
    public void showGameOverPanel()
    {
        GameOverPanel.SetActive(true);//显示游戏结束面板 
    }

    public void OnContinueButtonClick()
    {
       
    }
    public void OnRestartButtonClick()
    {
        //重新加载场景 
        SceneLoader.Instance.LoadMainScene();
    }

}
