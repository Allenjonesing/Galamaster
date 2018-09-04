using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject PlayerBullet;
    public GameObject SplittingBullet;
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject BulletPosition03;
    public GameObject Explosion;

    //public Text LivesUIText;

    const int MaxLives = 1;
    int lives;
    public float speed;

    public int shotTimer;
    public int shotTimeOut;
    public bool canShoot;
    public bool canDie;
    public bool firstTouch;



    public enum Power
    {
        Normal,
        Rapid,
        Split

    }
    Power power;
    public Text scoreUIText;

    public void Init()
    {
        lives = MaxLives;
        
        //LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
        power = Power.Normal;
    }
	// Use this for initialization
	void Start ()
    {
	    firstTouch = true;
	}
	
	// Update is called once per frame
	void Update ()
    {

        shotTimer++;
        switch(power)
        {
            case Power.Normal:
                if ((shotTimer >= shotTimeOut) && (canShoot))
                {
                     shotTimer = 0;
                    GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
                    bullet01.transform.position = BulletPosition01.transform.position;

                    GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
                    bullet02.transform.position = BulletPosition02.transform.position;

                }
                break;

            case Power.Rapid:
                if ((shotTimer >= (shotTimeOut/2)) && (canShoot))
                {
                    shotTimer = 0;
                    GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
                    bullet01.transform.position = BulletPosition01.transform.position;

                    GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
                    bullet02.transform.position = BulletPosition02.transform.position;

                }
                break;

            case Power.Split:
                if ((shotTimer >= shotTimeOut) && (canShoot))
                {
                    shotTimer = 0;
                    GameObject bullet01 = (GameObject)Instantiate(SplittingBullet);
                    bullet01.transform.position = BulletPosition01.transform.position;

                    GameObject bullet02 = (GameObject)Instantiate(SplittingBullet);
                    bullet02.transform.position = BulletPosition02.transform.position;

                    GameObject bullet03 = (GameObject)Instantiate(PlayerBullet);
                    bullet03.transform.position = BulletPosition03.transform.position;
                }
                break;
         }

        //Check for keyboard input
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            //is first time?
            if (firstTouch)
            {
                //Begin gameplay
                GameManager.StartGamePlay();
                firstTouch = false;
            }

            //Get key input
            float x = Input.GetAxisRaw("Horizontal"); //-1, 0 or 1
            float y = Input.GetAxisRaw("Vertical"); //-1, 0 or 1

            //Direction vector
            Vector2 direction = new Vector2(x, y).normalized;

            //Set player pos
            Move(direction);
        }//End key input


    }


    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        max.x = max.x - 0.025f; //player sprite half width
        min.x = min.x + 0.025f; //player sprite half width

        max.y = max.y - 0.085f; //player sprite half width
        min.y = min.y + 0.085f; //player sprite half width

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if( ( (col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "BossTag") ) && (canDie) ) 
        {


            //PlayExplosion();
            for (int i = 0; i < 6; i++)
            {
                float time = ((float)i / 4);
                Invoke("PlayExplosionCluster", time);
            }

            gameObject.SetActive(false);
                GameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
                GameObject[] bosses = GameObject.FindGameObjectsWithTag("BossTag");

                for (var i = 0; i < bosses.Length; i++)
                {
                    Destroy(bosses[i]);
                }



                // gameObject.SetActive(true);
            //}

            ;
        }
        else if (col.tag == "Powerup")
        {
            scoreUIText.GetComponent<GameScore>().Score += 500;
            power = 1 + ((Power)col.GetComponent<Powerup>().power);
            Destroy(col.gameObject);

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
        explosion.transform.position = new Vector2( transform.position.x +  randx, transform.position.y + randy );
    }
}
