using UnityEngine;
using System.Collections;

public class SplittingBullet : MonoBehaviour
{
    float speed;
    float xspeed;
    // Use this for initialization
    void Start()
    {
        speed = 4f;
        xspeed = 1f;
        GameObject playerShip = GameObject.Find("Player");
        if (playerShip != null && transform.position.x < playerShip.transform.position.x)
        {
            xspeed *= -1;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x + xspeed * Time.deltaTime, position.y + speed * Time.deltaTime);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }
    }
}
