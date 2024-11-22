using Enemy;
using System.Threading;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Enemy
{
    public class walkState : EnemyState
    {

        float timer = 0;

        // constructor
        public walkState(BaseEnemyScript enemy, EnemyStateMachine sm) : base(enemy, sm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            enemy.anim.Play("GoombaWalk");

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
            Vector2 vel = enemy.rb.linearVelocity;
            vel.x = enemy.speed;
            enemy.rb.linearVelocity = vel;
            faceRaycast();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        void faceRaycast()
        {
            if(timer <= 0)
            {
                if (Physics2D.BoxCast(enemy.transform.position, enemy.boxSize, 0, -enemy.transform.up, enemy.castDistance, enemy.Ground))
                {
                    enemy.speed = -enemy.speed;
                    enemy.sr.flipX = !enemy.sr.flipX;
                    timer = 1;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
