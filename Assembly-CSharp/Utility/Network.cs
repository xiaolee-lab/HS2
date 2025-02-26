using System;
using System.Net.NetworkInformation;

namespace Utility
{
	// Token: 0x0200118E RID: 4494
	public static class Network
	{
		// Token: 0x06009428 RID: 37928 RVA: 0x003D34B0 File Offset: 0x003D18B0
		public static string GetMACAddress()
		{
			string text = string.Empty;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (allNetworkInterfaces != null)
			{
				foreach (NetworkInterface networkInterface in allNetworkInterfaces)
				{
					PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
					if (physicalAddress != null)
					{
						if (physicalAddress.GetAddressBytes().Length == 6)
						{
							string str = physicalAddress.ToString();
							text += str;
						}
					}
				}
			}
			return text;
		}
	}
}
