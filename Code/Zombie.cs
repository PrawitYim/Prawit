using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{

    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator anim;
    public bool _isWalk = false;



    enum State { Idle, Patrol, Chase }
    State _currentState = State.Idle;

    public List<Transform> Waypoints;

    public float DetectionDistance = 12f;
    public float DetectionAngle = 90f;
    public float NearDetectDistance = 4f;
    public float WalkSpeed = 1f;
    public float ChaseSpeed = 3f;
    const float HuntTime = 3f;
    public AudioSource AudioSource;

    //public 

    Transform _transform;

    public Transform Target;
    NavMeshAgent _agent;
    Animator _animator;
    float _huntTimeCount;
    bool _attacking;
    int _currentWaypoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _animator = GetComponent<Animator>();  
        EnterState(State.Patrol); 
    }

    void Update()
    {
        UpdateState(); 
    }

    void EnterState(State state)
    {
        ExitState();
        _currentState = state;
        _animator.SetTrigger(_currentState.ToString());
        switch (_currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                _currentWaypoint = Random.Range(0, Waypoints.Count);
                _agent.speed = WalkSpeed;
                _agent.SetDestination(Waypoints[_currentWaypoint].position);
                break;
            case State.Chase:
                _agent.speed = ChaseSpeed;
                _huntTimeCount = 0;
                break;
        }
    }
    void UpdateState()
    {
        Vector3 eyePos = _transform.position + new Vector3(0, 1.8f, 0);
        Vector3 playerPos = Target.position + new Vector3(0, 1f, 0);
        Vector3 dir = (playerPos - eyePos).normalized * DetectionDistance;
        //  Debug.DrawRay(eyePos, dir, Color.cyan);
        Ray ray = new(eyePos, dir);
        RaycastHit hit;



        switch (_currentState)
        {
            case State.Idle:
                if (DetectPlayer(ray, dir, out hit))
                {
                    Target = hit.transform;
                    EnterState(State.Chase);
                }
                break;

            case State.Patrol:
                if (DetectPlayer(ray, dir, out hit))
                {
                    Target = hit.transform;
                    EnterState(State.Chase);
                }

                if(Vector3.Distance(_transform.position, _agent.destination) <= 2)
                {
                    _currentWaypoint = Random.Range(0, Waypoints.Count);
                    _agent.SetDestination(Waypoints[_currentWaypoint].position);
                }
                break;

            case State.Chase:
                bool sawPlayer = false;
                if (DetectPlayer(ray, dir, out hit))
                {
                    sawPlayer = true;
                    _huntTimeCount = 0;
                    _agent.SetDestination(Target.position);

                    if(hit.distance <= 2 && !_attacking)
                    {
                        StartCoroutine(Attack());
                    }

                }
                if(! sawPlayer)
                {
                    _huntTimeCount += Time.deltaTime;
                    if(_huntTimeCount >= HuntTime)
                    {
                        _agent.SetDestination(_transform.position);
                        EnterState(State.Patrol);
                    }
                }
                break;
        }
        float x = _transform.position.x;
        float y = _transform.position.y;
           
            

        if (x != 0 || y != 0)
        {
            _isWalk = true;
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            _isWalk = false;
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
        }


        if (x != 0 && x < 0)
            sr.flipX = true;
        else if (x != 0 && x > 0)
            sr.flipX = false;
    }
    void ExitState()
    {
        switch (_currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                break;
            case State.Chase:
                break;
        }
    }

    bool DetectPlayer(Ray ray, Vector3 dir, out RaycastHit hit)
    {
        Vector3 eyePos = _transform.position + new Vector3(0, 1.8f, 0);
        

        float detectionDistance = DetectionDistance;

        Debug.DrawRay(eyePos, dir.normalized * detectionDistance, Color.cyan);
        
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.collider.tag == "PP")
            {
                if (hit.distance < NearDetectDistance ||
                    Vector3.Angle(dir, _transform.forward) < DetectionAngle)
                    return true;
            }
        }
        return false;
    }


    IEnumerator Attack()
    {
        _agent.speed = 0;
        _animator.SetTrigger("Attack");
        _attacking = true;
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.7f);
        _attacking = false;
        _agent.speed = ChaseSpeed;
        _animator.SetTrigger("Chase");
    }

}
