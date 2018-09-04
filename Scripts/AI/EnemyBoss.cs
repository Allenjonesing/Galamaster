using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour {
    public GameObject EnemyBullet;
    private int timer;
    // Use this for initialization
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer > 55)
        {
            Invoke("FireEBullet", 0f);
            timer = 0;
        }
    }

    void FireEBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
