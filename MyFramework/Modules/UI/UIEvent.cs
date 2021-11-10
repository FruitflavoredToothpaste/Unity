using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyFramework.Modules
{
    public class UIEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<PointerEventData> OnClick;

        public event Action<PointerEventData> OnEnter;

        public event Action<PointerEventData> OnExit;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Equals(OnClick, null))
            {
                OnClick(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Equals(OnEnter, null))
            {
                OnEnter(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Equals(OnExit, null))
            {
                OnExit(eventData);
            }
        }
    }
}