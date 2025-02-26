using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020011BC RID: 4540
[Serializable]
public class YS_Dictionary<TKey, TValue, TPair> where TPair : YS_KeyAndValue<TKey, TValue>, new()
{
	// Token: 0x060094F2 RID: 38130 RVA: 0x003D6C3A File Offset: 0x003D503A
	public YS_Dictionary()
	{
		this.list = new List<TPair>();
	}

	// Token: 0x060094F3 RID: 38131 RVA: 0x003D6C4D File Offset: 0x003D504D
	public Dictionary<TKey, TValue> GetTable()
	{
		if (this.table == null)
		{
			this.table = YS_Dictionary<TKey, TValue, TPair>.ConvertListToDictionary(this.list);
		}
		return this.table;
	}

	// Token: 0x060094F4 RID: 38132 RVA: 0x003D6C74 File Offset: 0x003D5074
	public TValue GetValue(TKey key)
	{
		if (this.GetTable().Keys.Contains(key))
		{
			return this.GetTable()[key];
		}
		return default(TValue);
	}

	// Token: 0x060094F5 RID: 38133 RVA: 0x003D6CAD File Offset: 0x003D50AD
	public void SetValue(TKey key, TValue value)
	{
		if (this.GetTable().Keys.Contains(key))
		{
			this.table[key] = value;
		}
		else
		{
			this.table.Add(key, value);
		}
	}

	// Token: 0x060094F6 RID: 38134 RVA: 0x003D6CE4 File Offset: 0x003D50E4
	public void Reset()
	{
		this.table = new Dictionary<TKey, TValue>();
		this.list = new List<TPair>();
	}

	// Token: 0x060094F7 RID: 38135 RVA: 0x003D6CFC File Offset: 0x003D50FC
	public void Apply()
	{
		this.list = YS_Dictionary<TKey, TValue, TPair>.ConvertDictionaryToList(this.table);
	}

	// Token: 0x17001F8F RID: 8079
	// (get) Token: 0x060094F8 RID: 38136 RVA: 0x003D6D0F File Offset: 0x003D510F
	public int Length
	{
		get
		{
			if (this.list == null)
			{
				return 0;
			}
			return this.list.Count;
		}
	}

	// Token: 0x060094F9 RID: 38137 RVA: 0x003D6D2C File Offset: 0x003D512C
	private static Dictionary<TKey, TValue> ConvertListToDictionary(List<TPair> list)
	{
		Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
		foreach (TPair tpair in list)
		{
			YS_KeyAndValue<TKey, TValue> ys_KeyAndValue = tpair;
			dictionary.Add(ys_KeyAndValue.Key, ys_KeyAndValue.Value);
		}
		return dictionary;
	}

	// Token: 0x060094FA RID: 38138 RVA: 0x003D6D9C File Offset: 0x003D519C
	private static List<TPair> ConvertDictionaryToList(Dictionary<TKey, TValue> table)
	{
		List<TPair> list = new List<TPair>();
		if (table != null)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in table)
			{
				TPair tpair = Activator.CreateInstance<TPair>();
				tpair.Key = keyValuePair.Key;
				tpair.Value = keyValuePair.Value;
				list.Add(tpair);
			}
		}
		return list;
	}

	// Token: 0x040077B6 RID: 30646
	[SerializeField]
	protected List<TPair> list;

	// Token: 0x040077B7 RID: 30647
	protected Dictionary<TKey, TValue> table;
}
