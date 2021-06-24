﻿using UnityEngine;

namespace DllSky.StarterKITv2.UI.Components
{
    public class SafeAreaRect : MonoBehaviour
    {
        private RectTransform _rect;


        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            ApplyOffset();
        }


        private void ApplyOffset()
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            
            _rect.anchorMin = anchorMin;
            _rect.anchorMax = anchorMax;
        }
    }
}
