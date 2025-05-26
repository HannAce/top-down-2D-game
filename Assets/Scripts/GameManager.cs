using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string m_saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Define save location
        m_saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData()
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position
        };
        
        File.WriteAllText(m_saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game saved: " + m_saveLocation);
    }

    public void LoadGame()
    {
        if (File.Exists(m_saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(m_saveLocation));
            
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.PlayerPos;
        }
        else
        {
            SaveGame();
        }
    }
}
