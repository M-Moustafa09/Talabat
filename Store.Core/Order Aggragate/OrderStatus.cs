using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Order_Aggragate
{
    public enum OrderStatus
	{
		[EnumMember(Value ="Pending")]
		Pending,
		[EnumMember(Value = "PaymentReceived")]
		PaymentReceived,
		[EnumMember(Value = "PaumentFailed")]
		PaumentFailed
	}
}
