using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniFramework
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        public AssetLoader AssetLoader;
        public SceneLoader SceneLoader;

        public override void OnSingletonInit()
        {
            AssetLoader = new AssetLoader(this);
            SceneLoader = new SceneLoader(this);
        }
    }
}

