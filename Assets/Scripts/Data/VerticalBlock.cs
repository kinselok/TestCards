using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class VerticalBlock
    {
        [SerializeField] private List<int> RemovedCards = new List<int>();
        public float ScrollPosition = 1;

        public void AddRemovedCard(int id)
        {
            if(!RemovedCards.Contains(id))
                RemovedCards.Add(id);
        }

        public bool IsRemoved(int id) => 
            RemovedCards.Contains(id);
    }
}