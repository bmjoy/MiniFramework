using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace MiniFramework
{
    [CustomEditor(typeof(PoolComponent))]
    public class PoolComponentEditor : Editor
    {
        SerializedProperty prefabs;
        void OnEnable()
        {
            prefabs = serializedObject.FindProperty("CacheObjects");
        }

        void OnDraw()
        {
            EditorGUILayout.BeginVertical("box");
            {
                for (int i = 0; i < prefabs.arraySize; i++)
                {
                    SerializedProperty element = prefabs.GetArrayElementAtIndex(i);
                    SerializedProperty name = element.FindPropertyRelative("Name");
                    SerializedProperty prefab = element.FindPropertyRelative("Prefab");
                    SerializedProperty max = element.FindPropertyRelative("Max");
                    SerializedProperty min = element.FindPropertyRelative("Min");
                    SerializedProperty destroyOnLoad = element.FindPropertyRelative("DestroyOnLoad");
                    EditorGUILayout.BeginVertical("box");
                    {
                        if (prefab.objectReferenceValue != null)
                        {
                            name.stringValue = prefab.objectReferenceValue.name;
                        }
                        EditorGUILayout.LabelField(name.stringValue);
                        EditorGUILayout.ObjectField(prefab, GUIContent.none);
                        max.intValue = EditorGUILayout.IntField("Max", max.intValue);
                        min.intValue = EditorGUILayout.IntField("Min", min.intValue);
                        destroyOnLoad.boolValue = EditorGUILayout.ToggleLeft("Destroy On Load", destroyOnLoad.boolValue);
                    }
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(10);
                }
                if (GUILayout.Button("+"))
                {
                    prefabs.arraySize++;
                }
                if (GUILayout.Button("-"))
                {
                    prefabs.arraySize--;
                }
            }
            EditorGUILayout.EndVertical();
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            OnDraw();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

