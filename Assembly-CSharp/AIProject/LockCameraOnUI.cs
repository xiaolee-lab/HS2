using System;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000FA0 RID: 4000
	public class LockCameraOnUI : MonoBehaviour
	{
		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06008555 RID: 34133 RVA: 0x00375523 File Offset: 0x00373923
		// (set) Token: 0x06008556 RID: 34134 RVA: 0x0037552B File Offset: 0x0037392B
		public RectTransform BaseCalcRect
		{
			get
			{
				return this._baseCalcRect;
			}
			set
			{
				this._baseCalcRect = value;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06008557 RID: 34135 RVA: 0x00375534 File Offset: 0x00373934
		// (set) Token: 0x06008558 RID: 34136 RVA: 0x0037553C File Offset: 0x0037393C
		public ActorCameraControl Camera
		{
			get
			{
				return this._camera;
			}
			set
			{
				this._camera = value;
			}
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x00375548 File Offset: 0x00373948
		private void Start()
		{
			this.SearchCanvas();
			if (this._camera == null)
			{
				Camera main = UnityEngine.Camera.main;
				this._camera = ((main != null) ? main.GetComponent<ActorCameraControl>() : null);
			}
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x003755BC File Offset: 0x003739BC
		private void SearchCanvas()
		{
			GameObject gameObject = base.gameObject;
			for (;;)
			{
				Canvas component = gameObject.GetComponent<Canvas>();
				if (component)
				{
					break;
				}
				if (gameObject.transform.parent == null)
				{
					return;
				}
				gameObject = gameObject.transform.parent.gameObject;
			}
			this._baseCalcRect = (gameObject.transform as RectTransform);
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x0037562C File Offset: 0x00373A2C
		private void OnUpdate()
		{
			if (this._camera == null)
			{
				return;
			}
			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				if (this._camera.CinemachineBrain.enabled)
				{
					this._camera.CinemachineBrain.enabled = true;
				}
			}
			else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				RectTransform rectTransform = base.transform as RectTransform;
				float x = Input.mousePosition.x;
				float y = Input.mousePosition.y;
				bool flag = rectTransform.position.x <= x && x <= rectTransform.position.x + rectTransform.rect.width * this._baseCalcRect.localScale.x;
				bool flag2 = rectTransform.position.y <= y && y <= rectTransform.position.y + rectTransform.rect.height * this._baseCalcRect.localScale.y;
				if (flag && flag2)
				{
					this._camera.CinemachineBrain.enabled = false;
				}
			}
		}

		// Token: 0x04006BDA RID: 27610
		[SerializeField]
		private RectTransform _baseCalcRect;

		// Token: 0x04006BDB RID: 27611
		[SerializeField]
		private ActorCameraControl _camera;
	}
}
