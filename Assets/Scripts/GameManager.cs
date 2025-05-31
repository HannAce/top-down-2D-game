using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviourSingleton<SaveManager>
{
    [SerializeField] private List<MapTransition> m_mapTransitions = new();
    private MapTransition m_lastMapTransition;
    private string m_saveLocation;
    
    void Start()
    {
        //Define save location
        m_saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        LoadGame();
    }

    public void SaveGame()
    {
        // Gets the index of the last map transition to save
        int lastMapTransitionIndex = m_mapTransitions.IndexOf(m_lastMapTransition);
        if (lastMapTransitionIndex < 0)
        {
            Debug.Log("No map transition found.");
        }
        
        SaveData saveData = new SaveData()
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position,
            LastMapTransitionIndex = lastMapTransitionIndex,
        };
        
        File.WriteAllText(m_saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game saved: " + m_saveLocation);
    }

    public void LoadGame()
    {
        if (File.Exists(m_saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(m_saveLocation));

            // Check if player has gone through transition already before saving
            if (saveData.LastMapTransitionIndex >= 0)
            {
                // Retrieving the index of my map index the player was in when they saved and storing it
                m_lastMapTransition = m_mapTransitions[saveData.LastMapTransitionIndex];
                // Setting the map boundary to the stored index one so the camera loads in the same area
                m_lastMapTransition.SetMapBoundary();
            }

            // TODO swap player to singleton
            // setting player position to where they were when they saved
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.PlayerPos;
        }
        else
        {
            SaveGame();
        }
    }

    public void ClearSaveData()
    {
        if (!File.Exists(m_saveLocation))
        {
            Debug.Log("Unable to delete save data.");
            return;
        }

        File.Delete(m_saveLocation);
        Debug.Log("Save data deleted.");
    }

    public void UpdateMapTransition(MapTransition mapTransition)
    {
        m_lastMapTransition = mapTransition;
    }
}
