using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BaseClasses;
using UI;

namespace MissionData
{
    public class TaskManager : CustomBehaviour
    {
        [SerializeField]
        private TaskList taskList;

        private UIManager _uiManager;
        public static List<ATask> Tasks { get; } = new ();
        
        private static event Action UIUpdateEvent; 


        private void Start()
        {
            _uiManager = (UIManager)Instances[typeof(UIManager)][0];
            AddTasks(taskList);
            UIUpdateEvent += UIUpdate;
            
            if (Tasks.Count == 0)
            {
                Debug.Log("No tasks to complete!");
                return;
            }
            
            Debug.Log("Starting first task: " + Tasks.First().Name);
            Tasks.First().StartTask();
            
            OnUIUpdateEvent();
        }

        private void AddTasks(TaskList getTaskList)
        {
            foreach (TaskData taskData in getTaskList.tasks)
            {
                ATask aTask = taskData.type switch
                {
                    ETaskType.ItemCollection => new InventoryTask(taskData.name, taskData.description,
                        taskData.quantity, taskData.item),
                    ETaskType.Cooking => new CookingTask(taskData.name, taskData.description, taskData.quantity,
                        taskData.recipe),
                    _ => null
                };

                if (aTask != null)
                {
                    AddTask(aTask);
                }
            }
        }
        
        private static void AddTask(ATask aTask)
        {
            aTask.OnTaskCompleted += TaskCompletedHandler;
            aTask.OnTaskProgressUpdated += TaskProgressUpdatedHandler;
            Tasks.Add(aTask);
        }

        private static void TaskProgressUpdatedHandler(ATask obj)
        {
            OnUIUpdateEvent();
        }

        private static void TaskCompletedHandler(ATask aTask)
        {
            Debug.Log("Task Completed: " + aTask.Name);
            
            Tasks.Remove(aTask);
            
            if (Tasks.Count == 0)
            {
                Debug.Log("All tasks completed!");
                GameStateEvents.GameEnded?.Invoke();
            }
            else
            {
                Debug.Log("Starting next task: " + Tasks.First().Name);
                Tasks.First().StartTask();
            }
        }

        private void UIUpdate()
        {
            foreach (ATask task in Tasks.Where(task => task.Status == ETaskStatus.InProgress))
            {
                _uiManager.SeeTasks.SetTasks(task.Description, task.CurrentProgress, task.Quantity);
            }
        }

        private static void OnUIUpdateEvent()
        {
            UIUpdateEvent?.Invoke();
        }
    }
}