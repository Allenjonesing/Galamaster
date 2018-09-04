using UnityEngine;
using System.Collections;

public class EnemyDefender : MonoBehaviour {

    public GameObject EnemyBullet;

    // Use this for initialization
    void Start()
    {
        Invoke("FireEBullet", 0.5f);
        Invoke("FireEBullet", 1.0f);
        Invoke("FireEBullet", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireEBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            bullet.transform.position = transform.position;

            Vector2 direction = new Vector2(0,-1);

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
