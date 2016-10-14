// Опис використання модулів
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Numerics;
using System.Xml.Serialization;

// простір імен проекту
namespace ConsolePowerUsageCalcVisual
{
    [XmlInclude(typeof(PowerItem))]
    [XmlInclude(typeof(PowerLoad))]
    [XmlInclude(typeof(PowerCapacitor))]

    // Опис Класу
    [Serializable]
    public class PowerItem
    {
        // приватне поле
        private double _Vnom;

        // Конструктор 
        public PowerItem()
        {   
            Vnom = 220;
        }

        // публічне поле
        public Complex Snom
        {
            get { return new Complex(Pnom, Qnom); }
        }
        [XmlAttribute]
        public double Pnom
        {
            get;  set;
        }

        [XmlAttribute]
        public double Qnom
        {
            get; set;
        }

        // властивість на читання і запис
        [XmlAttribute]
        public double Vnom
        {
            get { return (_Vnom > 0.0 ? _Vnom : 220); }
            set { _Vnom = value; }
        }
        // розрахункова властивість лише для читання
        public double Inom {
            get { return Snom.Magnitude / Vnom / Math.Sqrt(3); }
        }

        // методи
        public override string ToString()
        {
            return string.Format("V = {0} V, S = {1:F1} VA, I = {2:F3} A", Vnom, Snom, Inom);
        }        
    }

    [Serializable]
    public class PowerCapacitor : PowerItem
    {
        public PowerCapacitor():
            base()
        {
        }
        public override string ToString()
        {
            return "Capacitor - " + base.ToString();
        }
    }

    [Serializable]
    public class PowerLoad : PowerItem
    {
        public PowerLoad():
            base()
        {
        }
        public override string ToString()
        {
            return "Load - " + base.ToString();
        }
    }
}
