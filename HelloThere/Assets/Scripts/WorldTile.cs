using UnityEngine;



public class WorldTile : MonoBehaviour
{
    [SerializeField] public Vector2Int WorldTilePosition;
    [HideInInspector] public bool Visited;
}