using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

// enum Direction
// {
//     Up,
//     Down,
//     Left,
//     Right
// }

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D m_mapBoundary;
    [SerializeField] private BoxCollider2D m_wayPointCollider;
    [SerializeField] private Transform m_teleportPos;
    //[SerializeField] private Direction m_direction;
    //[SerializeField] private float m_additivePos;
    
    private GameManager m_gameManager;
    private CinemachineConfiner2D m_confiner;

    private void Awake()
    {
        m_confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void Start()
    {
        m_gameManager = GameManager.Instance;
    }

    /// <summary>
    /// Checks when player enters the trigger for entering a new area, then switches to that map boundary
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Moving to map area {m_mapBoundary}");
            SetMapBoundary();
            m_gameManager.UpdateMapTransition(this); // gets this map transition (which instance this script is attached to)
            UpdatePlayerPos(other.gameObject);
        }
    }

    public void SetMapBoundary()
    {
        m_confiner.BoundingShape2D = m_mapBoundary;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(m_mapBoundary.bounds.center, m_mapBoundary.bounds.size); // Draw around the boundary of the map to make it more visible in scene
        Gizmos.color = Color.red;
        Gizmos.DrawCube(m_wayPointCollider.bounds.center, m_wayPointCollider.bounds.size); // Draw the waypoint collider to trigger new map area to make it more visible in scene
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(m_teleportPos.position, 0.2f); // Draw spawn point that the player will appear at after entering that map area to make it more visible in scene
    }

    /// <summary>
    /// Updates the player's position to where they should appear after entering that map area
    /// </summary>
    /// <param name="player"></param>
    private void UpdatePlayerPos(GameObject player)
    {
        Vector3 newPos = m_teleportPos.position;

        // switch (m_direction)
        // {
        //     case Direction.Up:
        //         newPos.y += m_additivePos;
        //         break;
        //     case Direction.Down:
        //         newPos.y -= m_additivePos;
        //         break;
        //     case Direction.Left:
        //         newPos.x -= m_additivePos;
        //         break;
        //     case Direction.Right:
        //         newPos.x += m_additivePos;
        //         break;
        //     default:
        //         Debug.LogError("Invalid Direction!");
        //         break;
        //}
        
        player.transform.position = newPos;
    }
}
