using GameDevTV.Inventories;
using Survivor.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Inventories
{
    [CreateAssetMenu(menuName = "Survivor/Inventory/Equipable Item")]
    public class StatsEquipableItem : EquipableItem,IModifierProvider
    {
        [SerializeField]
        Modifier[] additveModifiers;
        [SerializeField]
        Modifier[] percentageModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
        public override string GetDescription()
        {
            string result = base.GetDescription();

            result += GetAdditiveBonusText(Stat.Damage);
            result += GetAdditiveBonusText(Stat.Health);
            return result;
        }

        string GetAdditiveBonusText(Stat stat)
        {
            float accumulator = 0.0f;
            foreach (float modifier in GetAdditiveModifiers(stat))
            {
                accumulator += modifier;
            }

            if (Math.Abs(accumulator) > .0001f)
            {
                return $"<br>{Mathf.Abs(accumulator)} points to {stat}";
            }

            return "";
        }
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach(var modifier in additveModifiers)
            {
                if(modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}
