using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Tools.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.WindowsQueue
{
    public class WindowsQueueController : AutoLocatorObject
    {
        [SerializeField] private float _delayTime = 0.75f;

        private IWindowsManagerUsing _windowsManagerUser;
        private List<System.Type> _checkWindows = new List<System.Type>();


        private void Start()
        {
            _windowsManagerUser = GameManager.Instance;
        }

        private void Update()
        {
            //TODO: убрать из апдейта

            if (_checkWindows.Count < 1)
                return;

            if (_checkWindows.Contains(_windowsManagerUser.WindowsController.GetLastWindow()?.GetType()))
                Debug.LogError("!!! _checkWindows.Contains() !!!");
        }


        public WindowsQueueController Reset()
        {
            _checkWindows.Clear();
            return this;
        }

        public WindowsQueueController SetCheckWindows(List<System.Type> checkWindows)
        {
            _checkWindows = checkWindows;
            return this;
        }
    }
}
