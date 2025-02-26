using System;
using System.Collections.Generic;

// Token: 0x02000830 RID: 2096
public class ActionTable
{
	// Token: 0x1700099C RID: 2460
	public string this[int i]
	{
		get
		{
			return (!ActionTable.table_.ContainsKey((ActionID)i)) ? string.Empty : ActionTable.table_[(ActionID)i];
		}
	}

	// Token: 0x1700099D RID: 2461
	public string this[ActionID i]
	{
		get
		{
			return (!ActionTable.table_.ContainsKey(i)) ? string.Empty : ActionTable.table_[i];
		}
	}

	// Token: 0x1700099E RID: 2462
	public int this[string s]
	{
		get
		{
			foreach (KeyValuePair<ActionID, string> keyValuePair in ActionTable.table_)
			{
				if (keyValuePair.Value == s)
				{
					return (int)keyValuePair.Key;
				}
			}
			return 0;
		}
	}

	// Token: 0x1700099F RID: 2463
	// (get) Token: 0x0600354C RID: 13644 RVA: 0x0013A8A8 File Offset: 0x00138CA8
	public static IReadOnlyDictionary<ActionID, string> Table
	{
		get
		{
			return ActionTable.table_;
		}
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x0013A8AF File Offset: 0x00138CAF
	public static string Name(ActionID id)
	{
		return ActionTable.table_[id];
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x0013A8BC File Offset: 0x00138CBC
	public static int ID(string name)
	{
		foreach (KeyValuePair<ActionID, string> keyValuePair in ActionTable.table_)
		{
			if (keyValuePair.Value == name)
			{
				return (int)keyValuePair.Key;
			}
		}
		return 0;
	}

	// Token: 0x040035ED RID: 13805
	private static readonly IReadOnlyDictionary<ActionID, string> table_ = new Dictionary<ActionID, string>(new ActionIDComparer())
	{
		{
			ActionID.MoveHorizontal,
			"Move Horizontal"
		},
		{
			ActionID.MoveVertical,
			"Move Vertical"
		},
		{
			ActionID.CameraHorizontal,
			"Camera Horizontal"
		},
		{
			ActionID.CameraVertical,
			"Camera Vertical"
		},
		{
			ActionID.Action,
			"Action"
		},
		{
			ActionID.Jump,
			"Jump"
		},
		{
			ActionID.Attack,
			"Attack"
		},
		{
			ActionID.Guard,
			"Guard"
		},
		{
			ActionID.Special,
			"Special"
		},
		{
			ActionID.Submit,
			"Submit"
		},
		{
			ActionID.Cancel,
			"Cancel"
		},
		{
			ActionID.SelectHorizontal,
			"Select Horizontal"
		},
		{
			ActionID.SelectVertical,
			"Select Vertical"
		},
		{
			ActionID.MouseLeft,
			"Mouse Left"
		},
		{
			ActionID.MouseRight,
			"Mouse Right"
		},
		{
			ActionID.MouseCenter,
			"Mouse Center"
		},
		{
			ActionID.MouseWheel,
			"Mouse Wheel"
		},
		{
			ActionID.End,
			"End"
		},
		{
			ActionID.Angle1,
			"Angle1"
		},
		{
			ActionID.Angle2,
			"Angle2"
		},
		{
			ActionID.Angle3,
			"Angle3"
		}
	};
}
