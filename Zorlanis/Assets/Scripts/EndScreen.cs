using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Tebrikler! \n Skrounuz : " + scoreKeeper.CalculateScore() + "%";
    }
}
