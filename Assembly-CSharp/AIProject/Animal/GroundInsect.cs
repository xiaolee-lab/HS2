using System;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BA4 RID: 2980
	[RequireComponent(typeof(NavMeshController))]
	public abstract class GroundInsect : AnimalBase
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x00264FB6 File Offset: 0x002633B6
		public NavMeshController NavMeshCon
		{
			[CompilerGenerated]
			get
			{
				return this._navMeshCon;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600590F RID: 22799 RVA: 0x00264FBE File Offset: 0x002633BE
		public override bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return true;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x00264FC1 File Offset: 0x002633C1
		public override CommandLabel.CommandInfo[] Labels
		{
			[CompilerGenerated]
			get
			{
				return this.getLabels;
			}
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x00264FCC File Offset: 0x002633CC
		protected override void InitializeCommandLabels()
		{
			if (this.getLabels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				CommonDefine commonDefine = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.CommonDefine;
				CommonDefine.CommonIconGroup commonIconGroup = (!(commonDefine != null)) ? null : commonDefine.Icon;
				this.getLabels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = "捕まえる",
						Transform = base.transform,
						IsHold = false,
						Icon = null,
						TargetSpriteInfo = ((commonIconGroup != null) ? commonIconGroup.CharaSpriteInfo : null),
						Event = delegate
						{
							StuffItemInfo itemInfo = base.ItemInfo;
							if (itemInfo != null)
							{
								StuffItem stuffItem = new StuffItem(itemInfo.CategoryID, itemInfo.ID, 1);
								PlayerActor player = Singleton<Map>.Instance.Player;
								player.PlayerData.ItemList.AddItem(stuffItem);
								MapUIContainer.AddSystemItemLog(itemInfo, stuffItem.Count, true);
							}
							else
							{
								MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
							}
							this.Destroy();
						}
					}
				};
			}
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x00265084 File Offset: 0x00263484
		protected override void Awake()
		{
			if (this._navMeshCon == null)
			{
				this._navMeshCon = base.GetComponent<NavMeshController>();
			}
			if (this._navMeshCon == null)
			{
				this._navMeshCon = base.GetComponentInChildren<NavMeshController>(true);
			}
			if (this._navMeshCon == null)
			{
				this.Destroy();
				return;
			}
			base.Awake();
		}

		// Token: 0x04005191 RID: 20881
		[SerializeField]
		[Tooltip("ナビメッシュエージェント管理クラス")]
		private NavMeshController _navMeshCon;

		// Token: 0x04005192 RID: 20882
		protected AnimalRootMotion rootMotion;

		// Token: 0x04005193 RID: 20883
		protected CommandLabel.CommandInfo[] getLabels;
	}
}
