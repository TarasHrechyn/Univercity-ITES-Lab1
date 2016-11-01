using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IPowerCalcService
{
    public string GetPowerItemsData(int voltage)
    {
		return string.Format("You entered: {0}", voltage);
	}

    public string AddPowerItem(int voltage, double Pnom, double Qnom)
    {
        return string.Format("You added Item: U={0}, S=({1}, {2}) ", voltage, Pnom, Qnom);
    }

    public PowerItemType GetItemById(int id)
    {
        return new PowerItemType();
    }

    //public PowerItemType GetDataUsingDataContract(PowerItemType composite)
    //{
    //    if (composite == null)
    //    {
    //        throw new ArgumentNullException("composite");
    //    }
    //    if (composite.BoolValue)
    //    {
    //        composite.StringValue += "Suffix";
    //    }
    //    return composite;
    //}
}
