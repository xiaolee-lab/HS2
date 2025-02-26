using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001269 RID: 4713
	public class GuideObjectManager : Singleton<GuideObjectManager>
	{
		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06009BF8 RID: 39928 RVA: 0x003FC5F3 File Offset: 0x003FA9F3
		public GuideInput guideInput
		{
			get
			{
				return this.m_GuideInput;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06009BF9 RID: 39929 RVA: 0x003FC5FB File Offset: 0x003FA9FB
		public DrawLightLine drawLightLine
		{
			get
			{
				return this.m_DrawLightLine;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06009BFA RID: 39930 RVA: 0x003FC603 File Offset: 0x003FAA03
		// (set) Token: 0x06009BFB RID: 39931 RVA: 0x003FC628 File Offset: 0x003FAA28
		public GuideObject selectObject
		{
			get
			{
				return (this.hashSelectObject.Count == 0) ? null : this.hashSelectObject.ToArray<GuideObject>()[0];
			}
			set
			{
				this.SetSelectObject(value, true);
			}
		}

		// Token: 0x17002163 RID: 8547
		// (set) Token: 0x06009BFC RID: 39932 RVA: 0x003FC632 File Offset: 0x003FAA32
		public GuideObject deselectObject
		{
			set
			{
				this.SetDeselectObject(value);
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06009BFD RID: 39933 RVA: 0x003FC63B File Offset: 0x003FAA3B
		public GuideObject[] selectObjects
		{
			get
			{
				return (this.hashSelectObject.Count == 0) ? null : this.hashSelectObject.ToArray<GuideObject>();
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06009BFE RID: 39934 RVA: 0x003FC65E File Offset: 0x003FAA5E
		public ChangeAmount[] selectObjectChangeAmount
		{
			get
			{
				return (from v in this.hashSelectObject
				select v.changeAmount).ToArray<ChangeAmount>();
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06009BFF RID: 39935 RVA: 0x003FC68D File Offset: 0x003FAA8D
		public int[] selectObjectKey
		{
			get
			{
				return (from v in this.hashSelectObject
				select v.dicKey).ToArray<int>();
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06009C00 RID: 39936 RVA: 0x003FC6BC File Offset: 0x003FAABC
		public Dictionary<int, ChangeAmount> selectObjectDictionary
		{
			get
			{
				return this.hashSelectObject.ToDictionary((GuideObject v) => v.dicKey, (GuideObject v) => v.changeAmount);
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06009C01 RID: 39937 RVA: 0x003FC70E File Offset: 0x003FAB0E
		// (set) Token: 0x06009C02 RID: 39938 RVA: 0x003FC716 File Offset: 0x003FAB16
		public GuideObject operationTarget { get; set; }

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06009C03 RID: 39939 RVA: 0x003FC71F File Offset: 0x003FAB1F
		public bool isOperationTarget
		{
			get
			{
				return this.operationTarget != null;
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06009C04 RID: 39940 RVA: 0x003FC72D File Offset: 0x003FAB2D
		// (set) Token: 0x06009C05 RID: 39941 RVA: 0x003FC735 File Offset: 0x003FAB35
		public int mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				if (Utility.SetStruct<int>(ref this.m_Mode, value))
				{
					this.SetMode(this.m_Mode);
					if (this.ModeChangeEvent != null)
					{
						this.ModeChangeEvent(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06009C06 RID: 39942 RVA: 0x003FC770 File Offset: 0x003FAB70
		public static int GetMode()
		{
			if (!Singleton<GuideObjectManager>.IsInstance())
			{
				return 0;
			}
			return Singleton<GuideObjectManager>.Instance.mode;
		}

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06009C07 RID: 39943 RVA: 0x003FC788 File Offset: 0x003FAB88
		// (remove) Token: 0x06009C08 RID: 39944 RVA: 0x003FC7C0 File Offset: 0x003FABC0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ModeChangeEvent;

		// Token: 0x06009C09 RID: 39945 RVA: 0x003FC7F8 File Offset: 0x003FABF8
		public GuideObject Add(Transform _target, int _dicKey)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectOriginal);
			gameObject.transform.SetParent(this.transformWorkplace);
			GuideObject component = gameObject.GetComponent<GuideObject>();
			component.transformTarget = _target;
			component.dicKey = _dicKey;
			this.dicGuideObject.Add(_target, component);
			Light component2 = _target.GetComponent<Light>();
			if (component2 && component2.type != LightType.Directional)
			{
				this.dicTransLight.Add(_target, component2);
			}
			return component;
		}

		// Token: 0x06009C0A RID: 39946 RVA: 0x003FC870 File Offset: 0x003FAC70
		public void Delete(GuideObject _object, bool _destroy = true)
		{
			if (_object == null)
			{
				return;
			}
			if (this.hashSelectObject.Contains(_object))
			{
				this.SetSelectObject(null, false);
			}
			this.dicGuideObject.Remove(_object.transformTarget);
			this.dicTransLight.Remove(_object.transformTarget);
			this.dicGuideLight.Remove(_object);
			if (_destroy)
			{
				UnityEngine.Object.DestroyImmediate(_object.gameObject);
			}
			if (this.operationTarget == _object)
			{
				this.operationTarget = null;
			}
		}

		// Token: 0x06009C0B RID: 39947 RVA: 0x003FC900 File Offset: 0x003FAD00
		public void DeleteAll()
		{
			this.hashSelectObject.Clear();
			this.operationTarget = null;
			GameObject[] array = (from v in this.dicGuideObject
			where v.Value != null
			select v.Value.gameObject).ToArray<GameObject>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					UnityEngine.Object.DestroyImmediate(array[i]);
				}
			}
			this.dicGuideObject.Clear();
			this.dicTransLight.Clear();
			this.dicGuideLight.Clear();
			this.drawLightLine.Clear();
			this.guideInput.Stop();
		}

		// Token: 0x06009C0C RID: 39948 RVA: 0x003FC9D0 File Offset: 0x003FADD0
		public void AddSelectMultiple(GuideObject _object)
		{
			if (_object == null)
			{
				return;
			}
			if (this.hashSelectObject.Contains(_object))
			{
				return;
			}
			if (this.hashSelectObject.Count != 0 && !_object.enableMaluti)
			{
				return;
			}
			this.AddObject(_object, this.hashSelectObject.Count == 0);
			this.guideInput.AddSelectMultiple(_object);
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x003FCA3C File Offset: 0x003FAE3C
		public void SetScale()
		{
			foreach (KeyValuePair<Transform, GuideObject> keyValuePair in this.dicGuideObject)
			{
				keyValuePair.Value.SetScale();
			}
		}

		// Token: 0x06009C0E RID: 39950 RVA: 0x003FCAA0 File Offset: 0x003FAEA0
		public void SetVisibleTranslation()
		{
			bool visibleAxisTranslation = Singleton<Studio>.Instance.workInfo.visibleAxisTranslation;
			foreach (KeyValuePair<Transform, GuideObject> keyValuePair in this.dicGuideObject)
			{
				keyValuePair.Value.visibleTranslation = visibleAxisTranslation;
			}
		}

		// Token: 0x06009C0F RID: 39951 RVA: 0x003FCB14 File Offset: 0x003FAF14
		public void SetVisibleCenter()
		{
			bool visibleAxisCenter = Singleton<Studio>.Instance.workInfo.visibleAxisCenter;
			foreach (KeyValuePair<Transform, GuideObject> keyValuePair in this.dicGuideObject)
			{
				keyValuePair.Value.visibleCenter = visibleAxisCenter;
			}
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x003FCB88 File Offset: 0x003FAF88
		private void SetMode(int _mode)
		{
			foreach (GuideObject guideObject in this.hashSelectObject)
			{
				guideObject.SetMode(_mode, true);
			}
		}

		// Token: 0x06009C11 RID: 39953 RVA: 0x003FCBE8 File Offset: 0x003FAFE8
		private void SetSelectObject(GuideObject _object, bool _multiple = true)
		{
			bool flag = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			bool key = Input.GetKey(KeyCode.X);
			if (_multiple && flag && !key)
			{
				if (_object == null)
				{
					return;
				}
				if (this.hashSelectObject.Contains(_object))
				{
					return;
				}
				if (this.hashSelectObject.Count != 0 && !_object.enableMaluti)
				{
					return;
				}
				this.AddObject(_object, this.hashSelectObject.Count == 0);
			}
			else
			{
				int selectedState = Studio.optionSystem.selectedState;
				if (selectedState != 0)
				{
					if (selectedState == 1)
					{
						GuideObject selectObject = this.selectObject;
						if (!(selectObject == null))
						{
							if (!selectObject.isChild)
							{
								if (_object && _object.isChild)
								{
									selectObject.SetActive(false, false);
								}
								else
								{
									this.StopSelectObject();
								}
							}
							else
							{
								selectObject.SetActive(false, false);
							}
						}
					}
				}
				else
				{
					this.StopSelectObject();
				}
				this.hashSelectObject.Clear();
				if (_object && !_object.enables[this.m_Mode])
				{
					for (int i = 0; i < 3; i++)
					{
						if (_object.enables[i])
						{
							this.mode = i;
							break;
						}
					}
				}
				this.AddObject(_object, true);
			}
			this.guideInput.guideObject = _object;
		}

		// Token: 0x06009C12 RID: 39954 RVA: 0x003FCD7C File Offset: 0x003FB17C
		private void SetDeselectObject(GuideObject _object)
		{
			if (_object == null)
			{
				return;
			}
			bool isActive = _object.isActive;
			_object.isActive = false;
			Light light = null;
			if (this.dicTransLight.TryGetValue(_object.transformTarget, out light))
			{
				this.drawLightLine.Remove(light);
				this.dicGuideLight.Remove(_object);
			}
			this.hashSelectObject.Remove(_object);
			this.guideInput.deselectObject = _object;
			if (this.hashSelectObject.Count > 0 && isActive)
			{
				this.selectObject.isActive = true;
			}
		}

		// Token: 0x06009C13 RID: 39955 RVA: 0x003FCE14 File Offset: 0x003FB214
		private void StopSelectObject()
		{
			foreach (GuideObject guideObject in this.hashSelectObject)
			{
				guideObject.isActive = false;
				Light light = null;
				if (this.dicGuideLight.TryGetValue(guideObject, out light))
				{
					this.drawLightLine.Remove(light);
					this.dicGuideLight.Remove(guideObject);
				}
			}
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x003FCEA0 File Offset: 0x003FB2A0
		private void AddObject(GuideObject _object, bool _active = true)
		{
			if (_object == null)
			{
				return;
			}
			if (_active)
			{
				_object.isActive = true;
			}
			Light light = null;
			if (this.dicTransLight.TryGetValue(_object.transformTarget, out light))
			{
				this.drawLightLine.Add(light);
				this.dicGuideLight.Add(_object, light);
			}
			this.hashSelectObject.Add(_object);
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x003FCF07 File Offset: 0x003FB307
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			if (this.transformWorkplace == null)
			{
				this.transformWorkplace = base.transform;
			}
			this.operationTarget = null;
		}

		// Token: 0x04007C3C RID: 31804
		[SerializeField]
		private GameObject objectOriginal;

		// Token: 0x04007C3D RID: 31805
		[SerializeField]
		private GuideInput m_GuideInput;

		// Token: 0x04007C3E RID: 31806
		[SerializeField]
		private Transform transformWorkplace;

		// Token: 0x04007C3F RID: 31807
		[SerializeField]
		private DrawLightLine m_DrawLightLine;

		// Token: 0x04007C40 RID: 31808
		private HashSet<GuideObject> hashSelectObject = new HashSet<GuideObject>();

		// Token: 0x04007C42 RID: 31810
		private int m_Mode;

		// Token: 0x04007C44 RID: 31812
		private Dictionary<Transform, GuideObject> dicGuideObject = new Dictionary<Transform, GuideObject>();

		// Token: 0x04007C45 RID: 31813
		private Dictionary<Transform, Light> dicTransLight = new Dictionary<Transform, Light>();

		// Token: 0x04007C46 RID: 31814
		private Dictionary<GuideObject, Light> dicGuideLight = new Dictionary<GuideObject, Light>();
	}
}
