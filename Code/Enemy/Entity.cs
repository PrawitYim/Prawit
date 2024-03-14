using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Friend;

public class Entity : MonoBehaviour
{
    [System.Serializable]
    public class statues
    {
        public int Hp;
        public int Damage;
        public int speed = 2;
        public float seeDistance = 5;
    }
    public enum State
    {
        idel = 0,
        seeEnemy = 1,
        attack = 2,
        death = 3,
    }
    public Transform player;
    protected int state = 0;
    public statues Statues;
    public State currenState = State.idel;

    void Start()
    {
        player = GameObject.FindWithTag("PP").transform;
    }
    public virtual void AttackLogic()
    {
        Debug.Log("Attack");
    }



}
