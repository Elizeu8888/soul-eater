using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class State
    {
        protected EnemyMovement enemy;
        protected StateMachine sm;

        // base constructor
        protected State(EnemyMovement enemy, StateMachine sm)
        {
            this.enemy = enemy;
            this.sm = sm;
        }

        public virtual void Enter()
        {

        }

        public virtual void PlayAnim(string anim)
        {
            
        }

        public virtual Vector2 GetDirectionInput(Vector2 direction)
        {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector2(horizontal, vertical).normalized;
            return direction;
        }

        public virtual void PhysicsBehaviour(Rigidbody2D m_Rigidbody2D)
        {

        }


        public virtual void LogicUpdate()
        {
            if(enemy.RayCastForNav() == true)
            {
                enemy.SetNavAgent(2);
            }
            else
            {
                enemy.SetNavAgent(0);
            }
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }


}
