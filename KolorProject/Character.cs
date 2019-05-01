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

        public List<int> attack(Character target)
        {
            int physicalDmgToDeal = calculatePhysicalDamage();
            int magicalDmgToDeal = calculateMagicalDamage();

            List<int> list = new List<int>();//these values will be written to the fight log
            list.Add(physicalDmgToDeal);
            list.Add(magicalDmgToDeal);

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



            return list;
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
            int minAttack=0;
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
                else if((mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack != 0)
                {
                    if (mInventory.mEquipedItems["weapon"] as MeleWeapon != null)
                    { //
                        if ((mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack!=0)
                        minAttack = mBasicStats.minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack) * 0.4 * mAttributes.Strength);
                    }
                    else
                    {
                        if ((mInventory.mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack != 0)
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
            int maxAttack=0;
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
                else if((mInventory.mEquipedItems["weapon"] as Weapon).maxMagicalAttack != 0)
                {

                    if ((mInventory.mEquipedItems["weapon"] as MeleWeapon) != null )
                    { //strength there as multiplier
                        if((mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack != 0)
                        maxAttack = mBasicStats.maxPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack + (int)(((mInventory.mEquipedItems["weapon"] as MeleWeapon).minPhysicalAttack + (mInventory.mEquipedItems["weapon"] as MeleWeapon).maxPhysicalAttack) * 0.4 * mAttributes.Strength);
                    }
                    else
                    {
                        if ((mInventory.mEquipedItems["weapon"] as RangedWeapon).maxRangedPhysicalAttack != 0)
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

}
