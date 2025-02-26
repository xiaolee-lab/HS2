using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C29 RID: 3113
	public class SearchActionPoint : ActionPoint
	{
		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06006052 RID: 24658 RVA: 0x0027C14B File Offset: 0x0027A54B
		public int TableID
		{
			[CompilerGenerated]
			get
			{
				return this._tableID;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x0027C153 File Offset: 0x0027A553
		public int Grade
		{
			[CompilerGenerated]
			get
			{
				return this._grade;
			}
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x0027C15C File Offset: 0x0027A55C
		protected override void InitSub()
		{
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			EventType playerEventMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.PlayerEventMask;
			List<CommandLabel.CommandInfo> list = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator = ActionPoint.LabelTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SearchActionPoint.<InitSub>c__AnonStorey0 <InitSub>c__AnonStorey = new SearchActionPoint.<InitSub>c__AnonStorey0();
					<InitSub>c__AnonStorey.pair = enumerator.Current;
					<InitSub>c__AnonStorey.$this = this;
					if (this._playerEventType.Contains(<InitSub>c__AnonStorey.pair.Key))
					{
						if (playerEventMask.Contains(<InitSub>c__AnonStorey.pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(<InitSub>c__AnonStorey.pair.Key, out valueTuple))
							{
								ActionPointInfo actionPointInfo = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == <InitSub>c__AnonStorey.pair.Key);
								string actionName = actionPointInfo.actionName;
								Sprite icon2;
								if (<InitSub>c__AnonStorey.pair.Key == EventType.Search)
								{
									if (actionPointInfo.searchAreaID > -1)
									{
										this._tableID = actionPointInfo.searchAreaID;
									}
									if (actionPointInfo.gradeValue > -1 && actionPointInfo.gradeValue > this._grade)
									{
										this._grade = actionPointInfo.gradeValue;
									}
									Dictionary<int, int> dictionary;
									if (Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(this._tableID, out dictionary))
									{
										int key;
										if (dictionary.TryGetValue(this._grade, out key))
										{
											Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key, out icon2);
										}
										else
										{
											icon2 = null;
										}
									}
									else
									{
										icon2 = null;
									}
								}
								else
								{
									Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo.iconID, out icon2);
								}
								GameObject gameObject = base.transform.FindLoop(actionPointInfo.labelNullName);
								Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? base.transform;
								list.Add(new CommandLabel.CommandInfo
								{
									Text = actionName,
									Icon = icon2,
									IsHold = (<InitSub>c__AnonStorey.pair.Key == EventType.Search),
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = transform,
									Condition = ((PlayerActor x) => <InitSub>c__AnonStorey.$this.CanAction(x, <InitSub>c__AnonStorey.pair.Key, actionPointInfo.searchAreaID)),
									ErrorText = ((PlayerActor x) => <InitSub>c__AnonStorey.$this.ErrorText(x, <InitSub>c__AnonStorey.pair.Key, actionPointInfo.searchAreaID)),
									CoolTimeFillRate = delegate
									{
										int registerID = <InitSub>c__AnonStorey.$this.RegisterID;
										Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
										AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
										if (!searchActionLockTable.TryGetValue(registerID, out searchActionInfo))
										{
											return 0f;
										}
										EnvironmentProfile environmentProfile = Singleton<Manager.Map>.Instance.EnvironmentProfile;
										if (searchActionInfo.Count < environmentProfile.SearchCount)
										{
											return 0f;
										}
										float num = searchActionInfo.ElapsedTime / environmentProfile.SearchCoolTimeDuration;
										return 1f - num;
									},
									Event = delegate
									{
										<InitSub>c__AnonStorey.pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, <InitSub>c__AnonStorey.$this);
									}
								});
							}
						}
					}
				}
			}
			this._labels = list.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list);
			for (int i = 0; i < 2; i++)
			{
				List<CommandLabel.CommandInfo> list2 = ListPool<CommandLabel.CommandInfo>.Get();
				using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator2 = ActionPoint.DateLabelTable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SearchActionPoint.<InitSub>c__AnonStorey2 <InitSub>c__AnonStorey3 = new SearchActionPoint.<InitSub>c__AnonStorey2();
						<InitSub>c__AnonStorey3.pair = enumerator2.Current;
						<InitSub>c__AnonStorey3.$this = this;
						if (this._playerDateEventType[i].Contains(<InitSub>c__AnonStorey3.pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple2;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(<InitSub>c__AnonStorey3.pair.Key, out valueTuple2))
							{
								List<DateActionPointInfo> list3;
								if (this._playerDateInfos.TryGetValue(i, out list3))
								{
									DateActionPointInfo actionPointInfo = list3.Find((DateActionPointInfo x) => x.eventTypeMask == <InitSub>c__AnonStorey3.pair.Key);
									string actionName2 = actionPointInfo.actionName;
									Sprite icon3;
									if (<InitSub>c__AnonStorey3.pair.Key == EventType.Search)
									{
										if (actionPointInfo.searchAreaID > -1)
										{
											this._tableID = actionPointInfo.searchAreaID;
										}
										if (actionPointInfo.gradeValue > -1 && actionPointInfo.gradeValue > this._grade)
										{
											this._grade = actionPointInfo.gradeValue;
										}
										Dictionary<int, int> dictionary2;
										if (Singleton<Manager.Resources>.Instance.itemIconTables.EquipmentIconTable.TryGetValue(this._tableID, out dictionary2))
										{
											int key2;
											if (dictionary2.TryGetValue(this._grade, out key2))
											{
												Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key2, out icon3);
											}
											else
											{
												icon3 = null;
											}
										}
										else
										{
											icon3 = null;
										}
									}
									else
									{
										Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo.iconID, out icon3);
									}
									GameObject gameObject2 = base.transform.FindLoop(actionPointInfo.labelNullName);
									Transform transform2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
									list2.Add(new CommandLabel.CommandInfo
									{
										Text = actionName2,
										Icon = icon3,
										IsHold = (<InitSub>c__AnonStorey3.pair.Key == EventType.Search),
										TargetSpriteInfo = icon.ActionSpriteInfo,
										Transform = transform2,
										Condition = ((PlayerActor x) => <InitSub>c__AnonStorey3.$this.CanAction(x, <InitSub>c__AnonStorey3.pair.Key, actionPointInfo.searchAreaID)),
										ErrorText = ((PlayerActor x) => <InitSub>c__AnonStorey3.$this.ErrorText(x, <InitSub>c__AnonStorey3.pair.Key, actionPointInfo.searchAreaID)),
										CoolTimeFillRate = delegate
										{
											int registerID = <InitSub>c__AnonStorey3.$this.RegisterID;
											Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
											AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
											if (!searchActionLockTable.TryGetValue(registerID, out searchActionInfo))
											{
												return 0f;
											}
											EnvironmentProfile environmentProfile = Singleton<Manager.Map>.Instance.EnvironmentProfile;
											if (searchActionInfo.Count < environmentProfile.SearchCount)
											{
												return 0f;
											}
											float num = searchActionInfo.ElapsedTime / environmentProfile.SearchCoolTimeDuration;
											return 1f - num;
										},
										Event = delegate
										{
											<InitSub>c__AnonStorey3.pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, <InitSub>c__AnonStorey3.$this);
										}
									});
								}
							}
						}
					}
				}
				this._dateLabels[i] = list2.ToArray();
				ListPool<CommandLabel.CommandInfo>.Release(list2);
			}
			List<CommandLabel.CommandInfo> list4 = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator3 = ActionPoint.SickLabelTable.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator3.Current;
					SearchActionPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						if (playerEventMask.Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple3;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple3))
							{
								list4.Add(new CommandLabel.CommandInfo
								{
									Text = pair.Value.Item2,
									Icon = null,
									IsHold = false,
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = base.transform,
									Event = delegate
									{
										pair.Value.Item3(Singleton<Manager.Map>.Instance.Player, $this);
									}
								});
							}
						}
					}
				}
			}
			this._sickLabels = list4.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list4);
			if (!this._playerInfos.IsNullOrEmpty<ActionPointInfo>())
			{
				if (this._playerInfos.Exists((ActionPointInfo x) => x.eventTypeMask == EventType.Search))
				{
					return;
				}
			}
			if (this._agentEventType.Contains(EventType.Search))
			{
				this._tableID = this._agentInfos.Find((ActionPointInfo x) => x.eventTypeMask == EventType.Search).searchAreaID;
			}
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x0027C940 File Offset: 0x0027AD40
		public bool CanSearch(EventType eventType, StuffItem itemInfo)
		{
			if (eventType != EventType.Search)
			{
				return true;
			}
			int num = itemInfo.ID;
			if (itemInfo.CategoryID == -1)
			{
				num = -1;
			}
			Dictionary<int, int> dictionary;
			if (!Singleton<Manager.Resources>.Instance.CommonDefine.SearchItemGradeTable.TryGetValue(0, out dictionary))
			{
				return false;
			}
			switch (this.TableID)
			{
			case 0:
			case 1:
			case 2:
			{
				int num2;
				if (num == -1)
				{
					if (0 >= this.Grade)
					{
						return true;
					}
				}
				else if (dictionary.TryGetValue(num, out num2) && num2 >= this.Grade)
				{
					return true;
				}
				break;
			}
			default:
			{
				int num3;
				if (dictionary.TryGetValue(num, out num3) && num3 >= this.Grade)
				{
					return true;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x0027CA08 File Offset: 0x0027AE08
		private bool CanAction(PlayerActor player, EventType eventType, int searchAreaID)
		{
			int registerID = this.RegisterID;
			Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
			AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
			if (!searchActionLockTable.TryGetValue(registerID, out searchActionInfo))
			{
				AIProject.SaveData.Environment.SearchActionInfo searchActionInfo2 = new AIProject.SaveData.Environment.SearchActionInfo();
				searchActionLockTable[registerID] = searchActionInfo2;
				searchActionInfo = searchActionInfo2;
			}
			if (searchActionInfo.Count >= Singleton<Manager.Map>.Instance.EnvironmentProfile.SearchCount)
			{
				return false;
			}
			if (player.PlayerData.ItemList.Count >= player.PlayerData.InventorySlotMax)
			{
				return false;
			}
			StuffItem itemInfo = player.PlayerData.EquipedSearchItem(searchAreaID);
			return this.CanSearch(eventType, itemInfo);
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x0027CAA0 File Offset: 0x0027AEA0
		private string ErrorText(PlayerActor player, EventType eventType, int searchAreaID)
		{
			int registerID = this.RegisterID;
			Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
			AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
			if (searchActionLockTable.TryGetValue(registerID, out searchActionInfo))
			{
				EnvironmentProfile environmentProfile = Singleton<Manager.Map>.Instance.EnvironmentProfile;
				if (searchActionInfo.Count >= environmentProfile.SearchCount)
				{
					return "しばらく採取できません";
				}
			}
			if (player.PlayerData.ItemList.Count >= player.PlayerData.InventorySlotMax)
			{
				return "ポーチがいっぱいです";
			}
			StuffItem itemInfo = player.PlayerData.EquipedSearchItem(searchAreaID);
			if (!this.CanSearch(eventType, itemInfo))
			{
				return "装備のランクが足りません";
			}
			return string.Empty;
		}

		// Token: 0x0400558F RID: 21903
		[SerializeField]
		private int _tableID = -1;

		// Token: 0x04005590 RID: 21904
		[SerializeField]
		private int _grade;
	}
}
