using System;
using UniRx;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000883 RID: 2179
	public class CopyCameraParam : MonoBehaviour
	{
		// Token: 0x060037BF RID: 14271 RVA: 0x00149D84 File Offset: 0x00148184
		private void Start()
		{
			this.mainCamera.ObserveEveryValueChanged((Camera _c) => _c.fieldOfView, FrameCountType.Update, false).Subscribe(delegate(float _f)
			{
				this.targetCamera.fieldOfView = _f;
			}).AddTo(this);
			this.mainCamera.ObserveEveryValueChanged((Camera _c) => _c.nearClipPlane, FrameCountType.Update, false).Subscribe(delegate(float _f)
			{
				this.targetCamera.nearClipPlane = _f;
			}).AddTo(this);
			this.mainCamera.ObserveEveryValueChanged((Camera _c) => _c.farClipPlane, FrameCountType.Update, false).Subscribe(delegate(float _f)
			{
				this.targetCamera.farClipPlane = _f;
			}).AddTo(this);
		}

		// Token: 0x04003855 RID: 14421
		[SerializeField]
		private Camera mainCamera;

		// Token: 0x04003856 RID: 14422
		[SerializeField]
		private Camera targetCamera;
	}
}
