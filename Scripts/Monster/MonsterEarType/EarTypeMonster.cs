﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarTypeMonster : MonoBehaviour
{
    [field: Header("Reference")]
    [field: SerializeField] public MonsterSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public MonsterEarTypeAnimationData AnimationData { get; private set; }

    [field: Header("Behavior")]
    [field: SerializeField] public bool CanPatrol { get; set; } = true;
    [field: SerializeField] public bool CanComeBack { get; set; } = true;
    [SerializeField][field: Range(0f, 50f)] public float patrolRangeMin = 30f;
    [SerializeField][field: Range(0f, 70f)] public float patrolRangeMax = 50f;

    [field: Header("Noise")]
    public LayerMask targetLayer;
    public List<Collider> noiseMakers;

    //[Header("MonsterTransform")]
    //public Transform monsterTransform;
    //public Transform monsterEyeTransform;

    // 행동 관리
    public bool IsBehavior { get; set; } = true;
    private bool _isWaiting = false;
    private Coroutine _wait;
    //private float _waitTiem = 0f;

    private MonsterEarTypeStateMachine _stateMachine;

    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public Animator Animator { get; private set; }
    // Ai Nav
    public NavMeshAgent Agent { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        //Ai Nav
        Agent = GetComponent<NavMeshAgent>();
        ForceReceiver = GetComponent<ForceReceiver>();

        _stateMachine = new MonsterEarTypeStateMachine(this);
        noiseMakers = new List<Collider>();
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    private void Update()
    {
        if (GameManager.Instance.playerDie)
        {
            MonsterOff();
            return;
        }

        _stateMachine.Update();

        // 임시 코드
        //DrawCircle(transform.position, 36, Data.GroundData.PlayerChasingRange, Color.green);
        //DrawCircle(transform.position, 36, 50f, Color.green);

        //DrawCircle(transform.position, 36, Data.GroundData.AttackRange, Color.red);
    }

    public void WaitForBehavior(float time)
    {
        _wait = StartCoroutine(ChangeBehavior(time));
    }

    public void StopWait()
    {
        if (!_isWaiting) return;
        StopCoroutine(_wait);
    }

    public IEnumerator ChangeBehavior(float time)
    {
        _isWaiting = true;
        //_waitTiem = 0f;
        //Debug.Log($"{Data.GroundData.FocusTransitionTime}초 대기");
        // n초 대기
        yield return new WaitForSeconds(time);
        //yield return new WaitForSeconds(Data.GroundData.FocusTransitionTime);

        IsBehavior = !IsBehavior;
        //Debug.Log($"{Data.GroundData.FocusTransitionTime}초 대기 끝, {IsBehavior}");
        _isWaiting = false;
    }

    public void MonsterOff()
    {
        gameObject.SetActive(false);
    }

    private void DrawCircle(Vector3 center, int segments, float radius, Color color)
    {
        Vector3 normal = Vector3.up;

        float angleStep = 360.0f / segments;
        Quaternion rotation = Quaternion.LookRotation(normal);  // 법선 벡터를 기준으로 회전

        Vector3 prevPoint = center + rotation * new Vector3(Mathf.Cos(0) * radius, Mathf.Sin(0) * radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Vector3 currentPoint = center + rotation * point;

            Debug.DrawLine(prevPoint, currentPoint, color);  // 이전 점과 현재 점을 연결하여 선을 그림
            prevPoint = currentPoint;
        }

        // 마지막 점과 첫 번째 점을 연결하여 원을 완성
        Vector3 firstPoint = center + rotation * new Vector3(Mathf.Cos(0) * radius, Mathf.Sin(0) * radius, 0);
        Debug.DrawLine(prevPoint, firstPoint, color);
    }

    //private void FixedUpdate()
    //{
    //    _stateMachine.PhysicsUpdate();
    //}
}
