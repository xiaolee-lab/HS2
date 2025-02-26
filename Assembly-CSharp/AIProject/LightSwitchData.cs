using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C0F RID: 3087
	public class LightSwitchData : MonoBehaviour
	{
		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06005F70 RID: 24432 RVA: 0x002849E7 File Offset: 0x00282DE7
		public static IReadOnlyDictionary<int, LightSwitchData> Table
		{
			[CompilerGenerated]
			get
			{
				return LightSwitchData._table;
			}
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x002849F0 File Offset: 0x00282DF0
		public static LightSwitchData Get(int linkID)
		{
			LightSwitchData result;
			LightSwitchData._table.TryGetValue(linkID, out result);
			return result;
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x00284A0C File Offset: 0x00282E0C
		public GameObject[] OnModeObjects
		{
			[CompilerGenerated]
			get
			{
				return this._onModeObjects;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06005F73 RID: 24435 RVA: 0x00284A14 File Offset: 0x00282E14
		public GameObject[] OffModeObjects
		{
			[CompilerGenerated]
			get
			{
				return this._offModeObjects;
			}
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x00284A1C File Offset: 0x00282E1C
		private void Awake()
		{
			LightSwitchData._table[this._linkID] = this;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00284A2F File Offset: 0x00282E2F
		private void OnDestroy()
		{
			LightSwitchData._table.Remove(this._linkID);
		}

		// Token: 0x040054BC RID: 21692
		private static Dictionary<int, LightSwitchData> _table = new Dictionary<int, LightSwitchData>();

		// Token: 0x040054BD RID: 21693
		[SerializeField]
		private int _linkID;

		// Token: 0x040054BE RID: 21694
		[SerializeField]
		private GameObject[] _onModeObjects;

		// Token: 0x040054BF RID: 21695
		[SerializeField]
		private GameObject[] _offModeObjects;
	}
}
