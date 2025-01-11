using TMPro;
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
    public float playStartTime;
    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject[] spawner;
    public TMP_Text text_Score;
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

    float CalculateScore()
    {
        return Time.time - playStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        // 사용자의 컴퓨터에 동기 작업 (점수 기록)
        int currHighScore = PlayerPrefs.GetInt("highScore");
        // 현재 점수가 지금까지의 최고 점수보다 높다면
        if(score > currHighScore)
        {
            // 기록 해준다 (갱신)
            PlayerPrefs.SetInt("highScore", score);
            // 저장
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        // 초기 속도 셋팅
        if(State != GameState.Playing) return 5f;

        // 속도 늘려주기 (게임 시작하면 8 -> 30까지 늘어난다)
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        // Maximum Speed
        return Mathf.Min(speed, 30f);
    }

    void Update()
    {
        // score Text
        if(State == GameState.Playing)
        {
            text_Score.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if(State == GameState.Dead)
        {
            text_Score.text = "High Score: " + GetHighScore();
        }

        // 게임 시작
        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            // spawner 켜주기
            for(int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(true);
            }
            playStartTime = Time.time;
        }

        // 죽음
        if(State == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            // spawner 꺼주기
            for(int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }
            SaveHighScore();
            // 플레이어 죽으면 죽음 UI 활성화 시켜주기
            DeadUI.SetActive(true);
            State = GameState.Dead;
        }

        // 재시작
        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
