using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Illusion;
using Illusion.Extensions;

namespace AIChara
{
	// Token: 0x0200080B RID: 2059
	[Serializable]
	public class ListInfoBase
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x00133B78 File Offset: 0x00131F78
		public ListInfoBase()
		{
			this.dictInfo = this._dictInfo;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x00133B97 File Offset: 0x00131F97
		public int ListIndex
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfoInt(ChaListDefine.KeyType.ListIndex);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x00133BA0 File Offset: 0x00131FA0
		public int Category
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfoInt(ChaListDefine.KeyType.Category);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x00133BA9 File Offset: 0x00131FA9
		public int Distribution
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfoInt(ChaListDefine.KeyType.DistributionNo);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00133BB2 File Offset: 0x00131FB2
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfoInt(ChaListDefine.KeyType.ID);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x00133BBB File Offset: 0x00131FBB
		public int Kind
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfoInt(ChaListDefine.KeyType.Kind);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x00133BC4 File Offset: 0x00131FC4
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.GetInfo(ChaListDefine.KeyType.Name);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x00133BCD File Offset: 0x00131FCD
		public IReadOnlyDictionary<int, string> dictInfo { get; }

		// Token: 0x0600343B RID: 13371 RVA: 0x00133BD8 File Offset: 0x00131FD8
		public bool Set(int entryCnt, int _cateNo, int _distNo, List<string> lstKey, List<string> lstData)
		{
			string[] names = Illusion.Utils.Enum<ChaListDefine.KeyType>.Names;
			int key = names.Check("ListIndex");
			this._dictInfo[key] = entryCnt.ToString();
			int key2 = names.Check("Category");
			this._dictInfo[key2] = _cateNo.ToString();
			int key3 = names.Check("DistributionNo");
			this._dictInfo[key3] = _distNo.ToString();
			for (int i = 0; i < lstKey.Count; i++)
			{
				int key4 = names.Check(lstKey[i]);
				this._dictInfo[key4] = lstData[i];
			}
			return true;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x00133CA0 File Offset: 0x001320A0
		public void ChangeListIndex(int index)
		{
			string[] names = Illusion.Utils.Enum<ChaListDefine.KeyType>.Names;
			int key = 0;
			this._dictInfo[key] = index.ToString();
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x00133CD0 File Offset: 0x001320D0
		public int GetInfoInt(ChaListDefine.KeyType keyType)
		{
			string info = this.GetInfo(keyType);
			int result;
			if (!int.TryParse(info, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x00133CF8 File Offset: 0x001320F8
		public float GetInfoFloat(ChaListDefine.KeyType keyType)
		{
			string info = this.GetInfo(keyType);
			float result;
			if (!float.TryParse(info, out result))
			{
				return -1f;
			}
			return result;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00133D24 File Offset: 0x00132124
		public string GetInfo(ChaListDefine.KeyType keyType)
		{
			string result;
			if (!this._dictInfo.TryGetValue((int)keyType, out result))
			{
				return "0";
			}
			return result;
		}

		// Token: 0x04003426 RID: 13350
		private Dictionary<int, string> _dictInfo = new Dictionary<int, string>();
	}
}
