using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649
    // Reference to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    // Reference to the scriptable object file
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649

    private string currentCategory = "";
    private int correctAnswerCount = 0;
    // Questions data
    private List<Question> questions;
    // Current question data
    private Question selectedQuetion = new Question();
    private int gameScore;
    private int lifesRemaining;
    private float currentTime;
    private QuizDataScriptable dataScriptable;

    private GameStatus gameStatus = GameStatus.NEXT;

    public GameStatus GameStatus { get { return gameStatus; } }

    public List<QuizDataScriptable> QuizData { get => quizDataList; }

    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        correctAnswerCount = 0;
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;

        // Set and shuffle the questions
        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);

        // Use ShuffleList to shuffle the questions
        questions = ShuffleList.ShuffleListItems(questions);

        // Select the first question
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    /// <summary>
    /// Method to select the current question from the shuffled list
    /// </summary>
    private void SelectQuestion()
    {
        if (questions.Count > 0)
        {
            // Take the first question from the shuffled list
            selectedQuetion = questions[0];
            quizGameUI.SetQuestion(selectedQuetion);

            // Remove the selected question from the list
            questions.RemoveAt(0);
        }
        else
        {
            // If no more questions, end the game
            GameEnd();
        }
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime); // Set the time value
        quizGameUI.TimerText.text = time.ToString("mm':'ss"); // Convert time to Time format

        if (currentTime <= 0)
        {
            // Game Over
            GameEnd();
        }
    }

    /// <summary>
    /// Method called to check if the selected answer is correct or not
    /// </summary>
    /// <param name="selectedOption">Answer string</param>
    /// <returns></returns>
    public bool Answer(string selectedOption)
    {
        bool correct = false; // Default to false
        if (selectedQuetion.correctAns == selectedOption)
        {
            correctAnswerCount++;
            correct = true;
            gameScore += 50;
            quizGameUI.ScoreText.text = "Score:" + gameScore;
        }
        else
        {
            lifesRemaining--; // Reduce life
            quizGameUI.ReduceLife(lifesRemaining);

            if (lifesRemaining == 0)
            {
                GameEnd();
            }
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                GameEnd();
            }
        }
        return correct;
    }

    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.GameOverPanel.SetActive(true);

        // Save the score
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); // Save the score for this category
    }
}

// Data structure for storing question data
[System.Serializable]
public class Question
{
    public string questionInfo;         // Question text
    public QuestionType questionType;  // Type
    public Sprite questionImage;       // Image for Image Type
    public AudioClip audioClip;        // Audio for audio type
    public UnityEngine.Video.VideoClip videoClip; // Video for video type
    public List<string> options;       // Options to select
    public string correctAns;          // Correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}
