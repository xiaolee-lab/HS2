using System;
using System.Collections.Generic;

// Token: 0x02000485 RID: 1157
public class UsGeneric
{
	// Token: 0x0600155F RID: 5471 RVA: 0x00084278 File Offset: 0x00082678
	public static IEnumerable<List<T>> Slice<T>(List<T> objList, int slice)
	{
		for (int i = 0; i < objList.Count; i += slice)
		{
			yield return objList.GetRange(i, Math.Min(objList.Count - i, slice));
		}
		yield break;
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000842A2 File Offset: 0x000826A2
	public static byte[] Convert<T>(T value)
	{
		return BytesProvider<T>.Default.GetBytes(value);
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000842B0 File Offset: 0x000826B0
	public static object Convert<T>(byte[] buffer, int startIndex)
	{
		if (typeof(T) == typeof(int))
		{
			return BitConverter.ToInt32(buffer, startIndex);
		}
		if (typeof(T) == typeof(short))
		{
			return BitConverter.ToInt16(buffer, startIndex);
		}
		if (typeof(T) == typeof(float))
		{
			return BitConverter.ToSingle(buffer, startIndex);
		}
		return null;
	}
}
