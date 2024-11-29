using Enemy;
using Player;
using UnityEngine;

public class enemyDeathZone : MonoBehaviour
{

    public GameObject himself;
    public EnemyStateMachine sm;
    public BaseEnemyScript bes;

    public GameObject player;
    public PlayerScript ps;
    private void Start()
    {
        sm = himself.GetComponent<EnemyStateMachine>();
        bes = himself.GetComponent<BaseEnemyScript>();

        player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            ps = player.GetComponent<PlayerScript>();
        }  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 10)
        {
            if (sm == null)
            {
                sm = himself.GetComponent<EnemyStateMachine>();
                sm.ChangeState(bes.death);
                ps.onGround = true;
                ps.InputJump();
                ps.audioManager.playsfx(ps.soundBin[6]);
            }
            else
            {
                sm.ChangeState(bes.death);
                ps.onGround = true;
                ps.InputJump();
                ps.audioManager.playsfx(ps.soundBin[6]);
            }
        }
    }
}
