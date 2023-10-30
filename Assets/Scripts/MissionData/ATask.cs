using System;
using Cooking;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace MissionData
{
	public enum ETaskType
	{
		ItemCollection,
		Cooking,
	}


	public class ATask
	{
		public string Name;
		public string Description;
		public uint Quantity;
		public uint CurrentProgress;
		public ETaskStatus Status;

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

		[JsonConstructor]
		public ATask(string name, string description, uint quantity, uint currentProgress, ETaskStatus status)
		{
			Name = name;
			Description = description;
			Quantity = quantity;
			CurrentProgress = currentProgress;
			Status = status;
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