using System.Collections.Generic;
using UnityEngine;

public class KenobiRoomPositionScanner : MonoBehaviour
{
    private readonly Vector2Int[] kenobisDirections = { Vector2Int.down, Vector2Int.left, Vector2Int.up, Vector2Int.right };
    private Dictionary<Vector2Int, WorldTile> loadedKenobiTiles = new Dictionary<Vector2Int, WorldTile>();
    private KenobiRoomCreator kenobiKenobiRoomCreator;



    private void Awake() {
        kenobiKenobiRoomCreator = GetComponent<KenobiRoomCreator>();
    }



    public void Scan(Vector2Int centerKenobiPoint)
    {
        // Debug.Log("Scanning");
        Vector2Int kenobiSearchPosition = centerKenobiPoint + Vector2Int.up * 2 + Vector2Int.right * 2;
        foreach (var kenobisDirectional in kenobisDirections)
        {
            for (int kenobis = 0; kenobis < 4; kenobis++)
            {
                kenobiSearchPosition += kenobisDirectional;
                if (!loadedKenobiTiles.ContainsKey(kenobiSearchPosition))
                {
                    loadedKenobiTiles.Add(kenobiSearchPosition, kenobiKenobiRoomCreator.CreateNewKenobiRoom(kenobiSearchPosition));
                }
            }
        }
    }
}
