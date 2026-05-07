//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


////敌人待机状态
//public class EnemyIdleState : IState
//{
//    private Enemy enemy;

//    public EnemyIdleState(Enemy enemy)
//    {
//        this.enemy = enemy;
//    }
//    public void OnEnter()
//    {
//        enemy.animator.Play("Idle"); 
//    }

//    public void OnExit()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void OnFixedUpdate()
//    {
//        throw new System.NotImplementedException(); 
//    }

//    public void OnUpdate()
//    {
//        enemy.GetPlayerTransform();//玩家位置
//        if (enemy.Player != null)//如果玩家为空
//        {
//            if(enemy.distance > enemy.attackDistance)//大于攻击距离
//            {
//                enemy.TransitionState(EnemyStateType.Chase);
//            }
//            else if(enemy.distance <= enemy.attackDistance)//小于攻击距离切换为攻击状态
//            {
//                enemy.TransitionState(EnemyStateType.Attack);
//            }
//        }
//    }
//}