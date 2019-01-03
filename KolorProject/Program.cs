using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolorProject
{


    

    class Program
    {

        

        static void Main(string[] args)
        {

            CorruptedSanta s = new CorruptedSanta();
            s.mAttributes.Constitiution += 1;
            s.mBasicStats.currentLevel += 3;
            s.calculateBasicStats();
            s.displayStats();
            Console.ReadKey();

            GameController g = new GameController();
            g.initialize();
            g.start();
            Console.WriteLine("you have lost :("); //spoiler alert xd
            Console.ReadKey();

        }

        
    }

   

}
