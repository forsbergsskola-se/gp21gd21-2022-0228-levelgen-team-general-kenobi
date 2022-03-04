using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject kenobiRoomPrefab;
    [SerializeField] private int kenobiRoomSize;



    public KenobiTile CreateNewKenobiRoom(Vector2Int spawnPosition)
    {
        var kenobiApartment = Instantiate(kenobiRoomPrefab, KenobiTileToWorldPosition(spawnPosition), Quaternion.identity);
        return kenobiApartment.GetComponent<KenobiTile>();
    }

    private Vector3 KenobiTileToWorldPosition(Vector2Int kenobiTilePosition) {
        var temp = kenobiTilePosition * kenobiRoomSize;
        return new Vector3(temp.x, 0, temp.y);
    }
}
