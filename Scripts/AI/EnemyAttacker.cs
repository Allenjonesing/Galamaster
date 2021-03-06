﻿using UnityEngine;
using System.Collections;

public class EnemyAttacker : MonoBehaviour
{

    public GameObject EnemyBullet;

// Use this for initialization
void Start()
{
        Invoke("FireEBullet", 0.7f);
        Invoke("FireEBullet", 1f);
        Invoke("FireEBullet", 1.3f);
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

        Vector2 direction = playerShip.transform.position - bullet.transform.position;

        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
    }
}
}
