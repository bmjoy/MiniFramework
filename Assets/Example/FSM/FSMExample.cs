using MiniFramework;
using UnityEngine;


public class FSMExample:MonoBehaviour
{
    FSM playerActionFSM;
    private void Start()
    {
        playerActionFSM = new FSM();
        playerActionFSM.AddTranslation("Idle", "W_Down", "Run",Run);
        playerActionFSM.AddTranslation("Run", "W_Up", "Idle",Idle);
        playerActionFSM.AddTranslation("Idle", "Space", "Jump",Jump);
        playerActionFSM.AddTranslation("Jump", "Auto", "Idle",Idle);
        playerActionFSM.Start("Idle");
        Debug.Log("当前状态：" + playerActionFSM.CurState);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerActionFSM.Excute("W_Down");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerActionFSM.Excute("W_Up");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerActionFSM.Excute("Space");
        }
    }

    public void Run()
    {
        Debug.Log("Run");
    }

    public void Idle()
    {
        Debug.Log("Idle");
    }
    public void Jump()
    {
        Debug.Log("Jump");
    }
}
