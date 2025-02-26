using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class UITimingDict
{
	// Token: 0x0600168F RID: 5775 RVA: 0x0008A6E4 File Offset: 0x00088AE4
	public void StartTiming()
	{
		this.m_stopwatch.Reset();
		this.m_stopwatch.Start();
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x0008A6FC File Offset: 0x00088AFC
	public double StopTiming(UnityEngine.Object w)
	{
		int instanceID = w.GetInstanceID();
		this.m_stopwatch.Stop();
		double num = (double)this.m_stopwatch.ElapsedTicks / 10000.0;
		if (this.m_elapsedTicks.ContainsKey(instanceID))
		{
			Dictionary<int, double> elapsedTicks;
			int key;
			(elapsedTicks = this.m_elapsedTicks)[key = instanceID] = elapsedTicks[key] + num;
		}
		else
		{
			this.m_elapsedTicks.Add(instanceID, num);
		}
		if (!UIDebugCache.s_nameLut.ContainsKey(instanceID))
		{
			UIDebugCache.s_nameLut.Add(instanceID, w.name);
		}
		return num;
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x0008A790 File Offset: 0x00088B90
	public string PrintDict(int count = -1)
	{
		List<KeyValuePair<int, double>> list = this.m_elapsedTicks.ToList<KeyValuePair<int, double>>();
		list.Sort((KeyValuePair<int, double> pair1, KeyValuePair<int, double> pair2) => Math.Sign(pair2.Value - pair1.Value));
		if (count > 0 && count < list.Count)
		{
			list.RemoveRange(count - 1, list.Count - count);
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<int, double> keyValuePair in list)
		{
			stringBuilder.AppendFormat("{0}{1,-40} \t{2:0.00} \t{3:0.00}\n", new object[]
			{
				"    ",
				UIDebugCache.GetName(keyValuePair.Key),
				keyValuePair.Value,
				keyValuePair.Value / (double)(1f / Time.deltaTime)
			});
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04001954 RID: 6484
	private const string Indent = "    ";

	// Token: 0x04001955 RID: 6485
	private Stopwatch m_stopwatch = Stopwatch.StartNew();

	// Token: 0x04001956 RID: 6486
	private Dictionary<int, double> m_elapsedTicks = new Dictionary<int, double>(200);
}
