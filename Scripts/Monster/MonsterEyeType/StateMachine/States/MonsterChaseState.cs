﻿using UnityEngine.AI;

public class MonsterChaseState : MonsterGroundState
{

    public MonsterChaseState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("MonsterChaseState - 플레이어 추적 시작");
        stateMachine.IsChasing = true;
        stateMachine.Monster.Agent.isStopped = false;
        stateMachine.Monster.Agent.speed = groundData.ChaseSpeed;

        // 애니메이션 실행
        StartAnimation(stateMachine.Monster.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("MonsterChaseState - 플레이어 추적 종료");
        stateMachine.Monster.Agent.ResetPath();
        stateMachine.Monster.Agent.isStopped = true;
        stateMachine.IsChasing = false;
        // 애니메이션 종료
        StopAnimation(stateMachine.Monster.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ChaseCheck();
    }

    private void ChaseCheck()
    {
        //Debug.Log(Vector3.Distance(stateMachine.Monster.transform.position, stateMachine.Target.transform.position));

        if (!stateMachine.Monster.canSeePlayer)
        {
            //Debug.Log("플레이어 놓침");

            stateMachine.ChangeState(stateMachine.LoseSightState);
            return;
        }
        else 
        {
            Rotate(GetMovementDirection());

            //if (GetIsPlayerInFieldOfView() && IsInAttackRange())
            //{
            //    stateMachine.ChangeState(stateMachine.AttackState);
            //    return;
            //}

            if (stateMachine.Monster.Agent.pathPending) return;
            //stateMachine.Monster.Agent.SetDestination(stateMachine.Target.transform.position);

            if (NavMesh.SamplePosition(stateMachine.Target.transform.position, out NavMeshHit hit, stateMachine.Monster.Data.GroundData.PlayerChasingRange, NavMesh.AllAreas))
            {
                stateMachine.Monster.Agent.SetDestination(hit.position);

                return;
            }
        }
    }
}
