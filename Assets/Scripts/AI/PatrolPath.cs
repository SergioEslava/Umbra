using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PatrolPath : MonoBehaviour
{
    [SerializeField] Color pathColor = Color.green;
    [SerializeField] float nodeSize = 0.2f;

    List<Transform> m_pathList;

    private void Awake()
    {
        m_pathList = transform.Cast<Transform>().ToList();
    }

    public Transform GetTargetAtIndex(int _index)
    {
        if (m_pathList == null || m_pathList.Count == 0)
            throw new System.InvalidOperationException("Path list is null or empty.");

        if (_index < 0)
            throw new System.ArgumentException(tag, "Negative index not supported.");

        return m_pathList[_index % m_pathList.Count];
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = pathColor;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform _node = transform.GetChild(i);

            // Draw node
            Gizmos.DrawSphere(_node.position, nodeSize);

            // Draw line to the next node
            if (i < transform.childCount - 1)
            {
                Transform _nextNode = transform.GetChild(i + 1);
                Gizmos.DrawLine(_node.position, _nextNode.position);
            }
        }
    }
}
