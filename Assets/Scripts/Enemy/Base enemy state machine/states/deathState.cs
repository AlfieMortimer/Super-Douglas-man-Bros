using UnityEngine;

namespace Enemy
{
    public class deathState : EnemyState
    {
        float timer = 1;
        public deathState(BaseEnemyScript enemy, EnemyStateMachine sm) : base(enemy, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.anim.Play("Death");
            enemy.rb.linearVelocity = Vector2.zero;
        }
        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (timer <= 0)
            {
                enemy.gameObject.SetActive(false);
            }
            else
            {
                timer -= Time.deltaTime;
            }

        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }

}