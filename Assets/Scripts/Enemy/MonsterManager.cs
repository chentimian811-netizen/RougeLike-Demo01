//using UnityEngine;
//using System.Collections.Generic;

//public class MonsterManager : MonoBehaviour
//{
//    // 单例：全局唯一的怪物管理器
//    public static MonsterManager Instance;

//    [Header("怪物统计")]
//    private int totalMonsters; // 场景内总怪物数
//    public int remainingMonsters { get; private set; } // 剩余存活怪物数

//    [Header("物品解锁配置")]
//    [Tooltip("拖入场景中开局生成的所有物品（ItemPickup组件）")]
//    public List<ItemPickup> startItems;

//    void Awake()
//    {
//        // 单例逻辑
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    void Start()
//    {
//        // 统计场景中所有带“Monster”标签的怪物
//        GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Enemy");
//        totalMonsters = allMonsters.Length;
//        remainingMonsters = totalMonsters;

//        // 调试提示
//        Debug.Log($"最后一关总怪物数：{totalMonsters}");

//        // 校验物品列表是否为空
//        if (startItems == null || startItems.Count == 0)
//        {
//            Debug.LogWarning("【MonsterManager】请在Inspector中添加开局生成的物品！");
//        }
//    }

//    // 怪物被击杀时调用（由MonsterHealth触发）
//    public void OnMonsterKilled()
//    {
//        remainingMonsters--;
//        Debug.Log($"剩余怪物：{remainingMonsters}");

//        // 核心判断：最后一关 + 所有怪物已消灭
//        if (GameManager.Instance.IsLastLevel && remainingMonsters == 0)
//        {
//            UnlockAllStartItems();
//        }
//    }

//    // 解锁所有开局物品
//    private void UnlockAllStartItems()
//    {
//        foreach (var item in startItems)
//        {
//            if (item != null)
//            {
//                item.UnlockPickup();
//            }
//        }
//        Debug.Log("✅ 所有敌人已消灭，开局物品已解锁拾取！");
//    }
//}