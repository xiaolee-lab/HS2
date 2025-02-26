using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Cinemachine;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x020011CB RID: 4555
	public class CameraControl : CinemachineVirtualCameraBase
	{
		// Token: 0x0600955E RID: 38238 RVA: 0x003DB580 File Offset: 0x003D9980
		public CameraControl()
		{
			this.cameraData.parse = 23f;
			this.cameraReset.parse = 23f;
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x0600955F RID: 38239 RVA: 0x003DB67D File Offset: 0x003D9A7D
		private int mapLayer
		{
			get
			{
				if (this.m_MapLayer == -1)
				{
					this.m_MapLayer = LayerMask.GetMask(new string[]
					{
						"Map",
						"MapNoShadow"
					});
				}
				return this.m_MapLayer;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06009560 RID: 38240 RVA: 0x003DB6B2 File Offset: 0x003D9AB2
		// (set) Token: 0x06009561 RID: 38241 RVA: 0x003DB6BA File Offset: 0x003D9ABA
		public Camera mainCmaera { get; protected set; }

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x06009562 RID: 38242 RVA: 0x003DB6C3 File Offset: 0x003D9AC3
		public Camera subCamera
		{
			get
			{
				return this.m_SubCamera;
			}
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x06009563 RID: 38243 RVA: 0x003DB6CB File Offset: 0x003D9ACB
		// (set) Token: 0x06009564 RID: 38244 RVA: 0x003DB6D3 File Offset: 0x003D9AD3
		public bool isControlNow { get; protected set; }

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x06009565 RID: 38245 RVA: 0x003DB6DC File Offset: 0x003D9ADC
		// (set) Token: 0x06009566 RID: 38246 RVA: 0x003DB6E4 File Offset: 0x003D9AE4
		public bool isOutsideTargetTex { get; set; }

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06009567 RID: 38247 RVA: 0x003DB6ED File Offset: 0x003D9AED
		// (set) Token: 0x06009568 RID: 38248 RVA: 0x003DB6F5 File Offset: 0x003D9AF5
		public bool isCursorLock { get; set; }

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06009569 RID: 38249 RVA: 0x003DB6FE File Offset: 0x003D9AFE
		// (set) Token: 0x0600956A RID: 38250 RVA: 0x003DB706 File Offset: 0x003D9B06
		public bool isConfigTargetTex { get; set; }

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x0600956B RID: 38251 RVA: 0x003DB70F File Offset: 0x003D9B0F
		// (set) Token: 0x0600956C RID: 38252 RVA: 0x003DB717 File Offset: 0x003D9B17
		public bool isConfigVanish
		{
			get
			{
				return this.m_ConfigVanish;
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_ConfigVanish, value))
				{
					this.VisibleFroceVanish(true);
				}
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x0600956D RID: 38253 RVA: 0x003DB731 File Offset: 0x003D9B31
		public Transform targetTex
		{
			get
			{
				return this.m_TargetTex;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x0600956E RID: 38254 RVA: 0x003DB739 File Offset: 0x003D9B39
		// (set) Token: 0x0600956F RID: 38255 RVA: 0x003DB746 File Offset: 0x003D9B46
		public bool active
		{
			get
			{
				return this.objRoot.activeSelf;
			}
			set
			{
				this.objRoot.SetActive(value);
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06009570 RID: 38256 RVA: 0x003DB754 File Offset: 0x003D9B54
		// (set) Token: 0x06009571 RID: 38257 RVA: 0x003DB75C File Offset: 0x003D9B5C
		public bool IsOutsideSetting { get; set; }

		// Token: 0x06009572 RID: 38258 RVA: 0x003DB765 File Offset: 0x003D9B65
		public CameraControl.CameraData Export()
		{
			return new CameraControl.CameraData(this.cameraData);
		}

		// Token: 0x06009573 RID: 38259 RVA: 0x003DB772 File Offset: 0x003D9B72
		public CameraControl.CameraData ExportResetData()
		{
			return new CameraControl.CameraData(this.cameraReset);
		}

		// Token: 0x06009574 RID: 38260 RVA: 0x003DB77F File Offset: 0x003D9B7F
		public void Import(CameraControl.CameraData _src)
		{
			if (_src == null)
			{
				return;
			}
			this.cameraData.Copy(_src);
			this.fieldOfView = this.cameraData.parse;
		}

		// Token: 0x06009575 RID: 38261 RVA: 0x003DB7A5 File Offset: 0x003D9BA5
		public bool LoadVanish(string _assetbundle, string _file, GameObject _objMap)
		{
			this.lstMapVanish.Clear();
			return false;
		}

		// Token: 0x06009576 RID: 38262 RVA: 0x003DB7B3 File Offset: 0x003D9BB3
		public void CloerListCollider()
		{
			this.listCollider.Clear();
			this.lstMapVanish.Clear();
		}

		// Token: 0x06009577 RID: 38263 RVA: 0x003DB7CC File Offset: 0x003D9BCC
		public void VisibleFroceVanish(bool _visible)
		{
			foreach (CameraControl.VisibleObject visibleObject in this.lstMapVanish)
			{
				foreach (MeshRenderer meshRenderer in visibleObject.listRender)
				{
					if (meshRenderer)
					{
						meshRenderer.enabled = _visible;
					}
				}
				visibleObject.isVisible = _visible;
				visibleObject.delay = ((!_visible) ? 0f : 0.3f);
			}
		}

		// Token: 0x06009578 RID: 38264 RVA: 0x003DB89C File Offset: 0x003D9C9C
		private void VisibleFroceVanish(CameraControl.VisibleObject _obj, bool _visible)
		{
			if (_obj == null)
			{
				return;
			}
			if (_obj.listRender == null)
			{
				return;
			}
			foreach (MeshRenderer meshRenderer in _obj.listRender)
			{
				meshRenderer.enabled = _visible;
			}
			_obj.delay = ((!_visible) ? 0f : 0.3f);
			_obj.isVisible = _visible;
		}

		// Token: 0x06009579 RID: 38265 RVA: 0x003DB930 File Offset: 0x003D9D30
		private void VanishProc()
		{
			if (!this.isConfigVanish)
			{
				return;
			}
			int count = this.lstMapVanish.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				Collider x2 = this.listCollider.Find((Collider x) => this.lstMapVanish[i].nameCollider == x.name);
				if (x2 == null)
				{
					this.VanishDelayVisible(this.lstMapVanish[i]);
				}
				else if (this.lstMapVanish[i].isVisible)
				{
					this.VisibleFroceVanish(this.lstMapVanish[i], false);
				}
			}
		}

		// Token: 0x0600957A RID: 38266 RVA: 0x003DBA00 File Offset: 0x003D9E00
		private void VanishDelayVisible(CameraControl.VisibleObject _visible)
		{
			if (_visible.isVisible)
			{
				return;
			}
			if (!this.isFlashVisible)
			{
				_visible.delay += Time.deltaTime;
				if (_visible.delay >= 0.3f)
				{
					this.VisibleFroceVanish(_visible, true);
				}
			}
			else
			{
				this.VisibleFroceVanish(_visible, true);
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x0600957B RID: 38267 RVA: 0x003DBA5B File Offset: 0x003D9E5B
		// (set) Token: 0x0600957C RID: 38268 RVA: 0x003DBA68 File Offset: 0x003D9E68
		public Vector3 targetPos
		{
			get
			{
				return this.cameraData.pos;
			}
			set
			{
				this.cameraData.pos = value;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x0600957D RID: 38269 RVA: 0x003DBA76 File Offset: 0x003D9E76
		// (set) Token: 0x0600957E RID: 38270 RVA: 0x003DBA83 File Offset: 0x003D9E83
		public Vector3 cameraAngle
		{
			get
			{
				return this.cameraData.rotate;
			}
			set
			{
				base.transform.rotation = Quaternion.Euler(value);
				this.cameraData.rotate = value;
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x0600957F RID: 38271 RVA: 0x003DBAA2 File Offset: 0x003D9EA2
		// (set) Token: 0x06009580 RID: 38272 RVA: 0x003DBAB0 File Offset: 0x003D9EB0
		public float fieldOfView
		{
			get
			{
				return this.cameraData.parse;
			}
			set
			{
				this.cameraData.parse = value;
				if (this.mainCmaera != null)
				{
					this.mainCmaera.fieldOfView = value;
				}
				if (this.subCamera != null)
				{
					this.subCamera.fieldOfView = value;
				}
				this.lensSettings.FieldOfView = value;
				this.cameraState.Lens = this.lensSettings;
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06009581 RID: 38273 RVA: 0x003DBB20 File Offset: 0x003D9F20
		public override CameraState State
		{
			[CompilerGenerated]
			get
			{
				return this.cameraState;
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06009582 RID: 38274 RVA: 0x003DBB28 File Offset: 0x003D9F28
		// (set) Token: 0x06009583 RID: 38275 RVA: 0x003DBB30 File Offset: 0x003D9F30
		public override Transform LookAt
		{
			get
			{
				return this.targetObj;
			}
			set
			{
				this.targetObj = value;
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06009584 RID: 38276 RVA: 0x003DBB39 File Offset: 0x003D9F39
		// (set) Token: 0x06009585 RID: 38277 RVA: 0x003DBB41 File Offset: 0x003D9F41
		public override Transform Follow
		{
			get
			{
				return this.transBase;
			}
			set
			{
				this.transBase = value;
			}
		}

		// Token: 0x06009586 RID: 38278 RVA: 0x003DBB4C File Offset: 0x003D9F4C
		public void Reset(int _mode)
		{
			switch (_mode)
			{
			case 0:
				this.cameraData.Copy(this.cameraReset);
				this.fieldOfView = this.cameraData.parse;
				break;
			case 1:
				this.cameraData.pos = this.cameraReset.pos;
				break;
			case 2:
				base.transform.rotation = this.cameraReset.rotation;
				break;
			case 3:
				this.cameraData.distance = this.cameraReset.distance;
				break;
			}
		}

		// Token: 0x06009587 RID: 38279 RVA: 0x003DBBEC File Offset: 0x003D9FEC
		protected virtual bool InputMouseWheelZoomProc()
		{
			float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed;
			if (num != 0f)
			{
				CameraControl.CameraData cameraData = this.cameraData;
				cameraData.distance.z = cameraData.distance.z + num;
				this.cameraData.distance.z = Mathf.Min(0f, this.cameraData.distance.z);
				return true;
			}
			return false;
		}

		// Token: 0x06009588 RID: 38280 RVA: 0x003DBC5C File Offset: 0x003DA05C
		protected virtual bool InputMouseProc()
		{
			bool result = false;
			float axis = UnityEngine.Input.GetAxis("Mouse X");
			float axis2 = UnityEngine.Input.GetAxis("Mouse Y");
			if ((!EventSystem.current || !EventSystem.current.IsPointerOverGameObject()) && (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1) || UnityEngine.Input.GetMouseButtonDown(2)))
			{
				this.dragging = true;
			}
			else if (!UnityEngine.Input.GetMouseButton(0) && !UnityEngine.Input.GetMouseButton(1) && !UnityEngine.Input.GetMouseButton(2))
			{
				this.dragging = false;
			}
			if (!this.dragging)
			{
				return false;
			}
			if (UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.GetMouseButton(1))
			{
				Vector3 zero = Vector3.zero;
				zero.x = axis * this.moveSpeed * this.rateAddSpeed;
				zero.z = axis2 * this.moveSpeed * this.rateAddSpeed;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(zero));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(zero);
				}
				result = true;
			}
			else if (UnityEngine.Input.GetMouseButton(0))
			{
				Vector3 zero2 = Vector3.zero;
				zero2.y += axis * this.xRotSpeed * this.rateAddSpeed;
				zero2.x -= axis2 * this.yRotSpeed * this.rateAddSpeed;
				this.cameraData.rotate.y = (this.cameraData.rotate.y + zero2.y) % 360f;
				this.cameraData.rotate.x = (this.cameraData.rotate.x + zero2.x) % 360f;
				result = true;
			}
			else if (UnityEngine.Input.GetMouseButton(1))
			{
				CameraControl.CameraData cameraData = this.cameraData;
				cameraData.pos.y = cameraData.pos.y + axis2 * this.moveSpeed * this.rateAddSpeed;
				CameraControl.CameraData cameraData2 = this.cameraData;
				cameraData2.distance.z = cameraData2.distance.z - axis * this.moveSpeed * this.rateAddSpeed;
				this.cameraData.distance.z = Mathf.Min(0f, this.cameraData.distance.z);
				result = true;
			}
			else if (UnityEngine.Input.GetMouseButton(2))
			{
				Vector3 zero3 = Vector3.zero;
				zero3.x = axis * this.moveSpeed * this.rateAddSpeed;
				zero3.y = axis2 * this.moveSpeed * this.rateAddSpeed;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(zero3));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(zero3);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06009589 RID: 38281 RVA: 0x003DBF9C File Offset: 0x003DA39C
		protected virtual bool InputKeyProc()
		{
			bool flag = false;
			if (UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				this.Reset(0);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad5))
			{
				this.cameraData.rotate.x = this.cameraReset.rotate.x;
				this.cameraData.rotate.y = this.cameraReset.rotate.y;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Slash))
			{
				this.cameraData.rotate.z = 0f;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Semicolon))
			{
				this.fieldOfView = this.cameraReset.parse;
			}
			float deltaTime = Time.deltaTime;
			if (UnityEngine.Input.GetKey(KeyCode.Home))
			{
				flag = true;
				CameraControl.CameraData cameraData = this.cameraData;
				cameraData.distance.z = cameraData.distance.z + deltaTime;
				this.cameraData.distance.z = Mathf.Min(0f, this.cameraData.distance.z);
			}
			else if (UnityEngine.Input.GetKey(KeyCode.End))
			{
				flag = true;
				CameraControl.CameraData cameraData2 = this.cameraData;
				cameraData2.distance.z = cameraData2.distance.z - deltaTime;
			}
			if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
			{
				flag = true;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(deltaTime, 0f, 0f)));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(new Vector3(deltaTime, 0f, 0f));
				}
			}
			else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
			{
				flag = true;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(-deltaTime, 0f, 0f)));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(new Vector3(-deltaTime, 0f, 0f));
				}
			}
			if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
			{
				flag = true;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, deltaTime)));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(new Vector3(0f, 0f, deltaTime));
				}
			}
			else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
			{
				flag = true;
				if (this.transBase != null)
				{
					this.cameraData.pos += this.transBase.InverseTransformDirection(base.transform.TransformDirection(new Vector3(0f, 0f, -deltaTime)));
				}
				else
				{
					this.cameraData.pos += base.transform.TransformDirection(new Vector3(0f, 0f, -deltaTime));
				}
			}
			if (UnityEngine.Input.GetKey(KeyCode.PageUp))
			{
				flag = true;
				CameraControl.CameraData cameraData3 = this.cameraData;
				cameraData3.pos.y = cameraData3.pos.y + deltaTime;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.PageDown))
			{
				flag = true;
				CameraControl.CameraData cameraData4 = this.cameraData;
				cameraData4.pos.y = cameraData4.pos.y - deltaTime;
			}
			float num = 10f * Time.deltaTime;
			Vector3 zero = Vector3.zero;
			if (UnityEngine.Input.GetKey(KeyCode.Period))
			{
				flag = true;
				zero.z += num;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.Backslash))
			{
				flag = true;
				zero.z -= num;
			}
			if (UnityEngine.Input.GetKey(KeyCode.Keypad2))
			{
				flag = true;
				zero.x -= num * this.yRotSpeed;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.Keypad8))
			{
				flag = true;
				zero.x += num * this.yRotSpeed;
			}
			if (UnityEngine.Input.GetKey(KeyCode.Keypad4))
			{
				flag = true;
				zero.y += num * this.xRotSpeed;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.Keypad6))
			{
				flag = true;
				zero.y -= num * this.xRotSpeed;
			}
			if (flag)
			{
				this.cameraData.rotate.y = (this.cameraData.rotate.y + zero.y) % 360f;
				this.cameraData.rotate.x = (this.cameraData.rotate.x + zero.x) % 360f;
				this.cameraData.rotate.z = (this.cameraData.rotate.z + zero.z) % 360f;
			}
			float deltaTime2 = Time.deltaTime;
			if (UnityEngine.Input.GetKey(KeyCode.Equals))
			{
				flag = true;
				this.fieldOfView = Mathf.Max(this.cameraData.parse - deltaTime2 * 15f, 1f);
			}
			else if (UnityEngine.Input.GetKey(KeyCode.RightBracket))
			{
				flag = true;
				this.fieldOfView = Mathf.Min(this.cameraData.parse + deltaTime2 * 15f, this.limitFov);
			}
			return flag;
		}

		// Token: 0x0600958A RID: 38282 RVA: 0x003DC590 File Offset: 0x003DA990
		public void TargetSet(Transform target, bool isReset)
		{
			if (target)
			{
				this.targetObj = target;
			}
			if (this.targetObj)
			{
				this.cameraData.pos = this.targetObj.position;
			}
			Transform transform = base.transform;
			this.cameraData.distance = Vector3.zero;
			this.cameraData.distance.z = -Vector3.Distance(this.cameraData.pos, transform.position);
			transform.LookAt(this.cameraData.pos);
			this.cameraData.rotate = base.transform.rotation.eulerAngles;
			if (isReset)
			{
				this.cameraReset.Copy(this.cameraData);
			}
		}

		// Token: 0x0600958B RID: 38283 RVA: 0x003DC65C File Offset: 0x003DAA5C
		public void FrontTarget(Transform target, bool isReset, float dir = -3.4028235E+38f)
		{
			if (target)
			{
				this.targetObj = target;
			}
			if (this.targetObj)
			{
				target = this.targetObj;
				this.cameraData.pos = target.position;
			}
			if (!target)
			{
				return;
			}
			if (dir != -3.4028235E+38f)
			{
				this.cameraData.distance = Vector3.zero;
				this.cameraData.distance.z = -dir;
			}
			Transform transform = base.transform;
			transform.position = target.position;
			transform.rotation.eulerAngles.Set(this.cameraData.rotate.x, this.cameraData.rotate.y, this.cameraData.rotate.z);
			transform.position += transform.forward * this.cameraData.distance.z;
			transform.LookAt(this.cameraData.pos);
			this.cameraData.rotate = base.transform.rotation.eulerAngles;
			if (isReset)
			{
				this.cameraReset.Copy(this.cameraData);
			}
		}

		// Token: 0x0600958C RID: 38284 RVA: 0x003DC7A8 File Offset: 0x003DABA8
		public void SetCamera(Vector3 pos, Vector3 angle, Quaternion rot, Vector3 dir)
		{
			base.transform.localPosition = pos;
			base.transform.localRotation = rot;
			this.cameraData.rotate = angle;
			this.cameraData.distance = dir;
			this.cameraData.pos = -(base.transform.localRotation * this.cameraData.distance - base.transform.localPosition);
			this.cameraReset.Copy(this.cameraData);
		}

		// Token: 0x0600958D RID: 38285 RVA: 0x003DC834 File Offset: 0x003DAC34
		public void SetCamera(Vector3 _pos, Quaternion _rot, float _dis, bool _update = true, bool _reset = true)
		{
			this.cameraData.pos = _pos;
			this.cameraData.rotation = _rot;
			this.cameraData.distance = new Vector3(0f, 0f, -_dis);
			if (_reset)
			{
				this.cameraReset.Copy(this.cameraData);
			}
			if (_update)
			{
				this.InternalUpdateCameraState(Vector3.zero, 0f);
			}
		}

		// Token: 0x0600958E RID: 38286 RVA: 0x003DC8A4 File Offset: 0x003DACA4
		public void SetBase(Transform _trans)
		{
			if (this.transBase == null)
			{
				return;
			}
			this.transBase.transform.position = _trans.position;
			this.transBase.transform.rotation = _trans.rotation;
		}

		// Token: 0x0600958F RID: 38287 RVA: 0x003DC8E4 File Offset: 0x003DACE4
		public void ReflectOption()
		{
			this.rateAddSpeed = Studio.optionSystem.cameraSpeed;
			this.xRotSpeed = Studio.optionSystem.cameraSpeedX;
			this.yRotSpeed = Studio.optionSystem.cameraSpeedY;
			List<string> list = new List<string>();
			if (Singleton<Studio>.Instance.workInfo.visibleAxis)
			{
				if (Studio.optionSystem.selectedState == 0)
				{
					list.Add("Studio/Col");
				}
				list.Add("Studio/Select");
			}
			list.Add("Studio/Route");
			this.m_SubCamera.cullingMask = LayerMask.GetMask(list.ToArray());
		}

		// Token: 0x06009590 RID: 38288 RVA: 0x003DC984 File Offset: 0x003DAD84
		private void Awake()
		{
			this.m_MapLayer = -1;
			this.mainCmaera = base.GetComponent<Camera>();
			this.fieldOfView = this.cameraReset.parse;
			this.zoomCondition = (() => false);
			this.isControlNow = false;
			if (!this.targetObj)
			{
				Vector3 a = base.transform.TransformDirection(Vector3.forward);
				this.cameraData.pos = base.transform.position + a * this.noneTargetDir;
			}
			this.TargetSet(this.targetObj, true);
			this.isOutsideTargetTex = true;
			this.isConfigTargetTex = true;
			this.isCursorLock = true;
		}

		// Token: 0x06009591 RID: 38289 RVA: 0x003DCA4C File Offset: 0x003DAE4C
		private new IEnumerator Start()
		{
			if (this.m_TargetTex == null)
			{
				this.m_TargetTex = base.transform.Find("CameraTarget");
			}
			if (this.m_TargetTex)
			{
				this.m_TargetTex.localScale = Vector3.one * 0.1f;
				if (this.m_TargetRender == null)
				{
					this.m_TargetRender = this.m_TargetTex.GetComponent<Renderer>();
				}
			}
			if (this.m_SubCamera != null)
			{
				this.m_SubCamera.enabled = true;
			}
			this.ReflectOption();
			yield return new WaitWhile(() => !Manager.Config.initialized);
			this.lensSettings = this.cameraState.Lens;
			this.m_ConfigVanish = Manager.Config.GraphicData.Shield;
			this.listCollider.Clear();
			this.isInit = true;
			yield break;
		}

		// Token: 0x06009592 RID: 38290 RVA: 0x003DCA68 File Offset: 0x003DAE68
		private void LateUpdate()
		{
			if (Singleton<Scene>.Instance.AddSceneName != string.Empty || Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			if (this.IsOutsideSetting)
			{
				return;
			}
			if (!this.isControlNow && UnityEngine.Input.GetKey(KeyCode.B))
			{
				return;
			}
			this.isControlNow = false;
			this.xRotSpeed = Studio.optionSystem.cameraSpeedX;
			this.yRotSpeed = Studio.optionSystem.cameraSpeedY;
			if (!this.isControlNow)
			{
				this.isControlNow |= ((this.zoomCondition == null || this.zoomCondition()) && this.InputMouseWheelZoomProc());
			}
			if (!this.isControlNow && (this.noCtrlCondition == null || !this.noCtrlCondition()) && this.InputMouseProc())
			{
				this.isControlNow = true;
			}
			if (!this.isControlNow)
			{
				this.isControlNow |= ((this.keyCondition == null || this.keyCondition()) && this.InputKeyProc());
			}
		}

		// Token: 0x06009593 RID: 38291 RVA: 0x003DCBAC File Offset: 0x003DAFAC
		protected void OnTriggerEnter(Collider other)
		{
			if (other == null)
			{
				return;
			}
			if ((this.mapLayer & 1 << other.gameObject.layer) == 0)
			{
				return;
			}
			Collider x2 = this.listCollider.Find((Collider x) => other.name == x.name);
			if (x2 == null)
			{
				this.listCollider.Add(other);
			}
		}

		// Token: 0x06009594 RID: 38292 RVA: 0x003DCC30 File Offset: 0x003DB030
		protected void OnTriggerStay(Collider other)
		{
			if (other == null)
			{
				return;
			}
			if ((this.mapLayer & 1 << other.gameObject.layer) == 0)
			{
				return;
			}
			Collider x2 = this.listCollider.Find((Collider x) => other.name == x.name);
			if (x2 == null)
			{
				this.listCollider.Add(other);
			}
		}

		// Token: 0x06009595 RID: 38293 RVA: 0x003DCCB3 File Offset: 0x003DB0B3
		protected void OnTriggerExit(Collider other)
		{
			this.listCollider.Clear();
		}

		// Token: 0x06009596 RID: 38294 RVA: 0x003DCCC0 File Offset: 0x003DB0C0
		private void OnDrawGizmos()
		{
			Gizmos.color = ((this.cameraData.distance.z <= 0f) ? Color.blue : Color.red);
			Vector3 direction;
			if (this.transBase != null)
			{
				direction = this.transBase.TransformPoint(this.cameraData.pos) - base.transform.position;
			}
			else
			{
				direction = this.cameraData.pos - base.transform.position;
			}
			Gizmos.DrawRay(base.transform.position, direction);
		}

		// Token: 0x06009597 RID: 38295 RVA: 0x003DCD68 File Offset: 0x003DB168
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.IsOutsideSetting)
			{
				return;
			}
			if (this.isLimitDir)
			{
				this.cameraData.distance.z = Mathf.Clamp(this.cameraData.distance.z, -this.limitDir, 0f);
			}
			if (this.isLimitPos)
			{
				this.cameraData.pos = Vector3.ClampMagnitude(this.cameraData.pos, this.limitPos);
			}
			if (this.transBase != null)
			{
				this.cameraState.RawOrientation = this.transBase.rotation * Quaternion.Euler(this.cameraData.rotate);
				this.cameraState.RawPosition = this.cameraState.RawOrientation * this.cameraData.distance + this.transBase.TransformPoint(this.cameraData.pos);
			}
			else
			{
				this.cameraState.RawOrientation = Quaternion.Euler(this.cameraData.rotate);
				this.cameraState.RawPosition = this.cameraState.RawOrientation * this.cameraData.distance + this.cameraData.pos;
			}
			base.transform.position = this.cameraState.RawPosition;
			base.transform.rotation = this.cameraState.RawOrientation;
			if (this.targetTex)
			{
				if (this.transBase != null)
				{
					this.targetTex.position = this.transBase.TransformPoint(this.cameraData.pos);
				}
				else
				{
					this.targetTex.position = this.cameraData.pos;
				}
				Vector3 position = base.transform.position;
				position.y = this.targetTex.position.y;
				this.targetTex.transform.LookAt(position);
				this.targetTex.Rotate(90f, 0f, 0f);
				if (this.m_TargetRender)
				{
					this.m_TargetRender.enabled = (this.isControlNow & this.isOutsideTargetTex & this.isConfigTargetTex);
				}
				if (Singleton<GameCursor>.IsInstance() && this.isCursorLock)
				{
					Singleton<GameCursor>.Instance.SetCursorLock(this.isControlNow & this.isOutsideTargetTex);
				}
			}
			if (this.viewCollider != null)
			{
				this.viewCollider.height = this.cameraData.distance.z;
				this.viewCollider.center = -Vector3.forward * this.cameraData.distance.z * 0.5f;
				this.VanishProc();
			}
		}

		// Token: 0x06009598 RID: 38296 RVA: 0x003DD063 File Offset: 0x003DB463
		public void SetPositionAndRotation(Vector3 _position, Quaternion _orientation)
		{
			this.cameraState.RawPosition = _position;
			this.cameraState.RawOrientation = _orientation;
		}

		// Token: 0x0400784D RID: 30797
		private int m_MapLayer = -1;

		// Token: 0x0400784F RID: 30799
		public Transform transBase;

		// Token: 0x04007850 RID: 30800
		public Transform targetObj;

		// Token: 0x04007851 RID: 30801
		public float xRotSpeed = 5f;

		// Token: 0x04007852 RID: 30802
		public float yRotSpeed = 5f;

		// Token: 0x04007853 RID: 30803
		public float zoomSpeed = 5f;

		// Token: 0x04007854 RID: 30804
		public float moveSpeed = 0.05f;

		// Token: 0x04007855 RID: 30805
		public float noneTargetDir = 5f;

		// Token: 0x04007856 RID: 30806
		public bool isLimitPos;

		// Token: 0x04007857 RID: 30807
		public float limitPos = 2f;

		// Token: 0x04007858 RID: 30808
		public bool isLimitDir;

		// Token: 0x04007859 RID: 30809
		public float limitDir = 10f;

		// Token: 0x0400785A RID: 30810
		public float limitFov = 40f;

		// Token: 0x0400785B RID: 30811
		[SerializeField]
		private Camera m_SubCamera;

		// Token: 0x0400785C RID: 30812
		public CameraControl.NoCtrlFunc noCtrlCondition;

		// Token: 0x0400785D RID: 30813
		public CameraControl.NoCtrlFunc zoomCondition;

		// Token: 0x0400785E RID: 30814
		public CameraControl.NoCtrlFunc keyCondition;

		// Token: 0x04007860 RID: 30816
		public readonly int CONFIG_SIZE = Enum.GetNames(typeof(CameraControl.Config)).Length;

		// Token: 0x04007861 RID: 30817
		[SerializeField]
		protected CameraControl.CameraData cameraData = new CameraControl.CameraData();

		// Token: 0x04007862 RID: 30818
		protected CameraControl.CameraData cameraReset = new CameraControl.CameraData();

		// Token: 0x04007863 RID: 30819
		protected bool isInit;

		// Token: 0x04007864 RID: 30820
		private const float INIT_FOV = 23f;

		// Token: 0x04007865 RID: 30821
		protected CapsuleCollider viewCollider;

		// Token: 0x04007866 RID: 30822
		protected float rateAddSpeed = 1f;

		// Token: 0x04007867 RID: 30823
		private bool dragging;

		// Token: 0x0400786B RID: 30827
		private bool m_ConfigVanish = true;

		// Token: 0x0400786C RID: 30828
		[SerializeField]
		private Transform m_TargetTex;

		// Token: 0x0400786D RID: 30829
		[SerializeField]
		private Renderer m_TargetRender;

		// Token: 0x0400786E RID: 30830
		[SerializeField]
		private GameObject objRoot;

		// Token: 0x04007870 RID: 30832
		private List<CameraControl.VisibleObject> lstMapVanish = new List<CameraControl.VisibleObject>();

		// Token: 0x04007871 RID: 30833
		private List<Collider> listCollider = new List<Collider>();

		// Token: 0x04007872 RID: 30834
		public bool isFlashVisible;

		// Token: 0x04007873 RID: 30835
		[SerializeField]
		private LensSettings lensSettings = LensSettings.Default;

		// Token: 0x04007874 RID: 30836
		private CameraState cameraState = CameraState.Default;

		// Token: 0x020011CC RID: 4556
		// (Invoke) Token: 0x0600959B RID: 38299
		public delegate bool NoCtrlFunc();

		// Token: 0x020011CD RID: 4557
		[Serializable]
		public class CameraData
		{
			// Token: 0x0600959E RID: 38302 RVA: 0x003DD080 File Offset: 0x003DB480
			public CameraData()
			{
			}

			// Token: 0x0600959F RID: 38303 RVA: 0x003DD0B4 File Offset: 0x003DB4B4
			public CameraData(CameraControl.CameraData _src)
			{
				this.Copy(_src);
			}

			// Token: 0x17001FB2 RID: 8114
			// (get) Token: 0x060095A0 RID: 38304 RVA: 0x003DD0EF File Offset: 0x003DB4EF
			// (set) Token: 0x060095A1 RID: 38305 RVA: 0x003DD0FC File Offset: 0x003DB4FC
			public Quaternion rotation
			{
				get
				{
					return Quaternion.Euler(this.rotate);
				}
				set
				{
					this.rotate = value.eulerAngles;
				}
			}

			// Token: 0x060095A2 RID: 38306 RVA: 0x003DD10B File Offset: 0x003DB50B
			public void Set(Vector3 _pos, Vector3 _rotate, Vector3 _distance, float _parse)
			{
				this.pos = _pos;
				this.rotate = _rotate;
				this.distance = _distance;
				this.parse = _parse;
			}

			// Token: 0x060095A3 RID: 38307 RVA: 0x003DD12C File Offset: 0x003DB52C
			public void Save(BinaryWriter _writer)
			{
				_writer.Write(2);
				_writer.Write(this.pos.x);
				_writer.Write(this.pos.y);
				_writer.Write(this.pos.z);
				_writer.Write(this.rotate.x);
				_writer.Write(this.rotate.y);
				_writer.Write(this.rotate.z);
				_writer.Write(this.distance.x);
				_writer.Write(this.distance.y);
				_writer.Write(this.distance.z);
				_writer.Write(this.parse);
			}

			// Token: 0x060095A4 RID: 38308 RVA: 0x003DD1E8 File Offset: 0x003DB5E8
			public void Load(BinaryReader _reader)
			{
				int num = _reader.ReadInt32();
				this.pos.x = _reader.ReadSingle();
				this.pos.y = _reader.ReadSingle();
				this.pos.z = _reader.ReadSingle();
				this.rotate.x = _reader.ReadSingle();
				this.rotate.y = _reader.ReadSingle();
				this.rotate.z = _reader.ReadSingle();
				if (num == 1)
				{
					_reader.ReadSingle();
				}
				else
				{
					this.distance.x = _reader.ReadSingle();
					this.distance.y = _reader.ReadSingle();
					this.distance.z = _reader.ReadSingle();
				}
				this.parse = _reader.ReadSingle();
			}

			// Token: 0x060095A5 RID: 38309 RVA: 0x003DD2B4 File Offset: 0x003DB6B4
			public void Copy(CameraControl.CameraData _src)
			{
				this.pos = _src.pos;
				this.rotate = _src.rotate;
				this.distance = _src.distance;
				this.parse = _src.parse;
			}

			// Token: 0x04007876 RID: 30838
			private const int ver = 2;

			// Token: 0x04007877 RID: 30839
			public Vector3 pos = Vector3.zero;

			// Token: 0x04007878 RID: 30840
			public Vector3 rotate = Vector3.zero;

			// Token: 0x04007879 RID: 30841
			public Vector3 distance = Vector3.zero;

			// Token: 0x0400787A RID: 30842
			public float parse = 23f;
		}

		// Token: 0x020011CE RID: 4558
		public enum Config
		{
			// Token: 0x0400787C RID: 30844
			MoveXZ,
			// Token: 0x0400787D RID: 30845
			Rotation,
			// Token: 0x0400787E RID: 30846
			Translation,
			// Token: 0x0400787F RID: 30847
			MoveXY
		}

		// Token: 0x020011CF RID: 4559
		public class VisibleObject
		{
			// Token: 0x04007880 RID: 30848
			public string nameCollider;

			// Token: 0x04007881 RID: 30849
			public float delay;

			// Token: 0x04007882 RID: 30850
			public bool isVisible = true;

			// Token: 0x04007883 RID: 30851
			public List<MeshRenderer> listRender = new List<MeshRenderer>();
		}
	}
}
