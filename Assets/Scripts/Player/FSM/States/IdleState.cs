
using UnityEngine;
namespace Player
{
    public class IdleState : State
    {


        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.animator.Play("Idle", 0, 0);
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
            checkForWalk();
            if(player.onGround == false)
            {
                player.sm.ChangeState(player.fall);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void checkForWalk()
        {
            if (player.joystick.Horizontal >= 0.5 || player.joystick.Horizontal <= -0.5)
            {
                player.sm.ChangeState(player.walk);
                Debug.Log("changed state");
            }
        }
    }
}