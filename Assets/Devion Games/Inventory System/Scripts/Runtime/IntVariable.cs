using UnityEngine;

namespace DevionGames.InventorySystem
{
    [CreateAssetMenu(menuName = "Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] protected int value;

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = value;
        }
    }
}
