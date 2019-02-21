using UnityEditor;
using UnityEngine;
namespace MiniFramework
{
    public class MiniFramework : MonoSingleton<MiniFramework>
    {
        public override void OnSingletonInit(){ }
        void OnGUI(){
            if(GUILayout.Button("MiniFramework")){
                ResourceManager.Instance.SceneLoader.LoadScene("MiniFramework",null);
            }
            if(GUILayout.Button("A")){
                ResourceManager.Instance.SceneLoader.LoadScene("A",null);
            }
            if(GUILayout.Button("B")){
                ResourceManager.Instance.SceneLoader.LoadScene("B",null);
            }
        }
    }
}