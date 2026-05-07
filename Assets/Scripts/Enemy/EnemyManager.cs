using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //单列模式
    public static EnemyManager Instance {  get; private set; }

    [Header("当前刷新点")]
    public Transform[] spawnPoints;
    [Header("当前巡逻点")]
    public Transform[] patrolPoints;

    [Header("传送出口")]
    public SceneExit exit;
    public bool isExitActive;

    [Header("当前关卡敌人")]
    public List<EnemyWave> enemyWaves;

    public int currentWaveIndex = 0;//当前波数  

    public int enemyCount = 0;//敌人数量

    //判断是否为最后一波
    public bool GetLastWave()
    {
        return currentWaveIndex == enemyWaves.Count;
    }

    private void Awake()
    {
        Instance = this;
        exit = FindObjectOfType<SceneExit>();
    }
    private void Update()
    {
        if(enemyCount == 0 && !GetLastWave())//当敌人死亡则生成下一波敌人
        {
            StartCoroutine(nameof(startNextWaveCoroutine));
        }
        else if(enemyCount == 0 && GetLastWave() && !isExitActive)
        {
            if(exit != null)
            {
                exit.gameObject.SetActive(true);
                isExitActive = true;    
            }
        }
    }

    IEnumerator startNextWaveCoroutine()
    {
        if (currentWaveIndex >= enemyWaves.Count)
            yield break;//已经没有波数 退出协程

        List<EnemyDate> enemies = enemyWaves[currentWaveIndex].enemies;//获取当前波数对应敌人列表

        foreach (EnemyDate enemyDate in enemies)
        {
            for (int i = 0; i < enemyDate.waveEnemyCount; i++)
            {
                Vector3 pos = GetRandomSpawnPiont();
                pos.z = 0f;
                GameObject enemy = Instantiate(enemyDate.enemyPrefab,pos,Quaternion.identity);
                enemyCount++;

                //if(patrolPoints != null)//巡逻点
                //{
                //    enemy.GetComponent<Enemy>().patrolPoints = patrolPoints;
                //}
                yield return new WaitForSeconds(enemyDate.spawnInterval);
            } 

        }
        currentWaveIndex++;
    }

    //从敌人刷新点得位置列表随便选取一个刷新点
    private Vector3 GetRandomSpawnPiont()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
    [System.Serializable]
    public class EnemyDate
    {
        public GameObject enemyPrefab;
        public float spawnInterval;
        public int waveEnemyCount;
    }


    [System.Serializable]
    public class EnemyWave
    {
        public List<EnemyDate> enemies;
    } 
}