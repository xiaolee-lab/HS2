using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using Sound;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace AIProject
{
	// Token: 0x02001043 RID: 4163
	public class EnvLineArea3DSE : SerializedMonoBehaviour
	{
		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x06008BB4 RID: 35764 RVA: 0x003ABEC2 File Offset: 0x003AA2C2
		public List<EnvLineArea3DSE.EnvironmentSEInfo> SEInfoList
		{
			[CompilerGenerated]
			get
			{
				return this._seInfoList;
			}
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06008BB5 RID: 35765 RVA: 0x003ABECA File Offset: 0x003AA2CA
		// (set) Token: 0x06008BB6 RID: 35766 RVA: 0x003ABED2 File Offset: 0x003AA2D2
		public bool Playing { get; private set; }

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06008BB7 RID: 35767 RVA: 0x003ABEDB File Offset: 0x003AA2DB
		public EnvLineArea3DSE.PlayInfo[] PlayInfos
		{
			get
			{
				return this._playInfos;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06008BB8 RID: 35768 RVA: 0x003ABEE3 File Offset: 0x003AA2E3
		private static List<EnvArea3DSE.IPlayInfo> PlayAudioSourceList
		{
			get
			{
				return EnvArea3DSE.PlayAudioSourceList;
			}
		}

		// Token: 0x06008BB9 RID: 35769 RVA: 0x003ABEEA File Offset: 0x003AA2EA
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x003ABEF4 File Offset: 0x003AA2F4
		private void Start()
		{
			(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			where this.Playing
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06008BBB RID: 35771 RVA: 0x003ABF40 File Offset: 0x003AA340
		private void OnEnable()
		{
			this.Initialize();
		}

		// Token: 0x06008BBC RID: 35772 RVA: 0x003ABF48 File Offset: 0x003AA348
		private void OnDisable()
		{
			this.End();
		}

		// Token: 0x06008BBD RID: 35773 RVA: 0x003ABF50 File Offset: 0x003AA350
		private void OnDestroy()
		{
			this.End();
		}

		// Token: 0x06008BBE RID: 35774 RVA: 0x003ABF58 File Offset: 0x003AA358
		private void Initialize()
		{
			this.End();
			if (this._initFlag)
			{
				if (!this._playInfos.IsNullOrEmpty<EnvLineArea3DSE.PlayInfo>())
				{
					foreach (EnvLineArea3DSE.PlayInfo playInfo in this._playInfos)
					{
						if (playInfo != null)
						{
							playInfo.ResetDelay();
						}
					}
				}
				this.Playing = true;
				return;
			}
			this._initFlag = true;
			if (!this._seInfoList.IsNullOrEmpty<EnvLineArea3DSE.EnvironmentSEInfo>())
			{
				this._playInfos = new EnvLineArea3DSE.PlayInfo[this._seInfoList.Count];
				for (int j = 0; j < this._playInfos.Length; j++)
				{
					this._playInfos[j] = EnvLineArea3DSE.PlayInfo.Convert(this._seInfoList[j]);
					this._playInfos[j].ResetDelay();
				}
			}
			this.Playing = true;
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x003AC030 File Offset: 0x003AA430
		private void End()
		{
			if (!this.Playing)
			{
				return;
			}
			if (!this._playInfos.IsNullOrEmpty<EnvLineArea3DSE.PlayInfo>())
			{
				foreach (EnvLineArea3DSE.PlayInfo playInfo in this._playInfos)
				{
					if (playInfo != null)
					{
						playInfo.Release();
					}
				}
			}
			this.Playing = false;
		}

		// Token: 0x06008BC0 RID: 35776 RVA: 0x003AC090 File Offset: 0x003AA490
		private void OnUpdate()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (this._playInfos.IsNullOrEmpty<EnvLineArea3DSE.PlayInfo>())
			{
				return;
			}
			Map instance = Singleton<Map>.Instance;
			Manager.Resources res = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
			EnvironmentSimulator simulator = instance.Simulator;
			foreach (EnvLineArea3DSE.PlayInfo playInfo in this._playInfos)
			{
				playInfo.Update(simulator.Weather, simulator.TimeZone, res, instance);
			}
		}

		// Token: 0x06008BC1 RID: 35777 RVA: 0x003AC117 File Offset: 0x003AA517
		public void LoadFromExcelData(ExcelData data)
		{
		}

		// Token: 0x04007207 RID: 29191
		[SerializeField]
		[LabelText("概要")]
		private string _summary = string.Empty;

		// Token: 0x04007208 RID: 29192
		[SerializeField]
		[FormerlySerializedAs("SEList")]
		[LabelText("環境音リスト")]
		private List<EnvLineArea3DSE.EnvironmentSEInfo> _seInfoList = new List<EnvLineArea3DSE.EnvironmentSEInfo>();

		// Token: 0x0400720A RID: 29194
		private EnvLineArea3DSE.PlayInfo[] _playInfos = new EnvLineArea3DSE.PlayInfo[0];

		// Token: 0x0400720B RID: 29195
		private bool _initFlag;

		// Token: 0x02001044 RID: 4164
		public class PlayInfo : EnvArea3DSE.IPlayInfo
		{
			// Token: 0x17001E75 RID: 7797
			// (get) Token: 0x06008BC6 RID: 35782 RVA: 0x003AC198 File Offset: 0x003AA598
			// (set) Token: 0x06008BC7 RID: 35783 RVA: 0x003AC1A0 File Offset: 0x003AA5A0
			public bool FirstPlaying { get; private set; } = true;

			// Token: 0x17001E76 RID: 7798
			// (get) Token: 0x06008BC8 RID: 35784 RVA: 0x003AC1A9 File Offset: 0x003AA5A9
			// (set) Token: 0x06008BC9 RID: 35785 RVA: 0x003AC1B1 File Offset: 0x003AA5B1
			public List<Transform> Roots { get; set; } = new List<Transform>();

			// Token: 0x17001E77 RID: 7799
			// (get) Token: 0x06008BCA RID: 35786 RVA: 0x003AC1BA File Offset: 0x003AA5BA
			// (set) Token: 0x06008BCB RID: 35787 RVA: 0x003AC1C2 File Offset: 0x003AA5C2
			public List<EnvLineArea3DSE.LineT> Lines { get; set; } = new List<EnvLineArea3DSE.LineT>();

			// Token: 0x17001E78 RID: 7800
			// (get) Token: 0x06008BCC RID: 35788 RVA: 0x003AC1CB File Offset: 0x003AA5CB
			// (set) Token: 0x06008BCD RID: 35789 RVA: 0x003AC1D3 File Offset: 0x003AA5D3
			public int ClipID { get; set; } = -1;

			// Token: 0x17001E79 RID: 7801
			// (get) Token: 0x06008BCE RID: 35790 RVA: 0x003AC1DC File Offset: 0x003AA5DC
			// (set) Token: 0x06008BCF RID: 35791 RVA: 0x003AC1E4 File Offset: 0x003AA5E4
			public bool IsMooning { get; set; }

			// Token: 0x17001E7A RID: 7802
			// (get) Token: 0x06008BD0 RID: 35792 RVA: 0x003AC1ED File Offset: 0x003AA5ED
			// (set) Token: 0x06008BD1 RID: 35793 RVA: 0x003AC1F5 File Offset: 0x003AA5F5
			public bool IsNoon { get; set; }

			// Token: 0x17001E7B RID: 7803
			// (get) Token: 0x06008BD2 RID: 35794 RVA: 0x003AC1FE File Offset: 0x003AA5FE
			// (set) Token: 0x06008BD3 RID: 35795 RVA: 0x003AC206 File Offset: 0x003AA606
			public bool IsNight { get; set; }

			// Token: 0x17001E7C RID: 7804
			// (get) Token: 0x06008BD4 RID: 35796 RVA: 0x003AC20F File Offset: 0x003AA60F
			// (set) Token: 0x06008BD5 RID: 35797 RVA: 0x003AC217 File Offset: 0x003AA617
			public bool IsClear { get; set; }

			// Token: 0x17001E7D RID: 7805
			// (get) Token: 0x06008BD6 RID: 35798 RVA: 0x003AC220 File Offset: 0x003AA620
			// (set) Token: 0x06008BD7 RID: 35799 RVA: 0x003AC228 File Offset: 0x003AA628
			public bool IsCloud { get; set; }

			// Token: 0x17001E7E RID: 7806
			// (get) Token: 0x06008BD8 RID: 35800 RVA: 0x003AC231 File Offset: 0x003AA631
			// (set) Token: 0x06008BD9 RID: 35801 RVA: 0x003AC239 File Offset: 0x003AA639
			public bool IsRain { get; set; }

			// Token: 0x17001E7F RID: 7807
			// (get) Token: 0x06008BDA RID: 35802 RVA: 0x003AC242 File Offset: 0x003AA642
			// (set) Token: 0x06008BDB RID: 35803 RVA: 0x003AC24A File Offset: 0x003AA64A
			public bool IsFog { get; set; }

			// Token: 0x17001E80 RID: 7808
			// (get) Token: 0x06008BDC RID: 35804 RVA: 0x003AC253 File Offset: 0x003AA653
			// (set) Token: 0x06008BDD RID: 35805 RVA: 0x003AC25B File Offset: 0x003AA65B
			public Threshold Decay { get; set; } = default(Threshold);

			// Token: 0x17001E81 RID: 7809
			// (get) Token: 0x06008BDE RID: 35806 RVA: 0x003AC264 File Offset: 0x003AA664
			// (set) Token: 0x06008BDF RID: 35807 RVA: 0x003AC26C File Offset: 0x003AA66C
			public bool IsLoop { get; set; }

			// Token: 0x17001E82 RID: 7810
			// (get) Token: 0x06008BE0 RID: 35808 RVA: 0x003AC275 File Offset: 0x003AA675
			// (set) Token: 0x06008BE1 RID: 35809 RVA: 0x003AC27D File Offset: 0x003AA67D
			public Threshold Interval { get; set; } = default(Threshold);

			// Token: 0x17001E83 RID: 7811
			// (get) Token: 0x06008BE2 RID: 35810 RVA: 0x003AC286 File Offset: 0x003AA686
			// (set) Token: 0x06008BE3 RID: 35811 RVA: 0x003AC28E File Offset: 0x003AA68E
			public AudioSource Audio { get; private set; }

			// Token: 0x17001E84 RID: 7812
			// (get) Token: 0x06008BE4 RID: 35812 RVA: 0x003AC297 File Offset: 0x003AA697
			// (set) Token: 0x06008BE5 RID: 35813 RVA: 0x003AC29F File Offset: 0x003AA69F
			public float ElapsedTime { get; private set; }

			// Token: 0x17001E85 RID: 7813
			// (get) Token: 0x06008BE6 RID: 35814 RVA: 0x003AC2A8 File Offset: 0x003AA6A8
			// (set) Token: 0x06008BE7 RID: 35815 RVA: 0x003AC2B0 File Offset: 0x003AA6B0
			public float DelayTime { get; private set; }

			// Token: 0x17001E86 RID: 7814
			// (get) Token: 0x06008BE8 RID: 35816 RVA: 0x003AC2B9 File Offset: 0x003AA6B9
			// (set) Token: 0x06008BE9 RID: 35817 RVA: 0x003AC2C1 File Offset: 0x003AA6C1
			public bool IsPlay { get; private set; }

			// Token: 0x17001E87 RID: 7815
			// (get) Token: 0x06008BEA RID: 35818 RVA: 0x003AC2CA File Offset: 0x003AA6CA
			// (set) Token: 0x06008BEB RID: 35819 RVA: 0x003AC2D2 File Offset: 0x003AA6D2
			public bool IsEnableDistance { get; private set; }

			// Token: 0x17001E88 RID: 7816
			// (get) Token: 0x06008BEC RID: 35820 RVA: 0x003AC2DB File Offset: 0x003AA6DB
			// (set) Token: 0x06008BED RID: 35821 RVA: 0x003AC2E3 File Offset: 0x003AA6E3
			public bool PlayEnable { get; private set; }

			// Token: 0x17001E89 RID: 7817
			// (get) Token: 0x06008BEE RID: 35822 RVA: 0x003AC2EC File Offset: 0x003AA6EC
			// (set) Token: 0x06008BEF RID: 35823 RVA: 0x003AC2F4 File Offset: 0x003AA6F4
			public bool LoadSuccess { get; private set; }

			// Token: 0x17001E8A RID: 7818
			// (get) Token: 0x06008BF0 RID: 35824 RVA: 0x003AC2FD File Offset: 0x003AA6FD
			// (set) Token: 0x06008BF1 RID: 35825 RVA: 0x003AC305 File Offset: 0x003AA705
			public EnvLineArea3DSE.LineT NearLine { get; private set; } = default(EnvLineArea3DSE.LineT);

			// Token: 0x06008BF2 RID: 35826 RVA: 0x003AC310 File Offset: 0x003AA710
			private bool EnableDistance(Manager.Resources res, Map map, Vector3 hitPos)
			{
				if (res == null)
				{
					return false;
				}
				Vector3 position = map.Player.CameraControl.CameraComponent.transform.position;
				float num = Vector3.SqrMagnitude(hitPos - position);
				float num2 = this.Decay.max + res.SoundPack.EnviroInfo.EnableDistance;
				num2 *= num2;
				return num <= num2;
			}

			// Token: 0x06008BF3 RID: 35827 RVA: 0x003AC380 File Offset: 0x003AA780
			private bool DisableDistance(Manager.Resources res, Map map, Vector3 hitPos)
			{
				if (res == null)
				{
					return true;
				}
				Vector3 position = map.Player.CameraControl.CameraComponent.transform.position;
				float num = Vector3.SqrMagnitude(hitPos - position);
				float num2 = this.Decay.max + res.SoundPack.EnviroInfo.DisableDistance;
				num2 *= num2;
				return num2 < num;
			}

			// Token: 0x06008BF4 RID: 35828 RVA: 0x003AC3EC File Offset: 0x003AA7EC
			public float GetSqrDistanceFromCamera(Transform camera, Vector3 p1)
			{
				Vector3 vector = p1 - camera.position;
				return Vector3.SqrMagnitude(vector);
			}

			// Token: 0x06008BF5 RID: 35829 RVA: 0x003AC40C File Offset: 0x003AA80C
			public int GetSqrDistanceSort(Transform camera, Transform t1, Transform t2)
			{
				Vector3 vector = t1.position - camera.position;
				Vector3 vector2 = t2.position - camera.position;
				return (int)(Vector3.SqrMagnitude(vector) - Vector3.SqrMagnitude(vector2));
			}

			// Token: 0x06008BF6 RID: 35830 RVA: 0x003AC44C File Offset: 0x003AA84C
			public static EnvLineArea3DSE.PlayInfo Convert(EnvLineArea3DSE.EnvironmentSEInfo envInfo)
			{
				EnvLineArea3DSE.PlayInfo playInfo = new EnvLineArea3DSE.PlayInfo();
				playInfo.FirstPlaying = true;
				playInfo.Roots.Clear();
				playInfo.Roots.AddRange(envInfo.Roots);
				playInfo.ClipID = envInfo.ClipID;
				playInfo.IsMooning = envInfo.IsMooning;
				playInfo.IsNoon = envInfo.IsNoon;
				playInfo.IsNight = envInfo.IsNight;
				playInfo.IsClear = envInfo.IsClear;
				playInfo.IsCloud = envInfo.IsCloud;
				playInfo.IsRain = envInfo.IsRain;
				playInfo.IsFog = envInfo.IsFog;
				playInfo.Decay = envInfo.Decay;
				playInfo.IsLoop = envInfo.IsLoop;
				playInfo.Interval = envInfo.Interval;
				playInfo.Audio = null;
				playInfo.FadePlayer = null;
				playInfo.ElapsedTime = 0f;
				playInfo.DelayTime = 0f;
				playInfo.IsPlay = false;
				playInfo.IsEnableDistance = false;
				playInfo.PlayEnable = false;
				playInfo.LoadSuccess = false;
				playInfo.NearLine = default(EnvLineArea3DSE.LineT);
				if (!envInfo.Roots.IsNullOrEmpty<Transform>() && 2 <= envInfo.Roots.Count)
				{
					for (int i = 0; i < envInfo.Roots.Count - 1; i++)
					{
						EnvLineArea3DSE.LineT item = new EnvLineArea3DSE.LineT
						{
							P1 = envInfo.Roots[i],
							P2 = envInfo.Roots[i + 1]
						};
						playInfo.Lines.Add(item);
					}
				}
				return playInfo;
			}

			// Token: 0x06008BF7 RID: 35831 RVA: 0x003AC5D4 File Offset: 0x003AA9D4
			public bool Equal(EnvLineArea3DSE.EnvironmentSEInfo eInfo)
			{
				if ((this.Roots == null && eInfo.Roots != null) || (this.Roots != null && eInfo.Roots == null))
				{
					return false;
				}
				if (this.Roots != null && eInfo.Roots != null)
				{
					if (this.Roots.Count != eInfo.Roots.Count)
					{
						return false;
					}
					for (int i = 0; i < this.Roots.Count; i++)
					{
						if (this.Roots[i] != eInfo.Roots[i])
						{
							return false;
						}
					}
				}
				return this.ClipID == eInfo.ClipID && this.IsMooning == eInfo.IsMooning && this.IsNoon == eInfo.IsNoon && this.IsNight == eInfo.IsNight && this.IsClear == eInfo.IsClear && this.IsCloud == eInfo.IsCloud && this.IsRain == eInfo.IsRain && this.IsFog == eInfo.IsFog && this.Decay.min == eInfo.Decay.min && this.Decay.max == eInfo.Decay.max && this.IsLoop == eInfo.IsLoop && this.Interval.min == eInfo.Interval.min && this.Interval.max == eInfo.Interval.max;
			}

			// Token: 0x06008BF8 RID: 35832 RVA: 0x003AC7A4 File Offset: 0x003AABA4
			public bool TryGetNearPoint(out Vector3 getHitPoint, out EnvLineArea3DSE.LineT getHitLine)
			{
				getHitPoint = Vector3.zero;
				getHitLine = default(EnvLineArea3DSE.LineT);
				if (!Singleton<Manager.Resources>.IsInstance())
				{
					return false;
				}
				if (!Singleton<Map>.IsInstance())
				{
					return false;
				}
				if (this.Lines.IsNullOrEmpty<EnvLineArea3DSE.LineT>())
				{
					return false;
				}
				PlayerActor player = Singleton<Map>.Instance.Player;
				Transform transform;
				if (player == null)
				{
					transform = null;
				}
				else
				{
					ActorCameraControl cameraControl = player.CameraControl;
					if (cameraControl == null)
					{
						transform = null;
					}
					else
					{
						Camera cameraComponent = cameraControl.CameraComponent;
						transform = ((cameraComponent != null) ? cameraComponent.transform : null);
					}
				}
				Transform transform2 = transform;
				if (transform2 == null)
				{
					return false;
				}
				Vector3 position = transform2.position;
				int num = -1;
				float num2 = float.MaxValue;
				for (int i = 0; i < this.Lines.Count; i++)
				{
					EnvLineArea3DSE.LineT value = this.Lines[i];
					if (!(value.P1 == null) && !(value.P2 == null))
					{
						Vector3 position2 = value.P1.position;
						Vector3 position3 = value.P2.position;
						float maxDistance = Vector3.SqrMagnitude(position3 - position2);
						value.HitPoint = this.NearPointOnLine(position, position2, position3, maxDistance);
						value.Distance = Vector3.Distance(value.HitPoint, position);
						value.SqrDistance = value.Distance * value.Distance;
						if (value.SqrDistance < num2)
						{
							num2 = value.SqrDistance;
							num = i;
						}
						this.Lines[i] = value;
					}
				}
				if (0 > num)
				{
					return false;
				}
				float lineAreaSEBlendDistance = Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.LineAreaSEBlendDistance;
				getHitLine = this.Lines[num];
				if (this.Lines.Count == 1)
				{
					getHitPoint = getHitLine.HitPoint;
					return true;
				}
				if (num == 0)
				{
					EnvLineArea3DSE.LineT lineT = this.Lines[0];
					EnvLineArea3DSE.LineT lineT2 = this.Lines[1];
					float num3 = Mathf.Abs(lineT.Distance - lineT2.Distance);
					if (num3 <= lineAreaSEBlendDistance)
					{
						float t = Mathf.InverseLerp(0f, lineAreaSEBlendDistance, num3);
						getHitPoint = Vector3.Lerp((lineT.HitPoint + lineT2.HitPoint) / 2f, lineT.HitPoint, t);
						return true;
					}
				}
				else if (num == this.Lines.Count - 1)
				{
					int num4 = this.Lines.Count - 1;
					EnvLineArea3DSE.LineT lineT3 = this.Lines[num4];
					EnvLineArea3DSE.LineT lineT4 = this.Lines[num4 - 1];
					float num5 = Mathf.Abs(lineT3.Distance - lineT4.Distance);
					if (num5 <= lineAreaSEBlendDistance)
					{
						float t2 = Mathf.InverseLerp(0f, lineAreaSEBlendDistance, num5);
						getHitPoint = Vector3.Lerp((lineT3.HitPoint + lineT4.HitPoint) / 2f, lineT3.HitPoint, t2);
						return true;
					}
				}
				else
				{
					EnvLineArea3DSE.LineT lineT5 = this.Lines[num];
					EnvLineArea3DSE.LineT lineT6 = (this.Lines[num - 1].Distance > this.Lines[num + 1].Distance) ? this.Lines[num + 1] : this.Lines[num - 1];
					float num6 = Mathf.Abs(lineT5.Distance - lineT6.Distance);
					if (num6 <= lineAreaSEBlendDistance)
					{
						float t3 = Mathf.InverseLerp(0f, lineAreaSEBlendDistance, num6);
						getHitPoint = Vector3.Lerp((lineT5.HitPoint + lineT6.HitPoint) / 2f, lineT5.HitPoint, t3);
						return true;
					}
				}
				getHitPoint = getHitLine.HitPoint;
				return true;
			}

			// Token: 0x06008BF9 RID: 35833 RVA: 0x003ACB8C File Offset: 0x003AAF8C
			private Vector3 NearPointOnLine(Vector3 pc, Vector3 p1, Vector3 p2, float maxDistance)
			{
				Vector3 vector = p2 - p1;
				Vector3 rhs = pc - p1;
				Vector3 normalized = vector.normalized;
				float num = Vector3.Dot(normalized, rhs);
				if (num < 0f)
				{
					num = 0f;
				}
				else if (maxDistance < num * num)
				{
					num = Vector3.Distance(p2, p1);
				}
				return p1 + normalized * num;
			}

			// Token: 0x06008BFA RID: 35834 RVA: 0x003ACBF0 File Offset: 0x003AAFF0
			private bool CheckEnableEnvironment(Weather weather, TimeZone timeZone)
			{
				bool flag = true;
				switch (weather)
				{
				case Weather.Clear:
				case Weather.Cloud1:
				case Weather.Cloud2:
					flag &= this.IsClear;
					break;
				case Weather.Cloud3:
				case Weather.Cloud4:
					flag &= this.IsCloud;
					break;
				case Weather.Fog:
					flag &= this.IsFog;
					break;
				case Weather.Rain:
				case Weather.Storm:
					flag &= this.IsRain;
					break;
				}
				if (timeZone != TimeZone.Morning)
				{
					if (timeZone != TimeZone.Day)
					{
						if (timeZone == TimeZone.Night)
						{
							flag &= this.IsNight;
						}
					}
					else
					{
						flag &= this.IsNoon;
					}
				}
				else
				{
					flag &= this.IsMooning;
				}
				return flag;
			}

			// Token: 0x06008BFB RID: 35835 RVA: 0x003ACCA8 File Offset: 0x003AB0A8
			private void Play(Vector3 point)
			{
				this.IsPlay = true;
				EnvLineArea3DSE.PlayAudioSourceList.RemoveAll((EnvArea3DSE.IPlayInfo ax) => ax == null || ax.Audio == null || ax.Audio.gameObject == null);
				SoundPack.SoundSystemInfoGroup soundSystemInfo = Singleton<Manager.Resources>.Instance.SoundPack.SoundSystemInfo;
				if (soundSystemInfo.EnviroSEMaxCount <= EnvLineArea3DSE.PlayAudioSourceList.Count)
				{
					bool flag = true;
					int num = EnvLineArea3DSE.PlayAudioSourceList.Count - soundSystemInfo.EnviroSEMaxCount + 1;
					List<EnvArea3DSE.IPlayInfo> list = ListPool<EnvArea3DSE.IPlayInfo>.Get();
					list.AddRange(EnvLineArea3DSE.PlayAudioSourceList);
					Transform cameraT = Singleton<Map>.Instance.Player.CameraControl.CameraComponent.transform;
					list.Sort((EnvArea3DSE.IPlayInfo a1, EnvArea3DSE.IPlayInfo a2) => this.GetSqrDistanceSort(cameraT, a2.Audio.transform, a1.Audio.transform));
					float sqrDistanceFromCamera = this.GetSqrDistanceFromCamera(cameraT, point);
					for (int i = 0; i < num; i++)
					{
						EnvArea3DSE.IPlayInfo element = list.GetElement(i);
						if (sqrDistanceFromCamera < element.GetSqrDistanceFromCamera(cameraT, element.Audio.transform.position))
						{
							element.Stop();
							flag = false;
						}
					}
					ListPool<EnvArea3DSE.IPlayInfo>.Release(list);
					if (flag)
					{
						return;
					}
				}
				float fadeTime = 0f;
				if (this.FirstPlaying)
				{
					fadeTime = Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.FadeTime;
					this.FirstPlaying = false;
				}
				this.Audio = Singleton<Manager.Resources>.Instance.SoundPack.PlayEnviroSE(this.ClipID, fadeTime);
				if (this.Audio == null)
				{
					return;
				}
				this.FadePlayer = this.Audio.GetComponentInChildren<FadePlayer>(true);
				this.Audio.loop = this.IsLoop;
				this.Audio.minDistance = this.Decay.min;
				this.Audio.maxDistance = this.Decay.max;
				this.LoadSuccess = true;
				if (!EnvLineArea3DSE.PlayAudioSourceList.Contains(this))
				{
					EnvLineArea3DSE.PlayAudioSourceList.Add(this);
				}
			}

			// Token: 0x06008BFC RID: 35836 RVA: 0x003ACEB4 File Offset: 0x003AB2B4
			public void Update(Weather weather, TimeZone timeZone, Manager.Resources res, Map map)
			{
				Vector3 vector;
				EnvLineArea3DSE.LineT lineT;
				bool flag = this.TryGetNearPoint(out vector, out lineT);
				if (this.IsEnableDistance)
				{
					this.IsEnableDistance = (flag && !this.DisableDistance(res, map, vector));
				}
				else
				{
					this.IsEnableDistance = (flag && this.EnableDistance(res, map, vector));
				}
				bool playEnable = this.PlayEnable;
				this.PlayEnable = (this.CheckEnableEnvironment(weather, timeZone) && this.IsEnableDistance);
				if (this.IsPlay)
				{
					bool flag2 = this.Audio == null || (!this.IsLoop && !this.Audio.isPlaying);
					if (flag2 && this.IsLoop)
					{
						this.DelayTime = 1f;
					}
					if (flag2 || !this.PlayEnable)
					{
						this.Reset();
					}
				}
				else if (this.PlayEnable)
				{
					this.ElapsedTime += Time.deltaTime;
					if (this.DelayTime <= this.ElapsedTime)
					{
						this.ElapsedTime = 0f;
						this.ResetDelay();
						this.Play(vector);
					}
				}
				if (this.Audio != null && flag)
				{
					this.Audio.transform.position = vector;
				}
				if (!this.PlayEnable && playEnable)
				{
					this.ResetDelay();
					this.ElapsedTime = 0f;
					this.FirstPlaying = true;
				}
			}

			// Token: 0x06008BFD RID: 35837 RVA: 0x003AD048 File Offset: 0x003AB448
			public void ResetDelay()
			{
				this.DelayTime = ((!this.IsLoop) ? this.Interval.RandomValue : 0f);
			}

			// Token: 0x06008BFE RID: 35838 RVA: 0x003AD080 File Offset: 0x003AB480
			public void Stop()
			{
				if (EnvLineArea3DSE.PlayAudioSourceList.Contains(this))
				{
					EnvLineArea3DSE.PlayAudioSourceList.Remove(this);
				}
				if (this.FadePlayer != null)
				{
					this.FadePlayer.Stop(Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.FadeTime);
				}
				else if (this.Audio != null && Singleton<Sound>.IsInstance())
				{
					Singleton<Sound>.Instance.Stop(Sound.Type.ENV, this.Audio.transform);
				}
				this.FadePlayer = null;
				this.Audio = null;
				this.LoadSuccess = false;
			}

			// Token: 0x06008BFF RID: 35839 RVA: 0x003AD124 File Offset: 0x003AB524
			private void Reset()
			{
				this.IsPlay = false;
				this.ElapsedTime = 0f;
				this.Stop();
			}

			// Token: 0x06008C00 RID: 35840 RVA: 0x003AD13E File Offset: 0x003AB53E
			public void Release()
			{
				this.Reset();
				this.IsEnableDistance = false;
				this.PlayEnable = false;
			}

			// Token: 0x06008C01 RID: 35841 RVA: 0x003AD154 File Offset: 0x003AB554
			~PlayInfo()
			{
				this.Release();
			}

			// Token: 0x0400721B RID: 29211
			private FadePlayer FadePlayer;
		}

		// Token: 0x02001045 RID: 4165
		public struct LineT
		{
			// Token: 0x04007224 RID: 29220
			public Transform P1;

			// Token: 0x04007225 RID: 29221
			public Transform P2;

			// Token: 0x04007226 RID: 29222
			public Vector3 HitPoint;

			// Token: 0x04007227 RID: 29223
			public float Distance;

			// Token: 0x04007228 RID: 29224
			public float SqrDistance;
		}

		// Token: 0x02001046 RID: 4166
		[Serializable]
		public class EnvironmentSEInfo
		{
			// Token: 0x17001E8B RID: 7819
			// (get) Token: 0x06008C04 RID: 35844 RVA: 0x003AD233 File Offset: 0x003AB633
			// (set) Token: 0x06008C05 RID: 35845 RVA: 0x003AD23B File Offset: 0x003AB63B
			public string Summary
			{
				get
				{
					return this._summary;
				}
				set
				{
					this._summary = value;
				}
			}

			// Token: 0x17001E8C RID: 7820
			// (get) Token: 0x06008C06 RID: 35846 RVA: 0x003AD244 File Offset: 0x003AB644
			// (set) Token: 0x06008C07 RID: 35847 RVA: 0x003AD24C File Offset: 0x003AB64C
			public int ClipID
			{
				get
				{
					return this._clipID;
				}
				set
				{
					this._clipID = value;
				}
			}

			// Token: 0x17001E8D RID: 7821
			// (get) Token: 0x06008C08 RID: 35848 RVA: 0x003AD255 File Offset: 0x003AB655
			// (set) Token: 0x06008C09 RID: 35849 RVA: 0x003AD25D File Offset: 0x003AB65D
			public List<Transform> Roots
			{
				get
				{
					return this._roots;
				}
				set
				{
					this._roots = value;
				}
			}

			// Token: 0x17001E8E RID: 7822
			// (get) Token: 0x06008C0A RID: 35850 RVA: 0x003AD266 File Offset: 0x003AB666
			// (set) Token: 0x06008C0B RID: 35851 RVA: 0x003AD26E File Offset: 0x003AB66E
			public bool IsMooning
			{
				get
				{
					return this._isMooning;
				}
				set
				{
					this._isMooning = value;
				}
			}

			// Token: 0x17001E8F RID: 7823
			// (get) Token: 0x06008C0C RID: 35852 RVA: 0x003AD277 File Offset: 0x003AB677
			// (set) Token: 0x06008C0D RID: 35853 RVA: 0x003AD27F File Offset: 0x003AB67F
			public bool IsNoon
			{
				get
				{
					return this._isNoon;
				}
				set
				{
					this._isNoon = value;
				}
			}

			// Token: 0x17001E90 RID: 7824
			// (get) Token: 0x06008C0E RID: 35854 RVA: 0x003AD288 File Offset: 0x003AB688
			// (set) Token: 0x06008C0F RID: 35855 RVA: 0x003AD290 File Offset: 0x003AB690
			public bool IsNight
			{
				get
				{
					return this._isNight;
				}
				set
				{
					this._isNight = value;
				}
			}

			// Token: 0x17001E91 RID: 7825
			// (get) Token: 0x06008C10 RID: 35856 RVA: 0x003AD299 File Offset: 0x003AB699
			// (set) Token: 0x06008C11 RID: 35857 RVA: 0x003AD2A1 File Offset: 0x003AB6A1
			public bool IsClear
			{
				get
				{
					return this._isClear;
				}
				set
				{
					this._isClear = value;
				}
			}

			// Token: 0x17001E92 RID: 7826
			// (get) Token: 0x06008C12 RID: 35858 RVA: 0x003AD2AA File Offset: 0x003AB6AA
			// (set) Token: 0x06008C13 RID: 35859 RVA: 0x003AD2B2 File Offset: 0x003AB6B2
			public bool IsCloud
			{
				get
				{
					return this._isCloud;
				}
				set
				{
					this._isCloud = value;
				}
			}

			// Token: 0x17001E93 RID: 7827
			// (get) Token: 0x06008C14 RID: 35860 RVA: 0x003AD2BB File Offset: 0x003AB6BB
			// (set) Token: 0x06008C15 RID: 35861 RVA: 0x003AD2C3 File Offset: 0x003AB6C3
			public bool IsRain
			{
				get
				{
					return this._isRain;
				}
				set
				{
					this._isRain = value;
				}
			}

			// Token: 0x17001E94 RID: 7828
			// (get) Token: 0x06008C16 RID: 35862 RVA: 0x003AD2CC File Offset: 0x003AB6CC
			// (set) Token: 0x06008C17 RID: 35863 RVA: 0x003AD2D4 File Offset: 0x003AB6D4
			public bool IsFog
			{
				get
				{
					return this._isFog;
				}
				set
				{
					this._isFog = value;
				}
			}

			// Token: 0x17001E95 RID: 7829
			// (get) Token: 0x06008C18 RID: 35864 RVA: 0x003AD2DD File Offset: 0x003AB6DD
			// (set) Token: 0x06008C19 RID: 35865 RVA: 0x003AD2E5 File Offset: 0x003AB6E5
			public Threshold Decay
			{
				get
				{
					return this._decay;
				}
				set
				{
					this._decay = value;
				}
			}

			// Token: 0x17001E96 RID: 7830
			// (get) Token: 0x06008C1A RID: 35866 RVA: 0x003AD2EE File Offset: 0x003AB6EE
			// (set) Token: 0x06008C1B RID: 35867 RVA: 0x003AD2F6 File Offset: 0x003AB6F6
			public bool IsLoop
			{
				get
				{
					return this._isLoop;
				}
				set
				{
					this._isLoop = value;
				}
			}

			// Token: 0x17001E97 RID: 7831
			// (get) Token: 0x06008C1C RID: 35868 RVA: 0x003AD2FF File Offset: 0x003AB6FF
			// (set) Token: 0x06008C1D RID: 35869 RVA: 0x003AD307 File Offset: 0x003AB707
			public Threshold Interval
			{
				get
				{
					return this._interval;
				}
				set
				{
					this._interval = value;
				}
			}

			// Token: 0x04007229 RID: 29225
			[SerializeField]
			[LabelText("概要")]
			private string _summary = string.Empty;

			// Token: 0x0400722A RID: 29226
			[SerializeField]
			private int _clipID = -1;

			// Token: 0x0400722B RID: 29227
			[SerializeField]
			private List<Transform> _roots;

			// Token: 0x0400722C RID: 29228
			[SerializeField]
			[LabelText("朝")]
			[FoldoutGroup("時間帯", 0)]
			[ToggleLeft]
			private bool _isMooning;

			// Token: 0x0400722D RID: 29229
			[SerializeField]
			[LabelText("昼")]
			[FoldoutGroup("時間帯", 0)]
			[ToggleLeft]
			private bool _isNoon;

			// Token: 0x0400722E RID: 29230
			[SerializeField]
			[LabelText("夜")]
			[FoldoutGroup("時間帯", 0)]
			[ToggleLeft]
			private bool _isNight;

			// Token: 0x0400722F RID: 29231
			[SerializeField]
			[LabelText("晴")]
			[FoldoutGroup("天候", 0)]
			[ToggleLeft]
			private bool _isClear;

			// Token: 0x04007230 RID: 29232
			[SerializeField]
			[LabelText("曇")]
			[FoldoutGroup("天候", 0)]
			[ToggleLeft]
			private bool _isCloud;

			// Token: 0x04007231 RID: 29233
			[SerializeField]
			[LabelText("雨")]
			[FoldoutGroup("天候", 0)]
			[ToggleLeft]
			private bool _isRain;

			// Token: 0x04007232 RID: 29234
			[SerializeField]
			[LabelText("霧")]
			[FoldoutGroup("天候", 0)]
			[ToggleLeft]
			private bool _isFog;

			// Token: 0x04007233 RID: 29235
			[SerializeField]
			[LabelText("減衰値")]
			private Threshold _decay = new Threshold(1f, 500f);

			// Token: 0x04007234 RID: 29236
			[SerializeField]
			[LabelText("ループ専用")]
			[ToggleLeft]
			private bool _isLoop;

			// Token: 0x04007235 RID: 29237
			[SerializeField]
			[LabelText("再生間隔")]
			[HideIf("_isLoop", true)]
			private Threshold _interval = new Threshold(0f, 0f);
		}
	}
}
