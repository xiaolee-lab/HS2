using System;
using System.ComponentModel;

// Token: 0x0200048A RID: 1162
public class SysPost
{
	// Token: 0x06001566 RID: 5478 RVA: 0x00084515 File Offset: 0x00082915
	public static bool AssertException(bool expr, string msg)
	{
		if (expr)
		{
			return true;
		}
		throw new Exception(msg);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x00084525 File Offset: 0x00082925
	public static void InvokeMulticast(object sender, MulticastDelegate md)
	{
		if (md != null)
		{
			SysPost.InvokeMulticast(sender, md, null);
		}
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x00084538 File Offset: 0x00082938
	public static void InvokeMulticast(object sender, MulticastDelegate md, EventArgs e)
	{
		if (md == null)
		{
			return;
		}
		foreach (SysPost.StdMulticastDelegation stdMulticastDelegation in md.GetInvocationList())
		{
			ISynchronizeInvoke synchronizeInvoke = stdMulticastDelegation.Target as ISynchronizeInvoke;
			try
			{
				if (synchronizeInvoke != null && synchronizeInvoke.InvokeRequired)
				{
					synchronizeInvoke.Invoke(stdMulticastDelegation, new object[]
					{
						sender,
						e
					});
				}
				else
				{
					stdMulticastDelegation(sender, e);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Event handling failed. \n");
				Console.WriteLine("{0}:\n", ex.ToString());
			}
		}
	}

	// Token: 0x0200048B RID: 1163
	// (Invoke) Token: 0x0600156A RID: 5482
	public delegate void StdMulticastDelegation(object sender, EventArgs e);
}
