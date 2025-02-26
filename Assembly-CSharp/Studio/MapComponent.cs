using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using LuxWater;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001287 RID: 4743
	public class MapComponent : MonoBehaviour
	{
		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06009D0C RID: 40204 RVA: 0x004036AA File Offset: 0x00401AAA
		public bool CheckOption
		{
			[CompilerGenerated]
			get
			{
				return !this.optionInfos.IsNullOrEmpty<MapComponent.OptionInfo>();
			}
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x004036BC File Offset: 0x00401ABC
		public void SetOptionVisible(bool _value)
		{
			if (this.optionInfos.IsNullOrEmpty<MapComponent.OptionInfo>())
			{
				return;
			}
			foreach (MapComponent.OptionInfo optionInfo in this.optionInfos)
			{
				optionInfo.Visible = _value;
			}
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x00403700 File Offset: 0x00401B00
		public void SetOptionVisible(int _idx, bool _value)
		{
			this.optionInfos.SafeProc(_idx, delegate(MapComponent.OptionInfo _info)
			{
				_info.Visible = _value;
			});
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x00403733 File Offset: 0x00401B33
		public void SetSeaRenderer()
		{
			if (this.objSeaParent == null)
			{
				return;
			}
			this.renderersSea = this.objSeaParent.GetComponentsInChildren<Renderer>();
		}

		// Token: 0x06009D10 RID: 40208 RVA: 0x00403758 File Offset: 0x00401B58
		public void SetupSea()
		{
			if (this.renderersSea.IsNullOrEmpty<Renderer>())
			{
				return;
			}
			this.LateUpdateAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				foreach (Renderer renderer in from v in this.renderersSea
				where v != null
				select v)
				{
					Material material = renderer.material;
					material.DisableKeyword("USINGWATERVOLUME");
					renderer.material = material;
				}
			});
		}

		// Token: 0x04007CEB RID: 31979
		[Header("オプション")]
		public MapComponent.OptionInfo[] optionInfos;

		// Token: 0x04007CEC RID: 31980
		[Header("海面関係")]
		public GameObject objSeaParent;

		// Token: 0x04007CED RID: 31981
		public Renderer[] renderersSea;

		// Token: 0x04007CEE RID: 31982
		private Dictionary<Renderer, MapComponent.SeaInfo> dicSeaInfo;

		// Token: 0x02001288 RID: 4744
		[Serializable]
		public class OptionInfo
		{
			// Token: 0x17002198 RID: 8600
			// (set) Token: 0x06009D14 RID: 40212 RVA: 0x00403830 File Offset: 0x00401C30
			public bool Visible
			{
				set
				{
					if (value)
					{
						this.SetVisible(this.objectsOff, false);
						this.SetVisible(this.objectsOn, true);
					}
					else
					{
						this.SetVisible(this.objectsOn, false);
						this.SetVisible(this.objectsOff, true);
					}
				}
			}

			// Token: 0x06009D15 RID: 40213 RVA: 0x0040387C File Offset: 0x00401C7C
			private void SetVisible(GameObject[] _objects, bool _value)
			{
				foreach (GameObject self in from v in _objects
				where v != null
				select v)
				{
					self.SetActiveIfDifferent(_value);
				}
			}

			// Token: 0x04007CF0 RID: 31984
			public GameObject[] objectsOn;

			// Token: 0x04007CF1 RID: 31985
			public GameObject[] objectsOff;
		}

		// Token: 0x02001289 RID: 4745
		private class SeaInfo
		{
			// Token: 0x06009D17 RID: 40215 RVA: 0x004038FD File Offset: 0x00401CFD
			public SeaInfo(Collider _collider, LuxWater_WaterVolume _waterVolume)
			{
				this.Collider = _collider;
				this.WaterVolume = _waterVolume;
			}

			// Token: 0x17002199 RID: 8601
			// (get) Token: 0x06009D18 RID: 40216 RVA: 0x00403913 File Offset: 0x00401D13
			// (set) Token: 0x06009D19 RID: 40217 RVA: 0x0040391B File Offset: 0x00401D1B
			public Collider Collider { get; private set; }

			// Token: 0x1700219A RID: 8602
			// (get) Token: 0x06009D1A RID: 40218 RVA: 0x00403924 File Offset: 0x00401D24
			// (set) Token: 0x06009D1B RID: 40219 RVA: 0x0040392C File Offset: 0x00401D2C
			public LuxWater_WaterVolume WaterVolume { get; private set; }

			// Token: 0x1700219B RID: 8603
			// (set) Token: 0x06009D1C RID: 40220 RVA: 0x00403935 File Offset: 0x00401D35
			public bool Enable
			{
				set
				{
					if (this.Collider != null)
					{
						this.Collider.enabled = value;
					}
					if (this.WaterVolume != null)
					{
						this.WaterVolume.enabled = value;
					}
				}
			}
		}
	}
}
