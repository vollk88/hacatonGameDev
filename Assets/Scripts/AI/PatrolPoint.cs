using System.Collections.Generic;
using BaseClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI
{
    public class PatrolPoint : CustomBehaviour
    {
        #if UNITY_EDITOR
        
        [SerializeField]
        private bool _showGizmos = true;
        #endif
        
        
        [SerializeField]
        private List<PatrolPoint> nextPoints;
        
        [SerializeField]
        private float _radiusFindNextPoints = 5f;
        public float timeToStay = 1f;

        protected override void OnEnable()
        {
            base.OnEnable();
            PatrolPull.PatrolPoints.Add(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PatrolPull.PatrolPoints.Remove(this);
        }
        
        public PatrolPoint GetNextPoint()
        {
            return nextPoints[Random.Range(0, nextPoints.Count)];
        }

        void Start()
        {
            foreach (var point in PatrolPull.PatrolPoints)
            {
                if (point == this || nextPoints.Contains(point))
                    continue;
                
                float distance = Vector3.Distance(transform.position, point.transform.position);
                if (distance < _radiusFindNextPoints)
                {
                    nextPoints.Add(point);
                }
                else
                {
                    //Debug.Log($"Point {point.name} is too far from {name} ({distance})");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_showGizmos)
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radiusFindNextPoints);
            
        }
        #endif
    }
}
