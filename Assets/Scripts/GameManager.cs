using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Intro;
    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject[] spawner;
    public Player PlayerScript;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true);
    }

    void Update()
    {
        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            // spawner 켜주기
            for(int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(true);
            }
        }
        if(State == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            // spawner 꺼주기
            for(int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }
            State = GameState.Dead;
        }
        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
