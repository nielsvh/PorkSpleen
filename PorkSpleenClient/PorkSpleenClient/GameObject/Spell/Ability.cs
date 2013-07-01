using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorkSpleenClient
{
    class Ability
    {
        enum AbilityType
        {
            DAMAGEAOE, DAMAGE,PREVENTDAMAGE,SUMMON,CREATE
        }
        enum CreateType
        {
            AURA, ENCHANTMENT, ARTIFACT
        }
        int damage, preventDamage;
        Creature summon;
    }
}
