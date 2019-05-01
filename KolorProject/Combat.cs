using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
    class Combat
    {
        Random rnd = new Random();
        Team Player;
        Team Enemy;

        private string FightLog = "";

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


                    //fight log
                    Console.WriteLine("Fight Log:");
                    Console.WriteLine(FightLog);
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

                        List<int> mAttack = Player.mCharacters[heroIndex].attack(Enemy.mCharacters[enemyIndex]);
                        FightLog = "";//clear precious string
                        FightLog += Player.mCharacters[heroIndex].name + "(Index: " + heroIndex.ToString() + ") attacked " + Enemy.mCharacters[enemyIndex].name + "(Index: " + enemyIndex.ToString() + ") dealing " + mAttack[0].ToString() + " physical damage and " + mAttack[1].ToString() + " magical damage\n";
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

                int playerIndex = rnd.Next() % targetForEnemy.Count;
                int rndEnemyIndex = rnd.Next() % activeEnemies.Count;
                List<int> enemyAttack = Enemy.mCharacters[activeEnemies[rndEnemyIndex]].attack(Player.mCharacters[targetForEnemy[playerIndex]]);
                Enemy.mCharacters[activeEnemies[rndEnemyIndex]].mBasicStats.exhausted = true;
                FightLog += Enemy.mCharacters[activeEnemies[rndEnemyIndex]].name + "(Index: " + activeEnemies[rndEnemyIndex].ToString() + ") attacked " + Player.mCharacters[targetForEnemy[playerIndex]].name + "(Index: " + targetForEnemy[playerIndex].ToString() + ") dealing " + enemyAttack[0].ToString() + " physical damage and " + enemyAttack[1].ToString() + " magical damage";


                //enemy might have killed guy who was ready while other was exhausted so we set his flag to ready
                if (Player.getActiveCharacters().Count == 0) Player.resetExhausted();
                if (Enemy.getActiveCharacters().Count == 0) Enemy.resetExhausted();

            }//battle loop

            //battle has finished now determine who has won

            Console.Clear();

            for (int i = 0; i < Player.mCharacters.Count; i++)
                Player.mCharacters[i].finishCombat();

            if (Enemy.alive()) { Console.WriteLine("Enemy has won\nPress any key to continue..."); Console.ReadKey(); return false; }
            else { Console.WriteLine("Player has won\nPress any key to continue..."); Console.ReadKey(); return true; }



        } //true = player has won ; false = enemy has won

    }
}
