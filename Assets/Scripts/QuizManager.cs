using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GOPanel;
    public GameObject Retry;
    public GameObject Code;
    public GameObject QuestionImg;
    public GameObject qTxt;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0;
    public int score;

    public int timeElapsed;

    public string sceneName;

    Scene scene;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        scene = SceneManager.GetActiveScene();
        QuestionImg.SetActive(false);
        generateQuestion();
    }
    
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;

        if (score == totalQuestions)
        {
            Retry.SetActive(false);

            StartCoroutine(WaitForScene());
        }
        else
        {
            Code.SetActive(false);
        }
    }
    public void correct()
    {
       score += 1;
       QnA.RemoveAt(currentQuestion);
       StartCoroutine(WaitForNext()); 
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext()); 
    }
    
    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }

    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);
    }

    void SetAnswers()
    {
        if (scene.name == "Quiz2")
        {
            for (int i = 0; i < options.Length; i++)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = false;
                options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].ImgAnswers[i];
            
                if (QnA[currentQuestion].CorrectAnswer == i+1)
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < options.Length; i++)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = false;
                options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            
                if (QnA[currentQuestion].CorrectAnswer == i+1)
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = true;
                }
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0,QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            
            if (scene.name == "Quiz")
            {
                if (QuestionTxt.text == "What is the name of the organizational theory based on the following diagram?")
                {
                    qTxt.SetActive(false);
                    QuestionImg.SetActive(true);
                }
                else
                {
                    qTxt.SetActive(true);
                    QuestionImg.SetActive(false);
                }
            }

            SetAnswers();
        }
        else
        {
            Debug.Log("Out OfQuestions");
            GameOver();
        }
    }
}
