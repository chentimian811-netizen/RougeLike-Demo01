using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ËÀÍö×´Ì¬
public class EnemyDiedState : IState
{ 
    public Enemy enemy;
    public EnemyDiedState (Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }


}
