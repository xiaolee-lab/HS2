using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Player;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000BF4 RID: 3060
	public class CraftPoint : Point, ICommandable
	{
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x00279912 File Offset: 0x00277D12
		public CraftPoint.CraftKind Kind
		{
			[CompilerGenerated]
			get
			{
				return this._kind;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0027991A File Offset: 0x00277D1A
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06005DB7 RID: 23991 RVA: 0x00279922 File Offset: 0x00277D22
		public float Radius
		{
			[CompilerGenerated]
			get
			{
				return this._radius;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x0027992A File Offset: 0x00277D2A
		private List<Transform> NavMeshPoints { get; } = new List<Transform>();

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x00279932 File Offset: 0x00277D32
		public int InstanceID
		{
			get
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?(base.GetInstanceID());
				}
				return this._hashCode.Value;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x00279960 File Offset: 0x00277D60
		// (set) Token: 0x06005DBB RID: 23995 RVA: 0x00279968 File Offset: 0x00277D68
		public bool IsImpossible { get; private set; }

		// Token: 0x06005DBC RID: 23996 RVA: 0x00279971 File Offset: 0x00277D71
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x00279974 File Offset: 0x00277D74
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(commandCenter - basePosition, forward);
			if (num2 > num)
			{
				return false;
			}
			PlayerActor player = Map.GetPlayer();
			if (player != null)
			{
				IState state = player.PlayerController.State;
				if (state is Onbu)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x002799FC File Offset: 0x00277DFC
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				if (!this.NavMeshPoints.IsNullOrEmpty<Transform>())
				{
					bool flag2 = false;
					foreach (Transform transform in this.NavMeshPoints)
					{
						if (!(transform == null))
						{
							nmAgent.CalculatePath(transform.position, this._pathForCalc);
							if (this._pathForCalc.status == NavMeshPathStatus.PathComplete)
							{
								float num = 0f;
								Vector3[] corners = this._pathForCalc.corners;
								float num2 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
								num2 += this._radius;
								for (int i = 0; i < corners.Length - 1; i++)
								{
									float num3 = Vector3.Distance(corners[i], corners[i + 1]);
									num += num3;
								}
								if (num < num2)
								{
									flag2 = true;
									break;
								}
							}
						}
					}
					flag = flag2;
				}
				else
				{
					nmAgent.CalculatePath(this.Position, this._pathForCalc);
					flag &= (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
					float num4 = 0f;
					Vector3[] corners2 = this._pathForCalc.corners;
					for (int j = 0; j < corners2.Length - 1; j++)
					{
						float num5 = Vector3.Distance(corners2[j], corners2[j + 1]);
						num4 += num5;
						float num6 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
						num6 += this._radius;
						if (num4 > num6)
						{
							flag = false;
							break;
						}
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x00279C08 File Offset: 0x00278008
		public bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode();
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x00279C18 File Offset: 0x00278018
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x00279C25 File Offset: 0x00278025
		public Vector3 CommandCenter
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x00279C4F File Offset: 0x0027804F
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x00279C57 File Offset: 0x00278057
		// (set) Token: 0x06005DC4 RID: 24004 RVA: 0x00279C5F File Offset: 0x0027805F
		public CommandLabel.CommandInfo[] DateLabels { get; private set; }

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x00279C68 File Offset: 0x00278068
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00279C70 File Offset: 0x00278070
		public CommandType CommandType { get; }

		// Token: 0x06005DC7 RID: 24007 RVA: 0x00279C78 File Offset: 0x00278078
		protected override void Start()
		{
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
			}
			base.Start();
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			string text = string.Empty;
			int key = -1;
			switch (this._kind)
			{
			case CraftPoint.CraftKind.Medicine:
				key = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.MedicineCraftIconID;
				text = "薬台";
				break;
			case CraftPoint.CraftKind.Pet:
				key = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.PetCraftIcon;
				text = "ペット合成装置";
				break;
			case CraftPoint.CraftKind.Recycling:
				key = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.RecyclingCraftIcon;
				text = "リサイクル装置";
				break;
			default:
				text = "？？？？";
				break;
			}
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out icon2);
			GameObject gameObject2 = base.transform.FindLoop(mapDefines.CraftPointLabelTargetName);
			Transform transform = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
			this._labels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = text,
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = transform,
					Condition = null,
					Event = delegate
					{
						PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
						if (playerActor != null)
						{
							playerActor.CurrentCraftPoint = this;
							playerActor.PlayerController.ChangeState("Craft");
						}
					}
				}
			};
			this.NavMeshPoints.Add(base.transform);
			List<GameObject> list = ListPool<GameObject>.Get();
			base.transform.FindLoopPrefix(list, mapDefines.NavMeshTargetName);
			if (!list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject3 in list)
				{
					this.NavMeshPoints.Add(gameObject3.transform);
				}
			}
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x00279ED0 File Offset: 0x002782D0
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x00279ED7 File Offset: 0x002782D7
		private static List<StuffItem> ItemStock { get; } = new List<StuffItem>();

		// Token: 0x06005DCA RID: 24010 RVA: 0x00279EE0 File Offset: 0x002782E0
		private static StuffItem GetItemInstance()
		{
			CraftPoint.ItemStock.RemoveAll((StuffItem x) => x == null);
			if (CraftPoint.ItemStock.IsNullOrEmpty<StuffItem>())
			{
				return new StuffItem();
			}
			return (!CraftPoint.ItemStock.IsNullOrEmpty<StuffItem>()) ? CraftPoint.ItemStock.Pop<StuffItem>() : new StuffItem();
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x00279F4D File Offset: 0x0027834D
		private static void ReturnItemInstance(StuffItem item)
		{
			if (item == null || CraftPoint.ItemStock.Contains(item))
			{
				return;
			}
			CraftPoint.ItemStock.Add(item);
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x00279F71 File Offset: 0x00278371
		private static void CopyItem(StuffItem from, StuffItem to)
		{
			if (from == null || to == null)
			{
				return;
			}
			to.CategoryID = from.CategoryID;
			to.ID = from.ID;
			to.Count = from.Count;
			to.LatestDateTime = from.LatestDateTime;
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x00279FB0 File Offset: 0x002783B0
		public bool CanDelete()
		{
			if (this._kind != CraftPoint.CraftKind.Recycling)
			{
				return true;
			}
			if (!Singleton<Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return true;
			}
			Game instance = Singleton<Game>.Instance;
			WorldData worldData = instance.WorldData;
			AIProject.SaveData.Environment environment = (worldData == null) ? null : worldData.Environment;
			if (environment == null)
			{
				return true;
			}
			RecyclingData recyclingData = null;
			if (!environment.RecyclingDataTable.TryGetValue(this.RegisterID, out recyclingData) || recyclingData == null)
			{
				return true;
			}
			recyclingData.DecidedItemList.RemoveAll((StuffItem x) => x == null || x.Count <= 0);
			recyclingData.CreatedItemList.RemoveAll((StuffItem x) => x == null || x.Count <= 0);
			if (recyclingData.DecidedItemList.IsNullOrEmpty<StuffItem>() && recyclingData.CreatedItemList.IsNullOrEmpty<StuffItem>())
			{
				return true;
			}
			List<StuffItem> list = ListPool<StuffItem>.Get();
			foreach (StuffItem from in recyclingData.DecidedItemList)
			{
				StuffItem itemInstance = CraftPoint.GetItemInstance();
				CraftPoint.CopyItem(from, itemInstance);
				list.AddItem(itemInstance);
			}
			foreach (StuffItem from2 in recyclingData.CreatedItemList)
			{
				StuffItem itemInstance2 = CraftPoint.GetItemInstance();
				CraftPoint.CopyItem(from2, itemInstance2);
				list.AddItem(itemInstance2);
			}
			Map instance2 = Singleton<Map>.Instance;
			List<UnityEx.ValueTuple<int, List<StuffItem>>> inventoryList = instance2.GetInventoryList();
			List<UnityEx.ValueTuple<int, List<StuffItem>>> list2 = ListPool<UnityEx.ValueTuple<int, List<StuffItem>>>.Get();
			foreach (UnityEx.ValueTuple<int, List<StuffItem>> valueTuple in inventoryList)
			{
				int item = valueTuple.Item1;
				List<StuffItem> item2 = valueTuple.Item2;
				List<StuffItem> list3 = ListPool<StuffItem>.Get();
				list2.Add(new UnityEx.ValueTuple<int, List<StuffItem>>(item, list3));
				if (!item2.IsNullOrEmpty<StuffItem>())
				{
					foreach (StuffItem from3 in item2)
					{
						StuffItem itemInstance3 = CraftPoint.GetItemInstance();
						CraftPoint.CopyItem(from3, itemInstance3);
						list3.Add(itemInstance3);
					}
				}
			}
			foreach (UnityEx.ValueTuple<int, List<StuffItem>> valueTuple2 in list2)
			{
				int item3 = valueTuple2.Item1;
				List<StuffItem> item4 = valueTuple2.Item2;
				for (int i = 0; i < list.Count; i++)
				{
					StuffItem element = list.GetElement(i);
					if (element == null || element.Count <= 0)
					{
						list.RemoveAt(i);
						i--;
					}
					else
					{
						StuffItem itemInstance4 = CraftPoint.GetItemInstance();
						CraftPoint.CopyItem(element, itemInstance4);
						int num = 0;
						item4.CanAddItem(item3, itemInstance4, out num);
						if (0 < num)
						{
							num = Mathf.Min(num, itemInstance4.Count);
							item4.AddItem(itemInstance4, num, item3);
						}
						element.Count -= num;
						if (element.Count <= 0)
						{
							list.RemoveAt(i);
							i--;
						}
					}
				}
			}
			list.RemoveAll((StuffItem x) => x == null || x.Count <= 0);
			bool flag = list.IsNullOrEmpty<StuffItem>();
			if (flag)
			{
				foreach (UnityEx.ValueTuple<int, List<StuffItem>> valueTuple3 in inventoryList)
				{
					int item5 = valueTuple3.Item1;
					List<StuffItem> item6 = valueTuple3.Item2;
					instance2.SendItemListToList(item5, recyclingData.DecidedItemList, item6);
					instance2.SendItemListToList(item5, recyclingData.CreatedItemList, item6);
				}
			}
			foreach (UnityEx.ValueTuple<int, List<StuffItem>> valueTuple4 in list2)
			{
				if (valueTuple4.Item2 != null)
				{
					foreach (StuffItem item7 in valueTuple4.Item2)
					{
						CraftPoint.ReturnItemInstance(item7);
					}
					ListPool<StuffItem>.Release(valueTuple4.Item2);
				}
			}
			foreach (StuffItem item8 in list)
			{
				CraftPoint.ReturnItemInstance(item8);
			}
			ListPool<StuffItem>.Release(list);
			instance2.ReturnInventoryList(inventoryList);
			return flag;
		}

		// Token: 0x040053CB RID: 21451
		[SerializeField]
		private CraftPoint.CraftKind _kind;

		// Token: 0x040053CC RID: 21452
		[SerializeField]
		private int _id;

		// Token: 0x040053CD RID: 21453
		[SerializeField]
		private bool _enabledRangeCheck = true;

		// Token: 0x040053CE RID: 21454
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x040053D0 RID: 21456
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x040053D1 RID: 21457
		private int? _hashCode;

		// Token: 0x040053D3 RID: 21459
		private NavMeshPath _pathForCalc;

		// Token: 0x040053D4 RID: 21460
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x02000BF5 RID: 3061
		public enum CraftKind
		{
			// Token: 0x040053DE RID: 21470
			Medicine,
			// Token: 0x040053DF RID: 21471
			Pet,
			// Token: 0x040053E0 RID: 21472
			Recycling
		}
	}
}
