using Enemy;
using UnityEngine;


namespace Enemy
{
    public class BaseEnemyScript : MonoBehaviour
    {
        public float speed;

        public LayerMask Ground;

        public float castDistance;
        public Vector3 boxSize;

        public EnemyStateMachine sm;
        public Animator anim;
        public Rigidbody2D rb;
        public SpriteRenderer sr;
        
        //refer to states here

        public walkState walk;
        public deathState death;
        void Start()
        {
            sm = gameObject.AddComponent<EnemyStateMachine>();
            anim = gameObject.GetComponent<Animator>();
            rb = gameObject.GetComponent<Rigidbody2D>();
            sr = gameObject.GetComponent<SpriteRenderer>();

            //add states here
            walk = new walkState(this, sm);
            death = new deathState(this, sm);

            sm.Init(walk);
            Debug.Log(sm.CurrentState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        }

        void Update()
        {
            sm.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

    }

}
