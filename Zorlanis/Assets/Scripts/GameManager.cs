using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endScreen;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()//Quiz ekranýný görüntüleyecek
                //oyun sonu ekranýný göstermeyecek.
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        
    }
    void Update()//Oyun bittiðinde oyun sonu ekranýna götürecek.
    {
        if (quiz.isComplate == true)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()//Butona basýldýðýnda oyun tekrar baþlayacak.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
