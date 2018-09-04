using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    //Text highscoreTextUI;

    public GameObject oldHighscoreObj;
   // public int oldHighscore;
    int score;

    public int Score
    {
        get
        {
            return this.score;

        }
        set
        {
            this.score = value;
           
            oldHighscoreObj.GetComponent<GameHighscore>().StoreHighscore(score);
            UpdateScoreTextUI();
        }
    }

	// Use this for initialization
	void Start ()
    {

        scoreTextUI = GetComponent<Text>();
        //highscoreTextUI = GetComponent<Text>();
    }

   /* public void StoreHighscore(int newHighscore)
    {
        //highscoreTextUI = GetComponent<Text>();

        oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore)
        {
            PlayerPrefs.SetInt("highscore", newHighscore);
            PlayerPrefs.Save();
            string scoreStr = string.Format("{0:000000}", PlayerPrefs.GetInt("highscore"));
            highscoreTextUI.text = scoreStr;
        }
        else
        {
            string scoreStr = string.Format("{0:000000}", oldHighscore);
            highscoreTextUI.text = scoreStr;
        }
    }*/

    void UpdateScoreTextUI()
    {
        //scoreTextUI = GetComponent<Text>();

        string scoreStr = string.Format("{0:000000}", score);
        scoreTextUI.text = scoreStr;
    }

}
