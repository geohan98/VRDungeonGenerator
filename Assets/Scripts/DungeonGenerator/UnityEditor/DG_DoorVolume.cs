using UnityEngine;

[SelectionBase]
public class DG_DoorVolume : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.one);
    }
}
