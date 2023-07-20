using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField]GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    Image buttonImage;

    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)//Sorular�n cevaplar�n� ekrandaki butonlara yazd�r�lmas�.
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)//Cevaplar�n birinde t�kland���nda �al��acak yer.
    {
        string correctAnswer;

        if (index == question.GetCorrectAnswerIndex())//Cevap do�ruysa.
        {
            ShowSelectedAnswer();
            Wait();
            //MakeItDefault();
            ShowCorrectAnswer();
            questionText.text = "Do�ru!";
        }
        else//Cevap yanl��sa.
        {
            ShowSelectedAnswer();
            Wait();
            ShowCorrectAnswer();
            questionText.text = "Yanl��! \n Do�ru cevap  : " + correctAnswer + ".";            
        }

        void ShowCorrectAnswer()//Yanl�� cevap se�ildi�inde do�ru cevap g�sterilsin.
        {
            int correctAnswerIndex = question.GetCorrectAnswerIndex();
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            correctAnswer = question.GetAnswer(correctAnswerIndex);
        }

        void ShowSelectedAnswer()
        {
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        //void MakeItDefault()
        //{
        //    buttonImage = answerButtons[index].GetComponent<Image>();
        //    buttonImage.sprite = defaultAnswerSprite;
        //}

        void Wait()//Cevaba t�kland�ktan sonra bir miktar bekleme s�resi devreye girsin.
        {
            Thread.Sleep(1500);
        }
    }
}



