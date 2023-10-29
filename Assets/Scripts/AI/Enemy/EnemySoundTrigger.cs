using UnityEngine;

namespace AI.Enemy
{
    public class EnemySoundTrigger : MonoBehaviour
    {
        private bool CanTrigger { get; set; }
        
        private void OnCollisionEnter(Collision other)
        {
            Perception.Perception.SendToAllSoundTargets(gameObject);
            CanTrigger = false;
        }
        
        public void StartTrigger()
        {
            CanTrigger = true;
        }
    }
}
