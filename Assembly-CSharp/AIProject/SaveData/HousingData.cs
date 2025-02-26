using System;
using System.Collections.Generic;
using System.Linq;
using Housing;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000982 RID: 2434
	[MessagePackObject(false)]
	public class HousingData : IDiffComparer
	{
		// Token: 0x06004521 RID: 17697 RVA: 0x001AAC24 File Offset: 0x001A9024
		public HousingData()
		{
			this.UpdateDiff();
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x001AAC48 File Offset: 0x001A9048
		public HousingData(HousingData _src)
		{
			this.Unlock = _src.Unlock.ToDictionary((KeyValuePair<int, Dictionary<int, bool>> x) => x.Key, delegate(KeyValuePair<int, Dictionary<int, bool>> x)
			{
				Dictionary<int, bool> value = x.Value;
				Dictionary<int, bool> result;
				if (value == null)
				{
					result = null;
				}
				else
				{
					result = value.ToDictionary((KeyValuePair<int, bool> y) => y.Key, (KeyValuePair<int, bool> y) => y.Value);
				}
				return result;
			});
			this.CraftInfos = _src.CraftInfos.ToDictionary((KeyValuePair<int, CraftInfo> x) => x.Key, (KeyValuePair<int, CraftInfo> x) => (x.Value == null) ? null : new CraftInfo(x.Value, true));
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x001AAD07 File Offset: 0x001A9107
		// (set) Token: 0x06004524 RID: 17700 RVA: 0x001AAD0F File Offset: 0x001A910F
		[Key(0)]
		public Dictionary<int, Dictionary<int, bool>> Unlock { get; set; } = new Dictionary<int, Dictionary<int, bool>>();

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x001AAD18 File Offset: 0x001A9118
		// (set) Token: 0x06004526 RID: 17702 RVA: 0x001AAD20 File Offset: 0x001A9120
		[Key(1)]
		public Dictionary<int, CraftInfo> CraftInfos { get; set; } = new Dictionary<int, CraftInfo>();

		// Token: 0x06004527 RID: 17703 RVA: 0x001AAD2C File Offset: 0x001A912C
		public void UpdateDiff()
		{
			if (Singleton<Housing>.IsInstance())
			{
				HashSet<int> hashSet = new HashSet<int>(from v in Singleton<Housing>.Instance.dicLoadInfo
				select v.Value.category);
				using (HashSet<int>.Enumerator enumerator = hashSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int c = enumerator.Current;
						Dictionary<int, bool> dictionary = null;
						if (!this.Unlock.TryGetValue(c, out dictionary))
						{
							dictionary = new Dictionary<int, bool>();
							this.Unlock.Add(c, dictionary);
						}
						foreach (KeyValuePair<int, Housing.LoadInfo> keyValuePair in from v in Singleton<Housing>.Instance.dicLoadInfo
						where v.Value.category == c
						select v)
						{
							if (!dictionary.ContainsKey(keyValuePair.Key))
							{
								dictionary.Add(keyValuePair.Key, keyValuePair.Value.requiredMaterials.IsNullOrEmpty<Housing.RequiredMaterial>());
							}
						}
					}
				}
			}
			CraftInfo craftInfo = null;
			if (!Singleton<Housing>.IsInstance())
			{
				if (!this.CraftInfos.TryGetValue(0, out craftInfo))
				{
					this.CraftInfos.Add(0, new CraftInfo(new Vector3(100f, 80f, 100f), 0));
				}
				if (!this.CraftInfos.TryGetValue(1, out craftInfo))
				{
					this.CraftInfos.Add(1, new CraftInfo(new Vector3(100f, 80f, 100f), 1));
				}
				if (!this.CraftInfos.TryGetValue(2, out craftInfo))
				{
					this.CraftInfos.Add(2, new CraftInfo(new Vector3(200f, 100f, 200f), 2));
				}
				if (!this.CraftInfos.TryGetValue(3, out craftInfo))
				{
					this.CraftInfos.Add(3, new CraftInfo(new Vector3(500f, 150f, 500f), 3));
				}
			}
			else
			{
				foreach (KeyValuePair<int, Housing.AreaInfo> keyValuePair2 in Singleton<Housing>.Instance.dicAreaInfo)
				{
					Housing.AreaSizeInfo areaSizeInfo = null;
					if (!this.CraftInfos.ContainsKey(keyValuePair2.Key))
					{
						Vector3Int v2 = (!Singleton<Housing>.Instance.dicAreaSizeInfo.TryGetValue(keyValuePair2.Value.size, out areaSizeInfo)) ? new Vector3Int(100, 80, 100) : areaSizeInfo.limitSize;
						CraftInfo value = new CraftInfo(v2, keyValuePair2.Value.no);
						this.CraftInfos.Add(keyValuePair2.Key, value);
					}
				}
			}
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x001AB050 File Offset: 0x001A9450
		public void CopyInstances(HousingData _src)
		{
			if (!Singleton<Housing>.IsInstance())
			{
				return;
			}
			foreach (KeyValuePair<int, CraftInfo> keyValuePair in _src.CraftInfos)
			{
				CraftInfo craftInfo = this.CraftInfos[keyValuePair.Key];
				for (int i = 0; i < keyValuePair.Value.ObjectInfos.Count; i++)
				{
					IObjectInfo objectInfo = keyValuePair.Value.ObjectInfos[i];
					IObjectInfo objectInfo2 = craftInfo.ObjectInfos[i];
					ObjectCtrl srcCtrl;
					if (keyValuePair.Value.ObjectCtrls.TryGetValue(objectInfo, out srcCtrl))
					{
						craftInfo.ObjectCtrls[objectInfo2] = this.CopyObjectCtrl(objectInfo2, objectInfo, srcCtrl, null, craftInfo, keyValuePair.Value);
					}
				}
				craftInfo.ObjRoot = keyValuePair.Value.ObjRoot;
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x001AB158 File Offset: 0x001A9558
		private ObjectCtrl CopyObjectCtrl(IObjectInfo _dstObjectInfo, IObjectInfo _srcObjectInfo, ObjectCtrl _srcCtrl, ObjectCtrl _parent, CraftInfo _craftInfo, CraftInfo _srcCraftInfo)
		{
			if (_srcCtrl == null)
			{
				return null;
			}
			ObjectCtrl result = null;
			int kind = _dstObjectInfo.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					result = this.CopyFolder(_dstObjectInfo as OIFolder, _srcObjectInfo as OIFolder, _srcCtrl as OCFolder, _parent, _craftInfo, _srcCraftInfo);
				}
			}
			else
			{
				result = this.CopyObject(_dstObjectInfo as OIItem, _srcObjectInfo as OIItem, _srcCtrl.GameObject, _parent, _craftInfo, _srcCraftInfo);
			}
			return result;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x001AB1D4 File Offset: 0x001A95D4
		private ObjectCtrl CopyFolder(OIFolder _oiFolder, OIFolder _srcOIFolder, OCFolder _srcOCFolder, ObjectCtrl _parent, CraftInfo craftInfo, CraftInfo _srcCraftInfo)
		{
			OCFolder ocfolder = new OCFolder(_oiFolder, _srcOCFolder.GameObject, craftInfo);
			if (_parent != null)
			{
				ocfolder.OnAttach(_parent, -1);
			}
			List<IObjectInfo> list = new List<IObjectInfo>();
			for (int i = 0; i < _oiFolder.Child.Count; i++)
			{
				IObjectInfo objectInfo = _oiFolder.Child[i];
				IObjectInfo objectInfo2 = _srcOIFolder.Child[i];
				ObjectCtrl srcCtrl;
				if (_srcOCFolder.Child.TryGetValue(objectInfo2, out srcCtrl))
				{
					ocfolder.Child[objectInfo] = this.CopyObjectCtrl(objectInfo, objectInfo2, srcCtrl, ocfolder, craftInfo, _srcCraftInfo);
				}
			}
			return ocfolder;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x001AB274 File Offset: 0x001A9674
		private ObjectCtrl CopyObject(OIItem _oiItem, OIItem _srcOIFolder, GameObject _gameObject, ObjectCtrl _parent, CraftInfo craftInfo, CraftInfo _srcCraftInfo)
		{
			Housing.LoadInfo loadInfo;
			if (!Singleton<Housing>.Instance.dicLoadInfo.TryGetValue(_oiItem.ID, out loadInfo))
			{
				return null;
			}
			if (_gameObject == null)
			{
				return null;
			}
			return new OCItem(_oiItem, _gameObject, craftInfo, loadInfo);
		}
	}
}
