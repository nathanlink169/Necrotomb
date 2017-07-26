using UnityEngine;

namespace GameFramework
{
    public class GPlayerHealth : SingletonBehaviour<GPlayerHealth>
    {
        #region Public Constants
        public const float STARTING_HEALTH = 100.0f;
        public const float HEALTH_PER_UPGRADE = 25.0f;
        #endregion

        #region Public Accessors
        public float Current { get { return m_CurrentHealth.Value; } }
        public float Maximum { get { return m_CurrentHealth.MaximumValue; } }
        public int Upgrades
        {
            get
            {
                SaveData saveData = GPlayerManager.Instance.PlayerData;
                return 4 +
                       (saveData.UnlockedHealthUpgrade(GSaveManager.HEALTH_UPGRADE_1_FLAG) ? 1 : 0) +
                       (saveData.UnlockedHealthUpgrade(GSaveManager.HEALTH_UPGRADE_2_FLAG) ? 1 : 0) +
                       (saveData.UnlockedHealthUpgrade(GSaveManager.HEALTH_UPGRADE_3_FLAG) ? 1 : 0) +
                       (saveData.UnlockedHealthUpgrade(GSaveManager.HEALTH_UPGRADE_4_FLAG) ? 1 : 0);
            }
        }
        #endregion

        #region Public Functions
        public void Damage(Vector3 in_vLocation, float in_fDamage)
        {
            GameObject player = GPlayerManager.Instance.PlayerObject;
            bool bIsTripleDamage = false;
            if (Vector3.Dot(player.transform.forward, MathUtils.DirectionVector(player.transform.position, in_vLocation)) < -0.5f)
            {
                in_fDamage *= 3f;
                bIsTripleDamage = true;
            }

            m_CurrentHealth.Value -= in_fDamage;
            GEventManager.TriggerEvent(GEventManager.ON_PLAYER_TAKE_DAMAGE);
            if (bIsTripleDamage)
            {
                GEventManager.TriggerEvent(GEventManager.ON_PLAYER_TAKE_TRIPLE_DAMAGE);
            }
        }

        public void Heal(float in_fHeal)
        {
            m_CurrentHealth.Value += in_fHeal;
            GEventManager.TriggerEvent(GEventManager.ON_PLAYER_HEAL_DAMAGE);
        }

        public void FullyHeal()
        {
            m_CurrentHealth.Value = m_CurrentHealth.MaximumValue;
            GEventManager.TriggerEvent(GEventManager.ON_PLAYER_HEAL_DAMAGE);
        }
        #endregion

        #region Private
        private ClampedFloat m_CurrentHealth = new ClampedFloat(STARTING_HEALTH, 0.0f, STARTING_HEALTH);
        #endregion
    }
}