using System.Collections.Generic;
using UnityEngine.Events;

namespace GameFramework
{
    public class GEventManager : SingletonBehaviour<GEventManager>
    {
        #region EventNames
        public const string ON_PLAYER_INVENTORY_UPDATED = "OnPlayerInventoryUpdated";
        public const string ON_PLAYER_TAKE_DAMAGE = "OnPlayerTakeDamage";
        public const string ON_PLAYER_TAKE_TRIPLE_DAMAGE = "OnPlayerTakeTripleDamage";
        public const string ON_PLAYER_HEAL_DAMAGE = "OnPlayerHealDamage";

        public const string ON_VOLUME_CHANGED = "OnVolumeChanged";
        #endregion

        private Dictionary<string, UnityEvent> eventDictionary;

        public override void Awake()
        {
            base.Awake();
            Init();
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}