using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ITEMS;
using UnityEngine;

namespace COUNTER
{
    public class CounterManager : MonoBehaviour
    {
        public static CounterManager instance { get; private set; }

        ItemsManager itemsManager => ItemsManager.instance;

        public float spacing = 2.0f;
        public Vector2 centerPosition = Vector2.zero;

        private void Awake()
        {
            instance = this;
        }

        public void PlaceAllMedicine(int numberOfMedicineToPlace, List<string> necessaryMedicines)
        {
            if (necessaryMedicines.Count > numberOfMedicineToPlace || ItemWarehouse.Instance.ItemCount < numberOfMedicineToPlace)
            {
                return;
            }

            List<string> theOtherMedicines = SelectRandomElements(ItemWarehouse.Instance.GetAllItems(), numberOfMedicineToPlace - necessaryMedicines.Count);
            List<string> allMedicinesToPlace = necessaryMedicines.Union(theOtherMedicines).ToList();

            List<Vector2> positions = GeneratePositions(numberOfMedicineToPlace);
            positions = Shuffle(positions);

            for (int i = 0; i < numberOfMedicineToPlace; i++)
            {
                Item item = itemsManager.CreateItem(allMedicinesToPlace[i]);
                item.SetPosition(positions[i]);
                item.Show();
            }

        }

        public void Place()
        {

        }

        List<Vector2> GeneratePositions(int count)
        {
            List<Vector2> positions = new List<Vector2>();

            if (count == 9)
            {
                for (int row = -1; row <= 1; row++)
                {
                    for (int col = -1; col <= 1; col++)
                    {
                        float x = col * spacing;
                        float y = row * spacing;
                        positions.Add(new Vector2(x, y));
                    }
                }
            }
            else if (count == 4)
            {
                float halfSpacing = spacing * 0.5f;
                positions.Add(new Vector2(-halfSpacing, halfSpacing));
                positions.Add(new Vector2(halfSpacing, halfSpacing));
                positions.Add(new Vector2(-halfSpacing, -halfSpacing));
                positions.Add(new Vector2(halfSpacing, -halfSpacing));
            }

            return positions;
        }

        static List<string> SelectRandomElements(List<string> list, int count)
        {
            System.Random random = new System.Random();
            if (count > list.Count)
            {
                return list.OrderBy(x => random.Next()).Take(list.Count).ToList();
            }

            return list.OrderBy(x => random.Next()).Take(count).ToList();
        }

        List<T> Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            return list;
        }
    }
}