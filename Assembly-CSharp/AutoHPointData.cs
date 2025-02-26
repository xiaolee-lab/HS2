using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEx;

// Token: 0x02000AC1 RID: 2753
public class AutoHPointData : SerializedScriptableObject
{
	// Token: 0x060050AA RID: 20650 RVA: 0x001F8180 File Offset: 0x001F6580
	public void Allocation(Dictionary<string, List<UnityEx.ValueTuple<int, Vector3>>> pointLists)
	{
		this.Points.Clear();
		foreach (KeyValuePair<string, List<UnityEx.ValueTuple<int, Vector3>>> keyValuePair in pointLists)
		{
			this.Points.Add(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x060050AB RID: 20651 RVA: 0x001F81F4 File Offset: 0x001F65F4
	public void Allocation(Dictionary<string, List<int>> pointAreaIDLists, Dictionary<string, List<Vector3>> pointPosLists)
	{
		this.Points.Clear();
		foreach (KeyValuePair<string, List<int>> keyValuePair in pointAreaIDLists)
		{
			this.Points.Add(keyValuePair.Key, new List<UnityEx.ValueTuple<int, Vector3>>());
		}
		foreach (KeyValuePair<string, List<UnityEx.ValueTuple<int, Vector3>>> keyValuePair2 in this.Points)
		{
			for (int i = 0; i < pointAreaIDLists[keyValuePair2.Key].Count; i++)
			{
				keyValuePair2.Value.Add(new UnityEx.ValueTuple<int, Vector3>(pointAreaIDLists[keyValuePair2.Key][i], pointPosLists[keyValuePair2.Key][i]));
			}
		}
	}

	// Token: 0x060050AC RID: 20652 RVA: 0x001F830C File Offset: 0x001F670C
	public void Release()
	{
		this.Points = null;
		GC.Collect();
	}

	// Token: 0x04004A40 RID: 19008
	public Dictionary<string, List<UnityEx.ValueTuple<int, Vector3>>> Points = new Dictionary<string, List<UnityEx.ValueTuple<int, Vector3>>>();
}
