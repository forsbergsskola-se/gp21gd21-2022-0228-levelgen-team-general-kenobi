using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Handles the scanning of the surrounding area of worldtiles.
/// </summary>
public class WorldTilePositionScanner : MonoBehaviour
{
    private readonly Vector2Int[] worldTileDirections = { Vector2Int.down, Vector2Int.left, Vector2Int.up, Vector2Int.right };
    private readonly Dictionary<Vector2Int, WorldTile> loadedWorldTiles = new Dictionary<Vector2Int, WorldTile>();
    private WorldTileCreator kenobiWorldTileCreator;



    private void Awake() 
    {
        kenobiWorldTileCreator = GetComponent<WorldTileCreator>();
    }

    
    
    /// <summary>
    /// Search each world tile position two positions out around the center position. If a position is empty a call is made to populate that position. 
    /// </summary>
    /// <param name="centerPosition">Center position of scan</param>
    public void Scan(Vector2Int centerPosition)
    {
        // Debug.Log("Scanning");
        Vector2Int searchPosition = centerPosition + Vector2Int.up * 2 + Vector2Int.right * 2;
        foreach (var directionVector in worldTileDirections)
        {
            for (var i = 0; i < 4; i++)
            {
                searchPosition += directionVector;
                if (!loadedWorldTiles.ContainsKey(searchPosition))
                {
                    loadedWorldTiles.Add(searchPosition, kenobiWorldTileCreator.CreateNewWorldTile(searchPosition));
                }
            }
        }
    }
}
