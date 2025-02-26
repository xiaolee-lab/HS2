using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F79 RID: 3961
	public class MerchantProfile : SerializedScriptableObject
	{
		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x060083CA RID: 33738 RVA: 0x00371750 File Offset: 0x0036FB50
		public Dictionary<Merchant.ActionType, int> ResultAddFriendlyRelationShipTable
		{
			[CompilerGenerated]
			get
			{
				return this._resultAddFriendlyRelationShipTable;
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x060083CB RID: 33739 RVA: 0x00371758 File Offset: 0x0036FB58
		public int OneCycle
		{
			[CompilerGenerated]
			get
			{
				return this._oneCycle;
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x060083CC RID: 33740 RVA: 0x00371760 File Offset: 0x0036FB60
		public float ToSearchSelectedRange
		{
			[CompilerGenerated]
			get
			{
				return this._toSearchSelectedRange;
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x060083CD RID: 33741 RVA: 0x00371768 File Offset: 0x0036FB68
		public float DifferentAreaSelectedRange
		{
			[CompilerGenerated]
			get
			{
				return this._differentAreaSelectedRange;
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x060083CE RID: 33742 RVA: 0x00371770 File Offset: 0x0036FB70
		public float ActivateNavMeshElementDelayTime
		{
			[CompilerGenerated]
			get
			{
				return this._activateNavMeshElementDelayTime;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x060083CF RID: 33743 RVA: 0x00371778 File Offset: 0x0036FB78
		public int MapShipItemID
		{
			[CompilerGenerated]
			get
			{
				return this._mapShipItemID;
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x060083D0 RID: 33744 RVA: 0x00371780 File Offset: 0x0036FB80
		public IReadOnlyDictionary<int, List<UnityEx.ValueTuple<int, int>>> AreaOpenState
		{
			[CompilerGenerated]
			get
			{
				return this._areaOpenState;
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x060083D1 RID: 33745 RVA: 0x00371788 File Offset: 0x0036FB88
		public int[] SpendMoneyBorder
		{
			[CompilerGenerated]
			get
			{
				return this._spendMoneyBorder;
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x060083D2 RID: 33746 RVA: 0x00371790 File Offset: 0x0036FB90
		public int SpendMoneyMax
		{
			[CompilerGenerated]
			get
			{
				return this._spendMoneyMax;
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x060083D3 RID: 33747 RVA: 0x00371798 File Offset: 0x0036FB98
		public int LastADVSafeguardID
		{
			[CompilerGenerated]
			get
			{
				return this._lastADVSafeguardID;
			}
		}

		// Token: 0x04006A50 RID: 27216
		[SerializeField]
		private Dictionary<Merchant.ActionType, int> _resultAddFriendlyRelationShipTable = new Dictionary<Merchant.ActionType, int>();

		// Token: 0x04006A51 RID: 27217
		[SerializeField]
		[LabelText("行動一周の日数")]
		[Min(4f)]
		private int _oneCycle = 7;

		// Token: 0x04006A52 RID: 27218
		[SerializeField]
		[LabelText("待機後：探索移行率")]
		[Min(0f)]
		[MaxValue(100.0)]
		private float _toSearchSelectedRange = 35f;

		// Token: 0x04006A53 RID: 27219
		[SerializeField]
		[LabelText("違うエリア移行率")]
		[Min(0f)]
		[MaxValue(100.0)]
		private float _differentAreaSelectedRange = 70f;

		// Token: 0x04006A54 RID: 27220
		[SerializeField]
		[LabelText("NavMeshコンポーネントの始動遅延時間")]
		private float _activateNavMeshElementDelayTime = 0.08f;

		// Token: 0x04006A55 RID: 27221
		[SerializeField]
		[LabelText("商人の船のマップアイテムID")]
		private int _mapShipItemID;

		// Token: 0x04006A56 RID: 27222
		[SerializeField]
		private Dictionary<int, List<UnityEx.ValueTuple<int, int>>> _areaOpenState = new Dictionary<int, List<UnityEx.ValueTuple<int, int>>>();

		// Token: 0x04006A57 RID: 27223
		[SerializeField]
		private int[] _spendMoneyBorder = new int[0];

		// Token: 0x04006A58 RID: 27224
		[SerializeField]
		private int _spendMoneyMax = 99999999;

		// Token: 0x04006A59 RID: 27225
		[SerializeField]
		private int _lastADVSafeguardID;
	}
}
