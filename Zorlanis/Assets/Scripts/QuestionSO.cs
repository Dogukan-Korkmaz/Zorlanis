using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Zorlanis!", fileName = "Yeni Soru")]
public class QuestionSO : ScriptableObject//Veri depolama komut dosyasýdýr.Sorularý bu þekilde depolayýp,düzenleyebiliriz.
{
    [TextArea(4,6)] [SerializeField] string question = "Soruyu buraya gir.";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    

    public string GetQuestion()//Soruyu alýr.
    {
        return question;
    }

    public string GetAnswer(int index)//Cevaplarý alýr.
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()//Doðru cevabýn indeksini alýr.
    {
        return correctAnswerIndex;
    }
}


