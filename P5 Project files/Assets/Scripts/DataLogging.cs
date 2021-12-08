using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataLogging : MonoBehaviour
{
    private List<string> objectNames = new List<string>();
    [HideInInspector] public List<bool> completed = new List<bool>();
    
    private List<string> playerPosition = new List<string>();
    private List<string> playerRotation = new List<string>();
    private List<string> timeLogs = new List<string>();
    
    private int completeCount;

    private void Start()
    {
        print(Application.persistentDataPath);
        LogTimeAndObject("Test started");
        
        // Read collider names from text files and append them to object name list
        var lines = File.ReadAllLines("Assets/colliders.txt");
        foreach (var line in lines)
        {
            objectNames.Add(line);
        }

        // Initialize boolean list with 10 complete booleans 
        for (int i = 0; i < 10; i++)
        {
            completed.Add(false);
        }
        
        // Log where the player is
        InvokeRepeating(nameof(PlayerPosition), 1f, 1f);
        InvokeRepeating(nameof(PlayerRotation), 1f, 1f);
    }

    private void OnTriggerEnter(Collider col)
    {
        foreach (var objectName in objectNames.Where(objectName => objectName == col.gameObject.name))
        {
            if (objectName.Contains("True"))
            {
                completed[completeCount] = true;
                completeCount++;
            }

            if (objectName.Contains("Cube"))
            {
                LogTimeAndObject("Maze Completed");
                SendToFile(timeLogs);
                SendToFile(playerPosition);
                SendToFile(playerRotation);
            }
                
            LogTimeAndObject(objectName);
            GameObject.Find(objectName).GetComponent<Collider>().enabled = false;
        }
    }

    private void PlayerPosition()
    {
        var whatTime = DateTime.Now.ToLongTimeString();
        var position = gameObject.transform.position;
        var jsonString = "{\n\"time\":\""+whatTime+"\","+
                         "\n\"positions\": \n{\n"+
                         "\"x\":"+position.x.ToString(CultureInfo.InvariantCulture)+",\n"
                         +"\"y\":"+position.y.ToString(CultureInfo.InvariantCulture)+",\n"
                         +"\"z\":"+position.z.ToString(CultureInfo.InvariantCulture)+"\n}"
                         +"\n},";
        playerPosition.Add(jsonString);
    }
    
    private void PlayerRotation()
    {
        var whatTime = DateTime.Now.ToLongTimeString();
        var rotation = gameObject.transform.rotation.eulerAngles;
        var jsonString = "{\n\"time\":\""+whatTime+"\","+
                         "\n\"rotations\": \n{\n"+
                         "\"x\":"+rotation.x.ToString(CultureInfo.InvariantCulture)+",\n"
                         +"\"y\":"+rotation.y.ToString(CultureInfo.InvariantCulture)+",\n"
                         +"\"z\":"+rotation.z.ToString(CultureInfo.InvariantCulture)+"\n}"
                         +"\n},";
        playerRotation.Add(jsonString);
    }


    private void LogTimeAndObject(string objectName)
    {
        var whatTime = DateTime.Now.ToLongTimeString();
        var jsonString = "{\n\"time\":" + whatTime + "\",\n"
                         +"\"object\":\""+objectName+"\"\n},";
        timeLogs.Add(jsonString);
    }

    private void SendToFile(List<string> data)
    {
        string filename = Regex.Replace(DateTime.Now.ToString(CultureInfo.InvariantCulture), "[^A-Za-z0-9 -]", "") + Random.Range(0, 1000);
        string path = Application.persistentDataPath + "/" + filename + ".json";

        string dataString = string.Join("\n", data);
        string newDataString = dataString.Remove(dataString.Length-1); // Trim the last "," char at the end to prevent JSONDecodeError

        File.WriteAllText(path, "[\n"+newDataString+"\n]");

        Debug.Log("Data is saved");
    }
}
