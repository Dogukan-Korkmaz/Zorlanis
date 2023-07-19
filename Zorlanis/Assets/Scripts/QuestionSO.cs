using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Zorlanis!", fileName = "Yeni Soru")]
public class QuestionSO : ScriptableObject//Veri depolama komut dosyas�d�r.Sorular� bu �ekilde depolay�p,d�zenleyebiliriz.
{
    [TextArea(4,6)] [SerializeField] string question = "Soruyu buraya gir.";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    

    public string GetQuestion()//Soruyu al�r.
    {
        return question;
    }

    public string GetAnswer(int index)//Cevaplar� al�r.
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()//Do�ru cevab�n indeksini al�r.
    {
        return correctAnswerIndex;
    }
}


