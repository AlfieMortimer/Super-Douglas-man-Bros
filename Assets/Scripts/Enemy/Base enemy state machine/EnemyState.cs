using UnityEngine;

namespace Enemy
{

    public abstract class EnemyState
    {
        protected BaseEnemyScript enemy;
        protected EnemyStateMachine sm;


        // base constructor
        protected EnemyState(BaseEnemyScript enemy, EnemyStateMachine sm)
        {
            this.enemy = enemy;
            this.sm = sm;
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            //Debug.Log("This is base.enter");
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}
