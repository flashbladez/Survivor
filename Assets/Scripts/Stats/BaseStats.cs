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

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
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
