using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField]GameObject[] answerButtons;
    bool hasAnsweredEalry;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    Image buttonImage;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplate;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        //DisplayQuestion();
        //GetNextQuestion();      
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEalry = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEalry && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)//Sorular�n cevaplar�n� ekrandaki butonlara yazd�r�lmas�.
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)//Butonlar�n
                                   //bas�labilir olup olamayaca��n� ayarlar.
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void DisplayAnswer(int index)
    {
        string correctAnswer;

        if (index == currentQuestion.GetCorrectAnswerIndex())//Cevap do�ruysa.
        {
            //SetButtonState(false);
            ShowSelectedAnswer();
            //Wait();
            //MakeItDefault();
            ShowCorrectAnswer();
            questionText.text = "Do�ru!";
            scoreKeeper.IncrementCorrectAnswers();
        }
        else//Cevap yanl��sa.
        {
            //SetButtonState(false);
            ShowSelectedAnswer();
            //Wait();
            ShowCorrectAnswer();
            questionText.text = "Yanl��! \n Do�ru cevap  : " + correctAnswer + ".";
        }

        void ShowCorrectAnswer()//Yanl�� cevap se�ildi�inde do�ru cevap g�sterilsin.
        {
            int correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
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

        //void Wait()//Cevaba t�kland�ktan sonra bir miktar bekleme s�resi devreye girsin.
        //{
        //    Thread.Sleep(1500);
        //}

        SetButtonState(false);
        timer.CancelTimer();
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            GetRandomQuestions();
            SetDefaultButtonSprites();
            DisplayQuestion();
            progressBar.value++;
            //ProgressBarBugHandler();
            scoreKeeper.IncrementGetQuestionSeen();
        }
    }

    void GetRandomQuestions()//Rastgele bir soru se�ilmesini sa�lar ve
                                     //se�ilen sorunun havuzdan ��kart�lmas�n� sa�lar.
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }        
    }

    void SetDefaultButtonSprites()//Cevaplar�n
                                  //varsay�lan renk yapar
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image button = answerButtons[i].GetComponent<Image>();
            button.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)//Cevaplar�n birinde t�kland���nda �al��acak yer.
    {
        hasAnsweredEalry = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Skor :  " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue)
        {
            isComplate = true;
        }
    }

    //void ProgressBarBugHandler()
    //{
    //    bool bum = true;
    //    if (bum == true)
    //    {
    //        progressBar.value--;
    //        bum = false;
    //    }
    //}
}



