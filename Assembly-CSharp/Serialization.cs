using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
	// Token: 0x06001632 RID: 5682 RVA: 0x000888D7 File Offset: 0x00086CD7
	public Serialization(Dictionary<TKey, TValue> target)
	{
		this.target = target;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000888E6 File Offset: 0x00086CE6
	public Dictionary<TKey, TValue> ToDictionary()
	{
		return this.target;
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000888EE File Offset: 0x00086CEE
	public void OnBeforeSerialize()
	{
		this.keys = new List<TKey>(this.target.Keys);
		this.values = new List<TValue>(this.target.Values);
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x0008891C File Offset: 0x00086D1C
	public void OnAfterDeserialize()
	{
		int num = Math.Min(this.keys.Count, this.values.Count);
		this.target = new Dictionary<TKey, TValue>(num);
		for (int i = 0; i < num; i++)
		{
			this.target.Add(this.keys[i], this.values[i]);
		}
	}

	// Token: 0x040018F7 RID: 6391
	[SerializeField]
	private List<TKey> keys;

	// Token: 0x040018F8 RID: 6392
	[SerializeField]
	private List<TValue> values;

	// Token: 0x040018F9 RID: 6393
	private Dictionary<TKey, TValue> target;
}
