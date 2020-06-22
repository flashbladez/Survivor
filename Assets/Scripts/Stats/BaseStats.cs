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

        int currentLevel = 0;

        //   void Start()
        //  {
        //      currentLevel = CalculateLevel();
        //      Experience experience = GetComponent<Experience>();
        //    if(experience != null)
        //    {
        //        experience.onExperienceGained += UpdateLevel;
        //    }
        // }
        void Update()
        {
            if(gameObject.tag == "Player")
            {
                print(Getlevel());
            }
        }


        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, Getlevel());
        }

        public int Getlevel()
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

      //  void UpdateLevel()
      //  {
      //      int newLevel = CalculateLevel();
      //      if (newLevel > currentLevel)
      //     {
      //          currentLevel = newLevel;
      //          print("Levelled Up");
      //      }
      //  }

     //   int CalculateLevel()
     //   {
     //       return 0;
     //   }
    }
}
