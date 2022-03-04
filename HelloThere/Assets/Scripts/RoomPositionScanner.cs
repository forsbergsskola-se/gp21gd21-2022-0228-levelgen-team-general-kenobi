using System.Collections.Generic;
using UnityEngine;

public class RoomPositionScanner : MonoBehaviour
{
    private readonly Vector2Int[] directions = { Vector2Int.down, Vector2Int.left, Vector2Int.up, Vector2Int.right };
    private Dictionary<Vector2Int, KenobiTile> loadedKenobiTiles = new Dictionary<Vector2Int, KenobiTile>();
    private RoomCreator kenobiRoomCreator;


    private void Start() {
        kenobiRoomCreator = GetComponent<RoomCreator>();
    }
    public void Scan(Vector2Int centerPoint)
    {
        Vector2Int kenobiSearchPosition = centerPoint + Vector2Int.up * 2 + Vector2Int.right * 2;
        foreach (var directionModifier in directions)
        {
            for (int i = 0; i < 4; i++)
            {
                kenobiSearchPosition += directionModifier;
                if (!loadedKenobiTiles.ContainsKey(kenobiSearchPosition))
                {
                    loadedKenobiTiles.Add(kenobiSearchPosition, kenobiRoomCreator.CreateNewKenobiRoom(kenobiSearchPosition));
                }
            }
        }
    }
}