using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

namespace Studio
{
	// Token: 0x0200121A RID: 4634
	public class OCIRoutePoint : ObjectCtrlInfo
	{
		// Token: 0x06009869 RID: 39017 RVA: 0x003EDA58 File Offset: 0x003EBE58
		public OCIRoutePoint(OCIRoute _route, OIRoutePointInfo _info, GameObject _obj, GuideObject _guide, TreeNodeObject _treeNode)
		{
			this.route = _route;
			this.objectInfo = _info;
			this.objectItem = _obj;
			this.guideObject = _guide;
			this.treeNodeObject = _treeNode;
			this.routePoint = _obj.GetComponent<RoutePointComponent>();
			this.isParentDelete = false;
			this._line = null;
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x0600986A RID: 39018 RVA: 0x003EDAB5 File Offset: 0x003EBEB5
		public OIRoutePointInfo routePointInfo
		{
			get
			{
				return this.objectInfo as OIRoutePointInfo;
			}
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x0600986B RID: 39019 RVA: 0x003EDAC2 File Offset: 0x003EBEC2
		// (set) Token: 0x0600986C RID: 39020 RVA: 0x003EDACA File Offset: 0x003EBECA
		public OCIRoute route { get; private set; }

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x0600986D RID: 39021 RVA: 0x003EDAD3 File Offset: 0x003EBED3
		// (set) Token: 0x0600986E RID: 39022 RVA: 0x003EDADB File Offset: 0x003EBEDB
		public RoutePointComponent routePoint { get; private set; }

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x0600986F RID: 39023 RVA: 0x003EDAE4 File Offset: 0x003EBEE4
		public string name
		{
			get
			{
				return this.routePointInfo.name;
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06009870 RID: 39024 RVA: 0x003EDAF4 File Offset: 0x003EBEF4
		// (set) Token: 0x06009871 RID: 39025 RVA: 0x003EDB30 File Offset: 0x003EBF30
		public int number
		{
			get
			{
				int num = -1;
				return (!int.TryParse(this.routePointInfo.name.Replace("ポイント", string.Empty), out num)) ? 0 : num;
			}
			set
			{
				this.routePointInfo.number = value;
				this.routePoint.textName = ((value != 0) ? value.ToString() : "S");
				this.treeNodeObject.textName = this.name;
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06009872 RID: 39026 RVA: 0x003EDB82 File Offset: 0x003EBF82
		public Vector3 position
		{
			get
			{
				return this.objectItem.transform.position;
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06009873 RID: 39027 RVA: 0x003EDB94 File Offset: 0x003EBF94
		public Transform[] transform
		{
			get
			{
				return new Transform[]
				{
					this.objectItem.transform,
					this.pointAidInfo.target
				};
			}
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06009874 RID: 39028 RVA: 0x003EDBB8 File Offset: 0x003EBFB8
		public List<Vector3> positions
		{
			get
			{
				List<Vector3> list = new List<Vector3>();
				list.Add(this.position);
				if (this.connection == OIRoutePointInfo.Connection.Curve)
				{
					list.Add(this.routePoint.objAid.transform.position);
				}
				return list;
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06009875 RID: 39029 RVA: 0x003EDBFF File Offset: 0x003EBFFF
		// (set) Token: 0x06009876 RID: 39030 RVA: 0x003EDC0C File Offset: 0x003EC00C
		public float speed
		{
			get
			{
				return this.routePointInfo.speed;
			}
			set
			{
				this.routePointInfo.speed = value;
			}
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06009877 RID: 39031 RVA: 0x003EDC1A File Offset: 0x003EC01A
		// (set) Token: 0x06009878 RID: 39032 RVA: 0x003EDC27 File Offset: 0x003EC027
		public StudioTween.EaseType easeType
		{
			get
			{
				return this.routePointInfo.easeType;
			}
			set
			{
				this.routePointInfo.easeType = value;
			}
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x06009879 RID: 39033 RVA: 0x003EDC35 File Offset: 0x003EC035
		// (set) Token: 0x0600987A RID: 39034 RVA: 0x003EDC44 File Offset: 0x003EC044
		public OIRoutePointInfo.Connection connection
		{
			get
			{
				return this.routePointInfo.connection;
			}
			set
			{
				this.routePointInfo.connection = value;
				if (value != OIRoutePointInfo.Connection.Line)
				{
					if (value == OIRoutePointInfo.Connection.Curve)
					{
						this.InitAidPos();
						this.pointAidInfo.active = true;
					}
				}
				else
				{
					this.pointAidInfo.active = false;
				}
			}
		}

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x0600987B RID: 39035 RVA: 0x003EDC97 File Offset: 0x003EC097
		// (set) Token: 0x0600987C RID: 39036 RVA: 0x003EDCA4 File Offset: 0x003EC0A4
		public bool link
		{
			get
			{
				return this.routePointInfo.link;
			}
			set
			{
				this.routePointInfo.link = value;
			}
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x0600987D RID: 39037 RVA: 0x003EDCB2 File Offset: 0x003EC0B2
		public bool isLink
		{
			get
			{
				return this.routePointInfo.link && this.routePointInfo.connection == OIRoutePointInfo.Connection.Curve;
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x0600987E RID: 39038 RVA: 0x003EDCD5 File Offset: 0x003EC0D5
		// (set) Token: 0x0600987F RID: 39039 RVA: 0x003EDCDD File Offset: 0x003EC0DD
		public bool isParentDelete { get; set; }

		// Token: 0x17002066 RID: 8294
		// (set) Token: 0x06009880 RID: 39040 RVA: 0x003EDCE6 File Offset: 0x003EC0E6
		public bool visible
		{
			set
			{
				this.routePoint.visible = value;
				this.lineActive = value;
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x06009881 RID: 39041 RVA: 0x003EDCFB File Offset: 0x003EC0FB
		public VectorLine line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17002068 RID: 8296
		// (set) Token: 0x06009882 RID: 39042 RVA: 0x003EDD03 File Offset: 0x003EC103
		public bool lineActive
		{
			set
			{
				if (this._line != null)
				{
					this._line.active = value;
				}
			}
		}

		// Token: 0x06009883 RID: 39043 RVA: 0x003EDD1C File Offset: 0x003EC11C
		public override void OnDelete()
		{
			if (!this.isParentDelete)
			{
				this.route.DeletePoint(this);
			}
			if (this._line != null)
			{
				VectorLine.Destroy(ref this._line);
			}
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			Singleton<GuideObjectManager>.Instance.Delete(this.pointAidInfo.guideObject, true);
			UnityEngine.Object.Destroy(this.objectItem);
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x06009884 RID: 39044 RVA: 0x003EDD94 File Offset: 0x003EC194
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009885 RID: 39045 RVA: 0x003EDD96 File Offset: 0x003EC196
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009886 RID: 39046 RVA: 0x003EDD98 File Offset: 0x003EC198
		public override void OnDetach()
		{
		}

		// Token: 0x06009887 RID: 39047 RVA: 0x003EDD9C File Offset: 0x003EC19C
		public override void OnSelect(bool _select)
		{
			int layer = LayerMask.NameToLayer((!_select) ? "Studio/Select" : "Studio/Col");
			this.pointAidInfo.layer = layer;
		}

		// Token: 0x06009888 RID: 39048 RVA: 0x003EDDD0 File Offset: 0x003EC1D0
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009889 RID: 39049 RVA: 0x003EDDD2 File Offset: 0x003EC1D2
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
		}

		// Token: 0x0600988A RID: 39050 RVA: 0x003EDDDA File Offset: 0x003EC1DA
		public override void OnVisible(bool _visible)
		{
			this.routePoint.visible = (_visible && this.route.visibleLine);
		}

		// Token: 0x17002069 RID: 8297
		public override ObjectCtrlInfo this[int _idx]
		{
			get
			{
				return (_idx != 0) ? this.route : this;
			}
		}

		// Token: 0x0600988C RID: 39052 RVA: 0x003EDE14 File Offset: 0x003EC214
		public void UpdateLine(Vector3 _pos)
		{
			List<Vector3> positions = this.positions;
			positions.Add(_pos);
			if (this._line == null)
			{
				OIRoutePointInfo.Connection connection = this.connection;
				if (connection != OIRoutePointInfo.Connection.Line)
				{
					if (connection == OIRoutePointInfo.Connection.Curve)
					{
						this._line = new VectorLine("Spline", new List<Vector3>(this.segments + 1), Studio.optionSystem.routeLineWidth, LineType.Continuous);
						this._line.MakeSpline(positions.ToArray(), this.segments, false);
					}
				}
				else
				{
					this._line = new VectorLine("Line", positions, Studio.optionSystem.routeLineWidth, LineType.Continuous);
				}
				this._line.joins = Joins.Weld;
				this._line.color = this.route.routeInfo.color;
				this._line.continuousTexture = false;
				this._line.Draw3DAuto();
				this._line.layer = LayerMask.NameToLayer("Studio/Camera");
				this._line.active = this.route.routeInfo.visibleLine;
			}
			else
			{
				OIRoutePointInfo.Connection connection2 = this.connection;
				if (connection2 != OIRoutePointInfo.Connection.Line)
				{
					if (connection2 == OIRoutePointInfo.Connection.Curve)
					{
						this._line.MakeSpline(positions.ToArray(), this.segments, false);
					}
				}
				else
				{
					List<Vector3> points = this._line.points3;
					for (int i = 0; i < positions.Count; i++)
					{
						points[i] = positions[i];
					}
					this._line.points3 = points;
				}
				this._line.lineWidth = Studio.optionSystem.routeLineWidth;
				this._line.Draw3DAuto();
				this._line.active = this.route.routeInfo.visibleLine;
			}
		}

		// Token: 0x0600988D RID: 39053 RVA: 0x003EDFEA File Offset: 0x003EC3EA
		public void DeleteLine()
		{
			if (this._line != null)
			{
				VectorLine.Destroy(ref this._line);
			}
		}

		// Token: 0x0600988E RID: 39054 RVA: 0x003EE004 File Offset: 0x003EC404
		private void InitAidPos()
		{
			if (this.pointAidInfo.aidInfo.isInit)
			{
				return;
			}
			this.DeleteLine();
			int num = this.route.listPoint.IndexOf(this);
			Vector3 b = (num + 1 < this.route.listPoint.Count) ? this.route.listPoint[num + 1].position : this.route.listPoint[0].position;
			Vector3 position = Vector3.Lerp(this.position, b, 0.5f);
			this.pointAidInfo.aidInfo.changeAmount.pos = this.objectItem.transform.InverseTransformPoint(position);
			this.pointAidInfo.aidInfo.isInit = true;
		}

		// Token: 0x0600988F RID: 39055 RVA: 0x003EE0D4 File Offset: 0x003EC4D4
		public void SetEnable(bool _value, bool _first = false)
		{
			this.guideObject.SetEnable((!_first) ? ((!_value) ? 0 : 1) : -1, (!_first) ? -1 : ((!_value) ? 0 : 1), -1);
			this.pointAidInfo.guideObject.SetEnable((!_value) ? 0 : 1, -1, -1);
		}

		// Token: 0x040079B8 RID: 31160
		public GameObject objectItem;

		// Token: 0x040079B9 RID: 31161
		public OCIRoutePoint.PointAidInfo pointAidInfo;

		// Token: 0x040079BB RID: 31163
		private VectorLine _line;

		// Token: 0x040079BC RID: 31164
		private int segments = 160;

		// Token: 0x0200121B RID: 4635
		public class PointAidInfo
		{
			// Token: 0x06009890 RID: 39056 RVA: 0x003EE13E File Offset: 0x003EC53E
			public PointAidInfo(GuideObject _guideObject, OIRoutePointAidInfo _aidInfo)
			{
				this.guideObject = _guideObject;
				this.aidInfo = _aidInfo;
			}

			// Token: 0x1700206A RID: 8298
			// (get) Token: 0x06009891 RID: 39057 RVA: 0x003EE154 File Offset: 0x003EC554
			// (set) Token: 0x06009892 RID: 39058 RVA: 0x003EE15C File Offset: 0x003EC55C
			public GuideObject guideObject { get; private set; }

			// Token: 0x1700206B RID: 8299
			// (get) Token: 0x06009893 RID: 39059 RVA: 0x003EE165 File Offset: 0x003EC565
			// (set) Token: 0x06009894 RID: 39060 RVA: 0x003EE16D File Offset: 0x003EC56D
			public OIRoutePointAidInfo aidInfo { get; private set; }

			// Token: 0x1700206C RID: 8300
			// (get) Token: 0x06009895 RID: 39061 RVA: 0x003EE176 File Offset: 0x003EC576
			public GameObject gameObject
			{
				get
				{
					if (this.m_GameObject == null)
					{
						this.m_GameObject = this.guideObject.gameObject;
					}
					return this.m_GameObject;
				}
			}

			// Token: 0x1700206D RID: 8301
			// (get) Token: 0x06009896 RID: 39062 RVA: 0x003EE1A0 File Offset: 0x003EC5A0
			public Transform target
			{
				get
				{
					return this.guideObject.transformTarget;
				}
			}

			// Token: 0x1700206E RID: 8302
			// (get) Token: 0x06009897 RID: 39063 RVA: 0x003EE1AD File Offset: 0x003EC5AD
			public Vector3 position
			{
				get
				{
					return this.gameObject.transform.position;
				}
			}

			// Token: 0x1700206F RID: 8303
			// (get) Token: 0x06009898 RID: 39064 RVA: 0x003EE1BF File Offset: 0x003EC5BF
			public Transform transform
			{
				get
				{
					return this.gameObject.transform;
				}
			}

			// Token: 0x17002070 RID: 8304
			// (get) Token: 0x06009899 RID: 39065 RVA: 0x003EE1CC File Offset: 0x003EC5CC
			// (set) Token: 0x0600989A RID: 39066 RVA: 0x003EE1F0 File Offset: 0x003EC5F0
			public bool active
			{
				get
				{
					return this.gameObject != null && this.gameObject.activeSelf;
				}
				set
				{
					if (this.gameObject)
					{
						this.gameObject.SetActive(value);
					}
				}
			}

			// Token: 0x17002071 RID: 8305
			// (set) Token: 0x0600989B RID: 39067 RVA: 0x003EE20E File Offset: 0x003EC60E
			public int layer
			{
				set
				{
					this.guideObject.SetLayer(this.gameObject, value);
				}
			}

			// Token: 0x040079BF RID: 31167
			private GameObject m_GameObject;
		}
	}
}
