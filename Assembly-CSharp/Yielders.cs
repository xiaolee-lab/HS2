using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

// Token: 0x02000471 RID: 1137
public static class Yielders
{
	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00082FE1 File Offset: 0x000813E1
	public static WaitForEndOfFrame EndOfFrame
	{
		get
		{
			Yielders._internalCounter++;
			return (!Yielders.Enabled) ? new WaitForEndOfFrame() : Yielders._endOfFrame;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00083008 File Offset: 0x00081408
	public static WaitForFixedUpdate FixedUpdate
	{
		get
		{
			Yielders._internalCounter++;
			return (!Yielders.Enabled) ? new WaitForFixedUpdate() : Yielders._fixedUpdate;
		}
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x00083030 File Offset: 0x00081430
	public static WaitForSeconds GetWaitForSeconds(float seconds)
	{
		Yielders._internalCounter++;
		if (!Yielders.Enabled)
		{
			return new WaitForSeconds(seconds);
		}
		WaitForSeconds result;
		if (!Yielders._waitForSecondsYielders.TryGetValue(seconds, out result))
		{
			Yielders._waitForSecondsYielders.Add(seconds, result = new WaitForSeconds(seconds));
		}
		return result;
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x00083080 File Offset: 0x00081480
	public static void ClearWaitForSeconds()
	{
		Yielders._waitForSecondsYielders.Clear();
	}

	// Token: 0x04001812 RID: 6162
	public static bool Enabled = true;

	// Token: 0x04001813 RID: 6163
	public static int _internalCounter = 0;

	// Token: 0x04001814 RID: 6164
	private static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();

	// Token: 0x04001815 RID: 6165
	private static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();

	// Token: 0x04001816 RID: 6166
	private static Dictionary<float, WaitForSeconds> _waitForSecondsYielders = new Dictionary<float, WaitForSeconds>(100, new FloatComparer());
}
