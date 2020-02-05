using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(TutorialManager))]
public class TutorialManagerEditor : Editor
{
    private ReorderableList dialogList;
    private ReorderableList advanceList;

    private void OnEnable()
    {
        dialogList = new ReorderableList(serializedObject, serializedObject.FindProperty("dialog"), true, true, true, true);
        dialogList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Tutorial Dialog");
        };
        dialogList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = dialogList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, element, new GUIContent());
        };


        advanceList = new ReorderableList(serializedObject, serializedObject.FindProperty("advanceWords"), true, true, true, true);
        advanceList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Advance Keywords");
        };
        advanceList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = advanceList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, element, new GUIContent());
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        dialogList.DoLayoutList();
        advanceList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
