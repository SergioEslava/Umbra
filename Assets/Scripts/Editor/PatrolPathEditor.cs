using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolPath))]
public class PatrolPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PatrolPath _path = (PatrolPath)target;

        if (GUILayout.Button("Add Node"))
        {
            AddNode(_path);
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            ClearNodes(_path);
        }
    }

    private void AddNode(PatrolPath _path)
    {
        GameObject _newNode = new GameObject("Node " + _path.transform.childCount);
        _newNode.transform.parent = _path.transform;

        if (_path.transform.childCount > 1)
        {
            // Creating the new node near to the last node
            Transform lastNode = _path.transform.GetChild(_path.transform.childCount - 2);
            _newNode.transform.position = lastNode.position + Vector3.right;
        }
        else
        {
            _newNode.transform.position = _path.transform.position;
        }

        Undo.RegisterCreatedObjectUndo(_newNode, "Add Patrol Node");
    }

    private void ClearNodes(PatrolPath _path)
    {
        for (int i = _path.transform.childCount - 1; i >= 0; i--)
        {
            Undo.DestroyObjectImmediate(_path.transform.GetChild(i).gameObject);
        }
    }
}