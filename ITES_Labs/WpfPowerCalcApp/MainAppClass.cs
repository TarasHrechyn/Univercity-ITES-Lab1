using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using System.Data;

namespace ConsolePowerUsageCalcVisual
{
    [Serializable]
    public class PowerStation
    {
        // список обєктів
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
        /// повернення результату
        /// </summary>
        public Complex GetSum(List<PowerItem> SourceList = null)
        {
            Complex Ssum = Complex.Zero;
            List<PowerItem> sourceList = (SourceList == null) ? Items : SourceList;

            foreach (PowerItem item in sourceList)
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
            if (aVoltage == 0.0)
            {
                return Items;
            } else
            {
                var subItems = from item in Items
                               where (item.Vnom == aVoltage)
                               select item;
                return new List<PowerItem>(subItems);
            }
        }
    }
}
