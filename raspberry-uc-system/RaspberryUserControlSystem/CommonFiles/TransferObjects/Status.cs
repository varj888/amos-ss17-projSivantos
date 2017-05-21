using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects{
	/// <summary>
	/// Unit of Transfer used by the StatusConnClient Class
	/// only contains some random test variables at moment
	/// </summary>
	[DataContract]
	class Status
	{
		[DataMember]
		int testVariable;
	}
}
