using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Events;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.WindowsManager
{
    public class WorldCanvasCameraChanger : MonoBehaviour
    {
        [SerializeField] private Canvas _worldCanvas;


        private void OnEnable()
        {
            EventManager.AddEventListener(ConstantEventsName.ON_SCENE_LOADED, OnSceneLoadedHandler);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_SCENE_LOADED, OnSceneLoadedHandler);
        }


        private void OnSceneLoadedHandler(CustomEvent _event)
        {
            _worldCanvas.worldCamera = Camera.main;
        }
    }
}
