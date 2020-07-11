using Survivor.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        int currentLevel = 0;

        public event Action OnLevelUp;

        void Start()
        {
             currentLevel = CalculateLevel();
             Experience experience = GetComponent<Experience>();
             if(experience != null)
             {
                experience.onExperienceGained += UpdateLevel;
             }
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, Getlevel()) + GetAdditiveModifier(stat);
        }

        public int Getlevel()
        {
           if(currentLevel < 1)
           {
                currentLevel = CalculateLevel();
           }
           return currentLevel;
        }

        float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if(experience == null)
            {
                return startingLevel;
            } 

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(xpToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
              //  print(Getlevel());
                currentLevel = newLevel;
                LevelUpEffect();
                OnLevelUp();
            }
        }
    }
}
