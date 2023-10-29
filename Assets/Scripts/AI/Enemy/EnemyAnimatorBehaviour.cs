using System.Collections;
using System.Collections.Generic;
using AI.Enemy;
using AI.Enemy.State;
using UnityEngine;

public class EnemyAnimatorBehaviour : StateMachineBehaviour
{
    protected Enemy Enemy { get; private set; }

    private static readonly int SpeedID = Animator.StringToHash("Speed");
    private static readonly int AttackID = Animator.StringToHash("IsAttack");

    void attackStateOnOnAttack(bool b) => Enemy.Animator.SetBool(AttackID, b);
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Enemy is not null) return;
        
        Enemy = animator.GetComponent<Enemy>();
        AttackState attackState = Enemy.StateMachine.GetState<AttackState>();


        attackState.OnAttack += (attackStateOnOnAttack);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(SpeedID, Enemy.CurrentSpeed);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
