using System;
using System.Linq;
using Manager;
using UnityEngine;

// Token: 0x02000813 RID: 2067
public class BaseCameraControl_Ver2 : MonoBehaviour
{
	// Token: 0x0600348B RID: 13451 RVA: 0x001353D0 File Offset: 0x001337D0
	public BaseCameraControl_Ver2()
	{
		this.CamDat.Fov = 23f;
		this.CamReset.Fov = 23f;
	}

	// Token: 0x17000982 RID: 2434
	// (get) Token: 0x0600348C RID: 13452 RVA: 0x001354B0 File Offset: 0x001338B0
	// (set) Token: 0x0600348D RID: 13453 RVA: 0x001354B8 File Offset: 0x001338B8
	public Camera thisCamera { get; protected set; }

	// Token: 0x17000983 RID: 2435
	// (get) Token: 0x0600348E RID: 13454 RVA: 0x001354C1 File Offset: 0x001338C1
	// (set) Token: 0x0600348F RID: 13455 RVA: 0x001354C9 File Offset: 0x001338C9
	public bool isControlNow { get; protected set; }

	// Token: 0x17000984 RID: 2436
	// (get) Token: 0x06003490 RID: 13456 RVA: 0x001354D2 File Offset: 0x001338D2
	// (set) Token: 0x06003491 RID: 13457 RVA: 0x001354DA File Offset: 0x001338DA
	public BaseCameraControl_Ver2.Config CameraType
	{
		get
		{
			return this.cameraType;
		}
		set
		{
			this.cameraType = value;
		}
	}

	// Token: 0x17000985 RID: 2437
	// (get) Token: 0x06003492 RID: 13458 RVA: 0x001354E3 File Offset: 0x001338E3
	// (set) Token: 0x06003493 RID: 13459 RVA: 0x001354F0 File Offset: 0x001338F0
	public float CameraInitFov
	{
		get
		{
			return this.CamReset.Fov;
		}
		set
		{
			this.CamReset.Fov = value;
			this.CamDat.Fov = value;
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = value;
			}
		}
	}

	// Token: 0x17000986 RID: 2438
	// (get) Token: 0x06003494 RID: 13460 RVA: 0x00135527 File Offset: 0x00133927
	// (set) Token: 0x06003495 RID: 13461 RVA: 0x00135534 File Offset: 0x00133934
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

	// Token: 0x17000987 RID: 2439
	// (get) Token: 0x06003496 RID: 13462 RVA: 0x00135542 File Offset: 0x00133942
	// (set) Token: 0x06003497 RID: 13463 RVA: 0x0013554F File Offset: 0x0013394F
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

	// Token: 0x17000988 RID: 2440
	// (set) Token: 0x06003498 RID: 13464 RVA: 0x0013556E File Offset: 0x0013396E
	public Vector3 Rot
	{
		set
		{
			this.CamDat.Rot = value;
		}
	}

	// Token: 0x17000989 RID: 2441
	// (get) Token: 0x06003499 RID: 13465 RVA: 0x0013557C File Offset: 0x0013397C
	// (set) Token: 0x0600349A RID: 13466 RVA: 0x00135589 File Offset: 0x00133989
	public Vector3 CameraDir
	{
		get
		{
			return this.CamDat.Dir;
		}
		set
		{
			this.CamDat.Dir = value;
		}
	}

	// Token: 0x1700098A RID: 2442
	// (get) Token: 0x0600349B RID: 13467 RVA: 0x00135597 File Offset: 0x00133997
	// (set) Token: 0x0600349C RID: 13468 RVA: 0x001355A4 File Offset: 0x001339A4
	public float CameraFov
	{
		get
		{
			return this.CamDat.Fov;
		}
		set
		{
			this.CamDat.Fov = value;
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = value;
			}
		}
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x001355D0 File Offset: 0x001339D0
	public void Reset(int mode)
	{
		int num = 0;
		if (mode == num++)
		{
			this.CamDat.Copy(this.CamReset);
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = this.CamDat.Fov;
			}
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

	// Token: 0x0600349E RID: 13470 RVA: 0x00135694 File Offset: 0x00133A94
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
			Touch touch = UnityEngine.Input.touches.First<Touch>();
			TouchPhase phase = touch.phase;
			if (phase != TouchPhase.Began)
			{
				if (phase == TouchPhase.Moved)
				{
					float num2 = 0.1f;
					float num3 = 0.01f;
					Vector3 vector = Vector3.zero;
					if (this.cameraType == BaseCameraControl_Ver2.Config.Rotation)
					{
						vector.y += touch.deltaPosition.x * this.xRotSpeed * num * num2;
						vector.x -= touch.deltaPosition.y * this.yRotSpeed * num * num2;
						vector += base.transform.rotation.eulerAngles;
						base.transform.rotation = Quaternion.Euler(vector);
					}
					else if (this.cameraType == BaseCameraControl_Ver2.Config.Translation)
					{
						this.CamDat.Dir.z = this.CamDat.Dir.z - touch.deltaPosition.x * this.xRotSpeed * num * num3;
						this.CamDat.Pos.y = this.CamDat.Pos.y + touch.deltaPosition.y * this.yRotSpeed * num * num3;
					}
					else if (this.cameraType == BaseCameraControl_Ver2.Config.MoveXY)
					{
						vector.x = touch.deltaPosition.x * this.xRotSpeed * num * num3;
						vector.y = touch.deltaPosition.y * this.yRotSpeed * num * num3;
						this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(vector);
					}
					else if (this.cameraType == BaseCameraControl_Ver2.Config.MoveXZ)
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

	// Token: 0x0600349F RID: 13471 RVA: 0x00135918 File Offset: 0x00133D18
	protected bool InputMouseWheelZoomProc()
	{
		bool result = false;
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed;
		if (num != 0f)
		{
			this.CamDat.Dir.z = this.CamDat.Dir.z + num;
			this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
			result = true;
		}
		return result;
	}

	// Token: 0x060034A0 RID: 13472 RVA: 0x0013598C File Offset: 0x00133D8C
	protected virtual bool InputMouseProc()
	{
		bool result = false;
		bool[] array = new bool[this.CONFIG_SIZE];
		array[1] = UnityEngine.Input.GetMouseButton(0);
		array[2] = UnityEngine.Input.GetMouseButton(1);
		array[3] = UnityEngine.Input.GetMouseButton(2);
		array[0] = (UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.GetMouseButton(1));
		for (int i = 0; i < this.CONFIG_SIZE; i++)
		{
			if (array[i])
			{
				this.isDrags[i] = true;
			}
		}
		for (int j = 0; j < this.CONFIG_SIZE; j++)
		{
			if (this.isDrags[j] && !array[j])
			{
				this.isDrags[j] = false;
			}
		}
		float axis = UnityEngine.Input.GetAxis("Mouse X");
		float axis2 = UnityEngine.Input.GetAxis("Mouse Y");
		for (int k = 0; k < this.CONFIG_SIZE; k++)
		{
			if (this.isDrags[k])
			{
				Vector3 zero = Vector3.zero;
				if (k == 0)
				{
					zero.x = axis * this.moveSpeed * this.rateAddSpeed;
					zero.z = axis2 * this.moveSpeed * this.rateAddSpeed;
					if (this.transBase != null)
					{
						this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(zero));
					}
					else
					{
						this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
					}
				}
				else if (k == 1)
				{
					zero.y += axis * this.xRotSpeed * this.rateAddSpeed;
					zero.x -= axis2 * this.yRotSpeed * this.rateAddSpeed;
					this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
					this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
				}
				else if (k == 2)
				{
					this.CamDat.Pos.y = this.CamDat.Pos.y + axis2 * this.moveSpeed * this.rateAddSpeed;
					this.CamDat.Dir.z = this.CamDat.Dir.z - axis * this.moveSpeed * this.rateAddSpeed;
					this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
				}
				else if (k == 3)
				{
					zero.x = axis * this.moveSpeed * this.rateAddSpeed;
					zero.y = axis2 * this.moveSpeed * this.rateAddSpeed;
					if (this.transBase != null)
					{
						this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(zero));
					}
					else
					{
						this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
					}
				}
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x060034A1 RID: 13473 RVA: 0x00135CF0 File Offset: 0x001340F0
	protected bool InputKeyProc()
	{
		bool flag = false;
		if (UnityEngine.Input.GetKeyDown(KeyCode.R))
		{
			this.Reset(0);
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad5))
		{
			this.CamDat.Rot.x = this.CamReset.Rot.x;
			this.CamDat.Rot.y = this.CamReset.Rot.y;
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Slash))
		{
			this.CamDat.Rot.z = 0f;
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Semicolon))
		{
			this.CamDat.Fov = this.CamReset.Fov;
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = this.CamDat.Fov;
			}
		}
		float num = Time.deltaTime * this.keySpeed;
		if (UnityEngine.Input.GetKey(KeyCode.Home))
		{
			flag = true;
			this.CamDat.Dir.z = this.CamDat.Dir.z + num;
			this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
		}
		else if (UnityEngine.Input.GetKey(KeyCode.End))
		{
			flag = true;
			this.CamDat.Dir.z = this.CamDat.Dir.z - num;
		}
		if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
		{
			flag = true;
			if (this.transBase != null)
			{
				this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(num, 0f, 0f)));
			}
			else
			{
				this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(num, 0f, 0f));
			}
		}
		else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
		{
			flag = true;
			if (this.transBase != null)
			{
				this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(-num, 0f, 0f)));
			}
			else
			{
				this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(-num, 0f, 0f));
			}
		}
		if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
		{
			flag = true;
			if (this.transBase != null)
			{
				this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, num)));
			}
			else
			{
				this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, num));
			}
		}
		else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
		{
			flag = true;
			if (this.transBase != null)
			{
				this.CamDat.Pos = this.CamDat.Pos + this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, -num)));
			}
			else
			{
				this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, -num));
			}
		}
		if (UnityEngine.Input.GetKey(KeyCode.PageUp))
		{
			flag = true;
			this.CamDat.Pos.y = this.CamDat.Pos.y + num;
		}
		else if (UnityEngine.Input.GetKey(KeyCode.PageDown))
		{
			flag = true;
			this.CamDat.Pos.y = this.CamDat.Pos.y - num;
		}
		float num2 = 10f * Time.deltaTime;
		Vector3 zero = Vector3.zero;
		if (UnityEngine.Input.GetKey(KeyCode.Period))
		{
			flag = true;
			zero.z += num2;
		}
		else if (UnityEngine.Input.GetKey(KeyCode.Backslash))
		{
			flag = true;
			zero.z -= num2;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Keypad2))
		{
			flag = true;
			zero.x -= num2 * this.yRotSpeed;
		}
		else if (UnityEngine.Input.GetKey(KeyCode.Keypad8))
		{
			flag = true;
			zero.x += num2 * this.yRotSpeed;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Keypad4))
		{
			flag = true;
			zero.y += num2 * this.xRotSpeed;
		}
		else if (UnityEngine.Input.GetKey(KeyCode.Keypad6))
		{
			flag = true;
			zero.y -= num2 * this.xRotSpeed;
		}
		if (flag)
		{
			this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
			this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
			this.CamDat.Rot.z = (this.CamDat.Rot.z + zero.z) % 360f;
		}
		float deltaTime = Time.deltaTime;
		if (UnityEngine.Input.GetKey(KeyCode.Equals))
		{
			flag = true;
			this.CamDat.Fov = Mathf.Max(this.CamDat.Fov - deltaTime * 15f, this.limitFovMin);
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = this.CamDat.Fov;
			}
		}
		else if (UnityEngine.Input.GetKey(KeyCode.RightBracket))
		{
			flag = true;
			this.CamDat.Fov = Mathf.Min(this.CamDat.Fov + deltaTime * 15f, this.limitFov);
			if (this.thisCamera != null)
			{
				this.thisCamera.fieldOfView = this.CamDat.Fov;
			}
		}
		return flag;
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x00136370 File Offset: 0x00134770
	protected void Start()
	{
		this.SetCtrlSpeed();
		this.thisCamera = base.GetComponent<Camera>();
		if (this.thisCamera != null)
		{
			this.thisCamera.fieldOfView = this.CamReset.Fov;
		}
		this.ZoomCondition = (() => false);
		this.isControlNow = false;
		this.isDrags = new bool[this.CONFIG_SIZE];
		for (int i = 0; i < this.isDrags.Length; i++)
		{
			this.isDrags[i] = false;
		}
		if (this.isInit)
		{
			return;
		}
		if (!this.targetObj)
		{
			Vector3 a = base.transform.TransformDirection(Vector3.forward);
			this.CamDat.Pos = base.transform.position + a * this.noneTargetDir;
		}
		this.TargetSet(this.targetObj, true);
	}

	// Token: 0x060034A3 RID: 13475 RVA: 0x00136478 File Offset: 0x00134878
	protected void LateUpdate()
	{
		this.isControlNow = false;
		this.SetCtrlSpeed();
		if (!this.isControlNow)
		{
			BaseCameraControl_Ver2.NoCtrlFunc zoomCondition = this.ZoomCondition;
			bool flag = true;
			if (zoomCondition != null)
			{
				flag = zoomCondition();
			}
			this.isControlNow |= (flag && this.InputMouseWheelZoomProc());
		}
		if (!this.isControlNow)
		{
			BaseCameraControl_Ver2.NoCtrlFunc noCtrlCondition = this.NoCtrlCondition;
			bool flag2 = false;
			if (noCtrlCondition != null)
			{
				flag2 = noCtrlCondition();
			}
			if (!flag2)
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
			BaseCameraControl_Ver2.NoCtrlFunc keyCondition = this.KeyCondition;
			bool flag3 = true;
			if (keyCondition != null)
			{
				flag3 = keyCondition();
			}
			this.isControlNow |= (flag3 && this.InputKeyProc());
		}
		this.CameraUpdate();
	}

	// Token: 0x060034A4 RID: 13476 RVA: 0x0013656C File Offset: 0x0013496C
	protected void CameraUpdate()
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
		this.viewCollider.height = this.CamDat.Dir.z;
		this.viewCollider.center = -Vector3.forward * this.CamDat.Dir.z * 0.5f;
	}

	// Token: 0x060034A5 RID: 13477 RVA: 0x00136704 File Offset: 0x00134B04
	public void TargetSet(Transform target, bool isReset)
	{
		if (target)
		{
			this.targetObj = target;
		}
		if (this.targetObj)
		{
			this.CamDat.Pos = this.targetObj.position;
		}
		Transform transform = base.transform;
		this.CamDat.Dir = Vector3.zero;
		this.CamDat.Dir.z = -Vector3.Distance(this.CamDat.Pos, transform.position);
		transform.LookAt(this.CamDat.Pos);
		this.CamDat.Rot = base.transform.rotation.eulerAngles;
		if (isReset)
		{
			this.CamReset.Copy(this.CamDat, base.transform.rotation);
		}
	}

	// Token: 0x060034A6 RID: 13478 RVA: 0x001367D8 File Offset: 0x00134BD8
	public void FrontTarget(Transform target, bool isReset, float dir = -3.4028235E+38f)
	{
		if (target)
		{
			this.targetObj = target;
		}
		if (this.targetObj)
		{
			target = this.targetObj;
			this.CamDat.Pos = target.position;
		}
		if (!target)
		{
			return;
		}
		if (dir != -3.4028235E+38f)
		{
			this.CamDat.Dir = Vector3.zero;
			this.CamDat.Dir.z = -dir;
		}
		Transform transform = base.transform;
		transform.position = target.position;
		transform.rotation.eulerAngles.Set(this.CamDat.Rot.x, this.CamDat.Rot.y, this.CamDat.Rot.z);
		transform.position += transform.forward * this.CamDat.Dir.z;
		transform.LookAt(this.CamDat.Pos);
		this.CamDat.Rot = base.transform.rotation.eulerAngles;
		if (isReset)
		{
			this.CamReset.Copy(this.CamDat, base.transform.rotation);
		}
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x00136930 File Offset: 0x00134D30
	public void SetCamera(BaseCameraControl_Ver2 src)
	{
		base.transform.position = src.transform.position;
		base.transform.rotation = src.transform.rotation;
		this.CamDat = src.CamDat;
		this.CamDat.Pos = -(base.transform.rotation * this.CamDat.Dir - base.transform.position);
		this.CamReset.Copy(this.CamDat, base.transform.rotation);
		if (this.thisCamera != null && src.thisCamera != null)
		{
			this.thisCamera.CopyFrom(src.thisCamera);
		}
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x00136A00 File Offset: 0x00134E00
	public void SetCamera(Vector3 pos, Vector3 angle, Quaternion rot, Vector3 dir)
	{
		base.transform.localPosition = pos;
		base.transform.localRotation = rot;
		this.CamDat.Rot = angle;
		this.CamDat.Dir = dir;
		this.CamDat.Pos = -(base.transform.localRotation * this.CamDat.Dir - base.transform.localPosition);
		this.CamReset.Copy(this.CamDat, base.transform.rotation);
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x00136A98 File Offset: 0x00134E98
	public void CopyCamera(BaseCameraControl_Ver2 dest)
	{
		dest.transform.position = base.transform.position;
		dest.transform.rotation = base.transform.rotation;
		dest.CamDat = this.CamDat;
		dest.CamDat.Pos = -(dest.transform.rotation * dest.CamDat.Dir - dest.transform.position);
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x00136B18 File Offset: 0x00134F18
	public void CopyInstance(BaseCameraControl_Ver2 src)
	{
		this.isInit = true;
		this.targetObj = src.targetObj;
		this.xRotSpeed = src.xRotSpeed;
		this.yRotSpeed = src.yRotSpeed;
		this.zoomSpeed = src.zoomSpeed;
		this.moveSpeed = src.moveSpeed;
		this.keySpeed = src.keySpeed;
		this.noneTargetDir = src.noneTargetDir;
		this.NoCtrlCondition = src.NoCtrlCondition;
		this.ZoomCondition = src.ZoomCondition;
		this.KeyCondition = src.KeyCondition;
		if (this.thisCamera != null && src.thisCamera != null)
		{
			this.thisCamera.CopyFrom(src.thisCamera);
		}
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x00136BD8 File Offset: 0x00134FD8
	private void OnDrawGizmos()
	{
		Gizmos.color = ((this.CamDat.Dir.z <= 0f) ? Color.blue : Color.red);
		Vector3 direction;
		if (this.transBase != null)
		{
			direction = this.transBase.TransformPoint(this.CamDat.Pos) - base.transform.position;
		}
		else
		{
			direction = this.CamDat.Pos - base.transform.position;
		}
		Gizmos.DrawRay(base.transform.position, direction);
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x00136C80 File Offset: 0x00135080
	public bool SetBase(Transform _trans)
	{
		if (this.transBase == null)
		{
			return false;
		}
		this.transBase.transform.position = _trans.position;
		this.transBase.transform.rotation = _trans.rotation;
		return true;
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x00136CCD File Offset: 0x001350CD
	public bool SetCtrlSpeed()
	{
		if (Singleton<Manager.Config>.IsInstance())
		{
		}
		return true;
	}

	// Token: 0x04003534 RID: 13620
	public Transform transBase;

	// Token: 0x04003535 RID: 13621
	public Transform targetObj;

	// Token: 0x04003536 RID: 13622
	public float xRotSpeed = 5f;

	// Token: 0x04003537 RID: 13623
	public float yRotSpeed = 5f;

	// Token: 0x04003538 RID: 13624
	public float zoomSpeed = 5f;

	// Token: 0x04003539 RID: 13625
	public float moveSpeed = 0.05f;

	// Token: 0x0400353A RID: 13626
	public float keySpeed = 10f;

	// Token: 0x0400353B RID: 13627
	public float noneTargetDir = 5f;

	// Token: 0x0400353C RID: 13628
	public float rateSpeedMin = 0.5f;

	// Token: 0x0400353D RID: 13629
	public float rateSpeedMax = 2f;

	// Token: 0x0400353E RID: 13630
	public bool isLimitPos;

	// Token: 0x0400353F RID: 13631
	public float limitPos = 2f;

	// Token: 0x04003540 RID: 13632
	public bool isLimitDir;

	// Token: 0x04003541 RID: 13633
	public float limitDir = 10f;

	// Token: 0x04003542 RID: 13634
	public float limitFovMin = 10f;

	// Token: 0x04003543 RID: 13635
	public float limitFov = 40f;

	// Token: 0x04003544 RID: 13636
	public BaseCameraControl_Ver2.NoCtrlFunc NoCtrlCondition;

	// Token: 0x04003545 RID: 13637
	public BaseCameraControl_Ver2.NoCtrlFunc ZoomCondition;

	// Token: 0x04003546 RID: 13638
	public BaseCameraControl_Ver2.NoCtrlFunc KeyCondition;

	// Token: 0x04003548 RID: 13640
	public readonly int CONFIG_SIZE = Enum.GetNames(typeof(BaseCameraControl_Ver2.Config)).Length;

	// Token: 0x04003549 RID: 13641
	[SerializeField]
	protected BaseCameraControl_Ver2.CameraData CamDat;

	// Token: 0x0400354A RID: 13642
	protected BaseCameraControl_Ver2.Config cameraType = BaseCameraControl_Ver2.Config.Rotation;

	// Token: 0x0400354B RID: 13643
	protected bool[] isDrags;

	// Token: 0x0400354C RID: 13644
	protected BaseCameraControl_Ver2.ResetData CamReset;

	// Token: 0x0400354D RID: 13645
	protected bool isInit;

	// Token: 0x0400354E RID: 13646
	private const float INIT_FOV = 23f;

	// Token: 0x0400354F RID: 13647
	protected CapsuleCollider viewCollider;

	// Token: 0x04003550 RID: 13648
	protected float rateAddSpeed = 1f;

	// Token: 0x02000814 RID: 2068
	// (Invoke) Token: 0x060034B0 RID: 13488
	public delegate bool NoCtrlFunc();

	// Token: 0x02000815 RID: 2069
	[Serializable]
	protected struct CameraData
	{
		// Token: 0x060034B3 RID: 13491 RVA: 0x00136CDD File Offset: 0x001350DD
		public void Copy(BaseCameraControl_Ver2.ResetData copy)
		{
			this.Pos = copy.Pos;
			this.Dir = copy.Dir;
			this.Rot = copy.Rot;
			this.Fov = copy.Fov;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x00136D13 File Offset: 0x00135113
		public void Copy(BaseCameraControl_Ver2.CameraData _src)
		{
			this.Pos = _src.Pos;
			this.Dir = _src.Dir;
			this.Rot = _src.Rot;
			this.Fov = _src.Fov;
		}

		// Token: 0x04003552 RID: 13650
		public Vector3 Pos;

		// Token: 0x04003553 RID: 13651
		public Vector3 Dir;

		// Token: 0x04003554 RID: 13652
		public Vector3 Rot;

		// Token: 0x04003555 RID: 13653
		public float Fov;
	}

	// Token: 0x02000816 RID: 2070
	protected struct ResetData
	{
		// Token: 0x060034B5 RID: 13493 RVA: 0x00136D49 File Offset: 0x00135149
		public void Copy(BaseCameraControl_Ver2.CameraData copy, Quaternion rot)
		{
			this.Pos = copy.Pos;
			this.Dir = copy.Dir;
			this.Rot = copy.Rot;
			this.RotQ = rot;
			this.Fov = copy.Fov;
		}

		// Token: 0x04003556 RID: 13654
		public Vector3 Pos;

		// Token: 0x04003557 RID: 13655
		public Vector3 Dir;

		// Token: 0x04003558 RID: 13656
		public Vector3 Rot;

		// Token: 0x04003559 RID: 13657
		public Quaternion RotQ;

		// Token: 0x0400355A RID: 13658
		public float Fov;
	}

	// Token: 0x02000817 RID: 2071
	public enum Config
	{
		// Token: 0x0400355C RID: 13660
		MoveXZ,
		// Token: 0x0400355D RID: 13661
		Rotation,
		// Token: 0x0400355E RID: 13662
		Translation,
		// Token: 0x0400355F RID: 13663
		MoveXY
	}
}
