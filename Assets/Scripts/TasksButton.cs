using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksButton : MonoBehaviour
{
    public int sawTasksCounter = 0;
    public bool canOpenTaskList = true;
    public bool areTasksOpen = false;
    public void OpenTaskList()
    {
        if (canOpenTaskList)
        {
            TaskBar[] taskBarsList = Resources.FindObjectsOfTypeAll<TaskBar>();
            foreach (TaskBar i in taskBarsList)
            {
                i.gameObject.SetActive(true);
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
}
