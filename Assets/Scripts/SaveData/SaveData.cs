using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 PlayerPos;
    public int LastMapTransitionIndex;
    public string MapBoundary;
    public List<InventorySaveData> InventorySaveData;
}
