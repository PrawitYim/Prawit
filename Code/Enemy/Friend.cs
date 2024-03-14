using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Entity
{
    RaycastHit hit;
    public LayerMask Focouse;

    // Update is called once per frame
    void Update()
    {
        switch (currenState)
        {
            case State.idel:

                IdelLogic();
                break;

            case State.seeEnemy:
                //Change State From See to Attack
                SeeEnemyLogic();
                break;


            case State.attack:
                AttackLogic();
                break;
        }
    }

    public override void AttackLogic()
    {
        currenState = State.idel;
    }

    private void SeeEnemyLogic()
    {
        Vector3 directionToEnemy = hit.transform.position - transform.position;
        directionToEnemy.Normalize();
        float distance = Vector3.Distance(transform.position, directionToEnemy);
        transform.Translate(directionToEnemy * Statues.speed * Time.deltaTime);
        if (distance < 1)
        {
            currenState = State.attack;
        }
        else if (hit.collider == null)
        {
            currenState = State.idel;
        }
    }

    private void IdelLogic()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.Normalize();
        transform.Translate(directionToPlayer * Statues.speed * Time.deltaTime);
        //Check around for Enemy
        Physics.SphereCast(transform.position, Statues.seeDistance, transform.forward, out hit, Statues.seeDistance, Focouse);

        if (hit.collider != null)
        {
            currenState = State.idel;
        }
    }
}
