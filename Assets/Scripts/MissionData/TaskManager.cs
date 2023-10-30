using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BaseClasses;
using TMPro;

namespace MissionData
{
    public class TaskManager : CustomBehaviour
    {
        [SerializeField]
        private TaskList taskList;
        
        [SerializeField]
        private TextMeshProUGUI taskListText;
        [SerializeField]
        private TextMeshProUGUI taskProgressText;

        public static List<ATask> Tasks { get; } = new ();
        
        private static event Action UIUpdateEvent; 


        private void Start()
        {
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
            }
            else
            {
                Debug.Log("Starting next task: " + Tasks.First().Name);
                Tasks.First().StartTask();
            }
        }

        private void UIUpdate()
        {
            string taskListString = "";
            string taskProgressString = "";
            
            foreach (var task in Tasks)
            {
                if (task.Status != ETaskStatus.InProgress)
                {
                    continue;
                }
                
                taskListString = task.Description + "\n";
                taskProgressString = task.CurrentProgress + "/" + task.Quantity + "\n";
            }

            taskListText.text = taskListString;
            taskProgressText.text = taskProgressString;
        }

        private static void OnUIUpdateEvent()
        {
            UIUpdateEvent?.Invoke();
        }
    }
}