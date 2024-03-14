using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Entity
{
    void Update()
    {

        float distancePlayer = Vector3.Distance(transform.position, player.position);
        switch (currenState)
        {
            case State.idel:
                //x.transform.position = new Vector3(Random.Range(-Screen.width, Screen.width), Random.Range(-Screen.height, Screen.height), 0.0f);

                Debug.Log("Walk Around");
                if (distancePlayer < Statues.seeDistance)
                {
                    currenState = State.seeEnemy;
                }
                break;

            case State.seeEnemy:
                Debug.Log("Walk To Player");
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.Normalize();
                transform.Translate(directionToPlayer * Statues.speed * Time.deltaTime);
                if (distancePlayer < 1)
                {
                    currenState = State.attack;
                }
                else if (distancePlayer > Statues.seeDistance)
                {
                    currenState = State.idel;
                }
                break;

            case State.attack:
                Debug.Log("attack");
                if (distancePlayer > 1)
                {
                    currenState = State.idel;
                }
                break;

        }
    }

  
}
