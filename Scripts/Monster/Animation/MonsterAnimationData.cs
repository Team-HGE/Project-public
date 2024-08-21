using System;
using UnityEngine;
[Serializable]
public class MonsterAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string patrolParameterName = "Patrol";
    [SerializeField] private string findParameterName = "Find";
    [SerializeField] private string chaseParameterName = "Chase";
    [SerializeField] private string loseSightParameterName = "LoseSight";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string comeBackParameterName = "ComeBack";


    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int PatrolParameterHash { get; private set; }
    public int FindParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int LoseSightParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int ComeBackParameterHash { get; private set; }


    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        PatrolParameterHash = Animator.StringToHash(patrolParameterName);
        FindParameterHash = Animator.StringToHash(findParameterName);
        ChaseParameterHash = Animator.StringToHash(chaseParameterName);
        LoseSightParameterHash = Animator.StringToHash(loseSightParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComeBackParameterHash = Animator.StringToHash(comeBackParameterName);
    }
}
