//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    // 单例：全局唯一的关卡管理器
//    public static GameManager Instance;

//    [Header("关卡配置")]
//    [Tooltip("当前关卡编号（比如最后一关填5）")]
//    public int currentLevel = 5;
//    [Tooltip("最后一关的编号（比如你的游戏最后一关是5）")]
//    public int lastLevel = 5;

//    // 只读属性：判断是否是最后一关
//    public bool IsLastLevel => currentLevel == lastLevel;

//    void Awake()
//    {
//        // 单例逻辑：确保只有一个GameManager
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject); // 可选：跨场景保留
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//}