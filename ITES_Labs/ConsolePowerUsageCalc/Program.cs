using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace ConsolePowerUsageCalc
{
    class Program
    {

        public enum MenuItem
        {
            Unknown,

            Exit,
            InputFromCon,
            InputFromTxt,
            InputFromXML,
            OutputResult,
            PrintByVoltage,
            SaveToXML,

            First = Exit,
            Last = SaveToXML
        }
        
        static public MenuItem ReadMenu()
        {
            Console.WriteLine("Menu");
            for (MenuItem dispMenuItem = MenuItem.First; dispMenuItem <= MenuItem.Last; dispMenuItem++)
                Console.WriteLine("{0} - {1}", (int)dispMenuItem, dispMenuItem.ToString());

            int readValue = Convert.ToInt32(Console.ReadLine());
            if ((readValue >= (int)MenuItem.First) && (readValue <= (int)MenuItem.Last))
            {
                return (MenuItem)readValue;
            }
            else
            {   
                return MenuItem.Unknown;
            }
        }

        static void Main(string[] args)
        {
            MenuItem userMenu;
            // налаштування
            
            // Створення обєкта
            CalcApp App = new CalcApp();
            Console.WriteLine("Program Example ...");

            #region Цикл роботи меню
            do
            {
                userMenu = ReadMenu();
                switch (userMenu)
                {
                    case MenuItem.InputFromCon:
                        App.InputData();
                        break;
                    case MenuItem.InputFromTxt:
                        App.InputDataFromTxt("Data.txt");
                        break;
                    case MenuItem.InputFromXML:
                        App.InputDataFromXML("Data.xml");
                        break;
                    case MenuItem.OutputResult:
                        App.OutputResult();
                        break;
                    case MenuItem.SaveToXML:
                        App.SaveToXML("Data2.xml");
                        break;
                    case MenuItem.PrintByVoltage:
                        App.PrintVoltage(220);
                        break;
                    case MenuItem.Exit:
                        // Вихід з циклу меню
                        break;
                    default:
                        Console.Beep();
                        break;
                }
                Console.Clear();
            } while (userMenu != MenuItem.Exit);
            #endregion
        }
    }    
}
