//using UnityEngine;

//public class ItemPickup : MonoBehaviour
//{
//    [Header("物品基础配置")]
//    [Tooltip("物品名称（用于调试/UI提示）")]
//    public string itemName = "通关奖励";
//    [Tooltip("锁定时的透明度（0=完全透明，1=不透明）")]
//    public float lockAlpha = 0.5f;

//    private bool isPickupEnabled = false; // 拾取锁定状态：默认不可拾取
//    private SpriteRenderer spriteRenderer; // 物品渲染组件（视觉反馈）

//    void Start()
//    {
//        // 获取渲染组件，初始化锁定视觉效果
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        if (spriteRenderer != null)
//        {
//            SetLockVisual(true); // 开局设置为锁定视觉
//        }
//    }

//    // 解锁拾取（由MonsterManager调用）
//    public void UnlockPickup()
//    {
//        if (isPickupEnabled) return; // 避免重复解锁
//        isPickupEnabled = true;
//        SetLockVisual(false); // 恢复解锁视觉
//        Debug.Log($"🔓 【{itemName}】已解锁拾取！");
//    }

//    // 设置物品锁定/解锁的视觉效果
//    private void SetLockVisual(bool isLocked)
//    {
//        Color currentColor = spriteRenderer.color;
//        currentColor.a = isLocked ? lockAlpha : 1f; // 锁定半透明，解锁不透明
//        spriteRenderer.color = currentColor;
//    }

//    // 玩家触碰检测（2D触发）
//    void OnTriggerEnter2D(Collider2D other)
//    {
//        // 只有“解锁状态”+“玩家标签”才触发拾取
//        if (isPickupEnabled && other.CompareTag("Player"))
//        {
//            PickupItem();
//        }
//    }

//    // 物品拾取逻辑
//    private void PickupItem()
//    {
//        Debug.Log($"🎁 成功拾取【{itemName}】！");
//        // 扩展：这里可添加“加入背包”“显示UI提示”“播放拾取音效”等逻辑
//        Destroy(gameObject); // 拾取后销毁物品
//    }

//    // 调试辅助：场景视图中绘制拾取状态（红=锁定，绿=解锁）
//    void OnDrawGizmos()
//    {
//        Gizmos.color = isPickupEnabled ? Color.green : Color.red;
//        Gizmos.DrawWireSphere(transform.position, 0.5f);
//    }
//}