using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI LeftScore;
    public TextMeshProUGUI RightScore;

    public GameObject endScreen;       
    public TextMeshProUGUI winnerText;        

    private int scoreLeft = 0;
    private int scoreRight = 0;

    private void Start()
    {
        LeftScore.text = "0";
        RightScore.text = "0";

        if (endScreen != null)
            endScreen.SetActive(false);
    }

    public void AddLeftScore()
    {
        scoreLeft++;
        LeftScore.text = scoreLeft.ToString();
        CheckWinner();
    }

    public void AddRightScore()
    {
        scoreRight++;
        RightScore.text = scoreRight.ToString();
        CheckWinner();
    }

    private void CheckWinner()
    {
        if (scoreLeft >= 5)
        {
            ShowWinner("Vyhrál jsi! :-) ");
        }
        else if (scoreRight >= 5)
        {
            ShowWinner("Prohrál jsi :-( ");
        }
    }

    private void ShowWinner(string message)
    {
        winnerText.text = message;
        endScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        scoreLeft = 0;
        scoreRight = 0;

        LeftScore.text = "0";
        RightScore.text = "0";

        endScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}