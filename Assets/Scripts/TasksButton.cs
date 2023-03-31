using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TasksButton : MonoBehaviour
{

    [System.Serializable]
    public class Task
    {
        public int id { get; set; }
        public string Content { get; set; }
        public bool State { get; set; }
    }

    [System.Serializable]
    public class TasksList
    {
        public List<Task> Tasks { get; set; }
    }

    public TasksList tasksList;

    public int sawTasksCounter = 0;
    public bool canOpenTaskList = true;
    public bool areTasksOpen = false;

    public AudioSource audioSource;

    private void Start()
    {
        if (tasksList.Tasks != null)
        {
            tasksList.Tasks.Clear();
        }
        SetNewTasks();
    }
    public void OpenTaskList()
    {
        if (canOpenTaskList)
        {
            TaskBar[] taskBarsList = Resources.FindObjectsOfTypeAll<TaskBar>();
            foreach (TaskBar i in taskBarsList)
            {
                i.gameObject.SetActive(true);
                i.SetNewTasks();
            }
            sawTasksCounter += 1;
            areTasksOpen = true;
            BlockInventory();
        }

    }

    public void BlockInventory()
    {
        if (GameObject.FindObjectOfType<InventoryUI>().isOpened)
        {
            GameObject.FindObjectOfType<InventoryUI>().OpenCloseInventory();
        }
        GameObject.FindObjectOfType<InventoryUI>().canOpenInventory = false;
    }

    public void SetNewTasks()
    {
        string path = Application.dataPath;
        string levelName = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().GetCurrentLevel();
        string currLanguage = GameObject.FindObjectOfType<LanguageManager>().language;
        string pathToTasks = path + "/StreamingAssets/TasksStorage/" +currLanguage+"/" + levelName + ".json";
        var jsonString = System.IO.File.ReadAllText(pathToTasks);
        tasksList = JsonConvert.DeserializeObject<TasksList>(jsonString);
    }

    public void CompleteTask(int taskId)
    {
        foreach(Task i in tasksList.Tasks)
        {
            if(i.id == taskId)
            {
                i.State = true;
            }
        }
        audioSource.Play();
        Debug.Log(tasksList);
    }
}
