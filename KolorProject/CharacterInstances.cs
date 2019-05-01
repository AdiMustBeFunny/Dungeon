using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    class Hero : Character
    {

        public Hero(string name) : base(name)
        {
            // mEquipedItems["weapon"] = new Bow_1();
            // Console.WriteLine("{0}",(mEquipedItems["weapon"] as RangedWeapon).minRangedPhysicalAttack);
            // Console.WriteLine("{0}", (mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack);
            // mEquipedItems["chest"] = new Plate_Armor_1();
            // Console.WriteLine("Armor's physical armor: {0}", (mEquipedItems["chest"] as Appearal).physicalArmor);
            // Console.WriteLine("Armor's magical armor: {0}", (mEquipedItems["chest"] as Appearal).magicalArmor);
        }


    }

    #region Tier_1 characters
    class Bat_1 : Character
    {

        public Bat_1() : base("Bat_1")
        {

            //mBasicStats.minPhysicalAttack = 1;
            //mBasicStats.maxPhysicalAttack = 2;
            mInventory.mEquipedItems["weapon"] = new Bat_Claws();

        }

    }

    class Goblin_1 : Character
    {
        public Goblin_1() : base("Goblin_1")
        {
            //mBasicStats.minPhysicalAttack = 2;
            //mBasicStats.maxPhysicalAttack = 2;
            mInventory.mEquipedItems["weapon"] = new Goblin_Claws();
        }

    }
    #endregion

    #region Tier_2 characters

    class Watcher : Character
    {
        public Watcher() : base("Watcher")
        {
            // mBasicStats.currentLevel += 1;
            // calculateBasicStats();
            // mBasicStats.minPhysicalAttack = 3;
            // mBasicStats.maxPhysicalAttack = 4;
            mInventory.mEquipedItems["weapon"] = new Claw_1();
        }
    }

    class CorruptedSanta : Character
    {

        public CorruptedSanta() : base("Corrupted_Santa")
        {
            // mBasicStats.currentLevel += 1;
            // calculateBasicStats();
            // mBasicStats.minMagicalAttack = 2;
            // mBasicStats.maxMagicalAttack = 4;
            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }

    }

    class FrontEndWannabe : Character
    {
        public FrontEndWannabe() : base("FrontEndWannabe")
        {
            // mBasicStats.currentLevel += 1;
            // calculateBasicStats();
            // mBasicStats.minMagicalAttack = 2;
            // mBasicStats.maxMagicalAttack = 3;
            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }

    }

    class ViciousScrumMaster : Character
    {
        public ViciousScrumMaster() : base("ViciousScrumMaster")
        {
            //mBasicStats.currentLevel += 1;
            //calculateBasicStats();
            // mBasicStats.minPhysicalAttack = 4;
            // mBasicStats.maxPhysicalAttack = 4;
            mInventory.mEquipedItems["weapon"] = new Claw_1();
        }
    }

    #endregion

    #region Tier_3 characters

    class HeadlessHead : Character
    {
        public HeadlessHead() : base("HeadlessHead")
        {
            mBasicStats.currentLevel += 3;
            //calculateBasicStats();
            // mBasicStats.minPhysicalAttack = 4;
            // mBasicStats.maxPhysicalAttack = 8;

            mBasicStats.currentPhysicalArmor = 30;
            mBasicStats.currentMagicalArmor = 30;

            mInventory.mEquipedItems["weapon"] = new Claw_1();
        }
    }
    class AbusiveJanitor : Character
    {
        public AbusiveJanitor() : base("AbusiveJanitor")
        {
            mBasicStats.currentLevel += 3;
            //calculateBasicStats();
            //mBasicStats.minPhysicalAttack = 7;
            //mBasicStats.maxPhysicalAttack = 10;

            mBasicStats.currentPhysicalArmor = 15;

            mInventory.mEquipedItems["weapon"] = new Sword_1();
        }
    }
    class ImpressiveAnt : Character
    {
        public ImpressiveAnt() : base("ImpressiveAnt")
        {
            mBasicStats.currentLevel += 3;
            //calculateBasicStats();
            //mBasicStats.minMagicalAttack = 6;
            // mBasicStats.maxMagicalAttack = 8;

            mBasicStats.currentPhysicalArmor = 40;
            mBasicStats.currentMagicalArmor = 40;

            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }
    }
    class ShySuccubus : Character
    {
        public ShySuccubus() : base("ShySuccubus")
        {
            mBasicStats.currentLevel += 3;
            // calculateBasicStats();
            // mBasicStats.minMagicalAttack = 5;
            // mBasicStats.maxMagicalAttack = 12;

            mBasicStats.currentPhysicalArmor = 20;

            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }
    }

    #endregion


}
