
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using MiniFramework;

[CustomEditor(typeof(ArtNumber))]
public class ArtNumberEditor : Editor
{
    ArtNumber artNumber;
    private void OnEnable()
    {
        artNumber = (ArtNumber)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
		artNumber.Number = (uint)serializedObject.FindProperty("number").intValue;
		serializedObject.ApplyModifiedProperties();
    }

}