using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Events;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Tools.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.WindowsQueue
{
    public class WindowsQueueController : AutoLocatorObject
    {
        [SerializeField] private float _delayTime = 0.5f;

        private IWindowsManagerUsing _windowsManagerHolder;
        private List<System.Type> _checkWindows = new List<System.Type>();
        private List<WindowsQueueData> _queue = new List<WindowsQueueData>();


        private void Start()
        {
            _windowsManagerHolder = GameManager.Instance;
        }        


        public WindowsQueueController Reset()
        {
            Unsubscribe();

            _checkWindows.Clear();
            _queue.Clear();

            return this;
        }

        public WindowsQueueController SetCheckWindows(List<System.Type> checkWindows)
        {
            Unsubscribe();
            Subscribe();

            _checkWindows = checkWindows;
            return this;
        }

        public void AddToQueue(WindowsQueueData data)
        {
            _queue.Add(data);
            _queue = _queue.OrderByDescending(x => x.GetPriority()).ToList();   //Упорядочиваем от Большего к Меньшиму

            SetStartCheckQueue(_delayTime);
        }

        public void SetStartCheckQueue(float delay)
        {
            StopAllCoroutines();
            StartCoroutine(StartCheckQueue(delay));
        }


        private void Subscribe()
        {
            EventManager.AddEventListener(Constants.ConstantEventsName.ON_START_WINDOWS_QUEUE_CHECK, OnStartWindowQueueCheckHandler);

            _windowsManagerHolder.WindowsController.OnCloseWindow += OnCloseWindowHandler;
        }

        private void Unsubscribe()
        {
            StopAllCoroutines();

            EventManager.RemoveEventListener(Constants.ConstantEventsName.ON_START_WINDOWS_QUEUE_CHECK, OnStartWindowQueueCheckHandler);

            _windowsManagerHolder.WindowsController.OnCloseWindow -= OnCloseWindowHandler;
        }

        private void OnStartWindowQueueCheckHandler(CustomEvent e)
        {
            SetStartCheckQueue(_delayTime);
        }

        private void OnCloseWindowHandler(bool result, WindowBase window)
        {
            SetStartCheckQueue(_delayTime);
        }

        private bool CanUsingQueue()
        {
            return _checkWindows.Contains(_windowsManagerHolder.WindowsController.GetLastWindow()?.GetType());
        }

        private WindowsQueueData GetNextAction()
        {
            WindowsQueueData result = null;

            if (_queue.Count > 0)
            {
                result = _queue[0];
                _queue.RemoveAt(0);
            }

            return result;
        }

        protected override void CustomOnDestroy()
        {
            StopAllCoroutines();
            _windowsManagerHolder.WindowsController.OnCloseWindow -= OnCloseWindowHandler;
        }


        private IEnumerator StartCheckQueue(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (CanUsingQueue())
                GetNextAction()?.Execute();
        }
    }
}
