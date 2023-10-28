namespace AI
{
	public interface IPatrolActor
	{
		public bool OnPatrol { get; set; }
		public void SetPatrolPoint(PatrolPoint point, float timeToStay = 0f);
	}
}