using System;
using UnityEngine;

namespace Akassets.SmoothGridLayout
{
    public class ElementsContainer : MonoBehaviour
    {
        public event Action OnChildrenChanged;

        private int disabledObjectsCounter;
        private void OnTransformChildrenChanged()
        {
            OnChildrenChanged?.Invoke();
        }

        private void Update()
        {
            if(IsChanged())
                OnChildrenChanged?.Invoke();
        }

        private bool IsChanged()
        {
            int counter = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                    counter++;
            }
            if(counter != disabledObjectsCounter)
            {
                disabledObjectsCounter = counter;
                return true;
            }

            return false;
        }
    }
}
