using UnityEditor;
namespace MiniFramework
{
    [CustomEditor(typeof(DrawComponent)), CanEditMultipleObjects]
    public class DrawEditor : Editor
    {
        private SerializedProperty pattern;
        private void OnEnable()
        {
            pattern = serializedObject.FindProperty("Pattern");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //if (GUILayout.Button("Add"))
            //{
            //    patterns.arraySize++;
            //}
            //if (GUILayout.Button("Delete"))
            //{
            //    patterns.arraySize--; 
            //}
            SerializedProperty type = pattern.FindPropertyRelative("PatternType");
            EditorGUILayout.PropertyField(type);
            SerializedProperty drawType = pattern.FindPropertyRelative("DrawType");
            EditorGUILayout.PropertyField(drawType);
            SerializedProperty color = pattern.FindPropertyRelative("Color");
            EditorGUILayout.PropertyField(color);
            if (type.enumValueIndex == (int)PatternType.圆形)
            {
                SerializedProperty radius = pattern.FindPropertyRelative("Radius");
                EditorGUILayout.PropertyField(radius);
            }
            if (type.enumValueIndex == (int)PatternType.扇形)
            {
                SerializedProperty radius = pattern.FindPropertyRelative("Radius");

                SerializedProperty angle = pattern.FindPropertyRelative("Angle");
                EditorGUILayout.PropertyField(radius);
                EditorGUILayout.PropertyField(angle);
            }
            if (type.enumValueIndex == (int)PatternType.矩形)
            {
                SerializedProperty length = pattern.FindPropertyRelative("Length");
                SerializedProperty width = pattern.FindPropertyRelative("Width");
                EditorGUILayout.PropertyField(length);
                EditorGUILayout.PropertyField(width);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }

}