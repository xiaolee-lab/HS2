using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Animal;
using Manager;
using MessagePack;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000885 RID: 2181
	[MessagePackObject(false)]
	public class CraftInfo
	{
		// Token: 0x060037CD RID: 14285 RVA: 0x0014C95C File Offset: 0x0014AD5C
		public CraftInfo()
		{
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0014C988 File Offset: 0x0014AD88
		public CraftInfo(Vector3 _size, int _area)
		{
			this.LimitSize = _size;
			this.AreaNo = _area;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0014C9C2 File Offset: 0x0014ADC2
		public CraftInfo(CraftInfo _src)
		{
			this.Copy(_src);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x0014C9F5 File Offset: 0x0014ADF5
		public CraftInfo(CraftInfo _src, bool _idCopy)
		{
			this.Copy(_src, _idCopy);
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060037D1 RID: 14289 RVA: 0x0014CA29 File Offset: 0x0014AE29
		// (set) Token: 0x060037D2 RID: 14290 RVA: 0x0014CA31 File Offset: 0x0014AE31
		[Key(0)]
		public List<IObjectInfo> ObjectInfos { get; set; } = new List<IObjectInfo>();

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x0014CA3A File Offset: 0x0014AE3A
		// (set) Token: 0x060037D4 RID: 14292 RVA: 0x0014CA42 File Offset: 0x0014AE42
		[Key(1)]
		public Vector3 LimitSize { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x0014CA4B File Offset: 0x0014AE4B
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x0014CA53 File Offset: 0x0014AE53
		[Key(2)]
		public int AreaNo { get; set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x0014CA5C File Offset: 0x0014AE5C
		// (set) Token: 0x060037D8 RID: 14296 RVA: 0x0014CA64 File Offset: 0x0014AE64
		[IgnoreMember]
		public Dictionary<IObjectInfo, ObjectCtrl> ObjectCtrls { get; private set; } = new Dictionary<IObjectInfo, ObjectCtrl>();

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x0014CA6D File Offset: 0x0014AE6D
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x0014CA75 File Offset: 0x0014AE75
		[IgnoreMember]
		public GameObject ObjRoot { get; set; }

		// Token: 0x060037DB RID: 14299 RVA: 0x0014CA80 File Offset: 0x0014AE80
		public void Copy(CraftInfo _src)
		{
			if (_src == null)
			{
				return;
			}
			this.ObjectInfos.Clear();
			foreach (IObjectInfo objectInfo in _src.ObjectInfos)
			{
				int kind = objectInfo.Kind;
				if (kind != 0)
				{
					if (kind == 1)
					{
						this.ObjectInfos.Add(new OIFolder(objectInfo as OIFolder));
					}
				}
				else
				{
					this.ObjectInfos.Add(new OIItem(objectInfo as OIItem));
				}
			}
			this.LimitSize = _src.LimitSize;
			this.AreaNo = _src.AreaNo;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x0014CB50 File Offset: 0x0014AF50
		public void Copy(CraftInfo _src, bool _idCopy)
		{
			if (_src == null)
			{
				return;
			}
			this.ObjectInfos.Clear();
			foreach (IObjectInfo objectInfo in _src.ObjectInfos)
			{
				int kind = objectInfo.Kind;
				if (kind != 0)
				{
					if (kind == 1)
					{
						this.ObjectInfos.Add(new OIFolder(objectInfo as OIFolder, _idCopy));
					}
				}
				else
				{
					this.ObjectInfos.Add(new OIItem(objectInfo as OIItem, _idCopy));
				}
			}
			this.LimitSize = _src.LimitSize;
			this.AreaNo = _src.AreaNo;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0014CC20 File Offset: 0x0014B020
		public void GetActionPoint(ref List<ActionPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetActionPoint(ref _points);
				}
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0014CC90 File Offset: 0x0014B090
		public void GetFarmPoint(ref List<FarmPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetFarmPoint(ref _points);
				}
			}
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0014CD00 File Offset: 0x0014B100
		public void GetHPoint(ref List<HPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetHPoint(ref _points);
				}
			}
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0014CD70 File Offset: 0x0014B170
		public void GetColInfo(ref List<ItemComponent.ColInfo> _infos)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetColInfo(ref _infos);
				}
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0014CDE0 File Offset: 0x0014B1E0
		public void GetPetHomePoint(ref List<PetHomePoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetPetHomePoint(ref _points);
				}
			}
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0014CE50 File Offset: 0x0014B250
		public void GetJukePoint(ref List<JukePoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetJukePoint(ref _points);
				}
			}
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0014CEC0 File Offset: 0x0014B2C0
		public void GetCraftPoint(ref List<CraftPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetCraftPoint(ref _points);
				}
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0014CF30 File Offset: 0x0014B330
		public void GetLightSwitchPoint(ref List<LightSwitchPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetLightSwitchPoint(ref _points);
				}
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0014CFA0 File Offset: 0x0014B3A0
		public int GetUsedNum(int _no)
		{
			int result = 0;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetUsedNum(_no, ref result);
				}
			}
			return result;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0014D014 File Offset: 0x0014B414
		public void SetOverlapColliders(bool _flag)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.SetOverlapColliders(_flag);
				}
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x0014D084 File Offset: 0x0014B484
		[IgnoreMember]
		public bool IsOverlapNow
		{
			[CompilerGenerated]
			get
			{
				return this.ObjectCtrls.Any((KeyValuePair<IObjectInfo, ObjectCtrl> v) => v.Value.IsOverlapNow);
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0014D0B0 File Offset: 0x0014B4B0
		public ObjectCtrl FindOverlapObject(ObjectCtrl _old)
		{
			List<ObjectCtrl> list = new List<ObjectCtrl>();
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.ObjectCtrls)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.GetOverlapObject(ref list);
				}
			}
			if (list.IsNullOrEmpty<ObjectCtrl>())
			{
				return null;
			}
			int num = list.FindIndex((ObjectCtrl v) => v == _old);
			return list.SafeGet(GlobalMethod.ValLoop(num + 1, list.Count));
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0014D168 File Offset: 0x0014B568
		public void DeleteObject()
		{
			foreach (ObjectCtrl objectCtrl in this.ObjectCtrls.Values.ToArray<ObjectCtrl>())
			{
				if (objectCtrl != null)
				{
					objectCtrl.OnDelete();
				}
			}
			this.ObjectCtrls.Clear();
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0014D1B8 File Offset: 0x0014B5B8
		public void Save(string _path, byte[] _pngData)
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(_pngData);
					binaryWriter.Write(100);
					binaryWriter.Write("【AIS_Housing】");
					binaryWriter.Write(this._version.ToString());
					binaryWriter.Write(Singleton<GameSystem>.Instance.UserUUID);
					binaryWriter.Write(YS_Assist.CreateUUID());
					binaryWriter.Write(Singleton<Housing>.Instance.GetSizeType(this.AreaNo));
					binaryWriter.Write(Singleton<GameSystem>.Instance.languageInt);
					byte[] buffer = MessagePackSerializer.Serialize<CraftInfo>(this);
					binaryWriter.Write(buffer);
				}
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0014D290 File Offset: 0x0014B690
		public bool Load(string _path)
		{
			bool result;
			try
			{
				using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (fileStream.Length == 0L)
					{
						result = false;
					}
					else
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							long num = PngFile.SkipPng(binaryReader);
							binaryReader.ReadInt32();
							binaryReader.ReadString();
							Version version = new Version(binaryReader.ReadString());
							binaryReader.ReadString();
							binaryReader.ReadString();
							binaryReader.ReadInt32();
							if (version.CompareTo(new Version("0.0.2")) >= 0)
							{
								binaryReader.ReadInt32();
							}
							byte[] bytes = binaryReader.ReadBytes((int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position));
							result = this.Load(bytes);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is FileNotFoundException)
				{
					result = false;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0014D3A4 File Offset: 0x0014B7A4
		public bool Load(string _assetBundle, string _asset)
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(_assetBundle, _asset, false, string.Empty);
			return !(textAsset == null) && this.Load(textAsset.bytes);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0014D3E0 File Offset: 0x0014B7E0
		public bool Load(byte[] _bytes)
		{
			try
			{
				if (_bytes.IsNullOrEmpty<byte>())
				{
					return false;
				}
				CraftInfo src = MessagePackSerializer.Deserialize<CraftInfo>(_bytes);
				this.Copy(src);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0014D430 File Offset: 0x0014B830
		public static CraftInfo LoadStatic(string _path)
		{
			CraftInfo result;
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				if (fileStream.Length == 0L)
				{
					result = null;
				}
				else
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						long num = PngFile.SkipPng(binaryReader);
						try
						{
							binaryReader.ReadInt32();
							binaryReader.ReadString();
							Version version = new Version(binaryReader.ReadString());
							binaryReader.ReadString();
							binaryReader.ReadString();
							binaryReader.ReadInt32();
							if (version.CompareTo(new Version("0.0.2")) >= 0)
							{
								binaryReader.ReadInt32();
							}
							byte[] array = binaryReader.ReadBytes((int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position));
							if (array.IsNullOrEmpty<byte>())
							{
								result = null;
							}
							else
							{
								result = MessagePackSerializer.Deserialize<CraftInfo>(array);
							}
						}
						catch (Exception)
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0014D544 File Offset: 0x0014B944
		public static CraftInfo.AboutInfo LoadAbout(string _path)
		{
			CraftInfo.AboutInfo result;
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					PngFile.SkipPng(binaryReader);
					result = new CraftInfo.AboutInfo(binaryReader);
				}
			}
			return result;
		}

		// Token: 0x04003862 RID: 14434
		[IgnoreMember]
		private readonly Version _version = new Version(0, 0, 2);

		// Token: 0x02000886 RID: 2182
		public class AboutInfo
		{
			// Token: 0x060037F1 RID: 14321 RVA: 0x0014D5BC File Offset: 0x0014B9BC
			public AboutInfo(BinaryReader _reader)
			{
				this.ProductNo = _reader.ReadInt32();
				this.FileMark = _reader.ReadString();
				this.Version = new Version(_reader.ReadString());
				this.UUID = _reader.ReadString();
				this.HUID = _reader.ReadString();
				this.AreaType = _reader.ReadInt32();
				if (this.Version.CompareTo(new Version("0.0.2")) >= 0)
				{
					this.LanguageInt = _reader.ReadInt32();
				}
			}

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x060037F2 RID: 14322 RVA: 0x0014D643 File Offset: 0x0014BA43
			// (set) Token: 0x060037F3 RID: 14323 RVA: 0x0014D64B File Offset: 0x0014BA4B
			public int ProductNo { get; private set; }

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x060037F4 RID: 14324 RVA: 0x0014D654 File Offset: 0x0014BA54
			// (set) Token: 0x060037F5 RID: 14325 RVA: 0x0014D65C File Offset: 0x0014BA5C
			public string FileMark { get; private set; }

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x060037F6 RID: 14326 RVA: 0x0014D665 File Offset: 0x0014BA65
			// (set) Token: 0x060037F7 RID: 14327 RVA: 0x0014D66D File Offset: 0x0014BA6D
			public Version Version { get; private set; }

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x060037F8 RID: 14328 RVA: 0x0014D676 File Offset: 0x0014BA76
			// (set) Token: 0x060037F9 RID: 14329 RVA: 0x0014D67E File Offset: 0x0014BA7E
			public string UUID { get; private set; }

			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x060037FA RID: 14330 RVA: 0x0014D687 File Offset: 0x0014BA87
			// (set) Token: 0x060037FB RID: 14331 RVA: 0x0014D68F File Offset: 0x0014BA8F
			public string HUID { get; private set; }

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x060037FC RID: 14332 RVA: 0x0014D698 File Offset: 0x0014BA98
			// (set) Token: 0x060037FD RID: 14333 RVA: 0x0014D6A0 File Offset: 0x0014BAA0
			public int AreaType { get; private set; }

			// Token: 0x170009EE RID: 2542
			// (get) Token: 0x060037FE RID: 14334 RVA: 0x0014D6A9 File Offset: 0x0014BAA9
			// (set) Token: 0x060037FF RID: 14335 RVA: 0x0014D6B1 File Offset: 0x0014BAB1
			public int LanguageInt { get; private set; }
		}
	}
}
