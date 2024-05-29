using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public int mapWidth;
    public int mapHeight;
    public int roomCount = 0;
    public Vector2 roomSizeMin;
    public Vector2 roomSizeMax;
    PoissonDiscSampler poissonDiscSampler;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Generate a tour of rooms with it's size, start position and exit position,
    /// and return the list of rooms.
    /// </summary>
    /// <param name="startPos">Initial pos</param>
    /// <param name="exitPos">Way out</param>
    /// <param name="minDistances">The intersection hall way minimum distance</param>
    /// <param name="listRoomSize"> List of ordered room size demanded</param>
    /// <returns></returns>
    // public List<(Vector2Int,Vector2Int)> GenerateRooms(int startPos, int exitPos,int minDistances, List<Vector2Int> listOrderRoomSize)
    // {

    // }
    // // Function to check if a point is valid (within minDistance of existing points)
    // bool IsValidPoint(Vector2 point)
    // {
    //     foreach (Vector2 existingPoint in activeList)
    //     {
    //         if (Vector2.Distance(point, existingPoint) < minDistance)
    //         {
    //             return false;
    //         }
    //     }
    //     return true;
    // }

    //Draw Gizmo blueprint
    private void OnDrawGizmos()
    {

    }
}
