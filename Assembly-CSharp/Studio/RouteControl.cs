using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200133C RID: 4924
	public class RouteControl : MonoBehaviour
	{
		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x0600A4DB RID: 42203 RVA: 0x00433936 File Offset: 0x00431D36
		// (set) Token: 0x0600A4DC RID: 42204 RVA: 0x00433943 File Offset: 0x00431D43
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x0600A4DD RID: 42205 RVA: 0x00433954 File Offset: 0x00431D54
		public void Init()
		{
			int childCount = this.nodeRoot.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(this.nodeRoot.GetChild(i).gameObject);
			}
			this.nodeRoot.DetachChildren();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode.Clear();
			this.listInfo = ObjectInfoAssist.Find(4);
			for (int j = 0; j < this.listInfo.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				if (!gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
				gameObject.transform.SetParent(this.nodeRoot, false);
				RouteNode component = gameObject.GetComponent<RouteNode>();
				OIRouteInfo oirouteInfo = this.listInfo[j] as OIRouteInfo;
				component.spritePlay = this.spritePlay;
				component.text = oirouteInfo.name;
				int no = j;
				component.buttonSelect.onClick.AddListener(delegate()
				{
					this.OnSelect(no);
				});
				component.buttonPlay.onClick.AddListener(delegate()
				{
					this.OnPlay(no);
				});
				OCIRoute ociroute = Studio.GetCtrlInfo(this.listInfo[j].dicKey) as OCIRoute;
				component.state = ((!ociroute.isEnd) ? ((!ociroute.isPlay) ? RouteNode.State.Stop : RouteNode.State.Play) : RouteNode.State.End);
				this.dicNode.Add(this.listInfo[j], component);
			}
		}

		// Token: 0x0600A4DE RID: 42206 RVA: 0x00433B00 File Offset: 0x00431F00
		public void ReflectOption()
		{
			if (this.listInfo.IsNullOrEmpty<ObjectInfo>())
			{
				this.listInfo = ObjectInfoAssist.Find(4);
			}
			foreach (OCIRoute ociroute in from i in this.listInfo
			select Studio.GetCtrlInfo(i.dicKey) as OCIRoute)
			{
				ociroute.UpdateLine();
			}
		}

		// Token: 0x0600A4DF RID: 42207 RVA: 0x00433B98 File Offset: 0x00431F98
		public void SetState(ObjectInfo _info, RouteNode.State _state)
		{
			if (this.dicNode == null)
			{
				return;
			}
			RouteNode routeNode = null;
			if (this.dicNode.TryGetValue(_info, out routeNode))
			{
				routeNode.state = _state;
			}
		}

		// Token: 0x0600A4E0 RID: 42208 RVA: 0x00433BCD File Offset: 0x00431FCD
		private void OnSelect(int _idx)
		{
			Singleton<Studio>.Instance.treeNodeCtrl.SelectSingle(Studio.GetCtrlInfo(this.listInfo[_idx].dicKey).treeNodeObject, false);
		}

		// Token: 0x0600A4E1 RID: 42209 RVA: 0x00433BFC File Offset: 0x00431FFC
		private void OnPlay(int _idx)
		{
			OCIRoute ociroute = Studio.GetCtrlInfo(this.listInfo[_idx].dicKey) as OCIRoute;
			if (ociroute.isPlay)
			{
				ociroute.Stop(true);
				this.dicNode[this.listInfo[_idx]].state = RouteNode.State.Stop;
			}
			else if (ociroute.Play())
			{
				this.dicNode[this.listInfo[_idx]].state = RouteNode.State.Play;
			}
			this.mpRouteCtrl.UpdateInteractable(ociroute);
			this.mpRoutePointCtrl.UpdateInteractable(ociroute);
		}

		// Token: 0x0600A4E2 RID: 42210 RVA: 0x00433C9C File Offset: 0x0043209C
		private void OnClickALL()
		{
			foreach (ObjectInfo objectInfo in this.listInfo)
			{
				OCIRoute ociroute = Studio.GetCtrlInfo(objectInfo.dicKey) as OCIRoute;
				if (!ociroute.isPlay)
				{
					if (ociroute.Play())
					{
						this.dicNode[objectInfo].state = RouteNode.State.Play;
						this.mpRouteCtrl.UpdateInteractable(ociroute);
						this.mpRoutePointCtrl.UpdateInteractable(ociroute);
					}
				}
			}
		}

		// Token: 0x0600A4E3 RID: 42211 RVA: 0x00433D4C File Offset: 0x0043214C
		private void OnClickReAll()
		{
			foreach (ObjectInfo objectInfo in this.listInfo)
			{
				OCIRoute ociroute = Studio.GetCtrlInfo(objectInfo.dicKey) as OCIRoute;
				if (ociroute.Play())
				{
					this.dicNode[objectInfo].state = RouteNode.State.Play;
					this.mpRouteCtrl.UpdateInteractable(ociroute);
					this.mpRoutePointCtrl.UpdateInteractable(ociroute);
				}
			}
		}

		// Token: 0x0600A4E4 RID: 42212 RVA: 0x00433DEC File Offset: 0x004321EC
		private void OnClickStopAll()
		{
			foreach (ObjectInfo objectInfo in this.listInfo)
			{
				OCIRoute ociroute = Studio.GetCtrlInfo(objectInfo.dicKey) as OCIRoute;
				ociroute.Stop(true);
				this.dicNode[objectInfo].state = RouteNode.State.Stop;
				this.mpRouteCtrl.UpdateInteractable(ociroute);
				this.mpRoutePointCtrl.UpdateInteractable(ociroute);
			}
		}

		// Token: 0x0600A4E5 RID: 42213 RVA: 0x00433E84 File Offset: 0x00432284
		private void Awake()
		{
			this.buttonAll.onClick.AddListener(new UnityAction(this.OnClickALL));
			this.buttonReAll.onClick.AddListener(new UnityAction(this.OnClickReAll));
			this.buttonStopAll.onClick.AddListener(new UnityAction(this.OnClickStopAll));
			this._visible.Subscribe(delegate(bool _b)
			{
				if (_b)
				{
					this.Init();
				}
				base.gameObject.SetActive(_b);
			});
			this.dicNode = new Dictionary<ObjectInfo, RouteNode>();
			base.gameObject.SetActive(false);
		}

		// Token: 0x040081C6 RID: 33222
		[SerializeField]
		private Transform nodeRoot;

		// Token: 0x040081C7 RID: 33223
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x040081C8 RID: 33224
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x040081C9 RID: 33225
		[SerializeField]
		private Sprite[] spritePlay;

		// Token: 0x040081CA RID: 33226
		[Space]
		[SerializeField]
		private Button buttonAll;

		// Token: 0x040081CB RID: 33227
		[SerializeField]
		private Button buttonReAll;

		// Token: 0x040081CC RID: 33228
		[SerializeField]
		private Button buttonStopAll;

		// Token: 0x040081CD RID: 33229
		[Space]
		[SerializeField]
		private MPRouteCtrl mpRouteCtrl;

		// Token: 0x040081CE RID: 33230
		[SerializeField]
		private MPRoutePointCtrl mpRoutePointCtrl;

		// Token: 0x040081CF RID: 33231
		private BoolReactiveProperty _visible = new BoolReactiveProperty(false);

		// Token: 0x040081D0 RID: 33232
		private List<ObjectInfo> listInfo;

		// Token: 0x040081D1 RID: 33233
		private Dictionary<ObjectInfo, RouteNode> dicNode;
	}
}
