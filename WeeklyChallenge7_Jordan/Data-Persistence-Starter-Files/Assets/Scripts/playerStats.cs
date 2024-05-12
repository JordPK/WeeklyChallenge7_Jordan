using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class playerStats : MonoBehaviour
{
    public static playerStats Instance {  get; private set; }

    public TMP_InputField playerNameInput;
    public string playerName;
    public int highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
    class saveData
    {
        public int highScore;
        public string playerName;
    }

    public void SaveHighScore() 
    {
        playerName = playerNameInput.text;

        saveData data = new saveData();
        data.highScore = highScore;
        data.playerName = playerName;
        
        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
        Debug.Log(Application.persistentDataPath + "/savefile.json");
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData data = JsonUtility.FromJson<saveData>(json);

            highScore = data.highScore;
            playerName = data.playerName;
        }
    }
}
