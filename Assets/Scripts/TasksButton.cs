using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

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

    public string jsonString;

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
        //string path = Application.dataPath;
        string levelName = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().GetCurrentLevel();
        string currLanguage = SaveSystem.LoadPlayer().language;
        string pathToName = Path.Combine(Application.streamingAssetsPath, "TasksStorage",currLanguage, levelName + ".json");
        GetTextFromCorrectPlaceForTasks(pathToName, tasksList);
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
    //------------------------------------------FOR TASK---------------------------------------------------------------------------------//
    public void GetTextFromCorrectPlaceForTasks(string path, TasksList tasks)
    {
        Debug.Log(path);
        if (path.Contains("://") || path.Contains(":///"))
        {
            Debug.Log("in the Url Resolver");
            StartCoroutine(GetAssetsFromUrlForTasks(path, tasks));
        }
        else
        {
            jsonString = System.IO.File.ReadAllText(path);
            tasksList = JsonConvert.DeserializeObject<TasksList>(jsonString);
        }
    }
    IEnumerator GetAssetsFromUrlForTasks(string path, TasksList tasksList)
    {
        Debug.Log("in the Unity Web Function");
        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("something went wrong with connection");
            }
            else
            {
                jsonString = ASCIIEncoding.UTF8.GetString(www.downloadHandler.data);
                tasksList = JsonConvert.DeserializeObject<TasksList>(jsonString);
                Debug.Log("here is the string " + jsonString);
                yield return jsonString;
            }
        }

    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
}
