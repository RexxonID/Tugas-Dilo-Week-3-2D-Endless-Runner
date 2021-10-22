using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Score Highlight")]
    public int scoreHighlightRange;
    public CharacterSoundController sound;

    private int lastScoreHightlight = 0;
    private int currentScore = 0;

    private void Start()
    {
        //reset
        currentScore = 0;
        lastScoreHightlight = 0;
    }
    public float GetCurrentScore()
    {
        return currentScore;
    }
    public void IncreaseCurrentScore(int incerment)
    {
        currentScore += incerment;

        if(currentScore - lastScoreHightlight > scoreHighlightRange)
        {
            sound.playScoreHighlight();
            lastScoreHightlight += scoreHighlightRange;
        }
    }
    public void FinishScoreing()
    {
        //set high score
        if(currentScore > ScoreData.highScore)
        {
            ScoreData.highScore = currentScore;
        }
    }
}
