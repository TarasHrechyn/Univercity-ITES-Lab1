// Опис використання модулів
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Numerics;

// простір імен проекту
namespace ConsolePowerUsageCalc
{
    // Опис Класу
    
    class PowerItem
    {
        private const string xattrVnom = "Vnom";
        private const string xattrSnom = "Snom";
        private const string xattrCosFi = "CosFi";

        // приватне поле
        private double _Vnom;
        private System.Numerics.Complex _S;

        // Конструктор зі значенями по замовчуванні
        public PowerItem(double defV = 0.0, double defS = 0.0, double defCosFi = 0.0)
        {
            _S = Complex.FromPolarCoordinates(defS, defCosFi);
            Vnom = defV;
        }

        // публічне поле
        public double Snom
        {
            get { return _S.Magnitude; }
        }
        // властивість на читання і запис
        public double Vnom
        {
            get { return (_Vnom > 0.0 ? _Vnom : 220); }
            set { _Vnom = value; }
        }
        // розрахункова властивість лише для читання
        public double Inom {
            get { return Snom / Vnom / 1.73; }
        }

        // методи
        public void SaveToXML(XmlNode node)
        {
            XmlUtils.SetAttribute(node, xattrVnom, _Vnom);
            XmlUtils.SetAttribute(node, xattrSnom, Snom);
        }
        public void LoadFromXML(XmlNode node)
        {
            _Vnom = XmlUtils.GetAttribute(node, xattrVnom, _Vnom);
            _S = Complex.FromPolarCoordinates(
                XmlUtils.GetAttribute(node, xattrSnom, 0.0),
                XmlUtils.GetAttribute(node, xattrCosFi, 0.0));
        }
        public override string ToString()
        {
            return string.Format("V = {0} kV, S = {1:F1} kVA, I = {2:F3} kA", Vnom, Snom, Inom);
        }        
    }
}
