using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C32 RID: 3122
	public class TutorialSearchActionPoint : ActionPoint
	{
		// Token: 0x060060B2 RID: 24754 RVA: 0x00289DB8 File Offset: 0x002881B8
		protected override void OnEnable()
		{
			base.OnEnable();
			PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			CommandArea commandArea;
			if (playerActor == null)
			{
				commandArea = null;
			}
			else
			{
				PlayerController playerController = playerActor.PlayerController;
				commandArea = ((playerController != null) ? playerController.CommandArea : null);
			}
			CommandArea commandArea2 = commandArea;
			if (commandArea2 == null)
			{
				return;
			}
			if (this._labels == null)
			{
				base.Init();
			}
			if (!commandArea2.ContainsCommandableObject(this))
			{
				commandArea2.AddCommandableObject(this);
			}
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x00289E38 File Offset: 0x00288238
		protected override void OnDisable()
		{
			base.OnDisable();
			PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			CommandArea commandArea;
			if (playerActor == null)
			{
				commandArea = null;
			}
			else
			{
				PlayerController playerController = playerActor.PlayerController;
				commandArea = ((playerController != null) ? playerController.CommandArea : null);
			}
			CommandArea commandArea2 = commandArea;
			if (commandArea2 == null)
			{
				return;
			}
			bool flag = commandArea2.ContainsConsiderationObject(this);
			if (commandArea2.ContainsCommandableObject(this))
			{
				commandArea2.RemoveCommandableObject(this);
				if (flag)
				{
					commandArea2.RefreshCommands();
				}
			}
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x00289EB9 File Offset: 0x002882B9
		private void Awake()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance.MinimapUI.MinimapTutorialActionPointInit(this, this._iconID);
			}
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x00289EDB File Offset: 0x002882DB
		private void OnDestroy()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance.MinimapUI.MinimapTutorialActionPointDestroy(this);
			}
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x00289EF8 File Offset: 0x002882F8
		protected override void InitSub()
		{
			Tuple<int, string, Action<PlayerActor, ActionPoint>> pair;
			if (!ActionPoint.LabelTable.TryGetValue(EventType.Search, out pair))
			{
				return;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			CommonDefine.CommonIconGroup icon = instance.CommonDefine.Icon;
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			List<string> source;
			eventPointCommandLabelTextTable.TryGetValue(this._textID, out source);
			string text = source.GetElement(index) ?? string.Empty;
			Sprite icon2 = null;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(this._iconID, out icon2);
			GameObject gameObject = base.transform.FindLoop(this._labelNullName);
			Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? base.transform;
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
						Action<PlayerActor, ActionPoint> item = pair.Item3;
						if (item != null)
						{
							item(Singleton<Map>.Instance.Player, this);
						}
					}
				}
			};
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x0028A03C File Offset: 0x0028843C
		public ActionPointInfo GetPlayerActionPointInfo()
		{
			return new ActionPointInfo
			{
				poseID = this._animPoseID,
				baseNullName = this._baseNullName,
				recoveryNullName = this._recoveryNullName
			};
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x0028A07C File Offset: 0x0028847C
		public Actor.SearchInfo GetSearchInfo()
		{
			if (this._searchInfo == null && Singleton<Manager.Resources>.IsInstance())
			{
				CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
				Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
				ItemIDKeyPair driftwoodID = commonDefine.ItemIDDefine.DriftwoodID;
				StuffItemInfo item = gameInfo.GetItem(driftwoodID.categoryID, driftwoodID.itemID);
				if (item == null)
				{
					return null;
				}
				this._searchInfo = new Actor.SearchInfo
				{
					IsSuccess = true,
					ItemList = new List<Actor.ItemSearchInfo>
					{
						new Actor.ItemSearchInfo
						{
							categoryID = item.CategoryID,
							id = item.ID,
							name = item.Name,
							count = 1
						}
					}
				};
			}
			return this._searchInfo;
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x0028A14C File Offset: 0x0028854C
		public override bool TutorialHideMode()
		{
			int tutorialProgress = Map.GetTutorialProgress();
			return tutorialProgress != 3;
		}

		// Token: 0x040055C3 RID: 21955
		[SerializeField]
		private int _animPoseID;

		// Token: 0x040055C4 RID: 21956
		[SerializeField]
		private int _iconID;

		// Token: 0x040055C5 RID: 21957
		[SerializeField]
		private string _labelNullName = string.Empty;

		// Token: 0x040055C6 RID: 21958
		[SerializeField]
		private string _baseNullName = string.Empty;

		// Token: 0x040055C7 RID: 21959
		[SerializeField]
		private string _recoveryNullName = string.Empty;

		// Token: 0x040055C8 RID: 21960
		[SerializeField]
		private string[] _textList = new string[0];

		// Token: 0x040055C9 RID: 21961
		[SerializeField]
		private int _textID = 14;

		// Token: 0x040055CA RID: 21962
		private Actor.SearchInfo _searchInfo;
	}
}
