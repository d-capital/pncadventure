using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static TasksButton;

public class TaskBar : MonoBehaviour
{


    //public AudioSource audioSource;

    private void Start()
    {
        ClearTasks();
        SetNewTasks();
    }
    public void SetNewTasks()
    {
        ClearTasks();
        var tasksList = GameObject.FindObjectOfType<TasksButton>().GetComponent<TasksButton>().tasksList;
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

    /*public void CompleteExistingTask(int taskId)
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
        audioSource.Play();
    }*/

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
