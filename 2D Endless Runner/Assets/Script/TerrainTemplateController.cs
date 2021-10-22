using UnityEngine;

public class TerrainTemplateController : MonoBehaviour
{
    private const float debugLineHight = 10.0f;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * debugLineHight / 2,
            transform.position + Vector3.down * debugLineHight / 2, Color.green);
    }
}
