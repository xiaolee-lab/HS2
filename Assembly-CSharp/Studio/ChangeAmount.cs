using System;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200121D RID: 4637
	public class ChangeAmount
	{
		// Token: 0x060098C1 RID: 39105 RVA: 0x003EF658 File Offset: 0x003EDA58
		public ChangeAmount()
		{
			this.m_Pos = Vector3.zero;
			this.m_Rot = Vector3.zero;
			this.m_Scale = Vector3.one;
		}

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x060098C2 RID: 39106 RVA: 0x003EF6B8 File Offset: 0x003EDAB8
		// (set) Token: 0x060098C3 RID: 39107 RVA: 0x003EF6C0 File Offset: 0x003EDAC0
		public Vector3 pos
		{
			get
			{
				return this.m_Pos;
			}
			set
			{
				if (Utility.SetStruct<Vector3>(ref this.m_Pos, value) && this.onChangePos != null)
				{
					this.onChangePos();
					if (this.onChangePosAfter != null)
					{
						this.onChangePosAfter();
					}
				}
			}
		}

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x060098C4 RID: 39108 RVA: 0x003EF6FF File Offset: 0x003EDAFF
		// (set) Token: 0x060098C5 RID: 39109 RVA: 0x003EF707 File Offset: 0x003EDB07
		public Vector3 rot
		{
			get
			{
				return this.m_Rot;
			}
			set
			{
				if (Utility.SetStruct<Vector3>(ref this.m_Rot, value) && this.onChangeRot != null)
				{
					this.onChangeRot();
				}
			}
		}

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x060098C6 RID: 39110 RVA: 0x003EF730 File Offset: 0x003EDB30
		// (set) Token: 0x060098C7 RID: 39111 RVA: 0x003EF738 File Offset: 0x003EDB38
		public Vector3 scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				if (Utility.SetStruct<Vector3>(ref this.m_Scale, value) && this.onChangeScale != null)
				{
					this.onChangeScale(value);
				}
			}
		}

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x060098C8 RID: 39112 RVA: 0x003EF762 File Offset: 0x003EDB62
		// (set) Token: 0x060098C9 RID: 39113 RVA: 0x003EF76A File Offset: 0x003EDB6A
		public Vector3 defRot { get; set; } = Vector3.zero;

		// Token: 0x060098CA RID: 39114 RVA: 0x003EF774 File Offset: 0x003EDB74
		public void Save(BinaryWriter _writer)
		{
			_writer.Write(this.m_Pos.x);
			_writer.Write(this.m_Pos.y);
			_writer.Write(this.m_Pos.z);
			_writer.Write(this.m_Rot.x);
			_writer.Write(this.m_Rot.y);
			_writer.Write(this.m_Rot.z);
			_writer.Write(this.m_Scale.x);
			_writer.Write(this.m_Scale.y);
			_writer.Write(this.m_Scale.z);
		}

		// Token: 0x060098CB RID: 39115 RVA: 0x003EF81C File Offset: 0x003EDC1C
		public void Load(BinaryReader _reader)
		{
			Vector3 vector = this.m_Pos;
			vector.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
			this.m_Pos = vector;
			vector = this.m_Rot;
			vector.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
			this.m_Rot = vector;
			vector = this.m_Scale;
			vector.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
			this.m_Scale = vector;
		}

		// Token: 0x060098CC RID: 39116 RVA: 0x003EF8A0 File Offset: 0x003EDCA0
		public ChangeAmount Clone()
		{
			return new ChangeAmount
			{
				pos = new Vector3(this.m_Pos.x, this.m_Pos.y, this.m_Pos.z),
				rot = new Vector3(this.m_Rot.x, this.m_Rot.y, this.m_Rot.z),
				scale = new Vector3(this.m_Scale.x, this.m_Scale.y, this.m_Scale.z)
			};
		}

		// Token: 0x060098CD RID: 39117 RVA: 0x003EF938 File Offset: 0x003EDD38
		public void Copy(ChangeAmount _source, bool _pos = true, bool _rot = true, bool _scale = true)
		{
			if (_pos)
			{
				this.pos = _source.pos;
			}
			if (_rot)
			{
				this.rot = _source.rot;
			}
			if (_scale)
			{
				this.scale = _source.scale;
			}
		}

		// Token: 0x060098CE RID: 39118 RVA: 0x003EF974 File Offset: 0x003EDD74
		public void OnChange()
		{
			if (this.onChangePos != null)
			{
				this.onChangePos();
				if (this.onChangePosAfter != null)
				{
					this.onChangePosAfter();
				}
			}
			if (this.onChangeRot != null)
			{
				this.onChangeRot();
			}
			if (this.onChangeScale != null)
			{
				this.onChangeScale(this.scale);
			}
		}

		// Token: 0x060098CF RID: 39119 RVA: 0x003EF9DF File Offset: 0x003EDDDF
		public void Reset()
		{
			this.pos = Vector3.zero;
			this.rot = Vector3.zero;
			this.scale = Vector3.one;
		}

		// Token: 0x040079CE RID: 31182
		protected Vector3 m_Pos = Vector3.zero;

		// Token: 0x040079CF RID: 31183
		protected Vector3 m_Rot = Vector3.zero;

		// Token: 0x040079D0 RID: 31184
		protected Vector3 m_Scale = Vector3.one;

		// Token: 0x040079D2 RID: 31186
		public Action onChangePos;

		// Token: 0x040079D3 RID: 31187
		public Action onChangePosAfter;

		// Token: 0x040079D4 RID: 31188
		public Action onChangeRot;

		// Token: 0x040079D5 RID: 31189
		public Action<Vector3> onChangeScale;
	}
}
