using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
     abstract class Character
    {
        public BasicStats mBasicStats = new BasicStats();
        public Attributes mAttributes = new Attributes();
        public CombatAbilities mCombatAbilities = new CombatAbilities();
        public Inventory mInventory = new Inventory();//i will probably create class for inventory as i will be needin' some methods to manage things
        public string name;
        public Random randomValue = new Random();

        public Character(string n)
        {
            name = n;
            mInventory.mEquipedItems["chest"] = null;
            mInventory.mEquipedItems["weapon"] = null;
            mInventory.mEquipedItems["boots"] = null;
            mInventory.mEquipedItems["gauntlets"] = null;

            calculateBasicStats();
            // displayStats();
        }

        public virtual void attack(Character target)
        {
            int physicalDmgToDeal = calculatePhysicalDamage();
            int magicalDmgToDeal = calculateMagicalDamage();

            #region Taking physical armor into account
            // Console.WriteLine("{1} deals {0} dmg to {2}", physicalDmgToDeal,name,target.name);
            //  Console.WriteLine("Enemy had {0} armor", target.mBasicStats.currentPhysicalArmor);
            physicalDmgToDeal -= target.mBasicStats.currentPhysicalArmor;

            //dmg=10 def=5        (line 156)( 10 - 5 = 5 )  we ate whole armor so we set it to 0
            if (physicalDmgToDeal >= 0) target.mBasicStats.currentPhysicalArmor = 0;
            //dmg=10 def=60       (line 156)( 10 - 60 = -50 )  -(-50) = 50, our current armor
            if (physicalDmgToDeal < 0)
            {
                target.mBasicStats.currentPhysicalArmor = -physicalDmgToDeal;
                physicalDmgToDeal = 0;//so that we don't give them hp :)
            }

            #endregion

            magicalDmgToDeal -= target.mBasicStats.currentMagicalArmor;

            //dmg=10 def=5        (line 156)( 10 - 5 = 5 )  we ate whole armor so we set it to 0
            if (magicalDmgToDeal >= 0) target.mBasicStats.currentMagicalArmor = 0;
            //dmg=10 def=60       (line 156)( 10 - 60 = -50 )  -(-50) = 50, our current armor
            if (magicalDmgToDeal < 0)
            {
                target.mBasicStats.currentMagicalArmor = -magicalDmgToDeal;
                magicalDmgToDeal = 0;//so that we don't give them hp :)
            }


            //maybe some check that if target dies then some perk of his might revive him or cause some effect
            // Console.WriteLine("Enemy had {0} endurance", target.mBasicStats.currentEndurance);
            target.mBasicStats.currentEndurance -= physicalDmgToDeal;
            target.mBasicStats.currentEndurance -= magicalDmgToDeal;
            // Console.WriteLine("{2} now has {0} endurance, and {1} armor", target.mBasicStats.currentEndurance, target.mBasicStats.currentPhysicalArmor,target.name);
        }

        public void calculateBasicStats()
        {
            mBasicStats.maxEndurance = mBasicStats.currentLevel * (mBasicStats.baseEndurance + mAttributes.Constitiution * 4);
            mBasicStats.currentEndurance = mBasicStats.maxEndurance;

            mBasicStats.maxHp = mBasicStats.currentLevel * (mBasicStats.baseHp + mAttributes.Constitiution * 10);
            mBasicStats.currentHp = mBasicStats.maxHp;

            mBasicStats.maxMana = mBasicStats.currentLevel * (5 + mAttributes.Knowledge * 10);
            mBasicStats.currentMana = mBasicStats.maxMana;

            mBasicStats.minPhysicalAttack = mBasicStats.currentLevel * 1 + (int)(mBasicStats.currentLevel * 0.2 * mAttributes.Strength * 2);
            mBasicStats.maxPhysicalAttack = mBasicStats.minPhysicalAttack;// 1-1 dmg 2-2 dmg - base value 

            mBasicStats.minMagicalAttack = mBasicStats.currentLevel * 1 + (int)(mBasicStats.currentLevel * 0.2 * mAttributes.Intelligence * 2);
            mBasicStats.maxMagicalAttack = mBasicStats.minMagicalAttack;

            mBasicStats.minRangedPhysicalAttack = mBasicStats.currentLevel * 1 + (int)(mBasicStats.currentLevel * 0.2 * mAttributes.Agility * 2);
            mBasicStats.maxRangedPhysicalAttack = mBasicStats.minRangedPhysicalAttack;



        }

        public void levelUp()
        {
            mBasicStats.currentLevel += 1;
            //add one point to Attributes
            bool attributeMenu = true;
            while (attributeMenu)
            {
                Console.Clear();
                Console.WriteLine("{0} has reached lv.{1}", name, mBasicStats.currentLevel);
                Console.WriteLine("0) Strength: {0}", mAttributes.Strength);
                Console.WriteLine("1) Agility: {0}", mAttributes.Agility);
                Console.WriteLine("2) Constitiution: {0}", mAttributes.Constitiution);
                Console.WriteLine("3) Intelligence: {0}", mAttributes.Intelligence);
                // Console.WriteLine("4) Knowledge: {0}\n", mAttributes.Knowledge);

                Console.WriteLine("Select one attribute to increase it's value (0 - 3)");

                ConsoleKeyInfo cki = new ConsoleKeyInfo();
                while ((int)cki.Key < 48 || (int)cki.Key > 51)
                {

                    cki = Console.ReadKey(true);

                    //if ((int)cki.Key >= 48 && (int)cki.Key <= 51) break;

                }

                int index = (int)cki.Key % 48;
                if (index == 0) mAttributes.Strength += 1;
                else if (index == 1) mAttributes.Agility += 1;
                else if (index == 2) mAttributes.Constitiution += 1;
                else if (index == 3) mAttributes.Intelligence += 1;
                //else if (index == 4) mAttributes.Knowledge += 1;

                Console.Clear();
                Console.WriteLine("0) Strength: {0}", mAttributes.Strength);
                Console.WriteLine("1) Agility: {0}", mAttributes.Agility);
                Console.WriteLine("2) Constitiution: {0}", mAttributes.Constitiution);
                Console.WriteLine("3) Intelligence: {0}", mAttributes.Intelligence);
                // Console.WriteLine("4) Knowledge: {0}\n", mAttributes.Knowledge);
                Console.WriteLine("Do you want to keep these values?( y\\n )");


                bool proceed = false;
                cki = new ConsoleKeyInfo();
                while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N)
                {

                    cki = Console.ReadKey(true);

                    if (cki.Key == ConsoleKey.Y) { proceed = true; break; }
                    else if (cki.Key == ConsoleKey.N) { proceed = false; break; }

                }

                if (!proceed)
                {

                    if (index == 0) mAttributes.Strength -= 1;
                    else if (index == 1) mAttributes.Agility -= 1;
                    else if (index == 2) mAttributes.Constitiution -= 1;
                    else if (index == 3) mAttributes.Intelligence -= 1;
                    // else if (index == 4) mAttributes.Knowledge -= 1;

                }
                else { attributeMenu = false; }


            }//Attributes selected
            calculateBasicStats();


            //increase experience cap
            mBasicStats.currentExperience = 0;
            mBasicStats.experienceToNextLv = (int)((300 + mBasicStats.currentLevel * 100) * 1.25);


        }

        public void displayStats()
        {

            Console.WriteLine("####################");
            Console.WriteLine(name);
            Console.WriteLine("--------------------");
            Console.WriteLine("\nAttributes\n");
            Console.WriteLine("Strength: {0}", mAttributes.Strength);
            Console.WriteLine("Constitiution: {0}", mAttributes.Constitiution);
            Console.WriteLine("Agility: {0}", mAttributes.Agility);
            Console.WriteLine("Intelligence: {0}", mAttributes.Intelligence);
            //Console.WriteLine("Knowledge: {0}", mAttributes.Knowledge);

            Console.WriteLine("--------------------");

            Console.WriteLine("BasicStats\n");
            Console.WriteLine("Max endurance: {0}", mBasicStats.maxEndurance);
            Console.WriteLine("Current endurance: {0}", mBasicStats.currentEndurance);
            Console.WriteLine("Max hp: {0}", mBasicStats.maxHp);
            Console.WriteLine("Current hp: {0}", mBasicStats.currentHp);
            Console.WriteLine("Physical armor: {0}", calculateMaxPhysicalArmor());
            Console.WriteLine("Magical armor: {0}", calculateMaxMagicalArmor());
            // Console.WriteLine("Max mana: {0}", mBasicStats.maxMana);
            // Console.WriteLine("Current mana: {0}", mBasicStats.currentMana);

            // Console.WriteLine("Max physicalAttack: {0}", mBasicStats.maxPhysicalAttack);
            //Console.WriteLine("Min physicalAttack: {0}", mBasicStats.minPhysicalAttack);
            // Console.WriteLine("Max physicalRangedAttack: {0}", mBasicStats.maxRangedPhysicalAttack);
            // Console.WriteLine("Min physicalRangedAttack: {0}", mBasicStats.minRangedPhysicalAttack);
            // Console.WriteLine("Max magicalAttack: {0}", mBasicStats.maxMagicalAttack);
            //Console.WriteLine("Min magicalAttack: {0}", mBasicStats.minMagicalAttack);

            Console.WriteLine("\n");

        }
        public void displayFightInfo()
        {
            Console.WriteLine("-------------");
            Console.WriteLine("Character: {0}", name);
            Console.WriteLine("Status: {0}", mBasicStats.exhausted ? "EXHAUSTED" : "READY");
            Console.WriteLine("Current healthbank: {0} ", mBasicStats.currentHp);
            Console.WriteLine("Current endurance: {0}", mBasicStats.currentEndurance);
            Console.WriteLine("Physical Armor: {0}", mBasicStats.currentPhysicalArmor);
            Console.WriteLine("Magical Armor: {0}", mBasicStats.currentMagicalArmor);
            Console.WriteLine("Physical dmg: {0} - {1}", calculateMinPhysicalAttack(), calculateMaxPhysicalAttack());
            Console.WriteLine("Magical dmg: {0} - {1}", calculateMinMagicalAttack(), calculateMaxMagicalAttack());
            Console.WriteLine("-------------");
        }

        public int calculateMinPhysicalAttack()
        {
            int minAttack;
            if (mInventory.mEquipedItems["weapon"] != null)
            {
                if ((mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack == 0)
                {
                    if (mInventory.mEquipedItems["weapon"] as MeleWeapon != null)
                    { //strength there as multiplier
                        minAttack = mBasicStats.minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack) * 0.4 * mAttributes.Strength);
                    }
                    else
                    {
                        minAttack = mBasicStats.minRangedPhysicalAttack + (mInventory.mEquipedItems["weapon"] as RangedWeapon).minRangedPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as RangedWeapon).minRangedPhysicalAttack + (mInventory.mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack) * 0.4 * mAttributes.Agility);
                    }
                }
                else
                {
                    minAttack = 0;
                }
            }
            else
            {
                minAttack = mBasicStats.minPhysicalAttack;
            }
            return minAttack;
        }
        public int calculateMaxPhysicalAttack()
        {
            int maxAttack;
            if (mInventory.mEquipedItems["weapon"] != null)
            {
                if ((mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack == 0)
                {
                    if (mInventory.mEquipedItems["weapon"] as MeleWeapon != null)
                    { //strength there as multiplier
                        maxAttack = mBasicStats.maxPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack) * 0.4 * mAttributes.Strength);
                    }
                    else
                    {
                        maxAttack = mBasicStats.maxRangedPhysicalAttack + (mInventory.mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as RangedWeapon).minRangedPhysicalAttack + (mInventory.mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack) * 0.4 * mAttributes.Agility);
                    }
                }
                else
                {
                    maxAttack = 0;
                }
            }
            else
            {
                maxAttack = mBasicStats.maxPhysicalAttack;
            }
            return maxAttack;
        }
        public int calculateMinMagicalAttack()
        {
            int minAttack = 0;

            if (mInventory.mEquipedItems["weapon"] != null)
            {
                if ((mInventory.mEquipedItems["weapon"] as Weapon).minMagicalAttack != 0)
                {//if weapon has magical dmg
                    minAttack = mBasicStats.minMagicalAttack + (mInventory.mEquipedItems["weapon"] as Weapon).minMagicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as Weapon).minMagicalAttack + (mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack) * 0.4 * mAttributes.Intelligence);
                }
                else
                {//if weapon has no magical dmg
                    minAttack = 0;
                }
            }
            else
            {
                //without a weapon
                minAttack = 0; //mBasicStats.minMagicalAttack;          
            }

            return minAttack;
        }
        public int calculateMaxMagicalAttack()
        {
            int maxAttack = 0;

            if (mInventory.mEquipedItems["weapon"] != null)
            {
                if ((mInventory.mEquipedItems["weapon"] as Weapon).minMagicalAttack != 0)
                {//if weapon has magical dmg
                    maxAttack = mBasicStats.maxMagicalAttack + (mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as Weapon).minMagicalAttack + (mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack) * 0.4 * mAttributes.Intelligence);
                }
                else
                {//if weapon has no magical dmg
                    maxAttack = 0;
                }
            }
            else
            {
                //without a weapon
                maxAttack = 0;// mBasicStats.maxMagicalAttack;
            }

            return maxAttack;
        }

        public virtual int calculatePhysicalDamage()
        {
            int minAttack = calculateMinPhysicalAttack();
            int maxAttack = calculateMaxPhysicalAttack();

            int difference = (maxAttack - minAttack) + 1;
            int reminder = randomValue.Next() % difference;
            int dmgToDeal = minAttack + reminder;

            return dmgToDeal;
        }
        public virtual int calculateMagicalDamage()
        {
            int minAttack = calculateMinMagicalAttack();
            int maxAttack = calculateMaxMagicalAttack();
            #region old
            /*
            if (mEquipedItems["weapon"] != null)
            {
                if ((mEquipedItems["weapon"] as Weapon).minMagicalAttack != 0)
                {//if weapon has magical dmg
                    minAttack = mBasicStats.minMagicalAttack + (mEquipedItems["weapon"] as Weapon).minMagicalAttack + (int)(((mEquipedItems["weapon"] as Weapon).minMagicalAttack + (mEquipedItems["weapon"] as Weapon).maxMagicalAttack) * 0.4 * mAttributes.Intelligence);
                    maxAttack = mBasicStats.maxMagicalAttack + (mEquipedItems["weapon"] as Weapon).maxMagicalAttack + (int)(((mEquipedItems["weapon"] as Weapon).minMagicalAttack + (mEquipedItems["weapon"] as Weapon).maxMagicalAttack) * 0.4 * mAttributes.Intelligence);
                }
                else
                {//if weapon has no magical dmg
                    minAttack = 0; 
                    maxAttack = 0;
                }
            }
            else
            {

                //without a weapon
                minAttack = 0; //mBasicStats.minMagicalAttack;
                maxAttack = 0;// mBasicStats.maxMagicalAttack;
            }*/
            #endregion
            int difference = (maxAttack - minAttack) + 1;
            int reminder = randomValue.Next() % difference;
            int dmgToDeal = minAttack + reminder;

            return dmgToDeal;
        }

        public virtual int calculateMaxPhysicalArmor()
        {
            int armorToReturn = mBasicStats.maxPhysicalArmor;


            if (mInventory.mEquipedItems["chest"] != null)
                armorToReturn += (mInventory.mEquipedItems["chest"] as Appearal).physicalArmor;
            if (mInventory.mEquipedItems["helmet"] != null)
                armorToReturn += (mInventory.mEquipedItems["helmet"] as Appearal).physicalArmor;
            if (mInventory.mEquipedItems["gauntlets"] != null)
                armorToReturn += (mInventory.mEquipedItems["gauntlets"] as Appearal).physicalArmor;
            if (mInventory.mEquipedItems["boots"] != null)
                armorToReturn += (mInventory.mEquipedItems["boots"] as Appearal).physicalArmor;

            return armorToReturn;
        }
        public virtual int calculateMaxMagicalArmor()
        {
            int armorToReturn = mBasicStats.maxMagicalArmor;


            if (mInventory.mEquipedItems["chest"] != null)
                armorToReturn += (mInventory.mEquipedItems["chest"] as Appearal).magicalArmor;
            if (mInventory.mEquipedItems["helmet"] != null)
                armorToReturn += (mInventory.mEquipedItems["helmet"] as Appearal).magicalArmor;
            if (mInventory.mEquipedItems["gauntlets"] != null)
                armorToReturn += (mInventory.mEquipedItems["gauntlets"] as Appearal).magicalArmor;
            if (mInventory.mEquipedItems["boots"] != null)
                armorToReturn += (mInventory.mEquipedItems["boots"] as Appearal).magicalArmor;

            return armorToReturn;
        }
        public virtual void castSpell(Character target, int spell) { }//to be implemented :)



        public void prepareForCombat()
        {
            mBasicStats.currentMagicalArmor = calculateMaxMagicalArmor();
            mBasicStats.currentPhysicalArmor = calculateMaxPhysicalArmor();
        }

        public void finishCombat()
        {
            if (mBasicStats.currentEndurance < 0) mBasicStats.currentEndurance = 0;

            int missing_hp = mBasicStats.maxEndurance - mBasicStats.currentEndurance;
            int hp_to_refuel;
            if (missing_hp > mBasicStats.currentHp)
                hp_to_refuel = mBasicStats.currentHp;
            else
                hp_to_refuel = missing_hp;
            mBasicStats.currentHp -= hp_to_refuel;
            mBasicStats.currentEndurance += hp_to_refuel;
        }




    }

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
            /*
            mBasicStats.currentLevel += 1;
            calculateBasicStats();
            mBasicStats.minMagicalAttack = 5;
            mBasicStats.maxMagicalAttack = 10;*/
        }

    }

     class Goblin_1 : Character
    {
        public Goblin_1() : base("Goblin_1")
        {
            mBasicStats.minPhysicalAttack = 2;
            mBasicStats.maxPhysicalAttack = 2;
        }

    }
    #endregion

    #region Tier_2 characters

     class Watcher : Character
    {
        public Watcher() : base("Watcher")
        {
            mBasicStats.currentLevel += 1;
            calculateBasicStats();
            mBasicStats.minPhysicalAttack = 3;
            mBasicStats.maxPhysicalAttack = 4;
        }
    }

     class CorruptedSanta : Character
    {

        public CorruptedSanta() : base("Corrupted_Santa")
        {
            mBasicStats.currentLevel += 1;
            calculateBasicStats();
            mBasicStats.minMagicalAttack = 2;
            mBasicStats.maxMagicalAttack = 4;
            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }

    }

     class FrontEndWannabe : Character
    {
        public FrontEndWannabe() : base("FrontEndWannabe")
        {
            mBasicStats.currentLevel += 1;
            calculateBasicStats();
            mBasicStats.minMagicalAttack = 2;
            mBasicStats.maxMagicalAttack = 3;
            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }

    }

     class ViciousScrumMaster : Character
    {
        public ViciousScrumMaster() : base("ViciousScrumMaster")
        {
            mBasicStats.currentLevel += 1;
            calculateBasicStats();
            mBasicStats.minPhysicalAttack = 4;
            mBasicStats.maxPhysicalAttack = 4;
        }
    }

    #endregion

    #region Tier_3 characters

     class HeadlessHead : Character
    {
        public HeadlessHead() : base("HeadlessHead")
        {
            mBasicStats.currentLevel += 2;
            calculateBasicStats();
            mBasicStats.minPhysicalAttack = 4;
            mBasicStats.maxPhysicalAttack = 8;

            mBasicStats.currentPhysicalArmor = 30;
            mBasicStats.currentMagicalArmor = 30;
        }
    }
     class AbusiveJanitor : Character
    {
        public AbusiveJanitor() : base("AbusiveJanitor")
        {
            mBasicStats.currentLevel += 2;
            calculateBasicStats();
            mBasicStats.minPhysicalAttack = 7;
            mBasicStats.maxPhysicalAttack = 10;

            mBasicStats.currentPhysicalArmor = 15;

        }
    }
     class ImpressiveAnt : Character
    {
        public ImpressiveAnt() : base("ImpressiveAnt")
        {
            mBasicStats.currentLevel += 2;
            calculateBasicStats();
            mBasicStats.minMagicalAttack = 6;
            mBasicStats.maxMagicalAttack = 8;

            mBasicStats.currentPhysicalArmor = 40;
            mBasicStats.currentMagicalArmor = 40;

            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }
    }
     class ShySuccubus : Character
    {
        public ShySuccubus() : base("ShySuccubus")
        {
            mBasicStats.currentLevel += 2;
            calculateBasicStats();
            mBasicStats.minMagicalAttack = 5;
            mBasicStats.maxMagicalAttack = 12;

            mBasicStats.currentPhysicalArmor = 20;

            mInventory.mEquipedItems["weapon"] = new Magic_Wand();
        }
    }

    #endregion


     class Team
    {
        public List<Character> mCharacters = new List<Character>();
        public string teamName;
        public int goldToPillage = 0;
        public RoomInventory teamInventory = new RoomInventory();
        public Team() { }
        public Team(List<Character> characters) { mCharacters = characters; }


        public bool alive()
        {

            bool atLeastSomeoneAlive = false;
            for (int i = 0; i < mCharacters.Count; i++)
            {
                if (mCharacters[i].mBasicStats.currentEndurance > 0) atLeastSomeoneAlive = true;
            }

            return atLeastSomeoneAlive;
        }

        public void resetExhausted()
        {
            for (int i = 0; i < mCharacters.Count; i++)
            {
                mCharacters[i].mBasicStats.exhausted = false;
            }
        }

        public void displayFightInfo()
        {
            for (int i = 0; i < mCharacters.Count; i++)
            {
                ///mCharacters[i].displayFightInfo();
                /*Console.WriteLine("-------------");
                Console.WriteLine("Character: {0}",name);
                Console.WriteLine("Status: {0}",mBasicStats.exhausted ? "EXHAUSTED" : "READY");
                Console.WriteLine("Current healthbank: {0} ", mBasicStats.currentHp);
                Console.WriteLine("Current endurance: {0}", mBasicStats.currentEndurance);
                Console.WriteLine("Physical Armor: {0}",mBasicStats.currentPhysicalArmor);
                Console.WriteLine("Magical Armor: {0}", mBasicStats.currentMagicalArmor);
                Console.WriteLine("Physical dmg: {0} - {1}", calculateMinPhysicalAttack(), calculateMaxPhysicalAttack());
                Console.WriteLine("Magical dmg: {0} - {1}",calculateMinMagicalAttack(),calculateMaxMagicalAttack());
                 Console.WriteLine("-------------");*/
            }

            for (int i = 0; i < mCharacters.Count; i++) { String s = "Index: " + i.ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Character: " + mCharacters[i].name; Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Status: " + (mCharacters[i].mBasicStats.exhausted ? "EXHAUSTED" : "READY"); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Current healthbank: " + mCharacters[i].mBasicStats.currentHp.ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Current endurance: " + mCharacters[i].mBasicStats.currentEndurance.ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Physical Armor: " + mCharacters[i].mBasicStats.currentPhysicalArmor.ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Magical Armor: " + mCharacters[i].mBasicStats.currentMagicalArmor.ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Physical dmg: " + mCharacters[i].calculateMinPhysicalAttack().ToString() + " - " + mCharacters[i].calculateMaxPhysicalAttack().ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();
            for (int i = 0; i < mCharacters.Count; i++) { String s = "Magical dmg: " + mCharacters[i].calculateMinMagicalAttack().ToString() + " - " + mCharacters[i].calculateMaxMagicalAttack().ToString(); Console.Write(s.PadRight(40)); }
            Console.WriteLine();


            Console.WriteLine();

        }

        public List<int> getActiveCharacters()//Get the indices of currently active characters (alive and not exhausted)
        {
            List<int> listToReturn = new List<int>();

            for (int i = 0; i < mCharacters.Count; i++)
            {
                if (mCharacters[i].mBasicStats.exhausted == false && mCharacters[i].mBasicStats.currentEndurance > 0)
                    listToReturn.Add(i);
            }

            return listToReturn;
        }

        public List<int> getAliveCharacters()//Get the indices of currently alive characters
        {
            List<int> listToReturn = new List<int>();

            for (int i = 0; i < mCharacters.Count; i++)
            {
                if (mCharacters[i].mBasicStats.currentEndurance > 0)
                    listToReturn.Add(i);
            }

            return listToReturn;
        }


    }

     class Combat
    {
        Random rnd = new Random();
        Team Player;
        Team Enemy;
        public Combat(Team player, Team enemy)
        {
            Player = player;
            Enemy = enemy;
        }

        public bool start()
        {

            for (int i = 0; i < Player.mCharacters.Count; i++)
                Player.mCharacters[i].prepareForCombat();

            //while both teams alive
            while (Player.alive() && Enemy.alive())
            {

                bool player_did_his_furn = false;
                while (!player_did_his_furn)
                {
                    //display all characters

                    Console.Clear();
                    Console.WriteLine("Team: {0}", Player.teamName);
                    Player.displayFightInfo();
                    Console.WriteLine("Team: {0}", Enemy.teamName);
                    Enemy.displayFightInfo();


                    //process player's turn
                    Console.WriteLine("\n\nSelect character from your team by typing in his index");

                    List<int> characterIndices = Player.getActiveCharacters();
                    //select valid character

                    ConsoleKeyInfo cki = new ConsoleKeyInfo();
                    while ((int)cki.Key < 48 || (int)cki.Key > 50) //!= ConsoleKey.D1 )
                    {

                        cki = Console.ReadKey(true);
                        // Console.WriteLine("key: {0}, value {1}", cki.Key, (int)cki.Key);
                        if ((int)cki.Key >= 48 && (int)cki.Key <= 50)
                        {
                            if (characterIndices.Contains(((int)cki.Key) % 48)) break;
                            else cki = new ConsoleKeyInfo();
                        }
                        //break;//cki.Key == ConsoleKey.D1) break;

                    }
                    int heroIndex = (int)cki.Key % 48;
                    Console.WriteLine("You have selected: {0}", Player.mCharacters[heroIndex].name);

                    Console.WriteLine("What do you do?");
                    Console.WriteLine("0, to select other character");
                    Console.WriteLine("1, to attack");
                    Console.WriteLine("2, to open inventory");

                    cki = new ConsoleKeyInfo();
                    while ((int)cki.Key < 48 || (int)cki.Key > 50) //!= ConsoleKey.D1 )
                    {

                        cki = Console.ReadKey(true);
                        //Console.WriteLine("key: {0}, value {1}", cki.Key, (int)cki.Key);
                        if ((int)cki.Key >= 48 && (int)cki.Key <= 50)
                        {
                            break;
                        }
                        //break;//cki.Key == ConsoleKey.D1) break;

                    }

                    int playersChoice = (int)cki.Key % 48;

                    if (playersChoice == 0) continue;

                    if (playersChoice == 1)
                    {
                        Console.WriteLine("Select enemy's index");

                        List<int> enemyIndices = Enemy.getAliveCharacters();

                        cki = new ConsoleKeyInfo();
                        while ((int)cki.Key < 48 || (int)cki.Key > 50) //!= ConsoleKey.D1 )
                        {

                            cki = Console.ReadKey(true);
                            //Console.WriteLine("key: {0}, value {1}", cki.Key, (int)cki.Key);
                            if ((int)cki.Key >= 48 && (int)cki.Key <= 50)
                            {
                                if (enemyIndices.Contains(((int)cki.Key) % 48)) break;
                                else cki = new ConsoleKeyInfo();
                            }
                            //break;//cki.Key == ConsoleKey.D1) break;

                        }

                        int enemyIndex = (int)cki.Key % 48;

                        Player.mCharacters[heroIndex].attack(Enemy.mCharacters[enemyIndex]);

                        Player.mCharacters[heroIndex].mBasicStats.exhausted = true;
                        player_did_his_furn = true;
                    }

                    if (playersChoice == 2)//here inventory loop
                    {
                        bool loop = true;
                        while (loop)
                        {
                            Console.Clear();
                            Player.mCharacters[heroIndex].displayFightInfo();
                            Player.mCharacters[heroIndex].mInventory.displayCombatInventory();

                            Console.WriteLine();
                            Console.Write("".PadRight(5)); ;
                            Console.WriteLine("You can only drink potions while in combat");

                            cki = new ConsoleKeyInfo();
                            while (((int)cki.Key < 48 || (int)cki.Key > 55) && cki.Key != ConsoleKey.Escape) //!= ConsoleKey.D1 )
                            {




                                cki = Console.ReadKey(true);

                                //Console.WriteLine("key: {0}, value {1}", cki.Key, (int)cki.Key);
                                if ((int)cki.Key >= 48 && (int)cki.Key <= 55)
                                {//basic inventory
                                    int index = (int)cki.Key % 48;

                                    if ((Player.mCharacters[heroIndex].mInventory.mItems[index] as Potion) == null)
                                    {
                                        //no potion
                                        // cki = new ConsoleKeyInfo();
                                    }
                                    else
                                    {
                                        Potion mPotion = Player.mCharacters[heroIndex].mInventory.mItems[index] as Potion;

                                        if (mPotion.mPotionType == EPotionType.Health)
                                        {
                                            //drink potion
                                            int hpToReturn = (mPotion as HpPotion).hpToReturn;

                                            int missinghp = Player.mCharacters[heroIndex].mBasicStats.maxEndurance - Player.mCharacters[heroIndex].mBasicStats.currentEndurance;
                                            int upperBound;
                                            if (missinghp > hpToReturn)
                                                upperBound = hpToReturn;
                                            else
                                                upperBound = missinghp;

                                            if (upperBound > Player.mCharacters[heroIndex].mBasicStats.currentHp)
                                                upperBound = Player.mCharacters[heroIndex].mBasicStats.currentHp;
                                            //if()
                                            Player.mCharacters[heroIndex].mBasicStats.currentHp -= upperBound; //remove from hp
                                            Player.mCharacters[heroIndex].mBasicStats.currentEndurance += upperBound; //and add it to endurance
                                            Player.mCharacters[heroIndex].mInventory.mItems[index] = null; //remove item. I hope it wont break :)


                                            Console.Clear();
                                            Player.mCharacters[heroIndex].displayFightInfo();
                                            Player.mCharacters[heroIndex].mInventory.displayCombatInventory();

                                            Console.WriteLine();
                                            Console.Write("".PadRight(5)); ;
                                            Console.WriteLine("You can only drink potions while in combat");
                                        }

                                    }
                                }
                                if (cki.Key == ConsoleKey.Escape)
                                {
                                    //Console.WriteLine("key: {0}, value {1}", cki.Key, (int)cki.Key);
                                    loop = false;
                                    break;
                                }
                                //break;//cki.Key == ConsoleKey.D1) break;

                            }


                        }
                    }
                }//player's turn
                 //if every hero is exhausted then we reset this flag
                if (Player.getActiveCharacters().Count == 0) Player.resetExhausted();



                //process enemy's turn

                if (!Enemy.alive()) break;
                List<int> targetForEnemy = Player.getAliveCharacters();
                if (Enemy.getActiveCharacters().Count == 0) Enemy.resetExhausted();
                List<int> activeEnemies = Enemy.getActiveCharacters();

                Enemy.mCharacters[activeEnemies[0]].attack(Player.mCharacters[targetForEnemy[rnd.Next() % targetForEnemy.Count]]);
                Enemy.mCharacters[activeEnemies[0]].mBasicStats.exhausted = true;

                //enemy might have killed guy who was ready while other was exhausted so we set his flag to ready
                if (Player.getActiveCharacters().Count == 0) Player.resetExhausted();


            }//battle loop

            //battle has finished now determine who has won

            Console.Clear();

            for (int i = 0; i < Player.mCharacters.Count; i++)
                Player.mCharacters[i].finishCombat();

            if (Enemy.alive()) { Console.WriteLine("Enemy has won\nPress any key to continue..."); Console.ReadKey(); return false; }
            else { Console.WriteLine("Player has won\nPress any key to continue..."); Console.ReadKey(); return true; }



        } //true = player has won ; false = enemy has won

    }

    class Inventory
    {

        public Item[] mItems = new Item[9];
        public Dictionary<string, Item> mEquipedItems = new Dictionary<string, Item>();

        private const int INVENTORY_SIZE = 9;

        public Inventory()
        {
            for (int i = 0; i < INVENTORY_SIZE; i++)
            {

                mItems[i] = null;
            }

            mEquipedItems["chest"] = null;
            mEquipedItems["weapon"] = null;
            mEquipedItems["boots"] = null;
            mEquipedItems["helmet"] = null;
            mEquipedItems["gauntlets"] = null;

        }

        public Item releaseItem(int index)
        {
            Item i = mItems[index];
            mItems[index] = null;
            return i;
        }

        public void displayInventory()
        {

            displayEquippedItems();

            displayBackpack();


            Console.Write("".PadRight(5));
            Console.WriteLine("(0 - 8) to access inventory item, (e - i) to access equipped items, Esc to quit");
        }//during fight there will be no menu just drink potion(if the potion is in the inventory)

        public void displayCombatInventory()
        {


            displayBackpack();

            Console.Write("".PadRight(5));
            Console.WriteLine("(0 - 8) to access inventory item, Escape to quit");
        }

        private void displayEquippedItems()
        {
            Console.WriteLine();


            Console.WriteLine("Equipped items".PadLeft(35));
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("".PadRight(5));
            Console.Write("e) Helmet: ");
            if (mEquipedItems["helmet"] != null) Console.WriteLine(mEquipedItems["helmet"].mName);
            else Console.WriteLine("None");
            Console.WriteLine();

            Console.Write("".PadRight(5));
            Console.Write("f) Chest: ");
            if (mEquipedItems["chest"] != null) Console.WriteLine(mEquipedItems["chest"].mName);
            else Console.WriteLine("None");
            Console.WriteLine();

            Console.Write("".PadRight(5));
            Console.Write("g) Gauntlets: ");
            if (mEquipedItems["gauntlets"] != null) Console.WriteLine(mEquipedItems["gauntlets"].mName);
            else Console.WriteLine("None");
            Console.WriteLine();

            Console.Write("".PadRight(5));
            Console.Write("h) Boots: ");
            if (mEquipedItems["boots"] != null) Console.WriteLine(mEquipedItems["boots"].mName);
            else Console.WriteLine("None");
            Console.WriteLine();

            Console.Write("".PadRight(5));
            Console.Write("i) Weapon: ");
            if (mEquipedItems["weapon"] != null) Console.WriteLine(mEquipedItems["weapon"].mName);
            else Console.WriteLine("None");
            Console.WriteLine();
        }
        private void displayBackpack()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Inventory".PadLeft(35));
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("".PadRight(15));
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i.ToString().PadRight(20));
            }

            Console.WriteLine();

            Console.Write("".PadRight(15));
            for (int i = 0; i < 3; i++)
            {
                if (mItems[i] == null) Console.Write("Nothing".PadRight(20));
                else Console.Write(mItems[i].mName.PadRight(20));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("".PadRight(15));
            for (int i = 3; i < 6; i++)
            {
                Console.Write(i.ToString().PadRight(20));
            }
            Console.WriteLine();

            Console.Write("".PadRight(15));
            for (int i = 3; i < 6; i++)
            {
                if (mItems[i] == null) Console.Write("Nothing".PadRight(20));
                else Console.Write(mItems[i].mName.PadRight(20));
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("".PadRight(15));
            for (int i = 6; i < 9; i++)
            {
                Console.Write(i.ToString().PadRight(20));
            }
            Console.WriteLine();

            Console.Write("".PadRight(15));
            for (int i = 6; i < 9; i++)
            {
                if (mItems[i] == null) Console.Write("Nothing".PadRight(20));
                else Console.Write(mItems[i].mName.PadRight(20));
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public int emptySlotsCount()
        {
            int count = 0;
            for (int i = 0; i < INVENTORY_SIZE; i++)
            {
                if (mItems[i] == null) ++count;
            }
            return count;
        }

        public int firstEmptySlot()
        {

            for (int i = 0; i < INVENTORY_SIZE; i++)
            {
                if (mItems[i] == null) return i;
            }
            return -1;//no empty slot
        }

    }

}
