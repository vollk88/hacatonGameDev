using UnityEngine;

namespace AI
{
	public class UnitAnimatorBehaviour : StateMachineBehaviour
	{
		
        protected AUnit Unit { get; private set; }

        private static readonly int SpeedID = Animator.StringToHash("Speed");
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
	        if (Unit is not null) return;
        
	        Unit = animator.GetComponent<AUnit>();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(SpeedID, Unit.CurrentSpeed);
        }
    }
}
