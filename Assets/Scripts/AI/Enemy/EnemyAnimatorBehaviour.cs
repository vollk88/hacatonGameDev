using AI.Enemy.State;
using UnityEngine;

namespace AI.Enemy
{
    public class EnemyAnimatorBehaviour : UnitAnimatorBehaviour
    {
        protected Enemy Enemy { get; private set; }

        private static readonly int AttackID = Animator.StringToHash("IsAttack");

        void attackStateOnOnAttack(bool b) => Enemy.Animator.SetBool(AttackID, b);
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator,stateInfo,layerIndex);
            if (Enemy is not null) return;

            Enemy = Unit as Enemy;
            if (Enemy != null)
            {
                AttackState attackState = Enemy.StateMachine.GetState<AttackState>();
                attackState.OnAttack += (attackStateOnOnAttack);
            }
        }
    }
}
