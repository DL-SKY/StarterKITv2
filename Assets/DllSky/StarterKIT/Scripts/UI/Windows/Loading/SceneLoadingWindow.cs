using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Interfaces.Windows;
using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.Loading
{
    public class SceneLoadingWindow : WindowBase, IWindowSceneLoader
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\SceneLoadingWindow";

        public event Action OnSceneLoaded;
        
        public string SceneName { get; private set; }
        public AsyncOperation LoadingProc { get; private set; }


        public override void Initialize(object data)
        {
            SceneName = (string)data;

            LoadingProc = GameManager.Instance.LoadSceneAsync(SceneName);
            LoadingProc.completed += OnLoadSceneCompletedHandler;

            SetInitialize(true);
        }

        public void CloseLoaderWindow()
        {
            Close();
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (LoadingProc != null)
                LoadingProc.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();
        }
    }
}
