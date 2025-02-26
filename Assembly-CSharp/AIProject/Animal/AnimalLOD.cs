using System;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B86 RID: 2950
	public class AnimalLOD : MonoBehaviour
	{
		// Token: 0x060057C1 RID: 22465 RVA: 0x0025DA90 File Offset: 0x0025BE90
		private void Start()
		{
			if (this._animal == null)
			{
				this._animal = base.GetComponent<AnimalBase>();
			}
			if (this._animal == null)
			{
				this._animal = base.GetComponentInChildren<AnimalBase>(true);
			}
			if (this._animal == null)
			{
				this._animal = base.GetComponentInParent<AnimalBase>();
			}
			if (this._animal == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this.StartLateUpdate();
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x0025DB14 File Offset: 0x0025BF14
		private void OnLateUpdate()
		{
			float num = Vector3.Distance(this._camera.transform.position, this._animal.Position);
			bool flag = num <= this._visibleDistance;
			if (this._prevVisible != flag)
			{
				this._animal.SetForcedBodyEnabled(flag);
			}
			this._prevVisible = flag;
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x0025DB70 File Offset: 0x0025BF70
		private void StartLateUpdate()
		{
			this.StopLateUpdate();
			this._camera = Map.GetCameraComponent();
			if (this._camera == null)
			{
				this._camera = Camera.main;
			}
			if (this._camera == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this._lateUpdateDisposable = (from _ in Observable.EveryLateUpdate().TakeUntilDestroy(this).TakeUntilDestroy(this._camera)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x0025DC00 File Offset: 0x0025C000
		private void StopLateUpdate()
		{
			if (this._lateUpdateDisposable != null)
			{
				this._lateUpdateDisposable.Dispose();
				this._lateUpdateDisposable = null;
			}
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0025DC1F File Offset: 0x0025C01F
		private void OnDestroy()
		{
			this.StopLateUpdate();
		}

		// Token: 0x040050B5 RID: 20661
		[SerializeField]
		private AnimalBase _animal;

		// Token: 0x040050B6 RID: 20662
		[SerializeField]
		[Min(0f)]
		private float _visibleDistance;

		// Token: 0x040050B7 RID: 20663
		private Camera _camera;

		// Token: 0x040050B8 RID: 20664
		private bool _prevVisible;

		// Token: 0x040050B9 RID: 20665
		private IDisposable _lateUpdateDisposable;
	}
}
