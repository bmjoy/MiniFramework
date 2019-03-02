using MiniFramework;
using UnityEngine;
public class PoolExample : MonoBehaviour
{
    public GameObject Prefab;
    public uint MaxNum;
    public uint MinNum;
    private void Start()
    {

    }
    private void OnGUI()
    {
        if (GUILayout.Button("Pool"))
        {
            Pool();
        }
        if (GUILayout.Button("GetFromPool"))
        {
            Use();
        }
        if (GUILayout.Button("Recycle"))
        {
            Recycle();
        }
    }

    void Pool()
    {
        GamePool.Instance.Init(Prefab, MaxNum, MinNum);
    }
    void Use()
    {
        GameObject obj = GamePool.Instance.Allocate(Prefab.name);
        if (obj != null)
        {
            float red = Random.Range(0.0f, 1.0f);
            float green = Random.Range(0.0f, 1.0f);
            float blue = Random.Range(0.0f, 1.0f);
            obj.GetComponent<MeshRenderer>().material.color = new Color(red, green, blue);
        }
    }
    void Recycle()
    {
        GamePool.Instance.Recycle(Prefab.name);
    }


}
