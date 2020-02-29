using UnityEngine;

public class DG_EmptyCell : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.one);
    }
}
