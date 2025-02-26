using System;
using System.Collections.Generic;
using System.IO;

namespace Studio
{
	// Token: 0x0200121F RID: 4639
	public static class ObjectInfoAssist
	{
		// Token: 0x060098DE RID: 39134 RVA: 0x003EFB70 File Offset: 0x003EDF70
		public static void LoadChild(BinaryReader _reader, Version _version, List<ObjectInfo> _list, bool _import)
		{
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				switch (_reader.ReadInt32())
				{
				case 0:
				{
					OICharInfo oicharInfo = new OICharInfo(null, _import ? Studio.GetNewIndex() : -1);
					oicharInfo.Load(_reader, _version, _import, true);
					_list.Add(oicharInfo);
					break;
				}
				case 1:
				{
					OIItemInfo oiitemInfo = new OIItemInfo(-1, -1, -1, _import ? Studio.GetNewIndex() : -1);
					oiitemInfo.Load(_reader, _version, _import, true);
					_list.Add(oiitemInfo);
					break;
				}
				case 2:
				{
					OILightInfo oilightInfo = new OILightInfo(-1, _import ? Studio.GetNewIndex() : -1);
					oilightInfo.Load(_reader, _version, _import, true);
					_list.Add(oilightInfo);
					break;
				}
				case 3:
				{
					OIFolderInfo oifolderInfo = new OIFolderInfo(_import ? Studio.GetNewIndex() : -1);
					oifolderInfo.Load(_reader, _version, _import, true);
					_list.Add(oifolderInfo);
					break;
				}
				case 4:
				{
					OIRouteInfo oirouteInfo = new OIRouteInfo(_import ? Studio.GetNewIndex() : -1);
					oirouteInfo.Load(_reader, _version, _import, true);
					_list.Add(oirouteInfo);
					break;
				}
				case 5:
				{
					OICameraInfo oicameraInfo = new OICameraInfo(_import ? Studio.GetNewIndex() : -1);
					oicameraInfo.Load(_reader, _version, _import, true);
					_list.Add(oicameraInfo);
					break;
				}
				}
			}
		}

		// Token: 0x060098DF RID: 39135 RVA: 0x003EFCE8 File Offset: 0x003EE0E8
		public static List<ObjectInfo> Find(int _kind)
		{
			List<ObjectInfo> result = new List<ObjectInfo>();
			foreach (KeyValuePair<int, ObjectInfo> keyValuePair in Singleton<Studio>.Instance.sceneInfo.dicObject)
			{
				ObjectInfoAssist.FindLoop(ref result, keyValuePair.Value, _kind);
			}
			return result;
		}

		// Token: 0x060098E0 RID: 39136 RVA: 0x003EFD5C File Offset: 0x003EE15C
		private static void FindLoop(ref List<ObjectInfo> _list, ObjectInfo _src, int _kind)
		{
			if (_src == null)
			{
				return;
			}
			if (_src.kind == _kind)
			{
				_list.Add(_src);
			}
			switch (_src.kind)
			{
			case 0:
			{
				OICharInfo oicharInfo = _src as OICharInfo;
				foreach (KeyValuePair<int, List<ObjectInfo>> keyValuePair in oicharInfo.child)
				{
					foreach (ObjectInfo src in keyValuePair.Value)
					{
						ObjectInfoAssist.FindLoop(ref _list, src, _kind);
					}
				}
				break;
			}
			case 1:
				foreach (ObjectInfo src2 in (_src as OIItemInfo).child)
				{
					ObjectInfoAssist.FindLoop(ref _list, src2, _kind);
				}
				break;
			case 3:
				foreach (ObjectInfo src3 in (_src as OIFolderInfo).child)
				{
					ObjectInfoAssist.FindLoop(ref _list, src3, _kind);
				}
				break;
			case 4:
				foreach (ObjectInfo src4 in (_src as OIRouteInfo).child)
				{
					ObjectInfoAssist.FindLoop(ref _list, src4, _kind);
				}
				break;
			}
		}
	}
}
