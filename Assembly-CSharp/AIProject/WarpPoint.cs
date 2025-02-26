using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Player;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C34 RID: 3124
	public class WarpPoint : ActionPoint
	{
		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x0028A441 File Offset: 0x00288841
		public override CommandLabel.CommandInfo[] Labels
		{
			get
			{
				if (Singleton<Manager.Map>.Instance.Player.PlayerController.State is Onbu)
				{
					return this._sickLabels;
				}
				return this._labels;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x0028A46E File Offset: 0x0028886E
		public override bool IsNeutralCommand
		{
			get
			{
				return base.IsNeutralCommand && base.isActiveAndEnabled;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x0028A48B File Offset: 0x0028888B
		public int TableID
		{
			[CompilerGenerated]
			get
			{
				return this._tableID;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x060060C5 RID: 24773 RVA: 0x0028A493 File Offset: 0x00288893
		public Renderer[] Renderers
		{
			[CompilerGenerated]
			get
			{
				return this._renderers;
			}
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0028A49C File Offset: 0x0028889C
		protected override void InitSub()
		{
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			EventType playerEventMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.PlayerEventMask;
			List<CommandLabel.CommandInfo> list = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator = ActionPoint.LabelTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator.Current;
					WarpPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						if (playerEventMask.Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple))
							{
								ActionPointInfo actionPointInfo = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
								string actionName = actionPointInfo.actionName;
								Sprite icon2;
								Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo.iconID, out icon2);
								this._tableID = actionPointInfo.searchAreaID;
								GameObject gameObject = base.transform.FindLoop(actionPointInfo.labelNullName);
								Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? base.transform;
								list.Add(new CommandLabel.CommandInfo
								{
									Text = actionName,
									Icon = icon2,
									IsHold = true,
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = transform,
									Condition = ((PlayerActor x) => $this.CanAccess()),
									ErrorText = ((PlayerActor x) => $this.ErrorText()),
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
			this._labels = list.ToArray();
			ListPool<CommandLabel.CommandInfo>.Release(list);
			for (int i = 0; i < 2; i++)
			{
				List<CommandLabel.CommandInfo> list2 = ListPool<CommandLabel.CommandInfo>.Get();
				using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator2 = ActionPoint.DateLabelTable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator2.Current;
						WarpPoint $this = this;
						if (this._playerDateEventType[i].Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple2;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple2))
							{
								List<DateActionPointInfo> list3;
								if (this._playerDateInfos.TryGetValue(i, out list3))
								{
									DateActionPointInfo dateActionPointInfo = list3.Find((DateActionPointInfo x) => x.eventTypeMask == pair.Key);
									string actionName2 = dateActionPointInfo.actionName;
									Sprite icon3;
									Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(dateActionPointInfo.iconID, out icon3);
									GameObject gameObject2 = base.transform.FindLoop(dateActionPointInfo.labelNullName);
									Transform transform2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
									list2.Add(new CommandLabel.CommandInfo
									{
										Text = actionName2,
										Icon = icon3,
										IsHold = true,
										TargetSpriteInfo = icon.ActionSpriteInfo,
										Transform = transform2,
										Condition = ((PlayerActor x) => $this.CanAccess()),
										ErrorText = ((PlayerActor x) => $this.ErrorText()),
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
				this._dateLabels[i] = list2.ToArray();
				ListPool<CommandLabel.CommandInfo>.Release(list2);
			}
			List<CommandLabel.CommandInfo> list4 = ListPool<CommandLabel.CommandInfo>.Get();
			using (Dictionary<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>>.Enumerator enumerator3 = ActionPoint.SickLabelTable.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<EventType, Tuple<int, string, Action<PlayerActor, ActionPoint>>> pair = enumerator3.Current;
					WarpPoint $this = this;
					if (this._playerEventType.Contains(pair.Key))
					{
						if (playerEventMask.Contains(pair.Key))
						{
							UnityEx.ValueTuple<int, string> valueTuple3;
							if (AIProject.Definitions.Action.NameTable.TryGetValue(pair.Key, out valueTuple3))
							{
								ActionPointInfo actionPointInfo2 = this._playerInfos.Find((ActionPointInfo x) => x.eventTypeMask == pair.Key);
								string actionName3 = actionPointInfo2.actionName;
								Sprite icon4;
								Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(actionPointInfo2.iconID, out icon4);
								GameObject gameObject3 = base.transform.FindLoop(actionPointInfo2.labelNullName);
								Transform transform3 = ((gameObject3 != null) ? gameObject3.transform : null) ?? base.transform;
								list4.Add(new CommandLabel.CommandInfo
								{
									Text = actionName3,
									Icon = icon4,
									IsHold = true,
									TargetSpriteInfo = icon.ActionSpriteInfo,
									Transform = transform3,
									Condition = ((PlayerActor x) => $this.CanAccess()),
									ErrorText = ((PlayerActor x) => $this.ErrorText()),
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
				if (this._playerInfos.Exists((ActionPointInfo x) => x.eventTypeMask == EventType.Warp))
				{
					return;
				}
			}
			if (this._agentEventType.Contains(EventType.Warp))
			{
				this._tableID = this._agentInfos.Find((ActionPointInfo x) => x.eventTypeMask == EventType.Warp).searchAreaID;
			}
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0028AADC File Offset: 0x00288EDC
		public bool CanAccess()
		{
			MapArea ownerArea = base.OwnerArea;
			Dictionary<int, List<WarpPoint>> dictionary;
			List<WarpPoint> list;
			return !(ownerArea == null) && Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(ownerArea.ChunkID, out dictionary) && dictionary.TryGetValue(this._tableID, out list) && list.Count >= 2;
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x0028AB40 File Offset: 0x00288F40
		public string ErrorText()
		{
			MapArea ownerArea = base.OwnerArea;
			if (ownerArea == null)
			{
				return string.Empty;
			}
			Dictionary<int, List<WarpPoint>> dictionary;
			List<WarpPoint> list;
			if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(ownerArea.ChunkID, out dictionary) || !dictionary.TryGetValue(this._tableID, out list))
			{
				return "同じ色のワープ装置が無いと、移動できません";
			}
			if (list.Count < 2)
			{
				return "同じ色のワープ装置が無いと、移動できません";
			}
			return string.Empty;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0028ABB4 File Offset: 0x00288FB4
		public WarpPoint PairPoint()
		{
			MapArea ownerArea = base.OwnerArea;
			if (ownerArea == null)
			{
				return null;
			}
			Dictionary<int, List<WarpPoint>> dictionary;
			List<WarpPoint> list;
			if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(ownerArea.ChunkID, out dictionary) || !dictionary.TryGetValue(this._tableID, out list))
			{
				return null;
			}
			if (list.Count < 2)
			{
				return null;
			}
			WarpPoint result = null;
			foreach (WarpPoint warpPoint in list)
			{
				if (!(warpPoint == this))
				{
					if (!(warpPoint == null))
					{
						result = warpPoint;
					}
				}
			}
			return result;
		}

		// Token: 0x040055CF RID: 21967
		[SerializeField]
		private int _tableID = -1;

		// Token: 0x040055D0 RID: 21968
		[SerializeField]
		private Renderer[] _renderers;
	}
}
