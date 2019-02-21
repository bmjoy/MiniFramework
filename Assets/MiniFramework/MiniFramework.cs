using UnityEditor;
using UnityEngine;
namespace MiniFramework
{
    public class MiniFramework : MonoSingleton<MiniFramework>
    {
        public override void OnSingletonInit(){ }
        //static DrivenRectTransformTracker tracker = new DrivenRectTransformTracker();
        //[MenuItem("Test/Limit")]
        //static void Check()
        //{
        //    tracker.Clear();
        //    tracker.Add(Selection.activeGameObject, Selection.activeGameObject.GetComponent<RectTransform>(), DrivenTransformProperties.Pivot | DrivenTransformProperties.Anchors);
        //}
    }
}