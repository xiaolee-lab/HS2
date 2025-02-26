using System;
using Manager;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200120D RID: 4621
	public class OCICamera : ObjectCtrlInfo
	{
		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x0600974C RID: 38732 RVA: 0x003E8AF6 File Offset: 0x003E6EF6
		public OICameraInfo cameraInfo
		{
			get
			{
				return this.objectInfo as OICameraInfo;
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x0600974D RID: 38733 RVA: 0x003E8B03 File Offset: 0x003E6F03
		// (set) Token: 0x0600974E RID: 38734 RVA: 0x003E8B10 File Offset: 0x003E6F10
		public string name
		{
			get
			{
				return this.cameraInfo.name;
			}
			set
			{
				this.cameraInfo.name = value;
				this.treeNodeObject.textName = value;
			}
		}

		// Token: 0x0600974F RID: 38735 RVA: 0x003E8B2C File Offset: 0x003E6F2C
		public void SetActive(bool _active)
		{
			this.cameraInfo.active = _active;
			if (_active)
			{
				if (this.disposable != null)
				{
					return;
				}
				this.cameraControl = Singleton<Studio>.Instance.cameraCtrl;
				this.disposable = new SingleAssignmentDisposable();
				this.disposable.Disposable = Observable.EveryLateUpdate().Subscribe(delegate(long _)
				{
					this.cameraControl.SafeProc(delegate(CameraControl _cc)
					{
						_cc.SetPositionAndRotation(this.objectItem.transform.position, this.objectItem.transform.rotation);
					});
				});
				this.treeNodeObject.baseColor = AddObjectCamera.activeColor;
				if (!Singleton<Studio>.Instance.treeNodeCtrl.CheckSelect(this.treeNodeObject))
				{
					this.treeNodeObject.colorSelect = AddObjectCamera.activeColor;
				}
				this.guideObject.visible = false;
				this.meshRenderer.enabled = false;
			}
			else
			{
				if (this.disposable != null)
				{
					this.disposable.Dispose();
					this.disposable = null;
				}
				this.treeNodeObject.baseColor = AddObjectCamera.baseColor;
				if (!Singleton<Studio>.Instance.treeNodeCtrl.CheckSelect(this.treeNodeObject))
				{
					this.treeNodeObject.colorSelect = AddObjectCamera.baseColor;
				}
				this.guideObject.visible = true;
				this.meshRenderer.enabled = (!this.cameraInfo.active & this.visibleOutside);
			}
		}

		// Token: 0x06009750 RID: 38736 RVA: 0x003E8C70 File Offset: 0x003E7070
		public override void OnDelete()
		{
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			UnityEngine.Object.Destroy(this.objectItem);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
			Singleton<Studio>.Instance.DeleteCamera(this);
		}

		// Token: 0x06009751 RID: 38737 RVA: 0x003E8CC7 File Offset: 0x003E70C7
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009752 RID: 38738 RVA: 0x003E8CC9 File Offset: 0x003E70C9
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009753 RID: 38739 RVA: 0x003E8CCC File Offset: 0x003E70CC
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

		// Token: 0x06009754 RID: 38740 RVA: 0x003E8D7F File Offset: 0x003E717F
		public override void OnSelect(bool _select)
		{
		}

		// Token: 0x06009755 RID: 38741 RVA: 0x003E8D81 File Offset: 0x003E7181
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009756 RID: 38742 RVA: 0x003E8D83 File Offset: 0x003E7183
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
		}

		// Token: 0x06009757 RID: 38743 RVA: 0x003E8D8B File Offset: 0x003E718B
		public override void OnVisible(bool _visible)
		{
			this.visibleOutside = _visible;
			this.meshRenderer.enabled = (!this.cameraInfo.active & this.visibleOutside);
		}

		// Token: 0x04007969 RID: 31081
		public GameObject objectItem;

		// Token: 0x0400796A RID: 31082
		public MeshRenderer meshRenderer;

		// Token: 0x0400796B RID: 31083
		private SingleAssignmentDisposable disposable;

		// Token: 0x0400796C RID: 31084
		private bool visibleOutside = true;

		// Token: 0x0400796D RID: 31085
		private CameraControl cameraControl;
	}
}
