using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ConfigScene;
using Illusion;
using Illusion.CustomAttributes;
using Illusion.Elements.Xml;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.Audio;

namespace Manager
{
	// Token: 0x02001105 RID: 4357
	public sealed class Voice : Singleton<Voice>
	{
		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x0600909F RID: 37023 RVA: 0x003C369B File Offset: 0x003C1A9B
		public static AudioMixer Mixer
		{
			get
			{
				return Sound.Mixer;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x060090A0 RID: 37024 RVA: 0x003C36A2 File Offset: 0x003C1AA2
		public Dictionary<int, VoiceInfo.Param> voiceInfoDic
		{
			get
			{
				return this.GetCache(ref this._voiceInfoDic, () => this.voiceInfoList.ToDictionary((VoiceInfo.Param v) => v.No, (VoiceInfo.Param v) => v));
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x060090A1 RID: 37025 RVA: 0x003C36BC File Offset: 0x003C1ABC
		// (set) Token: 0x060090A2 RID: 37026 RVA: 0x003C36C4 File Offset: 0x003C1AC4
		public List<VoiceInfo.Param> voiceInfoList { get; private set; }

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x060090A3 RID: 37027 RVA: 0x003C36CD File Offset: 0x003C1ACD
		// (set) Token: 0x060090A4 RID: 37028 RVA: 0x003C36D5 File Offset: 0x003C1AD5
		public VoiceSystem _Config { get; private set; }

		// Token: 0x060090A5 RID: 37029 RVA: 0x003C36E0 File Offset: 0x003C1AE0
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.rootSetting = new GameObject("SettingObjectPCM").transform;
			this.rootSetting.SetParent(base.transform, false);
			this.rootPlay = new GameObject("PlayObjectPCM").transform;
			this.rootPlay.SetParent(base.transform, false);
			this.settingObjects = new GameObject[Illusion.Utils.Enum<Voice.Type>.Length];
			for (int i = 0; i < this.settingObjects.Length; i++)
			{
				this.LoadSetting((Voice.Type)i, -1);
			}
			this.dicASCache = new Dictionary<int, Dictionary<string, AudioSource>>();
			this.ASCacheRoot = new GameObject("AudioSourceCache").transform;
			this.ASCacheRoot.SetParent(this.rootPlay, false);
			string text = AssetBundleManager.BaseDownloadingURL + "sound/data/pcm/";
			List<VoiceInfo.Param> sortList = new List<VoiceInfo.Param>();
			HashSet<int> distinctCheck = new HashSet<int>();
			CommonLib.GetAssetBundleNameListFromPath("etcetra/list/config/", true).ForEach(delegate(string file)
			{
				AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAllAsset(file, typeof(VoiceInfo), null);
				foreach (List<VoiceInfo.Param> list in from p in assetBundleLoadAssetOperation.GetAllAssets<VoiceInfo>()
				select p.param)
				{
					using (IEnumerator<VoiceInfo.Param> enumerator2 = list.Where((VoiceInfo.Param p) => !distinctCheck.Add(p.No)).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							VoiceInfo.Param p = enumerator2.Current;
							sortList.Remove(sortList.FirstOrDefault((VoiceInfo.Param l) => l.No == p.No));
						}
					}
					sortList.AddRange(list);
				}
				AssetBundleManager.UnloadAssetBundle(file, false, null, false);
			});
			sortList.Sort((VoiceInfo.Param a, VoiceInfo.Param b) => a.Sort.CompareTo(b.Sort));
			this.voiceInfoList = sortList;
			Dictionary<int, string> dic = new Dictionary<int, string>();
			this.voiceInfoList.ForEach(delegate(VoiceInfo.Param p)
			{
				string text2 = string.Format("c{0}", p.No.MinusThroughToString("00"));
				dic.Add(p.No, text2);
				Transform transform = new GameObject(text2).transform;
				transform.SetParent(this.rootPlay, false);
				this.voiceDic.Add(p.No, transform);
			});
			this._Config = new VoiceSystem("Volume", dic);
			this.xmlCtrl = new Control("config", "voice.xml", "Voice", new Data[]
			{
				this._Config
			});
			this.Load();
		}

		// Token: 0x060090A6 RID: 37030 RVA: 0x003C389F File Offset: 0x003C1C9F
		protected override void OnDestroy()
		{
			base.OnDestroy();
			UnityEngine.Object.Destroy(this.rootSetting.gameObject);
			UnityEngine.Object.Destroy(this.rootPlay.gameObject);
			this.voiceDic.Clear();
		}

		// Token: 0x060090A7 RID: 37031 RVA: 0x003C38D4 File Offset: 0x003C1CD4
		private void LoadSetting(Voice.Type type, int settingNo = -1)
		{
			string text = type.ToString();
			AssetBundleData assetBundleData = new AssetBundleData("sound/setting/object/00.unity3d", text.ToLower());
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(assetBundleData.GetAsset<GameObject>(), this.rootSetting, false);
			gameObject.name = text + "_Setting";
			AudioSource component = gameObject.GetComponent<AudioSource>();
			Singleton<Sound>.Instance.AudioSettingData(component, settingNo);
			this.settingObjects[(int)type] = gameObject;
			assetBundleData.UnloadBundle(true, false);
		}

		// Token: 0x060090A8 RID: 37032 RVA: 0x003C394C File Offset: 0x003C1D4C
		public void SetParent(int no, Transform t)
		{
			Transform parent;
			if (!this.voiceDic.TryGetValue(no, out parent))
			{
				return;
			}
			t.SetParent(parent, false);
		}

		// Token: 0x060090A9 RID: 37033 RVA: 0x003C3978 File Offset: 0x003C1D78
		public void Bind(LoadVoice script)
		{
			if (script.audioSource == null)
			{
				Transform parent;
				if (!this.voiceDic.TryGetValue(script.no, out parent))
				{
					return;
				}
				Singleton<Sound>.Instance.SetParent(parent, script, this.settingObjects[(int)script.type]);
			}
			AudioSource audioSource = script.audioSource;
			audioSource.clip = script.clip;
			Singleton<Sound>.Instance.Register(script.clip);
			audioSource.name = audioSource.clip.name;
			audioSource.volume = this.GetVolume(script.no);
			Sound.OutputSettingData outputSettingData = Singleton<Sound>.Instance.AudioSettingData(audioSource, script.settingNo);
			if (outputSettingData != null)
			{
				script.delayTime = outputSettingData.delayTime;
			}
		}

		// Token: 0x060090AA RID: 37034 RVA: 0x003C3A34 File Offset: 0x003C1E34
		public List<AudioSource> GetPlayingList(int no)
		{
			List<AudioSource> list = new List<AudioSource>();
			Transform transform;
			if (!this.voiceDic.TryGetValue(no, out transform))
			{
				return list;
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).GetComponent<AudioSource>());
			}
			return list;
		}

		// Token: 0x060090AB RID: 37035 RVA: 0x003C3A88 File Offset: 0x003C1E88
		public bool IsVoiceCheck(int no)
		{
			Transform transform;
			return this.voiceDic.TryGetValue(no, out transform) && transform.childCount != 0;
		}

		// Token: 0x060090AC RID: 37036 RVA: 0x003C3AB8 File Offset: 0x003C1EB8
		public bool IsVoiceCheck(Transform voiceTrans, bool isLoopCheck = true)
		{
			foreach (Transform transform in this.voiceDic.Values)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					LoadVoice componentInChildren = transform.GetChild(i).GetComponentInChildren<LoadVoice>();
					if (componentInChildren.voiceTrans == voiceTrans)
					{
						if (isLoopCheck || !componentInChildren.audioSource.loop)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060090AD RID: 37037 RVA: 0x003C3B70 File Offset: 0x003C1F70
		public bool IsVoiceCheck(int no, Transform voiceTrans, bool isLoopCheck = true)
		{
			Transform transform;
			if (!this.voiceDic.TryGetValue(no, out transform))
			{
				return false;
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				LoadVoice componentInChildren = transform.GetChild(i).GetComponentInChildren<LoadVoice>();
				if (componentInChildren.voiceTrans == voiceTrans)
				{
					if (isLoopCheck || !componentInChildren.audioSource.loop)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060090AE RID: 37038 RVA: 0x003C3BE5 File Offset: 0x003C1FE5
		public bool IsVoiceCheck()
		{
			return this.voiceDic.Values.Any((Transform v) => v.childCount != 0);
		}

		// Token: 0x060090AF RID: 37039 RVA: 0x003C3C14 File Offset: 0x003C2014
		public Transform Play(int no, string assetBundleName, string assetName, float pitch = 1f, float delayTime = 0f, float fadeTime = 0f, bool isAsync = true, Transform voiceTrans = null, Voice.Type type = Voice.Type.PCM, int settingNo = -1, bool isPlayEndDelete = true, bool isBundleUnload = true, bool is2D = false)
		{
			LoadVoice loadVoice = new GameObject("Voice Loading").AddComponent<LoadVoice>();
			loadVoice.no = no;
			loadVoice.assetBundleName = assetBundleName;
			loadVoice.assetName = assetName;
			loadVoice.pitch = pitch;
			loadVoice.delayTime = delayTime;
			loadVoice.fadeTime = fadeTime;
			loadVoice.isAsync = isAsync;
			loadVoice.voiceTrans = voiceTrans;
			loadVoice.type = type;
			loadVoice.settingNo = settingNo;
			loadVoice.isPlayEndDelete = isPlayEndDelete;
			loadVoice.isBundleUnload = isBundleUnload;
			loadVoice.is2D = is2D;
			Transform parent;
			if (!this.voiceDic.TryGetValue(no, out parent))
			{
				return null;
			}
			return Singleton<Sound>.Instance.SetParent(parent, loadVoice, this.settingObjects[(int)type]);
		}

		// Token: 0x060090B0 RID: 37040 RVA: 0x003C3CC0 File Offset: 0x003C20C0
		public Transform OnecePlay(int no, string assetBundleName, string assetName, float pitch = 1f, float delayTime = 0f, float fadeTime = 0f, bool isAsync = true, Transform voiceTrans = null, Voice.Type type = Voice.Type.PCM, int settingNo = -1, bool isPlayEndDelete = true, bool isBundleUnload = true, bool is2D = false)
		{
			this.StopAll(true);
			return this.Play(no, assetBundleName, assetName, pitch, delayTime, fadeTime, isAsync, voiceTrans, type, settingNo, isPlayEndDelete, isBundleUnload, is2D);
		}

		// Token: 0x060090B1 RID: 37041 RVA: 0x003C3CF4 File Offset: 0x003C20F4
		public Transform OnecePlayChara(int no, string assetBundleName, string assetName, float pitch = 1f, float delayTime = 0f, float fadeTime = 0f, bool isAsync = true, Transform voiceTrans = null, Voice.Type type = Voice.Type.PCM, int settingNo = -1, bool isPlayEndDelete = true, bool isBundleUnload = true, bool is2D = false)
		{
			if (voiceTrans != null)
			{
				this.Stop(no, voiceTrans);
			}
			else
			{
				this.Stop(no);
			}
			return this.Play(no, assetBundleName, assetName, pitch, delayTime, fadeTime, isAsync, voiceTrans, type, settingNo, isPlayEndDelete, isBundleUnload, is2D);
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x003C3D40 File Offset: 0x003C2140
		public void StopAll(bool isLoopStop = true)
		{
			List<GameObject> list = new List<GameObject>();
			foreach (Transform transform in this.voiceDic.Values)
			{
				int i = 0;
				while (i < transform.childCount)
				{
					Transform child = transform.GetChild(i);
					if (isLoopStop)
					{
						goto IL_61;
					}
					AudioSource componentInChildren = child.GetComponentInChildren<AudioSource>();
					if (!(componentInChildren != null) || !componentInChildren.loop)
					{
						goto IL_61;
					}
					IL_6E:
					i++;
					continue;
					IL_61:
					list.Add(child.gameObject);
					goto IL_6E;
				}
			}
			list.ForEach(delegate(GameObject p)
			{
				UnityEngine.Object.Destroy(p);
			});
		}

		// Token: 0x060090B3 RID: 37043 RVA: 0x003C3E20 File Offset: 0x003C2220
		public void Stop(int no)
		{
			Transform transform;
			if (!this.voiceDic.TryGetValue(no, out transform))
			{
				return;
			}
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).gameObject);
			}
			list.ForEach(delegate(GameObject p)
			{
				UnityEngine.Object.Destroy(p);
			});
		}

		// Token: 0x060090B4 RID: 37044 RVA: 0x003C3E94 File Offset: 0x003C2294
		public void Stop(Transform voiceTrans)
		{
			List<GameObject> list = new List<GameObject>();
			foreach (Transform transform in this.voiceDic.Values)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					LoadVoice componentInChildren = child.GetComponentInChildren<LoadVoice>();
					if (componentInChildren.voiceTrans == voiceTrans)
					{
						list.Add(child.gameObject);
					}
				}
			}
			list.ForEach(delegate(GameObject p)
			{
				UnityEngine.Object.Destroy(p);
			});
		}

		// Token: 0x060090B5 RID: 37045 RVA: 0x003C3F60 File Offset: 0x003C2360
		public void Stop(int no, Transform voiceTrans)
		{
			Transform transform;
			if (!this.voiceDic.TryGetValue(no, out transform))
			{
				return;
			}
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				LoadVoice componentInChildren = child.GetComponentInChildren<LoadVoice>();
				if (componentInChildren.voiceTrans == voiceTrans)
				{
					list.Add(child.gameObject);
				}
			}
			List<GameObject> list2 = list;
			if (Voice.<>f__mg$cache0 == null)
			{
				Voice.<>f__mg$cache0 = new Action<GameObject>(UnityEngine.Object.Destroy);
			}
			list2.ForEach(Voice.<>f__mg$cache0);
		}

		// Token: 0x060090B6 RID: 37046 RVA: 0x003C3FF0 File Offset: 0x003C23F0
		public float GetVolume(int charaNo)
		{
			VoiceSystem.Voice voice;
			if (!this._Config.chara.TryGetValue(charaNo, out voice))
			{
				return 0f;
			}
			return voice.sound.GetVolume();
		}

		// Token: 0x060090B7 RID: 37047 RVA: 0x003C4026 File Offset: 0x003C2426
		public AudioSource CreateCache(int voiceNo, AssetBundleData data)
		{
			return this.CreateCache(voiceNo, data.bundle, data.asset, null);
		}

		// Token: 0x060090B8 RID: 37048 RVA: 0x003C403C File Offset: 0x003C243C
		public AudioSource CreateCache(int voiceNo, AssetBundleManifestData data)
		{
			return this.CreateCache(voiceNo, data.bundle, data.asset, data.manifest);
		}

		// Token: 0x060090B9 RID: 37049 RVA: 0x003C4058 File Offset: 0x003C2458
		public AudioSource CreateCache(int voiceNo, string bundle, string asset, string manifest = null)
		{
			Dictionary<string, AudioSource> dictionary;
			if (!this.dicASCache.TryGetValue(voiceNo, out dictionary))
			{
				Dictionary<string, AudioSource> dictionary2 = new Dictionary<string, AudioSource>();
				this.dicASCache[voiceNo] = dictionary2;
				dictionary = dictionary2;
			}
			AudioSource component;
			if (!dictionary.TryGetValue(asset, out component))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.settingObjects[0], this.ASCacheRoot, false);
				gameObject.name = asset;
				gameObject.SetActive(true);
				component = gameObject.GetComponent<AudioSource>();
				component.clip = AssetBundleManager.LoadAsset(bundle, asset, typeof(AudioClip), manifest).GetAsset<AudioClip>();
				Singleton<Sound>.Instance.Register(component.clip);
				dictionary.Add(asset, component);
			}
			return component;
		}

		// Token: 0x060090BA RID: 37050 RVA: 0x003C40FC File Offset: 0x003C24FC
		public void ReleaseCache(int voiceNo, string bundle, string asset, string manifest = null)
		{
			Dictionary<string, AudioSource> dictionary;
			if (!this.dicASCache.TryGetValue(voiceNo, out dictionary))
			{
				return;
			}
			AudioSource audioSource;
			if (dictionary.TryGetValue(asset, out audioSource))
			{
				Singleton<Sound>.Instance.Remove(audioSource.clip);
				UnityEngine.Object.Destroy(audioSource.gameObject);
				dictionary.Remove(asset);
				AssetBundleManager.UnloadAssetBundle(bundle, false, manifest, false);
			}
			if (!dictionary.Any<KeyValuePair<string, AudioSource>>())
			{
				this.dicASCache.Remove(voiceNo);
			}
		}

		// Token: 0x060090BB RID: 37051 RVA: 0x003C4170 File Offset: 0x003C2570
		public void Reset()
		{
			if (this.xmlCtrl != null)
			{
				this.xmlCtrl.Init();
			}
		}

		// Token: 0x060090BC RID: 37052 RVA: 0x003C4188 File Offset: 0x003C2588
		public void Load()
		{
			this.xmlCtrl.Read();
		}

		// Token: 0x060090BD RID: 37053 RVA: 0x003C4195 File Offset: 0x003C2595
		public void Save()
		{
			this.xmlCtrl.Write();
		}

		// Token: 0x04007536 RID: 30006
		private Dictionary<int, VoiceInfo.Param> _voiceInfoDic;

		// Token: 0x04007538 RID: 30008
		[SerializeField]
		[NotEditable]
		private Transform rootSetting;

		// Token: 0x04007539 RID: 30009
		[SerializeField]
		[NotEditable]
		private Transform rootPlay;

		// Token: 0x0400753A RID: 30010
		[SerializeField]
		[NotEditable]
		private Transform ASCacheRoot;

		// Token: 0x0400753B RID: 30011
		[SerializeField]
		[NotEditable]
		private GameObject[] settingObjects;

		// Token: 0x0400753C RID: 30012
		private Dictionary<int, Transform> voiceDic = new Dictionary<int, Transform>();

		// Token: 0x0400753D RID: 30013
		private Dictionary<int, Dictionary<string, AudioSource>> dicASCache;

		// Token: 0x0400753F RID: 30015
		private const string UserPath = "config";

		// Token: 0x04007540 RID: 30016
		private const string FileName = "voice.xml";

		// Token: 0x04007541 RID: 30017
		private const string RootName = "Voice";

		// Token: 0x04007542 RID: 30018
		private const string ElementName = "Volume";

		// Token: 0x04007543 RID: 30019
		private Control xmlCtrl;

		// Token: 0x04007549 RID: 30025
		[CompilerGenerated]
		private static Action<GameObject> <>f__mg$cache0;

		// Token: 0x02001106 RID: 4358
		public enum Type
		{
			// Token: 0x0400754D RID: 30029
			PCM
		}
	}
}
