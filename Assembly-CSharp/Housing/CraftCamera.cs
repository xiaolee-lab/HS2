using System;
using System.Collections;
using AIProject;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Housing
{
	// Token: 0x02000884 RID: 2180
	public class CraftCamera : VirtualCameraController
	{
		// Token: 0x060037C7 RID: 14279 RVA: 0x0014C198 File Offset: 0x0014A598
		private new void Start()
		{
			base.Init();
			this.bLock = false;
			this.nFloorCnt = 0;
			this.isLimitDir = true;
			this.limitDir = 190f;
			this.isLimitPos = true;
			this.limitPos = 50f;
			this.isLimitRotX = true;
			this.limitRotX = 180f;
			this.CamDat.Pos = this.initPos;
			this.CamDat.Dir = new Vector3(0f, 0f, this.initDis);
			this.CamDat.Rot = this.initRot;
			this.CamDat.Fov = base.lens.FieldOfView;
			this.CamReset.Copy(this.CamDat, Quaternion.identity);
			this.craft = true;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x0014C268 File Offset: 0x0014A668
		protected override void LateUpdate()
		{
			if (this.bLock)
			{
				return;
			}
			base.isControlNow = false;
			if (!base.isControlNow)
			{
				VirtualCameraController.NoCtrlFunc noCtrlCondition = this.NoCtrlCondition;
				bool flag = false;
				if (noCtrlCondition != null)
				{
					foreach (VirtualCameraController.NoCtrlFunc noCtrlFunc in noCtrlCondition.GetInvocationList())
					{
						flag |= noCtrlFunc();
					}
				}
				if (!flag)
				{
					if (base.InputTouchProc())
					{
						base.isControlNow = true;
					}
					else if (base.InputMouseProc())
					{
						base.isControlNow = true;
					}
				}
			}
			if (!base.isControlNow)
			{
				VirtualCameraController.NoCtrlFunc keyCondition = this.KeyCondition;
				bool flag = true;
				if (keyCondition != null)
				{
					foreach (VirtualCameraController.NoCtrlFunc noCtrlFunc2 in keyCondition.GetInvocationList())
					{
						flag &= noCtrlFunc2();
					}
				}
				base.isControlNow |= (flag && base.InputKeyProc());
			}
			if (!base.isControlNow)
			{
				VirtualCameraController.NoCtrlFunc zoomCondition = this.ZoomCondition;
				bool flag = true;
				if (zoomCondition != null)
				{
					foreach (VirtualCameraController.NoCtrlFunc noCtrlFunc3 in zoomCondition.GetInvocationList())
					{
						flag &= noCtrlFunc3();
					}
				}
				bool flag2 = EventSystem.current && EventSystem.current.IsPointerOverGameObject();
				base.isControlNow |= (!flag2 && flag && this.InputMouseWheelZoomProc());
			}
			if (!(EventSystem.current != null) || EventSystem.current.IsPointerOverGameObject())
			{
				base.isControlNow = false;
			}
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x0014C430 File Offset: 0x0014A830
		public void CameraUp(int TargetFloorCnt)
		{
			if (base.isControlNow)
			{
				return;
			}
			this.nFloorCnt = TargetFloorCnt;
			this.CamDat.Pos.y = this.CamReset.Pos.y + (float)(5 * this.nFloorCnt);
			this.ForceMoveCam(0f);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0014C488 File Offset: 0x0014A888
		private bool InputMouseWheelZoomProc()
		{
			bool result = false;
			this.isZoomNow = false;
			float num = this.input.ScrollValue() * this.zoomSpeed;
			if (num != 0f)
			{
				this.CamDat.Fov = this.CamDat.Fov - num;
				this.CamDat.Fov = Mathf.Clamp(this.CamDat.Fov, this.CamReset.Fov, this.limitFov);
				this.Lens.FieldOfView = this.CamDat.Fov;
				this.m_State.Lens = this.Lens;
				result = true;
				this.isZoomNow = true;
			}
			return result;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0014C52C File Offset: 0x0014A92C
		public void setLimitPos(float limit)
		{
			this.limitPos += limit;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0014C53C File Offset: 0x0014A93C
		public override void ForceMoveCam(float deltaTime = 0f)
		{
			if (this.isLimitDir)
			{
				this.CamDat.Dir.z = Mathf.Clamp(this.CamDat.Dir.z, -this.limitDir, 10f);
			}
			if (this.isLimitPos)
			{
				this.CamDat.Pos.x = Mathf.Clamp(this.CamDat.Pos.x, this.limitAreaMin.x, this.limitAreaMax.x);
				this.CamDat.Pos.y = Mathf.Clamp(this.CamDat.Pos.y, this.limitAreaMin.y, this.limitAreaMax.y);
				this.CamDat.Pos.z = Mathf.Clamp(this.CamDat.Pos.z, this.limitAreaMin.z, this.limitAreaMax.z);
			}
			if (this.isLimitRotX)
			{
				this.CamDat.Rot.x = Mathf.Clamp(this.CamDat.Rot.x, 0f, this.limitRotX);
			}
			if (this.follow != null)
			{
				this.m_State.RawOrientation = this.follow.rotation * Quaternion.Euler(this.CamDat.Rot);
				this.m_State.RawPosition = this.m_State.RawOrientation * this.CamDat.Dir + this.follow.TransformPoint(this.CamDat.Pos);
			}
			else
			{
				this.m_State.RawOrientation = Quaternion.Euler(this.CamDat.Rot);
				this.m_State.RawPosition = this.m_State.RawOrientation * this.CamDat.Dir + this.CamDat.Pos;
			}
			this.m_State.Lens = this.Lens;
			base.transform.position = this.State.RawPosition;
			base.transform.rotation = this.State.RawOrientation;
			if (this.viewCollider != null)
			{
				this.viewCollider.height = this.CamDat.Dir.z;
				this.viewCollider.center = -Vector3.forward * this.CamDat.Dir.z * 0.5f;
			}
			this.lookAtTarget.localPosition = this.CamDat.Pos;
			Vector3 position = base.transform.position;
			position.y = this.lookAtTarget.position.y;
			this.lookAtTarget.transform.LookAt(position);
			this.lookAtTarget.Rotate(new Vector3(90f, 0f, 0f));
			bool flag = Vector3.Distance(this.lookAtTarget.transform.position, position) < 0.001f;
			if (this.targetRender)
			{
				this.targetRender.enabled = (base.isControlNow & base.isOutsideTargetTex & base.isConfigTargetTex & !this.isZoomNow & !flag);
			}
			if (Singleton<GameCursor>.IsInstance() && base.isCursorLock)
			{
				Singleton<GameCursor>.Instance.SetCursorLock(base.isControlNow & base.isOutsideTargetTex);
			}
			IEnumerator enumerator = Enum.GetValues(typeof(CinemachineCore.Stage)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					CinemachineCore.Stage stage = (CinemachineCore.Stage)obj;
					base.InvokePostPipelineStageCallback(this, stage, ref this.m_State, deltaTime);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0400385A RID: 14426
		public bool bLock;

		// Token: 0x0400385B RID: 14427
		public int nFloorCnt;

		// Token: 0x0400385C RID: 14428
		public Vector3 initPos;

		// Token: 0x0400385D RID: 14429
		public Vector3 initRot;

		// Token: 0x0400385E RID: 14430
		public float initDis;

		// Token: 0x0400385F RID: 14431
		public Vector3 limitAreaMin;

		// Token: 0x04003860 RID: 14432
		public Vector3 limitAreaMax;

		// Token: 0x04003861 RID: 14433
		public float zoomSpeed;
	}
}
