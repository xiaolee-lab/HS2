using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using Illusion.Game;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F7F RID: 3967
	public class SoundPack : SerializedScriptableObject
	{
		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x060083FE RID: 33790 RVA: 0x00372268 File Offset: 0x00370668
		public SoundPack.SoundSystemInfoGroup SoundSystemInfo
		{
			[CompilerGenerated]
			get
			{
				return this._soundSystemInfo;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x060083FF RID: 33791 RVA: 0x00372270 File Offset: 0x00370670
		public SoundPack.BGMInfoGroup BGMInfo
		{
			[CompilerGenerated]
			get
			{
				return this._bgmInfo;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06008400 RID: 33792 RVA: 0x00372278 File Offset: 0x00370678
		public SoundPack.Game3DSEInfoGroup Game3DInfo
		{
			[CompilerGenerated]
			get
			{
				return this._game3DSEInfo;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06008401 RID: 33793 RVA: 0x00372280 File Offset: 0x00370680
		public SoundPack.EnviroSEInfoGroup EnviroInfo
		{
			[CompilerGenerated]
			get
			{
				return this._enviroInfo;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06008402 RID: 33794 RVA: 0x00372288 File Offset: 0x00370688
		public SoundPack.FootStepInfoGroup FootStepInfo
		{
			[CompilerGenerated]
			get
			{
				return this._footStepInfo;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06008403 RID: 33795 RVA: 0x00372290 File Offset: 0x00370690
		public Dictionary<DoorMatType, SoundPack.DoorSEIDInfo> DoorIDTable
		{
			[CompilerGenerated]
			get
			{
				return this._doorIDTable;
			}
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x00372298 File Offset: 0x00370698
		public float GetFootStepSEVolumeScale(byte sex)
		{
			if (sex == 0)
			{
				return this._maleFootStepVolumeScale;
			}
			if (sex != 1)
			{
				return 1f;
			}
			return this._femaleFootStepVolumeScale;
		}

		// Token: 0x06008405 RID: 33797 RVA: 0x003722C0 File Offset: 0x003706C0
		public void Play(SoundPack.SystemSE se)
		{
			int key;
			if (!this._systemSETable.TryGetValue(se, out key))
			{
				return;
			}
			SoundPack.Data2D data2D;
			if (!this._systemSEDataTable.TryGetValue(key, out data2D))
			{
				return;
			}
			if (!data2D.IsActive)
			{
				return;
			}
			this.Play(data2D);
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x00372310 File Offset: 0x00370710
		public AudioSource Play(int key, Sound.Type soundType, float fadeTime = 0f)
		{
			List<SoundPack.Data3D> list;
			if (!this._actionSEDataTable.TryGetValue(key, out list))
			{
				return null;
			}
			if (list.IsNullOrEmpty<SoundPack.Data3D>())
			{
				return null;
			}
			SoundPack.Data3D data3D = list[UnityEngine.Random.Range(0, list.Count)];
			return this.Play(data3D, soundType, fadeTime);
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x00372360 File Offset: 0x00370760
		public AudioSource PlayFootStep(byte sex, bool bareFoot, AIProject.Definitions.Map.FootStepSE seType, Weather weather, SoundPack.PlayAreaType areaType)
		{
			if (sex != 0 && sex != 1)
			{
				return null;
			}
			List<int> list = null;
			if (areaType == SoundPack.PlayAreaType.Normal && list.IsNullOrEmpty<int>())
			{
				if (weather == Weather.Rain || weather == Weather.Storm)
				{
					Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> dictionary = (sex != 0) ? this._femaleRainFootStepSEMeshTable : this._maleRainFootStepSEMeshTable;
					if (!dictionary.TryGetValue(seType, out list))
					{
						list = null;
					}
					else if (list.IsNullOrEmpty<int>())
					{
						return null;
					}
				}
			}
			if (bareFoot && list.IsNullOrEmpty<int>())
			{
				Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> dictionary2 = (sex != 0) ? this._femaleBareFootStepSEMeshTable : this._maleBareFootStepSEMeshTable;
				if (!dictionary2.TryGetValue(seType, out list))
				{
					list = null;
				}
				else if (list.IsNullOrEmpty<int>())
				{
					return null;
				}
			}
			if (list.IsNullOrEmpty<int>())
			{
				Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> dictionary3 = (sex != 0) ? this._femaleFootStepSEMeshTable : this._maleFootStepSEMeshTable;
				if (!dictionary3.TryGetValue(seType, out list))
				{
					return null;
				}
				if (list.IsNullOrEmpty<int>())
				{
					return null;
				}
			}
			if (list.IsNullOrEmpty<int>())
			{
				return null;
			}
			int key = list[UnityEngine.Random.Range(0, list.Count)];
			Dictionary<int, SoundPack.Data2D> dictionary4 = (sex != 0) ? this._femaleFootStepSEData : this._maleFootStepSEData;
			SoundPack.Data2D data2D;
			if (!dictionary4.TryGetValue(key, out data2D))
			{
				return null;
			}
			return this.Play(data2D, Sound.Type.GameSE3D, 0f);
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x003724D0 File Offset: 0x003708D0
		public AudioSource PlayFootStep(byte sex, bool bareFoot, string tag, Weather weather, SoundPack.PlayAreaType areaType)
		{
			if (tag == "Untagged")
			{
				return null;
			}
			if (sex != 0 && sex != 1)
			{
				return null;
			}
			AIProject.Definitions.Map.FootStepSE seType;
			if (!this._footStepSETagTable.TryGetValue(tag ?? string.Empty, out seType))
			{
				return null;
			}
			return this.PlayFootStep(sex, bareFoot, seType, weather, areaType);
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x0037252C File Offset: 0x0037092C
		public AudioSource PlayFootStep(byte sex, bool bareFoot, int mapID, int areaID, Weather weather, SoundPack.PlayAreaType areaType)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return null;
			}
			if (sex != 0 && sex != 1)
			{
				return null;
			}
			int[] array = null;
			Dictionary<int, Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>>> defaultFootStepSETable = Singleton<Manager.Resources>.Instance.Sound.DefaultFootStepSETable;
			Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>> dictionary;
			Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>> dictionary2;
			UnityEx.ValueTuple<int[], int[], int[]> valueTuple;
			if (defaultFootStepSETable.TryGetValue((int)sex, out dictionary) && dictionary.TryGetValue(mapID, out dictionary2) && dictionary2.TryGetValue(areaID, out valueTuple))
			{
				if (areaType == SoundPack.PlayAreaType.Normal && array.IsNullOrEmpty<int>())
				{
					if (weather == Weather.Rain || weather == Weather.Storm)
					{
						array = valueTuple.Item3;
					}
				}
				if (bareFoot && array.IsNullOrEmpty<int>())
				{
					array = valueTuple.Item2;
				}
				if (array.IsNullOrEmpty<int>())
				{
					array = valueTuple.Item1;
				}
			}
			if (array.IsNullOrEmpty<int>())
			{
				return null;
			}
			int key = array[UnityEngine.Random.Range(0, array.Length)];
			Dictionary<int, SoundPack.Data2D> dictionary3 = (sex != 0) ? this._femaleFootStepSEData : this._maleFootStepSEData;
			SoundPack.Data2D data2D;
			if (!dictionary3.TryGetValue(key, out data2D))
			{
				return null;
			}
			return this.Play(data2D, Sound.Type.GameSE3D, 0f);
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x0037264C File Offset: 0x00370A4C
		public AudioClip LoadEnviroSEClip(int clipID, out int idx)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>())
			{
				idx = -1;
				return null;
			}
			idx = UnityEngine.Random.Range(0, list.Count);
			return this.Load(list[idx]);
		}

		// Token: 0x0600840B RID: 33803 RVA: 0x003726A0 File Offset: 0x00370AA0
		public AudioClip LoadEnviroSEClip(int clipID, int idx)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>() || idx < 0 || list.Count <= idx)
			{
				return null;
			}
			return this.Load(list[idx]);
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x0600840C RID: 33804 RVA: 0x003726F2 File Offset: 0x00370AF2
		// (set) Token: 0x0600840D RID: 33805 RVA: 0x003726F9 File Offset: 0x00370AF9
		public static int EnviroSECount { get; private set; } = 0;

		// Token: 0x0600840E RID: 33806 RVA: 0x00372704 File Offset: 0x00370B04
		private static void AddCountUsedEnviroSEInfo(string assetBundle, string asset, AudioClip clip)
		{
			Dictionary<string, UnityEx.ValueTuple<int, AudioClip>> dictionary;
			if (!SoundPack._usedEnviroSEInfo.TryGetValue(assetBundle, out dictionary) || dictionary == null)
			{
				dictionary = (SoundPack._usedEnviroSEInfo[assetBundle] = new Dictionary<string, UnityEx.ValueTuple<int, AudioClip>>());
			}
			UnityEx.ValueTuple<int, AudioClip> value;
			if (dictionary.TryGetValue(asset, out value) && value.Item2 != null)
			{
				value.Item1++;
				dictionary[asset] = value;
				SoundPack.EnviroSECount++;
			}
			else if (clip != null)
			{
				dictionary[asset] = new UnityEx.ValueTuple<int, AudioClip>(1, clip);
				SoundPack.EnviroSECount++;
			}
		}

		// Token: 0x0600840F RID: 33807 RVA: 0x003727AC File Offset: 0x00370BAC
		private static void RemoveUsedEnviroSEInfo(string assetBundle, string asset)
		{
			Dictionary<string, UnityEx.ValueTuple<int, AudioClip>> dictionary;
			if (!SoundPack._usedEnviroSEInfo.TryGetValue(assetBundle, out dictionary))
			{
				return;
			}
			UnityEx.ValueTuple<int, AudioClip> value;
			if (!dictionary.TryGetValue(asset, out value))
			{
				return;
			}
			if (value.Item2 == null)
			{
				dictionary.Remove(asset);
				return;
			}
			value.Item1--;
			SoundPack.EnviroSECount--;
			if (value.Item1 <= 0)
			{
				UnityEngine.Resources.UnloadAsset(value.Item2);
				dictionary.Remove(asset);
				return;
			}
			dictionary[asset] = value;
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x0037283C File Offset: 0x00370C3C
		public AudioSource PlayEnviroSE(int clipID, float fadeTime = 0f)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>())
			{
				return null;
			}
			int index = (list.Count != 0) ? UnityEngine.Random.Range(0, list.Count) : 0;
			SoundPack.Data2D data2D = list[index];
			AudioClip audioClip = this.Load(data2D);
			if (audioClip == null)
			{
				return null;
			}
			AudioSource audioSource = Illusion.Game.Utils.Sound.Play(Sound.Type.ENV, audioClip, fadeTime);
			if (audioSource == null)
			{
				return null;
			}
			string bundle = data2D.AssetBundleName;
			string asset = data2D.AssetName;
			SoundPack.AddCountUsedEnviroSEInfo(bundle, asset, audioClip);
			audioSource.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				SoundPack.RemoveUsedEnviroSEInfo(bundle, asset);
			});
			audioSource.dopplerLevel = 0f;
			audioSource.rolloffMode = this.EnviroInfo.RolloffMode;
			return audioSource;
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x00372934 File Offset: 0x00370D34
		public AudioSource PlayEnviroSE(int clipID, out int idx, float fadeTime = 0f)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>())
			{
				idx = -1;
				return null;
			}
			idx = ((list.Count != 1) ? UnityEngine.Random.Range(0, list.Count) : 0);
			SoundPack.Data2D data2D = list[idx];
			AudioClip audioClip = this.Load(data2D);
			if (audioClip == null)
			{
				idx = -1;
				return null;
			}
			AudioSource audioSource = Illusion.Game.Utils.Sound.Play(Sound.Type.ENV, audioClip, fadeTime);
			if (audioSource == null)
			{
				return null;
			}
			string bundle = data2D.AssetBundleName;
			string asset = data2D.AssetName;
			SoundPack.AddCountUsedEnviroSEInfo(bundle, asset, audioClip);
			audioSource.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				SoundPack.RemoveUsedEnviroSEInfo(bundle, asset);
			});
			return audioSource;
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x00372A14 File Offset: 0x00370E14
		public AudioSource PlayEnviroSE(int clipID, int idx, float fadeTime = 0f)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>() || idx < 0 || list.Count <= idx)
			{
				return null;
			}
			SoundPack.Data2D data2D = list[idx];
			AudioClip audioClip = this.Load(data2D);
			if (audioClip == null)
			{
				return null;
			}
			AudioSource audioSource = Illusion.Game.Utils.Sound.Play(Sound.Type.ENV, audioClip, fadeTime);
			if (audioSource == null)
			{
				return null;
			}
			string bundle = data2D.AssetBundleName;
			string asset = data2D.AssetName;
			SoundPack.AddCountUsedEnviroSEInfo(bundle, asset, audioClip);
			audioSource.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				SoundPack.RemoveUsedEnviroSEInfo(bundle, asset);
			});
			return audioSource;
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x00372AE0 File Offset: 0x00370EE0
		public int EnviroSEClipCount(int clipID)
		{
			List<SoundPack.Data2D> list;
			if (!this._env3DSEData.TryGetValue(clipID, out list) || list.IsNullOrEmpty<SoundPack.Data2D>())
			{
				return 0;
			}
			return list.Count;
		}

		// Token: 0x06008414 RID: 33812 RVA: 0x00372B14 File Offset: 0x00370F14
		private int[] GetWideEnviroID(SoundPack.WideRangeEnviroInfo envInfo, Weather weather)
		{
			switch (weather)
			{
			case Weather.Clear:
			case Weather.Cloud1:
			case Weather.Cloud2:
				return envInfo.ClearID;
			case Weather.Cloud3:
				return envInfo.LightCloudID;
			case Weather.Cloud4:
				return envInfo.HeavyCloudID;
			case Weather.Fog:
				return envInfo.FogID;
			case Weather.Rain:
				return envInfo.LightRainID;
			case Weather.Storm:
				return envInfo.HeavyRainID;
			default:
				return null;
			}
		}

		// Token: 0x06008415 RID: 33813 RVA: 0x00372B80 File Offset: 0x00370F80
		public bool TryGetWideEnvIDList(SoundPack.PlayAreaType areaType, Weather weather, ref List<int> idList)
		{
			if (idList == null)
			{
				return false;
			}
			SoundPack.WideRangeEnviroInfo envInfo;
			if (this._wideRangeEnvTable.TryGetValue(areaType, out envInfo))
			{
				int[] wideEnviroID = this.GetWideEnviroID(envInfo, weather);
				if (!wideEnviroID.IsNullOrEmpty<int>())
				{
					idList.AddRange(wideEnviroID);
					return true;
				}
			}
			SoundPack.WideRangeEnviroInfo envInfo2;
			if (this._wideRangeEnvTable.TryGetValue(SoundPack.PlayAreaType.Normal, out envInfo2))
			{
				int[] wideEnviroID2 = this.GetWideEnviroID(envInfo2, weather);
				if (!wideEnviroID2.IsNullOrEmpty<int>())
				{
					idList.AddRange(wideEnviroID2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008416 RID: 33814 RVA: 0x00372BFC File Offset: 0x00370FFC
		public bool WideEnvMuteArea(int mapID, int areaID)
		{
			int[] source;
			return !this._muteAreaTable.IsNullOrEmpty<int, int[]>() && (this._muteAreaTable.TryGetValue(mapID, out source) && !source.IsNullOrEmpty<int>()) && source.Contains(areaID);
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x00372C44 File Offset: 0x00371044
		public static void UnloadAudioClipAll()
		{
			foreach (KeyValuePair<string, Dictionary<string, AudioClip>> keyValuePair in SoundPack._audioClipTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<string, AudioClip>())
				{
					foreach (KeyValuePair<string, AudioClip> keyValuePair2 in keyValuePair.Value)
					{
						if (keyValuePair2.Value != null)
						{
							UnityEngine.Resources.UnloadAsset(keyValuePair2.Value);
						}
					}
				}
			}
			SoundPack._audioClipTable.Clear();
			SoundPack._usedEnviroSEInfo.Clear();
		}

		// Token: 0x06008418 RID: 33816 RVA: 0x00372D28 File Offset: 0x00371128
		private void Play(SoundPack.IData data)
		{
			AudioSource audioSource = Illusion.Game.Utils.Sound.Get(Sound.Type.SystemSE, data.AssetBundleName, data.AssetName, null);
			if (audioSource != null)
			{
				audioSource.Play();
			}
		}

		// Token: 0x06008419 RID: 33817 RVA: 0x00372D58 File Offset: 0x00371158
		private AudioClip Load(SoundPack.IData data)
		{
			if (data.AssetBundleName.IsNullOrEmpty() || data.AssetName.IsNullOrEmpty())
			{
				if (global::Debug.isDebugBuild)
				{
				}
				return null;
			}
			Dictionary<string, AudioClip> dictionary;
			if (!SoundPack._audioClipTable.TryGetValue(data.AssetBundleName, out dictionary))
			{
				Dictionary<string, AudioClip> dictionary2 = new Dictionary<string, AudioClip>();
				SoundPack._audioClipTable[data.AssetBundleName] = dictionary2;
				dictionary = dictionary2;
			}
			AudioClip audioClip;
			if (!dictionary.TryGetValue(data.AssetName, out audioClip) || audioClip == null)
			{
				AudioClip audioClip2 = CommonLib.LoadAsset<AudioClip>(data.AssetBundleName, data.AssetName, false, string.Empty);
				dictionary[data.AssetName] = audioClip2;
				audioClip = audioClip2;
			}
			return audioClip;
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x00372E08 File Offset: 0x00371208
		public bool TryGetActionSEData(int clipID, out SoundPack.Data3D data)
		{
			List<SoundPack.Data3D> list;
			if (this._actionSEDataTable.TryGetValue(clipID, out list) && !list.IsNullOrEmpty<SoundPack.Data3D>())
			{
				data = list[UnityEngine.Random.Range(0, list.Count)];
				return true;
			}
			data = default(SoundPack.Data3D);
			return false;
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x00372E60 File Offset: 0x00371260
		private int GetSqrDistanceSort(Transform camera, Transform t1, Transform t2)
		{
			Vector3 vector = t1.position - camera.position;
			Vector3 vector2 = t2.position - camera.position;
			return (int)(Vector3.SqrMagnitude(vector) - Vector3.SqrMagnitude(vector2));
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x00372EA0 File Offset: 0x003712A0
		public AudioSource Play(SoundPack.IData data, Sound.Type type, float fadeTime = 0f)
		{
			AudioClip audioClip = this.Load(data);
			if (audioClip == null)
			{
				return null;
			}
			AudioSource audio = Illusion.Game.Utils.Sound.Play(type, audioClip, fadeTime);
			if (type == Sound.Type.GameSE3D && audio != null)
			{
				this.PlayingAudioList.RemoveAll((AudioSource x) => x == null || x.gameObject == null);
				if (this._soundSystemInfo.Game3DMaxCount <= this.PlayingAudioList.Count)
				{
					int num = this.PlayingAudioList.Count - this._soundSystemInfo.Game3DMaxCount + 1;
					Transform cameraT = Singleton<Manager.Map>.Instance.Player.CameraControl.CameraComponent.transform;
					List<AudioSource> list = ListPool<AudioSource>.Get();
					list.AddRange(this.PlayingAudioList);
					list.Sort((AudioSource a1, AudioSource a2) => this.GetSqrDistanceSort(cameraT, a2.transform, a1.transform));
					for (int i = 0; i < num; i++)
					{
						AudioSource element = list.GetElement(i);
						this.PlayingAudioList.Remove(element);
						UnityEngine.Object.Destroy(element.gameObject);
					}
					ListPool<AudioSource>.Release(list);
				}
				audio.OnDestroyAsObservable().Subscribe(delegate(Unit _)
				{
					if (audio != null)
					{
						this.PlayingAudioList.Remove(audio);
					}
				});
				this.PlayingAudioList.Add(audio);
			}
			if (data is SoundPack.Data3D)
			{
				SoundPack.Data3D data3D = (SoundPack.Data3D)data;
				audio.minDistance = data3D.MinDistance;
				audio.maxDistance = data3D.MaxDistance;
			}
			return audio;
		}

		// Token: 0x04006A8E RID: 27278
		[SerializeField]
		[FoldoutGroup("定義", 0)]
		[LabelText("サウンド全般的定義")]
		private SoundPack.SoundSystemInfoGroup _soundSystemInfo;

		// Token: 0x04006A8F RID: 27279
		[SerializeField]
		[FoldoutGroup("定義", 0)]
		[LabelText("BGM関連定義")]
		private SoundPack.BGMInfoGroup _bgmInfo;

		// Token: 0x04006A90 RID: 27280
		[SerializeField]
		[FoldoutGroup("定義", 0)]
		[LabelText("3DSE関連定義")]
		private SoundPack.Game3DSEInfoGroup _game3DSEInfo;

		// Token: 0x04006A91 RID: 27281
		[SerializeField]
		[FoldoutGroup("定義", 0)]
		[LabelText("環境音関連定義")]
		private SoundPack.EnviroSEInfoGroup _enviroInfo;

		// Token: 0x04006A92 RID: 27282
		[SerializeField]
		[FoldoutGroup("定義", 0)]
		[LabelText("足音関連定義")]
		private SoundPack.FootStepInfoGroup _footStepInfo;

		// Token: 0x04006A93 RID: 27283
		[SerializeField]
		[FoldoutGroup("定義/再生IDテーブル", 0)]
		[DictionaryDrawerSettings(KeyLabel = "ドアの材質", ValueLabel = "SE ID")]
		private Dictionary<DoorMatType, SoundPack.DoorSEIDInfo> _doorIDTable = new Dictionary<DoorMatType, SoundPack.DoorSEIDInfo>();

		// Token: 0x04006A94 RID: 27284
		[NonSerialized]
		private List<AudioSource> PlayingAudioList = new List<AudioSource>();

		// Token: 0x04006A95 RID: 27285
		[SerializeField]
		[FoldoutGroup("アクションSE関連", 0)]
		[DictionaryDrawerSettings(KeyLabel = "ID", ValueLabel = "Info")]
		private Dictionary<int, List<SoundPack.Data3D>> _actionSEDataTable = new Dictionary<int, List<SoundPack.Data3D>>();

		// Token: 0x04006A96 RID: 27286
		[SerializeField]
		[FoldoutGroup("システムSE関係", 0)]
		[DictionaryDrawerSettings(KeyLabel = "ID", ValueLabel = "Info")]
		private Dictionary<int, SoundPack.Data2D> _systemSEDataTable = new Dictionary<int, SoundPack.Data2D>();

		// Token: 0x04006A97 RID: 27287
		[SerializeField]
		[FoldoutGroup("システムSE関係", 0)]
		private Dictionary<SoundPack.SystemSE, int> _systemSETable = Enum.GetValues(typeof(SoundPack.SystemSE)).Cast<SoundPack.SystemSE>().ToDictionary((SoundPack.SystemSE x) => x, (SoundPack.SystemSE x) => -1);

		// Token: 0x04006A98 RID: 27288
		[SerializeField]
		[Header("男女共通")]
		[FoldoutGroup("足音関係", 0)]
		[DictionaryDrawerSettings(KeyLabel = "Tag Name", ValueLabel = "SE Type")]
		private Dictionary<string, AIProject.Definitions.Map.FootStepSE> _footStepSETagTable = Enum.GetValues(typeof(AIProject.Definitions.Map.FootStepSE)).Cast<AIProject.Definitions.Map.FootStepSE>().ToDictionary((AIProject.Definitions.Map.FootStepSE x) => string.Format("FootStepSE_{0}", x.ToString()), (AIProject.Definitions.Map.FootStepSE x) => x);

		// Token: 0x04006A99 RID: 27289
		[SerializeField]
		[FoldoutGroup("足音関係/男性", 0)]
		[HideInInspector]
		[LabelText("音量スケール")]
		[Range(0f, 1f)]
		private float _maleFootStepVolumeScale = 1f;

		// Token: 0x04006A9A RID: 27290
		[SerializeField]
		[FoldoutGroup("足音関係/男性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE ID", ValueLabel = "SE Info")]
		private Dictionary<int, SoundPack.Data2D> _maleFootStepSEData = new Dictionary<int, SoundPack.Data2D>();

		// Token: 0x04006A9B RID: 27291
		[SerializeField]
		[FoldoutGroup("足音関係/男性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _maleFootStepSEMeshTable = Enum.GetValues(typeof(AIProject.Definitions.Map.FootStepSE)).Cast<AIProject.Definitions.Map.FootStepSE>().ToDictionary((AIProject.Definitions.Map.FootStepSE x) => x, (AIProject.Definitions.Map.FootStepSE x) => new List<int>());

		// Token: 0x04006A9C RID: 27292
		[SerializeField]
		[FoldoutGroup("足音関係/男性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _maleBareFootStepSEMeshTable = new Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>>();

		// Token: 0x04006A9D RID: 27293
		[SerializeField]
		[FoldoutGroup("足音関係/男性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _maleRainFootStepSEMeshTable = new Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>>();

		// Token: 0x04006A9E RID: 27294
		[SerializeField]
		[FoldoutGroup("足音関係/女性", 0)]
		[HideInInspector]
		[LabelText("音量スケール")]
		[Range(0f, 1f)]
		private float _femaleFootStepVolumeScale = 1f;

		// Token: 0x04006A9F RID: 27295
		[SerializeField]
		[FoldoutGroup("足音関係/女性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE ID", ValueLabel = "SE Info")]
		private Dictionary<int, SoundPack.Data2D> _femaleFootStepSEData = new Dictionary<int, SoundPack.Data2D>();

		// Token: 0x04006AA0 RID: 27296
		[SerializeField]
		[FoldoutGroup("足音関係/女性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _femaleFootStepSEMeshTable = Enum.GetValues(typeof(AIProject.Definitions.Map.FootStepSE)).Cast<AIProject.Definitions.Map.FootStepSE>().ToDictionary((AIProject.Definitions.Map.FootStepSE x) => x, (AIProject.Definitions.Map.FootStepSE x) => new List<int>());

		// Token: 0x04006AA1 RID: 27297
		[SerializeField]
		[FoldoutGroup("足音関係/女性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _femaleBareFootStepSEMeshTable = new Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>>();

		// Token: 0x04006AA2 RID: 27298
		[SerializeField]
		[FoldoutGroup("足音関係/女性", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE Type", ValueLabel = "SE ID List")]
		private Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>> _femaleRainFootStepSEMeshTable = new Dictionary<AIProject.Definitions.Map.FootStepSE, List<int>>();

		// Token: 0x04006AA3 RID: 27299
		[SerializeField]
		[FoldoutGroup("環境音関係", 0)]
		[DictionaryDrawerSettings(KeyLabel = "SE ID", ValueLabel = "Info")]
		private Dictionary<int, List<SoundPack.Data2D>> _env3DSEData = new Dictionary<int, List<SoundPack.Data2D>>();

		// Token: 0x04006AA4 RID: 27300
		[SerializeField]
		[FoldoutGroup("環境音関係/広範囲系", 0)]
		[DictionaryDrawerSettings(KeyLabel = "Area Type", ValueLabel = "ID Info")]
		private Dictionary<SoundPack.PlayAreaType, SoundPack.WideRangeEnviroInfo> _wideRangeEnvTable = new Dictionary<SoundPack.PlayAreaType, SoundPack.WideRangeEnviroInfo>();

		// Token: 0x04006AA5 RID: 27301
		[SerializeField]
		[FoldoutGroup("環境音関係/広範囲系", 0)]
		[DictionaryDrawerSettings(KeyLabel = "Map ID", ValueLabel = "Area ID")]
		private Dictionary<int, int[]> _muteAreaTable = new Dictionary<int, int[]>();

		// Token: 0x04006AA6 RID: 27302
		private static Dictionary<string, Dictionary<string, UnityEx.ValueTuple<int, AudioClip>>> _usedEnviroSEInfo = new Dictionary<string, Dictionary<string, UnityEx.ValueTuple<int, AudioClip>>>();

		// Token: 0x04006AA8 RID: 27304
		private static Dictionary<string, Dictionary<string, AudioClip>> _audioClipTable = new Dictionary<string, Dictionary<string, AudioClip>>();

		// Token: 0x02000F80 RID: 3968
		public interface IData
		{
			// Token: 0x17001C4E RID: 7246
			// (get) Token: 0x06008427 RID: 33831
			string Summary { get; }

			// Token: 0x17001C4F RID: 7247
			// (get) Token: 0x06008428 RID: 33832
			string AssetBundleName { get; }

			// Token: 0x17001C50 RID: 7248
			// (get) Token: 0x06008429 RID: 33833
			string AssetName { get; }

			// Token: 0x17001C51 RID: 7249
			// (get) Token: 0x0600842A RID: 33834
			bool IsActive { get; }
		}

		// Token: 0x02000F81 RID: 3969
		[Serializable]
		public struct Data2D : SoundPack.IData
		{
			// Token: 0x17001C52 RID: 7250
			// (get) Token: 0x0600842B RID: 33835 RVA: 0x003730C3 File Offset: 0x003714C3
			public string Summary
			{
				[CompilerGenerated]
				get
				{
					return this._summary;
				}
			}

			// Token: 0x17001C53 RID: 7251
			// (get) Token: 0x0600842C RID: 33836 RVA: 0x003730CB File Offset: 0x003714CB
			public string AssetBundleName
			{
				[CompilerGenerated]
				get
				{
					return this._assetBundleName;
				}
			}

			// Token: 0x17001C54 RID: 7252
			// (get) Token: 0x0600842D RID: 33837 RVA: 0x003730D3 File Offset: 0x003714D3
			public string AssetName
			{
				[CompilerGenerated]
				get
				{
					return this._assetName;
				}
			}

			// Token: 0x17001C55 RID: 7253
			// (get) Token: 0x0600842E RID: 33838 RVA: 0x003730DB File Offset: 0x003714DB
			public bool IsActive
			{
				[CompilerGenerated]
				get
				{
					return !this.AssetBundleName.IsNullOrEmpty() && !this.AssetName.IsNullOrEmpty();
				}
			}

			// Token: 0x04006AB2 RID: 27314
			[SerializeField]
			private string _summary;

			// Token: 0x04006AB3 RID: 27315
			[SerializeField]
			private string _assetBundleName;

			// Token: 0x04006AB4 RID: 27316
			[SerializeField]
			private string _assetName;
		}

		// Token: 0x02000F82 RID: 3970
		[Serializable]
		public struct Data3D : SoundPack.IData
		{
			// Token: 0x17001C56 RID: 7254
			// (get) Token: 0x0600842F RID: 33839 RVA: 0x003730FE File Offset: 0x003714FE
			public string Summary
			{
				[CompilerGenerated]
				get
				{
					return this._summary;
				}
			}

			// Token: 0x17001C57 RID: 7255
			// (get) Token: 0x06008430 RID: 33840 RVA: 0x00373106 File Offset: 0x00371506
			public string AssetBundleName
			{
				[CompilerGenerated]
				get
				{
					return this._assetBundleName;
				}
			}

			// Token: 0x17001C58 RID: 7256
			// (get) Token: 0x06008431 RID: 33841 RVA: 0x0037310E File Offset: 0x0037150E
			public string AssetName
			{
				[CompilerGenerated]
				get
				{
					return this._assetName;
				}
			}

			// Token: 0x17001C59 RID: 7257
			// (get) Token: 0x06008432 RID: 33842 RVA: 0x00373116 File Offset: 0x00371516
			public float MinDistance
			{
				[CompilerGenerated]
				get
				{
					return this._minDistance;
				}
			}

			// Token: 0x17001C5A RID: 7258
			// (get) Token: 0x06008433 RID: 33843 RVA: 0x0037311E File Offset: 0x0037151E
			public float MaxDistance
			{
				[CompilerGenerated]
				get
				{
					return this._maxDistance;
				}
			}

			// Token: 0x17001C5B RID: 7259
			// (get) Token: 0x06008434 RID: 33844 RVA: 0x00373126 File Offset: 0x00371526
			public bool IsActive
			{
				[CompilerGenerated]
				get
				{
					return !this.AssetBundleName.IsNullOrEmpty() && !this.AssetName.IsNullOrEmpty();
				}
			}

			// Token: 0x04006AB5 RID: 27317
			[SerializeField]
			private string _summary;

			// Token: 0x04006AB6 RID: 27318
			[SerializeField]
			private string _assetBundleName;

			// Token: 0x04006AB7 RID: 27319
			[SerializeField]
			private string _assetName;

			// Token: 0x04006AB8 RID: 27320
			[SerializeField]
			[LabelText("減衰開始距離")]
			private float _minDistance;

			// Token: 0x04006AB9 RID: 27321
			[SerializeField]
			[LabelText("減衰終了距離")]
			private float _maxDistance;
		}

		// Token: 0x02000F83 RID: 3971
		[Serializable]
		public class SoundSystemInfoGroup
		{
			// Token: 0x17001C5C RID: 7260
			// (get) Token: 0x06008436 RID: 33846 RVA: 0x00373161 File Offset: 0x00371561
			public int EnviroSEMaxCount
			{
				[CompilerGenerated]
				get
				{
					return this._enviroSEMaxCount;
				}
			}

			// Token: 0x17001C5D RID: 7261
			// (get) Token: 0x06008437 RID: 33847 RVA: 0x00373169 File Offset: 0x00371569
			public int Game3DMaxCount
			{
				[CompilerGenerated]
				get
				{
					return this._game3DSEMaxCount;
				}
			}

			// Token: 0x04006ABA RID: 27322
			[SerializeField]
			[FoldoutGroup("同時最大再生数", 0)]
			[LabelText("環境音同時最大再生数")]
			[MinValue(1.0)]
			private int _enviroSEMaxCount = 20;

			// Token: 0x04006ABB RID: 27323
			[SerializeField]
			[FoldoutGroup("同時最大再生数", 0)]
			[LabelText("３ＤＳＥ同時最大再生数")]
			[MinValue(1.0)]
			private int _game3DSEMaxCount = 20;
		}

		// Token: 0x02000F84 RID: 3972
		[Serializable]
		public class BGMInfoGroup
		{
			// Token: 0x17001C5E RID: 7262
			// (get) Token: 0x06008439 RID: 33849 RVA: 0x00373184 File Offset: 0x00371584
			public float MapBGMFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._mapBGMFadeTime;
				}
			}

			// Token: 0x04006ABC RID: 27324
			[SerializeField]
			[LabelText("マップ切り替わり時のフェードの時間(秒)")]
			private float _mapBGMFadeTime = 4f;
		}

		// Token: 0x02000F85 RID: 3973
		[Serializable]
		public class Game3DSEInfoGroup
		{
			// Token: 0x17001C5F RID: 7263
			// (get) Token: 0x0600843B RID: 33851 RVA: 0x003731AA File Offset: 0x003715AA
			public float MarginMaxDistance
			{
				[CompilerGenerated]
				get
				{
					return this._marginMaxDistance;
				}
			}

			// Token: 0x17001C60 RID: 7264
			// (get) Token: 0x0600843C RID: 33852 RVA: 0x003731B2 File Offset: 0x003715B2
			public float StopFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._stopFadeTime;
				}
			}

			// Token: 0x04006ABD RID: 27325
			[SerializeField]
			[LabelText("減衰終了距離＋α 再生距離")]
			[MinValue(0.0)]
			private float _marginMaxDistance = 10f;

			// Token: 0x04006ABE RID: 27326
			[SerializeField]
			[LabelText("停止時のフェード時間(秒)")]
			[MinValue(0.0)]
			private float _stopFadeTime = 0.5f;
		}

		// Token: 0x02000F86 RID: 3974
		[Serializable]
		public class EnviroSEInfoGroup
		{
			// Token: 0x17001C61 RID: 7265
			// (get) Token: 0x0600843E RID: 33854 RVA: 0x00373211 File Offset: 0x00371611
			public float FadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._fadeTime;
				}
			}

			// Token: 0x17001C62 RID: 7266
			// (get) Token: 0x0600843F RID: 33855 RVA: 0x00373219 File Offset: 0x00371619
			public float EnableDistance
			{
				[CompilerGenerated]
				get
				{
					return this._enableDistance;
				}
			}

			// Token: 0x17001C63 RID: 7267
			// (get) Token: 0x06008440 RID: 33856 RVA: 0x00373221 File Offset: 0x00371621
			public float DisableDistance
			{
				[CompilerGenerated]
				get
				{
					return this._disableDistance;
				}
			}

			// Token: 0x17001C64 RID: 7268
			// (get) Token: 0x06008441 RID: 33857 RVA: 0x00373229 File Offset: 0x00371629
			public AudioRolloffMode RolloffMode
			{
				[CompilerGenerated]
				get
				{
					return this._rolloffMode;
				}
			}

			// Token: 0x17001C65 RID: 7269
			// (get) Token: 0x06008442 RID: 33858 RVA: 0x00373231 File Offset: 0x00371631
			public float LineAreaSEBlendDistance
			{
				[CompilerGenerated]
				get
				{
					return this._lineAreaSEBlendDistance;
				}
			}

			// Token: 0x17001C66 RID: 7270
			// (get) Token: 0x06008443 RID: 33859 RVA: 0x00373239 File Offset: 0x00371639
			public float WideRangeQuickFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._wideRangeQuickFadeTime;
				}
			}

			// Token: 0x17001C67 RID: 7271
			// (get) Token: 0x06008444 RID: 33860 RVA: 0x00373241 File Offset: 0x00371641
			public float WideRangeSlowFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._wideRangeSlowFadeTime;
				}
			}

			// Token: 0x04006ABF RID: 27327
			[SerializeField]
			[LabelText("環境音切り替わり時のフェードの時間(秒)")]
			private float _fadeTime = 4f;

			// Token: 0x04006AC0 RID: 27328
			[SerializeField]
			[LabelText("減衰値Max＋αでアクティブになる")]
			private float _enableDistance = 1f;

			// Token: 0x04006AC1 RID: 27329
			[SerializeField]
			[LabelText("減衰値Max＋αで非アクティブになる")]
			private float _disableDistance = 10f;

			// Token: 0x04006AC2 RID: 27330
			[SerializeField]
			[LabelText("ロールオフモード")]
			private AudioRolloffMode _rolloffMode;

			// Token: 0x04006AC3 RID: 27331
			[SerializeField]
			private float _lineAreaSEBlendDistance = 2f;

			// Token: 0x04006AC4 RID: 27332
			[SerializeField]
			[FoldoutGroup("広範囲系", 0)]
			[LabelText("エリア切り替わり時のフェードの時間(秒)")]
			private float _wideRangeQuickFadeTime = 0.5f;

			// Token: 0x04006AC5 RID: 27333
			[SerializeField]
			[FoldoutGroup("広範囲系", 0)]
			[LabelText("天候切り替わり時のフェードの時間(秒)")]
			private float _wideRangeSlowFadeTime = 4f;
		}

		// Token: 0x02000F87 RID: 3975
		[Serializable]
		public class FootStepInfoGroup
		{
			// Token: 0x17001C68 RID: 7272
			// (get) Token: 0x06008446 RID: 33862 RVA: 0x00373272 File Offset: 0x00371672
			public float MinDistance
			{
				[CompilerGenerated]
				get
				{
					return this._minDistance;
				}
			}

			// Token: 0x17001C69 RID: 7273
			// (get) Token: 0x06008447 RID: 33863 RVA: 0x0037327A File Offset: 0x0037167A
			public float MaxDistance
			{
				[CompilerGenerated]
				get
				{
					return this._maxDistance;
				}
			}

			// Token: 0x17001C6A RID: 7274
			// (get) Token: 0x06008448 RID: 33864 RVA: 0x00373282 File Offset: 0x00371682
			public float MarginMaxDistance
			{
				[CompilerGenerated]
				get
				{
					return this._marginMaxDistance;
				}
			}

			// Token: 0x17001C6B RID: 7275
			// (get) Token: 0x06008449 RID: 33865 RVA: 0x0037328A File Offset: 0x0037168A
			public AudioRolloffMode RolloffMode
			{
				[CompilerGenerated]
				get
				{
					return this._rolloffMode;
				}
			}

			// Token: 0x17001C6C RID: 7276
			// (get) Token: 0x0600844A RID: 33866 RVA: 0x00373292 File Offset: 0x00371692
			public float PlayEnableDistance
			{
				[CompilerGenerated]
				get
				{
					return this._maxDistance + this._marginMaxDistance;
				}
			}

			// Token: 0x04006AC6 RID: 27334
			[SerializeField]
			[LabelText("減衰開始距離")]
			[Range(0.1f, 1000f)]
			private float _minDistance = 1f;

			// Token: 0x04006AC7 RID: 27335
			[SerializeField]
			[LabelText("減衰終了距離")]
			[Range(0.1f, 1000f)]
			private float _maxDistance = 15f;

			// Token: 0x04006AC8 RID: 27336
			[SerializeField]
			[LabelText("減衰終了距離＋α 再生距離")]
			[MinValue(0.0)]
			private float _marginMaxDistance = 10f;

			// Token: 0x04006AC9 RID: 27337
			[SerializeField]
			[LabelText("ロールオフモード")]
			private AudioRolloffMode _rolloffMode;
		}

		// Token: 0x02000F88 RID: 3976
		[Serializable]
		public struct DoorSEIDInfo
		{
			// Token: 0x17001C6D RID: 7277
			// (get) Token: 0x0600844B RID: 33867 RVA: 0x003732A1 File Offset: 0x003716A1
			public int OpenID
			{
				[CompilerGenerated]
				get
				{
					return this._openID;
				}
			}

			// Token: 0x17001C6E RID: 7278
			// (get) Token: 0x0600844C RID: 33868 RVA: 0x003732A9 File Offset: 0x003716A9
			public int CloseID
			{
				[CompilerGenerated]
				get
				{
					return this._closeID;
				}
			}

			// Token: 0x04006ACA RID: 27338
			[SerializeField]
			private int _openID;

			// Token: 0x04006ACB RID: 27339
			[SerializeField]
			private int _closeID;
		}

		// Token: 0x02000F89 RID: 3977
		public enum PlayAreaType
		{
			// Token: 0x04006ACD RID: 27341
			Normal,
			// Token: 0x04006ACE RID: 27342
			Indoor
		}

		// Token: 0x02000F8A RID: 3978
		public enum EnvSEWeatherType
		{
			// Token: 0x04006AD0 RID: 27344
			Clear,
			// Token: 0x04006AD1 RID: 27345
			LightCloud,
			// Token: 0x04006AD2 RID: 27346
			HeavyCloud,
			// Token: 0x04006AD3 RID: 27347
			LightRain,
			// Token: 0x04006AD4 RID: 27348
			HeavyRain,
			// Token: 0x04006AD5 RID: 27349
			Fog
		}

		// Token: 0x02000F8B RID: 3979
		public enum SystemSE
		{
			// Token: 0x04006AD7 RID: 27351
			Select,
			// Token: 0x04006AD8 RID: 27352
			OK_L,
			// Token: 0x04006AD9 RID: 27353
			OK_S,
			// Token: 0x04006ADA RID: 27354
			Cancel,
			// Token: 0x04006ADB RID: 27355
			Error,
			// Token: 0x04006ADC RID: 27356
			Save,
			// Token: 0x04006ADD RID: 27357
			Load,
			// Token: 0x04006ADE RID: 27358
			Shop,
			// Token: 0x04006ADF RID: 27359
			Popup,
			// Token: 0x04006AE0 RID: 27360
			Page,
			// Token: 0x04006AE1 RID: 27361
			Craft,
			// Token: 0x04006AE2 RID: 27362
			Skill,
			// Token: 0x04006AE3 RID: 27363
			BootDevice,
			// Token: 0x04006AE4 RID: 27364
			Fishing_Result,
			// Token: 0x04006AE5 RID: 27365
			Photo,
			// Token: 0x04006AE6 RID: 27366
			Call,
			// Token: 0x04006AE7 RID: 27367
			LevelUP,
			// Token: 0x04006AE8 RID: 27368
			BoxOpen,
			// Token: 0x04006AE9 RID: 27369
			BoxClose,
			// Token: 0x04006AEA RID: 27370
			Warp_In,
			// Token: 0x04006AEB RID: 27371
			Warp_Out
		}

		// Token: 0x02000F8C RID: 3980
		[Serializable]
		public struct WideRangeEnviroInfo
		{
			// Token: 0x17001C6F RID: 7279
			// (get) Token: 0x0600844D RID: 33869 RVA: 0x003732B1 File Offset: 0x003716B1
			public int[] ClearID
			{
				[CompilerGenerated]
				get
				{
					return this._clearID;
				}
			}

			// Token: 0x17001C70 RID: 7280
			// (get) Token: 0x0600844E RID: 33870 RVA: 0x003732B9 File Offset: 0x003716B9
			public int[] LightCloudID
			{
				[CompilerGenerated]
				get
				{
					return this._lightCloudID;
				}
			}

			// Token: 0x17001C71 RID: 7281
			// (get) Token: 0x0600844F RID: 33871 RVA: 0x003732C1 File Offset: 0x003716C1
			public int[] HeavyCloudID
			{
				[CompilerGenerated]
				get
				{
					return this._heavyCloudID;
				}
			}

			// Token: 0x17001C72 RID: 7282
			// (get) Token: 0x06008450 RID: 33872 RVA: 0x003732C9 File Offset: 0x003716C9
			public int[] LightRainID
			{
				[CompilerGenerated]
				get
				{
					return this._lightRainID;
				}
			}

			// Token: 0x17001C73 RID: 7283
			// (get) Token: 0x06008451 RID: 33873 RVA: 0x003732D1 File Offset: 0x003716D1
			public int[] HeavyRainID
			{
				[CompilerGenerated]
				get
				{
					return this._heavyRainID;
				}
			}

			// Token: 0x17001C74 RID: 7284
			// (get) Token: 0x06008452 RID: 33874 RVA: 0x003732D9 File Offset: 0x003716D9
			public int[] FogID
			{
				[CompilerGenerated]
				get
				{
					return this._fogID;
				}
			}

			// Token: 0x04006AEC RID: 27372
			[SerializeField]
			[FormerlySerializedAs("ClearID")]
			[LabelText("晴れ")]
			private int[] _clearID;

			// Token: 0x04006AED RID: 27373
			[SerializeField]
			[FormerlySerializedAs("LightCloudID")]
			[LabelText("曇り")]
			private int[] _lightCloudID;

			// Token: 0x04006AEE RID: 27374
			[SerializeField]
			[FormerlySerializedAs("HeavyCloudID")]
			[LabelText("曇天")]
			private int[] _heavyCloudID;

			// Token: 0x04006AEF RID: 27375
			[SerializeField]
			[FormerlySerializedAs("LightRainID")]
			[LabelText("小雨")]
			private int[] _lightRainID;

			// Token: 0x04006AF0 RID: 27376
			[SerializeField]
			[FormerlySerializedAs("HeavyRainID")]
			[LabelText("大雨")]
			private int[] _heavyRainID;

			// Token: 0x04006AF1 RID: 27377
			[SerializeField]
			[FormerlySerializedAs("FogID")]
			[LabelText("霧")]
			private int[] _fogID;
		}
	}
}
