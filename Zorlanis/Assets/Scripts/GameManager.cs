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

    void Start()//Quiz ekranını görüntüleyecek
                //oyun sonu ekranını göstermeyecek.
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        
    }
    void Update()//Oyun bittiğinde oyun sonu ekranına götürecek.
    {
        if (quiz.isComplate == true)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()//Butona basıldığında oyun tekrar başlayacak.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
