
using Unity.VisualScripting;
using UnityEngine;
namespace Player
{
    public class JumpState : State
    {



        // constructor
        public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.animator.Play("Jump", 0, 0);
            player.animator.speed = 1;
            player.onGround = false;
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
                float speed = player.joystick.Horizontal * (player.speed * 0.5f);
                
                //if the stick is left and the velocity is positive
                if(player.joystick.Horizontal <= -.1 && player.rb.linearVelocityX >= 0.5)
                {
                    //allow left movement but not right
                    player.rb.linearVelocityX = player.rb.linearVelocityX * 0.8f;
                }
                //if the stick is held right and the velocity is negative
                else if(player.joystick.Horizontal >= .1 && player.rb.linearVelocityX <= -0.5)
                {
                    //allow right movement but not left
                    player.rb.linearVelocityX = player.rb.linearVelocityX * 0.8f;
                }
                //allow movement in all directions if no velocity is detected
                else if (player.rb.linearVelocityX <= 0.5 && player.rb.linearVelocityX >= -0.5)
                {
                    vel.x = speed;
                    player.rb.linearVelocityX = vel.x;
                    Debug.Log("Moved while jumping in one spot: " + player.rb.linearVelocityX);
                }

            }
            //if the players horizontal velocity is bigger than the calculated speed or smaller than the opposite speed then keep velocity 
            //if moving right and holding left change speed to 0.8

            if (vel.x < -0.1)
            {
                player.sr.flipX = true;
            }
            else if (vel.x > 0.1)
            {
                player.sr.flipX = false;
            }
            if (player.onGround == true && player.rb.linearVelocityY <=0)
            {
                player.sm.ChangeState(player.idle);
            }


            if(player.rb.linearVelocityY <= 0)
            {
                player.sm.ChangeState(player.fall);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }
}