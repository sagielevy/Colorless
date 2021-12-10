using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DevionGames.InventorySystem
{
    public class IntGameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IntGameEventListener> eventListeners =
            new List<IntGameEventListener>();

        public void Raise(int input)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(input);
            }
        }

        public void RegisterListener(IntGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IntGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}