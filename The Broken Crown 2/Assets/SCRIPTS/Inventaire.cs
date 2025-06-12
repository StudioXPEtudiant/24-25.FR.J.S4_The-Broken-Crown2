using System;
using System.Collections.Generic;
using StudioXP.Scripts.Objects;
using UnityEngine;

namespace StudioXP.Scripts.Characters
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField, Min(1)] private int size = 1;
        [SerializeField] private GameObject root;

        public GameObject Root => root;

        private readonly List<Pickable> _slots = new();
        private int _currentSlot = 0;

        private void Awake()
        {
            if (!root)
                root = gameObject;
        }

        public bool TrySetItemInEmptySlot(Pickable pickable)
        {
            if (_slots.Count >= size)
                return false;

            _slots.Add(pickable);
            return true;
        }

        public Pickable RemoveCurrentSlot()
        {
            if (_slots.Count == 0 || _currentSlot >= _slots.Count) return null;

            var pickable = _slots[_currentSlot];
            _slots.RemoveAt(_currentSlot);
            _currentSlot = Mathf.Clamp(_currentSlot, 0, _slots.Count - 1);

            return pickable;
        }

        public Pickable GetCurrentItem()
        {
            return _currentSlot >= _slots.Count ? null : _slots[_currentSlot];
        }

        public void ChangeSlot(float axis)
        {
            if (axis < 0)
                PreviousSlot();
            else if (axis > 0)
                NextSlot();
        }

        public void NextSlot()
        {
            Select((_currentSlot + 1) % _slots.Count);
        }

        public void PreviousSlot()
        {
            Select((_currentSlot - 1 + _slots.Count) % _slots.Count);
        }

        private void Select(int slot)
        {
            if (_slots.Count == 0) return;

            if (slot == _currentSlot) return;

            var currentInteractable = GetCurrentItem();
            if (currentInteractable)
                currentInteractable.gameObject.SetActive(false);

            _currentSlot = Mathf.Clamp(slot, 0, _slots.Count - 1);

            currentInteractable = GetCurrentItem();
            if (currentInteractable)
                currentInteractable.gameObject.SetActive(true);
        }
    }
}
