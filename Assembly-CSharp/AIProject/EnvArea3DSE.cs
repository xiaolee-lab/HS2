using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using Sound;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace AIProject
{
	// Token: 0x0200103F RID: 4159
	public class EnvArea3DSE : SerializedMonoBehaviour
	{
		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06008B45 RID: 35653 RVA: 0x003AAAC7 File Offset: 0x003A8EC7
		public IEnumerable<EnvArea3DSE.EnvironmentSEInfo> InfoList
		{
			[CompilerGenerated]
			get
			{
				return this._seInfoList;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06008B46 RID: 35654 RVA: 0x003AAACF File Offset: 0x003A8ECF
		public static List<EnvArea3DSE.IPlayInfo> PlayAudioSourceList
		{
			get
			{
				return EnvArea3DSE._playAudioSourceList;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06008B47 RID: 35655 RVA: 0x003AAAD6 File Offset: 0x003A8ED6
		public bool ShowAllGizmos
		{
			[CompilerGenerated]
			get
			{
				return this._showAllGizmos;
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06008B48 RID: 35656 RVA: 0x003AAADE File Offset: 0x003A8EDE
		// (set) Token: 0x06008B49 RID: 35657 RVA: 0x003AAAE6 File Offset: 0x003A8EE6
		public bool Playing { get; private set; }

		// Token: 0x06008B4A RID: 35658 RVA: 0x003AAAEF File Offset: 0x003A8EEF
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06008B4B RID: 35659 RVA: 0x003AAAF8 File Offset: 0x003A8EF8
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

		// Token: 0x06008B4C RID: 35660 RVA: 0x003AAB44 File Offset: 0x003A8F44
		private void OnEnable()
		{
			this.Initialize();
		}

		// Token: 0x06008B4D RID: 35661 RVA: 0x003AAB4C File Offset: 0x003A8F4C
		private void OnDisable()
		{
			this.End();
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x003AAB54 File Offset: 0x003A8F54
		private void OnDestroy()
		{
			this.End();
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x003AAB5C File Offset: 0x003A8F5C
		private void Initialize()
		{
			this.End();
			if (this._initFlag)
			{
				if (!this._playInfos.IsNullOrEmpty<EnvArea3DSE.PlayInfo>())
				{
					foreach (EnvArea3DSE.PlayInfo playInfo in this._playInfos)
					{
						playInfo.ResetDelay();
					}
				}
				this.Playing = true;
				return;
			}
			this._initFlag = true;
			if (!this._seInfoList.IsNullOrEmpty<EnvArea3DSE.EnvironmentSEInfo>())
			{
				this._playInfos = new EnvArea3DSE.PlayInfo[this._seInfoList.Length];
				for (int j = 0; j < this._seInfoList.Length; j++)
				{
					this._playInfos[j] = EnvArea3DSE.PlayInfo.Convert(this._seInfoList[j]);
					this._playInfos[j].ResetDelay();
				}
			}
			else
			{
				this._playInfos = new EnvArea3DSE.PlayInfo[0];
			}
			this.Playing = true;
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x003AAC34 File Offset: 0x003A9034
		private void End()
		{
			if (!this.Playing)
			{
				return;
			}
			if (!this._playInfos.IsNullOrEmpty<EnvArea3DSE.PlayInfo>())
			{
				foreach (EnvArea3DSE.PlayInfo playInfo in this._playInfos)
				{
					if (playInfo != null)
					{
						playInfo.Release();
					}
				}
			}
			this.Playing = false;
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x003AAC94 File Offset: 0x003A9094
		private void OnUpdate()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (this._playInfos.IsNullOrEmpty<EnvArea3DSE.PlayInfo>())
			{
				return;
			}
			Map instance = Singleton<Map>.Instance;
			Manager.Resources res = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
			EnvironmentSimulator simulator = instance.Simulator;
			foreach (EnvArea3DSE.PlayInfo playInfo in this._playInfos)
			{
				playInfo.Update(simulator.Weather, simulator.TimeZone, res, instance, base.transform);
			}
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x003AAD24 File Offset: 0x003A9124
		public void LoadFromExcelData(ExcelData data)
		{
			if (data == null)
			{
				return;
			}
			this._seInfoList = (from x in Enumerable.Range(1, data.MaxCell - 1).Select(delegate(int rowIdx)
			{
				List<string> list = data.list[rowIdx].list;
				if (list.IsNullOrEmpty<string>())
				{
					return null;
				}
				int num = 0;
				string summary = list.GetElement(num++) ?? string.Empty;
				string s = list.GetElement(num++) ?? string.Empty;
				int clipID;
				if (!int.TryParse(s, out clipID))
				{
					return null;
				}
				string s2 = list.GetElement(num++) ?? string.Empty;
				int num2;
				if (!int.TryParse(s2, out num2))
				{
					return null;
				}
				string s3 = list.GetElement(num++) ?? string.Empty;
				int num3;
				if (!int.TryParse(s3, out num3))
				{
					return null;
				}
				string s4 = list.GetElement(num++) ?? string.Empty;
				int num4;
				if (!int.TryParse(s4, out num4))
				{
					return null;
				}
				string s5 = list.GetElement(num++) ?? string.Empty;
				int num5;
				if (!int.TryParse(s5, out num5))
				{
					return null;
				}
				string s6 = list.GetElement(num++) ?? string.Empty;
				int num6;
				if (!int.TryParse(s6, out num6))
				{
					return null;
				}
				string s7 = list.GetElement(num++) ?? string.Empty;
				int num7;
				if (!int.TryParse(s7, out num7))
				{
					return null;
				}
				string s8 = list.GetElement(num++) ?? string.Empty;
				int num8;
				if (!int.TryParse(s8, out num8))
				{
					return null;
				}
				string s9 = list.GetElement(num++) ?? string.Empty;
				float minValue;
				if (!float.TryParse(s9, out minValue))
				{
					return null;
				}
				string s10 = list.GetElement(num++) ?? string.Empty;
				float maxValue;
				if (!float.TryParse(s10, out maxValue))
				{
					return null;
				}
				string s11 = list.GetElement(num++) ?? string.Empty;
				int num9;
				if (!int.TryParse(s11, out num9))
				{
					return null;
				}
				string s12 = list.GetElement(num++) ?? string.Empty;
				float minValue2;
				if (!float.TryParse(s12, out minValue2))
				{
					return null;
				}
				string s13 = list.GetElement(num++) ?? string.Empty;
				float maxValue2;
				if (!float.TryParse(s13, out maxValue2))
				{
					return null;
				}
				return new EnvArea3DSE.EnvironmentSEInfo
				{
					Summary = summary,
					ClipID = clipID,
					IsMooning = (num2 != 0),
					IsNoon = (num3 != 0),
					IsNight = (num4 != 0),
					IsClear = (num5 != 0),
					IsCloud = (num6 != 0),
					IsRain = (num7 != 0),
					IsFog = (num8 != 0),
					Decay = new Threshold(minValue, maxValue),
					IsLoop = (num9 != 0),
					Interval = new Threshold(minValue2, maxValue2)
				};
			})
			where x != null
			select x).ToArray<EnvArea3DSE.EnvironmentSEInfo>();
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x003AADA1 File Offset: 0x003A91A1
		public void DrawElementGizmos(Transform t)
		{
		}

		// Token: 0x040071DB RID: 29147
		[SerializeField]
		private string _summary = string.Empty;

		// Token: 0x040071DC RID: 29148
		[SerializeField]
		[FormerlySerializedAs("SEList")]
		private EnvArea3DSE.EnvironmentSEInfo[] _seInfoList = new EnvArea3DSE.EnvironmentSEInfo[0];

		// Token: 0x040071DD RID: 29149
		private static List<EnvArea3DSE.IPlayInfo> _playAudioSourceList = new List<EnvArea3DSE.IPlayInfo>();

		// Token: 0x040071DE RID: 29150
		[SerializeField]
		[HideInInspector]
		private bool _showAllGizmos;

		// Token: 0x040071E0 RID: 29152
		private EnvArea3DSE.PlayInfo[] _playInfos = new EnvArea3DSE.PlayInfo[0];

		// Token: 0x040071E1 RID: 29153
		private bool _initFlag;

		// Token: 0x02001040 RID: 4160
		public interface IPlayInfo
		{
			// Token: 0x17001E4E RID: 7758
			// (get) Token: 0x06008B59 RID: 35673
			AudioSource Audio { get; }

			// Token: 0x06008B5A RID: 35674
			float GetSqrDistanceFromCamera(Transform cameraT, Vector3 p1);

			// Token: 0x06008B5B RID: 35675
			void Stop();
		}

		// Token: 0x02001041 RID: 4161
		public class PlayInfo : EnvArea3DSE.IPlayInfo
		{
			// Token: 0x17001E4F RID: 7759
			// (get) Token: 0x06008B5D RID: 35677 RVA: 0x003AAE0F File Offset: 0x003A920F
			// (set) Token: 0x06008B5E RID: 35678 RVA: 0x003AAE17 File Offset: 0x003A9217
			public bool IsActive { get; set; }

			// Token: 0x17001E50 RID: 7760
			// (get) Token: 0x06008B5F RID: 35679 RVA: 0x003AAE20 File Offset: 0x003A9220
			// (set) Token: 0x06008B60 RID: 35680 RVA: 0x003AAE28 File Offset: 0x003A9228
			public bool FirstPlaying { get; private set; } = true;

			// Token: 0x17001E51 RID: 7761
			// (get) Token: 0x06008B61 RID: 35681 RVA: 0x003AAE31 File Offset: 0x003A9231
			// (set) Token: 0x06008B62 RID: 35682 RVA: 0x003AAE39 File Offset: 0x003A9239
			public Transform Root { get; set; }

			// Token: 0x17001E52 RID: 7762
			// (get) Token: 0x06008B63 RID: 35683 RVA: 0x003AAE42 File Offset: 0x003A9242
			// (set) Token: 0x06008B64 RID: 35684 RVA: 0x003AAE4A File Offset: 0x003A924A
			public int ClipID { get; set; } = -1;

			// Token: 0x17001E53 RID: 7763
			// (get) Token: 0x06008B65 RID: 35685 RVA: 0x003AAE53 File Offset: 0x003A9253
			// (set) Token: 0x06008B66 RID: 35686 RVA: 0x003AAE5B File Offset: 0x003A925B
			public bool IsMooning { get; set; }

			// Token: 0x17001E54 RID: 7764
			// (get) Token: 0x06008B67 RID: 35687 RVA: 0x003AAE64 File Offset: 0x003A9264
			// (set) Token: 0x06008B68 RID: 35688 RVA: 0x003AAE6C File Offset: 0x003A926C
			public bool IsNoon { get; set; }

			// Token: 0x17001E55 RID: 7765
			// (get) Token: 0x06008B69 RID: 35689 RVA: 0x003AAE75 File Offset: 0x003A9275
			// (set) Token: 0x06008B6A RID: 35690 RVA: 0x003AAE7D File Offset: 0x003A927D
			public bool IsNight { get; set; }

			// Token: 0x17001E56 RID: 7766
			// (get) Token: 0x06008B6B RID: 35691 RVA: 0x003AAE86 File Offset: 0x003A9286
			// (set) Token: 0x06008B6C RID: 35692 RVA: 0x003AAE8E File Offset: 0x003A928E
			public bool IsClear { get; set; }

			// Token: 0x17001E57 RID: 7767
			// (get) Token: 0x06008B6D RID: 35693 RVA: 0x003AAE97 File Offset: 0x003A9297
			// (set) Token: 0x06008B6E RID: 35694 RVA: 0x003AAE9F File Offset: 0x003A929F
			public bool IsCloud { get; set; }

			// Token: 0x17001E58 RID: 7768
			// (get) Token: 0x06008B6F RID: 35695 RVA: 0x003AAEA8 File Offset: 0x003A92A8
			// (set) Token: 0x06008B70 RID: 35696 RVA: 0x003AAEB0 File Offset: 0x003A92B0
			public bool IsRain { get; set; }

			// Token: 0x17001E59 RID: 7769
			// (get) Token: 0x06008B71 RID: 35697 RVA: 0x003AAEB9 File Offset: 0x003A92B9
			// (set) Token: 0x06008B72 RID: 35698 RVA: 0x003AAEC1 File Offset: 0x003A92C1
			public bool IsFog { get; set; }

			// Token: 0x17001E5A RID: 7770
			// (get) Token: 0x06008B73 RID: 35699 RVA: 0x003AAECA File Offset: 0x003A92CA
			// (set) Token: 0x06008B74 RID: 35700 RVA: 0x003AAED2 File Offset: 0x003A92D2
			public Threshold Decay { get; set; } = default(Threshold);

			// Token: 0x17001E5B RID: 7771
			// (get) Token: 0x06008B75 RID: 35701 RVA: 0x003AAEDB File Offset: 0x003A92DB
			// (set) Token: 0x06008B76 RID: 35702 RVA: 0x003AAEE3 File Offset: 0x003A92E3
			public bool IsLoop { get; set; }

			// Token: 0x17001E5C RID: 7772
			// (get) Token: 0x06008B77 RID: 35703 RVA: 0x003AAEEC File Offset: 0x003A92EC
			// (set) Token: 0x06008B78 RID: 35704 RVA: 0x003AAEF4 File Offset: 0x003A92F4
			public Threshold Interval { get; set; } = default(Threshold);

			// Token: 0x17001E5D RID: 7773
			// (get) Token: 0x06008B79 RID: 35705 RVA: 0x003AAEFD File Offset: 0x003A92FD
			// (set) Token: 0x06008B7A RID: 35706 RVA: 0x003AAF05 File Offset: 0x003A9305
			public AudioSource Audio { get; private set; }

			// Token: 0x17001E5E RID: 7774
			// (get) Token: 0x06008B7B RID: 35707 RVA: 0x003AAF0E File Offset: 0x003A930E
			// (set) Token: 0x06008B7C RID: 35708 RVA: 0x003AAF16 File Offset: 0x003A9316
			public float ElapsedTime { get; private set; }

			// Token: 0x17001E5F RID: 7775
			// (get) Token: 0x06008B7D RID: 35709 RVA: 0x003AAF1F File Offset: 0x003A931F
			// (set) Token: 0x06008B7E RID: 35710 RVA: 0x003AAF27 File Offset: 0x003A9327
			public float DelayTime { get; private set; }

			// Token: 0x17001E60 RID: 7776
			// (get) Token: 0x06008B7F RID: 35711 RVA: 0x003AAF30 File Offset: 0x003A9330
			// (set) Token: 0x06008B80 RID: 35712 RVA: 0x003AAF38 File Offset: 0x003A9338
			public bool IsPlay { get; private set; }

			// Token: 0x17001E61 RID: 7777
			// (get) Token: 0x06008B81 RID: 35713 RVA: 0x003AAF41 File Offset: 0x003A9341
			// (set) Token: 0x06008B82 RID: 35714 RVA: 0x003AAF49 File Offset: 0x003A9349
			public bool IsEnableDistance { get; private set; }

			// Token: 0x17001E62 RID: 7778
			// (get) Token: 0x06008B83 RID: 35715 RVA: 0x003AAF52 File Offset: 0x003A9352
			// (set) Token: 0x06008B84 RID: 35716 RVA: 0x003AAF5A File Offset: 0x003A935A
			public bool PlayEnable { get; private set; }

			// Token: 0x17001E63 RID: 7779
			// (get) Token: 0x06008B85 RID: 35717 RVA: 0x003AAF63 File Offset: 0x003A9363
			// (set) Token: 0x06008B86 RID: 35718 RVA: 0x003AAF6B File Offset: 0x003A936B
			public bool LoadSuccess { get; private set; }

			// Token: 0x06008B87 RID: 35719 RVA: 0x003AAF74 File Offset: 0x003A9374
			public float GetSqrDistanceFromCamera(Transform camera, Transform t1)
			{
				Vector3 vector = t1.position - camera.position;
				return Vector3.SqrMagnitude(vector);
			}

			// Token: 0x06008B88 RID: 35720 RVA: 0x003AAF9C File Offset: 0x003A939C
			public float GetSqrDistanceFromCamera(Transform cameraT, Vector3 p1)
			{
				Vector3 vector = p1 - cameraT.position;
				return Vector3.SqrMagnitude(vector);
			}

			// Token: 0x06008B89 RID: 35721 RVA: 0x003AAFBC File Offset: 0x003A93BC
			public int GetSqrDistanceSort(Transform camera, Transform t1, Transform t2)
			{
				Vector3 vector = t1.position - camera.position;
				Vector3 vector2 = t2.position - camera.position;
				return (int)(Vector3.SqrMagnitude(vector) - Vector3.SqrMagnitude(vector2));
			}

			// Token: 0x06008B8A RID: 35722 RVA: 0x003AAFFC File Offset: 0x003A93FC
			public bool EnableDistance()
			{
				if (!Singleton<Manager.Resources>.IsInstance())
				{
					return false;
				}
				if (!Singleton<Map>.IsInstance())
				{
					return false;
				}
				if (this.Root == null)
				{
					return false;
				}
				Transform transform = Singleton<Map>.Instance.Player.CameraControl.CameraComponent.transform;
				float num = Vector3.SqrMagnitude(this.Root.position - transform.position);
				float num2 = this.Decay.max + Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.EnableDistance;
				num2 *= num2;
				return num <= num2;
			}

			// Token: 0x06008B8B RID: 35723 RVA: 0x003AB09C File Offset: 0x003A949C
			public bool EnableDistance(Manager.Resources res, Map map, Transform root)
			{
				if (res == null)
				{
					return false;
				}
				Transform transform = map.Player.CameraControl.CameraComponent.transform;
				float num = Vector3.SqrMagnitude(root.position - transform.position);
				float num2 = this.Decay.max + res.SoundPack.EnviroInfo.EnableDistance;
				num2 *= num2;
				return num <= num2;
			}

			// Token: 0x06008B8C RID: 35724 RVA: 0x003AB110 File Offset: 0x003A9510
			public bool DisableDistance()
			{
				if (!Singleton<Manager.Resources>.IsInstance())
				{
					return true;
				}
				if (!Singleton<Map>.IsInstance())
				{
					return true;
				}
				if (this.Root == null)
				{
					return true;
				}
				Transform transform = Singleton<Map>.Instance.Player.CameraControl.CameraComponent.transform;
				float num = Vector3.SqrMagnitude(this.Root.position - transform.position);
				float num2 = this.Decay.max + Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.DisableDistance;
				num2 *= num2;
				return num2 < num;
			}

			// Token: 0x06008B8D RID: 35725 RVA: 0x003AB1AC File Offset: 0x003A95AC
			public bool DisableDistance(Manager.Resources res, Map map, Transform root)
			{
				if (res == null)
				{
					return true;
				}
				Transform transform = map.Player.CameraControl.CameraComponent.transform;
				float num = Vector3.SqrMagnitude(root.position - transform.position);
				float num2 = this.Decay.max + res.SoundPack.EnviroInfo.DisableDistance;
				num2 *= num2;
				return num2 < num;
			}

			// Token: 0x06008B8E RID: 35726 RVA: 0x003AB21C File Offset: 0x003A961C
			public static EnvArea3DSE.PlayInfo Convert(EnvArea3DSE.EnvironmentSEInfo envInfo)
			{
				return new EnvArea3DSE.PlayInfo
				{
					IsActive = false,
					FirstPlaying = true,
					Root = envInfo.Root,
					ClipID = envInfo.ClipID,
					IsMooning = envInfo.IsMooning,
					IsNoon = envInfo.IsNoon,
					IsNight = envInfo.IsNight,
					IsClear = envInfo.IsClear,
					IsCloud = envInfo.IsCloud,
					IsRain = envInfo.IsRain,
					IsFog = envInfo.IsFog,
					Decay = envInfo.Decay,
					IsLoop = envInfo.IsLoop,
					Interval = envInfo.Interval,
					Audio = null,
					FadePlayer = null,
					ElapsedTime = 0f,
					DelayTime = 0f,
					IsPlay = false,
					IsEnableDistance = false,
					PlayEnable = false,
					LoadSuccess = false
				};
			}

			// Token: 0x06008B8F RID: 35727 RVA: 0x003AB310 File Offset: 0x003A9710
			public bool Equal(EnvArea3DSE.EnvironmentSEInfo eInfo)
			{
				return this.Root == eInfo.Root && this.ClipID == eInfo.ClipID && this.IsMooning == eInfo.IsMooning && this.IsNoon == eInfo.IsNoon && this.IsNight == eInfo.IsNight && this.IsClear == eInfo.IsClear && this.IsCloud == eInfo.IsCloud && this.IsRain == eInfo.IsRain && this.IsFog == eInfo.IsFog && this.Decay.min == eInfo.Decay.min && this.Decay.max == eInfo.Decay.max && this.IsLoop == eInfo.IsLoop && this.Interval.min == eInfo.Interval.min && this.Interval.max == eInfo.Interval.max;
			}

			// Token: 0x06008B90 RID: 35728 RVA: 0x003AB454 File Offset: 0x003A9854
			public bool CheckPlayEnable(Weather weather, TimeZone timeZone)
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

			// Token: 0x06008B91 RID: 35729 RVA: 0x003AB50C File Offset: 0x003A990C
			private void Play(Transform root)
			{
				this.IsPlay = true;
				EnvArea3DSE._playAudioSourceList.RemoveAll((EnvArea3DSE.IPlayInfo ax) => ax == null || ax.Audio == null || ax.Audio.gameObject == null);
				SoundPack.SoundSystemInfoGroup soundSystemInfo = Singleton<Manager.Resources>.Instance.SoundPack.SoundSystemInfo;
				if (soundSystemInfo.EnviroSEMaxCount <= EnvArea3DSE._playAudioSourceList.Count)
				{
					bool flag = true;
					int num = EnvArea3DSE._playAudioSourceList.Count - soundSystemInfo.EnviroSEMaxCount + 1;
					List<EnvArea3DSE.IPlayInfo> list = ListPool<EnvArea3DSE.IPlayInfo>.Get();
					list.AddRange(EnvArea3DSE._playAudioSourceList);
					Transform cameraT = Singleton<Map>.Instance.Player.CameraControl.CameraComponent.transform;
					list.Sort((EnvArea3DSE.IPlayInfo a1, EnvArea3DSE.IPlayInfo a2) => this.GetSqrDistanceSort(cameraT, a2.Audio.transform, a1.Audio.transform));
					float sqrDistanceFromCamera = this.GetSqrDistanceFromCamera(cameraT, root);
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
				if (!EnvArea3DSE._playAudioSourceList.Contains(this))
				{
					EnvArea3DSE._playAudioSourceList.Add(this);
				}
			}

			// Token: 0x06008B92 RID: 35730 RVA: 0x003AB718 File Offset: 0x003A9B18
			public void Update(Weather weather, TimeZone timeZone, Manager.Resources res, Map map, Transform root)
			{
				Transform transform = (!(this.Root == null)) ? this.Root : root;
				if (this.IsEnableDistance)
				{
					this.IsEnableDistance = !this.DisableDistance(res, map, transform);
				}
				else
				{
					this.IsEnableDistance = this.EnableDistance(res, map, transform);
				}
				bool playEnable = this.PlayEnable;
				this.PlayEnable = (this.CheckPlayEnable(weather, timeZone) && this.IsEnableDistance);
				if (this.IsPlay)
				{
					bool flag = this.Audio == null || (!this.IsLoop && !this.Audio.isPlaying);
					if (flag && this.IsLoop)
					{
						this.DelayTime = 1f;
					}
					if (flag || !this.PlayEnable)
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
						this.Play(transform);
					}
				}
				if (this.Audio != null)
				{
					this.Audio.transform.position = transform.position;
					this.Audio.transform.rotation = transform.rotation;
				}
				if (!this.PlayEnable && playEnable)
				{
					this.ResetDelay();
					this.ElapsedTime = 0f;
					this.FirstPlaying = true;
				}
			}

			// Token: 0x06008B93 RID: 35731 RVA: 0x003AB8BC File Offset: 0x003A9CBC
			public void ResetDelay()
			{
				this.DelayTime = ((!this.IsLoop) ? this.Interval.RandomValue : 0f);
			}

			// Token: 0x06008B94 RID: 35732 RVA: 0x003AB8F4 File Offset: 0x003A9CF4
			public void Stop()
			{
				if (EnvArea3DSE._playAudioSourceList.Contains(this))
				{
					EnvArea3DSE._playAudioSourceList.Remove(this);
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

			// Token: 0x06008B95 RID: 35733 RVA: 0x003AB998 File Offset: 0x003A9D98
			private void Reset()
			{
				this.IsPlay = false;
				this.ElapsedTime = 0f;
				this.Stop();
			}

			// Token: 0x06008B96 RID: 35734 RVA: 0x003AB9B2 File Offset: 0x003A9DB2
			public void Release()
			{
				this.Reset();
				this.IsEnableDistance = false;
				this.PlayEnable = false;
			}

			// Token: 0x06008B97 RID: 35735 RVA: 0x003AB9C8 File Offset: 0x003A9DC8
			~PlayInfo()
			{
				this.Release();
			}

			// Token: 0x040071F2 RID: 29170
			private FadePlayer FadePlayer;
		}

		// Token: 0x02001042 RID: 4162
		[Serializable]
		public class EnvironmentSEInfo
		{
			// Token: 0x17001E64 RID: 7780
			// (get) Token: 0x06008B9A RID: 35738 RVA: 0x003ABAA7 File Offset: 0x003A9EA7
			// (set) Token: 0x06008B9B RID: 35739 RVA: 0x003ABAAF File Offset: 0x003A9EAF
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

			// Token: 0x17001E65 RID: 7781
			// (get) Token: 0x06008B9C RID: 35740 RVA: 0x003ABAB8 File Offset: 0x003A9EB8
			// (set) Token: 0x06008B9D RID: 35741 RVA: 0x003ABAC0 File Offset: 0x003A9EC0
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

			// Token: 0x17001E66 RID: 7782
			// (get) Token: 0x06008B9E RID: 35742 RVA: 0x003ABAC9 File Offset: 0x003A9EC9
			public Transform Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001E67 RID: 7783
			// (get) Token: 0x06008B9F RID: 35743 RVA: 0x003ABAD1 File Offset: 0x003A9ED1
			// (set) Token: 0x06008BA0 RID: 35744 RVA: 0x003ABAD9 File Offset: 0x003A9ED9
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

			// Token: 0x17001E68 RID: 7784
			// (get) Token: 0x06008BA1 RID: 35745 RVA: 0x003ABAE2 File Offset: 0x003A9EE2
			// (set) Token: 0x06008BA2 RID: 35746 RVA: 0x003ABAEA File Offset: 0x003A9EEA
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

			// Token: 0x17001E69 RID: 7785
			// (get) Token: 0x06008BA3 RID: 35747 RVA: 0x003ABAF3 File Offset: 0x003A9EF3
			// (set) Token: 0x06008BA4 RID: 35748 RVA: 0x003ABAFB File Offset: 0x003A9EFB
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

			// Token: 0x17001E6A RID: 7786
			// (get) Token: 0x06008BA5 RID: 35749 RVA: 0x003ABB04 File Offset: 0x003A9F04
			// (set) Token: 0x06008BA6 RID: 35750 RVA: 0x003ABB0C File Offset: 0x003A9F0C
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

			// Token: 0x17001E6B RID: 7787
			// (get) Token: 0x06008BA7 RID: 35751 RVA: 0x003ABB15 File Offset: 0x003A9F15
			// (set) Token: 0x06008BA8 RID: 35752 RVA: 0x003ABB1D File Offset: 0x003A9F1D
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

			// Token: 0x17001E6C RID: 7788
			// (get) Token: 0x06008BA9 RID: 35753 RVA: 0x003ABB26 File Offset: 0x003A9F26
			// (set) Token: 0x06008BAA RID: 35754 RVA: 0x003ABB2E File Offset: 0x003A9F2E
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

			// Token: 0x17001E6D RID: 7789
			// (get) Token: 0x06008BAB RID: 35755 RVA: 0x003ABB37 File Offset: 0x003A9F37
			// (set) Token: 0x06008BAC RID: 35756 RVA: 0x003ABB3F File Offset: 0x003A9F3F
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

			// Token: 0x17001E6E RID: 7790
			// (get) Token: 0x06008BAD RID: 35757 RVA: 0x003ABB48 File Offset: 0x003A9F48
			// (set) Token: 0x06008BAE RID: 35758 RVA: 0x003ABB50 File Offset: 0x003A9F50
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

			// Token: 0x17001E6F RID: 7791
			// (get) Token: 0x06008BAF RID: 35759 RVA: 0x003ABB59 File Offset: 0x003A9F59
			// (set) Token: 0x06008BB0 RID: 35760 RVA: 0x003ABB61 File Offset: 0x003A9F61
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

			// Token: 0x17001E70 RID: 7792
			// (get) Token: 0x06008BB1 RID: 35761 RVA: 0x003ABB6A File Offset: 0x003A9F6A
			// (set) Token: 0x06008BB2 RID: 35762 RVA: 0x003ABB72 File Offset: 0x003A9F72
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

			// Token: 0x040071FA RID: 29178
			[SerializeField]
			private string _summary = string.Empty;

			// Token: 0x040071FB RID: 29179
			[SerializeField]
			private int _clipID = -1;

			// Token: 0x040071FC RID: 29180
			[SerializeField]
			private Transform _root;

			// Token: 0x040071FD RID: 29181
			[SerializeField]
			private bool _isMooning;

			// Token: 0x040071FE RID: 29182
			[SerializeField]
			private bool _isNoon;

			// Token: 0x040071FF RID: 29183
			[SerializeField]
			private bool _isNight;

			// Token: 0x04007200 RID: 29184
			[SerializeField]
			private bool _isClear;

			// Token: 0x04007201 RID: 29185
			[SerializeField]
			private bool _isCloud;

			// Token: 0x04007202 RID: 29186
			[SerializeField]
			private bool _isRain;

			// Token: 0x04007203 RID: 29187
			[SerializeField]
			private bool _isFog;

			// Token: 0x04007204 RID: 29188
			[SerializeField]
			private Threshold _decay = new Threshold(1f, 500f);

			// Token: 0x04007205 RID: 29189
			[SerializeField]
			private bool _isLoop;

			// Token: 0x04007206 RID: 29190
			[SerializeField]
			private Threshold _interval = new Threshold(0f, 0f);
		}
	}
}
