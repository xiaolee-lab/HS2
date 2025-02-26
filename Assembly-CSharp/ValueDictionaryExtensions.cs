using System;

// Token: 0x02000A8D RID: 2701
public static class ValueDictionaryExtensions
{
	// Token: 0x06004F93 RID: 20371 RVA: 0x001E999E File Offset: 0x001E7D9E
	public static ValueDictionary<TKey2, TValue> New<TKey1, TKey2, TValue>(this ValueDictionary<TKey1, TKey2, TValue> dictionary)
	{
		return new ValueDictionary<TKey2, TValue>();
	}

	// Token: 0x06004F94 RID: 20372 RVA: 0x001E99A5 File Offset: 0x001E7DA5
	public static ValueDictionary<TKey2, TKey3, TValue> New<TKey1, TKey2, TKey3, TValue>(this ValueDictionary<TKey1, TKey2, TKey3, TValue> dictionary)
	{
		return new ValueDictionary<TKey2, TKey3, TValue>();
	}

	// Token: 0x06004F95 RID: 20373 RVA: 0x001E99AC File Offset: 0x001E7DAC
	public static ValueDictionary<TKey2, TKey3, TKey4, TValue> New<TKey1, TKey2, TKey3, TKey4, TValue>(this ValueDictionary<TKey1, TKey2, TKey3, TKey4, TValue> dictionary)
	{
		return new ValueDictionary<TKey2, TKey3, TKey4, TValue>();
	}

	// Token: 0x06004F96 RID: 20374 RVA: 0x001E99B3 File Offset: 0x001E7DB3
	public static ValueDictionary<TKey2, TKey3, TKey4, TKey5, TValue> New<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>(this ValueDictionary<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> dictionary)
	{
		return new ValueDictionary<TKey2, TKey3, TKey4, TKey5, TValue>();
	}

	// Token: 0x06004F97 RID: 20375 RVA: 0x001E99BA File Offset: 0x001E7DBA
	public static ValueDictionary<TKey2, TKey3, TKey4, TKey5, TKey6, TValue> New<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>(this ValueDictionary<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue> dictionary)
	{
		return new ValueDictionary<TKey2, TKey3, TKey4, TKey5, TKey6, TValue>();
	}
}
