using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace MiniFramework
{
    [CustomEditor(typeof(PoolComponent))]
    public class PoolComponentEditor : Editor
    {
        ReorderableList reorderableList;
        SerializedProperty prefabs;
        SerializedProperty defaultMaxValue;
        SerializedProperty min;
        void OnEnable()
        {
            prefabs = serializedObject.FindProperty("CacheObjects");
        }

        void OnDraw()
        {
            EditorGUILayout.BeginVertical("box");
            {
                for (int i = 0; i < prefabs.CountInProperty(); i++)
                {
                    SerializedProperty element = prefabs.GetArrayElementAtIndex(i);

                  //  SerializedProperty objs = element.FindPropertyRelative("Objs");
                    SerializedProperty max = element.FindPropertyRelative("Max");
                    SerializedProperty min = element.FindPropertyRelative("Min");
                    SerializedProperty destroyOnLoad = element.FindPropertyRelative("DestroyOnLoad");
                    EditorGUILayout.BeginVertical("box");
                    {
                        //EditorGUILayout.ObjectField(obj, GUIContent.none);
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
           // OnDraw();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

