using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{
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
}
