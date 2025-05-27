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
    
    private CinemachineConfiner2D m_confiner;

    private void Awake()
    {
        m_confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Moving to map area {m_mapBoundary}");
            m_confiner.BoundingShape2D = m_mapBoundary;
            UpdatePlayerPos(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(m_wayPointCollider.bounds.center, m_wayPointCollider.bounds.size);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(m_teleportPos.position, 0.2f);
    }

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
