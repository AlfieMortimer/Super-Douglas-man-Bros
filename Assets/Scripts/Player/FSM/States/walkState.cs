
using UnityEngine;
namespace Player
{
    public class walkState : State
    {
        // constructor
        public walkState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        bool ran = false;
        public override void Enter()
        {
            base.Enter();
            player.animator.Play("Walking", 0, 0);
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
            Vector2 vel = player.rb.linearVelocity;
            if (player.joystick.Horizontal >= .1 || player.joystick.Horizontal <= -0.1)
            {
                if(player.running == true)
                {
                    vel.x = player.joystick.Horizontal * (player.speed * 1.5f);
                }
                else
                {
                    vel.x = player.joystick.Horizontal * player.speed;
                }

                player.rb.linearVelocity = vel;
            }

            if(player.rb.linearVelocityX <= 0)
            {
                player.animator.speed = ((player.rb.linearVelocityX * -1) * 0.2f);
            }
            else
            {
                player.animator.speed = (player.rb.linearVelocityX * 0.2f);
            }
            

            if (vel.x < -0.1)
            {
                player.sr.flipX = true;
            }
            else if (vel.x > 0.1) 
            {
                player.sr.flipX = false;
            }
            checkforIdle();
            checkforFall();


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void checkforIdle()
        {
            if (player.rb.linearVelocityX <= 0.1 && player.rb.linearVelocityX >= -0.1)
            {
                player.sm.ChangeState(player.idle);
            }

        }
        public void checkforFall()
        {
            if (player.onGround == false)
            {
                player.sm.ChangeState(player.fall);
            }
        }
        public void running()
        {
            if(player.running == true && ran == false && player.playerBig == true)
            {
                player.animator.Play("run");
            }
        }
    }
}