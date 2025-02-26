using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Cinemachine;
using Illusion.Extensions;
using UniRx;
using UnityEngine;

namespace ADV.EventCG
{
	// Token: 0x020006AB RID: 1707
	public class CameraData : MonoBehaviour
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000EF440 File Offset: 0x000ED840
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000EF448 File Offset: 0x000ED848
		public bool initialized { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000EF451 File Offset: 0x000ED851
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x000EF459 File Offset: 0x000ED859
		public float fieldOfView
		{
			get
			{
				return this._fieldOfView;
			}
			set
			{
				this._fieldOfView = value;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000EF462 File Offset: 0x000ED862
		public void SetCameraData(Component component)
		{
			if (this.SetCameraData(component.GetComponent<Camera>()))
			{
				return;
			}
			if (this.SetCameraData(component.GetComponent<CinemachineVirtualCamera>()))
			{
				return;
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000EF488 File Offset: 0x000ED888
		public void RepairCameraData(Component component)
		{
			if (this.baseFieldOfView == null)
			{
				return;
			}
			if (this.RepairCameraData(component.GetComponent<Camera>()))
			{
				return;
			}
			if (this.RepairCameraData(component.GetComponent<CinemachineVirtualCamera>()))
			{
				return;
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000EF4BF File Offset: 0x000ED8BF
		private bool SetCameraData(Camera cam)
		{
			if (cam == null)
			{
				return false;
			}
			this.baseFieldOfView = new float?(cam.fieldOfView);
			return true;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000EF4E1 File Offset: 0x000ED8E1
		private bool SetCameraData(CinemachineVirtualCamera cam)
		{
			if (cam == null)
			{
				return false;
			}
			this.baseFieldOfView = new float?(cam.m_Lens.FieldOfView);
			this.lookAt = cam.LookAt;
			return true;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000EF514 File Offset: 0x000ED914
		private bool RepairCameraData(Camera cam)
		{
			if (cam == null || this.baseFieldOfView == null)
			{
				return false;
			}
			cam.fieldOfView = this.baseFieldOfView.Value;
			return true;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000EF548 File Offset: 0x000ED948
		private bool RepairCameraData(CinemachineVirtualCamera cam)
		{
			if (cam == null || this.baseFieldOfView == null)
			{
				return false;
			}
			cam.enabled = true;
			cam.m_Lens.FieldOfView = this.baseFieldOfView.Value;
			cam.LookAt = this.lookAt;
			return true;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000EF59D File Offset: 0x000ED99D
		public ReactiveCollection<ChaControl> chaCtrlList
		{
			get
			{
				return this._chaCtrlList;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000EF5A8 File Offset: 0x000ED9A8
		private void Calculate()
		{
			if (!this._chaCtrlList.Any<ChaControl>())
			{
				return;
			}
			float shape = this._chaCtrlList.Average((ChaControl p) => p.GetShapeBodyValue(0));
			Vector3 vector = MathfEx.GetShapeLerpPositionValue(shape, this._minPos, this._maxPos);
			Vector3 shapeLerpAngleValue = MathfEx.GetShapeLerpAngleValue(shape, this._minAng, this._maxAng);
			vector = base.transform.parent.TransformDirection(vector);
			base.transform.SetPositionAndRotation(this.basePos + vector, Quaternion.Euler(this.baseAng + shapeLerpAngleValue));
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000EF64F File Offset: 0x000EDA4F
		private void OnEnable()
		{
			this.basePos = base.transform.position;
			this.baseAng = base.transform.eulerAngles;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000EF673 File Offset: 0x000EDA73
		private void OnDisable()
		{
			base.transform.position = this.basePos;
			base.transform.eulerAngles = this.baseAng;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000EF698 File Offset: 0x000EDA98
		private IEnumerator Start()
		{
			base.enabled = false;
			Data componentInParent = base.GetComponentInParent<Data>();
			List<ChaControl> list = (from p in componentInParent.chaRoot.Children()
			select p.GetComponent<ChaControl>() into p
			where p != null
			select p).ToList<ChaControl>();
			list.ForEach(delegate(ChaControl item)
			{
				this._chaCtrlList.Add(item);
			});
			this._chaCtrlList.ObserveAdd().Subscribe(delegate(CollectionAddEvent<ChaControl> _)
			{
				this.Calculate();
			});
			this._chaCtrlList.ObserveRemove().Subscribe(delegate(CollectionRemoveEvent<ChaControl> _)
			{
				this.Calculate();
			});
			this._chaCtrlList.AddTo(this);
			base.enabled = true;
			this.initialized = true;
			this.Calculate();
			yield break;
		}

		// Token: 0x040029F4 RID: 10740
		[Header("カメラデータ")]
		[SerializeField]
		private float _fieldOfView;

		// Token: 0x040029F5 RID: 10741
		private float? baseFieldOfView;

		// Token: 0x040029F6 RID: 10742
		private Transform lookAt;

		// Token: 0x040029F7 RID: 10743
		[Header("身長補正座標")]
		[SerializeField]
		private Vector3 _minPos;

		// Token: 0x040029F8 RID: 10744
		[SerializeField]
		private Vector3 _maxPos;

		// Token: 0x040029F9 RID: 10745
		[Header("身長補正角度")]
		[SerializeField]
		private Vector3 _minAng;

		// Token: 0x040029FA RID: 10746
		[SerializeField]
		private Vector3 _maxAng;

		// Token: 0x040029FB RID: 10747
		private ReactiveCollection<ChaControl> _chaCtrlList = new ReactiveCollection<ChaControl>();

		// Token: 0x040029FC RID: 10748
		private Vector3 basePos;

		// Token: 0x040029FD RID: 10749
		private Vector3 baseAng;
	}
}
