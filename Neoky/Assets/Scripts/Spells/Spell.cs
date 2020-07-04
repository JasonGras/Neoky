using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class Spell
    {
        public string SpellID { get; set; }
        public string SpellName { get; set; }
        public string SpellIMG { get; set; }
        public float SpellRecastTime { get; set; }
        public float SpellAmount { get; set; }
        public float SpellEffectLast { get; set; }

        public Sprite Spell_image { get; set; }
        /*        public int SpellTarget { get; set; }
                public int SpellTargetProperty { get; set; }
                public int SpellTargetZone { get; set; }
                public int SpellType { get; set; }*/

        public SpellType spellType { get; set; }
        public SpellTarget spellTarget { get; set; }
        public SpellTargetZone spellTargetZone { get; set; }
        public SpellTargetProperty spellTargetProperty { get; set; }

        public enum SpellType
        {
            DEFENSE, // Shield, Armor
            OFFENSE, // Spell, Attack
            BUFF, // Heal, +Armor
            DEBUFF, // Remove, -Armor
            INCREMENTAL, //Turn 1 += 10 Damage , Turn 2 += 10 Damage ... 
            PASSIVE,
        }

        public enum SpellTarget
        {
            ALLY,
            ENEMY,
            SELF,
        }
        public enum SpellTargetZone
        {
            ONE_UNIT,
            ALL_UNITS,
            LINE_UNITS,
            COLUMN_UNITS,
            ZONE_UNITS,
        }
        public enum SpellTargetProperty
        {
            UNIT_HP,
            UNIT_MANA,
            UNIT_SHIELD,
            UNIT_DAMAGES,
            UNIT_VELOCITY,
        }

        public  Spell(string _SpellID, string _SpellName,string _SpellIMG, float _SpellRecastTime, float _SpellAmount, float _SpellEffectLast, int _SpellTarget, int _SpellTargetProperty, int _SpellTargetZone, int _SpellType)
        {
            SpellID = _SpellID;
            SpellName = _SpellName;
            SpellIMG = _SpellIMG;
            SpellRecastTime = _SpellRecastTime;
            SpellAmount = _SpellAmount;
            SpellEffectLast = _SpellEffectLast;
            spellTarget = (SpellTarget) _SpellTarget;
            spellTargetProperty = (SpellTargetProperty) _SpellTargetProperty;
            spellTargetZone = (SpellTargetZone) _SpellTargetZone;
            spellType = (SpellType) _SpellType;
        }
    }
}
