using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CallScan : UnityEvent<Vector2Int>
{
}



public class PlayerKenobiTileScanner : MonoBehaviour
{
    [HideInInspector] private KenobiTile currentKenobiTile;

    [SerializeField] private float kenobiScanningFrequency;

    private Transform playerKenobiTransform;
    private float kenobiScanDelay;


    public CallScan callScan;

    void Awake()
    {
        kenobiScanDelay = 1 / kenobiScanningFrequency;
        playerKenobiTransform = gameObject.transform;

        StartCoroutine(UpdateCurrentKenobiTileOnTimer());
    }




    private IEnumerator UpdateCurrentKenobiTileOnTimer()
    {
        while (true)
        {
            if (Physics.Raycast(playerKenobiTransform.position, Vector3.down, out RaycastHit kenobiHit, 30, LayerMask.GetMask("Ground")))
            {
                Debug.Log(kenobiHit.collider.gameObject.name);
                var kenobiTile = kenobiHit.collider.gameObject.GetComponent<KenobiTile>();

                if (kenobiTile == null)
                {
                    throw new System.Exception("WHAT DID YOU COLLIDE WITH THAT ISN'T A TILE! SO UNCIVILIZED!");
                }

                if (!kenobiTile.Visited)
                {
                    callScan.Invoke(kenobiTile.KenobiTilePosition);
                    kenobiTile.Visited = true;
                }
            }

            yield return new WaitForSeconds(kenobiScanDelay);
        }
    }
}
