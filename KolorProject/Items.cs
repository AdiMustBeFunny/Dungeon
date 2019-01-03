using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    
    abstract class Item
    {//there should be list of enchants for weapons and improvements for armor
        public EItemType mType = new EItemType();
        public EItemFunctionality mFunctionality = new EItemFunctionality();
        public string mName;

        public Item(EItemType mT, EItemFunctionality mF, string name)
        {
            mType = mT;
            mFunctionality = mF;
            mName = name;
        }

    }
    enum EItemType
    {
        Weapon,
        Appearal,
        Potion,
        Miscellaneous
    }
    enum EItemFunctionality
    {
        Wearable,
        Consumable,
        None
    }



    enum EPotionType
    {
        Health,
        Mana
    }
    abstract class Potion : Item
    {
        public EPotionType mPotionType = new EPotionType();
        public Potion(EItemType mT, EItemFunctionality mF, EPotionType mpotiontype, string name) : base(mT, mF, name) { mPotionType = mpotiontype; }
    }
     abstract class HpPotion : Potion
    {
        public int hpToReturn;
        public HpPotion(EItemType mT, EItemFunctionality mF, EPotionType mType, string name, int hptoreturn) : base(mT, mF, mType, name)
        { hpToReturn = hptoreturn; }

    }



     enum EWeaponType
    {
        Mele,
        Ranged
    }
     abstract class Weapon : Item
    {
        public EWeaponType mWeaponType = new EWeaponType();
        public int minMagicalAttack { get; }
        public int maxMagicalAttack { get; }

        public Weapon(EWeaponType mweapontype, int minmagicalattack, int maxmagicalattack, string name)
            : base(EItemType.Weapon, EItemFunctionality.Wearable, name)
        {

            minMagicalAttack = minmagicalattack;

            maxMagicalAttack = maxmagicalattack;
        }
    }
     abstract class MeleWeapon : Weapon
    {
        public int minPhysicalAttack { get; }
        public int maxPhysicalAttack { get; }

        public MeleWeapon(int minphysicalattack, int maxphysicalattack,
                      int minmagicalattack, int maxmagicalattack, string name)
            : base(EWeaponType.Mele, minmagicalattack, maxmagicalattack, name)
        {
            minPhysicalAttack = minphysicalattack;
            maxPhysicalAttack = maxphysicalattack;
        }
    }
     abstract class RangedWeapon : Weapon
    {
        public int minRangedPhysicalAttack { get; }
        public int maxRangedPhysicalAttack { get; }

        public RangedWeapon(int minrangedphysicalattack, int maxrangedphysicalattack,
                      int minmagicalattack, int maxmagicalattack, string name)
            : base(EWeaponType.Ranged, minmagicalattack, maxmagicalattack, name)
        {
            minRangedPhysicalAttack = minrangedphysicalattack;
            maxRangedPhysicalAttack = maxrangedphysicalattack;
        }
    }



     enum EAppearalBodyPart
    {
        Helmet,
        Chest,
        Gauntlets,
        Boots
    }
     abstract class Appearal : Item
    {
        public int physicalArmor;
        public int magicalArmor;
        public EAppearalBodyPart mBodyPart = new EAppearalBodyPart();

        public Appearal(int physicalarmor, int magicalarmor, EAppearalBodyPart bodyPart, string name)
            : base(EItemType.Appearal, EItemFunctionality.Wearable, name)
        {
            physicalArmor = physicalarmor;
            magicalArmor = magicalarmor;
            mBodyPart = bodyPart;
        }
    }


    #region Tier 1 items

     class MinorHealingPotion : HpPotion
    {

        public MinorHealingPotion(EItemType mT = EItemType.Potion, EItemFunctionality mF = EItemFunctionality.Consumable, EPotionType mType = EPotionType.Health, string name = "Heal_Potion_1", int hptoreturn = 10)
            : base(mT, mF, mType, name, hptoreturn) { }
    }


     class Plate_Armor_1 : Appearal
    {
        public Plate_Armor_1(int physicalarmor = 14, int magicalarmor = 10, EAppearalBodyPart bodyPart = EAppearalBodyPart.Chest, string name = "Plate_Armor_1")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }
    }

     class Metal_Helmet_1 : Appearal
    {
        public Metal_Helmet_1(int physicalarmor = 8, int magicalarmor = 4, EAppearalBodyPart bodyPart = EAppearalBodyPart.Helmet, string name = "Metal_Helmet_1")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }
    }

    class Furious_Boots : Appearal
    {

        public Furious_Boots(int physicalarmor = 6, int magicalarmor = 5, EAppearalBodyPart bodyPart = EAppearalBodyPart.Boots, string name = "Furious_Boots")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }

    }

    class Leather_Gloves : Appearal
    {

        public Leather_Gloves(int physicalarmor = 5, int magicalarmor = 5, EAppearalBodyPart bodyPart = EAppearalBodyPart.Gauntlets, string name = "Leather_Gloves")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }

    }

     class Sword_1 : MeleWeapon
    {
        public Sword_1(int minphysicalattack = 2, int maxphysicalattack = 4,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Sword_1")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

    class Axe_1 : MeleWeapon
    {
        public Axe_1(int minphysicalattack = 3, int maxphysicalattack = 4,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Axe_1")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

     class Claw_1 : MeleWeapon
    {
        public Claw_1(int minphysicalattack = 1, int maxphysicalattack = 2,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Claw_1")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

     class Magic_Staff_1 : MeleWeapon
    {

        public Magic_Staff_1(int minphysicalattack = 0, int maxphysicalattack = 0,
                      int minmagicalattack = 3, int maxmagicalattack = 5, string name = "Magic_Staff_1")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }

    }

     class Magic_Wand : MeleWeapon
    {

        public Magic_Wand(int minphysicalattack = 0, int maxphysicalattack = 0,
                      int minmagicalattack = 1, int maxmagicalattack = 1, string name = "Magic_Wand")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }

    }

    class Enchanted_Rod : MeleWeapon
    {

        public Enchanted_Rod(int minphysicalattack = 0, int maxphysicalattack = 0,
                      int minmagicalattack = 3, int maxmagicalattack = 7, string name = "Enchanted_Rod")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }

    }

     class Bow_1 : RangedWeapon
    {
        public Bow_1(int minrangedphysicalattack = 1, int maxrangedphysicalattack = 5,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Bow_1")
            : base(minrangedphysicalattack, maxrangedphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }

    }

    class Slingshot : RangedWeapon
    {
        public Slingshot(int minrangedphysicalattack = 2, int maxrangedphysicalattack = 4,
              int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Bow_1")
            : base(minrangedphysicalattack, maxrangedphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }

    }

    class Metal_Fists_1 : MeleWeapon
    {
        public Metal_Fists_1(int minphysicalattack = 1, int maxphysicalattack = 2,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Metal_Fists_1")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name) { }
    }
    #endregion

    #region Tier 2 items

     class MightyMace : MeleWeapon
    {
        public MightyMace(int minphysicalattack = 5, int maxphysicalattack = 9,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Mighty_Mace")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

     class StormHammer : MeleWeapon
    {
        public StormHammer(int minphysicalattack = 4, int maxphysicalattack = 8,
                      int minmagicalattack = 2, int maxmagicalattack = 2, string name = "StormHammer")
             : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

     class BiggusSwordus : MeleWeapon
    {
        public BiggusSwordus(int minphysicalattack = 5, int maxphysicalattack = 12,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Biggus_Swordus")
             : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        { }
    }

     class MediumHealingPotion : HpPotion
    {
        public MediumHealingPotion(EItemType mT = EItemType.Potion, EItemFunctionality mF = EItemFunctionality.Consumable, EPotionType mType = EPotionType.Health, string name = "Heal_Potion_2", int hptoreturn = 30)
            : base(mT, mF, mType, name, hptoreturn) { }
    }

     class OversizedHelmet : Appearal
    {
        public OversizedHelmet(int physicalarmor = 20, int magicalarmor = 14, EAppearalBodyPart bodyPart = EAppearalBodyPart.Helmet, string name = "Oversized_Helmet")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }
    }

     class FluffyArmor : Appearal
    {
        public FluffyArmor(int physicalarmor = 30, int magicalarmor = 30, EAppearalBodyPart bodyPart = EAppearalBodyPart.Chest, string name = "Fluffy_Armor")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        {

        }
    }

     class PinkGauntlets : Appearal
    {
        public PinkGauntlets(int physicalarmor = 20, int magicalarmor = 15, EAppearalBodyPart bodyPart = EAppearalBodyPart.Gauntlets, string name = "Pink_Gauntlets")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        { }
    }

    class HighHeelShoes : Appearal
    {
        public HighHeelShoes(int physicalarmor = 15, int magicalarmor = 15, EAppearalBodyPart bodyPart = EAppearalBodyPart.Boots, string name = "High_Heel_Shoes")
            : base(physicalarmor, magicalarmor, bodyPart, name)
        { }
    }

     class Crossbow : RangedWeapon
    {

        public Crossbow(int minrangedphysicalattack = 5, int maxrangedphysicalattack = 13,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Crossbow")
            : base(minrangedphysicalattack, maxrangedphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }

    }

     class GatlingGun : RangedWeapon
    {
        public GatlingGun(int minrangedphysicalattack = 8, int maxrangedphysicalattack = 10,
                      int minmagicalattack = 0, int maxmagicalattack = 0, string name = "Crossbow")
            : base(minrangedphysicalattack, maxrangedphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }
    }

    class StaffOfFlames : MeleWeapon
    {
        public StaffOfFlames(int minphysicalattack = 0, int maxphysicalattack = 0,
                      int minmagicalattack = 7, int maxmagicalattack = 11, string name = "Staff_Of_Flames")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }
    }

     class LigtningBolt : MeleWeapon
    {
        public LigtningBolt(int minphysicalattack = 0, int maxphysicalattack = 0,
                      int minmagicalattack = 11, int maxmagicalattack = 13, string name = "Lightning_Bolt")
            : base(minphysicalattack, maxphysicalattack, minmagicalattack, maxmagicalattack, name)
        {

        }
    }

    #endregion



}
