using MiniFramework;
using UnityEngine;
public class PoolExample : MonoBehaviour
{
    public GameObject Prefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        PoolComponent.Instance.Init(Prefab, 10, 5, true);
    }
    void Use()
    {
        GameObject obj = PoolComponent.Instance.Allocate(Prefab.name);
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
        PoolComponent.Instance.Recycle(Prefab.name);
    }
}
