using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Housing;
using Manager;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x0200103E RID: 4158
	public class Env3DSEPoint : Point, EnvArea3DSE.IPlayInfo
	{
		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06008B29 RID: 35625 RVA: 0x003A9F18 File Offset: 0x003A8318
		public AudioSource Audio
		{
			[CompilerGenerated]
			get
			{
				return this._audio;
			}
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x003A9F20 File Offset: 0x003A8320
		public void Stop()
		{
			this.SoundStop();
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06008B2B RID: 35627 RVA: 0x003A9F28 File Offset: 0x003A8328
		public bool IsHousingItem
		{
			[CompilerGenerated]
			get
			{
				return this._itemComponent != null;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06008B2C RID: 35628 RVA: 0x003A9F36 File Offset: 0x003A8336
		// (set) Token: 0x06008B2D RID: 35629 RVA: 0x003A9F3E File Offset: 0x003A833E
		public int AreaID { get; private set; } = -1;

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06008B2E RID: 35630 RVA: 0x003A9F47 File Offset: 0x003A8347
		// (set) Token: 0x06008B2F RID: 35631 RVA: 0x003A9F4F File Offset: 0x003A834F
		public bool PlayEnabled { get; set; }

		// Token: 0x06008B30 RID: 35632 RVA: 0x003A9F58 File Offset: 0x003A8358
		protected override void Start()
		{
			base.Start();
			base.OwnerArea = null;
			this.PlayEnabled = this._playOnAwake;
			this._itemComponent = base.GetComponent<ItemComponent>();
			if (this._itemComponent == null)
			{
				this._itemComponent = base.GetComponentInParent<ItemComponent>();
			}
			if (this._itemComponent != null && Singleton<Map>.IsInstance() && Singleton<Manager.Resources>.IsInstance())
			{
				LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
				RaycastHit raycastHit;
				if (Physics.Raycast(this._itemComponent.position + Vector3.up * 5f, Vector3.down, out raycastHit, 1000f, areaDetectionLayer))
				{
					Dictionary<int, Chunk> chunkTable = Singleton<Map>.Instance.ChunkTable;
					bool flag = false;
					foreach (KeyValuePair<int, Chunk> keyValuePair in chunkTable)
					{
						foreach (MapArea mapArea in keyValuePair.Value.MapAreas)
						{
							if (flag = mapArea.ContainsCollider(raycastHit.collider))
							{
								base.OwnerArea = mapArea;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
				bool flag2 = base.OwnerArea != null;
				this.PlayEnabled = flag2;
				if (flag2)
				{
					this.AreaID = base.OwnerArea.AreaID;
					Dictionary<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>> housingEnvSEPointTable = Singleton<Map>.Instance.HousingEnvSEPointTable;
					UnityEx.ValueTuple<bool, List<Env3DSEPoint>> value;
					if (!housingEnvSEPointTable.TryGetValue(this.AreaID, out value))
					{
						Dictionary<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>> dictionary = housingEnvSEPointTable;
						int areaID = this.AreaID;
						value = default(UnityEx.ValueTuple<bool, List<Env3DSEPoint>>);
						dictionary[areaID] = value;
						value.Item1 = Singleton<Map>.Instance.ActiveEnvAreaID(this.AreaID);
						value.Item2 = new List<Env3DSEPoint>();
					}
					if (base.enabled != value.Item1)
					{
						base.enabled = value.Item1;
					}
					if (!value.Item2.Contains(this))
					{
						value.Item2.Add(this);
					}
					housingEnvSEPointTable[this.AreaID] = value;
				}
			}
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06008B31 RID: 35633 RVA: 0x003AA1CC File Offset: 0x003A85CC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.ResetDelay();
		}

		// Token: 0x06008B32 RID: 35634 RVA: 0x003AA1DA File Offset: 0x003A85DA
		protected override void OnDisable()
		{
			this.SoundStop();
			base.OnDisable();
		}

		// Token: 0x06008B33 RID: 35635 RVA: 0x003AA1E8 File Offset: 0x003A85E8
		private void OnDestroy()
		{
			if (this._itemComponent != null && Singleton<Map>.IsInstance() && 0 <= this.AreaID)
			{
				Dictionary<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>> housingEnvSEPointTable = Singleton<Map>.Instance.HousingEnvSEPointTable;
				UnityEx.ValueTuple<bool, List<Env3DSEPoint>> valueTuple;
				if (housingEnvSEPointTable.TryGetValue(this.AreaID, out valueTuple) && !valueTuple.Item2.IsNullOrEmpty<Env3DSEPoint>() && valueTuple.Item2.Contains(this))
				{
					valueTuple.Item2.Remove(this);
				}
			}
		}

		// Token: 0x06008B34 RID: 35636 RVA: 0x003AA26C File Offset: 0x003A866C
		private void OnUpdate()
		{
			if (!Singleton<Map>.IsInstance() || !Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Map instance = Singleton<Map>.Instance;
			Manager.Resources instance2 = Singleton<Manager.Resources>.Instance;
			Transform transform = (!(this._playRoot != null)) ? base.transform : this._playRoot;
			bool isEnableDistance = this._isEnableDistance;
			if (this.PlayEnabled)
			{
				this._isEnableDistance = ((!this._isEnableDistance) ? this.EnableDistance(instance2, instance, transform) : (!this.DisableDistance(instance2, instance, transform)));
			}
			else
			{
				this._isEnableDistance = false;
			}
			if (this._isPlay)
			{
				bool flag = this._audio == null || (!this._isLoop && !this._audio.isPlaying);
				if (flag && this._isLoop)
				{
					this._delayTime = 1f;
				}
				if (flag || !this._isEnableDistance)
				{
					this.SoundReset();
				}
			}
			else if (this._isEnableDistance)
			{
				this._elapsedTime += Time.deltaTime;
				if (this._delayTime <= this._elapsedTime)
				{
					this._elapsedTime = 0f;
					this.ResetDelay();
					this.SoundPlay(transform);
				}
			}
			if (this._audio != null)
			{
				this._audio.transform.SetPositionAndRotation(transform.position, transform.rotation);
			}
			if (!this._isEnableDistance && isEnableDistance)
			{
				this.ResetDelay();
				this._elapsedTime = 0f;
				this._firstPlaying = true;
			}
		}

		// Token: 0x06008B35 RID: 35637 RVA: 0x003AA41C File Offset: 0x003A881C
		public float GetSqrDistanceFromCamera(Transform cameraT, Vector3 p1)
		{
			Vector3 vector = p1 - cameraT.position;
			return Vector3.SqrMagnitude(vector);
		}

		// Token: 0x06008B36 RID: 35638 RVA: 0x003AA43C File Offset: 0x003A883C
		public float GetSqrDistanceFromCamera(Transform camera, Transform t1)
		{
			Vector3 vector = t1.position - camera.position;
			return Vector3.SqrMagnitude(vector);
		}

		// Token: 0x06008B37 RID: 35639 RVA: 0x003AA464 File Offset: 0x003A8864
		public int GetSqrDistanceSort(Transform camera, Transform t1, Transform t2)
		{
			Vector3 vector = t1.position - camera.position;
			Vector3 vector2 = t2.position - camera.position;
			return (int)(Vector3.SqrMagnitude(vector) - Vector3.SqrMagnitude(vector2));
		}

		// Token: 0x06008B38 RID: 35640 RVA: 0x003AA4A4 File Offset: 0x003A88A4
		private bool EnableDistance(Manager.Resources res, Map map, Transform root)
		{
			Camera cameraComponent = Map.GetCameraComponent();
			Transform transform = (!(cameraComponent != null)) ? null : cameraComponent.transform;
			if (transform == null)
			{
				return false;
			}
			float num = Vector3.SqrMagnitude(root.position - transform.position);
			float num2 = this._decay.y + res.SoundPack.EnviroInfo.EnableDistance;
			num2 *= num2;
			return num <= num2;
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x003AA520 File Offset: 0x003A8920
		private bool DisableDistance(Manager.Resources res, Map map, Transform root)
		{
			Camera cameraComponent = Map.GetCameraComponent();
			Transform transform = (!(cameraComponent != null)) ? null : cameraComponent.transform;
			if (transform == null)
			{
				return true;
			}
			float num = Vector3.SqrMagnitude(root.position - transform.position);
			float num2 = this._decay.y + res.SoundPack.EnviroInfo.DisableDistance;
			num2 *= num2;
			return num2 < num;
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x003AA598 File Offset: 0x003A8998
		private void SoundPlay(Transform root)
		{
			this._isPlay = true;
			List<EnvArea3DSE.IPlayInfo> playList = EnvArea3DSE.PlayAudioSourceList;
			playList.RemoveAll((EnvArea3DSE.IPlayInfo ax) => ax == null || ax.Audio == null || ax.Audio.gameObject == null);
			SoundPack.SoundSystemInfoGroup soundSystemInfo = Singleton<Manager.Resources>.Instance.SoundPack.SoundSystemInfo;
			if (soundSystemInfo.EnviroSEMaxCount <= playList.Count)
			{
				bool flag = true;
				int num = playList.Count - soundSystemInfo.EnviroSEMaxCount + 1;
				List<EnvArea3DSE.IPlayInfo> list = ListPool<EnvArea3DSE.IPlayInfo>.Get();
				list.AddRange(playList);
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
			if (this._firstPlaying)
			{
				fadeTime = ((!this._setFirstFadeTime) ? Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.FadeTime : this._firstFadeTime);
				this._firstPlaying = false;
			}
			this._audio = Singleton<Manager.Resources>.Instance.SoundPack.PlayEnviroSE(this._clipID, fadeTime);
			if (this._audio == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this._fadePlayer = this._audio.GetComponentInChildren<FadePlayer>(true);
			this._audio.loop = this._isLoop;
			this._audio.minDistance = this._decay.x;
			this._audio.maxDistance = this._decay.y;
			if (!playList.Contains(this))
			{
				playList.Add(this);
			}
			this._audio.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				playList.Remove(this);
			});
		}

		// Token: 0x06008B3B RID: 35643 RVA: 0x003AA7EE File Offset: 0x003A8BEE
		private void ResetDelay()
		{
			this._delayTime = ((!this._isLoop) ? this._interval.RandomRange() : 0f);
		}

		// Token: 0x06008B3C RID: 35644 RVA: 0x003AA816 File Offset: 0x003A8C16
		private void SoundReset()
		{
			this._isPlay = false;
			this._elapsedTime = 0f;
			this.SoundStop();
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x003AA830 File Offset: 0x003A8C30
		private void SoundStop()
		{
			if (this._fadePlayer != null)
			{
				float fadeTime = (!Singleton<Manager.Resources>.IsInstance()) ? 0f : Singleton<Manager.Resources>.Instance.SoundPack.EnviroInfo.FadeTime;
				this._fadePlayer.Stop(fadeTime);
			}
			else if (this._audio != null && this._audio.transform != null)
			{
				if (Singleton<Sound>.IsInstance())
				{
					Singleton<Sound>.Instance.Stop(Sound.Type.ENV, this._audio.transform);
				}
				else
				{
					this._audio.Stop();
					UnityEngine.Object.Destroy(this._audio.gameObject);
				}
			}
			this._audio = null;
			this._fadePlayer = null;
		}

		// Token: 0x06008B3E RID: 35646 RVA: 0x003AA900 File Offset: 0x003A8D00
		public void SoundForcedPlay(bool useFadeTime = false)
		{
			this.PlayEnabled = true;
			this._firstPlaying = useFadeTime;
			if (this._audio == null || this._fadePlayer == null)
			{
				bool flag = Singleton<Map>.IsInstance() && Singleton<Map>.Instance.ActiveEnvAreaID(this.AreaID);
				if (flag)
				{
					this._elapsedTime = 0f;
					this.ResetDelay();
					this.SoundPlay((!(this._playRoot != null)) ? base.transform : this._playRoot);
				}
			}
		}

		// Token: 0x06008B3F RID: 35647 RVA: 0x003AA9A0 File Offset: 0x003A8DA0
		public void SoundForcedStop()
		{
			if (this._audio != null && this._audio.gameObject != null)
			{
				UnityEngine.Object.Destroy(this._audio.gameObject);
			}
			this._audio = null;
			this._fadePlayer = null;
			this.PlayEnabled = false;
			this._firstPlaying = true;
		}

		// Token: 0x06008B40 RID: 35648 RVA: 0x003AAA00 File Offset: 0x003A8E00
		public void RefreshMapAreaObject()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
		}

		// Token: 0x040071C7 RID: 29127
		[SerializeField]
		private string _summary = string.Empty;

		// Token: 0x040071C8 RID: 29128
		[SerializeField]
		private int _clipID = -1;

		// Token: 0x040071C9 RID: 29129
		[SerializeField]
		private Transform _playRoot;

		// Token: 0x040071CA RID: 29130
		[SerializeField]
		private bool _playOnAwake;

		// Token: 0x040071CB RID: 29131
		[SerializeField]
		private Vector2 _decay = default(Vector2);

		// Token: 0x040071CC RID: 29132
		[SerializeField]
		private bool _isLoop = true;

		// Token: 0x040071CD RID: 29133
		[SerializeField]
		private Vector2 _interval = default(Vector2);

		// Token: 0x040071CE RID: 29134
		[SerializeField]
		private bool _setFirstFadeTime;

		// Token: 0x040071CF RID: 29135
		[SerializeField]
		private float _firstFadeTime;

		// Token: 0x040071D0 RID: 29136
		private AudioSource _audio;

		// Token: 0x040071D1 RID: 29137
		private FadePlayer _fadePlayer;

		// Token: 0x040071D2 RID: 29138
		private float _delayTime;

		// Token: 0x040071D3 RID: 29139
		private float _elapsedTime;

		// Token: 0x040071D4 RID: 29140
		private bool _isPlay;

		// Token: 0x040071D5 RID: 29141
		private bool _isEnableDistance;

		// Token: 0x040071D6 RID: 29142
		private bool _firstPlaying = true;

		// Token: 0x040071D7 RID: 29143
		private ItemComponent _itemComponent;
	}
}
