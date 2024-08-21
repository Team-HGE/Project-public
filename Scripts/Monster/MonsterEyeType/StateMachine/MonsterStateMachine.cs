using UnityEngine;
public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    // ���� ���, �÷��̾�
    public GameObject Target { get; private set; }
    // ���� ���� ��ġ
    public Vector3 StartPosition { get; private set; }

    // ���� ����
    public MonsterIdleState IdleState { get; private set; }
    public MonsterPatrolState PatrolState { get; private set; }
    public MonsterFindState FindState { get; private set; }
    public MonsterChaseState ChaseState { get; private set; }
    public MonsterLoseSightState LoseSightState { get; private set; }
    public MonsterComeBackState ComBackState { get; private set; }
    public MonsterAttackState AttackState { get; private set; }

    // ����
    public bool IsChasing { get; set; }
    public bool IsPatrol { get; set; }
    public bool IsIdle { get; set; }
    public bool IsAttack { get; set; }
    public bool IsFind { get; set; }
    public bool IsComeBack { get; set; }

    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }

    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player");

        // ������ ��ġ
        StartPosition = Monster.transform.position;

        IdleState = new MonsterIdleState(this);
        PatrolState = new MonsterPatrolState(this);
        FindState = new MonsterFindState(this);
        ChaseState = new MonsterChaseState(this);
        LoseSightState = new MonsterLoseSightState(this);
        ComBackState = new MonsterComeBackState(this);
        AttackState = new MonsterAttackState(this);

        MovementSpeed = Monster.Data.GroundData.BaseSpeed;
        RotationDamping = Monster.Data.GroundData.BaseRotationDamping;
    }
}
