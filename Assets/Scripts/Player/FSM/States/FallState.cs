
using UnityEngine;
namespace Player
{
    public class FallState : State
    {



        // constructor
        public FallState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.animator.Play("falling", 0, 0);
            player.animator.speed = 1;
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
            if(player.onGround == true)
            {
                player.sm.ChangeState(player.idle);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }
}