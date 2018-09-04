using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;

    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject gameOver;
    public GameObject scoreUIText;
    public GameObject highscoreUIText;
    public GameObject timeCounter;
    public GameObject title;
    public GameObject tutorialTouch;
    public GameObject tutorialText;


    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,

    }
    GameManagerState GMState;

    // Use this for initialization
    void Start()
    {
        //Screen.autorotateToPortrait = false;
        //Screen.autorotateToPortraitUpsideDown = false;
        GMState = GameManagerState.Opening;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
        void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                gameOver.SetActive(false);
                playerShip.GetComponent<PlayerControl>().canShoot = false;
                title.SetActive(true);
                tutorialText.SetActive(true);
                playerShip.GetComponent<PlayerControl>().Init();

                break;
            case GameManagerState.Gameplay:
                playerShip.GetComponent<PlayerControl>().canShoot = true;
                scoreUIText.GetComponent<GameScore>().Score = 0;
                tutorialText.SetActive(false);
                title.SetActive(false);
                //playButton.SetActive(false);

                

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                timeCounter.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.Gameover:
                playerShip.GetComponent<PlayerControl>().canShoot = false;
                //tutorialText.SetActive(true);
                playerShip.GetComponent<TouchControl>().firstTouch = true;
                playerShip.GetComponent<PlayerControl>().firstTouch = true;

                timeCounter.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                gameOver.SetActive(true);
                
               // highscoreUIText.GetComponent<GameScore>().StoreHighscore(scoreUIText.GetComponent<GameScore>().Score);
                //StoreHighscore();
                Invoke("ChangeToOpeningState", 4f);

                break;
        }
    }





    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}