using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Text surviveTimer;
    Text highScore;
    GameObject gameoverUI;

    public bool isGameover = false;
    float score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        surviveTimer = GameObject.Find("SurviveTimeText").GetComponent<Text>();
        gameoverUI = GameObject.Find("GameOverUI");
        highScore = GameObject.Find("HighScore").GetComponent<Text>();
        gameoverUI.SetActive(false);
    }

 
    void Update()
    {
        if (!isGameover)
        {
            score += Time.deltaTime;
            surviveTimer.text = "생존 시간 : " + score.ToString("F0");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGameover)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && isGameover)
        {
            SceneManager.LoadScene("Main");
        }
    }
    public void GameOver()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    
        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            highScore.text = "최고 생존 시간 : " + score.ToString("F0") ;
            PlayerPrefs.SetFloat("HighScore", Mathf.RoundToInt(score));
        }
        else
        {
            highScore.text = "최고 생존 시간 : " + PlayerPrefs.GetFloat("HighScore");
        }
    }
}
