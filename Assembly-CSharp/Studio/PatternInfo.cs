using System;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200122A RID: 4650
	public class PatternInfo
	{
		// Token: 0x06009913 RID: 39187 RVA: 0x003F0E04 File Offset: 0x003EF204
		public PatternInfo()
		{
			this._key.Subscribe(delegate(int _)
			{
				if (_ != -1)
				{
					this._filePath.Value = string.Empty;
				}
			});
			this._filePath.Subscribe(delegate(string _)
			{
				if (!_.IsNullOrEmpty())
				{
					this._key.Value = -1;
				}
			});
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06009914 RID: 39188 RVA: 0x003F0E94 File Offset: 0x003EF294
		// (set) Token: 0x06009915 RID: 39189 RVA: 0x003F0EA1 File Offset: 0x003EF2A1
		public int key
		{
			get
			{
				return this._key.Value;
			}
			set
			{
				this._key.Value = value;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06009916 RID: 39190 RVA: 0x003F0EAF File Offset: 0x003EF2AF
		// (set) Token: 0x06009917 RID: 39191 RVA: 0x003F0EBC File Offset: 0x003EF2BC
		public string filePath
		{
			get
			{
				return this._filePath.Value;
			}
			set
			{
				this._filePath.Value = value;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06009918 RID: 39192 RVA: 0x003F0ECA File Offset: 0x003EF2CA
		// (set) Token: 0x06009919 RID: 39193 RVA: 0x003F0ED7 File Offset: 0x003EF2D7
		public float ut
		{
			get
			{
				return this.uv.z;
			}
			set
			{
				this.uv.z = value;
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x0600991A RID: 39194 RVA: 0x003F0EE5 File Offset: 0x003EF2E5
		// (set) Token: 0x0600991B RID: 39195 RVA: 0x003F0EF2 File Offset: 0x003EF2F2
		public float vt
		{
			get
			{
				return this.uv.w;
			}
			set
			{
				this.uv.w = value;
			}
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x0600991C RID: 39196 RVA: 0x003F0F00 File Offset: 0x003EF300
		// (set) Token: 0x0600991D RID: 39197 RVA: 0x003F0F0D File Offset: 0x003EF30D
		public float us
		{
			get
			{
				return this.uv.x;
			}
			set
			{
				this.uv.x = value;
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x0600991E RID: 39198 RVA: 0x003F0F1B File Offset: 0x003EF31B
		// (set) Token: 0x0600991F RID: 39199 RVA: 0x003F0F28 File Offset: 0x003EF328
		public float vs
		{
			get
			{
				return this.uv.y;
			}
			set
			{
				this.uv.y = value;
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x06009920 RID: 39200 RVA: 0x003F0F38 File Offset: 0x003EF338
		public string name
		{
			get
			{
				int _key = this.key;
				if (_key != -1)
				{
					List<PatternSelectInfo> lstSelectInfo = Singleton<Studio>.Instance.patternSelectListCtrl.lstSelectInfo;
					PatternSelectInfo patternSelectInfo = lstSelectInfo.Find((PatternSelectInfo p) => p.index == _key);
					return (patternSelectInfo == null) ? "なし" : patternSelectInfo.name;
				}
				return (!this.filePath.IsNullOrEmpty()) ? Path.GetFileNameWithoutExtension(this.filePath) : "なし";
			}
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x003F0FC4 File Offset: 0x003EF3C4
		public void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(JsonUtility.ToJson(this.color));
			_writer.Write(this._key.Value);
			_writer.Write(this._filePath.Value);
			_writer.Write(this.clamp);
			_writer.Write(JsonUtility.ToJson(this.uv));
			_writer.Write(this.rot);
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x003F1038 File Offset: 0x003EF438
		public void Load(BinaryReader _reader, Version _version)
		{
			this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
			this._key.Value = _reader.ReadInt32();
			this._filePath.Value = _reader.ReadString();
			this.clamp = _reader.ReadBoolean();
			this.uv = JsonUtility.FromJson<Vector4>(_reader.ReadString());
			this.rot = _reader.ReadSingle();
		}

		// Token: 0x04007A22 RID: 31266
		private IntReactiveProperty _key = new IntReactiveProperty(0);

		// Token: 0x04007A23 RID: 31267
		private StringReactiveProperty _filePath = new StringReactiveProperty(string.Empty);

		// Token: 0x04007A24 RID: 31268
		public Color color = Color.white;

		// Token: 0x04007A25 RID: 31269
		public bool clamp = true;

		// Token: 0x04007A26 RID: 31270
		public Vector4 uv = new Vector4(0f, 0f, 1f, 1f);

		// Token: 0x04007A27 RID: 31271
		public float rot;
	}
}
