using System.Collections.Generic;
using BaseClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI
{
    public class PatrolPoint : CustomBehaviour
    {
        [SerializeField]
        private List<PatrolPoint> nextPoints;
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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
