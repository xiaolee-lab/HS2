using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200122C RID: 4652
	public class OIItemInfo : ObjectInfo
	{
		// Token: 0x06009928 RID: 39208 RVA: 0x003F1180 File Offset: 0x003EF580
		public OIItemInfo(int _group, int _category, int _no, int _key) : base(_key)
		{
			this.group = _group;
			this.category = _category;
			this.no = _no;
			this.child = new List<ObjectInfo>();
			this.colors = new ColorInfo[]
			{
				new ColorInfo(),
				new ColorInfo(),
				new ColorInfo(),
				new ColorInfo()
			};
			this.emissionColor = Utility.ConvertColor(255, 255, 255);
			this.panel = new PatternInfo();
			this.bones = new Dictionary<string, OIBoneInfo>();
			this.option = new List<bool>();
			this.animeNormalizedTime = 0f;
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x06009929 RID: 39209 RVA: 0x003F1244 File Offset: 0x003EF644
		public override int kind
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x0600992A RID: 39210 RVA: 0x003F1247 File Offset: 0x003EF647
		// (set) Token: 0x0600992B RID: 39211 RVA: 0x003F124F File Offset: 0x003EF64F
		public int group { get; private set; }

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x0600992C RID: 39212 RVA: 0x003F1258 File Offset: 0x003EF658
		// (set) Token: 0x0600992D RID: 39213 RVA: 0x003F1260 File Offset: 0x003EF660
		public int category { get; private set; }

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x0600992E RID: 39214 RVA: 0x003F1269 File Offset: 0x003EF669
		// (set) Token: 0x0600992F RID: 39215 RVA: 0x003F1271 File Offset: 0x003EF671
		public int no { get; private set; }

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x06009930 RID: 39216 RVA: 0x003F127A File Offset: 0x003EF67A
		// (set) Token: 0x06009931 RID: 39217 RVA: 0x003F1282 File Offset: 0x003EF682
		public List<ObjectInfo> child { get; private set; }

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x06009932 RID: 39218 RVA: 0x003F128B File Offset: 0x003EF68B
		// (set) Token: 0x06009933 RID: 39219 RVA: 0x003F12B0 File Offset: 0x003EF6B0
		public Color EmissionColor
		{
			get
			{
				this.DecomposeHDRColor(this.emissionColor, out this.emissionColor32, out this.emissionColorIntensity);
				return this.emissionColor32;
			}
			set
			{
				this.emissionColor = ((!Mathf.Approximately(this.emissionColorIntensity, 0f)) ? (value * Mathf.Pow(2f, this.emissionColorIntensity)) : value);
			}
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x003F12EC File Offset: 0x003EF6EC
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.group);
			_writer.Write(this.category);
			_writer.Write(this.no);
			_writer.Write(this.animePattern);
			_writer.Write(this.animeSpeed);
			for (int i = 0; i < 4; i++)
			{
				this.colors[i].Save(_writer, _version);
			}
			_writer.Write(this.alpha);
			_writer.Write(JsonUtility.ToJson(this.emissionColor));
			_writer.Write(this.emissionPower);
			_writer.Write(this.lightCancel);
			this.panel.Save(_writer, _version);
			_writer.Write(this.enableFK);
			_writer.Write(this.bones.Count);
			foreach (KeyValuePair<string, OIBoneInfo> keyValuePair in this.bones)
			{
				_writer.Write(keyValuePair.Key);
				keyValuePair.Value.Save(_writer, _version);
			}
			_writer.Write(this.enableDynamicBone);
			_writer.Write(this.option.Count);
			foreach (bool value in this.option)
			{
				_writer.Write(value);
			}
			_writer.Write(this.animeNormalizedTime);
			int count = this.child.Count;
			_writer.Write(count);
			for (int j = 0; j < count; j++)
			{
				this.child[j].Save(_writer, _version);
			}
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x003F14DC File Offset: 0x003EF8DC
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.group = _reader.ReadInt32();
			this.category = _reader.ReadInt32();
			this.no = _reader.ReadInt32();
			if (_version.CompareTo(new Version(1, 0, 1)) >= 0)
			{
				this.animePattern = _reader.ReadInt32();
			}
			this.animeSpeed = _reader.ReadSingle();
			for (int i = 0; i < 4; i++)
			{
				this.colors[i].Load(_reader, _version);
			}
			this.alpha = _reader.ReadSingle();
			this.emissionColor = JsonUtility.FromJson<Color>(_reader.ReadString());
			this.emissionPower = _reader.ReadSingle();
			this.lightCancel = _reader.ReadSingle();
			this.panel.Load(_reader, _version);
			this.enableFK = _reader.ReadBoolean();
			int num = _reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				string key = _reader.ReadString();
				this.bones[key] = new OIBoneInfo(_import ? Studio.GetNewIndex() : -1);
				this.bones[key].Load(_reader, _version, _import, true);
			}
			this.enableDynamicBone = _reader.ReadBoolean();
			num = _reader.ReadInt32();
			for (int k = 0; k < num; k++)
			{
				this.option.Add(_reader.ReadBoolean());
			}
			this.animeNormalizedTime = _reader.ReadSingle();
			ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x003F1660 File Offset: 0x003EFA60
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
			int count = this.child.Count;
			for (int i = 0; i < count; i++)
			{
				this.child[i].DeleteKey();
			}
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x003F16A8 File Offset: 0x003EFAA8
		internal void DecomposeHDRColor(Color _colorHDR, out Color32 _baseColor, out float _intensity)
		{
			_baseColor = Color.black;
			float maxColorComponent = _colorHDR.maxColorComponent;
			byte b = 191;
			if (Mathf.Approximately(maxColorComponent, 0f) || (maxColorComponent <= 1f && maxColorComponent > 0.003921569f))
			{
				_intensity = 0f;
				_baseColor.r = (byte)Mathf.RoundToInt(_colorHDR.r * 255f);
				_baseColor.g = (byte)Mathf.RoundToInt(_colorHDR.g * 255f);
				_baseColor.b = (byte)Mathf.RoundToInt(_colorHDR.b * 255f);
			}
			else
			{
				float num = (float)b / maxColorComponent;
				_intensity = Mathf.Log(255f / num) / Mathf.Log(2f);
				_baseColor.r = Math.Min(b, (byte)Mathf.CeilToInt(num * _colorHDR.r));
				_baseColor.g = Math.Min(b, (byte)Mathf.CeilToInt(num * _colorHDR.g));
				_baseColor.b = Math.Min(b, (byte)Mathf.CeilToInt(num * _colorHDR.b));
			}
		}

		// Token: 0x04007A30 RID: 31280
		public int animePattern;

		// Token: 0x04007A31 RID: 31281
		public float animeSpeed = 1f;

		// Token: 0x04007A32 RID: 31282
		public ColorInfo[] colors;

		// Token: 0x04007A33 RID: 31283
		public float alpha = 1f;

		// Token: 0x04007A34 RID: 31284
		public Color emissionColor;

		// Token: 0x04007A35 RID: 31285
		public float emissionPower;

		// Token: 0x04007A36 RID: 31286
		public float lightCancel;

		// Token: 0x04007A37 RID: 31287
		public PatternInfo panel;

		// Token: 0x04007A38 RID: 31288
		public bool enableFK;

		// Token: 0x04007A39 RID: 31289
		public Dictionary<string, OIBoneInfo> bones;

		// Token: 0x04007A3A RID: 31290
		public bool enableDynamicBone = true;

		// Token: 0x04007A3B RID: 31291
		public List<bool> option;

		// Token: 0x04007A3C RID: 31292
		public float animeNormalizedTime;

		// Token: 0x04007A3D RID: 31293
		private Color32 emissionColor32;

		// Token: 0x04007A3E RID: 31294
		private float emissionColorIntensity;
	}
}
