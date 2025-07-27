using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    public GameObject rank;
    public GameObject restartText;
    public Text gameOverResult;

    bool isResult=false;
    IEnumerator Start()
    {
        rank.SetActive(false);
        restartText.SetActive(false);
        gameOverResult.text = "당신의 생존 시간 : " + GameManager.instance.score.ToString("F0") + "\n" + "당신의 최고 생존 시간 : " + UpdateBestScore();
        yield return new WaitForSeconds(2);
        rank.GetComponent<Text>().text = GetRank(GameManager.instance.rank);
        rank.SetActive(true);
        yield return new WaitForSeconds(1);
        restartText.SetActive(true);
        isResult = true;
    }
    
    string GetRank(Rank rank)
    {
        string result = "";
        switch (rank)
        {
            case Rank.F:
                result = "F";
                    break;
            case Rank.D:
                result = "D";
                break;
            case Rank.C:
                result = "C";
                break;
            case Rank.B:
                result = "B";
                break;
            case Rank.A:
                result = "A";
                break;
            case Rank.S:
                result = "S";
                break;
        }
        return result;
    }
    public int UpdateBestScore()
    {
        if (GameManager.instance.score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", (int)GameManager.instance.score);
        }
        return PlayerPrefs.GetInt("BestScore");

    }
    private void Update()
    {
        if (isResult && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
        else if (isResult && Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }
    }
}
