using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{

 

   


    enum RoomObjectType
    {
        Player,
        Enemy,
        Treasure,
        Exit
    }

     class RoomInventory
    {
        public List<Item> mItems;
        public RoomInventory(int size = 12)
        {
            mItems = new List<Item>(size);

            for (int i = 0; i < size; i++)
            {
                mItems.Add(null);
            }

        }

        public void display(int selectedIndex)
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Team Inventory".PadLeft(35));
            Console.WriteLine();
            Console.WriteLine("Navigation - W A S D");
            Console.WriteLine("Select - Enter");
            Console.WriteLine("Cancel selection / go back - Esc");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < mItems.Count; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }

                string itemName = "";

                if (mItems[i] != null)
                    itemName = mItems[i].mName;
                else
                {
                    itemName = "Empty";
                }

                if (i == selectedIndex)
                {
                    Console.Write("".PadRight(20 - itemName.Length));
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.Write(itemName);
                    Console.BackgroundColor = ConsoleColor.Black; ;
                }
                else
                {
                    Console.Write("".PadRight(20 - itemName.Length));
                    Console.Write(itemName);
                }

            }
            Console.WriteLine();


        }

        public int emptySlotsCount()
        {
            int counter = 0;
            for (int i = 0; i < mItems.Count; i++)
            {
                if (mItems[i] == null) ++counter;
            }
            return counter;
        }

        public int firstEmptySlot()
        {
            int counter = 0;
            for (int i = 0; i < mItems.Count; i++)
            {
                if (mItems[i] == null) { counter = i; break; }
            }
            return counter;
        }
    }

    abstract class RoomObject
    {
        public RoomObjectType mType = new RoomObjectType();
        public RoomObject(RoomObjectType mtype)
        { mType = mtype; }
    }
    class RoomPlayer : RoomObject
    {
        public Team mTeam;
        public RoomPlayer(Team mteam, RoomObjectType mType = RoomObjectType.Player) : base(mType)
        { mTeam = mteam; }

    }
     class RoomEnemy : RoomObject
    {
        public Team mTeam;
        public RoomEnemy(Team mteam, RoomObjectType mType = RoomObjectType.Enemy) : base(mType)
        { mTeam = mteam; }

    }
     class RoomTreasure : RoomObject
    {
        public Item mTreasure;
        public RoomTreasure(Item treasure) : base(RoomObjectType.Treasure) { mTreasure = treasure; }

    }
     class Tile
    {
        public int x, y;
        public bool wall = true;
        public bool visited = false;
        public char mChar = '#';
        public RoomObject mObject = null;
    }
    class Room
    {
        //player - $
        //wall - #
        //enemy - E
        //treasure - T

        public Tile[,] mRoom;
        public int Width { get; }
        public int Height { get; }
        public RoomPlayer mPlayer;

        public int playerX = 1, playerY = 1;
        public int enemyTier;
        public int treasureTier;
        public int enemyStrength;

        public Room(int width, int height, Team player, int enemytier = 1, int num3x4 = 3, int treasuretier = 1,int enemystrength = 0)
        {
            mRoom = new Tile[height, width];
            Height = height;
            Width = width;
            enemyTier = enemytier;
            treasureTier = treasuretier;
            enemyStrength = enemystrength;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mRoom[i, j] = new Tile();
                    mRoom[i, j].x = j;
                    mRoom[i, j].y = i;

                }
            }

            //mRoom[1, 1].mObject = new RoomPlayer(player);
            mPlayer = new RoomPlayer(player);

            genExit();

            for (int i = 0; i < num3x4; i++)
            { gen3x4Treasure(); }

        }

        public void genRoom()
        {

            int total_cell_number = (Width / 2) * (Height / 2);
            int current_cell_number = 1;

            Stack<Tile> sztos = new Stack<Tile>();

            sztos.Push(mRoom[1, 1]);

            mRoom[1, 1].visited = true;
            mRoom[1, 1].wall = false;
            mRoom[1, 1].mChar = ' ';

            Random rnd = new Random();

            while (current_cell_number != total_cell_number - (count3x4Treasure) - 1)//gen maze (1 is because exit)
            {

                Tile currentTile = sztos.Peek();
                List<Tile> neighbours = new List<Tile>();

                if (currentTile.x > 1 && mRoom[currentTile.y, currentTile.x - 2].visited == false) neighbours.Add(mRoom[currentTile.y, currentTile.x - 2]);
                if (currentTile.x < Width - 2 && mRoom[currentTile.y, currentTile.x + 2].visited == false) neighbours.Add(mRoom[currentTile.y, currentTile.x + 2]);

                if (currentTile.y > 1 && mRoom[currentTile.y - 2, currentTile.x].visited == false) neighbours.Add(mRoom[currentTile.y - 2, currentTile.x]);
                if (currentTile.y < Height - 2 && mRoom[currentTile.y + 2, currentTile.x].visited == false) neighbours.Add(mRoom[currentTile.y + 2, currentTile.x]);

                if (neighbours.Count != 0)
                {
                    Tile rndNeighbour = neighbours[rnd.Next() % neighbours.Count];
                    rndNeighbour.visited = true;
                    rndNeighbour.wall = false;
                    rndNeighbour.mChar = ' ';
                    sztos.Push(rndNeighbour);

                    if (rndNeighbour.x > currentTile.x)
                    {
                        mRoom[rndNeighbour.y, rndNeighbour.x - 1].wall = false;
                        mRoom[rndNeighbour.y, rndNeighbour.x - 1].mChar = ' ';
                    }
                    if (rndNeighbour.x < currentTile.x)
                    {
                        mRoom[rndNeighbour.y, rndNeighbour.x + 1].wall = false;
                        mRoom[rndNeighbour.y, rndNeighbour.x + 1].mChar = ' ';
                    }
                    if (rndNeighbour.y > currentTile.y)
                    {
                        mRoom[rndNeighbour.y - 1, rndNeighbour.x].wall = false;
                        mRoom[rndNeighbour.y - 1, rndNeighbour.x].mChar = ' ';
                    }
                    if (rndNeighbour.y < currentTile.y)
                    {
                        mRoom[rndNeighbour.y + 1, rndNeighbour.x].wall = false;
                        mRoom[rndNeighbour.y + 1, rndNeighbour.x].mChar = ' ';
                    }

                    ++current_cell_number;
                }
                else
                {
                    sztos.Pop();
                }

            }//maze generated

            mRoom[1, 1].mChar = '$';
            //It works!! 
            /*
            if(mRoom[1,2].mChar==' ')
            {
                mRoom[1,2].mObject= new RoomEnemy(new Team(new List<Character>() { new Bat_1("bat_1"), new Bat_1("bat_2") }));
                mRoom[1, 2].mChar = 'E';
            }
            */
        }


        private List<KeyValuePair<int, int>> reservedCells = new List<KeyValuePair<int, int>>();
        private int count3x4Treasure = 0;
        private void genExit()
        {

            Random rnd = new Random();
            bool rndStagePassed = false;

            int startX = 0, startY = 0;

            /*
             I - start index, it will be wall in game
                   # I #
                   # Q #
                   #   #
             */
            int localWidth = Width / 2;
            int localHeight = Height / 2;
            while (!rndStagePassed)
            {
                int possiblyStartX = 1 + (localWidth - (rnd.Next() % localWidth + 1)) * 2;
                int possiblyStartY = (localHeight - (rnd.Next() % localHeight + 1)) * 2;

                if (((possiblyStartX >= 3) && (possiblyStartX <= 1 + (localWidth - 2) * 2))
                    && ((possiblyStartY >= 2) && (possiblyStartY <= 1 + (localHeight - 3) * 2)))
                {//locations are seemingly ok but we have to take existing ones into account

                    if (reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY)) == false
                        &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY + 2)) == false
                        )
                    {//its free to use
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY + 2));
                        startX = possiblyStartX;
                        startY = possiblyStartY;
                        rndStagePassed = true;
                    }
                }
            }

            mRoom[startY, startX].mChar = '#';
            mRoom[startY, startX - 1].mChar = '#';
            mRoom[startY, startX + 1].mChar = '#';
            mRoom[startY + 1, startX].mChar = 'Q';
            mRoom[startY + 1, startX - 1].mChar = '#';
            mRoom[startY + 1, startX + 1].mChar = '#';

            mRoom[startY + 2, startX - 1].mChar = '#';
            mRoom[startY + 2, startX].mChar = 'E';
            mRoom[startY + 2, startX + 1].mChar = '#';



            mRoom[startY, startX].visited = true;
            mRoom[startY, startX - 1].visited = true;
            mRoom[startY, startX + 1].visited = true;
            mRoom[startY + 1, startX].visited = true;
            mRoom[startY + 1, startX - 1].visited = true;
            mRoom[startY + 1, startX + 1].visited = true;
            mRoom[startY + 2, startX].visited = true;
            mRoom[startY + 2, startX - 1].visited = true;
            mRoom[startY + 2, startX + 1].visited = true;

            //generate enemy
            Team enemy = new Team();
            enemy.teamName = "Enemy Team";
            int teamSize = rnd.Next() % 2 + 2;
            //he will be stronger, i will add some weapons for them
            if (enemyTier == 1)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_1_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }
                enemy.goldToPillage = 5 + rnd.Next() % 15;
            }

            if (enemyTier == 2)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_2_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }

            }

            if (enemyTier == 3)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_3_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }

            }

            mRoom[startY + 2, startX].mObject = new RoomEnemy(enemy);


        }
        private void gen3x4Treasure()
        {
            Random rnd = new Random();
            bool rndStagePassed = false;

            int startX = 0, startY = 0;

            /*
             I - start index, it will be wall in game
                   # I #
                   # T #
                   # E #
             */
            int localWidth = Width / 2;
            int localHeight = Height / 2;
            while (!rndStagePassed)
            {
                int possiblyStartX = 1 + (localWidth - (rnd.Next() % localWidth + 1)) * 2;
                int possiblyStartY = (localHeight - (rnd.Next() % localHeight + 1)) * 2;

                if (((possiblyStartX >= 3) && (possiblyStartX <= 1 + (localWidth - 2) * 2))
                    && ((possiblyStartY >= 2) && (possiblyStartY <= 1 + (localHeight - 3) * 2)))
                {//locations are seemingly ok but we have to take existing ones into account

                    if (reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY)) == false
                        &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY + 2)) == false
                       &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX - 2, possiblyStartY + 2)) == false
                       &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX - 2, possiblyStartY - 2)) == false
                       &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX + 2, possiblyStartY + 2)) == false
                       &&
                       reservedCells.Contains(new KeyValuePair<int, int>(possiblyStartX + 2, possiblyStartY - 2)) == false
                       )
                    {//its free to use
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX, possiblyStartY + 2));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX - 2, possiblyStartY + 2));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX - 2, possiblyStartY - 2));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX + 2, possiblyStartY + 2));
                        reservedCells.Add(new KeyValuePair<int, int>(possiblyStartX + 2, possiblyStartY - 2));
                        startX = possiblyStartX;
                        startY = possiblyStartY;
                        rndStagePassed = true;
                    }
                }

            }//rnd stage passed now we can update our room

            mRoom[startY, startX].mChar = '#';
            mRoom[startY, startX - 1].mChar = '#';
            mRoom[startY, startX + 1].mChar = '#';
            mRoom[startY + 1, startX].mChar = 'T';
            mRoom[startY + 1, startX - 1].mChar = '#';
            mRoom[startY + 1, startX + 1].mChar = '#';

            mRoom[startY + 2, startX - 1].mChar = '#';
            mRoom[startY + 2, startX].mChar = 'E';
            mRoom[startY + 2, startX + 1].mChar = '#';

            //mRoom[startY + 3, startX].mChar = ' ';

            mRoom[startY, startX].visited = true;
            mRoom[startY, startX - 1].visited = true;
            mRoom[startY, startX + 1].visited = true;
            mRoom[startY + 1, startX].visited = true;
            mRoom[startY + 1, startX - 1].visited = true;
            mRoom[startY + 1, startX + 1].visited = true;
            mRoom[startY + 2, startX].visited = true;
            mRoom[startY + 2, startX - 1].visited = true;
            mRoom[startY + 2, startX + 1].visited = true;


            //generate enemy
            Team enemy = new Team();
            enemy.teamName = "Enemy Team";
            int teamSize = rnd.Next() % 2 + 2;

            if (enemyTier == 1)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_1_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }

            }

            if (enemyTier == 2)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_2_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }

            }

            if (enemyTier == 3)
            {
                for (int i = 0; i < teamSize; i++)
                {
                    Character currentEnemy = All_Items.genTier_3_Enemy(enemyStrength);

                    enemy.mCharacters.Add(currentEnemy);
                }

            }

            mRoom[startY + 2, startX].mObject = new RoomEnemy(enemy);

            //generate treasure
            Item mTreasure = null;


            if (treasureTier == 1)
            {
                mTreasure = All_Items.genTier_1_Treasure();
            }
            if (treasureTier == 2)
            {
                mTreasure = All_Items.genTier_2_Treasure();
            }

            mRoom[startY + 1, startX].mObject = new RoomTreasure(mTreasure);

            count3x4Treasure += 1;
        }

        public void displayRoom()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(mRoom[i, j].mChar.ToString().PadRight(3));
                }
                Console.WriteLine();
            }


        }

        public enum inputType
        {
            Moving,
            Inventory,
            CharacterInfo
        }

        private void transferItem(int itemIndex, int playerIndex)
        {
            List<int> validTransferIndices = new List<int>();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Select inventory (0 - 3)");
            Console.WriteLine("------------------------");
            if (playerIndex == 0)
            {
                Console.WriteLine("1. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[1].name, mPlayer.mTeam.mCharacters[1].mInventory.emptySlotsCount());
                Console.WriteLine("2. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[2].name, mPlayer.mTeam.mCharacters[2].mInventory.emptySlotsCount());
                if (mPlayer.mTeam.mCharacters[1].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(1);
                if (mPlayer.mTeam.mCharacters[2].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(2);
            }
            else if (playerIndex == 1)
            {
                Console.WriteLine("0. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[0].name, mPlayer.mTeam.mCharacters[0].mInventory.emptySlotsCount());
                Console.WriteLine("2. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[2].name, mPlayer.mTeam.mCharacters[2].mInventory.emptySlotsCount());
                if (mPlayer.mTeam.mCharacters[0].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(0);
                if (mPlayer.mTeam.mCharacters[2].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(2);

            }
            else
            {
                Console.WriteLine("0. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[0].name, mPlayer.mTeam.mCharacters[0].mInventory.emptySlotsCount());
                Console.WriteLine("1. {0}'s inventory, empty slots: {1}", mPlayer.mTeam.mCharacters[1].name, mPlayer.mTeam.mCharacters[1].mInventory.emptySlotsCount());
                if (mPlayer.mTeam.mCharacters[0].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(0);
                if (mPlayer.mTeam.mCharacters[1].mInventory.emptySlotsCount() != 0)
                    validTransferIndices.Add(1);

            }

            Console.WriteLine("3. Teams' inventory, empty slots: {0}", mPlayer.mTeam.teamInventory.emptySlotsCount());
            Console.WriteLine("Esc to go back");
            Console.WriteLine("------------------------");

            int mChoice = 0; // 0 - players inventories ;; 1 - team inventory ;; 2 - nothing
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (cki.Key != ConsoleKey.D0 && cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3 && cki.Key != ConsoleKey.Escape)
            {
                cki = Console.ReadKey(false);

                if (cki.Key == ConsoleKey.D0 || cki.Key == ConsoleKey.D1 || cki.Key == ConsoleKey.D2)
                {
                    if (validTransferIndices.Contains((int)cki.Key % 48))
                        mChoice = 0;
                    else cki = new ConsoleKeyInfo();
                }
                if (cki.Key == ConsoleKey.D3)
                {
                    if (mPlayer.mTeam.teamInventory.emptySlotsCount() != 0)
                        mChoice = 1;
                    else cki = new ConsoleKeyInfo();
                }

                if (cki.Key == ConsoleKey.Escape) mChoice = 2;
            }

            if (mChoice == 0)
            {//transfer to player
                int playerToIndex = (int)cki.Key % 48;
                Item temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex];
                mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = null;
                mPlayer.mTeam.mCharacters[playerToIndex].mInventory.mItems[mPlayer.mTeam.mCharacters[playerToIndex].mInventory.firstEmptySlot()] = temp;
            }
            if (mChoice == 1)
            {
                Item temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex];
                mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = null;
                mPlayer.mTeam.teamInventory.mItems[mPlayer.mTeam.teamInventory.firstEmptySlot()] = temp;
            }
        }

        public void processMovement()
        {
            bool found_way_out = false;



            while (found_way_out == false)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("     T - treasure, E - enemy, Q - next dungeon, $ - YOU");
                Console.WriteLine();
                displayRoom();

                Console.WriteLine("W A S D => moving around");
                Console.WriteLine("I => inventory");
                Console.WriteLine("C => characterInfo");

                inputType mInput = new inputType();

                ConsoleKeyInfo cki = new ConsoleKeyInfo();
                while (cki.Key != ConsoleKey.A && cki.Key != ConsoleKey.W && cki.Key != ConsoleKey.S && cki.Key != ConsoleKey.D && cki.Key != ConsoleKey.I && cki.Key != ConsoleKey.C)
                {

                    // while (Console.KeyAvailable == false) if i press button for too long then it will break so  i had to comment that out
                    //{
                    cki = Console.ReadKey(false);

                    if (cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.S || cki.Key == ConsoleKey.D) { mInput = inputType.Moving; }
                    if (cki.Key == ConsoleKey.I) { mInput = inputType.Inventory; }
                    if (cki.Key == ConsoleKey.C) { mInput = inputType.CharacterInfo; }
                    // }


                }

                //now process input switch(key)
                if (mInput == inputType.CharacterInfo)
                {
                    Console.Clear();
                    // mPlayer.mTeam.displayFightInfo();
                    Console.WriteLine();
                    Console.WriteLine("Strength affects physical damage");
                    Console.WriteLine("Agility affects ranged (physical) damage");
                    Console.WriteLine("Intelligence affects magical damage");
                    Console.WriteLine("Constitiution affects health and endurance");


                    for (int i = 0; i < mPlayer.mTeam.mCharacters.Count; i++)
                        mPlayer.mTeam.mCharacters[i].displayStats();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    //clear
                }
                if (mInput == inputType.Moving)
                {
                    int x_dif = 0;
                    int y_dif = 0;

                    if (cki.Key == ConsoleKey.A) x_dif -= 1;
                    if (cki.Key == ConsoleKey.D) x_dif += 1;
                    if (cki.Key == ConsoleKey.W) y_dif -= 1;
                    if (cki.Key == ConsoleKey.S) y_dif += 1;

                    if (mRoom[playerY + y_dif, playerX + x_dif].mChar == '#') continue;
                    else if (mRoom[playerY + y_dif, playerX + x_dif].mChar == ' ')
                    {
                        mRoom[playerY, playerX].mChar = ' ';
                        mRoom[playerY + y_dif, playerX + x_dif].mChar = '$';
                        playerX += x_dif;
                        playerY += y_dif;


                    }
                    else if (mRoom[playerY + y_dif, playerX + x_dif].mChar == 'Q')
                    {
                        Console.WriteLine("You are about to finish this dungeon, do you wish to do so? ( y/n )");
                        bool proceed = false;
                        cki = new ConsoleKeyInfo();
                        while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N)
                        {

                            cki = Console.ReadKey(false);

                            if (cki.Key == ConsoleKey.Y) { proceed = true; }
                            if (cki.Key == ConsoleKey.N) { proceed = false; }

                        }

                        if (proceed)
                        {
                            found_way_out = true;
                        }
                    }
                    else if (mRoom[playerY + y_dif, playerX + x_dif].mChar == 'E')
                    {
                        Console.WriteLine("You are about to fight with: {0}, do you wish to do so? ( y/n )", (mRoom[playerY + y_dif, playerX + x_dif].mObject as RoomEnemy).mTeam.teamName);
                        bool proceed = false;
                        cki = new ConsoleKeyInfo();
                        while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N)
                        {

                            cki = Console.ReadKey(false);

                            if (cki.Key == ConsoleKey.Y) { proceed = true; }
                            if (cki.Key == ConsoleKey.N) { proceed = false; }

                        }

                        if (proceed)
                        {
                            bool combatResult;
                            Combat mCombat = new Combat(mPlayer.mTeam, (mRoom[playerY + y_dif, playerX + x_dif].mObject as RoomEnemy).mTeam);
                            combatResult = mCombat.start();
                            if (combatResult == true)//success
                            {
                                mPlayer.mTeam.goldToPillage += (mRoom[playerY + y_dif, playerX + x_dif].mObject as RoomEnemy).mTeam.goldToPillage;
                                mRoom[playerY + y_dif, playerX + x_dif].mObject = null;
                                mRoom[playerY, playerX].mChar = ' ';
                                mRoom[playerY + y_dif, playerX + x_dif].mChar = '$';
                                playerX += x_dif;
                                playerY += y_dif;

                                mPlayer.mTeam.resetExhausted();
                            }
                            else //player lost so he doesnt move any further
                            {

                                if (mPlayer.mTeam.getAliveCharacters().Count == 0) return; //all dead so quit game
                            }
                        }

                    }
                    else if (mRoom[playerY + y_dif, playerX + x_dif].mChar == 'T')
                    {//treasure
                        if (mPlayer.mTeam.teamInventory.emptySlotsCount() == 0)
                        {//no free space for this item
                            Console.WriteLine("No empty slots in your team inventory, free up some space.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                        else
                        {

                            Console.WriteLine("You have found: {0}", (mRoom[playerY + y_dif, playerX + x_dif].mObject as RoomTreasure).mTreasure.mName);
                            mPlayer.mTeam.teamInventory.mItems[mPlayer.mTeam.teamInventory.firstEmptySlot()] = (mRoom[playerY + y_dif, playerX + x_dif].mObject as RoomTreasure).mTreasure;
                            mRoom[playerY + y_dif, playerX + x_dif].mObject = null;
                            mRoom[playerY, playerX].mChar = ' ';
                            mRoom[playerY + y_dif, playerX + x_dif].mChar = '$';
                            playerX += x_dif;
                            playerY += y_dif;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }

                }

                else if (mInput == inputType.Inventory)
                {//display inventory

                    bool exitInventory = false;

                    while (!exitInventory)
                    {
                        bool exitLocalInventory = false;

                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Select inventory (0 - 3)");
                        Console.WriteLine("------------------------");
                        Console.WriteLine("0. {0}'s inventory", mPlayer.mTeam.mCharacters[0].name);
                        Console.WriteLine("1. {0}'s inventory", mPlayer.mTeam.mCharacters[1].name);
                        Console.WriteLine("2. {0}'s inventory", mPlayer.mTeam.mCharacters[2].name);
                        Console.WriteLine("3. Teams' inventory");
                        Console.WriteLine("Esc to go back");
                        Console.WriteLine("------------------------");


                        int choice = 0; // 0 - display players inventory;; 1 - display team inventory;; 2 - go back
                        cki = new ConsoleKeyInfo();
                        while (cki.Key != ConsoleKey.D0 && cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3 && cki.Key != ConsoleKey.Escape)
                        {
                            cki = Console.ReadKey(false);

                            if (cki.Key == ConsoleKey.D0 || cki.Key == ConsoleKey.D1 || cki.Key == ConsoleKey.D2) choice = 0;
                            if (cki.Key == ConsoleKey.D3) choice = 1;
                            if (cki.Key == ConsoleKey.Escape) choice = 2;
                        }


                        if (choice == 2) exitInventory = true;
                        else if (choice == 0)
                        {
                            int playerIndex = (int)cki.Key % 48;
                            while (!exitLocalInventory)
                            {
                                Console.Clear();
                                mPlayer.mTeam.mCharacters[playerIndex].mInventory.displayInventory();

                                cki = new ConsoleKeyInfo();
                                int itemType = 0; // 0 - from inventory ;; 1 - from equipped items ;; 2 - go back
                                while (!(cki.Key >= ConsoleKey.D0 && cki.Key <= ConsoleKey.D8) && !(cki.Key >= ConsoleKey.E && cki.Key <= ConsoleKey.I) && cki.Key != ConsoleKey.Escape)
                                {

                                    cki = Console.ReadKey(false);

                                    if (cki.Key >= ConsoleKey.D0 && cki.Key <= ConsoleKey.D8) itemType = 0;
                                    if (cki.Key >= ConsoleKey.E && cki.Key <= ConsoleKey.I) itemType = 1;
                                    if (cki.Key == ConsoleKey.Escape) itemType = 2;
                                }
                                if (itemType == 2) exitLocalInventory = true;
                                if (itemType == 0)
                                {//item from inventory
                                    int itemIndex = (int)cki.Key % 48;

                                    if (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] == null) continue;

                                    if ((mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Weapon) != null)
                                    {//its a weapon!
                                        Weapon mWeapon = (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Weapon);
                                        Console.WriteLine();
                                        Console.WriteLine("Weapon: {0}", mWeapon.mName);
                                        if ((mWeapon as MeleWeapon) != null) Console.WriteLine("Physical melee dmg {0} - {1}", (mWeapon as MeleWeapon).minPhysicalAttack, (mWeapon as MeleWeapon).maxPhysicalAttack);
                                        else Console.WriteLine("Physical melee dmg 0 - 0");
                                        if ((mWeapon as RangedWeapon) != null) Console.WriteLine("Physical ranged dmg {0} - {1}", (mWeapon as RangedWeapon).minRangedPhysicalAttack, (mWeapon as RangedWeapon).maxRangedPhysicalAttack);
                                        else Console.WriteLine("Physical ranged dmg 0 - 0");
                                        Console.WriteLine("Magical dmg {0} - {1}", mWeapon.minMagicalAttack, mWeapon.maxMagicalAttack);
                                        Console.WriteLine("Do you want to equip this weapon? ( y//n )");
                                        Console.WriteLine("Or transfer it to a diffrent inventory? ( t )");
                                        Console.WriteLine("Or delete it? ( delete )");

                                        cki = new ConsoleKeyInfo();
                                        int proceed = 0; // 0 nothing// 1 equip// 2 transfer// 3 delete
                                        while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N && cki.Key != ConsoleKey.T && cki.Key != ConsoleKey.Delete)
                                        {

                                            cki = Console.ReadKey(false);

                                            if (cki.Key == ConsoleKey.Y) { proceed = 1; }
                                            if (cki.Key == ConsoleKey.N) { proceed = 0; }
                                            if (cki.Key == ConsoleKey.T) { proceed = 2; }
                                            if (cki.Key == ConsoleKey.Delete) { proceed = 3; }
                                        }

                                        if (proceed == 3)
                                        { mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = null; }
                                        if (proceed == 1)
                                        {
                                            Item temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems["weapon"];//can be null
                                            mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems["weapon"] = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex];
                                            mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = temp;
                                        }

                                        //now choose person to transfer the item to ----- gosh i am exhausted xd
                                        if (proceed == 2)
                                        {
                                            transferItem(itemIndex, playerIndex);
                                        }

                                    }
                                    if ((mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Appearal) != null)
                                    {//It's an appearial
                                        Appearal temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Appearal;

                                        Console.WriteLine();
                                        Console.WriteLine("Appearal: {0}", temp.mName);
                                        Console.WriteLine("Physical defense: {0}", temp.physicalArmor);
                                        Console.WriteLine("Magical defense: {0}", temp.magicalArmor);
                                        Console.WriteLine("Do you want to equip this item? ( y//n )");
                                        Console.WriteLine("Or transfer it to a different inventory? ( t )");
                                        Console.WriteLine("Or delete it? ( delete )");


                                        cki = new ConsoleKeyInfo();
                                        int proceed = 0; // 0 nothing 1 equip 2 transfer
                                        while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N && cki.Key != ConsoleKey.T && cki.Key != ConsoleKey.Delete)
                                        {

                                            cki = Console.ReadKey(false);

                                            if (cki.Key == ConsoleKey.Y) { proceed = 1; }
                                            if (cki.Key == ConsoleKey.N) { proceed = 0; }
                                            if (cki.Key == ConsoleKey.T) { proceed = 2; }
                                            if (cki.Key == ConsoleKey.Delete) { proceed = 3; }
                                        }

                                        if (proceed == 3) mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = null;
                                        if (proceed == 1)
                                        {
                                            string key = "";
                                            switch (temp.mBodyPart)
                                            {
                                                case EAppearalBodyPart.Chest:
                                                    key = "chest";
                                                    break;
                                                case EAppearalBodyPart.Gauntlets:
                                                    key = "gauntlets";
                                                    break;
                                                case EAppearalBodyPart.Helmet:
                                                    key = "helmet";
                                                    break;
                                                case EAppearalBodyPart.Boots:
                                                    key = "boots";
                                                    break;

                                            }

                                            Item mTemp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[key];
                                            mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[key] = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex];
                                            mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = mTemp;

                                        }
                                        if (proceed == 2)
                                        {
                                            transferItem(itemIndex, playerIndex);
                                        }


                                    }

                                    if ((mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Potion) != null)
                                    {
                                        Potion temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] as Potion;

                                        Console.WriteLine();
                                        Console.WriteLine("Potion: {0}", temp.mName);
                                        Console.WriteLine("Heal strength: {0}", (temp as HpPotion).hpToReturn);
                                        Console.WriteLine("Info: Potions can be used only in combat to turn your hp into endurance");
                                        Console.WriteLine("To transfer potion press ( t ), Esc to go back");
                                        Console.WriteLine("Or delete it? ( delete )");

                                        cki = new ConsoleKeyInfo();
                                        int proceed = 0; // 0 transfer 1 go back 2 delete
                                        while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.T && cki.Key != ConsoleKey.Delete)
                                        {

                                            cki = Console.ReadKey(false);

                                            if (cki.Key == ConsoleKey.Escape) { proceed = 1; }
                                            if (cki.Key == ConsoleKey.T) { proceed = 0; }
                                            if (cki.Key == ConsoleKey.Delete) { proceed = 2; }


                                        }

                                        if (proceed == 2)
                                        {
                                            mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[itemIndex] = null;
                                        }

                                        if (proceed == 0)
                                        {//transfer
                                            transferItem(itemIndex, playerIndex);
                                        }

                                    }

                                }
                                if (itemType == 1)
                                {
                                    string itemKey = "";
                                    switch (cki.Key)
                                    {
                                        case ConsoleKey.E:
                                            itemKey = "helmet";
                                            break;
                                        case ConsoleKey.F:
                                            itemKey = "chest";
                                            break;
                                        case ConsoleKey.G:
                                            itemKey = "gauntlets";
                                            break;
                                        case ConsoleKey.H:
                                            itemKey = "boots";
                                            break;
                                        case ConsoleKey.I:
                                            itemKey = "weapon";
                                            break;
                                    }

                                    if (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey] == null) continue;
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    if (itemKey=="weapon")
                                    {

                                        if((mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey])as MeleWeapon!=null)
                                        {
                                            MeleWeapon mItem = (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey]) as MeleWeapon;

                                            Console.WriteLine("Physical attack: {0} - {1}",mItem.minPhysicalAttack, mItem.maxPhysicalAttack);
                                            Console.WriteLine("Magical attack: {0} - {1}", mItem.minMagicalAttack, mItem.maxMagicalAttack);

                                        }
                                        if ((mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey]) as RangedWeapon != null)
                                        {
                                            RangedWeapon mItem = (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey]) as RangedWeapon;

                                            Console.WriteLine("Physical attack: {0} - {1}", mItem.minRangedPhysicalAttack, mItem.maxRangedPhysicalAttack);
                                            Console.WriteLine("Magical attack: {0} - {1}", mItem.minMagicalAttack, mItem.maxMagicalAttack);

                                        }

                                    }
                                    else
                                    {
                                        //appearal
                                        Appearal mAppearal = (mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey]) as Appearal;

                                        Console.WriteLine("Physical armor: {0}", mAppearal.physicalArmor);
                                        Console.WriteLine("Magical armor: {0}", mAppearal.magicalArmor);

                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("Unequip this item? ( y/n )");
                                    cki = new ConsoleKeyInfo();
                                    int proceed = 0;
                                    while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N)
                                    {

                                        cki = Console.ReadKey(false);

                                        if (cki.Key == ConsoleKey.Y) { proceed = 1; }
                                        if (cki.Key == ConsoleKey.N) { proceed = 0; }

                                    }

                                    if (proceed == 1 && mPlayer.mTeam.mCharacters[playerIndex].mInventory.emptySlotsCount() != 0)
                                    {
                                        Item temp = mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey];
                                        mPlayer.mTeam.mCharacters[playerIndex].mInventory.mEquipedItems[itemKey] = null;
                                        mPlayer.mTeam.mCharacters[playerIndex].mInventory.mItems[mPlayer.mTeam.mCharacters[playerIndex].mInventory.firstEmptySlot()] = temp;
                                    }


                                }

                                //now we have to process the input

                                //cki = new ConsoleKeyInfo();
                            }
                        }

                        else if (choice == 1)
                        {
                            int selectedIndex = 0;
                            while (!exitLocalInventory)
                            {
                                //this should be in a switch or sth

                                Console.Clear();
                                mPlayer.mTeam.teamInventory.display(selectedIndex);

                                cki = new ConsoleKeyInfo();
                                int action = 0;// 1 - change selected;  2 - go back; 3 - do sth with selected item
                                               //select an item or go back
                                while (cki.Key != ConsoleKey.A && cki.Key != ConsoleKey.W && cki.Key != ConsoleKey.S && cki.Key != ConsoleKey.D && cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Enter)
                                {
                                    cki = Console.ReadKey(false);

                                    if (cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.S || cki.Key == ConsoleKey.D) { action = 1; }
                                    if (cki.Key == ConsoleKey.Escape) action = 2;
                                    if (cki.Key == ConsoleKey.Enter) action = 3;
                                }

                                if (action == 1)
                                {
                                    if (cki.Key == ConsoleKey.D)
                                    {
                                        if ((selectedIndex + 1) % 4 == 0)//rightmost one
                                            selectedIndex -= 3;
                                        else selectedIndex += 1;
                                    }
                                    else if (cki.Key == ConsoleKey.A)
                                    {
                                        if (selectedIndex % 4 == 0)//leftmost one
                                            selectedIndex += 3;
                                        else selectedIndex -= 1;
                                    }
                                    else if (cki.Key == ConsoleKey.W)
                                    {
                                        if (selectedIndex < 4)//topmost one
                                            selectedIndex = mPlayer.mTeam.teamInventory.mItems.Count - (4 - selectedIndex);
                                        else selectedIndex -= 4;
                                    }
                                    else if (cki.Key == ConsoleKey.S)
                                    {
                                        if (selectedIndex >= mPlayer.mTeam.teamInventory.mItems.Count - 4)//bottommost one
                                            selectedIndex = selectedIndex % 4;
                                        else selectedIndex += 4;
                                    }
                                }
                                else if (action == 2)
                                {
                                    exitLocalInventory = true;

                                }
                                else if (action == 3)
                                {
                                    if (mPlayer.mTeam.teamInventory.mItems[selectedIndex] == null) continue;//no item

                                    Console.WriteLine("You can give this item to: (0 - 2), Esc to go back");
                                    Console.WriteLine("0. {0} has {1} empty slots in inventory",
                                        mPlayer.mTeam.mCharacters[0].name,
                                        mPlayer.mTeam.mCharacters[0].mInventory.emptySlotsCount()
                                        );
                                    Console.WriteLine("1. {0} has {1} empty slots in inventory",
                                        mPlayer.mTeam.mCharacters[1].name,
                                        mPlayer.mTeam.mCharacters[1].mInventory.emptySlotsCount()
                                        );
                                    Console.WriteLine("2. {0} has {1} empty slots in inventory",
                                        mPlayer.mTeam.mCharacters[2].name,
                                        mPlayer.mTeam.mCharacters[2].mInventory.emptySlotsCount()
                                        );
                                    Console.WriteLine("To delete item press ( delete )");

                                    cki = new ConsoleKeyInfo();
                                    //action,  0 - go back; 1 - give item; 2 - delete item
                                    while (cki.Key != ConsoleKey.D0 && cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Delete)
                                    {

                                        cki = Console.ReadKey(false);

                                        if (cki.Key == ConsoleKey.Escape) action = 0;
                                        if (cki.Key == ConsoleKey.D0 || cki.Key == ConsoleKey.D1 || cki.Key == ConsoleKey.D2) action = 1;
                                        if (cki.Key == ConsoleKey.Delete) action = 2;
                                    }

                                    if (action == 2) { mPlayer.mTeam.teamInventory.mItems[selectedIndex] = null; }
                                    if (action == 0) continue;
                                    if (action == 1)
                                    {
                                        int destinationPerson = (int)cki.Key % 48; // 0 - 2

                                        if (mPlayer.mTeam.mCharacters[destinationPerson].mInventory.emptySlotsCount() == 0) continue;//no free slots for items

                                        int playerItemSlot = mPlayer.mTeam.mCharacters[destinationPerson].mInventory.firstEmptySlot();

                                        mPlayer.mTeam.mCharacters[destinationPerson].mInventory.mItems[playerItemSlot] = mPlayer.mTeam.teamInventory.mItems[selectedIndex];
                                        mPlayer.mTeam.teamInventory.mItems[selectedIndex] = null;
                                    }

                                }
                                cki = new ConsoleKeyInfo();//without it inventory selection loop is broken
                            }
                        }

                    }


                }


            }

        }
    }
}
