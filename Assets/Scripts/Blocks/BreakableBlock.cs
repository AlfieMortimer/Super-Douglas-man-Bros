using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D bc;
    ParticleSystem ps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        ps = gameObject.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            sr.enabled = false;
            bc.enabled = false;
            ps.Play();
            //Play break sound

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.linearVelocityY = 0f;
        }
    }
}
