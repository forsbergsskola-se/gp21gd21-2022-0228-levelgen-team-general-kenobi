using UnityEngine;
using UnityEngine.Events;
using System.Collections;



/// <summary>
/// Unity event to call scanning at specified position.
/// </summary>
[System.Serializable]
public class CallScan : UnityEvent<Vector2Int>
{
}



/// <summary>
/// Handles finding the current tile the player is on, and calls to generate new world tiles.
/// </summary>
public class PlayerWorldTileGetter : MonoBehaviour
{
    public CallScan CallScan;
    
    [SerializeField] private float updateFrequency;
    
    private WorldTile currentWorldTile;
    private Transform playerTransform;
    private float waitDelay;
    

    
    void Awake()
    {
        waitDelay = 1 / updateFrequency;
        playerTransform = gameObject.transform;

        StartCoroutine(FindCurrentWorldTileOnTimer());
    }
    

    
    /// <summary>
    /// Runs as many times a second as updateFrequency, every time checking which tile the player is on, invoking the
    /// callScan event if the player enters a new tile.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private IEnumerator FindCurrentWorldTileOnTimer()
    {
        while (true)
        {
            if (Physics.Raycast(playerTransform.position, Vector3.down, out RaycastHit hit, 30, LayerMask.GetMask("Ground")))
            {
                // Debug.Log(kenobiHit.collider.gameObject.name);
                var worldTile = hit.collider.gameObject.GetComponent<WorldTile>();

                if (worldTile == null)
                {
                    throw new System.Exception("WHAT DID YOU COLLIDE WITH THAT ISN'T A WORLD-TILE! SO UNCIVILIZED!");
                }

                if (!worldTile.Visited)
                {
                    CallScan.Invoke(worldTile.WorldTilePosition);
                    worldTile.Visited = true;
                }
            }

            yield return new WaitForSeconds(waitDelay);
        }
    }
}
