using System;
using System.Collections.Generic;

namespace MiniFramework
{
    public class FSM
    {
        private string mCurState;
        public string CurState
        {
            get { return mCurState; }
        }
        class FSMState
        {
            public string Name;
            public FSMState(string name)
            {
                Name = name;
            }
            public readonly Dictionary<string, FSMTranslation> mTranslationDict = new Dictionary<string, FSMTranslation>();//每个状态对应一个跳转
        }
        class FSMTranslation
        {
            public string FromState;
            public string Command;
            public string ToState;
            public Action OnTranslationCallback;
            public FSMTranslation(string fromState,string command,string toState,Action onTranslationCallback)
            {
                FromState = fromState;
                Command = command;
                ToState = toState;
                OnTranslationCallback = onTranslationCallback;
            }
        }
        Dictionary<string, FSMState> mStateDict = new Dictionary<string, FSMState>();   //状态池   
        /// <summary>
        /// 添加跳转
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="command"></param>
        /// <param name="toState"></param>
        public void AddTranslation(string fromState,string command,string toState, Action onTranslationCallback)
        {
            if (!mStateDict.ContainsKey(fromState))
            {
                mStateDict[fromState] = new FSMState(fromState);
            }
            if (!mStateDict.ContainsKey(toState))
            {
                mStateDict[toState] = new FSMState(toState);
            }
            mStateDict[fromState].mTranslationDict[command] = new FSMTranslation(fromState, command, toState,onTranslationCallback);
        }
        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="stateName"></param>
        public void Start(string stateName)
        {
            if(!mStateDict.ContainsKey(stateName))
            {
                mStateDict[stateName] = new FSMState(stateName);
                
            }
            mCurState = stateName;
        }
        /// <summary>
        /// 根据命令执行跳转
        /// </summary>
        /// <param name="command"></param>
        public void Excute(string command)
        {
            if (mCurState != null && mStateDict[mCurState].mTranslationDict.ContainsKey(command))
            {
                var tempTranslation = mStateDict[mCurState].mTranslationDict[command];
                tempTranslation.OnTranslationCallback();
                mCurState = tempTranslation.ToState;
            }
        }
        /// <summary>
        /// 清空状态池
        /// </summary>
        public void Clear()
        {
            mStateDict.Clear();
        }
    }
}
