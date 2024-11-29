using Player;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour
{
    public PlayerScript player;

    float timer = 7;

    public Scene lvl1;

    public float coins;
    public float lives;

    bool gameover = false;

    public AudioClip outOfLives;
    public AudioClip menuMusic;
    public audioManager audioManager;
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
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
        
    }

    private void Update()
    {
        if(player != null)
        {
            if(player.Coins != coins)
            {
                coins = player.Coins;
                updateLivesorCoins();
            }
        }
        if (lives == 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SceneManager.LoadScene("TitleScreen");
                audioManager.playMusic(menuMusic);
                lives = 6;
                gameover = false;

            }
        }

        if (lives == 0 && gameover == false)
        {
            gameover = true;
            audioManager.musicSource.Stop();
            audioManager.SFXSource.Stop();

            audioManager.playsfx(outOfLives);
            SceneManager.LoadScene("OutOfLives");
            
        }
    }


    public void updateLivesorCoins()
    {
        if(player != null)
        {
            player.coinsCount.text = "X " + coins;
            player.livesCount.text = "X " + lives;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
            
        if(player != null)
        {
            player.Coins = coins;
            lives--;
            updateLivesorCoins();

        }

    }





}
