using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UniRx;
using UnityEngine;
using Vectrosity;

namespace Studio
{
	// Token: 0x0200121C RID: 4636
	public class OCIRoute : ObjectCtrlInfo
	{
		// Token: 0x0600989C RID: 39068 RVA: 0x003EE222 File Offset: 0x003EC622
		public OCIRoute()
		{
			this.listPoint = new List<OCIRoutePoint>();
		}

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x0600989D RID: 39069 RVA: 0x003EE240 File Offset: 0x003EC640
		public OIRouteInfo routeInfo
		{
			get
			{
				return this.objectInfo as OIRouteInfo;
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x0600989E RID: 39070 RVA: 0x003EE24D File Offset: 0x003EC64D
		// (set) Token: 0x0600989F RID: 39071 RVA: 0x003EE25A File Offset: 0x003EC65A
		public string name
		{
			get
			{
				return this.routeInfo.name;
			}
			set
			{
				this.routeInfo.name = value;
				this.treeNodeObject.textName = value;
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x060098A0 RID: 39072 RVA: 0x003EE274 File Offset: 0x003EC674
		// (set) Token: 0x060098A1 RID: 39073 RVA: 0x003EE27C File Offset: 0x003EC67C
		public List<OCIRoutePoint> listPoint { get; private set; }

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x060098A2 RID: 39074 RVA: 0x003EE285 File Offset: 0x003EC685
		public bool isPlay
		{
			get
			{
				return this.routeInfo.active;
			}
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x060098A3 RID: 39075 RVA: 0x003EE294 File Offset: 0x003EC694
		public bool isEnd
		{
			get
			{
				return this.routeInfo.route.Count > 1 && (this.nowIndex >= this.listPoint.Count - 1 & !this.routeInfo.active);
			}
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x060098A4 RID: 39076 RVA: 0x003EE2E4 File Offset: 0x003EC6E4
		// (set) Token: 0x060098A5 RID: 39077 RVA: 0x003EE2F1 File Offset: 0x003EC6F1
		public bool visibleLine
		{
			get
			{
				return this.routeInfo.visibleLine;
			}
			set
			{
				this.routeInfo.visibleLine = value;
				this.SetVisible(value);
			}
		}

		// Token: 0x060098A6 RID: 39078 RVA: 0x003EE308 File Offset: 0x003EC708
		public override void OnDelete()
		{
			if (this.line != null)
			{
				VectorLine.Destroy(ref this.line);
			}
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			UnityEngine.Object.Destroy(this.objectItem);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x060098A7 RID: 39079 RVA: 0x003EE36C File Offset: 0x003EC76C
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.routeInfo.child.Contains(_child.objectInfo))
			{
				this.routeInfo.child.Add(_child.objectInfo);
			}
			_child.guideObject.transformTarget.SetParent(this.childRoot);
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_child.objectInfo.changeAmount.pos = _child.guideObject.transformTarget.localPosition;
			_child.objectInfo.changeAmount.rot = _child.guideObject.transformTarget.localEulerAngles;
			_child.parentInfo = this;
		}

		// Token: 0x060098A8 RID: 39080 RVA: 0x003EE458 File Offset: 0x003EC858
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.routeInfo.child.Contains(_child.objectInfo))
			{
				this.routeInfo.child.Add(_child.objectInfo);
			}
			_child.guideObject.transformTarget.SetParent(this.childRoot, false);
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_child.objectInfo.changeAmount.OnChange();
			_child.parentInfo = this;
		}

		// Token: 0x060098A9 RID: 39081 RVA: 0x003EE518 File Offset: 0x003EC918
		public override void OnDetach()
		{
			this.parentInfo.OnDetachChild(this);
			this.guideObject.parent = null;
			Studio.AddInfo(this.objectInfo, this);
			this.objectItem.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			this.objectInfo.changeAmount.pos = this.objectItem.transform.localPosition;
			this.objectInfo.changeAmount.rot = this.objectItem.transform.localEulerAngles;
			this.guideObject.mode = GuideObject.Mode.Local;
			this.guideObject.moveCalc = GuideMove.MoveCalc.TYPE1;
			this.treeNodeObject.ResetVisible();
		}

		// Token: 0x060098AA RID: 39082 RVA: 0x003EE5CB File Offset: 0x003EC9CB
		public override void OnSelect(bool _select)
		{
		}

		// Token: 0x060098AB RID: 39083 RVA: 0x003EE5CD File Offset: 0x003EC9CD
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
			if (!this.routeInfo.child.Remove(_child.objectInfo))
			{
			}
			_child.parentInfo = null;
		}

		// Token: 0x060098AC RID: 39084 RVA: 0x003EE5F1 File Offset: 0x003EC9F1
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
		}

		// Token: 0x060098AD RID: 39085 RVA: 0x003EE5F9 File Offset: 0x003EC9F9
		public override void OnVisible(bool _visible)
		{
			if (this.line != null)
			{
				this.line.active = (_visible && this.visibleLine);
			}
		}

		// Token: 0x060098AE RID: 39086 RVA: 0x003EE624 File Offset: 0x003ECA24
		public void OnDeleteNode()
		{
			foreach (OCIRoutePoint ociroutePoint in this.listPoint)
			{
				ociroutePoint.isParentDelete = true;
			}
		}

		// Token: 0x060098AF RID: 39087 RVA: 0x003EE680 File Offset: 0x003ECA80
		private void SetVisible(bool _flag)
		{
			bool flag = _flag & this.treeNodeObject.visible;
			if (this.line != null)
			{
				this.line.active = flag;
			}
			foreach (OCIRoutePoint ociroutePoint in this.listPoint)
			{
				ociroutePoint.visible = flag;
			}
		}

		// Token: 0x060098B0 RID: 39088 RVA: 0x003EE704 File Offset: 0x003ECB04
		public bool CheckParentLoop(TreeNodeObject _parent)
		{
			if (_parent == null)
			{
				return true;
			}
			ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(_parent);
			if (ctrlInfo != null)
			{
				int kind = ctrlInfo.kind;
				if (kind != 1)
				{
					if (kind == 4)
					{
						return false;
					}
				}
				else
				{
					OCIItem ociitem = ctrlInfo as OCIItem;
					if (ociitem.itemInfo.group == 10 || ociitem.itemInfo.group == 15)
					{
						return false;
					}
				}
			}
			return this.CheckParentLoop(_parent.parent);
		}

		// Token: 0x060098B1 RID: 39089 RVA: 0x003EE788 File Offset: 0x003ECB88
		public OCIRoutePoint AddPoint()
		{
			if (Studio.optionSystem.routePointLimit && this.routeInfo.route.Count > 10)
			{
				return null;
			}
			OCIRoutePoint result = AddObjectRoute.AddPoint(this);
			this.UpdateLine();
			return result;
		}

		// Token: 0x060098B2 RID: 39090 RVA: 0x003EE7CC File Offset: 0x003ECBCC
		public void DeletePoint(OCIRoutePoint _routePoint)
		{
			this.Stop(true);
			this.treeNodeObject.RemoveChild(_routePoint.treeNodeObject, true);
			this.listPoint.Remove(_routePoint);
			this.routeInfo.route.Remove(_routePoint.routePointInfo);
			this.UpdateNumber();
			this.UpdateLine();
		}

		// Token: 0x060098B3 RID: 39091 RVA: 0x003EE824 File Offset: 0x003ECC24
		public bool Play()
		{
			if (this.routeInfo.route.Count <= 1)
			{
				return false;
			}
			this.Stop(false);
			Transform transform = this.listPoint[0].objectItem.transform;
			this.childRoot.SetPositionAndRotation(transform.position, transform.rotation);
			int i = 0;
			StudioTween studioTween = this.SetPath(null, ref i);
			while (i < this.listPoint.Count)
			{
				this.SetPath(studioTween, ref i);
				if (!this.routeInfo.loop && i == this.listPoint.Count - 1)
				{
					break;
				}
			}
			studioTween.loopType = ((!this.routeInfo.loop) ? StudioTween.LoopType.none : StudioTween.LoopType.loop);
			if (!this.routeInfo.loop)
			{
				StudioTween studioTween2 = studioTween;
				studioTween2.onComplete = (StudioTween.CompleteFunction)Delegate.Combine(studioTween2.onComplete, new StudioTween.CompleteFunction(delegate()
				{
					this.routeInfo.active = false;
					Singleton<Studio>.Instance.routeControl.SetState(this.objectInfo, RouteNode.State.End);
					return true;
				}));
			}
			this.routeInfo.active = true;
			return true;
		}

		// Token: 0x060098B4 RID: 39092 RVA: 0x003EE930 File Offset: 0x003ECD30
		private bool Move()
		{
			StudioTween.Stop(this.childRoot.gameObject);
			if (this.routeInfo.loop)
			{
				if (this.nowIndex >= this.listPoint.Count)
				{
					this.nowIndex = 0;
				}
			}
			else if (this.nowIndex >= this.listPoint.Count - 1)
			{
				this.routeInfo.active = false;
				Singleton<Studio>.Instance.routeControl.SetState(this.objectInfo, RouteNode.State.End);
				return false;
			}
			Hashtable hashtable = new Hashtable();
			OIRoutePointInfo.Connection connection = this.listPoint[this.nowIndex].connection;
			if (connection != OIRoutePointInfo.Connection.Line)
			{
				if (connection == OIRoutePointInfo.Connection.Curve)
				{
					List<Transform> list = this.listPoint[this.nowIndex].transform.ToList<Transform>();
					if (this.nowIndex + 1 >= this.listPoint.Count)
					{
						list.Add(this.listPoint[0].objectItem.transform);
					}
					else
					{
						list.Add(this.listPoint[this.nowIndex + 1].objectItem.transform);
					}
					hashtable.Add("path", list.ToArray());
				}
			}
			else
			{
				Transform[] value;
				if (this.nowIndex == this.listPoint.Count - 1)
				{
					value = new Transform[]
					{
						this.listPoint[this.listPoint.Count - 1].objectItem.transform,
						this.listPoint[0].objectItem.transform
					};
				}
				else
				{
					value = (from v in this.listPoint.Skip(this.nowIndex).Take(2)
					select v.objectItem.transform).ToArray<Transform>();
				}
				hashtable.Add("path", value);
			}
			hashtable.Add("speed", this.listPoint[this.nowIndex].routePointInfo.speed * 10f);
			hashtable.Add("easetype", this.listPoint[this.nowIndex].routePointInfo.easeType);
			hashtable.Add("looptype", StudioTween.LoopType.none);
			OIRouteInfo.Orient orient = this.routeInfo.orient;
			if (orient != OIRouteInfo.Orient.Y)
			{
				if (orient == OIRouteInfo.Orient.XY)
				{
					hashtable.Add("orienttopath", true);
				}
			}
			else
			{
				hashtable.Add("orienttopath", true);
				hashtable.Add("axis", "y");
			}
			StudioTween studioTween = StudioTween.MoveTo(this.childRoot.gameObject, hashtable);
			StudioTween studioTween2 = studioTween;
			studioTween2.onComplete = (StudioTween.CompleteFunction)Delegate.Combine(studioTween2.onComplete, new StudioTween.CompleteFunction(this.Move));
			this.nowIndex++;
			return true;
		}

		// Token: 0x060098B5 RID: 39093 RVA: 0x003EEC3C File Offset: 0x003ED03C
		private StudioTween SetPath(StudioTween _tween, ref int _index)
		{
			if (!this.routeInfo.loop && _index == this.listPoint.Count - 1)
			{
				return _tween;
			}
			int num = _index;
			_index = num + 1;
			Hashtable hashtable = new Hashtable();
			OIRoutePointInfo.Connection connection = this.listPoint[num].connection;
			if (connection != OIRoutePointInfo.Connection.Line)
			{
				if (connection == OIRoutePointInfo.Connection.Curve)
				{
					List<Transform> list = this.listPoint[num].transform.ToList<Transform>();
					while (_index < this.listPoint.Count)
					{
						if (!this.routeInfo.loop && _index == this.listPoint.Count - 1)
						{
							break;
						}
						if (!this.listPoint[_index].isLink)
						{
							break;
						}
						list.AddRange(this.listPoint[_index].transform);
						_index++;
					}
					if (_index >= this.listPoint.Count)
					{
						list.Add(this.listPoint[0].objectItem.transform);
					}
					else
					{
						list.Add(this.listPoint[_index].objectItem.transform);
					}
					hashtable.Add("path", list.ToArray());
				}
			}
			else
			{
				Transform[] value;
				if (num == this.listPoint.Count - 1)
				{
					value = new Transform[]
					{
						this.listPoint[this.listPoint.Count - 1].objectItem.transform,
						this.listPoint[0].objectItem.transform
					};
				}
				else
				{
					value = (from v in this.listPoint.Skip(num).Take(2)
					select v.objectItem.transform).ToArray<Transform>();
				}
				hashtable.Add("path", value);
			}
			hashtable.Add("speed", this.listPoint[num].routePointInfo.speed * 10f);
			hashtable.Add("easetype", this.listPoint[num].routePointInfo.easeType);
			OIRouteInfo.Orient orient = this.routeInfo.orient;
			if (orient != OIRouteInfo.Orient.Y)
			{
				if (orient == OIRouteInfo.Orient.XY)
				{
					hashtable.Add("orienttopath", true);
				}
			}
			else
			{
				hashtable.Add("orienttopath", true);
				hashtable.Add("axis", "y");
			}
			if (_tween != null)
			{
				_tween.MoveTo(hashtable);
				return _tween;
			}
			return StudioTween.MoveTo(this.childRoot.gameObject, hashtable);
		}

		// Token: 0x060098B6 RID: 39094 RVA: 0x003EEF28 File Offset: 0x003ED328
		public void Stop(bool _copy = true)
		{
			StudioTween.Stop(this.childRoot.gameObject);
			if (this.disposable != null)
			{
				this.disposable.Dispose();
				this.disposable = null;
			}
			if (!this.listPoint.IsNullOrEmpty<OCIRoutePoint>() && _copy)
			{
				this.disposable = new SingleAssignmentDisposable();
				this.disposable.Disposable = Observable.EveryLateUpdate().Subscribe(delegate(long _)
				{
					Transform transform = this.listPoint[0].objectItem.transform;
					this.childRoot.SetPositionAndRotation(transform.position, transform.rotation);
				}).AddTo(this.childRoot);
			}
			this.nowIndex = 0;
			this.routeInfo.active = false;
		}

		// Token: 0x060098B7 RID: 39095 RVA: 0x003EEFC4 File Offset: 0x003ED3C4
		public void UpdateLine()
		{
			if (this.routeInfo.route.Count <= 1)
			{
				this.DeleteLine();
				return;
			}
			bool flag = this.line == null;
			int i = 0;
			int num = 0;
			while (i < this.listPoint.Count)
			{
				OIRoutePointInfo.Connection connection = this.listPoint[i].connection;
				if (connection != OIRoutePointInfo.Connection.Line)
				{
					if (connection == OIRoutePointInfo.Connection.Curve)
					{
						if (!this.routeInfo.loop && i == this.listPoint.Count - 1)
						{
							num += ((this.routeInfo.loop || i != this.listPoint.Count - 1) ? this.segments : 0) + 1;
							i++;
						}
						else
						{
							int num2 = 1;
							for (i++; i < this.listPoint.Count; i++)
							{
								if (!this.routeInfo.loop && i == this.listPoint.Count - 1)
								{
									break;
								}
								if (!this.listPoint[i].isLink)
								{
									break;
								}
								num2++;
							}
							num += this.segments * num2 + 1;
						}
					}
				}
				else
				{
					num++;
					i++;
					if (i >= this.listPoint.Count && this.routeInfo.loop)
					{
						num++;
					}
				}
			}
			if (!flag && this.line.points3.Count != num)
			{
				VectorLine.Destroy(ref this.line);
				flag = true;
			}
			if (flag)
			{
				this.line = new VectorLine("Spline", new List<Vector3>(num), Studio.optionSystem.routeLineWidth, LineType.Continuous);
				this.objLine = GameObject.Find("Spline");
				if (this.objLine)
				{
					this.objLine.name = "Spline " + this.routeInfo.name;
					this.objLine.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
				}
			}
			i = 0;
			int num3 = 0;
			while (i < this.listPoint.Count)
			{
				OIRoutePointInfo.Connection connection2 = this.listPoint[i].connection;
				if (connection2 != OIRoutePointInfo.Connection.Line)
				{
					if (connection2 == OIRoutePointInfo.Connection.Curve)
					{
						if (!this.routeInfo.loop && i == this.listPoint.Count - 1)
						{
							List<Vector3> points = this.line.points3;
							points[num3] = this.listPoint[i].position;
							i++;
							num3++;
							this.line.points3 = points;
						}
						else
						{
							List<Transform> list = this.listPoint[i].transform.ToList<Transform>();
							int j = i + 1;
							int num4 = 1;
							while (j < this.listPoint.Count)
							{
								if (!this.routeInfo.loop && j == this.listPoint.Count - 1)
								{
									break;
								}
								if (!this.listPoint[j].isLink)
								{
									break;
								}
								list.AddRange(this.listPoint[j].transform);
								num4++;
								j++;
							}
							bool flag2 = i == 0 && j >= this.listPoint.Count && this.routeInfo.loop;
							if (j >= this.listPoint.Count)
							{
								if (!flag2)
								{
									list.Add(this.listPoint[0].objectItem.transform);
								}
							}
							else
							{
								list.Add(this.listPoint[j].objectItem.transform);
							}
							this.line.MakeSpline((from v in list
							select v.position).ToArray<Vector3>(), this.segments * num4, num3, flag2);
							i = j;
							num3 += this.segments * num4 + 1;
						}
					}
				}
				else
				{
					List<Vector3> points2 = this.line.points3;
					points2[num3] = this.listPoint[i].position;
					i++;
					num3++;
					if (i >= this.listPoint.Count && this.routeInfo.loop)
					{
						points2[num3] = this.listPoint[0].position;
					}
					this.line.points3 = points2;
				}
			}
			this.line.joins = Joins.Weld;
			this.line.color = this.routeInfo.color;
			this.line.continuousTexture = false;
			this.line.lineWidth = Studio.optionSystem.routeLineWidth;
			this.line.Draw3DAuto();
			this.line.layer = LayerMask.NameToLayer("Studio/Route");
			if (flag)
			{
				this.line.active = this.routeInfo.visibleLine;
				Renderer component = this.objLine.GetComponent<Renderer>();
				if (component)
				{
					component.material.renderQueue = 2900;
				}
			}
		}

		// Token: 0x060098B8 RID: 39096 RVA: 0x003EF550 File Offset: 0x003ED950
		public void ForceUpdateLine()
		{
			this.DeleteLine();
			this.UpdateLine();
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x003EF55E File Offset: 0x003ED95E
		public void DeleteLine()
		{
			if (this.line != null)
			{
				VectorLine.Destroy(ref this.line);
			}
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x003EF576 File Offset: 0x003ED976
		public void SetLineColor(Color _color)
		{
			if (this.line == null)
			{
				return;
			}
			this.line.SetColor(_color);
		}

		// Token: 0x060098BB RID: 39099 RVA: 0x003EF598 File Offset: 0x003ED998
		public void UpdateNumber()
		{
			for (int i = 0; i < this.listPoint.Count; i++)
			{
				this.listPoint[i].number = i;
			}
		}

		// Token: 0x040079C0 RID: 31168
		public const int limitNum = 10;

		// Token: 0x040079C1 RID: 31169
		public GameObject objectItem;

		// Token: 0x040079C2 RID: 31170
		public Transform childRoot;

		// Token: 0x040079C3 RID: 31171
		public TreeNodeObject childNodeRoot;

		// Token: 0x040079C5 RID: 31173
		public RouteComponent routeComponent;

		// Token: 0x040079C6 RID: 31174
		private int nowIndex;

		// Token: 0x040079C7 RID: 31175
		private SingleAssignmentDisposable disposable;

		// Token: 0x040079C8 RID: 31176
		private VectorLine line;

		// Token: 0x040079C9 RID: 31177
		private int segments = 160;

		// Token: 0x040079CA RID: 31178
		private GameObject objLine;
	}
}
