using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Saving;
using Survivor.Stats;
using Survivor.Core;
using System;

namespace Survivor.Resources
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;

        float healthPoints = -1f;

        bool isDead = false;

        void Start()
        {
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
               healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        void RegenerateHealth()
        {
           // Debug.Log(healthPoints);
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            //float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            //healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null)
            {
                return;
            }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        void Die()
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}
