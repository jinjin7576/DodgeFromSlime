using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Rank
{
    F,
    D,
    C,
    B,
    A,
    S,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Text surviveTimer;

    public Rank rank = Rank.F;
    public bool isGameover = false;
    public float score;

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

    }

 
    void Update()
    {
        if (!isGameover)
        {
            score += Time.deltaTime;
            surviveTimer.text = "생존 시간 : " + score.ToString("F0");
        }
    }
    public void GameOver()
    {
        isGameover = true;
    
        if (score < 5)
        {
            rank = Rank.F;
        }
        else if (score >= 5 && score < 10)
        {
            rank = Rank.D;
        }
        else if (score >= 10 && score < 20)
        {
            rank = Rank.C;
        }
        else if (score >= 20 && score < 40)
        {
            rank = Rank.B;
        }
        else if (score >= 40 && score < 60)
        {
            rank = Rank.A;
        }
        else
        {
            rank = Rank.S;
        }
        Invoke("LoadGameOver", 1.5f);
    }
    void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
