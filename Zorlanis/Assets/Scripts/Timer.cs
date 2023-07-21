using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteTheQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool loadNextQuestion;
    float timerValue;
    public bool isAnsweringQuestion;
    public float  fillFraction;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()//Soruyu cevaplama süresi ve doðru
                      //cevabý gösterme süresi þeklinde bir zaman döngüsü
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                 fillFraction = timerValue / timeToCompleteTheQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                 fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteTheQuestion;
                loadNextQuestion = true;
            }
        }
        Debug.Log(isAnsweringQuestion + " : " + timerValue + " = " +  fillFraction);
    }
}



