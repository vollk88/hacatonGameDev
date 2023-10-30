using System;
using Cooking;
using UnityEngine;
using UnityEngine.Serialization;

namespace MissionData
{
	public enum ETaskType
	{
		ItemCollection,
		Cooking,
	}


	public abstract class ATask
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public uint Quantity { get; private set; }
		public uint CurrentProgress { get; private set; }
		public ETaskStatus Status { get; private set; }

		// Declare an event for task completion
		public event Action<ATask> OnTaskCompleted;
		public event Action<ATask> OnTaskProgressUpdated;

		protected ATask(string name, string description, uint quantity)
		{
			Name = name;
			Description = description;
			Quantity = quantity;
			Status = ETaskStatus.NotStarted;
		}

		public virtual void StartTask()
		{
			Status = ETaskStatus.InProgress;
		}
		
		public void UpdateProgress(uint progress)
		{
			CurrentProgress = progress;

			if (CurrentProgress >= Quantity)
			{
				CurrentProgress = Quantity;
				Status = ETaskStatus.Completed;
				OnTaskCompleted?.Invoke(this);
			}
			else
			{
				Status = ETaskStatus.InProgress;
			}
		}
		
		public void UpdateProgressIncrement()
		{
			CurrentProgress++;

			if (CurrentProgress >= Quantity)
			{
				CurrentProgress = Quantity;
				Status = ETaskStatus.Completed;
				OnTaskCompleted?.Invoke(this);
			}
			else
			{
				Status = ETaskStatus.InProgress;
			}
		}
		
		protected virtual void OnTaskProgressUpdatedHandler()
		{
			OnTaskProgressUpdated?.Invoke(this);
		}
	}
}