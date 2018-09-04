using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6, Enemy7;

    float maxRate = 3f;
    int spawnCount;

    public enum AIType
    {
        Cruiser,
        Attacker,
        Defender,
        Leader,
        Suicider,
        Miniboss,
        Boss
    }
    AIType aISpawn;
    // Use this for initialization
    void Start ()
    {
        aISpawn = AIType.Cruiser;
        spawnCount = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        spawnCount++;
        int range = (spawnCount - 1) / 5;

        if (range > 6)
            range = 6;

        switch(Random.Range(0,range+1))
        {
            case 0: //0-10

                GameObject anEnemy = (GameObject)Instantiate(Enemy);
                anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

                break;

            case 1: //11-20

                anEnemy = (GameObject)Instantiate(Enemy2);
                anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


                break;

            case 2: //21-30

                anEnemy = (GameObject)Instantiate(Enemy3);
                anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


                break;

            case 3://31-40

                anEnemy = (GameObject)Instantiate(Enemy4);
                anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


                break;

            case 4:
                float xpos = Random.Range(min.x, max.x);
                anEnemy = (GameObject)Instantiate(Enemy5);
                anEnemy.transform.position = new Vector2(xpos, max.y);
                anEnemy = (GameObject)Instantiate(Enemy2);
                anEnemy.transform.position = new Vector2(xpos + 0.2f, max.y+0.1f);
                anEnemy = (GameObject)Instantiate(Enemy2);
                anEnemy.transform.position = new Vector2(xpos- 0.2f, max.y+0.1f);


                break;

            case 5:

                anEnemy = (GameObject)Instantiate(Enemy6);
                anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


                break;
            case 6:

                anEnemy = (GameObject)Instantiate(Enemy7);
                anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


                break;


        }


        

        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float spawn;

        if (maxRate > 1f)
        {
            spawn = Random.Range(0.5f, maxRate);

        }
        else
            spawn = 1f;

        Invoke("SpawnEnemy", spawn);
    }

    void IncreaseSpawnRate()
    {
        if (maxRate > 1f)
            maxRate--;
        if (maxRate == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        maxRate = 3f;

        Invoke("SpawnEnemy", maxRate);

        InvokeRepeating("IncreaseSpawnRate", 0f, 10f);

    }

    public void UnscheduleEnemySpawner()
    {
        spawnCount = 0;
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
