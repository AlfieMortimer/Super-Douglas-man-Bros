using UnityEngine;

public class mushroomMove : MonoBehaviour
{
    float timer;
    SpriteRenderer sr;
    Rigidbody2D rb;

    public Vector2 boxsize;
    public float castDistance;
    public LayerMask Ground;
    public bool startmove;


    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        faceRaycast();
        if (startmove)
        {
        rb.linearVelocityX = speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxsize);
    }

    void faceRaycast()
    {
        if (timer <= 0)
        {
            if (Physics2D.BoxCast(transform.position, boxsize, 0, -transform.up, castDistance, Ground))
            {
                speed = -speed;
                sr.flipX = !sr.flipX;
                timer = 1;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
