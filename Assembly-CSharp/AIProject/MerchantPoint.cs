using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C22 RID: 3106
	public class MerchantPoint : Point
	{
		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x0028690F File Offset: 0x00284D0F
		public int ActionID
		{
			[CompilerGenerated]
			get
			{
				return this._actionID;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x00286917 File Offset: 0x00284D17
		public int AreaID
		{
			[CompilerGenerated]
			get
			{
				return this._areaID;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x0028691F File Offset: 0x00284D1F
		// (set) Token: 0x06005FD7 RID: 24535 RVA: 0x00286927 File Offset: 0x00284D27
		[ShowInInlineEditors]
		[HideInEditorMode]
		[ReadOnly]
		public int AreaIDOnChunk { get; set; }

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06005FD8 RID: 24536 RVA: 0x00286930 File Offset: 0x00284D30
		public int GroupID
		{
			[CompilerGenerated]
			get
			{
				return this._groupID;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x00286938 File Offset: 0x00284D38
		public Merchant.EventType EventType
		{
			[CompilerGenerated]
			get
			{
				return this._eventType;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x00286940 File Offset: 0x00284D40
		public bool IsStartPoint
		{
			[CompilerGenerated]
			get
			{
				return this._isStartPoint;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x00286948 File Offset: 0x00284D48
		public bool IsExitPoint
		{
			[CompilerGenerated]
			get
			{
				return this._isExitPoint;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x00286950 File Offset: 0x00284D50
		// (set) Token: 0x06005FDD RID: 24541 RVA: 0x00286958 File Offset: 0x00284D58
		public int PointID { get; set; }

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06005FDE RID: 24542 RVA: 0x00286961 File Offset: 0x00284D61
		public Vector3 Destination
		{
			get
			{
				return (!(this._destination != null)) ? base.transform.position : this._destination.position;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x0028698F File Offset: 0x00284D8F
		// (set) Token: 0x06005FE0 RID: 24544 RVA: 0x00286997 File Offset: 0x00284D97
		public List<ForcedHideObject> ItemObjects { get; set; }

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x002869A0 File Offset: 0x00284DA0
		public int InstanceID
		{
			get
			{
				if (this.instanceID != null)
				{
					return this.instanceID.Value;
				}
				int? num = this.instanceID = new int?(base.GetInstanceID());
				return num.Value;
			}
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x002869E5 File Offset: 0x00284DE5
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x002869ED File Offset: 0x00284DED
		protected override void Start()
		{
			this.Refresh();
			base.Start();
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x002869FC File Offset: 0x00284DFC
		public void SetItemObjectsActive(bool active)
		{
			if (this.ItemObjects.IsNullOrEmpty<ForcedHideObject>())
			{
				return;
			}
			foreach (ForcedHideObject forcedHideObject in this.ItemObjects)
			{
				if (!(forcedHideObject == null))
				{
					if (active)
					{
						forcedHideObject.Show();
					}
					else
					{
						forcedHideObject.Hide();
					}
				}
			}
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x00286A8C File Offset: 0x00284E8C
		public void ShowItemObjects()
		{
			this.SetItemObjectsActive(true);
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x00286A95 File Offset: 0x00284E95
		public void HideItemObjects()
		{
			this.SetItemObjectsActive(false);
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00286AA0 File Offset: 0x00284EA0
		public bool AnyActiveItemObjects()
		{
			if (this.ItemObjects.IsNullOrEmpty<ForcedHideObject>())
			{
				return false;
			}
			foreach (ForcedHideObject forcedHideObject in this.ItemObjects)
			{
				if (forcedHideObject != null && forcedHideObject.Active)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x00286B28 File Offset: 0x00284F28
		public void Refresh()
		{
			if (this._destination == null)
			{
				this._destination = base.transform;
			}
			if (this._pointInfos != null)
			{
				this._pointInfos.Clear();
			}
			if (base.OwnerArea == null)
			{
				return;
			}
			this.AreaIDOnChunk = base.OwnerArea.AreaID;
			Dictionary<int, Dictionary<int, List<MerchantPointInfo>>> merchantPointInfoTable = Singleton<Manager.Resources>.Instance.Map.MerchantPointInfoTable;
			int key = (!Singleton<Manager.Map>.IsInstance()) ? 0 : Singleton<Manager.Map>.Instance.MapID;
			if (merchantPointInfoTable.ContainsKey(key) && merchantPointInfoTable[key].ContainsKey(this._actionID))
			{
				this._pointInfos = merchantPointInfoTable[key][this._actionID];
			}
			this._eventType = (Merchant.EventType)0;
			foreach (MerchantPointInfo merchantPointInfo in this._pointInfos)
			{
				this._eventType |= merchantPointInfo.eventTypeMask;
			}
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x00286C58 File Offset: 0x00285058
		public void GetPointInfoList(Merchant.EventType eventType, ref List<MerchantPointInfo> outInfoList)
		{
			if (eventType == (Merchant.EventType)0)
			{
				return;
			}
			outInfoList.Clear();
			foreach (MerchantPointInfo item in this._pointInfos)
			{
				if (item.eventTypeMask == eventType)
				{
					if (outInfoList == null)
					{
						outInfoList = new List<MerchantPointInfo>();
					}
					outInfoList.Add(item);
				}
			}
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x00286CE0 File Offset: 0x002850E0
		public bool TryGetPointInfo(Merchant.EventType eventType, out MerchantPointInfo pointInfo)
		{
			List<MerchantPointInfo> list = ListPool<MerchantPointInfo>.Get();
			this.GetPointInfoList(eventType, ref list);
			if (list.IsNullOrEmpty<MerchantPointInfo>())
			{
				ListPool<MerchantPointInfo>.Release(list);
				pointInfo = default(MerchantPointInfo);
				return false;
			}
			pointInfo = list[UnityEngine.Random.Range(0, list.Count)];
			ListPool<MerchantPointInfo>.Release(list);
			return true;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00286D40 File Offset: 0x00285140
		public Transform GetBasePoint(string baseNullName)
		{
			GameObject gameObject = base.transform.FindLoop(baseNullName);
			return (!(gameObject == null)) ? gameObject.transform : base.transform;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x00286D78 File Offset: 0x00285178
		public Transform GetBasePoint(Merchant.EventType eventType)
		{
			MerchantPointInfo merchantPointInfo;
			if (!this.TryGetPointInfo(eventType, out merchantPointInfo))
			{
				return base.transform;
			}
			return this.GetBasePoint(merchantPointInfo.baseNullName);
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x00286DA7 File Offset: 0x002851A7
		public Transform GetRecoveryPoint(string recoveryNullName)
		{
			GameObject gameObject = base.transform.FindLoop(recoveryNullName);
			return (gameObject != null) ? gameObject.transform : null;
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x00286DC4 File Offset: 0x002851C4
		public Transform GetRecoveryPoint(Merchant.EventType eventType)
		{
			MerchantPointInfo merchantPointInfo;
			if (!this.TryGetPointInfo(eventType, out merchantPointInfo))
			{
				return base.transform;
			}
			return this.GetRecoveryPoint(merchantPointInfo.recoveryNullName);
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x00286DF4 File Offset: 0x002851F4
		public Tuple<Transform, Transform> GetEventPoint(Merchant.EventType eventType)
		{
			MerchantPointInfo merchantPointInfo;
			if (!this.TryGetPointInfo(eventType, out merchantPointInfo))
			{
				return new Tuple<Transform, Transform>(base.transform, base.transform);
			}
			Transform basePoint = this.GetBasePoint(merchantPointInfo.baseNullName);
			Transform recoveryPoint = this.GetRecoveryPoint(merchantPointInfo.recoveryNullName);
			return new Tuple<Transform, Transform>(basePoint, recoveryPoint);
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x00286E44 File Offset: 0x00285244
		public Tuple<MerchantPointInfo, Transform, Transform> GetEventInfo(Merchant.EventType eventType)
		{
			MerchantPointInfo item;
			this.TryGetPointInfo(eventType, out item);
			Transform basePoint = this.GetBasePoint(item.baseNullName);
			Transform recoveryPoint = this.GetRecoveryPoint(item.recoveryNullName);
			return new Tuple<MerchantPointInfo, Transform, Transform>(item, basePoint, recoveryPoint);
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x00286E80 File Offset: 0x00285280
		public bool CheckDistance(Vector3 position, float checkDistance)
		{
			if (Vector3.Distance(this.Destination, position) <= checkDistance)
			{
				return true;
			}
			if (!this.ItemObjects.IsNullOrEmpty<ForcedHideObject>())
			{
				foreach (ForcedHideObject forcedHideObject in this.ItemObjects)
				{
					if (!(forcedHideObject == null))
					{
						if (Vector3.Distance(forcedHideObject.transform.position, position) <= checkDistance)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x00286F2C File Offset: 0x0028532C
		public void SetStand(MerchantActor merchant, Transform t, bool enableFade, float fadeTime, int dirc, System.Action onComplete = null)
		{
			MerchantPoint.<SetStand>c__AnonStorey0 <SetStand>c__AnonStorey = new MerchantPoint.<SetStand>c__AnonStorey0();
			<SetStand>c__AnonStorey.t = t;
			<SetStand>c__AnonStorey.merchant = merchant;
			<SetStand>c__AnonStorey.onComplete = onComplete;
			if (<SetStand>c__AnonStorey.merchant == null || <SetStand>c__AnonStorey.t == null)
			{
				System.Action onComplete2 = <SetStand>c__AnonStorey.onComplete;
				if (onComplete2 != null)
				{
					onComplete2();
				}
				return;
			}
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(fadeTime, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			IDisposable disposable = connectableObservable.Connect();
			<SetStand>c__AnonStorey.merchant.DisposeSequenceAction();
			<SetStand>c__AnonStorey.position = <SetStand>c__AnonStorey.merchant.Position;
			<SetStand>c__AnonStorey.rotation = <SetStand>c__AnonStorey.merchant.Rotation;
			if (dirc != 0)
			{
				if (dirc != 1)
				{
					System.Action onComplete3 = <SetStand>c__AnonStorey.onComplete;
					if (onComplete3 != null)
					{
						onComplete3();
					}
				}
				else
				{
					Vector3 vector = base.transform.position - <SetStand>c__AnonStorey.merchant.Position;
					vector.y = 0f;
					Quaternion lookRotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
					if (enableFade)
					{
						disposable = connectableObservable.Subscribe(delegate(TimeInterval<float> x)
						{
							<SetStand>c__AnonStorey.merchant.Rotation = Quaternion.Lerp(<SetStand>c__AnonStorey.rotation, lookRotation, x.Value);
						});
						<SetStand>c__AnonStorey.merchant.AddSequenceActionDisposable(disposable);
						disposable = Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
						{
							connectableObservable
						}).Subscribe(delegate(TimeInterval<float>[] _)
						{
							System.Action onComplete6 = <SetStand>c__AnonStorey.onComplete;
							if (onComplete6 != null)
							{
								onComplete6();
							}
						}).AddTo(<SetStand>c__AnonStorey.merchant);
						<SetStand>c__AnonStorey.merchant.AddSequenceActionDisposable(disposable);
						<SetStand>c__AnonStorey.merchant.AddSequenceActionOnComplete(<SetStand>c__AnonStorey.onComplete);
					}
					else
					{
						<SetStand>c__AnonStorey.merchant.Rotation = lookRotation;
						System.Action onComplete4 = <SetStand>c__AnonStorey.onComplete;
						if (onComplete4 != null)
						{
							onComplete4();
						}
					}
				}
			}
			else if (enableFade)
			{
				<SetStand>c__AnonStorey.merchant.AddSequenceActionDisposable(disposable);
				disposable = connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					<SetStand>c__AnonStorey.merchant.Position = Vector3.Lerp(<SetStand>c__AnonStorey.position, <SetStand>c__AnonStorey.t.position, x.Value);
					<SetStand>c__AnonStorey.merchant.Rotation = Quaternion.Lerp(<SetStand>c__AnonStorey.rotation, <SetStand>c__AnonStorey.t.rotation, x.Value);
				}).AddTo(<SetStand>c__AnonStorey.merchant);
				<SetStand>c__AnonStorey.merchant.AddSequenceActionDisposable(disposable);
				disposable = Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
				{
					connectableObservable
				}).Subscribe(delegate(TimeInterval<float>[] _)
				{
					System.Action onComplete6 = <SetStand>c__AnonStorey.onComplete;
					if (onComplete6 != null)
					{
						onComplete6();
					}
					<SetStand>c__AnonStorey.merchant.ClearSequenceAction();
				}).AddTo(<SetStand>c__AnonStorey.merchant);
				<SetStand>c__AnonStorey.merchant.AddSequenceActionDisposable(disposable);
				<SetStand>c__AnonStorey.merchant.AddSequenceActionOnComplete(<SetStand>c__AnonStorey.onComplete);
			}
			else
			{
				<SetStand>c__AnonStorey.merchant.Position = <SetStand>c__AnonStorey.t.position;
				<SetStand>c__AnonStorey.merchant.Rotation = <SetStand>c__AnonStorey.t.rotation;
				System.Action onComplete5 = <SetStand>c__AnonStorey.onComplete;
				if (onComplete5 != null)
				{
					onComplete5();
				}
			}
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x002871BC File Offset: 0x002855BC
		public void SetStand(Transform root, Transform t, bool enableFade, float fadeTime, int dirc, System.Action onComplete = null)
		{
			MerchantPoint.<SetStand>c__AnonStorey2 <SetStand>c__AnonStorey = new MerchantPoint.<SetStand>c__AnonStorey2();
			<SetStand>c__AnonStorey.t = t;
			<SetStand>c__AnonStorey.root = root;
			<SetStand>c__AnonStorey.onComplete = onComplete;
			if (<SetStand>c__AnonStorey.root == null || <SetStand>c__AnonStorey.t == null)
			{
				System.Action onComplete2 = <SetStand>c__AnonStorey.onComplete;
				if (onComplete2 != null)
				{
					onComplete2();
				}
				return;
			}
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(fadeTime, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			connectableObservable.Connect();
			<SetStand>c__AnonStorey.position = <SetStand>c__AnonStorey.root.position;
			<SetStand>c__AnonStorey.rotation = <SetStand>c__AnonStorey.root.rotation;
			if (dirc != 0)
			{
				if (dirc != 1)
				{
					System.Action onComplete3 = <SetStand>c__AnonStorey.onComplete;
					if (onComplete3 != null)
					{
						onComplete3();
					}
				}
				else
				{
					Vector3 vector = base.transform.position - <SetStand>c__AnonStorey.root.position;
					vector.y = 0f;
					Quaternion lookRotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
					if (enableFade)
					{
						connectableObservable.Subscribe(delegate(TimeInterval<float> x)
						{
							<SetStand>c__AnonStorey.root.rotation = Quaternion.Lerp(<SetStand>c__AnonStorey.rotation, lookRotation, x.Value);
						});
						Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
						{
							connectableObservable
						}).Subscribe(delegate(TimeInterval<float>[] _)
						{
							System.Action onComplete6 = <SetStand>c__AnonStorey.onComplete;
							if (onComplete6 != null)
							{
								onComplete6();
							}
						});
					}
					else
					{
						<SetStand>c__AnonStorey.root.rotation = lookRotation;
						System.Action onComplete4 = <SetStand>c__AnonStorey.onComplete;
						if (onComplete4 != null)
						{
							onComplete4();
						}
					}
				}
			}
			else if (enableFade)
			{
				connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					<SetStand>c__AnonStorey.root.position = Vector3.Lerp(<SetStand>c__AnonStorey.position, <SetStand>c__AnonStorey.t.position, x.Value);
					<SetStand>c__AnonStorey.root.rotation = Quaternion.Lerp(<SetStand>c__AnonStorey.rotation, <SetStand>c__AnonStorey.t.rotation, x.Value);
				});
				Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
				{
					connectableObservable
				}).Subscribe(delegate(TimeInterval<float>[] _)
				{
					System.Action onComplete6 = <SetStand>c__AnonStorey.onComplete;
					if (onComplete6 != null)
					{
						onComplete6();
					}
				});
			}
			else
			{
				<SetStand>c__AnonStorey.root.position = <SetStand>c__AnonStorey.t.position;
				<SetStand>c__AnonStorey.root.rotation = <SetStand>c__AnonStorey.t.rotation;
				System.Action onComplete5 = <SetStand>c__AnonStorey.onComplete;
				if (onComplete5 != null)
				{
					onComplete5();
				}
			}
		}

		// Token: 0x04005550 RID: 21840
		[SerializeField]
		[Tooltip("行動ID")]
		private int _actionID;

		// Token: 0x04005551 RID: 21841
		[SerializeField]
		[Tooltip("開放エリア等に使用")]
		[DisableInPlayMode]
		private int _areaID;

		// Token: 0x04005553 RID: 21843
		[SerializeField]
		[Tooltip("エリア内でのグループ分け")]
		[DisableInPlayMode]
		private int _groupID;

		// Token: 0x04005554 RID: 21844
		[SerializeField]
		[Tooltip("このポイントで起こせる行動")]
		[HideInEditorMode]
		private Merchant.EventType _eventType;

		// Token: 0x04005555 RID: 21845
		[SerializeField]
		[Tooltip("商人登場イベント時のスタートポイント")]
		private bool _isStartPoint;

		// Token: 0x04005556 RID: 21846
		[SerializeField]
		[Tooltip("島から出ていく時に使うポイント")]
		private bool _isExitPoint;

		// Token: 0x04005558 RID: 21848
		private List<MerchantPointInfo> _pointInfos = new List<MerchantPointInfo>();

		// Token: 0x04005559 RID: 21849
		[SerializeField]
		[Tooltip("Actorがこのポイントを目指す到達地点")]
		private Transform _destination;

		// Token: 0x0400555B RID: 21851
		private int? instanceID;
	}
}
