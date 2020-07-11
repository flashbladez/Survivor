using System.Collections.Generic;

namespace Survivor.Stats
{
    public interface IModifierProvider 
    {
       IEnumerable<float> GetAdditiveModifier(Stat stat);

    }
}
