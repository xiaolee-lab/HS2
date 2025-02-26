using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C00 RID: 3072
	public class DropSearchActionPoint : SearchActionPoint
	{
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06005E2C RID: 24108 RVA: 0x0027CE0D File Offset: 0x0027B20D
		public int MapItemID
		{
			[CompilerGenerated]
			get
			{
				return this._mapItemID;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x0027CE15 File Offset: 0x0027B215
		public float CoolTime
		{
			[CompilerGenerated]
			get
			{
				return this._setCoolTime;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x0027CE1D File Offset: 0x0027B21D
		public float CurrentCoolTime
		{
			[CompilerGenerated]
			get
			{
				return this._coolTime;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x0027CE25 File Offset: 0x0027B225
		public bool HaveMapItems
		{
			[CompilerGenerated]
			get
			{
				return !this._mapItems.IsNullOrEmpty<GameObject>();
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x0027CE35 File Offset: 0x0027B235
		public bool IsCoolTime
		{
			[CompilerGenerated]
			get
			{
				return this._isCoolTime;
			}
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0027CE3D File Offset: 0x0027B23D
		private void Awake()
		{
			this._mapItems = AIProject.MapItemData.Get(this._mapItemID);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x0027CE50 File Offset: 0x0027B250
		protected override void Start()
		{
			base.Start();
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				Dictionary<int, float> dictionary = (environment != null) ? environment.DropSearchActionPointCoolTimeTable : null;
				float num;
				if (!dictionary.IsNullOrEmpty<int, float>() && dictionary.TryGetValue(this.RegisterID, out num))
				{
					if (num <= 0f)
					{
						dictionary.Remove(this.RegisterID);
					}
					else
					{
						this._coolTime = num;
						this._isCoolTime = true;
						if (base.gameObject.activeSelf)
						{
							base.gameObject.SetActive(false);
						}
					}
				}
			}
			if (this._updateDisposable != null)
			{
				this._updateDisposable.Dispose();
			}
			this._updateDisposable = Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x0027CF34 File Offset: 0x0027B334
		protected override void OnEnable()
		{
			base.OnEnable();
			if (!this._mapItems.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._mapItems)
				{
					if (gameObject != null && !gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
				}
			}
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0027CFC0 File Offset: 0x0027B3C0
		protected override void OnDisable()
		{
			if (!this._mapItems.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in this._mapItems)
				{
					if (gameObject != null && gameObject.activeSelf)
					{
						gameObject.SetActive(false);
					}
				}
			}
			base.OnDisable();
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0027D04C File Offset: 0x0027B44C
		private void OnDestroy()
		{
			if (this._updateDisposable != null)
			{
				this._updateDisposable.Dispose();
			}
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x0027D068 File Offset: 0x0027B468
		protected override void InitSub()
		{
			base.InitSub();
			if (!this._labels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				foreach (CommandLabel.CommandInfo commandInfo in this._labels)
				{
					if (commandInfo != null)
					{
						commandInfo.CoolTimeFillRate = null;
					}
				}
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0027D0BC File Offset: 0x0027B4BC
		private void OnUpdate()
		{
			if (!this._isCoolTime)
			{
				return;
			}
			if (Mathf.Approximately(Time.timeScale, 0f))
			{
				return;
			}
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			if (simulator == null || !simulator.EnabledTimeProgression)
			{
				return;
			}
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, float> dictionary = (environment != null) ? environment.DropSearchActionPointCoolTimeTable : null;
			if (dictionary == null)
			{
				return;
			}
			float num;
			if (!dictionary.TryGetValue(this.RegisterID, out num))
			{
				this.SetAvailable();
				return;
			}
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			this._coolTime = Mathf.Max(num - unscaledDeltaTime, -1f);
			dictionary[this.RegisterID] = this._coolTime;
			if (this._coolTime <= 0f)
			{
				Camera cameraComponent = Map.GetCameraComponent();
				if (cameraComponent == null)
				{
					return;
				}
				if (!Singleton<Manager.Resources>.IsInstance())
				{
					return;
				}
				LocomotionProfile.DropSearchActionPointSettings dropSearchActionPointSetting = Singleton<Manager.Resources>.Instance.LocomotionProfile.DropSearchActionPointSetting;
				Transform transform = cameraComponent.transform;
				float num2 = Vector3.Angle(transform.forward, base.transform.position - transform.position);
				float num3 = Vector3.Distance(transform.position, base.transform.position);
				if (dropSearchActionPointSetting.AvailableAngle < num2 || dropSearchActionPointSetting.AvailableDistance < num3)
				{
					this._isCoolTime = false;
					this._coolTime = 0f;
					dictionary.Remove(this.RegisterID);
					base.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x0027D258 File Offset: 0x0027B658
		public void SetAvailable()
		{
			this._isCoolTime = false;
			this._coolTime = 0f;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				Dictionary<int, float> dictionary = (environment != null) ? environment.DropSearchActionPointCoolTimeTable : null;
				if (!dictionary.IsNullOrEmpty<int, float>())
				{
					dictionary.Remove(this.RegisterID);
				}
			}
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0027D2D0 File Offset: 0x0027B6D0
		public void SetCoolTime()
		{
			this._isCoolTime = true;
			this._coolTime = this._setCoolTime;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				Dictionary<int, float> dictionary = (environment != null) ? environment.DropSearchActionPointCoolTimeTable : null;
				if (dictionary != null)
				{
					dictionary[this.RegisterID] = (this._coolTime = this._setCoolTime);
				}
			}
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0027D351 File Offset: 0x0027B751
		public override bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			return base.isActiveAndEnabled && !this._isCoolTime && base.Entered(basePosition, distance, radiusA, radiusB, angle, forward);
		}

		// Token: 0x04005428 RID: 21544
		[SerializeField]
		private int _mapItemID = -1;

		// Token: 0x04005429 RID: 21545
		[SerializeField]
		private float _setCoolTime = 2400f;

		// Token: 0x0400542A RID: 21546
		private float _coolTime;

		// Token: 0x0400542B RID: 21547
		private List<GameObject> _mapItems;

		// Token: 0x0400542C RID: 21548
		private bool _isCoolTime;

		// Token: 0x0400542D RID: 21549
		private IDisposable _updateDisposable;
	}
}
