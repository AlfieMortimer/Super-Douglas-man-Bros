using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.UI;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed;
        public bool grounded = false;
        public float jumpheight = 0;
        public LayerMask ground;
        public bool onGround = false;
        public FixedJoystick joystick;

        public Vector2 boxSize;
        public float castDistance;
        public bool running = false;

        public bool playerBig = false;
        public float Health = 1;

        public float Coins;

        public float invincibilityTimer;

        // variables holding the different player states
        //public exmapleState example;
        public IdleState idle;
        public walkState walk;
        public JumpState jump;
        public FallState fall;

        public StateMachine sm;
        public Animator animator;
        public SpriteRenderer sr;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();



            // add new states here
            //exampleState = new exampleState(this, sm);
            idle = new IdleState(this, sm);
            walk = new walkState(this, sm);
            jump = new JumpState(this, sm);
            fall = new FallState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idle);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();
            onGround = GroundCheck();
            CheckOnHealth();
        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

        public void InputJump()
        {
            if (sm.CurrentState != jump && onGround == true)
            {
                rb.linearVelocityY = 0;
                rb.AddForce(transform.up * jumpheight * 10, ForceMode2D.Impulse);

                sm.ChangeState(jump);
                onGround = false;
                Debug.Log("Succesfully jumped");
            }
            else
            {
                Debug.Log("One or more conditions were not met to jump");
                Debug.Log(onGround + " : " + sm.CurrentState);
            }
        }

        public void CheckOnHealth()
        {
            if(Health <= 0)
            {
                Debug.Log("PlayerHasDied");
            }
            if(Health >= 4)
            {
                Health = 3;
            }
            if(invincibilityTimer > 0 )
            {
                invincibilityTimer -= Time.deltaTime;
            }
            if(invincibilityTimer <= 0 && sr.color != Color.white)
            {
                sr.color = Color.white;
            }


        }

        public void runInput(bool Input)
        {
            running = Input;
            Debug.Log (Input + " : " + running);
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        }
        public bool GroundCheck()
        {
            if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, ground))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Coin")
            {
                Coins++;
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.layer == 9)
            {
                if (invincibilityTimer <= 0)
                {
                    Health--;
                    Debug.Log("Player has been hit. the player now has: " + Health + " Health remaining");
                    invincibilityTimer = 3;
                    sr.color = Color.grey;
                }
                
                
            }
        }
    }
}