using DllSky.StarterKITv2.Application;
using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.Loading
{
    public class SceneLoadingWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\SceneLoadingWindow";

        public Action OnSceneLoaded;

        private string _sceneName;
        private AsyncOperation _loading;


        public override void Initialize(object data)
        {
            _sceneName = (string)data;

            _loading = GameManager.Instance.LoadSceneAsync(_sceneName);
            _loading.completed += OnLoadSceneCompletedHandler;

            base.Initialize(data);
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (_loading != null)
                _loading.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();
        }
    }
}
