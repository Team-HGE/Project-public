public class MonsterEarTypeGroundState : MonsterEarTypeBaseState
{
    public MonsterEarTypeGroundState(MonsterEarTypeStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �ִϸ��̼� ����
        StartAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        // �ִϸ��̼� ����
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
