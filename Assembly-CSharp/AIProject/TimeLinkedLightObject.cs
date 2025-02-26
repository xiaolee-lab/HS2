using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C2F RID: 3119
	public class TimeLinkedLightObject : Point
	{
		// Token: 0x06006094 RID: 24724 RVA: 0x002896FF File Offset: 0x00287AFF
		protected virtual void Awake()
		{
			this.ResetMaterial();
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x00289708 File Offset: 0x00287B08
		protected override void Start()
		{
			if (Singleton<Map>.IsInstance())
			{
				Map instance = Singleton<Map>.Instance;
				EnvironmentSimulator simulator = instance.Simulator;
				this.Refresh(simulator.MapLightTimeZone);
				List<TimeLinkedLightObject> timeLinkedLightObjectList = instance.TimeLinkedLightObjectList;
				if (!timeLinkedLightObjectList.Contains(this))
				{
					timeLinkedLightObjectList.Add(this);
				}
			}
			base.Start();
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x00289758 File Offset: 0x00287B58
		protected virtual void OnDestroy()
		{
			if (Singleton<Map>.IsInstance())
			{
				Map instance = Singleton<Map>.Instance;
				List<TimeLinkedLightObject> timeLinkedLightObjectList = instance.TimeLinkedLightObjectList;
				if (timeLinkedLightObjectList.Contains(this))
				{
					timeLinkedLightObjectList.Remove(this);
				}
			}
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x00289790 File Offset: 0x00287B90
		public void Refresh(TimeZone timeZone)
		{
			bool isOn = false;
			if (timeZone != TimeZone.Morning)
			{
				if (timeZone != TimeZone.Day)
				{
					if (timeZone == TimeZone.Night)
					{
						isOn = this._isNight;
					}
				}
				else
				{
					isOn = this._isDay;
				}
			}
			else
			{
				isOn = this._isMorning;
			}
			if (this._isEmission)
			{
				this.RefreshEmissionMode(isOn);
			}
			else
			{
				this.RefreshObjectMode(isOn);
			}
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x002897FC File Offset: 0x00287BFC
		private void RefreshObjectMode(bool isOn)
		{
			if (!this._onModeObjects.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._onModeObjects)
				{
					if (!(gameObject == null) && gameObject.activeSelf != isOn)
					{
						gameObject.SetActive(isOn);
					}
				}
			}
			if (!this._offModeObjects.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject2 in this._offModeObjects)
				{
					if (!(gameObject2 == null) && gameObject2.activeSelf == isOn)
					{
						gameObject2.SetActive(!isOn);
					}
				}
			}
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x002898BC File Offset: 0x00287CBC
		private void RefreshEmissionMode(bool isOn)
		{
			if (this._emissionInfos.IsNullOrEmpty<TimeLinkedLightObject.EmissionInfo>())
			{
				return;
			}
			foreach (TimeLinkedLightObject.EmissionInfo emissionInfo in this._emissionInfos)
			{
				Material material = emissionInfo.Material;
				if (!(material == null))
				{
					if (emissionInfo.IsStrengthSwitching)
					{
						if (isOn)
						{
							if (material.HasProperty(this._emissionStrengthName))
							{
								material.SetFloat(this._emissionStrengthName, emissionInfo.EmissionStrength);
							}
						}
						else if (material.HasProperty(this._emissionStrengthName))
						{
							material.SetFloat(this._emissionStrengthName, 0f);
						}
					}
					else if (emissionInfo.IsEmissionSwitching)
					{
						if (isOn)
						{
							if (!material.IsKeywordEnabled(this._emissionKeyName))
							{
								material.EnableKeyword(this._emissionKeyName);
							}
						}
						else if (material.IsKeywordEnabled(this._emissionKeyName))
						{
							material.DisableKeyword(this._emissionKeyName);
						}
					}
					else if (isOn)
					{
						if (!material.IsKeywordEnabled(this._emissionKeyName))
						{
							material.EnableKeyword(this._emissionKeyName);
						}
						if (material.HasProperty(this._emissionParamName))
						{
							material.SetVector(this._emissionParamName, emissionInfo.OnColor);
						}
					}
					else if (emissionInfo.IsOffModeDisable)
					{
						if (material.IsKeywordEnabled(this._emissionKeyName))
						{
							material.DisableKeyword(this._emissionKeyName);
						}
					}
					else
					{
						if (!material.IsKeywordEnabled(this._emissionKeyName))
						{
							material.EnableKeyword(this._emissionKeyName);
						}
						if (material.HasProperty(this._emissionParamName))
						{
							material.SetVector(this._emissionParamName, emissionInfo.OffColor);
						}
					}
				}
			}
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x00289A88 File Offset: 0x00287E88
		private void ResetMaterial()
		{
			if (this._emissionInfos.IsNullOrEmpty<TimeLinkedLightObject.EmissionInfo>())
			{
				return;
			}
			foreach (TimeLinkedLightObject.EmissionInfo emissionInfo in this._emissionInfos)
			{
				if (emissionInfo != null && !(emissionInfo.Renderer == null))
				{
					Material material = emissionInfo.Renderer.material;
					float num = emissionInfo.EmissionStrength;
					if (num < 0f && material.HasProperty(this._emissionStrengthName))
					{
						num = material.GetFloat(this._emissionStrengthName);
					}
					emissionInfo.SetInfo(material, num);
				}
			}
		}

		// Token: 0x040055AD RID: 21933
		[SerializeField]
		private bool _isEmission = true;

		// Token: 0x040055AE RID: 21934
		[SerializeField]
		private bool _isMorning;

		// Token: 0x040055AF RID: 21935
		[SerializeField]
		private bool _isDay;

		// Token: 0x040055B0 RID: 21936
		[SerializeField]
		private bool _isNight;

		// Token: 0x040055B1 RID: 21937
		[SerializeField]
		private GameObject[] _onModeObjects = new GameObject[0];

		// Token: 0x040055B2 RID: 21938
		[SerializeField]
		private GameObject[] _offModeObjects = new GameObject[0];

		// Token: 0x040055B3 RID: 21939
		[SerializeField]
		private string _emissionParamName = "_EmissionColor";

		// Token: 0x040055B4 RID: 21940
		[SerializeField]
		private string _emissionKeyName = "_EMISSION";

		// Token: 0x040055B5 RID: 21941
		[SerializeField]
		private string _emissionStrengthName = "_EmissionStrength";

		// Token: 0x040055B6 RID: 21942
		[SerializeField]
		private TimeLinkedLightObject.EmissionInfo[] _emissionInfos = new TimeLinkedLightObject.EmissionInfo[0];

		// Token: 0x02000C30 RID: 3120
		[Serializable]
		public class EmissionInfo
		{
			// Token: 0x0600609B RID: 24731 RVA: 0x00289B2C File Offset: 0x00287F2C
			public EmissionInfo()
			{
			}

			// Token: 0x0600609C RID: 24732 RVA: 0x00289B90 File Offset: 0x00287F90
			public EmissionInfo(Renderer ren)
			{
				this._renderer = ren;
			}

			// Token: 0x17001309 RID: 4873
			// (get) Token: 0x0600609D RID: 24733 RVA: 0x00289BFA File Offset: 0x00287FFA
			public Renderer Renderer
			{
				[CompilerGenerated]
				get
				{
					return this._renderer;
				}
			}

			// Token: 0x1700130A RID: 4874
			// (get) Token: 0x0600609E RID: 24734 RVA: 0x00289C02 File Offset: 0x00288002
			public Material Material
			{
				[CompilerGenerated]
				get
				{
					return this._material;
				}
			}

			// Token: 0x1700130B RID: 4875
			// (get) Token: 0x0600609F RID: 24735 RVA: 0x00289C0A File Offset: 0x0028800A
			public float EmissionStrength
			{
				[CompilerGenerated]
				get
				{
					return this._emissionStrength;
				}
			}

			// Token: 0x1700130C RID: 4876
			// (get) Token: 0x060060A0 RID: 24736 RVA: 0x00289C12 File Offset: 0x00288012
			public Color OnColor
			{
				[CompilerGenerated]
				get
				{
					return this._onColor;
				}
			}

			// Token: 0x1700130D RID: 4877
			// (get) Token: 0x060060A1 RID: 24737 RVA: 0x00289C1A File Offset: 0x0028801A
			public Color OffColor
			{
				[CompilerGenerated]
				get
				{
					return this._offColor;
				}
			}

			// Token: 0x1700130E RID: 4878
			// (get) Token: 0x060060A2 RID: 24738 RVA: 0x00289C22 File Offset: 0x00288022
			public bool IsStrengthSwitching
			{
				[CompilerGenerated]
				get
				{
					return this._isStrengthSwitch;
				}
			}

			// Token: 0x1700130F RID: 4879
			// (get) Token: 0x060060A3 RID: 24739 RVA: 0x00289C2A File Offset: 0x0028802A
			public bool IsEmissionSwitching
			{
				[CompilerGenerated]
				get
				{
					return this._isEmissionSwitching;
				}
			}

			// Token: 0x17001310 RID: 4880
			// (get) Token: 0x060060A4 RID: 24740 RVA: 0x00289C32 File Offset: 0x00288032
			public bool IsOffModeDisable
			{
				[CompilerGenerated]
				get
				{
					return this._isOffModeDisable;
				}
			}

			// Token: 0x060060A5 RID: 24741 RVA: 0x00289C3A File Offset: 0x0028803A
			public void SetMaterial(Material material)
			{
				this._material = material;
			}

			// Token: 0x060060A6 RID: 24742 RVA: 0x00289C43 File Offset: 0x00288043
			public void SetInfo(Material material, float strength)
			{
				this._material = material;
				this._emissionStrength = strength;
			}

			// Token: 0x060060A7 RID: 24743 RVA: 0x00289C53 File Offset: 0x00288053
			public void SetStrengthSwitching(bool flag)
			{
				this._isStrengthSwitch = flag;
			}

			// Token: 0x060060A8 RID: 24744 RVA: 0x00289C5C File Offset: 0x0028805C
			public void SetEmissionSwithcing(bool flag)
			{
				this._isEmissionSwitching = flag;
			}

			// Token: 0x060060A9 RID: 24745 RVA: 0x00289C65 File Offset: 0x00288065
			public void SetOffModeDisable(bool flag)
			{
				this._isOffModeDisable = flag;
			}

			// Token: 0x040055B7 RID: 21943
			[SerializeField]
			private Renderer _renderer;

			// Token: 0x040055B8 RID: 21944
			[ShowInInspector]
			[ReadOnly]
			private Material _material;

			// Token: 0x040055B9 RID: 21945
			[SerializeField]
			private bool _isStrengthSwitch;

			// Token: 0x040055BA RID: 21946
			[SerializeField]
			private float _emissionStrength = -1f;

			// Token: 0x040055BB RID: 21947
			[SerializeField]
			private bool _isEmissionSwitching = true;

			// Token: 0x040055BC RID: 21948
			[SerializeField]
			private bool _isOffModeDisable;

			// Token: 0x040055BD RID: 21949
			[SerializeField]
			[ColorUsage(false, true)]
			private Color _onColor = new Color(0f, 0f, 0f, 1f);

			// Token: 0x040055BE RID: 21950
			[SerializeField]
			[ColorUsage(false, true)]
			private Color _offColor = new Color(0f, 0f, 0f, 1f);
		}
	}
}
