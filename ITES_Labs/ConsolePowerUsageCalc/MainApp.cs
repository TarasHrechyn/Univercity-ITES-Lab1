using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsolePowerUsageCalc
{
    class CalcApp
    {
        // приватний список обєктів
        private List<PowerItem> Items;

        // конструктор
        public CalcApp()
        {
            // ініціалізація списку (створення нового)
            Items = new List<PowerItem>();
        }

        /// <summary>
        /// Ввід даних з консолі
        /// приклад неправильної реалізації методу з огляду декомпозиції 
        /// інтерфейсу користувача і логіки роботи обєкта
        /// </summary>
        public void InputData()
        {
            Console.WriteLine("Please enter item data. -1 for any value means exit");
            while (true)
            {
                // приклад ввід даних
                try
                {
                    Console.Write("Vnom, kV=");
                    double Vnom = Convert.ToDouble(Console.ReadLine());
                    if (Vnom <= 0.0)
                        break;
                    Console.Write("Snom, kVA=");
                    double Snom = Convert.ToDouble(Console.ReadLine());
                    if (Snom <= 0.0)
                        break;
                    // Створення обєкта
                    PowerItem Item = new PowerItem(Vnom, Snom);
                    Items.Add(Item);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid value: {0}", e.Message);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// ввід з файла TXT
        /// приклади правильної ізоляції даних і вводу інформації
        /// </summary>
        public void InputDataFromTxt(string aFileName)
        {
            string Line;
            try
            {
                StreamReader File = new System.IO.StreamReader(aFileName);
                try
                {
                    while ((Line = File.ReadLine()) != null)
                    {
                        if (!Line.StartsWith("#"))
                        {
                            Line = Line.Trim(' ');
                            string[] Values = Line.Split(' ');
                            if (Values.Length >= 2)
                            {
                                double cosFi = 0.0;
                                if (Values.Length > 3)
                                {
                                    cosFi = Convert.ToDouble(Values[2]);
                                }

                                PowerItem Item = new PowerItem(
                                        Convert.ToDouble(Values[0]),
                                        Convert.ToDouble(Values[1]),
                                        cosFi
                                    );

                                Items.Add(Item);
                            }
                            else
                            {
                                break;
                            }
                        }

                    }
                }
                finally
                {
                    File.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid text content {0}: {1}", aFileName, e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// ввід з файла XML
        /// </summary>
        public void InputDataFromXML(string aFileName)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(aFileName);
                XmlNodeList xList = xDoc.DocumentElement.SelectNodes("items/item");
                foreach (XmlNode node in xList)
                {
                    PowerItem Item = new PowerItem();
                    Item.LoadFromXML(node);
                    Items.Add(Item);
                }
            }
            // опрацювання помилок
            catch (Exception e)
            {
                Console.WriteLine("Invalid document content {0}: {1}", aFileName, e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// ввід з файла XML
        /// </summary>
        public void SaveToXML(string aFileName)
        {
            XmlNode itemsNode = XmlUtils.CreateDocument("items");
            try
            {
                foreach (PowerItem Item in Items)
                {
                    XmlNode itemNode = XmlUtils.CreateSubNode(itemsNode, "item");
                    Item.SaveToXML(itemNode);
                }
                itemsNode.OwnerDocument.Save(aFileName);
            }
            // опрацювання помилок
            catch (Exception e)
            {
                Console.WriteLine("Unable to save file: {0}, Error: {1}", aFileName, e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// ввивід результату
        /// </summary>
        public void OutputResult()
        {
            double Ssum = 0.0;

            foreach (PowerItem item in Items)
            {
                Ssum += item.Snom;
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\nCount {0}, Ssum {1}", Items.Count, Ssum);
            Console.WriteLine("\nPress any key");
            Console.ReadKey();
        }

        /// <summary>
        /// Приклад Запиту
        /// </summary>
        /// <param name="aVoltage"> параметри запиту </param>
        public void PrintVoltage(double aVoltage)
        {
            Console.WriteLine("Items at voltage {0}", aVoltage);
            var subItems = from item in Items
                           where item.Vnom == aVoltage
                           select item;

            foreach (PowerItem item in subItems)
                Console.WriteLine(item.ToString());

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
        }
    }
}
