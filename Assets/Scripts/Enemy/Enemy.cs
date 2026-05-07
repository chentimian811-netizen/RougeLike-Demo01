using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput;
    public UnityEvent OnAttack;

    [SerializeField] private Transform Player;
    [SerializeField] private float chaseDistance = 3f;//¹¥»÷¾àÀë
    [SerializeField] private float attackDistance = 0.8f;//×·»÷¾àÀë

    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currentIndex = 0;
    private float pathGenerateInterval = 0.5f;
    private float pathGenerateTimer = 0f;

    
    private void Awake()
    {
        
        seeker = GetComponent<Seeker>();
        if (Player == null )
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if ( p != null ) Player = p.transform;
        }
    }
    private void Start()
    {
        //EnemyManager.Instance.enemyCount++;
    }

    private void OnDestroy()
    {
        if ( EnemyManager.Instance != null)
        {
            EnemyManager.Instance.enemyCount--;
        }
       
    }
    private void Update()
    {
        if (Player == null)
            return;

        float distance = Vector2.Distance(Player.position, transform.position);

        if (distance < chaseDistance)//Ð¡ÓÚ¹¥»÷·¶Î§
        {
            AutoPath();
            if (pathPointList == null)
                return;

            if (distance <= attackDistance)//ÊÇ·ñ´¦ÓÚ¹¥»÷·¶Î§ 
            {
                //¹¥»÷Íæ¼Ò
                OnMovementInput?.Invoke(Vector2.zero);
                OnAttack?.Invoke();
            }
            else
            {
                //×·»÷Íæ¼Ò
                //Vector2 direction = Player.position - transform.position;
                Vector2 direction = (pathPointList[currentIndex] - transform.position).normalized;
                OnMovementInput?.Invoke(direction.normalized);//°ÑÒÆ¶¯·½Ïò×ª¸øEnemyController
            }
        }
        else
        {
            //·ÅÆú×·»÷
            OnMovementInput?.Invoke(Vector2.zero);
        }

    }

    private void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(Player.position);
            pathGenerateTimer = 0f;

        }

        if (pathPointList == null || pathPointList.Count <= 0)
        {
            GeneratePath(Player.position);
        }
        else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count) GeneratePath(Player.position);
        }
    }

    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;

        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
}
