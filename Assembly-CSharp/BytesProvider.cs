using System;

// Token: 0x02000483 RID: 1155
public class BytesProvider<T> : IBytesProvider<T>
{
	// Token: 0x06001559 RID: 5465 RVA: 0x0008414D File Offset: 0x0008254D
	internal BytesProvider(Func<T, byte[]> conversion)
	{
		this._conversion = conversion;
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600155A RID: 5466 RVA: 0x0008415C File Offset: 0x0008255C
	public static BytesProvider<T> Default
	{
		get
		{
			return DefaultBytesProviders.GetDefaultProvider<T>();
		}
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x00084163 File Offset: 0x00082563
	public byte[] GetBytes(T value)
	{
		return this._conversion(value);
	}

	// Token: 0x04001851 RID: 6225
	private Func<T, byte[]> _conversion;
}
