using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

// Token: 0x0200095C RID: 2396
public class IPManager
{
	// Token: 0x06004293 RID: 17043 RVA: 0x001A2DB0 File Offset: 0x001A11B0
	public static string GetIP(ADDRESSFAM addfam)
	{
		if (addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
		{
			return null;
		}
		string result = string.Empty;
		foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
		{
			NetworkInterfaceType networkInterfaceType = NetworkInterfaceType.Wireless80211;
			NetworkInterfaceType networkInterfaceType2 = NetworkInterfaceType.Ethernet;
			if ((networkInterface.NetworkInterfaceType == networkInterfaceType || networkInterface.NetworkInterfaceType == networkInterfaceType2) && networkInterface.OperationalStatus == OperationalStatus.Up)
			{
				foreach (UnicastIPAddressInformation unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
				{
					if (addfam == ADDRESSFAM.IPv4)
					{
						if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							result = unicastIPAddressInformation.Address.ToString();
						}
					}
					else if (addfam == ADDRESSFAM.IPv6 && unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetworkV6)
					{
						result = unicastIPAddressInformation.Address.ToString();
					}
				}
			}
		}
		return result;
	}
}
