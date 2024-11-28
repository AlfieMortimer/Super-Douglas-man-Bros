using Player;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour
{
    public PlayerScript player;

    public float coins;
    public float lives;

    public static playerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if(player.Coins != coins)
        {
            coins = player.Coins;
            updateLivesorCoins();
        }
    }

    public void updateLivesorCoins()
    {
        if(player != null)
        {
            player.updateCoinsandLives();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        player.Coins = coins;
        lives--;
        updateLivesorCoins();
    }

}
