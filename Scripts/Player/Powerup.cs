using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    private SpriteRenderer rend;

    //public object Powerup { get; internal set; }

    public int power;
    public int life;
    public float speed;



	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        power = Random.Range(0, 2);

        switch (power)
        {
            case 0:
               
                rend.color = Color.blue;
                break;

            case 1:
                rend.color = Color.green;

                break;

            case 2:
                rend.color = Color.yellow;

                break;

        }
    }

    void Update()
    {
        life++;

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if ((transform.position.y < min.y) || (life > 300))
        {
            //PlayExplosion();
            Destroy(gameObject);
        }


    }
	
}
