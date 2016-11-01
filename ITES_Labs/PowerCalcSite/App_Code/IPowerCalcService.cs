using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IPowerCalcService
{
	[OperationContract]
	string GetPowerItemsData(int voltage);

	[OperationContract]
	PowerItemType GetItemById(int id);
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class PowerItemType
{
	int _Unom = 220;
	string _Name = "Power Item ";

	[DataMember]
	public int Unom
	{
		get { return _Unom; }
		set { _Unom = value; }
	}

	[DataMember]
	public string Name
	{
		get { return _Name; }
		set { _Name = value; }
	}
}
