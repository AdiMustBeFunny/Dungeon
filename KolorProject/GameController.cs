using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    class GameController
    {

        Team mTeam = new Team();
        private int counter = 0;
        public GameController() { }

        public void initialize()
        {

            Console.WriteLine();
            Console.WriteLine("Welcome to the Dungeon");
            Console.WriteLine("Before you descend into the darkness name your heroes");
            Console.WriteLine();
            Console.WriteLine();


            string[] heroNames = new string[3];




            Console.Write("First hero: ");
            do { heroNames[0] = Console.ReadLine(); }
            while (heroNames[0] == "");

            Console.Write("Second hero: ");
            do { heroNames[1] = Console.ReadLine(); }
            while (heroNames[1] == "");
            Console.Write("Third hero: ");
            do { heroNames[2] = Console.ReadLine(); }
            while (heroNames[2] == "");

            mTeam.mCharacters.Add(new Hero(heroNames[0]));
            mTeam.mCharacters.Add(new Hero(heroNames[1]));
            mTeam.mCharacters.Add(new Hero(heroNames[2]));

            mTeam.mCharacters[0].mInventory.mEquipedItems["weapon"] = new Metal_Fists_1();
            mTeam.mCharacters[1].mInventory.mEquipedItems["weapon"] = new Metal_Fists_1();
            mTeam.mCharacters[2].mInventory.mEquipedItems["weapon"] = new Metal_Fists_1();
            mTeam.mCharacters[0].mInventory.mEquipedItems["chest"] = new Plate_Armor_1();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Before you start let me clarify some concepts: ");
            Console.WriteLine("In the game your charactees have 'healthbank' and 'endurance' property");
            Console.WriteLine("During combat your endurance counts, if it goes down to 0 or below");
            Console.WriteLine("you can no longer use that character in this fight. ");
            Console.WriteLine("If during a combat you decide to use healing potion then endurance gained");
            Console.WriteLine("from this potion will be subtracted from your 'healthbank' property");
            Console.WriteLine("Example: Your max endurance is 10, healthbank is 20 and currently you have 3 endurance");
            Console.WriteLine("You decide to drink potion that will bring back 10 endurance");
            Console.WriteLine("10 - 3 = 7 is the amount of endurance you can regain to have max.");
            Console.WriteLine("This '7' value is subtracted from healthbank so now it is equal 20 - 7 = 13");
            Console.WriteLine("Once your endurance and healthbank is equal to 0 you loose");
            Console.WriteLine("Finishing level sets your healthbank and endurance back to max");
            Console.WriteLine("GL HF");
            Console.ReadKey();

        }

        public void start()
        {

            bool dead = false;
            for (int c = 0; c < 2; c++)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("You are about to enter Dungeon Tier1 lv.{0}", counter + 1);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Room r = new Room(21, 21, mTeam, 1, 4);

                r.genRoom();



                r.processMovement();
                if (mTeam.getAliveCharacters().Count == 0) { dead = true; break; }
                Console.Clear();
                Console.WriteLine("You have finished this dungeon");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();


                for (int i = 0; i < 3; i++)
                {
                    mTeam.mCharacters[i].mBasicStats.currentHp = mTeam.mCharacters[i].mBasicStats.maxHp;
                    mTeam.mCharacters[i].mBasicStats.currentEndurance = mTeam.mCharacters[i].mBasicStats.maxEndurance;

                }
                ++counter;
            } //Tier 1 dungeon
            if (dead) { Console.WriteLine("You have lasted {0} dungeons", counter); return; }

            for (int i = 0; i < 3; i++)
            {
                mTeam.mCharacters[i].levelUp();
            }//level up

            for (int c = 0; c < 3; c++)
            {
                Console.Clear();
                Console.WriteLine("You are about to enter Dungeon Tier2 lv.{0}", counter + 1);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Room r = new Room(25, 25, mTeam, 2, 7);

                r.genRoom();

                r.processMovement();
                if (mTeam.getAliveCharacters().Count == 0) { dead = true; break; }
                Console.Clear();
                Console.WriteLine("You have finished this dungeon");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                if (c % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        mTeam.mCharacters[i].levelUp();
                    }
                }//level up

                for (int i = 0; i < 3; i++)
                {
                    mTeam.mCharacters[i].mBasicStats.currentHp = mTeam.mCharacters[i].mBasicStats.maxHp;
                    mTeam.mCharacters[i].mBasicStats.currentEndurance = mTeam.mCharacters[i].mBasicStats.maxEndurance;

                }
                ++counter;
            }//Tier 2 dungeon
            if (dead) { Console.WriteLine("You have lasted {0} dungeons", counter); return; }

            int m = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("You are about to enter Dungeon Tier 3 lv.{0}", m);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Room r = new Room(27, 27, mTeam, 3, 9, 2);

                r.genRoom();

                r.processMovement();
                if (mTeam.getAliveCharacters().Count == 0) { dead = true; break; }
                Console.Clear();
                Console.WriteLine("You have finished this dungeon");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                if (m % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        mTeam.mCharacters[i].levelUp();
                    }
                }//level up

                for (int i = 0; i < 3; i++)
                {
                    mTeam.mCharacters[i].mBasicStats.currentHp = mTeam.mCharacters[i].mBasicStats.maxHp;
                    mTeam.mCharacters[i].mBasicStats.currentEndurance = mTeam.mCharacters[i].mBasicStats.maxEndurance;
                }
                ++counter;
                ++m;
            }



            if (dead) { Console.WriteLine("You have lasted {0} dungeons", counter); return; }

        }
    }
}
