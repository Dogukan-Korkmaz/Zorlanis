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

        for (int i = 0; i < answerButtons.Length; i++)//Sorularýn cevaplarýný ekrandaki butonlara yazdýrýlmasý.
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
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

    void DisplayAnswer(int index)
    {
        string correctAnswer;

        if (index == currentQuestion.GetCorrectAnswerIndex())//Cevap doðruysa.
        {
            //SetButtonState(false);
            ShowSelectedAnswer();
            //Wait();
            //MakeItDefault();
            ShowCorrectAnswer();
            questionText.text = "Doðru!";
            scoreKeeper.IncrementCorrectAnswers();
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

        //void Wait()//Cevaba týklandýktan sonra bir miktar bekleme süresi devreye girsin.
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

    void GetRandomQuestions()//Rastgele bir soru seçilmesini saðlar ve
                                     //seçilen sorunun havuzdan çýkartýlmasýný saðlar.
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }        
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



