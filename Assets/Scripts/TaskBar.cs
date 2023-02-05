using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TaskBar : MonoBehaviour
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

    TasksList tasksList;

    private void Start()
    {
        ClearTasks();
        SetNewTasks();
    }
    public void SetNewTasks()
    {
        string path = Application.dataPath;
        string levelName = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().GetCurrentLevel();
        string pathToDialogue = path + "/StreamingAssets/TasksStorage/" + levelName + ".json";
        var jsonString = System.IO.File.ReadAllText(pathToDialogue);
        tasksList = JsonConvert.DeserializeObject<TasksList>(jsonString);
        Transform Grid = FindObjectOfType<TaskGridControl>().transform;
        foreach (Task i in tasksList.Tasks)
        {
            TaskLine taskLine = Resources.Load<TaskLine>("Task");
            TaskLine hello = Instantiate<TaskLine>(taskLine, new Vector3(0, 0, 0), Quaternion.identity, Grid);
            hello.checkBox.isOn = i.State;
            hello.content.text = i.Content;
            hello.id = i.id;
        }
    }

    void ClearTasks()
    {
        TaskGridControl[] tasksGrids = Resources.FindObjectsOfTypeAll<TaskGridControl>();
        foreach (TaskGridControl i in tasksGrids)
        {
            i.removeAllTasks();
        }
    }

    public void CompleteExistingTask(int taskId)
    {
        TaskLine[] taskLines = Resources.FindObjectsOfTypeAll<TaskLine>();
        foreach (TaskLine i in taskLines)
        {
            if (i.id == taskId)
            {
                i.checkBox.isOn = true;
                break;
            }
        }
        //TODO: Play some sound
    }

    public void CloseTaskList()
    {
        int sawTasksCounter = GameObject.FindObjectOfType<TasksButton>().sawTasksCounter;
        GameObject.FindObjectOfType<TaskBar>().gameObject.SetActive(false);
        GameObject.FindObjectOfType<TasksButton>().areTasksOpen = false;
        UnblockInventory();
        if (sawTasksCounter >= 1 && GameObject.FindObjectOfType<newIconControll>())
        {
            GameObject.FindObjectOfType<newIconControll>().gameObject.SetActive(false);
        }
    }

    public void UnblockInventory()
    {
        GameObject.FindObjectOfType<InventoryUI>().canOpenInventory = true;
    }

}
