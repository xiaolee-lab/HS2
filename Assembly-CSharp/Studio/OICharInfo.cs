using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AIChara;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001225 RID: 4645
	public class OICharInfo : ObjectInfo
	{
		// Token: 0x060098F5 RID: 39157 RVA: 0x003F00BC File Offset: 0x003EE4BC
		public OICharInfo(ChaFileControl _charFile, int _key)
		{
			bool[] array = new bool[7];
			array[1] = true;
			array[3] = true;
			this.activeFK = array;
			this.expression = new bool[8];
			this.animeSpeed = 1f;
			this.animeOptionVisible = true;
			this.voiceCtrl = new VoiceCtrl();
			this.sonLength = 1f;
			this.animeOptionParam = new float[2];
			this.siru = new byte[5];
			base..ctor(_key);
			this.sex = (int)((_charFile == null) ? 0 : _charFile.parameter.sex);
			this.charFile = _charFile;
			this.bones = new Dictionary<int, OIBoneInfo>();
			this.ikTarget = new Dictionary<int, OIIKTargetInfo>();
			this.child = new Dictionary<int, List<ObjectInfo>>();
			foreach (int key in Singleton<Info>.Instance.AccessoryPointsIndex)
			{
				this.child[key] = new List<ObjectInfo>();
			}
			this.simpleColor = Color.blue;
			this.dicAccessGroup = new Dictionary<int, TreeNodeObject.TreeState>();
			this.dicAccessNo = new Dictionary<int, TreeNodeObject.TreeState>();
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x060098F6 RID: 39158 RVA: 0x003F0201 File Offset: 0x003EE601
		public override int kind
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x060098F7 RID: 39159 RVA: 0x003F0204 File Offset: 0x003EE604
		// (set) Token: 0x060098F8 RID: 39160 RVA: 0x003F020C File Offset: 0x003EE60C
		public int sex { get; private set; }

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x060098F9 RID: 39161 RVA: 0x003F0215 File Offset: 0x003EE615
		// (set) Token: 0x060098FA RID: 39162 RVA: 0x003F021D File Offset: 0x003EE61D
		public ChaFileControl charFile { get; private set; }

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x060098FB RID: 39163 RVA: 0x003F0226 File Offset: 0x003EE626
		// (set) Token: 0x060098FC RID: 39164 RVA: 0x003F022E File Offset: 0x003EE62E
		public Dictionary<int, OIBoneInfo> bones { get; private set; }

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x060098FD RID: 39165 RVA: 0x003F0237 File Offset: 0x003EE637
		// (set) Token: 0x060098FE RID: 39166 RVA: 0x003F023F File Offset: 0x003EE63F
		public Dictionary<int, OIIKTargetInfo> ikTarget { get; private set; }

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x060098FF RID: 39167 RVA: 0x003F0248 File Offset: 0x003EE648
		// (set) Token: 0x06009900 RID: 39168 RVA: 0x003F0250 File Offset: 0x003EE650
		public Dictionary<int, List<ObjectInfo>> child { get; private set; }

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x06009901 RID: 39169 RVA: 0x003F0259 File Offset: 0x003EE659
		// (set) Token: 0x06009902 RID: 39170 RVA: 0x003F0261 File Offset: 0x003EE661
		public LookAtTargetInfo lookAtTarget { get; set; }

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x06009903 RID: 39171 RVA: 0x003F026A File Offset: 0x003EE66A
		public float WetRate
		{
			[CompilerGenerated]
			get
			{
				return this.charFile.status.wetRate;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x06009904 RID: 39172 RVA: 0x003F027C File Offset: 0x003EE67C
		public float SkinTuyaRate
		{
			[CompilerGenerated]
			get
			{
				return this.charFile.status.skinTuyaRate;
			}
		}

		// Token: 0x06009905 RID: 39173 RVA: 0x003F0290 File Offset: 0x003EE690
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.sex);
			this.charFile.SaveCharaFile(_writer, false);
			int num = this.bones.Count;
			_writer.Write(num);
			foreach (KeyValuePair<int, OIBoneInfo> keyValuePair in this.bones)
			{
				int key = keyValuePair.Key;
				_writer.Write(key);
				keyValuePair.Value.Save(_writer, _version);
			}
			num = this.ikTarget.Count;
			_writer.Write(num);
			foreach (KeyValuePair<int, OIIKTargetInfo> keyValuePair2 in this.ikTarget)
			{
				int key2 = keyValuePair2.Key;
				_writer.Write(key2);
				keyValuePair2.Value.Save(_writer, _version);
			}
			num = this.child.Count;
			_writer.Write(num);
			foreach (KeyValuePair<int, List<ObjectInfo>> keyValuePair3 in this.child)
			{
				int key3 = keyValuePair3.Key;
				_writer.Write(key3);
				num = keyValuePair3.Value.Count;
				_writer.Write(num);
				for (int i = 0; i < num; i++)
				{
					keyValuePair3.Value[i].Save(_writer, _version);
				}
			}
			_writer.Write((int)this.kinematicMode);
			_writer.Write(this.animeInfo.group);
			_writer.Write(this.animeInfo.category);
			_writer.Write(this.animeInfo.no);
			for (int j = 0; j < 2; j++)
			{
				_writer.Write(this.handPtn[j]);
			}
			_writer.Write(this.nipple);
			_writer.Write(this.siru);
			_writer.Write(this.mouthOpen);
			_writer.Write(this.lipSync);
			this.lookAtTarget.Save(_writer, _version);
			_writer.Write(this.enableIK);
			for (int k = 0; k < 5; k++)
			{
				_writer.Write(this.activeIK[k]);
			}
			_writer.Write(this.enableFK);
			for (int l = 0; l < 7; l++)
			{
				_writer.Write(this.activeFK[l]);
			}
			for (int m = 0; m < 8; m++)
			{
				_writer.Write(this.expression[m]);
			}
			_writer.Write(this.animeSpeed);
			_writer.Write(this.animePattern);
			_writer.Write(this.animeOptionVisible);
			_writer.Write(this.isAnimeForceLoop);
			this.voiceCtrl.Save(_writer, _version);
			_writer.Write(this.visibleSon);
			_writer.Write(this.sonLength);
			_writer.Write(this.visibleSimple);
			_writer.Write(JsonUtility.ToJson(this.simpleColor));
			_writer.Write(this.animeOptionParam[0]);
			_writer.Write(this.animeOptionParam[1]);
			_writer.Write(this.neckByteData.Length);
			_writer.Write(this.neckByteData);
			_writer.Write(this.eyesByteData.Length);
			_writer.Write(this.eyesByteData);
			_writer.Write(this.animeNormalizedTime);
			num = ((this.dicAccessGroup == null) ? 0 : this.dicAccessGroup.Count);
			_writer.Write(num);
			if (num != 0)
			{
				foreach (KeyValuePair<int, TreeNodeObject.TreeState> keyValuePair4 in this.dicAccessGroup)
				{
					_writer.Write(keyValuePair4.Key);
					_writer.Write((int)keyValuePair4.Value);
				}
			}
			num = ((this.dicAccessNo == null) ? 0 : this.dicAccessNo.Count);
			_writer.Write(num);
			if (num != 0)
			{
				foreach (KeyValuePair<int, TreeNodeObject.TreeState> keyValuePair5 in this.dicAccessNo)
				{
					_writer.Write(keyValuePair5.Key);
					_writer.Write((int)keyValuePair5.Value);
				}
			}
		}

		// Token: 0x06009906 RID: 39174 RVA: 0x003F0768 File Offset: 0x003EEB68
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.sex = _reader.ReadInt32();
			this.charFile = new ChaFileControl();
			this.charFile.LoadCharaFile(_reader, true, false);
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int key = _reader.ReadInt32();
				this.bones[key] = new OIBoneInfo(_import ? Studio.GetNewIndex() : -1);
				this.bones[key].Load(_reader, _version, _import, true);
			}
			num = _reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				int key2 = _reader.ReadInt32();
				this.ikTarget[key2] = new OIIKTargetInfo(_import ? Studio.GetNewIndex() : -1);
				this.ikTarget[key2].Load(_reader, _version, _import, true);
			}
			num = _reader.ReadInt32();
			for (int k = 0; k < num; k++)
			{
				int key3 = _reader.ReadInt32();
				ObjectInfoAssist.LoadChild(_reader, _version, this.child[key3], _import);
			}
			this.kinematicMode = (OICharInfo.KinematicMode)_reader.ReadInt32();
			this.animeInfo.group = _reader.ReadInt32();
			this.animeInfo.category = _reader.ReadInt32();
			this.animeInfo.no = _reader.ReadInt32();
			for (int l = 0; l < 2; l++)
			{
				this.handPtn[l] = _reader.ReadInt32();
			}
			this.nipple = _reader.ReadSingle();
			this.siru = _reader.ReadBytes(5);
			this.mouthOpen = _reader.ReadSingle();
			this.lipSync = _reader.ReadBoolean();
			if (this.lookAtTarget == null)
			{
				this.lookAtTarget = new LookAtTargetInfo(_import ? Studio.GetNewIndex() : -1);
			}
			this.lookAtTarget.Load(_reader, _version, _import, true);
			this.enableIK = _reader.ReadBoolean();
			for (int m = 0; m < 5; m++)
			{
				this.activeIK[m] = _reader.ReadBoolean();
			}
			this.enableFK = _reader.ReadBoolean();
			for (int n = 0; n < 7; n++)
			{
				this.activeFK[n] = _reader.ReadBoolean();
			}
			for (int num2 = 0; num2 < 8; num2++)
			{
				this.expression[num2] = _reader.ReadBoolean();
			}
			this.animeSpeed = _reader.ReadSingle();
			this.animePattern = _reader.ReadSingle();
			this.animeOptionVisible = _reader.ReadBoolean();
			this.isAnimeForceLoop = _reader.ReadBoolean();
			this.voiceCtrl.Load(_reader, _version);
			this.visibleSon = _reader.ReadBoolean();
			this.sonLength = _reader.ReadSingle();
			this.visibleSimple = _reader.ReadBoolean();
			this.simpleColor = JsonUtility.FromJson<Color>(_reader.ReadString());
			this.animeOptionParam[0] = _reader.ReadSingle();
			this.animeOptionParam[1] = _reader.ReadSingle();
			num = _reader.ReadInt32();
			this.neckByteData = _reader.ReadBytes(num);
			num = _reader.ReadInt32();
			this.eyesByteData = _reader.ReadBytes(num);
			this.animeNormalizedTime = _reader.ReadSingle();
			num = _reader.ReadInt32();
			if (num != 0)
			{
				this.dicAccessGroup = new Dictionary<int, TreeNodeObject.TreeState>();
			}
			for (int num3 = 0; num3 < num; num3++)
			{
				int key4 = _reader.ReadInt32();
				this.dicAccessGroup[key4] = (TreeNodeObject.TreeState)_reader.ReadInt32();
			}
			num = _reader.ReadInt32();
			if (num != 0)
			{
				this.dicAccessNo = new Dictionary<int, TreeNodeObject.TreeState>();
			}
			for (int num4 = 0; num4 < num; num4++)
			{
				int key5 = _reader.ReadInt32();
				this.dicAccessNo[key5] = (TreeNodeObject.TreeState)_reader.ReadInt32();
			}
		}

		// Token: 0x06009907 RID: 39175 RVA: 0x003F0B38 File Offset: 0x003EEF38
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
			foreach (KeyValuePair<int, OIBoneInfo> keyValuePair in this.bones)
			{
				Studio.DeleteIndex(keyValuePair.Value.dicKey);
			}
			foreach (KeyValuePair<int, OIIKTargetInfo> keyValuePair2 in this.ikTarget)
			{
				Studio.DeleteIndex(keyValuePair2.Value.dicKey);
			}
			foreach (KeyValuePair<int, List<ObjectInfo>> keyValuePair3 in this.child)
			{
				int count = keyValuePair3.Value.Count;
				for (int i = 0; i < count; i++)
				{
					keyValuePair3.Value[i].DeleteKey();
				}
			}
			Studio.DeleteIndex(this.lookAtTarget.dicKey);
		}

		// Token: 0x040079EF RID: 31215
		public OICharInfo.KinematicMode kinematicMode;

		// Token: 0x040079F0 RID: 31216
		public OICharInfo.AnimeInfo animeInfo = new OICharInfo.AnimeInfo();

		// Token: 0x040079F1 RID: 31217
		public int[] handPtn = new int[2];

		// Token: 0x040079F2 RID: 31218
		public float mouthOpen;

		// Token: 0x040079F3 RID: 31219
		public bool lipSync = true;

		// Token: 0x040079F5 RID: 31221
		public bool enableIK;

		// Token: 0x040079F6 RID: 31222
		public bool[] activeIK = new bool[]
		{
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x040079F7 RID: 31223
		public bool enableFK;

		// Token: 0x040079F8 RID: 31224
		public bool[] activeFK;

		// Token: 0x040079F9 RID: 31225
		public bool[] expression;

		// Token: 0x040079FA RID: 31226
		public float animeSpeed;

		// Token: 0x040079FB RID: 31227
		public float animePattern;

		// Token: 0x040079FC RID: 31228
		public bool animeOptionVisible;

		// Token: 0x040079FD RID: 31229
		public bool isAnimeForceLoop;

		// Token: 0x040079FE RID: 31230
		public VoiceCtrl voiceCtrl;

		// Token: 0x040079FF RID: 31231
		public bool visibleSon;

		// Token: 0x04007A00 RID: 31232
		public float sonLength;

		// Token: 0x04007A01 RID: 31233
		public float[] animeOptionParam;

		// Token: 0x04007A02 RID: 31234
		public float nipple;

		// Token: 0x04007A03 RID: 31235
		public byte[] siru;

		// Token: 0x04007A04 RID: 31236
		public bool visibleSimple;

		// Token: 0x04007A05 RID: 31237
		public Color simpleColor;

		// Token: 0x04007A06 RID: 31238
		public byte[] neckByteData;

		// Token: 0x04007A07 RID: 31239
		public byte[] eyesByteData;

		// Token: 0x04007A08 RID: 31240
		public float animeNormalizedTime;

		// Token: 0x04007A09 RID: 31241
		public Dictionary<int, TreeNodeObject.TreeState> dicAccessGroup;

		// Token: 0x04007A0A RID: 31242
		public Dictionary<int, TreeNodeObject.TreeState> dicAccessNo;

		// Token: 0x02001226 RID: 4646
		public enum IKTargetEN
		{
			// Token: 0x04007A0C RID: 31244
			Body,
			// Token: 0x04007A0D RID: 31245
			LeftShoulder,
			// Token: 0x04007A0E RID: 31246
			LeftArmChain,
			// Token: 0x04007A0F RID: 31247
			LeftHand,
			// Token: 0x04007A10 RID: 31248
			RightShoulder,
			// Token: 0x04007A11 RID: 31249
			RightArmChain,
			// Token: 0x04007A12 RID: 31250
			RightHand,
			// Token: 0x04007A13 RID: 31251
			LeftThigh,
			// Token: 0x04007A14 RID: 31252
			LeftLegChain,
			// Token: 0x04007A15 RID: 31253
			LeftFoot,
			// Token: 0x04007A16 RID: 31254
			RightThigh,
			// Token: 0x04007A17 RID: 31255
			RightLegChain,
			// Token: 0x04007A18 RID: 31256
			RightFoot
		}

		// Token: 0x02001227 RID: 4647
		public enum KinematicMode
		{
			// Token: 0x04007A1A RID: 31258
			None,
			// Token: 0x04007A1B RID: 31259
			FK,
			// Token: 0x04007A1C RID: 31260
			IK
		}

		// Token: 0x02001228 RID: 4648
		public class AnimeInfo
		{
			// Token: 0x06009909 RID: 39177 RVA: 0x003F0C98 File Offset: 0x003EF098
			public void Set(int _group, int _category, int _no)
			{
				this.group = _group;
				this.category = _category;
				this.no = _no;
			}

			// Token: 0x17002090 RID: 8336
			// (get) Token: 0x0600990A RID: 39178 RVA: 0x003F0CAF File Offset: 0x003EF0AF
			public bool exist
			{
				get
				{
					return this.group != -1 & this.category != -1 & this.no != -1;
				}
			}

			// Token: 0x0600990B RID: 39179 RVA: 0x003F0CD7 File Offset: 0x003EF0D7
			public void Copy(OICharInfo.AnimeInfo _src)
			{
				this.group = _src.group;
				this.category = _src.category;
				this.no = _src.no;
			}

			// Token: 0x04007A1D RID: 31261
			public int group;

			// Token: 0x04007A1E RID: 31262
			public int category;

			// Token: 0x04007A1F RID: 31263
			public int no;
		}
	}
}
