using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.UI;
using JetBrains.Annotations;
using TMPro;

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
        public float gravityScale;
        public GameObject feet;

        public Vector2 boxSize;
        public float castDistance;
        public bool running = false;

        public bool playerBig = false;
        public float Health = 1;

        public float Coins;
        public float lives;

        public bool DIE;

        public TextMeshProUGUI livesCount;
        public TextMeshProUGUI coinsCount;

        public float invincibilityTimer;
        public int levelIndex;
        // variables holding the different player states
        //public exmapleState example;
        public IdleState idle;
        public walkState walk;
        public JumpState jump;
        public FallState fall;
        public DeathState death;

        public StateMachine sm;
        public Animator animator;
        public SpriteRenderer sr;

        public audioManager audioManager;
        public AudioClip[] soundBin;
        void Start()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();

            Time.timeScale = 1;
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();


            levelIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // add new states here
            //exampleState = new exampleState(this, sm);
            idle = new IdleState(this, sm);
            walk = new walkState(this, sm);
            jump = new JumpState(this, sm);
            fall = new FallState(this, sm);
            death = new DeathState(this, sm);

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
            if (sm.CurrentState != jump && onGround == true && Health > 0)
            {
                sm.ChangeState(jump);
            }
            else
            {
                Debug.Log("One or more conditions were not met to jump");
                Debug.Log(onGround + " : " + sm.CurrentState);
            }
        }

        public void stopJump()
        {
            if(onGround == false)
            {
                rb.gravityScale = gravityScale;
            }
        }

        public void CheckOnHealth()
        {
            if(Health <= 0 && DIE == false)
            {
                sm.ChangeState(death);
                DIE = true;
                audioManager.playsfx(soundBin[2]);
            }
            if (Health >= 4)
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

        public void paused(bool pause)
        {
            if (pause)
            {
                audioManager.playsfx(soundBin[3]);
                audioManager.musicSource.Pause();
            }
            else
            {
                audioManager.musicSource.UnPause();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Coin")
            {
                Coins++;
                coinsCount.text = "X " + Coins;
                Destroy(collision.gameObject);
                audioManager.playsfx(soundBin[0]);
            }

            if (collision.gameObject.tag == "Mushroom")
            {
                if(playerBig == false)
                {
                    playerBig = true;
                    transform.localScale = new Vector3(1, 2, 1);
                    //feet.transform.localPosition = new Vector2(0, -1);
                    castDistance = 1.1f;
                    Destroy(collision.gameObject);
                    Health++;
                    audioManager.playsfx(soundBin[7]);
                }
            }

            if (collision.gameObject.layer == 9 && invincibilityTimer <= 0 && rb.linearVelocityY <= 0.1)
            {
                Health--;
                playerBig = false;
                castDistance = 0.5f;
                transform.localScale = new Vector3(1, 1, 1);
                invincibilityTimer = 3;
                sr.color = Color.grey;

                if(Health > 0)
                {
                    audioManager.playsfx(soundBin[8]);
                }

            }
            if (collision.gameObject.layer == 11)
            {
                //play animations when win BEFORE loading next scene.
                SceneManager.LoadScene(levelIndex);
                Debug.Log(levelIndex);
            }

            if (collision.gameObject.layer == 12)
            {
                Health = 0;
            }
        }
    }
}