using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    private SerializedProperty nodesProperty;
    private GUIContent addButtonContent;
    private GUIContent deleteButtonContent;

    private void OnEnable()
    {
        nodesProperty = serializedObject.FindProperty("nodes");
        addButtonContent = new GUIContent("Add Node");
        deleteButtonContent = new GUIContent("Delete Node");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nodesProperty, true);

        GUILayout.Space(20);

        if (GUILayout.Button(addButtonContent))
        {
            AddNode();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void AddNode()
    {
        Dialogue dialogue = (Dialogue)target;
        dialogue.nodes = (dialogue.nodes == null) ? new DialogueNode[1] : dialogue.nodes.Append(new DialogueNode()).ToArray();
    }

    private void DeleteNode(int index)
    {
        Dialogue dialogue = (Dialogue)target;
        List<DialogueNode> nodeList = dialogue.nodes.ToList();
        nodeList.RemoveAt(index);
        dialogue.nodes = nodeList.ToArray();
    }

    private void SwapNodes(int indexA, int indexB)
    {
        Dialogue dialogue = (Dialogue)target;
        List<DialogueNode> nodeList = dialogue.nodes.ToList();

        if (indexA >= 0 && indexB >= 0 && indexA < nodeList.Count && indexB < nodeList.Count)
        {
            DialogueNode temp = nodeList[indexA];
            nodeList[indexA] = nodeList[indexB];
            nodeList[indexB] = temp;
        }

        dialogue.nodes = nodeList.ToArray();
    }
}
