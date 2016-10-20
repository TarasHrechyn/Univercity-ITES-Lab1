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
using System.Data.SqlClient;

namespace PowerUsageCalc
{
    [Serializable]
    public class PowerStation
    {

        private double GetAsDBFloat(object databaseValue, double defaultValue = 0.0)
        {
            if (databaseValue is DBNull || databaseValue == null)
                return defaultValue;
            else
                return ((double?)(databaseValue)).GetValueOrDefault(defaultValue);
        }
        private int GetAsDBInt(object databaseValue, int defaultValue = 0)
        {
            if (databaseValue is DBNull || databaseValue == null)
                return defaultValue;
            else
                return ((int?)(databaseValue)).GetValueOrDefault(defaultValue);
        }
        private string GetAsDBString(object databaseValue, string defaultValue = "")
        {
            if (databaseValue is DBNull || databaseValue == null)
                return defaultValue;
            else
                return ((string)(databaseValue));
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public PowerStation()
        {            
        }

        /// <summary>
        /// повернення результату
        /// </summary>
        public Complex GetSum(List<PowerItem> items )
        {
            Complex sum = new Complex(0, 0);
            foreach (PowerItem item in items)
            {
                sum += item.Snom;
            }

            return sum;
        }

        /// Приклад роботи з базою даних

        private SqlConnection _connection;

        private void CheckConnected()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection();
                try
                {
                    _connection.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=PowerGridData;Integrated Security=True";
                    _connection.Open();
                }
                catch
                {
                    _connection = null;
                    throw;
                }
            }
        }
        /// <summary>
        /// Добавлення Запису в БД
        /// </summary>
        /// <param name="Item"></param>
        public void AddItem(PowerItem Item)
        {
            // відкрити зєднання
            CheckConnected();
            // створити запит   
            string sql = string.Format("Insert Into PowerItems" +     
                "(Name, Unom, Pnom, Qnom, Type) Values" +
                "('{0}', '{1}', '{2}', '{3}', '{4}')", 
                Item.Name, Item.Unom, Item.Pnom, Item.Qnom, Item.GetType().FullName);
                // Execute using our connection.   
            using (SqlCommand cmd = new SqlCommand(sql, this._connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Приклад Запиту
        /// </summary>
        /// <param name="Voltage"> параметри запиту </param>
        public List<PowerItem> GetItemsByVoltage(int Voltage)
        {
            CheckConnected();
            List<PowerItem> list = new List<PowerItem>();
            // Prep command object.   
            string sql = string.Format("Select * From PowerItems where Unom={0}", Voltage);
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Type type = Type.GetType(GetAsDBString(dr["Type"]));
                    PowerItem item;
                    if (type == null)
                        item = new PowerItem();
                    else 
                        item = (PowerItem)Activator.CreateInstance(type);
                    item.Id = GetAsDBInt(dr["Id"]);
                    item.Name = GetAsDBString(dr["Name"]);
                    item.Unom = GetAsDBInt(dr["Unom"]);
                    item.Pnom = GetAsDBFloat(dr["Pnom"]);
                    item.Qnom = GetAsDBFloat(dr["Qnom"]);
                    list.Add(item);
                }
                dr.Close();
            }
            return list;
        }
    }
}
