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
        GetNextQuestion();
        //DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)//Sorularýn cevaplarýný ekrandaki butonlara yazdýrýlmasý.
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)//Butonlarýn
                                   //basýlabilir olup olamayacaðýný ayarlar.
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        DisplayQuestion();
        SetDefaultButtonSprites();
    }

    void SetDefaultButtonSprites()//Cevaplarýn
                                  //varsayýlan renk yapar
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image button = answerButtons[i].GetComponent<Image>();
            button.sprite = defaultAnswerSprite;
        }
    }

    

    public void OnAnswerSelected(int index)//Cevaplarýn birinde týklandýðýnda çalýþacak yer.
    {
        string correctAnswer;

        if (index == question.GetCorrectAnswerIndex())//Cevap doðruysa.
        {
            //SetButtonState(false);
            ShowSelectedAnswer();
            //Wait();
            //MakeItDefault();
            ShowCorrectAnswer();
            questionText.text = "Doðru!";
        }
        else//Cevap yanlýþsa.
        {
            //SetButtonState(false);
            ShowSelectedAnswer();
            //Wait();
            ShowCorrectAnswer();
            questionText.text = "Yanlýþ! \n Doðru cevap  : " + correctAnswer + ".";            
        }

        void ShowCorrectAnswer()//Yanlýþ cevap seçildiðinde doðru cevap gösterilsin.
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

        //void Wait()//Cevaba týklandýktan sonra bir miktar bekleme süresi devreye girsin.
        //{
        //    Thread.Sleep(1500);
        //}

        SetButtonState(false);
    }
}



