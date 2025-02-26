using System;
using System.IO;
using Manager;
using UnityEngine;

namespace CreateThumbnailAnother
{
	// Token: 0x02000880 RID: 2176
	public class CameraControl : BaseCameraControl_Ver2
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x00149571 File Offset: 0x00147971
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x00149579 File Offset: 0x00147979
		public Transform targetTex { get; private set; }

		// Token: 0x060037B0 RID: 14256 RVA: 0x00149582 File Offset: 0x00147982
		public void SetTarget(Vector3 _pos, Vector3 _rot, float _dis, float _fov)
		{
			base.TargetPos = _pos;
			base.Rot = _rot;
			base.CameraDir = new Vector3(0f, 0f, -_dis);
			base.CameraFov = _fov;
			this.CameraUpdate();
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x001495B8 File Offset: 0x001479B8
		protected new void Start()
		{
			base.Start();
			this.targetTex = base.transform.Find("CameraTarget");
			if (this.targetTex)
			{
				this.targetTex.localScale = Vector3.one * 0.01f;
				this.targetRender = this.targetTex.GetComponent<Renderer>();
			}
			this.isOutsideTargetTex = true;
			this.isCursorLock = true;
			this.isInit = true;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x00149634 File Offset: 0x00147A34
		protected new void LateUpdate()
		{
			bool flag = !Singleton<Scene>.IsInstance() || (!Singleton<Scene>.Instance.IsNowLoading & !Singleton<Scene>.Instance.IsNowLoadingFade);
			if (flag)
			{
				base.isControlNow = false;
				base.SetCtrlSpeed();
				if (!base.isControlNow)
				{
					BaseCameraControl_Ver2.NoCtrlFunc zoomCondition = this.ZoomCondition;
					bool flag2 = true;
					if (zoomCondition != null)
					{
						flag2 = zoomCondition();
					}
					base.isControlNow |= (flag2 && base.InputMouseWheelZoomProc());
				}
				if (!base.isControlNow)
				{
					BaseCameraControl_Ver2.NoCtrlFunc noCtrlCondition = this.NoCtrlCondition;
					bool flag3 = false;
					if (noCtrlCondition != null)
					{
						flag3 = noCtrlCondition();
					}
					if (!flag3)
					{
						if (base.InputTouchProc())
						{
							base.isControlNow = true;
						}
						else if (this.InputMouseProc())
						{
							base.isControlNow = true;
						}
					}
				}
				if (!base.isControlNow)
				{
					BaseCameraControl_Ver2.NoCtrlFunc keyCondition = this.KeyCondition;
					bool flag4 = true;
					if (keyCondition != null)
					{
						flag4 = keyCondition();
					}
					base.isControlNow |= (flag4 && base.InputKeyProc());
				}
				this.CameraUpdate();
			}
			if (this.targetTex)
			{
				if (this.transBase != null)
				{
					this.targetTex.position = this.transBase.TransformPoint(this.CamDat.Pos);
				}
				else
				{
					this.targetTex.position = this.CamDat.Pos;
				}
				Vector3 position = base.transform.position;
				position.y = this.targetTex.position.y;
				this.targetTex.transform.LookAt(position);
				this.targetTex.Rotate(new Vector3(90f, 0f, 0f));
				if (this.targetRender)
				{
					this.targetRender.enabled = (base.isControlNow & this.isOutsideTargetTex);
				}
				if (Singleton<GameCursor>.IsInstance() && this.isCursorLock)
				{
					Singleton<GameCursor>.Instance.SetCursorLock(base.isControlNow & this.isOutsideTargetTex);
				}
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x00149864 File Offset: 0x00147C64
		protected new void CameraUpdate()
		{
			if (this.isLimitDir)
			{
				this.CamDat.Dir.z = Mathf.Clamp(this.CamDat.Dir.z, -this.limitDir, 0f);
			}
			if (this.isLimitPos)
			{
				this.CamDat.Pos = Vector3.ClampMagnitude(this.CamDat.Pos, this.limitPos);
			}
			if (this.transBase != null)
			{
				base.transform.rotation = this.transBase.rotation * Quaternion.Euler(this.CamDat.Rot);
				base.transform.position = base.transform.rotation * this.CamDat.Dir + this.transBase.TransformPoint(this.CamDat.Pos);
			}
			else
			{
				base.transform.rotation = Quaternion.Euler(this.CamDat.Rot);
				base.transform.position = base.transform.rotation * this.CamDat.Dir + this.CamDat.Pos;
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x001499AC File Offset: 0x00147DAC
		public void CameraDataSaveBinary(BinaryWriter bw)
		{
			bw.Write(this.CamDat.Pos.x);
			bw.Write(this.CamDat.Pos.y);
			bw.Write(this.CamDat.Pos.z);
			bw.Write(this.CamDat.Dir.x);
			bw.Write(this.CamDat.Dir.y);
			bw.Write(this.CamDat.Dir.z);
			bw.Write(this.CamDat.Rot.x);
			bw.Write(this.CamDat.Rot.y);
			bw.Write(this.CamDat.Rot.z);
			bw.Write(this.CamDat.Fov);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x00149A90 File Offset: 0x00147E90
		public void CameraDataLoadBinary(BinaryReader br, bool isUpdate)
		{
			BaseCameraControl_Ver2.CameraData cameraData = default(BaseCameraControl_Ver2.CameraData);
			cameraData.Pos.x = br.ReadSingle();
			cameraData.Pos.y = br.ReadSingle();
			cameraData.Pos.z = br.ReadSingle();
			cameraData.Dir.x = br.ReadSingle();
			cameraData.Dir.y = br.ReadSingle();
			cameraData.Dir.z = br.ReadSingle();
			cameraData.Rot.x = br.ReadSingle();
			cameraData.Rot.y = br.ReadSingle();
			cameraData.Rot.z = br.ReadSingle();
			cameraData.Fov = br.ReadSingle();
			this.CamReset.Copy(cameraData, Quaternion.identity);
			if (isUpdate)
			{
				this.CamDat.Copy(cameraData);
				if (base.thisCamera != null)
				{
					base.thisCamera.fieldOfView = cameraData.Fov;
				}
				this.CameraUpdate();
				if (!this.isInit)
				{
					this.isInit = true;
				}
			}
		}

		// Token: 0x04003848 RID: 14408
		public bool isOutsideTargetTex;

		// Token: 0x04003849 RID: 14409
		public bool isCursorLock;

		// Token: 0x0400384B RID: 14411
		private Renderer targetRender;
	}
}
