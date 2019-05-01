using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    static class All_Items
    {
        static Random rnd = new Random();
        //there can be more than one item of the same type
        //it is so in order to increasy probability of selecting this item
        public static List<ETier_1_Treasure> Tier_1_treasure = new List<ETier_1_Treasure>()
            {

                ETier_1_Treasure.Minor_healing_potion,
                ETier_1_Treasure.Bow_1,
                ETier_1_Treasure.Sword_1,
                ETier_1_Treasure.Magic_Staff_1,
                ETier_1_Treasure.Plate_Armor_1,
                ETier_1_Treasure.Metal_Helmet_1,
                ETier_1_Treasure.Furious_Boots,
                ETier_1_Treasure.Leather_Gloves,
                ETier_1_Treasure.SlingShot,
                ETier_1_Treasure.Axe_1,
                ETier_1_Treasure.Enchanted_Rod
            };

        public static List<ETier_2_Treasure> Tier_2_treasure = new List<ETier_2_Treasure>()
            {
                ETier_2_Treasure.MediumHealPotion,
                ETier_2_Treasure.MediumHealPotion,
                ETier_2_Treasure.BiggusSwordus,
                ETier_2_Treasure.Crossbow,
                ETier_2_Treasure.FluffyArmor,
                ETier_2_Treasure.GatlingGun,
                ETier_2_Treasure.HighHeelShoes,
                ETier_2_Treasure.LightningBolt,
                ETier_2_Treasure.MightyMace,
                ETier_2_Treasure.OversizedHelmet,
                ETier_2_Treasure.PinkGauntlets,
                ETier_2_Treasure.StaffOfFlames,
                ETier_2_Treasure.StormHammer
            };

        public static List<ETier_1_Enemy> Tier_1_enemy = new List<ETier_1_Enemy>()
            {
                ETier_1_Enemy.Bat_1,
                ETier_1_Enemy.Bat_1,
                ETier_1_Enemy.Goblin_1,
                ETier_1_Enemy.Bat_1
            };

        public static List<ETier_2_Enemy> Tier_2_enemy = new List<ETier_2_Enemy>()
            {
                ETier_2_Enemy.CorruptedSanta,
                ETier_2_Enemy.FrontEndWannabe,
                ETier_2_Enemy.ViciousScrumMaster,
                ETier_2_Enemy.Watcher
            };

        public static List<ETier_3_Enemy> Tier_3_enemy = new List<ETier_3_Enemy>()
            {
                ETier_3_Enemy.AbusiveJanitor,
                ETier_3_Enemy.HeadlessHead,
                ETier_3_Enemy.ImpressiveAnt,
                ETier_3_Enemy.ShySuccubus
            };

        public static Character genTier_1_Enemy(int strength = 0)
        {
            Character mCharacter = null;


            ETier_1_Enemy enemyType = Tier_1_enemy[rnd.Next() % Tier_1_enemy.Count];

            switch (enemyType)
            {

                case ETier_1_Enemy.Bat_1:
                    mCharacter = new Bat_1();
                    break;
                case ETier_1_Enemy.Goblin_1:
                    mCharacter = new Goblin_1();
                    break;
            }


            mCharacter.mAttributes.Strength += strength;
            mCharacter.mAttributes.Agility += strength;
            mCharacter.mAttributes.Intelligence += strength;
            mCharacter.mAttributes.Constitiution += strength / 2;
            mCharacter.mBasicStats.currentLevel += strength / 2;
            mCharacter.calculateBasicStats();

            return mCharacter;
        }

        public static Character genTier_2_Enemy(int strength = 0)
        {
            Character mCharacter = null;

            ETier_2_Enemy enemyType = Tier_2_enemy[rnd.Next() % Tier_2_enemy.Count];

            switch (enemyType)
            {

                case ETier_2_Enemy.Watcher:
                    mCharacter = new Watcher();
                    break;
                case ETier_2_Enemy.ViciousScrumMaster:
                    mCharacter = new ViciousScrumMaster();
                    break;
                case ETier_2_Enemy.FrontEndWannabe:
                    mCharacter = new FrontEndWannabe();
                    break;
                case ETier_2_Enemy.CorruptedSanta:
                    mCharacter = new CorruptedSanta();
                    break;
            }

            mCharacter.mAttributes.Strength += strength;
            mCharacter.mAttributes.Agility += strength;
            mCharacter.mAttributes.Intelligence += strength;

            mCharacter.mBasicStats.currentLevel += strength;
            mCharacter.calculateBasicStats();

            return mCharacter;
        }

        public static Character genTier_3_Enemy(int strength = 0)
        {
            Character mCharacter = null;

            ETier_3_Enemy enemyType = Tier_3_enemy[rnd.Next() % Tier_3_enemy.Count];

            switch (enemyType)
            {

                case ETier_3_Enemy.AbusiveJanitor:
                    mCharacter = new AbusiveJanitor();
                    break;
                case ETier_3_Enemy.HeadlessHead:
                    mCharacter = new HeadlessHead();
                    break;
                case ETier_3_Enemy.ImpressiveAnt:
                    mCharacter = new ImpressiveAnt();
                    break;
                case ETier_3_Enemy.ShySuccubus:
                    mCharacter = new ShySuccubus();
                    break;
            }

            mCharacter.mAttributes.Strength += strength / 2;
            mCharacter.mAttributes.Agility += strength / 2;
            mCharacter.mAttributes.Intelligence += strength / 2;
            mCharacter.mAttributes.Constitiution += strength / 2;
            mCharacter.mBasicStats.currentLevel += strength / 2;
            mCharacter.calculateBasicStats();

            return mCharacter;
        }


        public static Item genTier_1_Treasure()
        {
            Item mItem = null;

            ETier_1_Treasure mKey = Tier_1_treasure[rnd.Next() % Tier_1_treasure.Count];

            switch (mKey)
            {
                case ETier_1_Treasure.Minor_healing_potion:
                    mItem = new MinorHealingPotion();
                    break;
                case ETier_1_Treasure.Bow_1:
                    mItem = new Bow_1();
                    break;
                case ETier_1_Treasure.Sword_1:
                    mItem = new Sword_1();
                    break;
                case ETier_1_Treasure.Metal_Helmet_1:
                    mItem = new Metal_Helmet_1();
                    break;
                case ETier_1_Treasure.Plate_Armor_1:
                    mItem = new Plate_Armor_1();
                    break;
                case ETier_1_Treasure.Magic_Staff_1:
                    mItem = new Magic_Staff_1();
                    break;
                case ETier_1_Treasure.Leather_Gloves:
                    mItem = new Leather_Gloves();
                    break;
                case ETier_1_Treasure.Furious_Boots:
                    mItem = new Furious_Boots();
                    break;
                case ETier_1_Treasure.Axe_1:
                    mItem = new Axe_1();
                    break;
                case ETier_1_Treasure.SlingShot:
                    mItem = new Slingshot();
                    break;
                case ETier_1_Treasure.Enchanted_Rod:
                    mItem = new Enchanted_Rod();
                    break;
            }

            return mItem;
        }

        public static Item genTier_2_Treasure()
        {
            Item mItem = null;

            ETier_2_Treasure mKey = Tier_2_treasure[rnd.Next() % Tier_2_treasure.Count];

            switch (mKey)
            {
                case ETier_2_Treasure.StormHammer:
                    mItem = new StormHammer();
                    break;
                case ETier_2_Treasure.StaffOfFlames:
                    mItem = new StaffOfFlames();
                    break;
                case ETier_2_Treasure.PinkGauntlets:
                    mItem = new PinkGauntlets();
                    break;
                case ETier_2_Treasure.OversizedHelmet:
                    mItem = new OversizedHelmet();
                    break;
                case ETier_2_Treasure.MightyMace:
                    mItem = new MightyMace();
                    break;
                case ETier_2_Treasure.MediumHealPotion:
                    mItem = new MediumHealingPotion();
                    break;
                case ETier_2_Treasure.LightningBolt:
                    mItem = new LigtningBolt();
                    break;
                case ETier_2_Treasure.HighHeelShoes:
                    mItem = new HighHeelShoes();
                    break;
                case ETier_2_Treasure.GatlingGun:
                    mItem = new GatlingGun();
                    break;
                case ETier_2_Treasure.FluffyArmor:
                    mItem = new FluffyArmor();
                    break;
                case ETier_2_Treasure.Crossbow:
                    mItem = new Crossbow();
                    break;
                case ETier_2_Treasure.BiggusSwordus:
                    mItem = new BiggusSwordus();
                    break;

            }

            return mItem;
        }

    }
}
