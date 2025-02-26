using System;

// Token: 0x020011A5 RID: 4517
public class SimpleSingleton<T> where T : class, new()
{
	// Token: 0x06009491 RID: 38033 RVA: 0x003D4CC2 File Offset: 0x003D30C2
	private SimpleSingleton()
	{
	}

	// Token: 0x17001F86 RID: 8070
	// (get) Token: 0x06009492 RID: 38034 RVA: 0x003D4CCA File Offset: 0x003D30CA
	public static T Instance
	{
		get
		{
			if (SimpleSingleton<T>.instance == null)
			{
				SimpleSingleton<T>.instance = Activator.CreateInstance<T>();
			}
			return SimpleSingleton<T>.instance;
		}
	}

	// Token: 0x0400778D RID: 30605
	private static T instance;
}
