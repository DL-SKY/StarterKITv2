using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        public Action OnInitialize;
        public Action<bool, WindowBase> OnClose;        

        [Header("Main Settings")]
        [SerializeField] protected bool _canUseEsc;


        public virtual void Initialize(object data)
        {
            OnInitialize?.Invoke();
        }

        public void Close(bool result = false)
        {
            OnClose?.Invoke(result, this);
            Destroy(gameObject);

            CustomClose(result);
        }

        public void OnClickEsc()
        {
            if (_canUseEsc)
                Close();

            CustomOnClickEsc();
        }


        protected virtual void CustomClose(bool result) { }
        protected virtual void CustomOnClickEsc() { }
    }
}
