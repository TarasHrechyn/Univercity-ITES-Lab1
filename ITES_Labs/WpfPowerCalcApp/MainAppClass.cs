using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsolePowerUsageCalcVisual
{
    [Serializable]
    public class PowerStation
    {
        // список обєктів
        [XmlArray]
        public List<PowerItem> Items = new List<PowerItem>();
       
        /// <summary>
        /// Конструктор
        /// </summary>
        public PowerStation()
        {            
        }
        /// <summary>
        /// ввід з файла XML
        /// </summary>
        public static PowerStation LoadFromXML(string aFileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PowerStation));
            try
            {
                using (Stream stream = new FileStream(aFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return (PowerStation)xml.Deserialize(stream);
                }
            }
            // опрацювання помилок
            catch 
            {
                return new PowerStation();
//                Console.WriteLine("Invalid document content  {0}: {1}", aFileName, e.Message);
//                Console.ReadKey();
            }

        }

        /// <summary>
        /// ввід з файла XML
        /// </summary>
        public void SaveToXML(string aFileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PowerStation));
            using (Stream stream = new FileStream(aFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml.Serialize(stream, this);
            } 
        }

        /// <summary>
        /// ввивід результату
        /// </summary>
        public Complex GetSum()
        {
            Complex Ssum = Complex.Zero;

            foreach (PowerItem item in Items)
            {
                Ssum += item.Snom;
            }
            return Ssum;
        }

        /// <summary>
        /// Приклад Запиту
        /// </summary>
        /// <param name="aVoltage"> параметри запиту </param>
        internal List<PowerItem> ItemsByVoltage(double aVoltage)
        {
            Console.WriteLine("Items at voltage {0}", aVoltage);
            var subItems = from item in Items
                           where ((item.Vnom == aVoltage) || (aVoltage == 0.0))
                           select item;
            return new List<PowerItem>(subItems);
        }
    }
}
