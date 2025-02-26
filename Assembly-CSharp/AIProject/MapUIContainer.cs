using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Animal;
using AIProject.ColorDefine;
using AIProject.DebugUtil;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI;
using AIProject.UI.Recycling;
using Cinemachine;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000FE6 RID: 4070
	public class MapUIContainer : Singleton<MapUIContainer>
	{
		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06008860 RID: 34912 RVA: 0x0038D693 File Offset: 0x0038BA93
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06008861 RID: 34913 RVA: 0x0038D69B File Offset: 0x0038BA9B
		public static FadeCanvas FadeCanvas
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._fadeCanvas;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06008862 RID: 34914 RVA: 0x0038D6A7 File Offset: 0x0038BAA7
		public static NotifyMessageList NotifyPanel
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._notifyPanel;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06008863 RID: 34915 RVA: 0x0038D6B3 File Offset: 0x0038BAB3
		public MiniMapControler MinimapUI
		{
			[CompilerGenerated]
			get
			{
				return this._minimapUI;
			}
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06008864 RID: 34916 RVA: 0x0038D6BB File Offset: 0x0038BABB
		public static GameObject MiniMapRenderTex
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._MiniMapRenderTex;
			}
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06008865 RID: 34917 RVA: 0x0038D6C7 File Offset: 0x0038BAC7
		public static SystemMenuUI SystemMenuUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._systemMenuUI;
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06008866 RID: 34918 RVA: 0x0038D6D3 File Offset: 0x0038BAD3
		public static GameLog GameLogUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._gameLogUI;
			}
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06008867 RID: 34919 RVA: 0x0038D6DF File Offset: 0x0038BADF
		public static CommCommandList CommandList
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._commandList;
			}
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06008868 RID: 34920 RVA: 0x0038D6EB File Offset: 0x0038BAEB
		public static CommCommandList ChoiceUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._choiceUI;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06008869 RID: 34921 RVA: 0x0038D6F7 File Offset: 0x0038BAF7
		public static CommandLabel CommandLabel
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._commandLabel;
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x0600886A RID: 34922 RVA: 0x0038D703 File Offset: 0x0038BB03
		public static ClosetUI DressRoomUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._dressRoomUI;
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x0600886B RID: 34923 RVA: 0x0038D70F File Offset: 0x0038BB0F
		public static ClosetUI ClosetUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._closetUI;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x0600886C RID: 34924 RVA: 0x0038D71B File Offset: 0x0038BB1B
		public static CharaEntryUI CharaEntryUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._charaEntryUI;
			}
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x0600886D RID: 34925 RVA: 0x0038D727 File Offset: 0x0038BB27
		public static CharaChangeUI CharaChangeUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._charaChangeUI;
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x0600886E RID: 34926 RVA: 0x0038D733 File Offset: 0x0038BB33
		public static PlayerChangeUI PlayerChangeUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._playerChangeUI;
			}
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x0600886F RID: 34927 RVA: 0x0038D73F File Offset: 0x0038BB3F
		public static CharaLookEditUI CharaLookEditUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._charaLookEditUI;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06008870 RID: 34928 RVA: 0x0038D74B File Offset: 0x0038BB4B
		public static PlayerLookEditUI PlayerLookEditUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._playerLookEditUI;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06008871 RID: 34929 RVA: 0x0038D757 File Offset: 0x0038BB57
		public static CharaMigrateUI CharaMigrateUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._charaMigrateUI;
			}
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x06008872 RID: 34930 RVA: 0x0038D763 File Offset: 0x0038BB63
		public static ItemBoxUI ItemBoxUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._itemBoxUI;
			}
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06008873 RID: 34931 RVA: 0x0038D76F File Offset: 0x0038BB6F
		public static FishingUI FishingUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._fishingUI;
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06008874 RID: 34932 RVA: 0x0038D77B File Offset: 0x0038BB7B
		public static ResultMessageUI ResultMessageUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._resultMessageUI;
			}
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06008875 RID: 34933 RVA: 0x0038D787 File Offset: 0x0038BB87
		public static WarningMessageUI WarningMessageUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._warningMessageUI;
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06008876 RID: 34934 RVA: 0x0038D793 File Offset: 0x0038BB93
		public static StorySupportUI StorySupportUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._storySupportUI;
			}
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06008877 RID: 34935 RVA: 0x0038D79F File Offset: 0x0038BB9F
		public static RequestUI RequestUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._requestUI;
			}
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06008878 RID: 34936 RVA: 0x0038D7AB File Offset: 0x0038BBAB
		public static PhotoShotUI PhotoShotUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._photoShotUI;
			}
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06008879 RID: 34937 RVA: 0x0038D7B7 File Offset: 0x0038BBB7
		public static TutorialUI TutorialUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._tutorialUI;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x0600887A RID: 34938 RVA: 0x0038D7C3 File Offset: 0x0038BBC3
		public static EventDialogUI EventDialogUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._eventDialogUI;
			}
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x0600887B RID: 34939 RVA: 0x0038D7CF File Offset: 0x0038BBCF
		public static AllAreaMapUI AllAreaMapUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._AllAreaMapUI;
			}
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x0600887C RID: 34940 RVA: 0x0038D7DB File Offset: 0x0038BBDB
		public static ShopUI ShopUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._ShopUI;
			}
		}

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x0600887D RID: 34941 RVA: 0x0038D7E7 File Offset: 0x0038BBE7
		public static ScroungeUI ScroungeUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._ScroungeUI;
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x0600887E RID: 34942 RVA: 0x0038D7F3 File Offset: 0x0038BBF3
		public static RefrigeratorUI RefrigeratorUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._RefrigeratorUI;
			}
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x0600887F RID: 34943 RVA: 0x0038D7FF File Offset: 0x0038BBFF
		public static CraftUI CraftUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._CraftUI;
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06008880 RID: 34944 RVA: 0x0038D80B File Offset: 0x0038BC0B
		public static CraftUI CookingUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._CookingUI;
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06008881 RID: 34945 RVA: 0x0038D817 File Offset: 0x0038BC17
		public static CraftUI PetCraftUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._PetCraftUI;
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06008882 RID: 34946 RVA: 0x0038D823 File Offset: 0x0038BC23
		public static CraftUI MedicineCraftUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._MedicineCraftUI;
			}
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06008883 RID: 34947 RVA: 0x0038D82F File Offset: 0x0038BC2F
		public static FarmlandUI FarmlandUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._FarmlandUI;
			}
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06008884 RID: 34948 RVA: 0x0038D83B File Offset: 0x0038BC3B
		public static ChickenCoopUI ChickenCoopUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._ChickenCoopUI;
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06008885 RID: 34949 RVA: 0x0038D847 File Offset: 0x0038BC47
		public static PetHomeUI PetHomeUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._petHomeUI;
			}
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06008886 RID: 34950 RVA: 0x0038D853 File Offset: 0x0038BC53
		public static JukeBoxUI JukeBoxUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._jukeBoxUI;
			}
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06008887 RID: 34951 RVA: 0x0038D85F File Offset: 0x0038BC5F
		public static SpendMoneyUI SpendMoneyUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._spendMoneyUI;
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06008888 RID: 34952 RVA: 0x0038D86B File Offset: 0x0038BC6B
		public static AnimalNicknameOutput NicknameUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._nicknameUI;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06008889 RID: 34953 RVA: 0x0038D877 File Offset: 0x0038BC77
		public static RecyclingUI RecyclingUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<MapUIContainer>.Instance._recyclingUI;
			}
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x0038D883 File Offset: 0x0038BC83
		public void OpenADV(OpenData openData, IPack pack)
		{
			this._advData.openData = openData;
			this._advData.pack = pack;
			this._advScene.gameObject.SetActive(true);
		}

		// Token: 0x0600888B RID: 34955 RVA: 0x0038D8AE File Offset: 0x0038BCAE
		public void CloseADV()
		{
			this._advScene.Scenario.captions.EndADV(delegate
			{
				this._advScene.gameObject.SetActive(false);
			});
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x0600888C RID: 34956 RVA: 0x0038D8D1 File Offset: 0x0038BCD1
		public ADVScene advScene
		{
			[CompilerGenerated]
			get
			{
				return this._advScene;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x0600888D RID: 34957 RVA: 0x0038D8D9 File Offset: 0x0038BCD9
		public ADVData advData
		{
			[CompilerGenerated]
			get
			{
				return this._advData;
			}
		}

		// Token: 0x0600888E RID: 34958 RVA: 0x0038D8E4 File Offset: 0x0038BCE4
		private void Reset()
		{
			this._systemMenuUI = base.GetComponentInChildren<SystemMenuUI>();
			this._commandLabel = base.GetComponentInChildren<CommandLabel>();
			this._notifyPanel = base.GetComponentInChildren<NotifyMessageList>();
			this._itemBoxUI = base.GetComponentInChildren<ItemBoxUI>();
			this._AllAreaMapUI = base.GetComponentInChildren<AllAreaMapUI>();
			this._ShopUI = base.GetComponentInChildren<ShopUI>();
			this._ScroungeUI = base.GetComponentInChildren<ScroungeUI>();
			this._RefrigeratorUI = base.GetComponentInChildren<RefrigeratorUI>();
			CraftUI[] componentsInChildren = base.GetComponentsInChildren<CraftUI>();
			foreach (CraftUI craftUI in componentsInChildren)
			{
				if (craftUI.name.StartsWith("CraftUI"))
				{
					this._CraftUI = craftUI;
				}
				else if (craftUI.name.StartsWith("CookingUI"))
				{
					this._CookingUI = craftUI;
				}
				else if (craftUI.name.StartsWith("PetCraftUI"))
				{
					this._PetCraftUI = craftUI;
				}
				else if (craftUI.name.StartsWith("MedicineCraftUI"))
				{
					this._MedicineCraftUI = craftUI;
				}
			}
			this._FarmlandUI = base.GetComponentInChildren<FarmlandUI>();
			this._ChickenCoopUI = base.GetComponentInChildren<ChickenCoopUI>();
			if (this._MiniMapRenderTex == null)
			{
				this._MiniMapRenderTex = this._minimapCanvasGroup.GetComponentInChildren<RawImage>(true).gameObject;
			}
		}

		// Token: 0x0600888F RID: 34959 RVA: 0x0038DA31 File Offset: 0x0038BE31
		public static void SwitchSystemMenu()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._systemMenuUI.IsActiveControl = !Singleton<MapUIContainer>.Instance._systemMenuUI.IsActiveControl;
			}
		}

		// Token: 0x06008890 RID: 34960 RVA: 0x0038DA60 File Offset: 0x0038BE60
		public static void InitializeMinimap()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				if (Singleton<MapUIContainer>.Instance._MiniMapRenderTex == null)
				{
					Singleton<MapUIContainer>.Instance._MiniMapRenderTex = Singleton<MapUIContainer>.Instance._minimapCanvasGroup.GetComponentInChildren<RawImage>(true).gameObject;
				}
				Singleton<MapUIContainer>.Instance.MinimapUI.Init(-1);
			}
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x0038DABC File Offset: 0x0038BEBC
		public static void SetVisibleHUD(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				float from = Singleton<MapUIContainer>.Instance._minimapCanvasGroup.alpha;
				float fromStoryUIAlpha = Singleton<MapUIContainer>.Instance._storySupportUI.CanvasAlpha;
				float fromPointerAlpha = Singleton<MapUIContainer>.Instance._centerPointerCanvasGroup.alpha;
				float duration = 0.3f;
				int to = (!active) ? 0 : 1;
				IDisposable hudActivateSubscriber = Singleton<MapUIContainer>.Instance._hudActivateSubscriber;
				if (hudActivateSubscriber != null)
				{
					hudActivateSubscriber.Dispose();
				}
				Singleton<MapUIContainer>.Instance._hudActivateSubscriber = ObservableEasing.EaseOutQuint(duration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					Singleton<MapUIContainer>.Instance._minimapCanvasGroup.alpha = Mathf.Lerp(from, (float)to, x.Value);
					Singleton<MapUIContainer>.Instance._centerPointerCanvasGroup.alpha = Mathf.Lerp(fromPointerAlpha, (float)to, x.Value);
				}, delegate(Exception ex)
				{
				});
				IDisposable storySupprtUIActivateSubscriber = Singleton<MapUIContainer>.Instance._storySupprtUIActivateSubscriber;
				if (storySupprtUIActivateSubscriber != null)
				{
					storySupprtUIActivateSubscriber.Dispose();
				}
				Singleton<MapUIContainer>.Instance._storySupprtUIActivateSubscriber = ObservableEasing.EaseOutQuint(duration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					Singleton<MapUIContainer>.Instance._storySupportUI.CanvasAlpha = Mathf.Lerp(fromStoryUIAlpha, (float)to, x.Value);
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x0038DBEC File Offset: 0x0038BFEC
		public static void SetVisibleHUDExceptStoryUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				float fromMiniMapUIAlpha = Singleton<MapUIContainer>.Instance._minimapCanvasGroup.alpha;
				float fromPointerAlpha = Singleton<MapUIContainer>.Instance._centerPointerCanvasGroup.alpha;
				float to = (!active) ? 0f : 1f;
				IDisposable hudActivateSubscriber = Singleton<MapUIContainer>.Instance._hudActivateSubscriber;
				if (hudActivateSubscriber != null)
				{
					hudActivateSubscriber.Dispose();
				}
				Singleton<MapUIContainer>.Instance._hudActivateSubscriber = ObservableEasing.EaseOutQuint(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					Singleton<MapUIContainer>.Instance._minimapCanvasGroup.alpha = Mathf.Lerp(fromMiniMapUIAlpha, to, x.Value);
					Singleton<MapUIContainer>.Instance._centerPointerCanvasGroup.alpha = Mathf.Lerp(fromPointerAlpha, to, x.Value);
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x0038DCB0 File Offset: 0x0038C0B0
		public static void ChangeActiveCanvas()
		{
			bool flag = Singleton<MapUIContainer>.Instance._activeCanvas = !Singleton<MapUIContainer>.Instance._activeCanvas;
			Singleton<MapUIContainer>.Instance._canvasGroup.alpha = (float)((!flag) ? 0 : 1);
		}

		// Token: 0x06008894 RID: 34964 RVA: 0x0038DCF5 File Offset: 0x0038C0F5
		public static void AddNotify(string message)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._notifyPanel.AddMessage(message);
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06008895 RID: 34965 RVA: 0x0038DD11 File Offset: 0x0038C111
		public static string InfoName
		{
			[CompilerGenerated]
			get
			{
				return "system";
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06008896 RID: 34966 RVA: 0x0038DD18 File Offset: 0x0038C118
		public static string ItemGetText
		{
			[CompilerGenerated]
			get
			{
				return " を入手しました。";
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06008897 RID: 34967 RVA: 0x0038DD1F File Offset: 0x0038C11F
		public static string ItemGetEmptyText
		{
			[CompilerGenerated]
			get
			{
				return "何も入手できませんでした。";
			}
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x0038DD26 File Offset: 0x0038C126
		public static string ToHtmlStringRGB(Color color)
		{
			return string.Format("#{0}", ColorUtility.ToHtmlStringRGB(color));
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x0038DD38 File Offset: 0x0038C138
		public static string CharaNameColor(Actor actor)
		{
			if (actor == null || actor.CharaName.IsNullOrEmpty())
			{
				return string.Empty;
			}
			Color? actorColor = MapUIContainer.GetActorColor(actor.ID);
			return (actorColor != null) ? actor.CharaName.Coloring(MapUIContainer.ToHtmlStringRGB(actorColor.Value)) : actor.CharaName;
		}

		// Token: 0x0600889A RID: 34970 RVA: 0x0038DDA4 File Offset: 0x0038C1A4
		public static Color? GetActorColor(int actorID)
		{
			Color? result = null;
			switch (actorID)
			{
			case 0:
				result = new Color?(new Color32(233, 67, 33, byte.MaxValue));
				break;
			case 1:
				result = new Color?(Define.Get(Colors.Blue));
				break;
			case 2:
				result = new Color?(new Color32(250, 239, 0, byte.MaxValue));
				break;
			case 3:
				result = new Color?(new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue));
				break;
			default:
				if (actorID != -99)
				{
					if (actorID == -90)
					{
						result = new Color?(Define.Get(Colors.Green));
					}
				}
				else
				{
					result = new Color?(Define.Get(Colors.LightGreen));
				}
				break;
			}
			return result;
		}

		// Token: 0x0600889B RID: 34971 RVA: 0x0038DE8C File Offset: 0x0038C28C
		public static string GetItemColor(StuffItemInfo info)
		{
			if (info == null)
			{
				return string.Empty;
			}
			Color color;
			switch (info.Grade)
			{
			case Grade._1:
				color = Define.Get(Colors.White);
				break;
			case Grade._2:
				color = Define.Get(Colors.Blue);
				break;
			case Grade._3:
				color = Define.Get(Colors.Orange);
				break;
			default:
				color = Define.Get(Colors.White);
				break;
			}
			return info.Name.Coloring(MapUIContainer.ToHtmlStringRGB(color));
		}

		// Token: 0x0600889C RID: 34972 RVA: 0x0038DF05 File Offset: 0x0038C305
		private static string GetItemMessage(StuffItemInfo info, int count)
		{
			return string.Format("{0} x {1}{2}", MapUIContainer.GetItemColor(info), count, MapUIContainer.ItemGetText);
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x0038DF22 File Offset: 0x0038C322
		public static void AddSystemItemLog(StuffItemInfo info, int count, bool isNotify)
		{
			MapUIContainer.AddSystemLog(MapUIContainer.GetItemMessage(info, count), isNotify);
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x0038DF31 File Offset: 0x0038C331
		public static void AddSystemLog(string message, bool isNotify)
		{
			MapUIContainer.AddLog(MapUIContainer.InfoName.Coloring(MapUIContainer.ToHtmlStringRGB(Define.Get(Colors.Green))), message, null);
			if (isNotify)
			{
				MapUIContainer.AddNotify(message);
			}
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x0038DF5C File Offset: 0x0038C35C
		public static void AddItemLog(Actor actor, StuffItemInfo info, int count, bool isNotify)
		{
			string itemMessage = MapUIContainer.GetItemMessage(info, count);
			MapUIContainer.AddLog(actor, itemMessage, null);
			if (isNotify)
			{
				MapUIContainer.AddNotify(itemMessage);
			}
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x0038DF85 File Offset: 0x0038C385
		public static void AddLog(Actor actor, string message, IReadOnlyCollection<TextScenario.IVoice[]> voices = null)
		{
			MapUIContainer.AddLog(MapUIContainer.CharaNameColor(actor), message, voices);
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x0038DF94 File Offset: 0x0038C394
		public static void AddLog(string name, string message, IReadOnlyCollection<TextScenario.IVoice[]> voices = null)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				if (!name.IsNullOrEmpty())
				{
					name += " :";
				}
				Singleton<MapUIContainer>.Instance._gameLogUI.AddLog(name, message, voices);
			}
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x0038DFCA File Offset: 0x0038C3CA
		public static void PushResultMessage(string message)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._resultMessageUI.ShowMessage(message);
			}
		}

		// Token: 0x060088A3 RID: 34979 RVA: 0x0038DFE6 File Offset: 0x0038C3E6
		public static void PushWarningMessage(Popup.Warning.Type type)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._warningMessageUI.ShowMessage(type);
			}
		}

		// Token: 0x060088A4 RID: 34980 RVA: 0x0038E002 File Offset: 0x0038C402
		public static void PushMessageUI(Popup.Warning.Type type, int colorID, int poseID, System.Action onComplete = null)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._warningMessageUI.ShowMessage(type, colorID, poseID, onComplete);
			}
		}

		// Token: 0x060088A5 RID: 34981 RVA: 0x0038E021 File Offset: 0x0038C421
		public static void PushMessageUI(string mes, int colorID, int posID, System.Action onComplete = null)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._warningMessageUI.ShowMessage(mes, colorID, posID, onComplete);
			}
		}

		// Token: 0x060088A6 RID: 34982 RVA: 0x0038E040 File Offset: 0x0038C440
		public static void OpenRequestUI(Popup.Request.Type type)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._requestUI.Open(type);
			}
		}

		// Token: 0x060088A7 RID: 34983 RVA: 0x0038E05C File Offset: 0x0038C45C
		public static void CloseRequestUI()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._requestUI.IsActiveControl = false;
			}
		}

		// Token: 0x060088A8 RID: 34984 RVA: 0x0038E078 File Offset: 0x0038C478
		public static void OpenStorySupportUI(Popup.StorySupport.Type type)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._storySupportUI.Open(type);
			}
		}

		// Token: 0x060088A9 RID: 34985 RVA: 0x0038E094 File Offset: 0x0038C494
		public static void OpenStorySupportUI(int id)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._storySupportUI.Open(id);
			}
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x0038E0B0 File Offset: 0x0038C4B0
		public static void CloseStorySupportUI()
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._storySupportUI.Close();
			}
		}

		// Token: 0x060088AB RID: 34987 RVA: 0x0038E0CB File Offset: 0x0038C4CB
		public static void SetActiveSystemMenuUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._systemMenuUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088AC RID: 34988 RVA: 0x0038E0E7 File Offset: 0x0038C4E7
		public static void SetActiveCommandList(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._commandList.IsActiveControl = active;
			}
		}

		// Token: 0x060088AD RID: 34989 RVA: 0x0038E103 File Offset: 0x0038C503
		public static void SetActiveCommandList(bool active, string title)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._commandList.Label.text = title;
				Singleton<MapUIContainer>.Instance._commandList.IsActiveControl = active;
			}
		}

		// Token: 0x060088AE RID: 34990 RVA: 0x0038E134 File Offset: 0x0038C534
		public static void SetActiveChoiceUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._choiceUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088AF RID: 34991 RVA: 0x0038E150 File Offset: 0x0038C550
		public static void SetActiveChoiceUI(bool active, string title = "")
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._choiceUI.Label.text = title;
				Singleton<MapUIContainer>.Instance._choiceUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B0 RID: 34992 RVA: 0x0038E181 File Offset: 0x0038C581
		public static void SetActiveItemBoxUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._itemBoxUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B1 RID: 34993 RVA: 0x0038E19D File Offset: 0x0038C59D
		public static void SetActiveDressRoomUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._dressRoomUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x0038E1B9 File Offset: 0x0038C5B9
		public static void SetActiveClosetUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._closetUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x0038E1D5 File Offset: 0x0038C5D5
		public static void SetActiveCharaEntryUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._charaEntryUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B4 RID: 34996 RVA: 0x0038E1F1 File Offset: 0x0038C5F1
		public static void SetActiveCharaChangeUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._charaChangeUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x0038E20D File Offset: 0x0038C60D
		public static void SetActivePlayerChangeUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._playerChangeUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x0038E229 File Offset: 0x0038C629
		public static void SetActiveCharaLookEditUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._charaLookEditUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x0038E245 File Offset: 0x0038C645
		public static void SetActivePlayerLookEditUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._playerLookEditUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x0038E261 File Offset: 0x0038C661
		public static void SetActiveCharaMigrationUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._charaMigrateUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088B9 RID: 35001 RVA: 0x0038E27D File Offset: 0x0038C67D
		public static void SetActiveFishingUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._fishingUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x0038E299 File Offset: 0x0038C699
		public static void SetActiveShopUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._ShopUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x0038E2B5 File Offset: 0x0038C6B5
		public static void SetActiveScroungeUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._ScroungeUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BC RID: 35004 RVA: 0x0038E2D1 File Offset: 0x0038C6D1
		public static void SetActiveRefrigeratorUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._RefrigeratorUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BD RID: 35005 RVA: 0x0038E2ED File Offset: 0x0038C6ED
		public static void SetActiveCraftUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._CraftUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BE RID: 35006 RVA: 0x0038E309 File Offset: 0x0038C709
		public static void SetActiveCookingUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._CookingUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088BF RID: 35007 RVA: 0x0038E325 File Offset: 0x0038C725
		public static void SetActivePetCraftUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._PetCraftUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088C0 RID: 35008 RVA: 0x0038E341 File Offset: 0x0038C741
		public static void SetActiveMedicineCraftUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._MedicineCraftUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088C1 RID: 35009 RVA: 0x0038E35D File Offset: 0x0038C75D
		public static void SetActiveFarmlandUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._FarmlandUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088C2 RID: 35010 RVA: 0x0038E379 File Offset: 0x0038C779
		public static void SetActiveChickenCoopUI(bool active, ChickenCoopUI.Mode mode)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._ChickenCoopUI.SetMode(mode);
				Singleton<MapUIContainer>.Instance._ChickenCoopUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088C3 RID: 35011 RVA: 0x0038E3A5 File Offset: 0x0038C7A5
		public static void SetActivePetHomeUI(bool active)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return;
			}
			Singleton<MapUIContainer>.Instance._petHomeUI.IsActiveControl = active;
		}

		// Token: 0x060088C4 RID: 35012 RVA: 0x0038E3C2 File Offset: 0x0038C7C2
		public static void SetActiveJukeBoxUI(bool active)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return;
			}
			Singleton<MapUIContainer>.Instance._jukeBoxUI.IsActiveControl = active;
		}

		// Token: 0x060088C5 RID: 35013 RVA: 0x0038E3DF File Offset: 0x0038C7DF
		public static void SetVisibledSpendMoneyUI(bool visibled)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return;
			}
			Singleton<MapUIContainer>.Instance._spendMoneyUI.Visibled = visibled;
		}

		// Token: 0x060088C6 RID: 35014 RVA: 0x0038E3FC File Offset: 0x0038C7FC
		public static void SetActiveRecyclingUI(bool active)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return;
			}
			Singleton<MapUIContainer>.Instance._recyclingUI.IsActiveControl = active;
		}

		// Token: 0x060088C7 RID: 35015 RVA: 0x0038E41C File Offset: 0x0038C81C
		public static void OpenTutorialUI(int id, bool groupDisplay = false)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return;
			}
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				Dictionary<int, bool> dictionary = (worldData == null) ? null : worldData.TutorialOpenStateTable;
				if (dictionary != null)
				{
					dictionary[id] = true;
				}
			}
			TutorialUI tutorialUI = Singleton<MapUIContainer>.Instance._tutorialUI;
			tutorialUI.SetCondition(id, groupDisplay);
			tutorialUI.IsActiveControl = true;
		}

		// Token: 0x060088C8 RID: 35016 RVA: 0x0038E484 File Offset: 0x0038C884
		public static bool OpenOnceTutorial(int id, bool groupDisplay = false)
		{
			if (!Singleton<MapUIContainer>.IsInstance())
			{
				return false;
			}
			TutorialUI tutorialUI = Singleton<MapUIContainer>.Instance._tutorialUI;
			if (tutorialUI == null || tutorialUI.IsActiveControl)
			{
				return false;
			}
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			if (worldData == null)
			{
				return false;
			}
			Dictionary<int, bool> tutorialOpenStateTable = worldData.TutorialOpenStateTable;
			if (tutorialOpenStateTable == null)
			{
				return false;
			}
			bool flag;
			if (!tutorialOpenStateTable.TryGetValue(id, out flag) || !flag)
			{
				tutorialOpenStateTable[id] = true;
				tutorialUI.SetCondition(id, groupDisplay);
				tutorialUI.IsActiveControl = true;
				return true;
			}
			return false;
		}

		// Token: 0x060088C9 RID: 35017 RVA: 0x0038E520 File Offset: 0x0038C920
		public static bool GetTutorialOpenState(int id)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			if (worldData == null)
			{
				return false;
			}
			Dictionary<int, bool> tutorialOpenStateTable = worldData.TutorialOpenStateTable;
			if (tutorialOpenStateTable == null)
			{
				return false;
			}
			bool result;
			if (!tutorialOpenStateTable.TryGetValue(id, out result))
			{
				result = (tutorialOpenStateTable[id] = false);
			}
			return result;
		}

		// Token: 0x060088CA RID: 35018 RVA: 0x0038E574 File Offset: 0x0038C974
		public static void SetActiveTutorialUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._tutorialUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088CB RID: 35019 RVA: 0x0038E590 File Offset: 0x0038C990
		public static IEnumerator DrawOnceTutorialUI(int tutorialID, ActorCameraControl camera = null)
		{
			if (!Singleton<MapUIContainer>.IsInstance() || MapUIContainer.GetTutorialOpenState(tutorialID))
			{
				yield break;
			}
			yield return null;
			if (camera != null)
			{
				CinemachineBrain brain = camera.CinemachineBrain;
				CrossFade fade = camera.CrossFade;
				for (;;)
				{
					CinemachineBrain cinemachineBrain = brain;
					bool? flag = (cinemachineBrain != null) ? new bool?(cinemachineBrain.IsBlending) : null;
					if ((flag == null || !flag.Value) && (!(fade != null) || fade.isEnd))
					{
						break;
					}
					yield return null;
				}
			}
			bool standby = MapUIContainer.OpenOnceTutorial(tutorialID, false);
			if (standby)
			{
				MapUIContainer.TutorialUI.ClosedEvent = delegate()
				{
					standby = false;
				};
				while (standby)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x060088CC RID: 35020 RVA: 0x0038E5B2 File Offset: 0x0038C9B2
		public static void SetActiveEventDialogUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._eventDialogUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088CD RID: 35021 RVA: 0x0038E5CE File Offset: 0x0038C9CE
		public static void SetActivePhotoShotUI(bool active)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._photoShotUI.IsActiveControl = active;
			}
		}

		// Token: 0x060088CE RID: 35022 RVA: 0x0038E5EA File Offset: 0x0038C9EA
		public static void SetSystemNotify(string message)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._systemMenuUI.SetNotify(message);
			}
		}

		// Token: 0x060088CF RID: 35023 RVA: 0x0038E606 File Offset: 0x0038CA06
		public static void SetInventoryStock(StuffItem presentingItem, Action<bool> onCompleted)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
			}
		}

		// Token: 0x060088D0 RID: 35024 RVA: 0x0038E612 File Offset: 0x0038CA12
		public static void RefreshCommands(int id, CommCommandList.CommandInfo[] options)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._commandList.ID = id;
				Singleton<MapUIContainer>.Instance._commandList.Options = options;
			}
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x0038E63E File Offset: 0x0038CA3E
		public static void RefreshChoices(int id, CommCommandList.CommandInfo[] options)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._choiceUI.ID = id;
				Singleton<MapUIContainer>.Instance._choiceUI.Options = options;
			}
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x0038E66A File Offset: 0x0038CA6A
		public static void SetCommandLabelAcception(CommandLabel.AcceptionState acception)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._commandLabel.Acception = acception;
			}
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x0038E686 File Offset: 0x0038CA86
		public static void ReserveSystemMenuMode(SystemMenuUI.MenuMode mode)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance._systemMenuUI.ReserveMove(mode);
			}
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x0038E6A2 File Offset: 0x0038CAA2
		public static IObservable<Unit> StartFade(FadeCanvas.PanelType panelType, FadeType fadeType, float duration, bool ignoreTimeScale)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				return Singleton<MapUIContainer>.Instance._fadeCanvas.StartFade(panelType, fadeType, duration, ignoreTimeScale);
			}
			return null;
		}

		// Token: 0x060088D5 RID: 35029 RVA: 0x0038E6C3 File Offset: 0x0038CAC3
		public static void SetMarkerTargetCameraTransform(Transform cameraTransform)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				Singleton<MapUIContainer>.Instance.GetComponentInChildren<MarkerOutput>().CameraTransform = cameraTransform;
			}
		}

		// Token: 0x060088D6 RID: 35030 RVA: 0x0038E6DF File Offset: 0x0038CADF
		private void InitiateDebugOutput()
		{
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x0038E6E4 File Offset: 0x0038CAE4
		private T LoadAsset<T>(string assetBundlePath, string assetName, string manifest = "") where T : UnityEngine.Object
		{
			T t = CommonLib.LoadAsset<T>(assetBundlePath, assetName, false, manifest);
			if (t == null)
			{
				return (T)((object)null);
			}
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == assetBundlePath && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(assetBundlePath, manifest));
			}
			return t;
		}

		// Token: 0x060088D8 RID: 35032 RVA: 0x0038E768 File Offset: 0x0038CB68
		public static bool AnyUIActive()
		{
			return Singleton<MapUIContainer>.IsInstance() && (Singleton<MapUIContainer>.Instance._commandList.IsActiveControl || Singleton<MapUIContainer>.Instance._choiceUI.IsActiveControl || Singleton<MapUIContainer>.Instance._itemBoxUI.IsActiveControl || Singleton<MapUIContainer>.Instance._charaEntryUI.IsActiveControl || Singleton<MapUIContainer>.Instance._charaChangeUI.IsActiveControl || Singleton<MapUIContainer>.Instance._playerChangeUI.IsActiveControl || Singleton<MapUIContainer>.Instance._charaLookEditUI.IsActiveControl || Singleton<MapUIContainer>.Instance._playerLookEditUI.IsActiveControl || Singleton<MapUIContainer>.Instance._charaMigrateUI.IsActiveControl || Singleton<MapUIContainer>.Instance._dressRoomUI.IsActiveControl || Singleton<MapUIContainer>.Instance._closetUI.IsActiveControl || Singleton<MapUIContainer>.Instance._RefrigeratorUI.IsActiveControl || Singleton<MapUIContainer>.Instance._CookingUI.IsActiveControl || Singleton<MapUIContainer>.Instance._MedicineCraftUI.IsActiveControl || Singleton<MapUIContainer>.Instance._PetCraftUI.IsActiveControl || Singleton<MapUIContainer>.Instance._FarmlandUI.IsActiveControl || Singleton<MapUIContainer>.Instance._ChickenCoopUI.IsActiveControl || Singleton<MapUIContainer>.Instance._petHomeUI.IsActiveControl || Singleton<MapUIContainer>.Instance._jukeBoxUI.IsActiveControl || Singleton<MapUIContainer>.Instance._systemMenuUI.IsActiveControl || Singleton<MapUIContainer>.Instance._requestUI.IsActiveControl || Singleton<MapUIContainer>.Instance._tutorialUI.IsActiveControl || Singleton<MapUIContainer>.Instance._eventDialogUI.IsActiveControl || Singleton<MapUIContainer>.Instance._recyclingUI.IsActiveControl || Singleton<ADV>.Instance.Captions.Active || Singleton<MapUIContainer>.Instance._minimapUI.AllAreaMap.activeSelf || Singleton<Manager.Resources>.Instance.HSceneTable.HSceneUISet.activeSelf || (Singleton<Housing>.IsInstance() && Singleton<Housing>.Instance.IsCraft) || (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.MapShortcutUI != null) || (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Config != null) || (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Dialog != null) || (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.ExitScene != null));
		}

		// Token: 0x060088D9 RID: 35033 RVA: 0x0038EA40 File Offset: 0x0038CE40
		private void Start()
		{
			DefinePack.AssetBundlePathsDefine abpaths = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths;
			DefinePack.AssetBundleManifestsDefine abmanifests = Singleton<Manager.Resources>.Instance.DefinePack.ABManifests;
			Transform transform = this._hudCanvas.transform;
			GameObject original = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "NotifyLog", abmanifests.Default);
			this._notifyPanel = UnityEngine.Object.Instantiate<GameObject>(original, transform, false).GetComponent<NotifyMessageList>();
			GameObject original2 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ResultMessageUI", abmanifests.Default);
			this._resultMessageUI = UnityEngine.Object.Instantiate<GameObject>(original2, transform, false).GetComponent<ResultMessageUI>();
			GameObject original3 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "StorySupportUI", abmanifests.Default);
			this._storySupportUI = UnityEngine.Object.Instantiate<GameObject>(original3, transform, false).GetComponent<StorySupportUI>();
			GameObject original4 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "WarningMessageUI", abmanifests.Default);
			this._warningMessageUI = UnityEngine.Object.Instantiate<GameObject>(original4, transform, false).GetComponent<WarningMessageUI>();
			GameObject original5 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CenterPointer", abmanifests.Default);
			this._centerPointerCanvasGroup = UnityEngine.Object.Instantiate<GameObject>(original5, transform, false).GetComponent<CanvasGroup>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._MiniMapObject);
			this._minimapUI = gameObject.GetComponentInChildren<MiniMapControler>();
			GameObject original6 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "MiniMapUI", abmanifests.Default);
			this._minimapCanvasGroup = UnityEngine.Object.Instantiate<GameObject>(original6, transform, false).GetComponent<CanvasGroup>();
			Transform transform2 = this._commandCanvas.transform;
			GameObject original7 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "NicknameUI", abmanifests.Default);
			this._nicknameUI = UnityEngine.Object.Instantiate<GameObject>(original7, transform2, false).GetComponent<AnimalNicknameOutput>();
			GameObject original8 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CommandLabel", abmanifests.Default);
			this._commandLabel = UnityEngine.Object.Instantiate<GameObject>(original8, transform2, false).GetComponent<CommandLabel>();
			GameObject original9 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ADVWindow", abmanifests.Default);
			this._advScene = UnityEngine.Object.Instantiate<GameObject>(original9, transform2, false).GetComponentInChildren<ADVScene>(true);
			ParameterList.Add(new ADVParames(this._advData));
			GameObject original10 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd19, "ChoiceUI", abmanifests.Add19);
			this._choiceUI = UnityEngine.Object.Instantiate<GameObject>(original10, transform2, false).GetComponent<CommCommandList>();
			GameObject original11 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd05, "CommandList", abmanifests.Add05);
			this._commandList = UnityEngine.Object.Instantiate<GameObject>(original11, transform2, false).GetComponent<CommCommandList>();
			GameObject original12 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "MenuUI", abmanifests.Default);
			this._systemMenuUI = UnityEngine.Object.Instantiate<GameObject>(original12, transform2, false).GetComponent<SystemMenuUI>();
			GameObject original13 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "GameLog", abmanifests.Default);
			this._gameLogUI = UnityEngine.Object.Instantiate<GameObject>(original13, transform2, false).GetComponent<GameLog>();
			GameObject original14 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ItemBoxUI", abmanifests.Default);
			this._itemBoxUI = UnityEngine.Object.Instantiate<GameObject>(original14, transform2, false).GetComponent<ItemBoxUI>();
			GameObject original15 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "DressRoomUI", abmanifests.Default);
			this._dressRoomUI = UnityEngine.Object.Instantiate<GameObject>(original15, transform2, false).GetComponent<ClosetUI>();
			GameObject original16 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ClosetUI", abmanifests.Default);
			this._closetUI = UnityEngine.Object.Instantiate<GameObject>(original16, transform2, false).GetComponent<ClosetUI>();
			GameObject original17 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CharaEntryUI", abmanifests.Default);
			this._charaEntryUI = UnityEngine.Object.Instantiate<GameObject>(original17, transform2, false).GetComponent<CharaEntryUI>();
			GameObject original18 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CharaChangeUI", abmanifests.Default);
			this._charaChangeUI = UnityEngine.Object.Instantiate<GameObject>(original18, transform2, false).GetComponent<CharaChangeUI>();
			GameObject original19 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "PlayerChangeUI", abmanifests.Default);
			this._playerChangeUI = UnityEngine.Object.Instantiate<GameObject>(original19, transform2, false).GetComponent<PlayerChangeUI>();
			GameObject original20 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CharaLookEditUI", abmanifests.Default);
			this._charaLookEditUI = UnityEngine.Object.Instantiate<GameObject>(original20, transform2, false).GetComponent<CharaLookEditUI>();
			GameObject original21 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "PlayerLookEditUI", abmanifests.Default);
			this._playerLookEditUI = UnityEngine.Object.Instantiate<GameObject>(original21, transform2, false).GetComponent<PlayerLookEditUI>();
			GameObject original22 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd05, "CharaMigrateUI", abmanifests.Add05);
			this._charaMigrateUI = UnityEngine.Object.Instantiate<GameObject>(original22, transform2, false).GetComponent<CharaMigrateUI>();
			GameObject gameObject2 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd05, "FishingUI", abmanifests.Add05);
			this._fishingUI = ((!(gameObject2 == null)) ? UnityEngine.Object.Instantiate<GameObject>(gameObject2, transform2, false).GetComponent<FishingUI>() : null);
			GameObject gameObject3 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd05, "RequestUI", abmanifests.Add05);
			this._requestUI = ((!(gameObject3 == null)) ? UnityEngine.Object.Instantiate<GameObject>(gameObject3, transform2, false).GetComponent<RequestUI>() : null);
			GameObject original23 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "PhotoShotUI", abmanifests.Default);
			this._photoShotUI = UnityEngine.Object.Instantiate<GameObject>(original23, transform2, false).GetComponent<PhotoShotUI>();
			GameObject original24 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "SpendMoneyUI", abmanifests.Default);
			this._spendMoneyUI = UnityEngine.Object.Instantiate<GameObject>(original24, transform2, false).GetComponent<SpendMoneyUI>();
			GameObject original25 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd12, "AllAreaMap", abmanifests.Add12);
			this._AllAreaMapUI = UnityEngine.Object.Instantiate<GameObject>(original25, transform2, false).GetComponent<AllAreaMapUI>();
			GameObject original26 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ShopUI", abmanifests.Default);
			this._ShopUI = UnityEngine.Object.Instantiate<GameObject>(original26, transform2, false).GetComponent<ShopUI>();
			GameObject original27 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ScroungeUI", abmanifests.Default);
			this._ScroungeUI = UnityEngine.Object.Instantiate<GameObject>(original27, transform2, false).GetComponent<ScroungeUI>();
			GameObject original28 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "RefrigeratorUI", abmanifests.Default);
			this._RefrigeratorUI = UnityEngine.Object.Instantiate<GameObject>(original28, transform2, false).GetComponent<RefrigeratorUI>();
			GameObject original29 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CraftUI", abmanifests.Default);
			this._CraftUI = UnityEngine.Object.Instantiate<GameObject>(original29, transform2, false).GetComponent<CraftUI>();
			GameObject original30 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "CookingUI", abmanifests.Default);
			this._CookingUI = UnityEngine.Object.Instantiate<GameObject>(original30, transform2, false).GetComponent<CraftUI>();
			GameObject original31 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "PetCraftUI", abmanifests.Default);
			this._PetCraftUI = UnityEngine.Object.Instantiate<GameObject>(original31, transform2, false).GetComponent<CraftUI>();
			GameObject original32 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "MedicineCraftUI", abmanifests.Default);
			this._MedicineCraftUI = UnityEngine.Object.Instantiate<GameObject>(original32, transform2, false).GetComponent<CraftUI>();
			GameObject gameObject4 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd12, "RecyclingUI", abmanifests.Add12);
			if (gameObject4 != null)
			{
				this._recyclingUI = UnityEngine.Object.Instantiate<GameObject>(gameObject4, transform2, false).GetComponent<RecyclingUI>();
			}
			else
			{
				this._recyclingUI = null;
			}
			GameObject original33 = this.LoadAsset<GameObject>(abpaths.MapScenePrefabAdd07, "FarmlandUI", abmanifests.Add07);
			this._FarmlandUI = UnityEngine.Object.Instantiate<GameObject>(original33, transform2, false).GetComponent<FarmlandUI>();
			GameObject original34 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "ChickenCoopUI", abmanifests.Default);
			this._ChickenCoopUI = UnityEngine.Object.Instantiate<GameObject>(original34, transform2, false).GetComponent<ChickenCoopUI>();
			GameObject original35 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "PetHomeUI", abmanifests.Default);
			this._petHomeUI = UnityEngine.Object.Instantiate<GameObject>(original35, transform2, false).GetComponent<PetHomeUI>();
			GameObject original36 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "JukeBoxUI", abmanifests.Default);
			this._jukeBoxUI = UnityEngine.Object.Instantiate<GameObject>(original36, transform2, false).GetComponent<JukeBoxUI>();
			GameObject original37 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "EventDialogUI", abmanifests.Default);
			this._eventDialogUI = UnityEngine.Object.Instantiate<GameObject>(original37, transform2, false).GetComponent<EventDialogUI>();
			GameObject original38 = this.LoadAsset<GameObject>(abpaths.MapScenePrefab, "TutorialUI", abmanifests.Default);
			this._tutorialUI = UnityEngine.Object.Instantiate<GameObject>(original38, transform2, false).GetComponent<TutorialUI>();
		}

		// Token: 0x060088DA RID: 35034 RVA: 0x0038F246 File Offset: 0x0038D646
		private void Update()
		{
			if (this._centerPointerCanvasGroup != null)
			{
				this._centerPointerCanvasGroup.gameObject.SetActiveIfDifferent(Config.GameData.CenterPointer);
			}
		}

		// Token: 0x060088DB RID: 35035 RVA: 0x0038F274 File Offset: 0x0038D674
		protected override void OnDestroy()
		{
			base.OnDestroy();
			ParameterList.Remove(this._advData);
		}

		// Token: 0x04006E90 RID: 28304
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E91 RID: 28305
		[SerializeField]
		private Canvas _hudCanvas;

		// Token: 0x04006E92 RID: 28306
		[SerializeField]
		private Canvas _commandCanvas;

		// Token: 0x04006E93 RID: 28307
		[Header("Progression")]
		[SerializeField]
		private FadeCanvas _fadeCanvas;

		// Token: 0x04006E94 RID: 28308
		[SerializeField]
		private NotifyMessageList _notifyPanel;

		// Token: 0x04006E95 RID: 28309
		[SerializeField]
		[Header("HUD")]
		private CanvasGroup _centerPointerCanvasGroup;

		// Token: 0x04006E96 RID: 28310
		[SerializeField]
		private MiniMapControler _minimapUI;

		// Token: 0x04006E97 RID: 28311
		[SerializeField]
		private CanvasGroup _minimapCanvasGroup;

		// Token: 0x04006E98 RID: 28312
		[SerializeField]
		private GameObject _MiniMapRenderTex;

		// Token: 0x04006E99 RID: 28313
		[SerializeField]
		private GameObject _MiniMapObject;

		// Token: 0x04006E9A RID: 28314
		[Header("Command")]
		[SerializeField]
		private SystemMenuUI _systemMenuUI;

		// Token: 0x04006E9B RID: 28315
		[SerializeField]
		private GameLog _gameLogUI;

		// Token: 0x04006E9C RID: 28316
		[SerializeField]
		private CommCommandList _commandList;

		// Token: 0x04006E9D RID: 28317
		[SerializeField]
		private CommCommandList _choiceUI;

		// Token: 0x04006E9E RID: 28318
		[SerializeField]
		private CommandLabel _commandLabel;

		// Token: 0x04006E9F RID: 28319
		private ClosetUI _dressRoomUI;

		// Token: 0x04006EA0 RID: 28320
		private ClosetUI _closetUI;

		// Token: 0x04006EA1 RID: 28321
		private CharaEntryUI _charaEntryUI;

		// Token: 0x04006EA2 RID: 28322
		private CharaChangeUI _charaChangeUI;

		// Token: 0x04006EA3 RID: 28323
		private PlayerChangeUI _playerChangeUI;

		// Token: 0x04006EA4 RID: 28324
		private CharaLookEditUI _charaLookEditUI;

		// Token: 0x04006EA5 RID: 28325
		private PlayerLookEditUI _playerLookEditUI;

		// Token: 0x04006EA6 RID: 28326
		private CharaMigrateUI _charaMigrateUI;

		// Token: 0x04006EA7 RID: 28327
		[SerializeField]
		private ItemBoxUI _itemBoxUI;

		// Token: 0x04006EA8 RID: 28328
		[SerializeField]
		private FishingUI _fishingUI;

		// Token: 0x04006EA9 RID: 28329
		private ResultMessageUI _resultMessageUI;

		// Token: 0x04006EAA RID: 28330
		private WarningMessageUI _warningMessageUI;

		// Token: 0x04006EAB RID: 28331
		private StorySupportUI _storySupportUI;

		// Token: 0x04006EAC RID: 28332
		private RequestUI _requestUI;

		// Token: 0x04006EAD RID: 28333
		private PhotoShotUI _photoShotUI;

		// Token: 0x04006EAE RID: 28334
		private TutorialUI _tutorialUI;

		// Token: 0x04006EAF RID: 28335
		private EventDialogUI _eventDialogUI;

		// Token: 0x04006EB0 RID: 28336
		[SerializeField]
		private AllAreaMapUI _AllAreaMapUI;

		// Token: 0x04006EB1 RID: 28337
		[SerializeField]
		private ShopUI _ShopUI;

		// Token: 0x04006EB2 RID: 28338
		[SerializeField]
		private ScroungeUI _ScroungeUI;

		// Token: 0x04006EB3 RID: 28339
		[SerializeField]
		private RefrigeratorUI _RefrigeratorUI;

		// Token: 0x04006EB4 RID: 28340
		[SerializeField]
		private CraftUI _CraftUI;

		// Token: 0x04006EB5 RID: 28341
		[SerializeField]
		private CraftUI _CookingUI;

		// Token: 0x04006EB6 RID: 28342
		[SerializeField]
		private CraftUI _PetCraftUI;

		// Token: 0x04006EB7 RID: 28343
		[SerializeField]
		private CraftUI _MedicineCraftUI;

		// Token: 0x04006EB8 RID: 28344
		[SerializeField]
		private FarmlandUI _FarmlandUI;

		// Token: 0x04006EB9 RID: 28345
		[SerializeField]
		private ChickenCoopUI _ChickenCoopUI;

		// Token: 0x04006EBA RID: 28346
		[SerializeField]
		private PetHomeUI _petHomeUI;

		// Token: 0x04006EBB RID: 28347
		[SerializeField]
		private JukeBoxUI _jukeBoxUI;

		// Token: 0x04006EBC RID: 28348
		[SerializeField]
		private SpendMoneyUI _spendMoneyUI;

		// Token: 0x04006EBD RID: 28349
		[SerializeField]
		private AnimalNicknameOutput _nicknameUI;

		// Token: 0x04006EBE RID: 28350
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x04006EBF RID: 28351
		private IDisposable _hudActivateSubscriber;

		// Token: 0x04006EC0 RID: 28352
		private IDisposable _storySupprtUIActivateSubscriber;

		// Token: 0x04006EC1 RID: 28353
		[Header("Debug")]
		[SerializeField]
		private Canvas _debugUIRoot;

		// Token: 0x04006EC2 RID: 28354
		[SerializeField]
		private Canvas _debugBackgroundUIRoot;

		// Token: 0x04006EC3 RID: 28355
		private ADVScene _advScene;

		// Token: 0x04006EC4 RID: 28356
		private readonly ADVData _advData = new ADVData();

		// Token: 0x04006EC5 RID: 28357
		private bool _activeCanvas = true;
	}
}
