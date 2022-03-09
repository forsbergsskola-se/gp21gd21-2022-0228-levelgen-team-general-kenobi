using UnityEngine;


public class FloorTile : MonoBehaviour
{
    public bool Spawnable;

    private void OnDrawGizmos()
    {
        if (Spawnable)
        {
            Gizmos.DrawSphere(gameObject.GetComponent<Renderer>().bounds.center, 1);
        }
    }
}
