using System.Collections;
using System.Collections.Generic;
using ITEMS;
using UnityEngine;

namespace COUNTER
{
    public class CounterManager : MonoBehaviour
    {
        public static CounterManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public void PlaceAllMedicine(int numberOfMedicineToPlace, List<string> necessaryMedicines)
        {
            if (necessaryMedicines.Count > numberOfMedicineToPlace)
            {
                return;
            }

        }

        public void Place()
        {

        }
    }
}