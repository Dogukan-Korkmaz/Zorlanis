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

    void Start()//Quiz ekran�n� g�r�nt�leyecek
                //oyun sonu ekran�n� g�stermeyecek.
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        
    }
    void Update()//Oyun bitti�inde oyun sonu ekran�na g�t�recek.
    {
        if (quiz.isComplate == true)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()//Butona bas�ld���nda oyun tekrar ba�layacak.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
