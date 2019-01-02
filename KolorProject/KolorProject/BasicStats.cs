using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    class Attributes
    {

        public int Strength;
        public int Agility;
        public int Constitiution;
        public int Intelligence;
        public int Knowledge;
        public int Initiative;

        public Attributes(int strength = 0, int agility = 0, int constition = 0, int intelligence = 0, int knowlledge = 0, int initiative = 0)
        {
            Strength = strength;
            Agility = agility;
            Constitiution = constition;
            Intelligence = intelligence;
            Knowledge = knowlledge;
            Initiative = initiative;
        }

    }

     class CombatAbilities//for spells
    {

        public int Warfare;
        public int Hunter;
        public int FireLord;
        public int WindLord;
        public int EarthLord;
        public int BloodLord;

        public CombatAbilities(int warfare = 0, int hunter = 0, int firelord = 0, int windLord = 0, int earthlord = 0, int bloodlord = 0)
        {
            Warfare = warfare;
            Hunter = hunter;
            FireLord = firelord;
            WindLord = windLord;
            EarthLord = earthlord;
            BloodLord = bloodlord;
        }
    }


    class BasicStats
    {
        public int currentLevel;
        public bool exhausted = false;//once action is performed he waits until the end of the turn
        public int baseHp, baseEndurance;
        public int currentEndurance, maxEndurance;
        public int currentHp, maxHp;
        public int currentMana, maxMana;
        public int currentPhysicalArmor, maxPhysicalArmor;
        public int currentMagicalArmor, maxMagicalArmor;
        public int minPhysicalAttack, maxPhysicalAttack;
        public int minRangedPhysicalAttack, maxRangedPhysicalAttack;
        public int minMagicalAttack, maxMagicalAttack;
        public int currentActionPoints, maxActionPoints;
        public int currentExperience, experienceToNextLv;

        public int experienceOnDeath = 40;

        public BasicStats(
            int currentlevel = 1,
            int currentendurance = 10, int maxendurance = 10,
            int currentactionpoints = 4, int maxactionpoints = 4,
            int currenthp = 20, int maxhp = 20,
            int currentmana = 10, int maxmana = 10,
            int currentphysicalarmor = 0, int maxphysicalarmor = 0,
            int currentmagicalarmor = 0, int maxmagicalarmor = 0,
            int minphysicalattack = 1, int maxphysicalattack = 1,//mele
            int minmagicalattack = 1, int maxmagicalattack = 1,//magical
            int minrangedphysicalattack = 1, int maxrangedphysicalattack = 1)//ranged
        {
            currentLevel = currentlevel;

            currentActionPoints = currentactionpoints;
            maxActionPoints = maxactionpoints;


            baseHp = 25;
            baseEndurance = 10;
            currentExperience = 0;
            experienceToNextLv = 300;
            currentHp = currenthp;
            maxHp = maxhp;
            currentEndurance = currentendurance;
            maxEndurance = maxendurance;
            currentMana = currentmana;
            maxMana = maxmana;

            currentPhysicalArmor = currentphysicalarmor;
            currentMagicalArmor = currentmagicalarmor;
            maxMagicalArmor = maxmagicalarmor;
            maxPhysicalArmor = maxphysicalarmor;

            minPhysicalAttack = minphysicalattack;
            minMagicalAttack = minmagicalattack;
            maxMagicalAttack = maxmagicalattack;
            maxPhysicalAttack = maxphysicalattack;
            minRangedPhysicalAttack = minrangedphysicalattack;
            maxRangedPhysicalAttack = maxrangedphysicalattack;
        }

    }

}
