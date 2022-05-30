using System;
using UnityEngine;

namespace Tools.Resources
{
    public class ResourceController : Singleton<ResourceController>
    {
        public void LoadAsync<T>(string path, Action<T> callback) where T : class
        {
            var resourceRequest = UnityEngine.Resources.LoadAsync(path, typeof(T));
        
            resourceRequest.completed += OnLoad;

            void OnLoad(AsyncOperation operation)
            {
                T asset = resourceRequest.GetAwaiter().GetResult() as T;
                
                callback?.Invoke(asset);
            }
        }

        public void UnloadAsset(UnityEngine.Object asset)
        {
            UnityEngine.Resources.UnloadAsset(asset);
        }

        public void UnloadUnusedAssets()
        {
            UnityEngine.Resources.UnloadUnusedAssets();
        }
    }
}
