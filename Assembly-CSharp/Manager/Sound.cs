using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion;
using Illusion.CustomAttributes;
using Illusion.Extensions;
using Sound;
using UnityEngine;
using UnityEngine.Audio;

namespace Manager
{
	// Token: 0x02001102 RID: 4354
	public sealed class Sound : Singleton<Sound>
	{
		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x0600906C RID: 36972 RVA: 0x003C289F File Offset: 0x003C0C9F
		// (set) Token: 0x0600906D RID: 36973 RVA: 0x003C28A6 File Offset: 0x003C0CA6
		public static AudioMixer Mixer { get; private set; }

		// Token: 0x0600906E RID: 36974 RVA: 0x003C28B0 File Offset: 0x003C0CB0
		public static GameObject PlayFade(GameObject fadeOut, AudioSource audio, float fadeTime = 0f)
		{
			if (fadeOut != null)
			{
				fadeOut.GetComponent<FadePlayer>().SafeProc(delegate(FadePlayer p)
				{
					p.Stop(fadeTime);
				});
			}
			GameObject gameObject = audio.gameObject;
			gameObject.AddComponent<FadePlayer>().Play(fadeTime);
			return gameObject;
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x0600906F RID: 36975 RVA: 0x003C2907 File Offset: 0x003C0D07
		public AudioListener AudioListener
		{
			get
			{
				return this.listener.GetComponent<AudioListener>();
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x06009070 RID: 36976 RVA: 0x003C2914 File Offset: 0x003C0D14
		// (set) Token: 0x06009071 RID: 36977 RVA: 0x003C291C File Offset: 0x003C0D1C
		public Transform Listener
		{
			get
			{
				return this._listener;
			}
			set
			{
				this._listener = value;
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x06009072 RID: 36978 RVA: 0x003C2925 File Offset: 0x003C0D25
		// (set) Token: 0x06009073 RID: 36979 RVA: 0x003C292D File Offset: 0x003C0D2D
		public GameObject currentBGM
		{
			get
			{
				return this._currentBGM;
			}
			set
			{
				if (this.oldBGM != null)
				{
					UnityEngine.Object.Destroy(this.oldBGM);
				}
				this.oldBGM = this._currentBGM;
				this._currentBGM = value;
			}
		}

		// Token: 0x06009074 RID: 36980 RVA: 0x003C295E File Offset: 0x003C0D5E
		public void Register(AudioClip clip)
		{
			this.useAudioClipList.Add(clip);
		}

		// Token: 0x06009075 RID: 36981 RVA: 0x003C296C File Offset: 0x003C0D6C
		public void Remove(AudioClip clip)
		{
			if (!this.useAudioClipList.Remove(clip) || this.useAudioClipList.Count((AudioClip p) => p == clip) == 0)
			{
				Resources.UnloadAsset(clip);
			}
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x06009076 RID: 36982 RVA: 0x003C29C3 File Offset: 0x003C0DC3
		// (set) Token: 0x06009077 RID: 36983 RVA: 0x003C29CB File Offset: 0x003C0DCB
		public List<SoundSettingData.Param> settingDataList { get; private set; }

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x06009078 RID: 36984 RVA: 0x003C29D4 File Offset: 0x003C0DD4
		// (set) Token: 0x06009079 RID: 36985 RVA: 0x003C29DC File Offset: 0x003C0DDC
		public List<Sound3DSettingData.Param> setting3DDataList { get; private set; }

		// Token: 0x0600907A RID: 36986 RVA: 0x003C29E8 File Offset: 0x003C0DE8
		public Sound.OutputSettingData AudioSettingData(AudioSource audio, int settingNo)
		{
			if (settingNo < 0)
			{
				return null;
			}
			SoundSettingData.Param audioSettingData = this.GetAudioSettingData(settingNo);
			if (audioSettingData == null)
			{
				return null;
			}
			audio.volume = audioSettingData.Volume;
			audio.pitch = audioSettingData.Pitch;
			audio.panStereo = audioSettingData.Pan;
			audio.spatialBlend = audioSettingData.Level3D;
			audio.priority = audioSettingData.Priority;
			audio.playOnAwake = audioSettingData.PlayAwake;
			audio.loop = audioSettingData.Loop;
			this.AudioSettingData3DOnly(audio, audioSettingData);
			return new Sound.OutputSettingData
			{
				delayTime = audioSettingData.DelayTime
			};
		}

		// Token: 0x0600907B RID: 36987 RVA: 0x003C2A7D File Offset: 0x003C0E7D
		public SoundSettingData.Param GetAudioSettingData(int settingNo)
		{
			return (settingNo >= 0) ? this.settingDataList[settingNo] : null;
		}

		// Token: 0x0600907C RID: 36988 RVA: 0x003C2A98 File Offset: 0x003C0E98
		public void AudioSettingData3DOnly(AudioSource audio, int settingNo)
		{
			this.AudioSettingData3DOnly(audio, this.GetAudioSettingData(settingNo));
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x003C2AA8 File Offset: 0x003C0EA8
		private void AudioSettingData3DOnly(AudioSource audio, SoundSettingData.Param param)
		{
			if (param == null || param.Setting3DNo < 0)
			{
				return;
			}
			Sound3DSettingData.Param param2 = this.setting3DDataList[param.Setting3DNo];
			if (param2 == null)
			{
				return;
			}
			audio.dopplerLevel = param2.DopplerLevel;
			audio.spread = param2.Spread;
			audio.minDistance = param2.MinDistance;
			audio.maxDistance = param2.MaxDistance;
			audio.rolloffMode = (AudioRolloffMode)param2.AudioRolloffMode;
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x003C2B20 File Offset: 0x003C0F20
		public AudioSource Create(Sound.Type type, bool isCache = false)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.settingObjects[(int)type], (!isCache) ? this.typeObjects[(int)type] : this.ASCacheRoot, false);
			gameObject.SetActive(true);
			return gameObject.GetComponent<AudioSource>();
		}

		// Token: 0x0600907F RID: 36991 RVA: 0x003C2B64 File Offset: 0x003C0F64
		public void SetParent(Sound.Type type, Transform t)
		{
			t.SetParent(this.typeObjects[(int)type], false);
		}

		// Token: 0x06009080 RID: 36992 RVA: 0x003C2B78 File Offset: 0x003C0F78
		public Transform SetParent(Transform parent, LoadAudioBase script, GameObject settingObject)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(settingObject, parent, false);
			gameObject.SetActive(true);
			script.Init(gameObject.GetComponent<AudioSource>());
			return gameObject.transform;
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x003C2BA8 File Offset: 0x003C0FA8
		public void Bind(LoadSound script)
		{
			if (script.audioSource == null)
			{
				int type = (int)script.type;
				this.SetParent(this.typeObjects[type], script, this.settingObjects[type]);
			}
			AudioSource audioSource = script.audioSource;
			audioSource.clip = script.clip;
			this.Register(script.clip);
			audioSource.name = script.clip.name;
			Sound.OutputSettingData outputSettingData = this.AudioSettingData(audioSource, script.settingNo);
			if (outputSettingData != null && script.delayTime <= 0f)
			{
				script.delayTime = outputSettingData.delayTime;
			}
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x003C2C48 File Offset: 0x003C1048
		public List<AudioSource> GetPlayingList(Sound.Type type)
		{
			List<AudioSource> list = new List<AudioSource>();
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).GetComponent<AudioSource>());
			}
			return list;
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x003C2C90 File Offset: 0x003C1090
		public bool IsPlay(Sound.Type type, string playName = null)
		{
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				if (playName.IsNullOrEmpty() || !(playName != transform.GetChild(i).name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x003C2CE8 File Offset: 0x003C10E8
		public bool IsPlay(Transform trans)
		{
			for (int i = 0; i < this.typeObjects.Length; i++)
			{
				if (this.IsPlay((Sound.Type)i, trans))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x003C2D20 File Offset: 0x003C1120
		public bool IsPlay(Sound.Type type, Transform trans)
		{
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child == trans)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009086 RID: 36998 RVA: 0x003C2D64 File Offset: 0x003C1164
		public Transform FindAsset(Sound.Type type, string assetName, string assetBundleName = null)
		{
			if (this.typeObjects == null)
			{
				return null;
			}
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				LoadAudioBase componentInChildren = child.GetComponentInChildren<LoadAudioBase>();
				if (componentInChildren != null && componentInChildren.clip != null && componentInChildren.assetName == assetName && (assetBundleName == null || componentInChildren.assetBundleName == assetBundleName))
				{
					if (type != Sound.Type.BGM)
					{
						return child;
					}
					if (child.gameObject != this.oldBGM)
					{
						return child;
					}
				}
			}
			return null;
		}

		// Token: 0x06009087 RID: 36999 RVA: 0x003C2E14 File Offset: 0x003C1214
		public Transform Play(Sound.Type type, string assetBundleName, string assetName, float delayTime = 0f, float fadeTime = 0f, bool isAssetEqualPlay = true, bool isAsync = true, int settingNo = -1, bool isBundleUnload = true)
		{
			LoadSound loadSound = new GameObject("Sound Loading").AddComponent<LoadSound>();
			loadSound.assetBundleName = assetBundleName;
			loadSound.assetName = assetName;
			loadSound.type = type;
			loadSound.delayTime = delayTime;
			loadSound.fadeTime = fadeTime;
			loadSound.isAssetEqualPlay = isAssetEqualPlay;
			loadSound.isAsync = isAsync;
			loadSound.settingNo = settingNo;
			loadSound.isBundleUnload = isBundleUnload;
			return this.SetParent(this.typeObjects[(int)type], loadSound, this.settingObjects[(int)type]);
		}

		// Token: 0x06009088 RID: 37000 RVA: 0x003C2E90 File Offset: 0x003C1290
		public void Stop(Sound.Type type)
		{
			List<GameObject> list = new List<GameObject>();
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).gameObject);
			}
			list.ForEach(delegate(GameObject p)
			{
				UnityEngine.Object.Destroy(p);
			});
		}

		// Token: 0x06009089 RID: 37001 RVA: 0x003C2EF8 File Offset: 0x003C12F8
		public void Stop(Sound.Type type, Transform trans)
		{
			Transform transform = this.typeObjects[(int)type];
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child == trans)
				{
					UnityEngine.Object.Destroy(child.gameObject);
					return;
				}
			}
		}

		// Token: 0x0600908A RID: 37002 RVA: 0x003C2F48 File Offset: 0x003C1348
		public void Stop(Transform trans)
		{
			for (int i = 0; i < this.typeObjects.Length; i++)
			{
				Transform transform = this.typeObjects[i];
				for (int j = 0; j < transform.childCount; j++)
				{
					Transform child = transform.GetChild(j);
					if (child == trans)
					{
						if (i == 0)
						{
							this.Stop(Sound.Type.BGM);
						}
						else
						{
							UnityEngine.Object.Destroy(child.gameObject);
						}
						return;
					}
				}
			}
		}

		// Token: 0x0600908B RID: 37003 RVA: 0x003C2FC0 File Offset: 0x003C13C0
		public void PlayBGM(float fadeTime = 0f)
		{
			if (this.currentBGM != null)
			{
				this.currentBGM.GetComponent<FadePlayer>().SafeProc(delegate(FadePlayer p)
				{
					p.Play(fadeTime);
				});
			}
		}

		// Token: 0x0600908C RID: 37004 RVA: 0x003C3008 File Offset: 0x003C1408
		public void PauseBGM()
		{
			if (this.currentBGM != null)
			{
				this.currentBGM.GetComponent<FadePlayer>().SafeProc(delegate(FadePlayer p)
				{
					p.Pause();
				});
			}
		}

		// Token: 0x0600908D RID: 37005 RVA: 0x003C3054 File Offset: 0x003C1454
		public void StopBGM(float fadeTime = 0f)
		{
			if (this.currentBGM != null)
			{
				this.currentBGM.GetComponent<FadePlayer>().SafeProc(delegate(FadePlayer p)
				{
					p.Stop(fadeTime);
				});
			}
			List<GameObject> list = (from item in this.typeObjects[0].gameObject.Children()
			where item != this.currentBGM
			select item).ToList<GameObject>();
			if (Sound.<>f__mg$cache0 == null)
			{
				Sound.<>f__mg$cache0 = new Action<GameObject>(UnityEngine.Object.Destroy);
			}
			list.ForEach(Sound.<>f__mg$cache0);
		}

		// Token: 0x0600908E RID: 37006 RVA: 0x003C30F0 File Offset: 0x003C14F0
		public AudioSource Play(Sound.Type type, AudioClip clip, float fadeTime = 0f)
		{
			AudioSource audioSource = this.Create(type, false);
			audioSource.clip = clip;
			audioSource.GetOrAddComponent<FadePlayer>().SafeProc(delegate(FadePlayer p)
			{
				p.Play(fadeTime);
			});
			return audioSource;
		}

		// Token: 0x0600908F RID: 37007 RVA: 0x003C3133 File Offset: 0x003C1533
		public AudioSource CreateCache(Sound.Type type, AssetBundleData data)
		{
			return this.CreateCache(type, data.bundle, data.asset, null);
		}

		// Token: 0x06009090 RID: 37008 RVA: 0x003C3149 File Offset: 0x003C1549
		public AudioSource CreateCache(Sound.Type type, AssetBundleManifestData data)
		{
			return this.CreateCache(type, data.bundle, data.asset, data.manifest);
		}

		// Token: 0x06009091 RID: 37009 RVA: 0x003C3164 File Offset: 0x003C1564
		public AudioSource CreateCache(Sound.Type type, string bundle, string asset, string manifest = null)
		{
			Dictionary<string, AudioSource> dictionary;
			if (!this.dicASCache.TryGetValue((int)type, out dictionary))
			{
				Dictionary<string, AudioSource> dictionary2 = new Dictionary<string, AudioSource>();
				this.dicASCache[(int)type] = dictionary2;
				dictionary = dictionary2;
			}
			AudioSource audioSource;
			if (!dictionary.TryGetValue(asset, out audioSource))
			{
				audioSource = this.Create(type, true);
				audioSource.name = asset;
				audioSource.clip = new AssetBundleManifestData(bundle, asset, manifest).GetAsset<AudioClip>();
				this.Register(audioSource.clip);
				dictionary.Add(asset, audioSource);
			}
			return audioSource;
		}

		// Token: 0x06009092 RID: 37010 RVA: 0x003C31E4 File Offset: 0x003C15E4
		public void ReleaseCache(Sound.Type type, string bundle, string asset, string manifest = null)
		{
			Dictionary<string, AudioSource> dictionary;
			if (!this.dicASCache.TryGetValue((int)type, out dictionary))
			{
				return;
			}
			AudioSource audioSource;
			if (dictionary.TryGetValue(asset, out audioSource))
			{
				this.Remove(audioSource.clip);
				UnityEngine.Object.Destroy(audioSource.gameObject);
				dictionary.Remove(asset);
				AssetBundleManager.UnloadAssetBundle(bundle, false, manifest, false);
			}
			if (!dictionary.Any<KeyValuePair<string, AudioSource>>())
			{
				this.dicASCache.Remove((int)type);
			}
		}

		// Token: 0x06009093 RID: 37011 RVA: 0x003C3258 File Offset: 0x003C1658
		private void LoadSettingData()
		{
			AssetBundleData assetBundleData = new AssetBundleData("sound/setting/soundsettingdata/00.unity3d", null);
			this.settingDataList = new List<SoundSettingData.Param>(assetBundleData.GetAllAssets<SoundSettingData>().SelectMany((SoundSettingData p) => p.param));
			this.settingDataList.Sort((SoundSettingData.Param a, SoundSettingData.Param b) => a.No.CompareTo(b.No));
			assetBundleData.UnloadBundle(true, false);
			AssetBundleData assetBundleData2 = new AssetBundleData("sound/setting/sound3dsettingdata/00.unity3d", null);
			this.setting3DDataList = new List<Sound3DSettingData.Param>(assetBundleData2.GetAllAssets<Sound3DSettingData>().SelectMany((Sound3DSettingData p) => p.param));
			this.setting3DDataList.Sort((Sound3DSettingData.Param a, Sound3DSettingData.Param b) => a.No.CompareTo(b.No));
			assetBundleData2.UnloadBundle(true, false);
		}

		// Token: 0x06009094 RID: 37012 RVA: 0x003C3344 File Offset: 0x003C1744
		private void LoadSetting(Sound.Type type, int settingNo = -1)
		{
			string text = type.ToString();
			AssetBundleData assetBundleData = new AssetBundleData("sound/setting/object/00.unity3d", text.ToLower());
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(assetBundleData.GetAsset<GameObject>(), this.rootSetting, false);
			gameObject.name = text + "_Setting";
			AudioSource component = gameObject.GetComponent<AudioSource>();
			this.AudioSettingData(component, settingNo);
			if (text.CompareParts("gamese", true))
			{
			}
			this.settingObjects[(int)type] = gameObject;
			assetBundleData.UnloadBundle(true, false);
		}

		// Token: 0x06009095 RID: 37013 RVA: 0x003C33CC File Offset: 0x003C17CC
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Sound.Mixer = new AssetBundleData("sound/data/mixer/00.unity3d", "master").GetAsset<AudioMixer>();
			this.rootSetting = new GameObject("SettingObject").transform;
			this.rootSetting.SetParent(base.transform, false);
			this.rootPlay = new GameObject("PlayObject").transform;
			this.rootPlay.SetParent(base.transform, false);
			this.LoadSettingData();
			this.settingObjects = new GameObject[Illusion.Utils.Enum<Sound.Type>.Length];
			this.typeObjects = new Transform[this.settingObjects.Length];
			for (int i = 0; i < this.settingObjects.Length; i++)
			{
				Sound.Type type = (Sound.Type)i;
				this.LoadSetting(type, -1);
				Transform transform = new GameObject(type.ToString()).transform;
				transform.SetParent(this.rootPlay, false);
				this.typeObjects[i] = transform;
			}
			this.dicASCache = new Dictionary<int, Dictionary<string, AudioSource>>();
			this.ASCacheRoot = new GameObject("AudioSourceCache").transform;
			this.ASCacheRoot.SetParent(this.rootPlay, false);
			this.listener = new GameObject("Listener", new System.Type[]
			{
				typeof(AudioListener)
			}).transform;
			this.listener.SetParent(base.transform, false);
			if (Camera.main != null)
			{
				this._listener = Camera.main.transform;
			}
		}

		// Token: 0x06009096 RID: 37014 RVA: 0x003C3560 File Offset: 0x003C1960
		private void Update()
		{
			if (this._listener != null)
			{
				this.listener.SetPositionAndRotation(this._listener.position, this._listener.rotation);
			}
			else
			{
				this.listener.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			}
		}

		// Token: 0x0400751B RID: 29979
		[SerializeField]
		[NotEditable]
		private Transform _listener;

		// Token: 0x0400751C RID: 29980
		[SerializeField]
		[NotEditable]
		private GameObject _currentBGM;

		// Token: 0x0400751D RID: 29981
		[SerializeField]
		[NotEditable]
		private GameObject oldBGM;

		// Token: 0x0400751E RID: 29982
		[SerializeField]
		[NotEditable]
		private Transform listener;

		// Token: 0x0400751F RID: 29983
		[SerializeField]
		[NotEditable]
		private Transform rootSetting;

		// Token: 0x04007520 RID: 29984
		[SerializeField]
		[NotEditable]
		private Transform rootPlay;

		// Token: 0x04007521 RID: 29985
		[SerializeField]
		[NotEditable]
		private Transform ASCacheRoot;

		// Token: 0x04007522 RID: 29986
		[SerializeField]
		[NotEditable]
		private Transform[] typeObjects;

		// Token: 0x04007523 RID: 29987
		[SerializeField]
		[NotEditable]
		private GameObject[] settingObjects;

		// Token: 0x04007524 RID: 29988
		private Dictionary<int, Dictionary<string, AudioSource>> dicASCache;

		// Token: 0x04007525 RID: 29989
		[SerializeField]
		[NotEditable]
		private List<AudioClip> useAudioClipList = new List<AudioClip>();

		// Token: 0x0400752A RID: 29994
		[CompilerGenerated]
		private static Action<GameObject> <>f__mg$cache0;

		// Token: 0x02001103 RID: 4355
		public enum Type
		{
			// Token: 0x04007530 RID: 30000
			BGM,
			// Token: 0x04007531 RID: 30001
			ENV,
			// Token: 0x04007532 RID: 30002
			SystemSE,
			// Token: 0x04007533 RID: 30003
			GameSE2D,
			// Token: 0x04007534 RID: 30004
			GameSE3D
		}

		// Token: 0x02001104 RID: 4356
		public class OutputSettingData
		{
			// Token: 0x04007535 RID: 30005
			public float delayTime;
		}
	}
}
