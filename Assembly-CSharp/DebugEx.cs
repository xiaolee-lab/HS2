using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02001174 RID: 4468
public static class DebugEx
{
	// Token: 0x060093A3 RID: 37795 RVA: 0x003D110E File Offset: 0x003CF50E
	public static void ArrayPrint<T>(T[] print, string head, UnityEngine.Object context = null)
	{
	}

	// Token: 0x060093A4 RID: 37796 RVA: 0x003D1110 File Offset: 0x003CF510
	public static string ConvertString<T>(T[] print, string head)
	{
		StringBuilder stringBuilder = new StringBuilder(head);
		foreach (var <>__AnonType in (from p in print
		select p.ToString()).Select((string s, int i) => new
		{
			s,
			i
		}))
		{
			stringBuilder.AppendFormat("{0}{1}", <>__AnonType.s, (<>__AnonType.i >= print.Length - 1) ? string.Empty : ",");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060093A5 RID: 37797 RVA: 0x003D11C0 File Offset: 0x003CF5C0
	public static void PathCheck()
	{
	}
}
