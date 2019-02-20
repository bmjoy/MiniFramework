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

