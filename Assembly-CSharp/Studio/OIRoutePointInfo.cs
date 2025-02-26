using System;
using System.IO;

namespace Studio
{
	// Token: 0x0200122F RID: 4655
	public class OIRoutePointInfo : ObjectInfo
	{
		// Token: 0x06009944 RID: 39236 RVA: 0x003F1985 File Offset: 0x003EFD85
		public OIRoutePointInfo(int _key) : base(_key)
		{
			this.number = 0;
			this.speed = 2f;
			this.easeType = StudioTween.EaseType.linear;
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06009945 RID: 39237 RVA: 0x003F19BB File Offset: 0x003EFDBB
		public override int kind
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06009946 RID: 39238 RVA: 0x003F19BE File Offset: 0x003EFDBE
		// (set) Token: 0x06009947 RID: 39239 RVA: 0x003F19C6 File Offset: 0x003EFDC6
		public string name { get; private set; }

		// Token: 0x170020A5 RID: 8357
		// (set) Token: 0x06009948 RID: 39240 RVA: 0x003F19CF File Offset: 0x003EFDCF
		public int number
		{
			set
			{
				this.name = ((value != 0) ? string.Format("ポイント{0}", value) : "スタート");
			}
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x06009949 RID: 39241 RVA: 0x003F19F7 File Offset: 0x003EFDF7
		public override int[] kinds
		{
			get
			{
				return new int[]
				{
					6,
					4
				};
			}
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x003F1A08 File Offset: 0x003EFE08
		public override void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(base.dicKey);
			base.changeAmount.Save(_writer);
			_writer.Write(this.speed);
			_writer.Write((int)this.easeType);
			_writer.Write((int)this.connection);
			this.aidInfo.Save(_writer, _version);
			_writer.Write(this.link);
		}

		// Token: 0x0600994B RID: 39243 RVA: 0x003F1A6C File Offset: 0x003EFE6C
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, false);
			this.speed = _reader.ReadSingle();
			this.easeType = (StudioTween.EaseType)_reader.ReadInt32();
			this.connection = (OIRoutePointInfo.Connection)_reader.ReadInt32();
			if (this.aidInfo == null)
			{
				this.aidInfo = new OIRoutePointAidInfo(_import ? Studio.GetNewIndex() : -1);
			}
			this.aidInfo.Load(_reader, _version, _import, true);
			this.link = _reader.ReadBoolean();
		}

		// Token: 0x0600994C RID: 39244 RVA: 0x003F1AE9 File Offset: 0x003EFEE9
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}

		// Token: 0x04007A49 RID: 31305
		public float speed = 2f;

		// Token: 0x04007A4A RID: 31306
		public StudioTween.EaseType easeType = StudioTween.EaseType.linear;

		// Token: 0x04007A4B RID: 31307
		public OIRoutePointInfo.Connection connection;

		// Token: 0x04007A4C RID: 31308
		public OIRoutePointAidInfo aidInfo;

		// Token: 0x04007A4D RID: 31309
		public bool link;

		// Token: 0x02001230 RID: 4656
		public enum Connection
		{
			// Token: 0x04007A4F RID: 31311
			Line,
			// Token: 0x04007A50 RID: 31312
			Curve
		}
	}
}
