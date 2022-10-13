using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class WalkingState : State
    {

        string animName = "walking";
        // constructor
        public WalkingState(EnemyMovement enemy, StateMachine sm) : base(enemy, sm)
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

            PhysicsUpdate();


            enemy.MoveRBtoNav();
            enemy.RotatingEnemy();

            if (enemy.CheckForGhost() == true)
            {
                sm.ChangeState(enemy.idleState);
            }



        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            enemy.rate = 0.7f;
        }

        public override void PlayAnim(string name)
        {
            enemy.PlayAnim(name);
        }

        

    }
}
