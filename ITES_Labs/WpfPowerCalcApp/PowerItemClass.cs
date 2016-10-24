// Опис використання модулів
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

// простір імен проекту
namespace PowerUsageCalc
{
    // Опис Класу
    public class PowerItem
    {
        // Конструктор 
        public PowerItem()
        {   
            Unom = 220;
        }

        // приватне поле
        private int _Vnom;

        // службові поля ідентифікації
        public int Id
        {
            get; set;
        }
        public virtual string Name
        {
            get; set;
        }

        // публічні поля
        public Complex Snom
        {
            get { return new Complex(Pnom, Qnom); }
        }
        public double Pnom
        {
            get; set;
        }
        public double Qnom
        {
            get; set;
        }

        // властивість на читання і запис
        public int Unom
        {
            get { return (_Vnom > 0 ? _Vnom : 220); }
            set { _Vnom = value; }
        }
        // розрахункова властивість лише для читання
        public double Inom {
            get { return (Snom.Magnitude / Unom / Math.Sqrt(3)); }
        }
        public string InomDisp
        {
            get { return Inom.ToString("F3"); }
        }

        // методи
        public override string ToString()
        {
            return string.Format("{0} V = {1} V, S = {2:F1} VA, I = {3:F3} A", Name, Unom, Snom, Inom);
        }        
    }

    public class PowerCapacitor : PowerItem
    {
        public override string Name
        {
            get {
                return (base.Name == "")?"Capacitor":base.Name;
            }
        }
    }

    public class PowerLoad : PowerItem
    {
        public override string Name
        {
            get
            {
                return (base.Name == "") ? "Load" : base.Name;
            }
        }
    }
}
