using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
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
