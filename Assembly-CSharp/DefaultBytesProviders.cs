using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000484 RID: 1156
internal static class DefaultBytesProviders
{
	// Token: 0x0600155C RID: 5468 RVA: 0x00084174 File Offset: 0x00082574
	static DefaultBytesProviders()
	{
		Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
		Dictionary<Type, object> dictionary2 = dictionary;
		Type typeFromHandle = typeof(int);
		if (DefaultBytesProviders.<>f__mg$cache0 == null)
		{
			DefaultBytesProviders.<>f__mg$cache0 = new Func<int, byte[]>(BitConverter.GetBytes);
		}
		dictionary2.Add(typeFromHandle, new BytesProvider<int>(DefaultBytesProviders.<>f__mg$cache0));
		Dictionary<Type, object> dictionary3 = dictionary;
		Type typeFromHandle2 = typeof(long);
		if (DefaultBytesProviders.<>f__mg$cache1 == null)
		{
			DefaultBytesProviders.<>f__mg$cache1 = new Func<long, byte[]>(BitConverter.GetBytes);
		}
		dictionary3.Add(typeFromHandle2, new BytesProvider<long>(DefaultBytesProviders.<>f__mg$cache1));
		Dictionary<Type, object> dictionary4 = dictionary;
		Type typeFromHandle3 = typeof(short);
		if (DefaultBytesProviders.<>f__mg$cache2 == null)
		{
			DefaultBytesProviders.<>f__mg$cache2 = new Func<short, byte[]>(BitConverter.GetBytes);
		}
		dictionary4.Add(typeFromHandle3, new BytesProvider<short>(DefaultBytesProviders.<>f__mg$cache2));
		Dictionary<Type, object> dictionary5 = dictionary;
		Type typeFromHandle4 = typeof(float);
		if (DefaultBytesProviders.<>f__mg$cache3 == null)
		{
			DefaultBytesProviders.<>f__mg$cache3 = new Func<float, byte[]>(BitConverter.GetBytes);
		}
		dictionary5.Add(typeFromHandle4, new BytesProvider<float>(DefaultBytesProviders.<>f__mg$cache3));
		DefaultBytesProviders._providers = dictionary;
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x00084255 File Offset: 0x00082655
	public static BytesProvider<T> GetDefaultProvider<T>()
	{
		return (BytesProvider<T>)DefaultBytesProviders._providers[typeof(T)];
	}

	// Token: 0x04001852 RID: 6226
	private static Dictionary<Type, object> _providers;

	// Token: 0x04001853 RID: 6227
	[CompilerGenerated]
	private static Func<int, byte[]> <>f__mg$cache0;

	// Token: 0x04001854 RID: 6228
	[CompilerGenerated]
	private static Func<long, byte[]> <>f__mg$cache1;

	// Token: 0x04001855 RID: 6229
	[CompilerGenerated]
	private static Func<short, byte[]> <>f__mg$cache2;

	// Token: 0x04001856 RID: 6230
	[CompilerGenerated]
	private static Func<float, byte[]> <>f__mg$cache3;
}
