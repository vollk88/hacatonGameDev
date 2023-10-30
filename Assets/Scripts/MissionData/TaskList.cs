using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace MissionData
{
	[Serializable]
	public struct TaskData
	{
		public string name;
		public string description;
		public uint quantity;
		public ETaskType type;
		public EItems item;
		public string recipe;
	}

	[CreateAssetMenu(fileName = "TaskList", menuName = "Mission/TaskList", order = 0)]
	public class TaskList : ScriptableObject
	{
		public List<TaskData> tasks = new();
	}
}