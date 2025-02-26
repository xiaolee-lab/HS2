using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BF8 RID: 3064
	public class DevicePointLinkedObject : MonoBehaviour
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x0027B244 File Offset: 0x00279644
		public bool IsEmpty
		{
			[CompilerGenerated]
			get
			{
				return this._lightList.IsNullOrEmpty<DevicePointLinkedObject.OneLight>() && this._enableObjectList.IsNullOrEmpty<GameObject>() && this._disableObjectList.IsNullOrEmpty<GameObject>();
			}
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0027B274 File Offset: 0x00279674
		private void Awake()
		{
			if (this.IsEmpty)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this._enable = this.IsEnable();
			this.SetupList();
			this.UpdateObject(this._enable);
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0027B2E0 File Offset: 0x002796E0
		private void SetupList()
		{
			for (int i = 0; i < this._lightList.Count; i++)
			{
				DevicePointLinkedObject.OneLight oneLight = this._lightList[i];
				if (oneLight == null || oneLight.rend == null)
				{
					this._lightList.RemoveAt(i);
					i--;
				}
				else
				{
					Material material = oneLight.rend.material;
					oneLight.Mat = material;
					if (material == null)
					{
						this._lightList.RemoveAt(i);
						i--;
					}
					else if (!oneLight.Mat.HasProperty(oneLight.emissionParamName))
					{
						this._lightList.RemoveAt(i);
						i--;
					}
					else
					{
						oneLight.Mat.EnableKeyword(oneLight.emissionKeyName);
					}
				}
			}
			this.UpdateColor(this._enable);
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0027B3C0 File Offset: 0x002797C0
		private void OnUpdate()
		{
			bool flag = this.IsEnable();
			if (this._enable != flag)
			{
				this.UpdateElement(this._enable = flag);
			}
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0027B3F0 File Offset: 0x002797F0
		private bool IsEnable()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Dictionary<int, AgentData> dictionary = (worldData != null) ? worldData.AgentTable : null;
			AgentData agentData;
			return dictionary != null && (dictionary.TryGetValue(this._devicePointID, out agentData) && agentData != null) && agentData.OpenState;
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0027B450 File Offset: 0x00279850
		private void UpdateElement(bool enable)
		{
			this.UpdateColor(enable);
			this.UpdateObject(enable);
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0027B460 File Offset: 0x00279860
		private void UpdateColor(bool enable)
		{
			foreach (DevicePointLinkedObject.OneLight oneLight in this._lightList)
			{
				if (oneLight != null && !(oneLight.Mat == null))
				{
					if (oneLight.Mat.IsKeywordEnabled(oneLight.emissionKeyName))
					{
						oneLight.Mat.SetColor(oneLight.emissionParamName, (!enable) ? oneLight.disableColor : oneLight.enableColor);
					}
				}
			}
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x0027B510 File Offset: 0x00279910
		private void UpdateObject(bool enable)
		{
			List<GameObject> list = (!enable) ? this._enableObjectList : this._disableObjectList;
			List<GameObject> list2 = (!enable) ? this._disableObjectList : this._enableObjectList;
			if (!list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in list)
				{
					if (gameObject != null && gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
				}
			}
			if (!list2.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject2 in list2)
				{
					if (gameObject2 != null && !gameObject2.activeSelf)
					{
						gameObject2.SetActive(true);
					}
				}
			}
		}

		// Token: 0x040053FA RID: 21498
		private bool _enable;

		// Token: 0x040053FB RID: 21499
		[SerializeField]
		private int _devicePointID;

		// Token: 0x040053FC RID: 21500
		[SerializeField]
		private List<DevicePointLinkedObject.OneLight> _lightList = new List<DevicePointLinkedObject.OneLight>();

		// Token: 0x040053FD RID: 21501
		[SerializeField]
		private List<GameObject> _enableObjectList = new List<GameObject>();

		// Token: 0x040053FE RID: 21502
		[SerializeField]
		private List<GameObject> _disableObjectList = new List<GameObject>();

		// Token: 0x02000BF9 RID: 3065
		[Serializable]
		public class OneLight
		{
			// Token: 0x17001212 RID: 4626
			// (get) Token: 0x06005E05 RID: 24069 RVA: 0x0027B66C File Offset: 0x00279A6C
			// (set) Token: 0x06005E06 RID: 24070 RVA: 0x0027B674 File Offset: 0x00279A74
			public Material Mat { get; set; }

			// Token: 0x040053FF RID: 21503
			public Renderer rend;

			// Token: 0x04005400 RID: 21504
			public Color enableColor = Color.white;

			// Token: 0x04005401 RID: 21505
			public Color disableColor = Color.white;

			// Token: 0x04005402 RID: 21506
			public string emissionKeyName = string.Empty;

			// Token: 0x04005403 RID: 21507
			public string emissionParamName = string.Empty;
		}
	}
}
