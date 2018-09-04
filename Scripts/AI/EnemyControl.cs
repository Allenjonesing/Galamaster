using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUIText;
    public GameObject Explosion;
    public GameObject Powerup;
    public float speed;
    public Vector2 direction;
    public GameObject playerShip;
    public int MaxLives;
    int lives;

    // public enum AIType

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
    public AIType aIType;

    public enum AIState
    {
        Wait,
        Pursue,
        Avoid,
        Attack,
        Flee
    }
    AIState aIState;

	// Use this for initialization
	void Start ()
    {
        
        playerShip = GameObject.Find("Player");
        //speed = 2f;
        //aIType = (AIType)Random.Range(0, 6);
        switch (aIType)
        {
            case (AIType.Miniboss):
                MaxLives = 3;
                break;

            case (AIType.Boss):
                MaxLives = 5;
                break;

            default:
                MaxLives = 1;
                break;
        }

        lives = MaxLives;

        aIState = AIState.Wait;
        scoreUIText = GameObject.FindGameObjectWithTag("Scoretext");
        direction = playerShip.transform.position - transform.position;
        direction *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;


        if (aIType == AIType.Suicider)
        {


            if (playerShip != null)
            {
                //Vector2 direction = playerShip.transform.position - transform.position;
                position = transform.position;

                position += direction * speed * Time.deltaTime;

                transform.position = position;

                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

                if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (aIType == AIType.Miniboss)
        {
            if (transform.position.y >= 0.3f)
            {
                position = new Vector2(position.x, position.y - speed * Time.deltaTime);
                transform.position = position;

                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

                if (transform.position.y < min.y)
                {
                    //PlayExplosion();
                    Destroy(gameObject);
                    
                }
            }
        }
        else if (aIType == AIType.Boss)
        {
            if (transform.position.y >= 0.3f)
            {
                position = new Vector2(position.x, position.y - speed * Time.deltaTime);
                transform.position = position;

                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

                if (transform.position.y < min.y)
                {
                    //PlayExplosion();
                    Destroy(gameObject);
                }
            }
            else
            {
                if (playerShip != null)
                {
                    if ((transform.position.x < playerShip.transform.position.x) && (lives > 0))
                    {
                        position = new Vector2(position.x + speed * Time.deltaTime, position.y);
                        transform.position = position;
                    }
                    else if ((transform.position.x > playerShip.transform.position.x) && (lives > 0))
                    {
                        position = new Vector2(position.x - speed * Time.deltaTime, position.y);
                        transform.position = position;
                    }

                }
            }
                  
        }
        else
        { 
                    
            
           
                position = new Vector2(position.x, position.y - speed * Time.deltaTime);
                transform.position = position;

                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

                if (transform.position.y < min.y)
                {
                    //PlayExplosion();
                    Destroy(gameObject);
                }
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
           
            PlayExplosion();
            lives--;
            if (lives == 0)
            {
                scoreUIText.GetComponent<GameScore>().Score += 100;

                if (Random.Range(0, 9) == 5)
                {
                    GameObject powerup = Instantiate(Powerup);
                    powerup.transform.position = transform.position;
                }

                if ((aIType == AIType.Miniboss) || (aIType == AIType.Boss))
                {
                    //GetComponent<EnemyBoss>().enabled = false;
                    foreach (Transform child in transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    float time = 0.0f;
                    for (int i = 0; i < 6; i++)
                    {
                        time = ((float)i / 4);
                        Invoke("PlayExplosionCluster", time);
                        
                    }
                    Invoke("Destroy", time);
                    scoreUIText.GetComponent<GameScore>().Score += 400;
                }
                else
                    Destroy(gameObject);


            
               
            }
            
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }

    void PlayExplosionCluster()
    {
        GameObject explosion = Instantiate(Explosion);
        float randx = Random.Range(-0.1f, 0.1f);
        float randy = Random.Range(-0.1f, 0.1f);
        explosion.transform.position = new Vector2(transform.position.x + randx, transform.position.y + randy);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
