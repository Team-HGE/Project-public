﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchState : PlayerGroundState
{
    public AudioManager audioManager;

    public PlayerCrouchState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("앉기");
        stateMachine.Player.transform.localScale = new Vector3(stateMachine.Player.transform.localScale.x, groundData.CrouchHeight, stateMachine.Player.transform.localScale.z);
        stateMachine.MovementSpeedModifier = groundData.CrouchSpeedModifier;
        stateMachine.Player.SumNoiseAmount = 2f;
        stateMachine.IsCrouch = true;
        AudioManager.Instance.PlaySoundEffect(SoundEffect.duck); // 권용 수정
    }

    public override void Exit() 
    {
        base.Exit();
        stateMachine.Player.transform.localScale = new Vector3(stateMachine.Player.transform.localScale.x, stateMachine.OriginHeight, stateMachine.Player.transform.localScale.z);
        stateMachine.IsCrouch = false;

    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        base.OnCrouchCanceled(context);

        if (stateMachine.Player.InputsData.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            AudioManager.Instance.PlaySoundEffect(SoundEffect.duck); // 권용 수정
            

            return;
        }

        if (stateMachine.PressShift)
        {
            stateMachine.ChangeState(stateMachine.RunState);
            AudioManager.Instance.PlaySoundEffect(SoundEffect.duck); // 권용 수정
            
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
