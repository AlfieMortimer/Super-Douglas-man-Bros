using UnityEngine.SceneManagement;
using UnityEngine;

namespace Player
{
    public class DeathState : State
    {
        float timer;
        float position;
        // constructor
        public DeathState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            position = player.transform.position.y - 2f;
            Debug.Log(position);

            Time.timeScale = 0;
            base.Enter();
            player.animator.Play("DeathAnim", 0, 0);
            player.animator.speed = 1;
            player.rb.linearVelocity = new Vector2(0, 0);
            timer = 2;
            
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
            if (timer < 0)
            {
                player.transform.position -= new Vector3(0, 2, 0) * Time.unscaledDeltaTime * 2;
            }
            else
            {
                timer -= Time.unscaledDeltaTime;
            }

            if (player.transform.position.y <= position)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Debug.Log(player.transform.position.y + " : " + position);

            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }
}