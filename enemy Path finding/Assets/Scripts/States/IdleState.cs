using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class IdleState : State
    {

        string animName = "idle";
        // constructor
        public IdleState(EnemyMovement enemy, StateMachine sm) : base(enemy, sm)
        {
        }


        public override void Enter()
        {
            base.Enter();
            PlayAnim(animName);
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            enemy.RotatingEnemy();

            if (enemy.CheckForGhost() == false)
            {
                sm.ChangeState(enemy.walkingState);
            }


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            enemy.rate = 0.1f;
        }


        public override void PlayAnim(string name)
        {
            enemy.PlayAnim(name);
        }


    }
}