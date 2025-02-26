using System;
using System.Linq;
using Manager;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200124E RID: 4686
	public class GuideObject : MonoBehaviour
	{
		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06009A72 RID: 39538 RVA: 0x003F6C7D File Offset: 0x003F507D
		// (set) Token: 0x06009A73 RID: 39539 RVA: 0x003F6C85 File Offset: 0x003F5085
		public int dicKey
		{
			get
			{
				return this.m_DicKey;
			}
			set
			{
				if (Utility.SetStruct<int>(ref this.m_DicKey, value))
				{
					this.changeAmount = Studio.GetChangeAmount(this.m_DicKey);
				}
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06009A74 RID: 39540 RVA: 0x003F6CA9 File Offset: 0x003F50A9
		// (set) Token: 0x06009A75 RID: 39541 RVA: 0x003F6CB4 File Offset: 0x003F50B4
		public ChangeAmount changeAmount
		{
			get
			{
				return this.m_ChangeAmount;
			}
			private set
			{
				this.m_ChangeAmount = value;
				if (this.m_ChangeAmount != null)
				{
					ChangeAmount changeAmount = this.m_ChangeAmount;
					changeAmount.onChangePos = (Action)Delegate.Combine(changeAmount.onChangePos, new Action(this.CalcPosition));
					ChangeAmount changeAmount2 = this.m_ChangeAmount;
					changeAmount2.onChangeRot = (Action)Delegate.Combine(changeAmount2.onChangeRot, new Action(this.CalcRotation));
					ChangeAmount changeAmount3 = this.m_ChangeAmount;
					changeAmount3.onChangeScale = (Action<Vector3>)Delegate.Combine(changeAmount3.onChangeScale, new Action<Vector3>(this.CalcScale));
				}
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06009A76 RID: 39542 RVA: 0x003F6D48 File Offset: 0x003F5148
		public GuideMove[] guideMove
		{
			get
			{
				return (from g in this.guide.Skip(1).Take(3)
				select g as GuideMove).ToArray<GuideMove>();
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06009A77 RID: 39543 RVA: 0x003F6D83 File Offset: 0x003F5183
		public GuideSelect guideSelect
		{
			get
			{
				return this.guide[11] as GuideSelect;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06009A78 RID: 39544 RVA: 0x003F6D93 File Offset: 0x003F5193
		public GameObject objCenter
		{
			get
			{
				return this.m_objCenter;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06009A79 RID: 39545 RVA: 0x003F6D9B File Offset: 0x003F519B
		public bool[] enables
		{
			get
			{
				return this.m_Enables;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06009A7A RID: 39546 RVA: 0x003F6DA3 File Offset: 0x003F51A3
		// (set) Token: 0x06009A7B RID: 39547 RVA: 0x003F6DAD File Offset: 0x003F51AD
		public bool enablePos
		{
			get
			{
				return this.m_Enables[0];
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Enables[0], value))
				{
					this.roots[0].SetActive(this.isActive && this.m_Enables[0]);
				}
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06009A7C RID: 39548 RVA: 0x003F6DE9 File Offset: 0x003F51E9
		// (set) Token: 0x06009A7D RID: 39549 RVA: 0x003F6DF3 File Offset: 0x003F51F3
		public bool enableRot
		{
			get
			{
				return this.m_Enables[1];
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Enables[1], value))
				{
					this.roots[1].SetActive(this.isActive && this.m_Enables[1]);
				}
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06009A7E RID: 39550 RVA: 0x003F6E2F File Offset: 0x003F522F
		// (set) Token: 0x06009A7F RID: 39551 RVA: 0x003F6E39 File Offset: 0x003F5239
		public bool enableScale
		{
			get
			{
				return this.m_Enables[2];
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Enables[2], value))
				{
					this.roots[2].SetActive(this.isActive && this.m_Enables[2]);
				}
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06009A80 RID: 39552 RVA: 0x003F6E75 File Offset: 0x003F5275
		// (set) Token: 0x06009A81 RID: 39553 RVA: 0x003F6E7D File Offset: 0x003F527D
		public bool calcScale
		{
			get
			{
				return this._calcScale;
			}
			set
			{
				this._calcScale = value;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06009A82 RID: 39554 RVA: 0x003F6E86 File Offset: 0x003F5286
		// (set) Token: 0x06009A83 RID: 39555 RVA: 0x003F6E8E File Offset: 0x003F528E
		public bool enableMaluti { get; set; }

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06009A84 RID: 39556 RVA: 0x003F6E97 File Offset: 0x003F5297
		// (set) Token: 0x06009A85 RID: 39557 RVA: 0x003F6E9F File Offset: 0x003F529F
		public bool isActive
		{
			get
			{
				return this.m_IsActive;
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_IsActive, value))
				{
					this.SetMode(GuideObjectManager.GetMode(), true);
				}
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06009A86 RID: 39558 RVA: 0x003F6EBE File Offset: 0x003F52BE
		// (set) Token: 0x06009A87 RID: 39559 RVA: 0x003F6EC6 File Offset: 0x003F52C6
		public float scaleRate
		{
			get
			{
				return this.m_ScaleRate;
			}
			set
			{
				if (Utility.SetStruct<float>(ref this.m_ScaleRate, value))
				{
					this.SetScale();
				}
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06009A88 RID: 39560 RVA: 0x003F6EDF File Offset: 0x003F52DF
		// (set) Token: 0x06009A89 RID: 39561 RVA: 0x003F6EE7 File Offset: 0x003F52E7
		public float scaleRot
		{
			get
			{
				return this.m_ScaleRot;
			}
			set
			{
				if (Utility.SetStruct<float>(ref this.m_ScaleRot, value))
				{
					this.SetScale();
				}
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06009A8A RID: 39562 RVA: 0x003F6F00 File Offset: 0x003F5300
		// (set) Token: 0x06009A8B RID: 39563 RVA: 0x003F6F08 File Offset: 0x003F5308
		public float scaleSelect
		{
			get
			{
				return this.m_ScaleSelect;
			}
			set
			{
				if (Utility.SetStruct<float>(ref this.m_ScaleSelect, value))
				{
					this.SetScale();
				}
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06009A8C RID: 39564 RVA: 0x003F6F21 File Offset: 0x003F5321
		public bool isChild
		{
			get
			{
				return this.parentGuide != null;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06009A8D RID: 39565 RVA: 0x003F6F2F File Offset: 0x003F532F
		public int layer
		{
			get
			{
				return (!this.isChild) ? base.gameObject.layer : this.parentGuide.gameObject.layer;
			}
		}

		// Token: 0x06009A8E RID: 39566 RVA: 0x003F6F5C File Offset: 0x003F535C
		public void SetMode(int _mode, bool _layer = true)
		{
			for (int i = 0; i < 3; i++)
			{
				if (!(this.roots[i] == null))
				{
					this.roots[i].SetActive(this.isActive && this.m_Enables[i] && _mode == i);
				}
			}
			bool flag;
			if (!this.isActive)
			{
				flag = this.m_Enables.Any((bool b) => b);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag | (this.isActive && !this.m_Enables[_mode]);
			this.objectSelect.SetActive(flag2);
			if (_layer)
			{
				this.SetLayer(base.gameObject, (!this.isChild) ? LayerMask.NameToLayer((!flag2) ? "Studio/Select" : "Studio/Col") : this.layer);
				if (this.isActiveFunc != null)
				{
					this.isActiveFunc(flag2);
				}
			}
		}

		// Token: 0x06009A8F RID: 39567 RVA: 0x003F7075 File Offset: 0x003F5475
		public void SetActive(bool _active, bool _layer = true)
		{
			this.m_IsActive = _active;
			this.SetMode(GuideObjectManager.GetMode(), _layer);
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06009A90 RID: 39568 RVA: 0x003F708A File Offset: 0x003F548A
		// (set) Token: 0x06009A91 RID: 39569 RVA: 0x003F7097 File Offset: 0x003F5497
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06009A92 RID: 39570 RVA: 0x003F70A5 File Offset: 0x003F54A5
		// (set) Token: 0x06009A93 RID: 39571 RVA: 0x003F70B2 File Offset: 0x003F54B2
		public bool visibleOutside
		{
			get
			{
				return this._visibleOutside.Value;
			}
			set
			{
				this._visibleOutside.Value = value;
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06009A94 RID: 39572 RVA: 0x003F70C0 File Offset: 0x003F54C0
		// (set) Token: 0x06009A95 RID: 39573 RVA: 0x003F70CD File Offset: 0x003F54CD
		public bool visibleCenter
		{
			get
			{
				return this.rendererCenter.enabled;
			}
			set
			{
				this.rendererCenter.enabled = value;
			}
		}

		// Token: 0x17002104 RID: 8452
		// (set) Token: 0x06009A96 RID: 39574 RVA: 0x003F70DC File Offset: 0x003F54DC
		public bool visibleTranslation
		{
			set
			{
				foreach (GuideBase guideBase in this.guide.Skip(13))
				{
					guideBase.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06009A97 RID: 39575 RVA: 0x003F7144 File Offset: 0x003F5544
		// (set) Token: 0x06009A98 RID: 39576 RVA: 0x003F714C File Offset: 0x003F554C
		public GuideMove.MoveCalc moveCalc
		{
			get
			{
				return this._moveCalc;
			}
			set
			{
				this._moveCalc = value;
				foreach (GuideMove guideMove2 in this.guideMove)
				{
					guideMove2.moveCalc = value;
				}
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06009A99 RID: 39577 RVA: 0x003F7186 File Offset: 0x003F5586
		// (set) Token: 0x06009A9A RID: 39578 RVA: 0x003F718E File Offset: 0x003F558E
		private bool isQuit { get; set; }

		// Token: 0x06009A9B RID: 39579 RVA: 0x003F7198 File Offset: 0x003F5598
		private void CalcPosition()
		{
			if (!this.m_Enables[0])
			{
				return;
			}
			if (this.transformTarget)
			{
				if (this.parent && this.nonconnect)
				{
					this.transformTarget.position = this.parent.TransformPoint(this.changeAmount.pos);
				}
				else
				{
					this.transformTarget.localPosition = this.changeAmount.pos;
				}
			}
		}

		// Token: 0x06009A9C RID: 39580 RVA: 0x003F721C File Offset: 0x003F561C
		private void CalcRotation()
		{
			if (!this.m_Enables[1])
			{
				return;
			}
			if (this.transformTarget)
			{
				if (this.parent && this.nonconnect)
				{
					this.transformTarget.rotation = this.parent.rotation * Quaternion.Euler(this.changeAmount.rot);
				}
				else
				{
					this.transformTarget.localRotation = Quaternion.Euler(this.changeAmount.rot);
				}
			}
		}

		// Token: 0x06009A9D RID: 39581 RVA: 0x003F72B0 File Offset: 0x003F56B0
		private void CalcScale(Vector3 _value)
		{
			if (this.transformTarget && this.parent && this.nonconnect)
			{
				this.transformTarget.localScale = this.changeAmount.scale;
			}
		}

		// Token: 0x06009A9E RID: 39582 RVA: 0x003F7300 File Offset: 0x003F5700
		public void SetScale()
		{
			this.roots[0].transform.localScale = Vector3.one * this.m_ScaleRate * Studio.optionSystem.manipulateSize;
			this.roots[1].transform.localScale = Vector3.one * 15f * this.m_ScaleRate * 1.1f * this.m_ScaleRot * Studio.optionSystem.manipulateSize;
			this.roots[2].transform.localScale = Vector3.one * this.m_ScaleRate * Studio.optionSystem.manipulateSize;
			this.objectSelect.transform.localScale = Vector3.one * this.m_ScaleRate * this.m_ScaleSelect * Studio.optionSystem.manipulateSize;
			this.m_objCenter.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f) * this.m_ScaleRate * Studio.optionSystem.manipulateSize;
		}

		// Token: 0x06009A9F RID: 39583 RVA: 0x003F7438 File Offset: 0x003F5838
		public void SetLayer(GameObject _object, int _layer)
		{
			if (_object == null)
			{
				return;
			}
			_object.layer = _layer;
			Transform transform = _object.transform;
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.SetLayer(transform.GetChild(i).gameObject, _layer);
			}
		}

		// Token: 0x06009AA0 RID: 39584 RVA: 0x003F748C File Offset: 0x003F588C
		public GuideCommand.EqualsInfo SetWorldPos(Vector3 _pos)
		{
			Vector3 pos = this.m_ChangeAmount.pos;
			if (this.parent && this.nonconnect)
			{
				this.m_ChangeAmount.pos = this.parent.InverseTransformPoint(_pos);
			}
			else
			{
				this.transformTarget.position = _pos;
				this.m_ChangeAmount.pos = this.transformTarget.localPosition;
			}
			return new GuideCommand.EqualsInfo
			{
				dicKey = this.dicKey,
				oldValue = pos,
				newValue = this.m_ChangeAmount.pos
			};
		}

		// Token: 0x06009AA1 RID: 39585 RVA: 0x003F752C File Offset: 0x003F592C
		public void MoveWorld(Vector3 _value)
		{
			if (this.parent && this.nonconnect)
			{
				Vector3 vector = this.m_ChangeAmount.pos;
				vector += this.parent.InverseTransformVector(_value);
				this.m_ChangeAmount.pos = vector;
			}
			else
			{
				Vector3 vector2 = this.transformTarget.position;
				vector2 += _value;
				this.transformTarget.position = vector2;
				this.m_ChangeAmount.pos = this.transformTarget.localPosition;
			}
		}

		// Token: 0x06009AA2 RID: 39586 RVA: 0x003F75BC File Offset: 0x003F59BC
		public void MoveLocal(Vector3 _value, bool _snap, GuideMove.MoveAxis _axis)
		{
			GuideMove.MoveCalc moveCalc = this.moveCalc;
			if (moveCalc != GuideMove.MoveCalc.TYPE1)
			{
				if (moveCalc == GuideMove.MoveCalc.TYPE2 || moveCalc == GuideMove.MoveCalc.TYPE3)
				{
					if (this.parent && this.nonconnect)
					{
						Vector3 vector = this.m_ChangeAmount.pos;
						vector += this.parent.InverseTransformVector(_value);
						this.m_ChangeAmount.pos = ((!_snap) ? vector : this.Parse(vector, _axis));
					}
					else
					{
						Vector3 vector2 = this.transformTarget.position;
						vector2 += _value;
						this.transformTarget.position = ((!_snap) ? vector2 : this.Parse(vector2, _axis));
						this.m_ChangeAmount.pos = this.transformTarget.localPosition;
					}
				}
			}
			else
			{
				Vector3 vector3 = this.m_ChangeAmount.pos;
				vector3 += _value;
				this.m_ChangeAmount.pos = ((!_snap) ? vector3 : this.Parse(vector3, _axis));
			}
		}

		// Token: 0x06009AA3 RID: 39587 RVA: 0x003F76D0 File Offset: 0x003F5AD0
		private Vector3 Parse(Vector3 _src, GuideMove.MoveAxis _axis)
		{
			string format = string.Format("F{0}", 2 - Studio.optionSystem.snap);
			_src[(int)_axis] = float.Parse(_src[(int)_axis].ToString(format));
			return _src;
		}

		// Token: 0x06009AA4 RID: 39588 RVA: 0x003F7718 File Offset: 0x003F5B18
		public void MoveLocal(Vector3 _value)
		{
			Vector3 vector = this.m_ChangeAmount.pos;
			vector += base.transform.InverseTransformVector(_value);
			this.m_ChangeAmount.pos = vector;
		}

		// Token: 0x06009AA5 RID: 39589 RVA: 0x003F7750 File Offset: 0x003F5B50
		public void Rotation(Vector3 _axis, float _angle)
		{
			this.transformTarget.Rotate(_axis, _angle, Space.World);
			this.m_ChangeAmount.rot = this.transformTarget.localEulerAngles;
		}

		// Token: 0x06009AA6 RID: 39590 RVA: 0x003F7776 File Offset: 0x003F5B76
		public void ForceUpdate()
		{
			this.CalcPosition();
			this.CalcRotation();
		}

		// Token: 0x06009AA7 RID: 39591 RVA: 0x003F7784 File Offset: 0x003F5B84
		public void SetEnable(int _pos = -1, int _rot = -1, int _scale = -1)
		{
			if (_pos != -1)
			{
				this.m_Enables[0] = (_pos == 1);
			}
			if (_rot != -1)
			{
				this.m_Enables[1] = (_rot == 1);
			}
			if (_scale != -1)
			{
				this.m_Enables[2] = (_scale == 1);
			}
			this.SetMode(GuideObjectManager.GetMode(), true);
		}

		// Token: 0x06009AA8 RID: 39592 RVA: 0x003F77D6 File Offset: 0x003F5BD6
		public void SetVisibleCenter(bool _value)
		{
			this.m_objCenter.SetActive(_value);
		}

		// Token: 0x06009AA9 RID: 39593 RVA: 0x003F77E4 File Offset: 0x003F5BE4
		private void Awake()
		{
			this.m_DicKey = -1;
			this.isActiveFunc = null;
			this.parentGuide = null;
			this.enableMaluti = true;
			this.calcScale = true;
			this.visibleTranslation = Singleton<Studio>.Instance.workInfo.visibleAxisTranslation;
			this.visibleCenter = Singleton<Studio>.Instance.workInfo.visibleAxisCenter;
			this.SetVisibleCenter(false);
			Renderer component = this.objectSelect.GetComponent<Renderer>();
			if (component)
			{
				component.material.renderQueue = 3500;
			}
			this._visible.Subscribe(delegate(bool _b)
			{
				foreach (GuideBase guideBase in this.guide)
				{
					guideBase.draw = (_b & this.visibleOutside);
				}
			});
			this._visibleOutside.Subscribe(delegate(bool _b)
			{
				foreach (GuideBase guideBase in this.guide)
				{
					guideBase.draw = (_b & this.visible);
				}
			});
			for (int i = 0; i < this.guide.Length; i++)
			{
				this.guide[i].guideObject = this;
			}
		}

		// Token: 0x06009AAA RID: 39594 RVA: 0x003F78C3 File Offset: 0x003F5CC3
		private void Start()
		{
			this.isQuit = false;
			this._visible.Value = true;
		}

		// Token: 0x06009AAB RID: 39595 RVA: 0x003F78D8 File Offset: 0x003F5CD8
		private void LateUpdate()
		{
			if (this.parent && this.nonconnect)
			{
				this.CalcPosition();
				this.CalcRotation();
			}
			base.transform.position = this.transformTarget.position;
			base.transform.rotation = this.transformTarget.rotation;
			GuideObject.Mode mode = this.mode;
			if (mode != GuideObject.Mode.Local)
			{
				if (mode != GuideObject.Mode.LocalIK)
				{
					if (mode == GuideObject.Mode.World)
					{
						this.roots[0].transform.eulerAngles = Vector3.zero;
					}
				}
				else
				{
					this.roots[0].transform.localEulerAngles = Vector3.zero;
				}
			}
			else
			{
				this.roots[0].transform.eulerAngles = ((!this.parent) ? Vector3.zero : this.parent.eulerAngles);
			}
			if (this.calcScale)
			{
				Vector3 localScale = this.transformTarget.localScale;
				Vector3 lossyScale = this.transformTarget.lossyScale;
				Vector3 vector = (!this.enableScale) ? Vector3.one : this.changeAmount.scale;
				this.transformTarget.localScale = new Vector3(localScale.x / lossyScale.x * vector.x, localScale.y / lossyScale.y * vector.y, localScale.z / lossyScale.z * vector.z);
			}
		}

		// Token: 0x06009AAC RID: 39596 RVA: 0x003F7A65 File Offset: 0x003F5E65
		private void OnApplicationQuit()
		{
			this.isQuit = true;
		}

		// Token: 0x06009AAD RID: 39597 RVA: 0x003F7A6E File Offset: 0x003F5E6E
		private void OnDestroy()
		{
			if (this.isQuit)
			{
				return;
			}
			if (Scene.isGameEnd)
			{
				return;
			}
			if (Singleton<GuideObjectManager>.IsInstance())
			{
				Singleton<GuideObjectManager>.Instance.Delete(this, false);
			}
		}

		// Token: 0x04007B3F RID: 31551
		public Transform transformTarget;

		// Token: 0x04007B40 RID: 31552
		private int m_DicKey = -1;

		// Token: 0x04007B41 RID: 31553
		protected ChangeAmount m_ChangeAmount;

		// Token: 0x04007B42 RID: 31554
		[SerializeField]
		protected GameObject[] roots = new GameObject[3];

		// Token: 0x04007B43 RID: 31555
		[SerializeField]
		protected GameObject objectSelect;

		// Token: 0x04007B44 RID: 31556
		[SerializeField]
		private GuideBase[] guide;

		// Token: 0x04007B45 RID: 31557
		[SerializeField]
		private GameObject m_objCenter;

		// Token: 0x04007B46 RID: 31558
		[SerializeField]
		private MeshRenderer rendererCenter;

		// Token: 0x04007B47 RID: 31559
		[SerializeField]
		protected bool[] m_Enables = new bool[]
		{
			true,
			true,
			true,
			true
		};

		// Token: 0x04007B48 RID: 31560
		[SerializeField]
		private bool _calcScale = true;

		// Token: 0x04007B4A RID: 31562
		public GuideObject.IsActiveFunc isActiveFunc;

		// Token: 0x04007B4B RID: 31563
		protected bool m_IsActive;

		// Token: 0x04007B4C RID: 31564
		protected float m_ScaleRate = 1f;

		// Token: 0x04007B4D RID: 31565
		protected float m_ScaleRot = 1f;

		// Token: 0x04007B4E RID: 31566
		protected float m_ScaleSelect = 1f;

		// Token: 0x04007B4F RID: 31567
		public GuideObject parentGuide;

		// Token: 0x04007B50 RID: 31568
		protected BoolReactiveProperty _visible = new BoolReactiveProperty(true);

		// Token: 0x04007B51 RID: 31569
		private BoolReactiveProperty _visibleOutside = new BoolReactiveProperty(true);

		// Token: 0x04007B52 RID: 31570
		public GuideObject.Mode mode;

		// Token: 0x04007B53 RID: 31571
		public Transform parent;

		// Token: 0x04007B54 RID: 31572
		private GuideMove.MoveCalc _moveCalc;

		// Token: 0x04007B55 RID: 31573
		public bool nonconnect;

		// Token: 0x0200124F RID: 4687
		// (Invoke) Token: 0x06009AB3 RID: 39603
		public delegate void IsActiveFunc(bool _active);

		// Token: 0x02001250 RID: 4688
		public enum Mode
		{
			// Token: 0x04007B5A RID: 31578
			Local,
			// Token: 0x04007B5B RID: 31579
			LocalIK,
			// Token: 0x04007B5C RID: 31580
			World
		}
	}
}
