using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class UsvConsoleCmds
{
	// Token: 0x060015A3 RID: 5539 RVA: 0x00085C04 File Offset: 0x00084004
	[ConsoleHandler("showmesh")]
	public bool ShowMesh(string[] args)
	{
		return this.SetMeshVisible(args[1], true);
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x00085C10 File Offset: 0x00084010
	[ConsoleHandler("hidemesh")]
	public bool HideMesh(string[] args)
	{
		return this.SetMeshVisible(args[1], false);
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x00085C1C File Offset: 0x0008401C
	private bool SetMeshVisible(string strInstID, bool visible)
	{
		int num = 0;
		if (!int.TryParse(strInstID, out num))
		{
			return false;
		}
		MeshRenderer[] array = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
		foreach (MeshRenderer meshRenderer in array)
		{
			if (meshRenderer.gameObject.GetInstanceID() == num)
			{
				meshRenderer.enabled = visible;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00085C8C File Offset: 0x0008408C
	[ConsoleHandler("testlogs")]
	public bool PrintTestLogs(string[] args)
	{
		try
		{
			throw new ApplicationException("A user-thrown exception.");
		}
		catch (ApplicationException ex)
		{
		}
		return true;
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x00085CBC File Offset: 0x000840BC
	[ConsoleHandler("toggle")]
	public bool ToggleSwitch(string[] args)
	{
		try
		{
			GameInterface.Instance.ToggleSwitch(args[1], int.Parse(args[2]) != 0);
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00085D04 File Offset: 0x00084104
	[ConsoleHandler("slide")]
	public bool SlideChanged(string[] args)
	{
		try
		{
			GameInterface.Instance.ChangePercentage(args[1], double.Parse(args[2]));
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x00085D48 File Offset: 0x00084148
	[ConsoleHandler("flyto")]
	public bool FlyTo(string[] args)
	{
		try
		{
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x1400005B RID: 91
	// (add) Token: 0x060015AA RID: 5546 RVA: 0x00085D74 File Offset: 0x00084174
	// (remove) Token: 0x060015AB RID: 5547 RVA: 0x00085DAC File Offset: 0x000841AC
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation QueryEffectList;

	// Token: 0x1400005C RID: 92
	// (add) Token: 0x060015AC RID: 5548 RVA: 0x00085DE4 File Offset: 0x000841E4
	// (remove) Token: 0x060015AD RID: 5549 RVA: 0x00085E1C File Offset: 0x0008421C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation RunEffectStressTest;

	// Token: 0x1400005D RID: 93
	// (add) Token: 0x060015AE RID: 5550 RVA: 0x00085E54 File Offset: 0x00084254
	// (remove) Token: 0x060015AF RID: 5551 RVA: 0x00085E8C File Offset: 0x0008428C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation StartAnalyzePixel;

	// Token: 0x060015B0 RID: 5552 RVA: 0x00085EC4 File Offset: 0x000842C4
	[ConsoleHandler("start_analyze_pixels")]
	public bool StartAnalysePixelsTriggered(string[] args)
	{
		try
		{
			bool b = false;
			if (args.Length == 2)
			{
				string a = args[1];
				if (a == "refresh")
				{
					b = true;
				}
			}
			SysPost.InvokeMulticast(this, this.StartAnalyzePixel, new UsvConsoleCmds.AnalysePixelsArgs(b));
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x00085F24 File Offset: 0x00084324
	[ConsoleHandler("get_effect_list")]
	public bool QueryEffectListTriggered(string[] args)
	{
		try
		{
			if (args.Length != 1)
			{
				Log.Error("Command 'get_effect_list' parameter count mismatched. ({0} expected, {1} got)", new object[]
				{
					1,
					args.Length
				});
				return false;
			}
			SysPost.InvokeMulticast(this, this.QueryEffectList);
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00085F94 File Offset: 0x00084394
	[ConsoleHandler("run_effect_stress")]
	public bool EffectStressTestTriggered(string[] args)
	{
		try
		{
			if (args.Length == 3)
			{
				string effectName = args[1];
				int effectCount = int.Parse(args[2]);
				SysPost.InvokeMulticast(this, this.RunEffectStressTest, new UsvConsoleCmds.UsEffectStressTestEventArgs(effectName, effectCount));
			}
			else
			{
				int effectCount2 = int.Parse(args[args.Length - 1]);
				List<string> list = new List<string>(args);
				list.RemoveAt(0);
				list.RemoveAt(list.Count - 1);
				string effectName2 = string.Join(" ", list.ToArray());
				SysPost.InvokeMulticast(this, this.RunEffectStressTest, new UsvConsoleCmds.UsEffectStressTestEventArgs(effectName2, effectCount2));
			}
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			throw;
		}
		return true;
	}

	// Token: 0x040018A8 RID: 6312
	public static UsvConsoleCmds Instance;

	// Token: 0x02000496 RID: 1174
	public class AnalysePixelsArgs : EventArgs
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x00086040 File Offset: 0x00084440
		public AnalysePixelsArgs(bool b)
		{
			this.bRefresh = b;
		}

		// Token: 0x040018AC RID: 6316
		public bool bRefresh;
	}

	// Token: 0x02000497 RID: 1175
	public class UsEffectStressTestEventArgs : EventArgs
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x0008604F File Offset: 0x0008444F
		public UsEffectStressTestEventArgs(string effectName, int effectCount)
		{
			this._effectName = effectName;
			this._effectCount = effectCount;
		}

		// Token: 0x040018AD RID: 6317
		public string _effectName;

		// Token: 0x040018AE RID: 6318
		public int _effectCount;
	}
}
