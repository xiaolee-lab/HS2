using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001231 RID: 4657
	public class OIRouteInfo : ObjectInfo
	{
		// Token: 0x0600994D RID: 39245 RVA: 0x003F1AF8 File Offset: 0x003EFEF8
		public OIRouteInfo(int _key) : base(_key)
		{
			this.name = "ルート";
			base.treeState = TreeNodeObject.TreeState.Open;
			this.child = new List<ObjectInfo>();
			this.route = new List<OIRoutePointInfo>();
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x0600994E RID: 39246 RVA: 0x003F1B58 File Offset: 0x003EFF58
		public override int kind
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x0600994F RID: 39247 RVA: 0x003F1B5B File Offset: 0x003EFF5B
		// (set) Token: 0x06009950 RID: 39248 RVA: 0x003F1B63 File Offset: 0x003EFF63
		public List<ObjectInfo> child { get; private set; }

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x06009951 RID: 39249 RVA: 0x003F1B6C File Offset: 0x003EFF6C
		// (set) Token: 0x06009952 RID: 39250 RVA: 0x003F1B74 File Offset: 0x003EFF74
		public List<OIRoutePointInfo> route { get; private set; }

		// Token: 0x06009953 RID: 39251 RVA: 0x003F1B80 File Offset: 0x003EFF80
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.name);
			int count = this.child.Count;
			_writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				this.child[i].Save(_writer, _version);
			}
			count = this.route.Count;
			_writer.Write(count);
			for (int j = 0; j < count; j++)
			{
				this.route[j].Save(_writer, _version);
			}
			_writer.Write(this.active);
			_writer.Write(this.loop);
			_writer.Write(this.visibleLine);
			_writer.Write((int)this.orient);
			_writer.Write(JsonUtility.ToJson(this.color));
		}

		// Token: 0x06009954 RID: 39252 RVA: 0x003F1C58 File Offset: 0x003F0058
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.name = _reader.ReadString();
			ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				OIRoutePointInfo oiroutePointInfo = new OIRoutePointInfo(-1);
				oiroutePointInfo.Load(_reader, _version, false, true);
				this.route.Add(oiroutePointInfo);
			}
			this.active = _reader.ReadBoolean();
			this.loop = _reader.ReadBoolean();
			this.visibleLine = _reader.ReadBoolean();
			this.orient = (OIRouteInfo.Orient)_reader.ReadInt32();
			this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
		}

		// Token: 0x06009955 RID: 39253 RVA: 0x003F1D00 File Offset: 0x003F0100
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
			foreach (ObjectInfo objectInfo in this.child)
			{
				objectInfo.DeleteKey();
			}
			foreach (OIRoutePointInfo oiroutePointInfo in this.route)
			{
				oiroutePointInfo.DeleteKey();
			}
		}

		// Token: 0x04007A51 RID: 31313
		public string name = string.Empty;

		// Token: 0x04007A54 RID: 31316
		public bool active;

		// Token: 0x04007A55 RID: 31317
		public bool loop = true;

		// Token: 0x04007A56 RID: 31318
		public bool visibleLine = true;

		// Token: 0x04007A57 RID: 31319
		public OIRouteInfo.Orient orient;

		// Token: 0x04007A58 RID: 31320
		public Color color = Color.blue;

		// Token: 0x02001232 RID: 4658
		public enum Orient
		{
			// Token: 0x04007A5A RID: 31322
			None,
			// Token: 0x04007A5B RID: 31323
			XY,
			// Token: 0x04007A5C RID: 31324
			Y
		}

		// Token: 0x02001233 RID: 4659
		public enum Connection
		{
			// Token: 0x04007A5E RID: 31326
			Line,
			// Token: 0x04007A5F RID: 31327
			Curve
		}
	}
}
