using System;
using System.Collections.Generic;

// Token: 0x0200047E RID: 1150
public class UsCmdUtil
{
	// Token: 0x06001552 RID: 5458 RVA: 0x00083FC8 File Offset: 0x000823C8
	public static List<int> ReadIntList(UsCmd c)
	{
		List<int> list = new List<int>();
		int num = c.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			list.Add(c.ReadInt32());
		}
		return list;
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x00084004 File Offset: 0x00082404
	public static void WriteIntList(UsCmd c, List<int> l)
	{
		c.WriteInt32(l.Count);
		foreach (int value in l)
		{
			c.WriteInt32(value);
		}
	}
}
