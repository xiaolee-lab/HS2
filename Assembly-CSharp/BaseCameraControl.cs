using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020010B1 RID: 4273
public class BaseCameraControl : MonoBehaviour
{
	// Token: 0x06008E96 RID: 36502 RVA: 0x003B4E54 File Offset: 0x003B3254
	public BaseCameraControl()
	{
		this.CamDat.Fov = 23f;
		this.CamReset.Fov = 23f;
	}

	// Token: 0x17001EEF RID: 7919
	// (get) Token: 0x06008E97 RID: 36503 RVA: 0x003B4EE3 File Offset: 0x003B32E3
	// (set) Token: 0x06008E98 RID: 36504 RVA: 0x003B4EEB File Offset: 0x003B32EB
	public bool isControlNow { get; protected set; }

	// Token: 0x17001EF0 RID: 7920
	// (get) Token: 0x06008E99 RID: 36505 RVA: 0x003B4EF4 File Offset: 0x003B32F4
	// (set) Token: 0x06008E9A RID: 36506 RVA: 0x003B4EFC File Offset: 0x003B32FC
	public BaseCameraControl.Config CameraType
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

	// Token: 0x17001EF1 RID: 7921
	// (get) Token: 0x06008E9B RID: 36507 RVA: 0x003B4F05 File Offset: 0x003B3305
	// (set) Token: 0x06008E9C RID: 36508 RVA: 0x003B4F12 File Offset: 0x003B3312
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
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = value;
			}
		}
	}

	// Token: 0x17001EF2 RID: 7922
	// (get) Token: 0x06008E9D RID: 36509 RVA: 0x003B4F49 File Offset: 0x003B3349
	// (set) Token: 0x06008E9E RID: 36510 RVA: 0x003B4F56 File Offset: 0x003B3356
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

	// Token: 0x17001EF3 RID: 7923
	// (get) Token: 0x06008E9F RID: 36511 RVA: 0x003B4F64 File Offset: 0x003B3364
	// (set) Token: 0x06008EA0 RID: 36512 RVA: 0x003B4F71 File Offset: 0x003B3371
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

	// Token: 0x17001EF4 RID: 7924
	// (set) Token: 0x06008EA1 RID: 36513 RVA: 0x003B4F90 File Offset: 0x003B3390
	public Vector3 Rot
	{
		set
		{
			this.CamDat.Rot = value;
		}
	}

	// Token: 0x17001EF5 RID: 7925
	// (get) Token: 0x06008EA2 RID: 36514 RVA: 0x003B4F9E File Offset: 0x003B339E
	// (set) Token: 0x06008EA3 RID: 36515 RVA: 0x003B4FAB File Offset: 0x003B33AB
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

	// Token: 0x17001EF6 RID: 7926
	// (get) Token: 0x06008EA4 RID: 36516 RVA: 0x003B4FB9 File Offset: 0x003B33B9
	// (set) Token: 0x06008EA5 RID: 36517 RVA: 0x003B4FC6 File Offset: 0x003B33C6
	public float CameraFov
	{
		get
		{
			return this.CamDat.Fov;
		}
		set
		{
			this.CamDat.Fov = value;
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = value;
			}
		}
	}

	// Token: 0x06008EA6 RID: 36518 RVA: 0x003B4FF4 File Offset: 0x003B33F4
	public void Reset(int mode)
	{
		int num = 0;
		if (mode == num++)
		{
			this.CamDat.Copy(this.CamReset);
			base.transform.rotation = this.CamReset.RotQ;
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = this.CamDat.Fov;
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

	// Token: 0x06008EA7 RID: 36519 RVA: 0x003B50D0 File Offset: 0x003B34D0
	protected bool InputTouchProc()
	{
		if (Input.touchCount < 1)
		{
			return false;
		}
		float num = 10f * Time.deltaTime;
		if (Input.touchCount == 3)
		{
			this.Reset(0);
		}
		else if (Input.touchCount == 1)
		{
			Touch touch = Input.touches.First<Touch>();
			TouchPhase phase = touch.phase;
			if (phase != TouchPhase.Began)
			{
				if (phase == TouchPhase.Moved)
				{
					float num2 = 0.1f;
					float num3 = 0.01f;
					Vector3 vector = Vector3.zero;
					if (this.cameraType == BaseCameraControl.Config.Rotation)
					{
						vector.y += touch.deltaPosition.x * this.xRotSpeed * num * num2;
						vector.x -= touch.deltaPosition.y * this.yRotSpeed * num * num2;
						vector += base.transform.rotation.eulerAngles;
						base.transform.rotation = Quaternion.Euler(vector);
					}
					else if (this.cameraType == BaseCameraControl.Config.Translation)
					{
						this.CamDat.Dir.z = this.CamDat.Dir.z - touch.deltaPosition.x * this.xRotSpeed * num * num3;
						this.CamDat.Pos.y = this.CamDat.Pos.y + touch.deltaPosition.y * this.yRotSpeed * num * num3;
					}
					else if (this.cameraType == BaseCameraControl.Config.MoveXY)
					{
						vector.x = touch.deltaPosition.x * this.xRotSpeed * num * num3;
						vector.y = touch.deltaPosition.y * this.yRotSpeed * num * num3;
						this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(vector);
					}
					else if (this.cameraType == BaseCameraControl.Config.MoveXZ)
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

	// Token: 0x06008EA8 RID: 36520 RVA: 0x003B5354 File Offset: 0x003B3754
	protected bool InputMouseWheelZoomProc()
	{
		bool result = false;
		float num = Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed;
		if (num != 0f)
		{
			this.CamDat.Dir.z = this.CamDat.Dir.z + num;
			this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
			result = true;
		}
		return result;
	}

	// Token: 0x06008EA9 RID: 36521 RVA: 0x003B53C8 File Offset: 0x003B37C8
	protected virtual bool InputMouseProc()
	{
		bool flag = false;
		bool[] array = new bool[this.CONFIG_SIZE];
		array[1] = Input.GetMouseButton(0);
		array[2] = Input.GetMouseButton(1);
		array[3] = false;
		array[0] = (Input.GetMouseButton(0) && Input.GetMouseButton(1));
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
		float axis = Input.GetAxis("Mouse X");
		float axis2 = Input.GetAxis("Mouse Y");
		for (int k = 0; k < this.CONFIG_SIZE; k++)
		{
			if (this.isDrags[k])
			{
				Vector3 zero = Vector3.zero;
				if (k == 0)
				{
					zero.x = axis * this.moveSpeed;
					zero.z = axis2 * this.moveSpeed;
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
				}
				else if (k == 1)
				{
					zero.y += axis * this.xRotSpeed;
					zero.x -= axis2 * this.yRotSpeed;
					this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
					this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
					base.transform.rotation = Quaternion.Euler(this.CamDat.Rot);
				}
				else if (k == 2)
				{
					this.CamDat.Pos.y = this.CamDat.Pos.y + axis2 * this.moveSpeed;
					this.CamDat.Dir.z = this.CamDat.Dir.z - axis * this.moveSpeed;
					this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
				}
				else if (k == 3)
				{
					zero.x = axis * this.moveSpeed;
					zero.y = axis2 * this.moveSpeed;
					this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(zero);
				}
				flag = true;
				break;
			}
		}
		if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject() && Singleton<GameCursor>.IsInstance())
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

	// Token: 0x06008EAA RID: 36522 RVA: 0x003B56CC File Offset: 0x003B3ACC
	protected bool InputKeyProc()
	{
		bool flag = false;
		if (this.EnableResetKey && Input.GetKeyDown(KeyCode.R))
		{
			this.Reset(0);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			this.CamDat.Rot.x = this.CamReset.Rot.x;
			this.CamDat.Rot.y = this.CamReset.Rot.y;
			base.transform.rotation = Quaternion.Euler(this.CamDat.Rot);
		}
		else if (Input.GetKeyDown(KeyCode.Slash))
		{
			this.CamDat.Rot.z = 0f;
			base.transform.rotation = Quaternion.Euler(this.CamDat.Rot);
		}
		else if (Input.GetKeyDown(KeyCode.Semicolon))
		{
			this.CamDat.Fov = this.CamReset.Fov;
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = this.CamDat.Fov;
			}
		}
		float deltaTime = Time.deltaTime;
		if (Input.GetKey(KeyCode.Home))
		{
			flag = true;
			this.CamDat.Dir.z = this.CamDat.Dir.z + deltaTime;
			this.CamDat.Dir.z = Mathf.Min(0f, this.CamDat.Dir.z);
		}
		else if (Input.GetKey(KeyCode.End))
		{
			flag = true;
			this.CamDat.Dir.z = this.CamDat.Dir.z - deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			flag = true;
			this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(deltaTime, 0f, 0f));
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			flag = true;
			this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(-deltaTime, 0f, 0f));
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			flag = true;
			this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, deltaTime));
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			flag = true;
			this.CamDat.Pos = this.CamDat.Pos + base.transform.TransformDirection(new Vector3(0f, 0f, -deltaTime));
		}
		if (Input.GetKey(KeyCode.PageUp))
		{
			flag = true;
			this.CamDat.Pos.y = this.CamDat.Pos.y + deltaTime;
		}
		else if (Input.GetKey(KeyCode.PageDown))
		{
			flag = true;
			this.CamDat.Pos.y = this.CamDat.Pos.y - deltaTime;
		}
		float num = 10f * Time.deltaTime;
		Vector3 zero = Vector3.zero;
		if (Input.GetKey(KeyCode.Period))
		{
			flag = true;
			zero.z += num;
		}
		else if (Input.GetKey(KeyCode.Backslash))
		{
			flag = true;
			zero.z -= num;
		}
		if (Input.GetKey(KeyCode.Keypad2))
		{
			flag = true;
			zero.x -= num;
		}
		else if (Input.GetKey(KeyCode.Keypad8))
		{
			flag = true;
			zero.x += num * this.xRotSpeed;
		}
		if (Input.GetKey(KeyCode.Keypad4))
		{
			flag = true;
			zero.y += num * this.yRotSpeed;
		}
		else if (Input.GetKey(KeyCode.Keypad6))
		{
			flag = true;
			zero.y -= num * this.yRotSpeed;
		}
		if (flag)
		{
			this.CamDat.Rot.y = (this.CamDat.Rot.y + zero.y) % 360f;
			this.CamDat.Rot.x = (this.CamDat.Rot.x + zero.x) % 360f;
			this.CamDat.Rot.z = (this.CamDat.Rot.z + zero.z) % 360f;
			base.transform.rotation = Quaternion.Euler(this.CamDat.Rot);
		}
		float deltaTime2 = Time.deltaTime;
		if (Input.GetKey(KeyCode.Equals))
		{
			flag = true;
			this.CamDat.Fov = Mathf.Max(this.CamDat.Fov - deltaTime2 * 15f, 1f);
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = this.CamDat.Fov;
			}
		}
		else if (Input.GetKey(KeyCode.RightBracket))
		{
			flag = true;
			this.CamDat.Fov = Mathf.Min(this.CamDat.Fov + deltaTime2 * 15f, 100f);
			if (base.GetComponent<Camera>() != null)
			{
				base.GetComponent<Camera>().fieldOfView = this.CamDat.Fov;
			}
		}
		return flag;
	}

	// Token: 0x06008EAB RID: 36523 RVA: 0x003B5C50 File Offset: 0x003B4050
	protected virtual void Start()
	{
		if (base.GetComponent<Camera>() != null)
		{
			base.GetComponent<Camera>().fieldOfView = this.CamReset.Fov;
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
		this.isInit = true;
	}

	// Token: 0x06008EAC RID: 36524 RVA: 0x003B5D4C File Offset: 0x003B414C
	protected void LateUpdate()
	{
		this.isControlNow = false;
		if (!this.isControlNow)
		{
			BaseCameraControl.NoCtrlFunc zoomCondition = this.ZoomCondition;
			bool flag = true;
			if (zoomCondition != null)
			{
				flag = zoomCondition();
			}
			this.isControlNow |= (flag && this.InputMouseWheelZoomProc());
		}
		if (!this.isControlNow)
		{
			BaseCameraControl.NoCtrlFunc noCtrlCondition = this.NoCtrlCondition;
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
			BaseCameraControl.NoCtrlFunc keyCondition = this.KeyCondition;
			bool flag3 = true;
			if (keyCondition != null)
			{
				flag3 = keyCondition();
			}
			this.isControlNow |= (flag3 && this.InputKeyProc());
		}
		base.transform.position = base.transform.rotation * this.CamDat.Dir + this.CamDat.Pos;
	}

	// Token: 0x06008EAD RID: 36525 RVA: 0x003B5E66 File Offset: 0x003B4266
	public void ForceCalculate()
	{
		base.transform.position = base.transform.rotation * this.CamDat.Dir + this.CamDat.Pos;
	}

	// Token: 0x06008EAE RID: 36526 RVA: 0x003B5EA0 File Offset: 0x003B42A0
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

	// Token: 0x06008EAF RID: 36527 RVA: 0x003B5F74 File Offset: 0x003B4374
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

	// Token: 0x06008EB0 RID: 36528 RVA: 0x003B60CC File Offset: 0x003B44CC
	public void SetCamera(BaseCameraControl src)
	{
		base.transform.position = src.transform.position;
		base.transform.rotation = src.transform.rotation;
		this.CamDat = src.CamDat;
		this.CamDat.Pos = -(base.transform.rotation * this.CamDat.Dir - base.transform.position);
		this.CamReset.Copy(this.CamDat, base.transform.rotation);
		if (base.GetComponent<Camera>() != null && src.GetComponent<Camera>() != null)
		{
			base.GetComponent<Camera>().CopyFrom(src.GetComponent<Camera>());
		}
	}

	// Token: 0x06008EB1 RID: 36529 RVA: 0x003B619C File Offset: 0x003B459C
	public void SetCamera(Vector3 pos, Vector3 angle, Quaternion rot, Vector3 dir)
	{
		base.transform.position = pos;
		base.transform.rotation = rot;
		this.CamDat.Rot = angle;
		this.CamDat.Dir = dir;
		this.CamDat.Pos = -(base.transform.rotation * this.CamDat.Dir - base.transform.position);
		this.CamReset.Copy(this.CamDat, base.transform.rotation);
	}

	// Token: 0x06008EB2 RID: 36530 RVA: 0x003B6234 File Offset: 0x003B4634
	public void SetCamera(Vector3 targPos, Vector3 camAngle, Vector3 camDir, float fov)
	{
		this.CameraAngle = camAngle;
		this.CameraDir = camDir;
		this.TargetPos = targPos;
		this.CameraFov = fov;
		base.transform.position = base.transform.rotation * camDir + targPos;
		this.CamReset.Copy(this.CamDat, base.transform.rotation);
	}

	// Token: 0x06008EB3 RID: 36531 RVA: 0x003B629C File Offset: 0x003B469C
	public void CopyCamera(BaseCameraControl dest)
	{
		dest.transform.position = base.transform.position;
		dest.transform.rotation = base.transform.rotation;
		dest.CamDat = this.CamDat;
		dest.CamDat.Pos = -(dest.transform.rotation * dest.CamDat.Dir - dest.transform.position);
	}

	// Token: 0x06008EB4 RID: 36532 RVA: 0x003B631C File Offset: 0x003B471C
	public void CopyInstance(BaseCameraControl src)
	{
		this.isInit = true;
		this.targetObj = src.targetObj;
		this.xRotSpeed = src.xRotSpeed;
		this.yRotSpeed = src.yRotSpeed;
		this.zoomSpeed = src.zoomSpeed;
		this.moveSpeed = src.moveSpeed;
		this.noneTargetDir = src.noneTargetDir;
		this.NoCtrlCondition = src.NoCtrlCondition;
		this.ZoomCondition = src.ZoomCondition;
		this.KeyCondition = src.KeyCondition;
		if (base.GetComponent<Camera>() != null && src.GetComponent<Camera>() != null)
		{
			base.GetComponent<Camera>().CopyFrom(src.GetComponent<Camera>());
		}
	}

	// Token: 0x06008EB5 RID: 36533 RVA: 0x003B63D0 File Offset: 0x003B47D0
	private void OnDrawGizmos()
	{
		Gizmos.color = ((this.CamDat.Dir.z <= 0f) ? Color.blue : Color.red);
		Vector3 direction = this.CamDat.Pos - base.transform.position;
		Gizmos.DrawRay(base.transform.position, direction);
	}

	// Token: 0x04007349 RID: 29513
	public Transform targetObj;

	// Token: 0x0400734A RID: 29514
	public float xRotSpeed = 5f;

	// Token: 0x0400734B RID: 29515
	public float yRotSpeed = 5f;

	// Token: 0x0400734C RID: 29516
	public float zoomSpeed = 10f;

	// Token: 0x0400734D RID: 29517
	public float moveSpeed = 5f;

	// Token: 0x0400734E RID: 29518
	public float noneTargetDir = 5f;

	// Token: 0x0400734F RID: 29519
	public BaseCameraControl.NoCtrlFunc NoCtrlCondition;

	// Token: 0x04007350 RID: 29520
	public BaseCameraControl.NoCtrlFunc ZoomCondition;

	// Token: 0x04007351 RID: 29521
	public BaseCameraControl.NoCtrlFunc KeyCondition;

	// Token: 0x04007352 RID: 29522
	public bool EnableResetKey = true;

	// Token: 0x04007354 RID: 29524
	public readonly int CONFIG_SIZE = Enum.GetNames(typeof(BaseCameraControl.Config)).Length;

	// Token: 0x04007355 RID: 29525
	protected BaseCameraControl.CameraData CamDat;

	// Token: 0x04007356 RID: 29526
	protected BaseCameraControl.Config cameraType = BaseCameraControl.Config.Rotation;

	// Token: 0x04007357 RID: 29527
	protected bool[] isDrags;

	// Token: 0x04007358 RID: 29528
	private BaseCameraControl.ResetData CamReset;

	// Token: 0x04007359 RID: 29529
	public bool isInit;

	// Token: 0x0400735A RID: 29530
	private const float INIT_FOV = 23f;

	// Token: 0x020010B2 RID: 4274
	// (Invoke) Token: 0x06008EB8 RID: 36536
	public delegate bool NoCtrlFunc();

	// Token: 0x020010B3 RID: 4275
	protected struct CameraData
	{
		// Token: 0x06008EBB RID: 36539 RVA: 0x003B643B File Offset: 0x003B483B
		public void Copy(BaseCameraControl.ResetData copy)
		{
			this.Pos = copy.Pos;
			this.Dir = copy.Dir;
			this.Rot = copy.Rot;
			this.Fov = copy.Fov;
		}

		// Token: 0x0400735C RID: 29532
		public Vector3 Pos;

		// Token: 0x0400735D RID: 29533
		public Vector3 Dir;

		// Token: 0x0400735E RID: 29534
		public Vector3 Rot;

		// Token: 0x0400735F RID: 29535
		public float Fov;
	}

	// Token: 0x020010B4 RID: 4276
	protected struct ResetData
	{
		// Token: 0x06008EBC RID: 36540 RVA: 0x003B6471 File Offset: 0x003B4871
		public void Copy(BaseCameraControl.CameraData copy, Quaternion rot)
		{
			this.Pos = copy.Pos;
			this.Dir = copy.Dir;
			this.Rot = copy.Rot;
			this.RotQ = rot;
			this.Fov = copy.Fov;
		}

		// Token: 0x04007360 RID: 29536
		public Vector3 Pos;

		// Token: 0x04007361 RID: 29537
		public Vector3 Dir;

		// Token: 0x04007362 RID: 29538
		public Vector3 Rot;

		// Token: 0x04007363 RID: 29539
		public Quaternion RotQ;

		// Token: 0x04007364 RID: 29540
		public float Fov;
	}

	// Token: 0x020010B5 RID: 4277
	public enum Config
	{
		// Token: 0x04007366 RID: 29542
		MoveXZ,
		// Token: 0x04007367 RID: 29543
		Rotation,
		// Token: 0x04007368 RID: 29544
		Translation,
		// Token: 0x04007369 RID: 29545
		MoveXY
	}
}
