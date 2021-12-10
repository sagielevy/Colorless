﻿using UnityEngine;
using UnityEngine.Events;

namespace DevionGames.InventorySystem
{
    public class IntGameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        private IntGameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField]
        private UnityEvent<int> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int input)
        {
            Response.Invoke(input);
        }
    }
}