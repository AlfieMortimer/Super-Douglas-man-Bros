using NUnit.Framework.Constraints;
using Player;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public GameObject itemSpawn;
    public Sprite deadblock;

    public bool item;

    Vector3 spawnpos;

    Animator animator;
    bool spawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnpos = transform.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //instanciate dead block when hit
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        sr.sprite = deadblock;
    //        if(spawned == false)
    //        {
    //            Instantiate(itemSpawn, spawnpos, Quaternion.identity);
    //            spawned = true;
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (collision.gameObject.layer == 6 && rb.linearVelocityY >= 0.1)
        {
            animator.Play("Stop");
            if (spawned == false)
            {
                if (item)
                {
                Instantiate(itemSpawn, spawnpos, Quaternion.identity);
                }
                else
                {
                    PlayerScript player = collision.GetComponent<PlayerScript>();
                    player.Coins++;
                    player.audioManager.playsfx(player.soundBin[0]);
                }
                spawned = true;
            }
        }
    }


}
