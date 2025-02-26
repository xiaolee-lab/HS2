using System;
using AIChara;
using UniRx;
using UnityEngine;

namespace Correct.Process
{
	// Token: 0x02000B3E RID: 2878
	[RequireComponent(typeof(BaseData))]
	public abstract class BaseProcess : MonoBehaviour
	{
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x00252A9A File Offset: 0x00250E9A
		public BaseData data
		{
			get
			{
				if (this._data == null)
				{
					this._data = base.GetComponent<BaseData>();
				}
				return this._data;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005448 RID: 21576 RVA: 0x00252AC0 File Offset: 0x00250EC0
		public ChaControl chaCtrl
		{
			get
			{
				if (this._chaCtrl == null)
				{
					this._chaCtrl = base.GetComponentInParent<ChaControl>();
				}
				return this._chaCtrl;
			}
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00252AF2 File Offset: 0x00250EF2
		private void Start()
		{
			(from _ in Observable.EveryEndOfFrame().TakeUntilDestroy(base.gameObject)
			where this != null && base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.Restore();
			});
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00252B28 File Offset: 0x00250F28
		protected virtual void LateUpdate()
		{
			if (this._data == null)
			{
				return;
			}
			Transform bone = this._data.bone;
			if (bone == null)
			{
				return;
			}
			BaseProcess.Type type = this.type;
			if (type != BaseProcess.Type.Target)
			{
				if (type == BaseProcess.Type.Sync)
				{
					if (this.chaCtrl != null)
					{
						Vector3 position = bone.position + this.chaCtrl.objBodyBone.transform.TransformDirection(this._data.pos);
						base.transform.position = position;
						base.transform.rotation = bone.rotation * this._data.rot;
					}
				}
			}
			else
			{
				this.pos = bone.localPosition;
				this.rot = bone.localRotation;
				bone.localPosition = this.pos + this._data.pos;
				bone.localRotation = this.rot * this._data.rot;
			}
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00252C40 File Offset: 0x00251040
		private void Restore()
		{
			if (this.type != BaseProcess.Type.Target || this.noRestore)
			{
				return;
			}
			if (this._data == null)
			{
				return;
			}
			Transform bone = this._data.bone;
			if (bone == null)
			{
				return;
			}
			bone.localPosition = this.pos;
			bone.localRotation = this.rot;
		}

		// Token: 0x04004F13 RID: 20243
		protected BaseData _data;

		// Token: 0x04004F14 RID: 20244
		public BaseProcess.Type type;

		// Token: 0x04004F15 RID: 20245
		public bool noRestore;

		// Token: 0x04004F16 RID: 20246
		private ChaControl _chaCtrl;

		// Token: 0x04004F17 RID: 20247
		private Vector3 pos = Vector3.zero;

		// Token: 0x04004F18 RID: 20248
		private Quaternion rot = Quaternion.identity;

		// Token: 0x02000B3F RID: 2879
		public enum Type
		{
			// Token: 0x04004F1A RID: 20250
			Target,
			// Token: 0x04004F1B RID: 20251
			Sync
		}
	}
}
