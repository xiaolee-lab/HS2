using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cinemachine;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AIProject
{
	// Token: 0x02000B20 RID: 2848
	public class VirtualCameraController : CinemachineVirtualCameraBase
	{
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00149F42 File Offset: 0x00148342
		// (set) Token: 0x0600537A RID: 21370 RVA: 0x00149F4A File Offset: 0x0014834A
		public Camera thisCamera { get; private set; }

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x00149F53 File Offset: 0x00148353
		// (set) Token: 0x0600537C RID: 21372 RVA: 0x00149F5B File Offset: 0x0014835B
		public bool isOutsideTargetTex { get; set; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x00149F64 File Offset: 0x00148364
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x00149F6C File Offset: 0x0014836C
		public bool isCursorLock { get; set; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x00149F75 File Offset: 0x00148375
		// (set) Token: 0x06005380 RID: 21376 RVA: 0x00149F7D File Offset: 0x0014837D
		public bool isConfigTargetTex { get; set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x00149F86 File Offset: 0x00148386
		// (set) Token: 0x06005382 RID: 21378 RVA: 0x00149F8E File Offset: 0x0014838E
		public bool ConfigVanish
		{
			get
			{
				return this.isConfigVanish;
			}
			set
			{
				if (this.isConfigVanish != value)
				{
					this.isConfigVanish = value;
					this.visibleForceVanish(true);
					if (this.housingMgr != null)
					{
						this.housingMgr.VisibleShield();
					}
				}
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00149FC2 File Offset: 0x001483C2
		// (set) Token: 0x06005384 RID: 21380 RVA: 0x00149FCA File Offset: 0x001483CA
		public bool isControlNow { get; protected set; }

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x00149FD3 File Offset: 0x001483D3
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06005386 RID: 21382 RVA: 0x00149FDB File Offset: 0x001483DB
		// (set) Token: 0x06005387 RID: 21383 RVA: 0x00149FE3 File Offset: 0x001483E3
		public override Transform LookAt
		{
			get
			{
				return this.lookAtTarget;
			}
			set
			{
				this.lookAtTarget = value;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x00149FEC File Offset: 0x001483EC
		// (set) Token: 0x06005389 RID: 21385 RVA: 0x00149FF4 File Offset: 0x001483F4
		public override Transform Follow
		{
			get
			{
				return this.follow;
			}
			set
			{
				this.follow = value;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x0600538A RID: 21386 RVA: 0x00149FFD File Offset: 0x001483FD
		// (set) Token: 0x0600538B RID: 21387 RVA: 0x0014A005 File Offset: 0x00148405
		public LensSettings lens
		{
			get
			{
				return this.Lens;
			}
			set
			{
				this.Lens = value;
			}
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0014A010 File Offset: 0x00148410
		protected override void Start()
		{
			base.Start();
			this.isCursorLock = true;
			this.viewCollider = base.gameObject.AddComponent<CapsuleCollider>();
			this.viewCollider.radius = 0.05f;
			this.viewCollider.isTrigger = true;
			this.viewCollider.direction = 2;
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0014A07D File Offset: 0x0014847D
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			if (!base.enabled)
			{
				return;
			}
			this.ForceMoveCam(deltaTime);
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0014A094 File Offset: 0x00148494
		public virtual void ForceMoveCam(float deltaTime = 0f)
		{
			if (this.craft)
			{
				if (this.isLimitDir)
				{
					this.CamDat.Dir.z = Mathf.Clamp(this.CamDat.Dir.z, -this.limitDir, 10f);
				}
				if (this.isLimitPos)
				{
					this.CamDat.Pos = Vector3.ClampMagnitude(this.CamDat.Pos, this.limitPos);
					if (this.CamDat.Pos.y < 2.5f)
					{
						this.CamDat.Pos.y = 2.5f;
					}
				}
				if (this.isLimitRotX)
				{
					this.CamDat.Rot.x = Mathf.Clamp(this.CamDat.Rot.x, 0f, this.limitRotX);
				}
			}
			else
			{
				if (this.isLimitDir)
				{
					this.CamDat.Dir.z = Mathf.Clamp(this.CamDat.Dir.z, -this.limitDir, 10f);
				}
				if (this.isLimitPos)
				{
					this.CamDat.Pos = Vector3.ClampMagnitude(this.CamDat.Pos, this.limitPos);
				}
				if (this.isLimitRotX)
				{
					this.CamDat.Rot.x = Mathf.Clamp(this.CamDat.Rot.x, 0f, this.limitRotX);
				}
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
			if (this.targetRender)
			{
				this.targetRender.enabled = (this.isControlNow & this.isOutsideTargetTex & this.isConfigTargetTex & !this.isZoomNow);
			}
			if (Singleton<GameCursor>.IsInstance() && this.isCursorLock)
			{
				Singleton<GameCursor>.Instance.SetCursorLock(this.isControlNow & this.isOutsideTargetTex);
			}
			this.VanishProc();
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

		// Token: 0x0600538F RID: 21391 RVA: 0x0014A4E8 File Offset: 0x001488E8
		public void Init()
		{
			this.thisCamera = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera;
			this.housingMgr = Singleton<Housing>.Instance;
			this.CamDat.Fov = 23f;
			this.CamReset.Fov = 23f;
			this.m_State.Lens = this.lens;
			this.isDrags = new bool[this.CONFIG_SIZE];
			for (int i = 0; i < this.isDrags.Length; i++)
			{
				this.isDrags[i] = false;
			}
			if (this.isInit)
			{
				return;
			}
			this.isButtons = new bool[this.CONFIG_SIZE];
			this.targetRender = this.lookAtTarget.GetComponent<Renderer>();
			this.TargetSet(this.lookAtTarget, true);
			this.isOutsideTargetTex = true;
			this.isConfigTargetTex = true;
			this.input = Singleton<Manager.Input>.Instance;
			this.isInit = true;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0014A5D5 File Offset: 0x001489D5
		protected override void Update()
		{
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0014A5D8 File Offset: 0x001489D8
		protected virtual void LateUpdate()
		{
			this.isControlNow = false;
			if (!this.craft && Singleton<HSceneFlagCtrl>.Instance.BeforeHWait)
			{
				return;
			}
			if (!this.isControlNow)
			{
				VirtualCameraController.NoCtrlFunc noCtrlCondition = this.NoCtrlCondition;
				bool flag = false;
				if (noCtrlCondition != null)
				{
					flag = noCtrlCondition();
				}
				if (!flag)
				{
					if (this.InputTouchProc())
					{
						this.isControlNow = true;
					}
					else if (this.InputMouseProc())
					{
						this.isControlNow = true;
					}
				}
			}
			if (!this.isControlNow)
			{
				VirtualCameraController.NoCtrlFunc keyCondition = this.KeyCondition;
				bool flag = true;
				if (keyCondition != null)
				{
					flag = keyCondition();
				}
				this.isControlNow |= (flag && this.InputKeyProc());
			}
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0014A698 File Offset: 0x00148A98
		protected bool InputTouchProc()
		{
			if (UnityEngine.Input.touchCount < 1)
			{
				return false;
			}
			float num = 10f * Time.deltaTime;
			if (UnityEngine.Input.touchCount == 3)
			{
				this.Reset(0);
			}
			else if (UnityEngine.Input.touchCount == 1)
			{
				Touch touch = UnityEngine.Input.touches[0];
				TouchPhase phase = touch.phase;
				if (phase != TouchPhase.Began)
				{
					if (phase == TouchPhase.Moved)
					{
						float num2 = 0.01f;
						float num3 = 0.1f;
						Vector3 vector = Vector3.zero;
						if (this.cameraType == VirtualCameraController.Config.Rotation)
						{
							vector.y += touch.deltaPosition.x * this.xRotSpeed * num * num2;
							vector.x -= touch.deltaPosition.y * this.yRotSpeed * num * num2;
							vector += base.transform.rotation.eulerAngles;
							base.transform.rotation = Quaternion.Euler(vector);
						}
						else if (this.cameraType == VirtualCameraController.Config.Translation)
						{
							this.CamDat.Dir.z = this.CamDat.Dir.z - touch.deltaPosition.x * this.xRotSpeed * num * num3;
							this.CamDat.Pos.y = this.CamDat.Pos.y + touch.deltaPosition.y * this.yRotSpeed * num * num3;
						}
						else if (this.cameraType == VirtualCameraController.Config.MoveXY)
						{
							vector.x = touch.deltaPosition.x * this.xRotSpeed * num * num3;
							vector.y = touch.deltaPosition.y * this.yRotSpeed * num * num3;
							this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(vector);
						}
						else if (this.cameraType == VirtualCameraController.Config.MoveXZ)
						{
							vector.x = touch.deltaPosition.x * this.xRotSpeed * num * num3;
							vector.z = touch.deltaPosition.y * this.yRotSpeed * num * num3;
							this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(vector);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0014A924 File Offset: 0x00148D24
		protected bool InputMouseProc()
		{
			bool flag = false;
			float deltaTime = Time.deltaTime;
			bool flag2 = false;
			for (int i = 0; i < this.CONFIG_SIZE; i++)
			{
				if (this.isDrags[i])
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2 && (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject()))
			{
				if (Singleton<GameCursor>.IsInstance())
				{
					if (flag)
					{
						Singleton<GameCursor>.Instance.SetCursorLock(true);
					}
					else
					{
						Singleton<GameCursor>.Instance.UnLockCursor();
					}
				}
				return flag;
			}
			this.isButtons[1] = this.input.IsDown(ActionID.MouseLeft);
			this.isButtons[2] = this.input.IsDown(ActionID.MouseRight);
			this.isButtons[3] = this.input.IsDown(ActionID.MouseCenter);
			this.isButtons[0] = (this.input.IsDown(ActionID.MouseLeft) && this.input.IsDown(ActionID.MouseRight));
			for (int j = 0; j < this.CONFIG_SIZE; j++)
			{
				if (this.isButtons[j])
				{
					this.isDrags[j] = true;
				}
			}
			for (int k = 0; k < this.CONFIG_SIZE; k++)
			{
				if (this.isDrags[k] && !this.isButtons[k])
				{
					this.isDrags[k] = false;
				}
			}
			float x = this.input.MouseAxis.x;
			float y = this.input.MouseAxis.y;
			for (int l = 0; l < this.CONFIG_SIZE; l++)
			{
				if (this.isDrags[l])
				{
					Vector3 zero = Vector3.zero;
					if (l == 0)
					{
						zero.x = x * this.moveSpeed * deltaTime * this.rateAddSpeed;
						zero.z = y * this.moveSpeed * deltaTime * this.rateAddSpeed;
						if (this.follow != null)
						{
							this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(zero));
						}
						else
						{
							this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
						}
					}
					else if (l == 1)
					{
						zero.y += x * this.xRotSpeed * deltaTime * this.rateAddSpeed;
						zero.x -= y * this.yRotSpeed * deltaTime * this.rateAddSpeed;
						this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
						this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
					}
					else if (l == 2)
					{
						this.CamDat.Pos.y = this.CamDat.Pos.y + y * this.moveSpeed * deltaTime * this.rateAddSpeed;
						this.CamDat.Dir.z = this.CamDat.Dir.z - x * this.moveSpeed * deltaTime * this.rateAddSpeed;
						this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
					}
					else if (l == 3)
					{
						zero.x = x * this.moveSpeed * deltaTime * this.rateAddSpeed;
						zero.y = y * this.moveSpeed * deltaTime * this.rateAddSpeed;
						if (this.follow != null)
						{
							this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(zero));
						}
						else
						{
							this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
						}
					}
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0014AD78 File Offset: 0x00149178
		protected bool InputKeyProc()
		{
			bool flag = false;
			if (this.input.IsDown(KeyCode.R))
			{
				this.Reset(0);
			}
			else if (this.input.IsDown(KeyCode.Keypad5))
			{
				this.CamDat.Rot.x = this.CamReset.Rot.x;
				this.CamDat.Rot.y = this.CamReset.Rot.y;
			}
			else if (this.input.IsDown(KeyCode.Slash))
			{
				this.CamDat.Rot.z = 0f;
			}
			else if (this.input.IsDown(KeyCode.Semicolon))
			{
				this.CamDat.Fov = this.CamReset.Fov;
				this.Lens.FieldOfView = this.CamDat.Fov;
			}
			float deltaTime = Time.deltaTime;
			if (this.input.IsDown(KeyCode.Home))
			{
				flag = true;
				this.CamDat.Dir.z = this.CamDat.Dir.z + deltaTime * this.keyMoveSpeed * this.rateAddSpeed;
				this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
			}
			else if (this.input.IsDown(KeyCode.End))
			{
				flag = true;
				this.CamDat.Dir.z = this.CamDat.Dir.z - deltaTime * this.keyMoveSpeed * this.rateAddSpeed;
			}
			if (this.input.IsDown(KeyCode.RightArrow))
			{
				flag = true;
				if (this.follow != null)
				{
					this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(new Vector3(deltaTime * this.keyMoveSpeed * this.rateAddSpeed, 0f, 0f)));
				}
				else
				{
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(deltaTime * this.keyMoveSpeed * this.rateAddSpeed, 0f, 0f));
				}
			}
			else if (this.input.IsDown(KeyCode.LeftArrow))
			{
				flag = true;
				if (this.follow != null)
				{
					this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(new Vector3(-deltaTime * this.keyMoveSpeed * this.rateAddSpeed, 0f, 0f)));
				}
				else
				{
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(-deltaTime * this.keyMoveSpeed * this.rateAddSpeed, 0f, 0f));
				}
			}
			if (this.input.IsDown(KeyCode.UpArrow))
			{
				flag = true;
				if (this.follow != null)
				{
					this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, deltaTime * this.keyMoveSpeed * this.rateAddSpeed)));
				}
				else
				{
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, deltaTime * this.keyMoveSpeed * this.rateAddSpeed));
				}
			}
			else if (this.input.IsDown(KeyCode.DownArrow))
			{
				flag = true;
				if (this.follow != null)
				{
					this.CamDat.Pos = this.CamDat.Pos + this.follow.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, -deltaTime * this.keyMoveSpeed * this.rateAddSpeed)));
				}
				else
				{
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, -deltaTime * this.keyMoveSpeed * this.rateAddSpeed));
				}
			}
			if (this.input.IsDown(KeyCode.PageUp))
			{
				flag = true;
				this.CamDat.Pos.y = this.CamDat.Pos.y + deltaTime * this.keyMoveSpeed * this.rateAddSpeed;
			}
			else if (this.input.IsDown(KeyCode.PageDown))
			{
				flag = true;
				this.CamDat.Pos.y = this.CamDat.Pos.y - deltaTime * this.keyMoveSpeed * this.rateAddSpeed;
			}
			float num = 10f * Time.deltaTime;
			Vector3 zero = Vector3.zero;
			if (this.input.IsDown(KeyCode.Period))
			{
				flag = true;
				zero.z += num;
			}
			else if (this.input.IsDown(KeyCode.Backslash))
			{
				flag = true;
				zero.z -= num;
			}
			if (this.input.IsDown(KeyCode.Keypad2))
			{
				flag = true;
				zero.x -= num * this.yRotSpeed;
			}
			else if (this.input.IsDown(KeyCode.Keypad8))
			{
				flag = true;
				zero.x += num * this.yRotSpeed;
			}
			if (this.input.IsDown(KeyCode.Keypad4))
			{
				flag = true;
				zero.y += num * this.xRotSpeed;
			}
			else if (this.input.IsDown(KeyCode.Keypad6))
			{
				flag = true;
				zero.y -= num * this.xRotSpeed;
			}
			if (flag)
			{
				this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
				this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
				this.CamDat.Rot.z = (this.CamDat.Rot.z + zero.z) % 360f;
			}
			float deltaTime2 = Time.deltaTime;
			if (this.input.IsDown(KeyCode.Equals))
			{
				flag = true;
				this.CamDat.Fov = Mathf.Max(this.CamDat.Fov - deltaTime2 * 15f, 1f);
				this.Lens.FieldOfView = this.CamDat.Fov;
				this.m_State.Lens = this.Lens;
			}
			else if (this.input.IsDown(KeyCode.RightBracket))
			{
				flag = true;
				this.CamDat.Fov = Mathf.Min(this.CamDat.Fov + deltaTime2 * 15f, this.limitFov);
				this.Lens.FieldOfView = this.CamDat.Fov;
				this.m_State.Lens = this.Lens;
			}
			return flag;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0014B500 File Offset: 0x00149900
		public void Reset(int mode)
		{
			int num = 0;
			if (mode == num++)
			{
				this.CamDat.Copy(this.CamReset);
				this.Lens = this.m_State.Lens;
				this.Lens.FieldOfView = this.CamDat.Fov;
			}
			else if (mode == num++)
			{
				this.CamDat.Pos = this.CamReset.Pos;
			}
			else if (mode == num++)
			{
				base.transform.rotation = this.CamReset.RotQ;
			}
			else if (mode == num++)
			{
				this.CamDat.Dir = this.CamReset.Dir;
			}
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0014B5C4 File Offset: 0x001499C4
		public void TargetSet(Transform target, bool isReset)
		{
			if (target)
			{
				this.lookAtTarget = target;
			}
			if (this.lookAtTarget)
			{
				this.CamDat.Pos = this.lookAtTarget.localPosition;
			}
			Transform transform = base.transform;
			this.CamDat.Dir = Vector3.zero;
			this.CamDat.Dir.z = -Vector3.Distance(this.LookAt.position, transform.position);
			transform.LookAt(this.CamDat.Pos);
			this.CamDat.Rot = base.transform.rotation.eulerAngles;
			if (isReset)
			{
				this.CamReset.Copy(this.CamDat, base.transform.rotation);
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x0014B698 File Offset: 0x00149A98
		// (set) Token: 0x06005398 RID: 21400 RVA: 0x0014B6A5 File Offset: 0x00149AA5
		public Vector3 TargetPos
		{
			get
			{
				return this.CamDat.Pos;
			}
			set
			{
				this.CamDat.Pos = value;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x0014B6B3 File Offset: 0x00149AB3
		// (set) Token: 0x0600539A RID: 21402 RVA: 0x0014B6C0 File Offset: 0x00149AC0
		public Vector3 CameraAngle
		{
			get
			{
				return this.CamDat.Rot;
			}
			set
			{
				base.transform.rotation = Quaternion.Euler(value);
				this.CamDat.Rot = value;
			}
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0014B6E0 File Offset: 0x00149AE0
		public void CameraDataSave(string _strCreateAssetPath, string _strFile)
		{
			FileData fileData = new FileData(string.Empty);
			string path = fileData.Create(_strCreateAssetPath) + _strFile + ".txt";
			using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8")))
			{
				streamWriter.Write(this.CamDat.Pos.x);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Pos.y);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Pos.z);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Dir.x);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Dir.y);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Dir.z);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Rot.x);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Rot.y);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Rot.z);
				streamWriter.Write('\n');
				streamWriter.Write(this.CamDat.Fov);
				streamWriter.Write('\n');
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0014B874 File Offset: 0x00149C74
		public bool CameraDataLoad(string _assetbundleFolder, string _strFile, bool _isDirect = false)
		{
			string text = string.Empty;
			if (!_isDirect)
			{
				text = GlobalMethod.LoadAllListText(_assetbundleFolder, _strFile, null);
			}
			else
			{
				TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(_assetbundleFolder, _strFile, false, string.Empty);
				AssetBundleManager.UnloadAssetBundle(_assetbundleFolder, true, null, false);
				if (textAsset)
				{
					text = textAsset.text;
				}
			}
			if (text == string.Empty)
			{
				GlobalMethod.DebugLog("cameraファイル読み込めません", 1);
				return false;
			}
			string[][] array;
			GlobalMethod.GetListString(text, out array);
			this.CamDat.Pos.x = float.Parse(array[0][0]);
			this.CamDat.Pos.y = float.Parse(array[1][0]);
			this.CamDat.Pos.z = float.Parse(array[2][0]);
			this.CamDat.Dir.x = float.Parse(array[3][0]);
			this.CamDat.Dir.y = float.Parse(array[4][0]);
			this.CamDat.Dir.z = float.Parse(array[5][0]);
			this.CamDat.Rot.x = float.Parse(array[6][0]);
			this.CamDat.Rot.y = float.Parse(array[7][0]);
			this.CamDat.Rot.z = float.Parse(array[8][0]);
			this.CamDat.Fov = float.Parse(array[9][0]);
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = this.CamDat.Fov;
			}
			this.CamReset.Copy(this.CamDat, Quaternion.identity);
			this.ForceMoveCam(0f);
			if (!this.isInit)
			{
				this.isInit = true;
			}
			return true;
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0014BA44 File Offset: 0x00149E44
		public bool CameraResetDataLoad(string _assetbundleFolder, string _strFile, bool _isDirect = false)
		{
			string text = string.Empty;
			if (!_isDirect)
			{
				text = GlobalMethod.LoadAllListText(_assetbundleFolder, _strFile, null);
			}
			else
			{
				TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(_assetbundleFolder, _strFile, false, string.Empty);
				AssetBundleManager.UnloadAssetBundle(_assetbundleFolder, true, null, false);
				if (textAsset)
				{
					text = textAsset.text;
				}
			}
			if (text == string.Empty)
			{
				GlobalMethod.DebugLog("cameraファイル読み込めません", 1);
				return false;
			}
			string[][] array;
			GlobalMethod.GetListString(text, out array);
			VirtualCameraController.CameraData copy = default(VirtualCameraController.CameraData);
			copy.Pos.x = float.Parse(array[0][0]);
			copy.Pos.y = float.Parse(array[1][0]);
			copy.Pos.z = float.Parse(array[2][0]);
			copy.Dir.x = float.Parse(array[3][0]);
			copy.Dir.y = float.Parse(array[4][0]);
			copy.Dir.z = float.Parse(array[5][0]);
			copy.Rot.x = float.Parse(array[6][0]);
			copy.Rot.y = float.Parse(array[7][0]);
			copy.Rot.z = float.Parse(array[8][0]);
			copy.Fov = float.Parse(array[9][0]);
			this.CamReset.Copy(copy, Quaternion.identity);
			return true;
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0014BBAC File Offset: 0x00149FAC
		public void CameraDataReset()
		{
			this.CamDat.Pos = Vector3.zero;
			this.CamDat.Dir = Vector3.zero;
			this.CamDat.Rot = Vector3.zero;
			this.CamDat.Pos = this.LookAt.localPosition;
			this.CamDat.Dir.z = -Vector3.Distance(this.LookAt.position, base.transform.position);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0014BC2C File Offset: 0x0014A02C
		public bool loadVanish()
		{
			this.lstMapVanish.Clear();
			List<Map.VisibleObject> list = Singleton<Map>.Instance.LstMapVanish;
			for (int i = 0; i < list.Count; i++)
			{
				int index = i;
				if (!(list[index].collider == null) && list[index].collider.gameObject.activeSelf)
				{
					VirtualCameraController.VisibleObjectH visibleObjectH = new VirtualCameraController.VisibleObjectH();
					visibleObjectH.nameCollider = list[index].nameCollider;
					visibleObjectH.collider = list[index].collider;
					visibleObjectH.vanishObj = list[index].vanishObj;
					visibleObjectH.initEnable = list[index].collider.enabled;
					this.lstMapVanish.Add(visibleObjectH);
					visibleObjectH.collider.enabled = true;
				}
			}
			return true;
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0014BD0C File Offset: 0x0014A10C
		public void visibleForceVanish(bool _visible)
		{
			foreach (VirtualCameraController.VisibleObjectH visibleObjectH in this.lstMapVanish)
			{
				if (visibleObjectH.vanishObj != null)
				{
					visibleObjectH.vanishObj.SetActive(_visible);
				}
				visibleObjectH.isVisible = _visible;
				visibleObjectH.delay = ((!_visible) ? 0f : 0.3f);
			}
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0014BDA0 File Offset: 0x0014A1A0
		private void VisibleForceVanish(VirtualCameraController.VisibleObjectH _obj, bool _visible)
		{
			if (_obj == null)
			{
				return;
			}
			if (_obj.vanishObj == null)
			{
				return;
			}
			_obj.vanishObj.SetActive(_visible);
			_obj.delay = ((!_visible) ? 0f : 0.3f);
			_obj.isVisible = _visible;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0014BDF4 File Offset: 0x0014A1F4
		private bool VanishProc()
		{
			if (!this.isConfigVanish)
			{
				return false;
			}
			int i;
			for (i = 0; i < this.lstMapVanish.Count; i++)
			{
				List<Collider> list = this.listCollider.FindAll((Collider x) => x != null && this.lstMapVanish[i].nameCollider == x.name);
				if (list == null || list.Count == 0)
				{
					this.VanishDelayVisible(this.lstMapVanish[i]);
				}
				else if (this.lstMapVanish[i].isVisible)
				{
					this.VisibleForceVanish(this.lstMapVanish[i], false);
				}
			}
			if (this.viewCollider != null)
			{
				if (this.housingMgr != null)
				{
					this.housingMgr.ShieldProc(this.viewCollider);
				}
			}
			return true;
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0014BEF9 File Offset: 0x0014A2F9
		private bool VanishDelayVisible(VirtualCameraController.VisibleObjectH _visible)
		{
			if (_visible.isVisible)
			{
				return false;
			}
			_visible.delay += Time.deltaTime;
			if (_visible.delay >= 0.3f)
			{
				this.VisibleForceVanish(_visible, true);
			}
			return true;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0014BF33 File Offset: 0x0014A333
		protected override void OnDisable()
		{
			base.OnDisable();
			this.visibleForceVanish(true);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0014BF44 File Offset: 0x0014A344
		protected void OnTriggerEnter(Collider other)
		{
			if (this.listCollider.FindAll((Collider x) => x != null && other.name == x.name) == null)
			{
				this.listCollider.Add(other);
			}
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x0014BF90 File Offset: 0x0014A390
		protected void OnTriggerStay(Collider other)
		{
			Collider x2 = this.listCollider.Find((Collider x) => x != null && other.name == x.name);
			if (x2 == null)
			{
				this.listCollider.Add(other);
			}
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0014BFDF File Offset: 0x0014A3DF
		protected void OnTriggerExit(Collider other)
		{
			this.listCollider.Clear();
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0014BFEC File Offset: 0x0014A3EC
		public void ResetVanish()
		{
			for (int i = 0; i < this.lstMapVanish.Count; i++)
			{
				if (!(this.lstMapVanish[i].collider == null))
				{
					this.lstMapVanish[i].collider.enabled = this.lstMapVanish[i].initEnable;
				}
			}
		}

		// Token: 0x04004E15 RID: 19989
		protected Manager.Input input;

		// Token: 0x04004E17 RID: 19991
		[SerializeField]
		protected Transform follow;

		// Token: 0x04004E18 RID: 19992
		[SerializeField]
		protected Transform lookAtTarget;

		// Token: 0x04004E19 RID: 19993
		protected Renderer targetRender;

		// Token: 0x04004E1A RID: 19994
		protected CameraState m_State = CameraState.Default;

		// Token: 0x04004E1B RID: 19995
		public float xRotSpeed = 0.5f;

		// Token: 0x04004E1C RID: 19996
		public float yRotSpeed = 0.5f;

		// Token: 0x04004E1D RID: 19997
		public float moveSpeed = 0.5f;

		// Token: 0x04004E1E RID: 19998
		public float keyMoveSpeed = 5f;

		// Token: 0x04004E1F RID: 19999
		public float limitFov = 40f;

		// Token: 0x04004E20 RID: 20000
		public bool isLimitPos;

		// Token: 0x04004E21 RID: 20001
		public bool isLimitDir;

		// Token: 0x04004E22 RID: 20002
		public bool isLimitRotX;

		// Token: 0x04004E23 RID: 20003
		public float limitRotX = 90f;

		// Token: 0x04004E24 RID: 20004
		public float limitPos;

		// Token: 0x04004E25 RID: 20005
		public float limitDir;

		// Token: 0x04004E26 RID: 20006
		protected bool isZoomNow;

		// Token: 0x04004E27 RID: 20007
		protected Housing housingMgr;

		// Token: 0x04004E28 RID: 20008
		private const float INIT_FOV = 23f;

		// Token: 0x04004E2C RID: 20012
		protected bool isConfigVanish = true;

		// Token: 0x04004E2D RID: 20013
		[SerializeField]
		protected List<VirtualCameraController.VisibleObjectH> lstMapVanish = new List<VirtualCameraController.VisibleObjectH>();

		// Token: 0x04004E2E RID: 20014
		[SerializeField]
		protected List<Collider> listCollider = new List<Collider>();

		// Token: 0x04004E30 RID: 20016
		public readonly int CONFIG_SIZE = Enum.GetNames(typeof(VirtualCameraController.Config)).Length;

		// Token: 0x04004E31 RID: 20017
		[SerializeField]
		protected VirtualCameraController.CameraData CamDat;

		// Token: 0x04004E32 RID: 20018
		protected VirtualCameraController.Config cameraType = VirtualCameraController.Config.Rotation;

		// Token: 0x04004E33 RID: 20019
		protected bool[] isDrags;

		// Token: 0x04004E34 RID: 20020
		protected bool[] isButtons;

		// Token: 0x04004E35 RID: 20021
		protected VirtualCameraController.ResetData CamReset;

		// Token: 0x04004E36 RID: 20022
		protected bool isInit;

		// Token: 0x04004E37 RID: 20023
		protected CapsuleCollider viewCollider;

		// Token: 0x04004E38 RID: 20024
		protected float rateAddSpeed = 1f;

		// Token: 0x04004E39 RID: 20025
		public VirtualCameraController.NoCtrlFunc NoCtrlCondition;

		// Token: 0x04004E3A RID: 20026
		public VirtualCameraController.NoCtrlFunc ZoomCondition;

		// Token: 0x04004E3B RID: 20027
		public VirtualCameraController.NoCtrlFunc KeyCondition;

		// Token: 0x04004E3C RID: 20028
		public LensSettings Lens;

		// Token: 0x04004E3D RID: 20029
		protected bool craft;

		// Token: 0x04004E3E RID: 20030
		public const string PipelineName = "cm";

		// Token: 0x02000B21 RID: 2849
		[Serializable]
		public class VisibleObjectH : Map.VisibleObject
		{
			// Token: 0x04004E3F RID: 20031
			public bool initEnable;
		}

		// Token: 0x02000B22 RID: 2850
		[Serializable]
		protected struct CameraData
		{
			// Token: 0x060053AA RID: 21418 RVA: 0x0014C07F File Offset: 0x0014A47F
			public void Copy(VirtualCameraController.ResetData copy)
			{
				this.Pos = copy.Pos;
				this.Dir = copy.Dir;
				this.Rot = copy.Rot;
				this.Fov = copy.Fov;
			}

			// Token: 0x04004E40 RID: 20032
			public Vector3 Pos;

			// Token: 0x04004E41 RID: 20033
			public Vector3 Dir;

			// Token: 0x04004E42 RID: 20034
			public Vector3 Rot;

			// Token: 0x04004E43 RID: 20035
			[HideInInspector]
			public float Fov;
		}

		// Token: 0x02000B23 RID: 2851
		protected struct ResetData
		{
			// Token: 0x060053AB RID: 21419 RVA: 0x0014C0B5 File Offset: 0x0014A4B5
			public void Copy(VirtualCameraController.CameraData copy, Quaternion rot)
			{
				this.Pos = copy.Pos;
				this.Dir = copy.Dir;
				this.Rot = copy.Rot;
				this.RotQ = rot;
				this.Fov = copy.Fov;
			}

			// Token: 0x04004E44 RID: 20036
			public Vector3 Pos;

			// Token: 0x04004E45 RID: 20037
			public Vector3 Dir;

			// Token: 0x04004E46 RID: 20038
			public Vector3 Rot;

			// Token: 0x04004E47 RID: 20039
			public Quaternion RotQ;

			// Token: 0x04004E48 RID: 20040
			public float Fov;
		}

		// Token: 0x02000B24 RID: 2852
		public enum Config
		{
			// Token: 0x04004E4A RID: 20042
			MoveXZ,
			// Token: 0x04004E4B RID: 20043
			Rotation,
			// Token: 0x04004E4C RID: 20044
			Translation,
			// Token: 0x04004E4D RID: 20045
			MoveXY
		}

		// Token: 0x02000B25 RID: 2853
		// (Invoke) Token: 0x060053AD RID: 21421
		public delegate bool NoCtrlFunc();
	}
}
