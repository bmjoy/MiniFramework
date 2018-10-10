using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace MiniFramework
{
    [CustomEditor(typeof(ObjectPool))]
    public class ObjectPoolEditor : Editor
    {
        ReorderableList reorderableList;

        void OnEnable()
        {
            SerializedProperty prop = serializedObject.FindProperty("NeedCachePrefabs");

            reorderableList = new ReorderableList(serializedObject, prop, true, true, true, true);

            reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
                rect.y += 2;
                SerializedProperty element = prop.GetArrayElementAtIndex(index);               
                SerializedProperty obj = element.FindPropertyRelative("Obj");
                SerializedProperty max = element.FindPropertyRelative("Max");
                SerializedProperty min = element.FindPropertyRelative("Min");
                SerializedProperty destroyOnLoad = element.FindPropertyRelative("DestroyOnLoad");
                EditorGUI.PropertyField(new Rect(rect.x,rect.y, 100, EditorGUIUtility.singleLineHeight), obj,GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 110, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), "Max");
                EditorGUI.PropertyField(new Rect(rect.x+140, rect.y, 30, EditorGUIUtility.singleLineHeight), max,GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 180, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), "Min");
                EditorGUI.PropertyField(new Rect(rect.x + 210, rect.y, 30, EditorGUIUtility.singleLineHeight), min, GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 250, rect.y, EditorGUIUtility.labelWidth,EditorGUIUtility.singleLineHeight),"DestoryOnLoad");
                EditorGUI.PropertyField(new Rect(rect.x + 350, rect.y, 20, EditorGUIUtility.singleLineHeight), destroyOnLoad,GUIContent.none);
            };
            reorderableList.drawHeaderCallback = (rect) =>
                EditorGUI.LabelField(rect, prop.displayName);

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

