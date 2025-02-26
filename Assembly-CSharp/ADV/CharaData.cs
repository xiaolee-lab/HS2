using System;
using System.Collections.Generic;
using ADV.Commands.Base;
using AIChara;
using AIProject;
using Illusion.Game.Elements.EasyLoader;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace ADV
{
	// Token: 0x020006C8 RID: 1736
	public class CharaData
	{
		// Token: 0x06002935 RID: 10549 RVA: 0x000F2464 File Offset: 0x000F0864
		public CharaData(TextScenario.ParamData data, TextScenario scenario)
		{
			this.data = data;
			this.scenario = scenario;
			this.itemDic = new Dictionary<int, CharaData.CharaItem>();
			this.isADVCreateChara = true;
			this.initialized = true;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000F24A0 File Offset: 0x000F08A0
		public CharaData(TextScenario.ParamData data, TextScenario scenario, CharaData.MotionReserver motionReserver)
		{
			this.data = data;
			this.scenario = scenario;
			this.motionReserver = motionReserver;
			this._chaCtrl = data.chaCtrl;
			this.isADVCreateChara = (this._chaCtrl == null);
			if (this._chaCtrl != null)
			{
				this.Initialize();
				this.backup = new CharaData.Backup(data);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000F2514 File Offset: 0x000F0914
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x000F251C File Offset: 0x000F091C
		public Dictionary<int, CharaData.CharaItem> itemDic { get; private set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000F2525 File Offset: 0x000F0925
		// (set) Token: 0x0600293A RID: 10554 RVA: 0x000F252D File Offset: 0x000F092D
		public bool initialized { get; private set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000F2536 File Offset: 0x000F0936
		// (set) Token: 0x0600293C RID: 10556 RVA: 0x000F253E File Offset: 0x000F093E
		private bool isADVCreateChara { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x000F2547 File Offset: 0x000F0947
		// (set) Token: 0x0600293E RID: 10558 RVA: 0x000F254F File Offset: 0x000F094F
		private GameObject dataBaseRoot { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000F2558 File Offset: 0x000F0958
		public Transform root
		{
			get
			{
				if (this.chaCtrl == null)
				{
					return null;
				}
				return this.chaCtrl.transform;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000F2578 File Offset: 0x000F0978
		public int voiceNo
		{
			get
			{
				return this.data.voiceNo;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000F2585 File Offset: 0x000F0985
		public float voicePitch
		{
			get
			{
				return this.data.voicePitch;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000F2594 File Offset: 0x000F0994
		public Transform voiceTrans
		{
			get
			{
				if (this.chaCtrl == null || !this.chaCtrl.loadEnd)
				{
					return null;
				}
				Transform trfHeadParent = this.chaCtrl.cmpBoneBody.targetEtc.trfHeadParent;
				if (trfHeadParent == null)
				{
					return null;
				}
				return trfHeadParent.transform;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000F25EE File Offset: 0x000F09EE
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x000F25F6 File Offset: 0x000F09F6
		public TextScenario.ParamData data { get; private set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000F25FF File Offset: 0x000F09FF
		public ChaControl chaCtrl
		{
			get
			{
				return this.GetCacheObject(ref this._chaCtrl, () => this.data.chaCtrl);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000F261C File Offset: 0x000F0A1C
		public Transform transform
		{
			get
			{
				if (this.data.transform != null)
				{
					return this.data.transform;
				}
				if (this.chaCtrl == null)
				{
					return null;
				}
				return this.chaCtrl.transform;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000F2669 File Offset: 0x000F0A69
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x000F2671 File Offset: 0x000F0A71
		private CharaData.MotionReserver motionReserver { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000F267A File Offset: 0x000F0A7A
		public IKMotion ikMotion
		{
			get
			{
				return this.GetCache(ref this._ikMotion, delegate
				{
					IKMotion ikmotion = new IKMotion();
					ikmotion.Create(this.chaCtrl, null, Array.Empty<MotionIK>());
					return ikmotion;
				});
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000F2694 File Offset: 0x000F0A94
		public YureMotion yureMotion
		{
			get
			{
				return this._yureMotion;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000F269C File Offset: 0x000F0A9C
		// (set) Token: 0x0600294C RID: 10572 RVA: 0x000F26A4 File Offset: 0x000F0AA4
		private YureMotion _yureMotion { get; set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000F26AD File Offset: 0x000F0AAD
		public MotionOverride motionOverride
		{
			get
			{
				return this._motionOverride;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000F26B5 File Offset: 0x000F0AB5
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x000F26BD File Offset: 0x000F0ABD
		public CharaData.Backup backup { get; set; }

		// Token: 0x06002950 RID: 10576 RVA: 0x000F26C8 File Offset: 0x000F0AC8
		public void Initialize()
		{
			this.itemDic = new Dictionary<int, CharaData.CharaItem>();
			bool flag = this.motionReserver != null;
			if (flag)
			{
				if (this.motionReserver.ikMotion != null)
				{
					this._ikMotion = this.motionReserver.ikMotion;
				}
				if (this.motionReserver.yureMotion != null)
				{
					this._yureMotion = this.motionReserver.yureMotion;
				}
				if (this.motionReserver.motionOverride != null)
				{
					this._motionOverride = this.motionReserver.motionOverride;
				}
			}
			if (this._yureMotion == null)
			{
				this._yureMotion = new YureMotion();
				this._yureMotion.Create(this.chaCtrl, null);
			}
			if (this.isADVCreateChara)
			{
				this.scenario.commandController.LoadingCharaList.Remove(this);
				this.chaCtrl.SetActiveTop(true);
				MotionIK motionIK = new MotionIK(this.chaCtrl, false, null);
				if (this._ikMotion == null)
				{
					this._ikMotion = new IKMotion();
					this._ikMotion.Create(this.chaCtrl, motionIK, Array.Empty<MotionIK>());
				}
			}
			this.initialized = true;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000F27F0 File Offset: 0x000F0BF0
		public void MotionPlay(ADV.Commands.Base.Motion.Data motion, bool isCrossFade)
		{
			if (isCrossFade)
			{
				this.scenario.CrossFadeStart();
			}
			if (motion.pair == null)
			{
				bool flag = this._motionOverride.Setting(this.chaCtrl.animBody, motion.assetBundleName, motion.assetName, motion.overrideAssetBundleName, motion.overrideAssetName, motion.stateName, true);
				if (flag)
				{
					Info.Anime.Play play = this.scenario.info.anime.play;
					this._motionOverride.isCrossFade = play.isCrossFade;
					this._motionOverride.layers = motion.layerNo;
					this._motionOverride.transitionDuration = play.transitionDuration;
					this._motionOverride.normalizedTime = play.normalizedTime;
					this._motionOverride.Play(this.chaCtrl.animBody);
				}
				IKMotion ikMotion = this.ikMotion;
				bool enabled = this.data.actor.Animation.enabled;
				ikMotion.use = !enabled;
				ikMotion.motionIK.enabled = enabled;
				ikMotion.Setting(this.chaCtrl, motion.ikAssetBundleName, motion.ikAssetName, motion.stateName, false);
				this._yureMotion.Setting(this.chaCtrl, motion.shakeAssetBundleName, motion.shakeAssetName, motion.stateName, false);
			}
			else
			{
				int postureID = motion.pair.Value.postureID;
				int poseID = motion.pair.Value.poseID;
				Actor actor = this.data.actor;
				actor.ActionID = postureID;
				actor.PoseID = poseID;
				PlayState autoPlayState = this.GetAutoPlayState();
				if (autoPlayState != null)
				{
					actor.Animation.StopAllAnimCoroutine();
					ActorAnimInfo actorAnimInfo = actor.Animation.LoadActionState(postureID, poseID, autoPlayState);
					actor.Animation.PlayInAnimation(actorAnimInfo.inEnableBlend, actorAnimInfo.inBlendSec, autoPlayState.MainStateInfo.FadeOutTime, actorAnimInfo.layer);
				}
			}
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000F29FC File Offset: 0x000F0DFC
		public PlayState GetAutoPlayState()
		{
			Actor actor = this.data.actor;
			if (actor == null)
			{
				return null;
			}
			Manager.Resources.AnimationTables animation = Singleton<Manager.Resources>.Instance.Animation;
			PlayState playState;
			if (this.data.agentActor != null)
			{
				playState = this.GetPlayState(animation.AgentActionAnimTable);
			}
			else if (this.data.playerActor != null)
			{
				PlayerActor playerActor = this.data.playerActor;
				playState = this.GetPlayState(animation.PlayerActionAnimTable[(int)playerActor.PlayerData.Sex]);
			}
			else
			{
				playState = this.GetPlayState(animation.MerchantOnlyActionAnimStateTable);
				if (playState == null)
				{
					playState = this.GetPlayState(animation.MerchantCommonActionAnimStateTable);
				}
				if (playState == null)
				{
					playState = this.GetPlayState(animation.AgentActionAnimTable);
				}
			}
			return playState;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000F2AD0 File Offset: 0x000F0ED0
		public PlayState GetPlayState(Dictionary<int, Dictionary<int, PlayState>> stateTable)
		{
			Actor actor = this.data.actor;
			if (actor == null)
			{
				return null;
			}
			return CharaData.GetPlayState(stateTable, actor.ActionID, actor.PoseID);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000F2B0C File Offset: 0x000F0F0C
		private static PlayState GetPlayState(IReadOnlyDictionary<int, Dictionary<int, PlayState>> stateTable, int postureID, int poseID)
		{
			Dictionary<int, PlayState> dictionary;
			PlayState result;
			if (stateTable.TryGetValue(postureID, out dictionary) && dictionary.TryGetValue(poseID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000F2B38 File Offset: 0x000F0F38
		public void Release()
		{
			if (this.initialized)
			{
				foreach (KeyValuePair<int, CharaData.CharaItem> keyValuePair in this.itemDic)
				{
					keyValuePair.Value.Delete();
				}
			}
			if (this.backup != null)
			{
				this.backup.Repair();
			}
		}

		// Token: 0x04002A75 RID: 10869
		private ChaControl _chaCtrl;

		// Token: 0x04002A77 RID: 10871
		private IKMotion _ikMotion;

		// Token: 0x04002A79 RID: 10873
		private MotionOverride _motionOverride = new MotionOverride();

		// Token: 0x04002A7A RID: 10874
		private TextScenario scenario;

		// Token: 0x020006C9 RID: 1737
		public class MotionReserver
		{
			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06002959 RID: 10585 RVA: 0x000F2BFA File Offset: 0x000F0FFA
			// (set) Token: 0x0600295A RID: 10586 RVA: 0x000F2C02 File Offset: 0x000F1002
			public IKMotion ikMotion { get; set; }

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x0600295B RID: 10587 RVA: 0x000F2C0B File Offset: 0x000F100B
			// (set) Token: 0x0600295C RID: 10588 RVA: 0x000F2C13 File Offset: 0x000F1013
			public YureMotion yureMotion { get; set; }

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x0600295D RID: 10589 RVA: 0x000F2C1C File Offset: 0x000F101C
			// (set) Token: 0x0600295E RID: 10590 RVA: 0x000F2C24 File Offset: 0x000F1024
			public MotionOverride motionOverride { get; set; }
		}

		// Token: 0x020006CA RID: 1738
		public class Backup
		{
			// Token: 0x0600295F RID: 10591 RVA: 0x000F2C30 File Offset: 0x000F1030
			public Backup(TextScenario.ParamData data)
			{
				this.navMeshAgent = data.actor.NavMeshAgent;
				this.isNavmesh = this.navMeshAgent.enabled;
				this.transform = data.actor.Animation.Character.transform;
				this.position = this.transform.localPosition;
				this.rotation = this.transform.localRotation;
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06002960 RID: 10592 RVA: 0x000F2CA2 File Offset: 0x000F10A2
			public Transform transform { get; }

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06002961 RID: 10593 RVA: 0x000F2CAA File Offset: 0x000F10AA
			private Vector3 position { get; }

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06002962 RID: 10594 RVA: 0x000F2CB2 File Offset: 0x000F10B2
			private Quaternion rotation { get; }

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06002963 RID: 10595 RVA: 0x000F2CBA File Offset: 0x000F10BA
			private bool isNavmesh { get; }

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06002964 RID: 10596 RVA: 0x000F2CC2 File Offset: 0x000F10C2
			private NavMeshAgent navMeshAgent { get; }

			// Token: 0x06002965 RID: 10597 RVA: 0x000F2CCA File Offset: 0x000F10CA
			public void Set()
			{
				this.isRepair = true;
				this.navMeshAgent.enabled = false;
			}

			// Token: 0x06002966 RID: 10598 RVA: 0x000F2CE0 File Offset: 0x000F10E0
			public void Repair()
			{
				if (!this.isRepair)
				{
					return;
				}
				if (this.transform != null)
				{
					this.transform.localPosition = this.position;
					this.transform.localRotation = this.rotation;
				}
				if (this.navMeshAgent != null)
				{
					this.navMeshAgent.enabled = this.isNavmesh;
				}
			}

			// Token: 0x04002A84 RID: 10884
			private bool isRepair;
		}

		// Token: 0x020006CB RID: 1739
		public class CharaItem
		{
			// Token: 0x06002967 RID: 10599 RVA: 0x000F2D4E File Offset: 0x000F114E
			public CharaItem()
			{
			}

			// Token: 0x06002968 RID: 10600 RVA: 0x000F2D56 File Offset: 0x000F1156
			public CharaItem(GameObject item)
			{
				this.item = item;
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06002969 RID: 10601 RVA: 0x000F2D65 File Offset: 0x000F1165
			// (set) Token: 0x0600296A RID: 10602 RVA: 0x000F2D6D File Offset: 0x000F116D
			public GameObject item { get; private set; }

			// Token: 0x0600296B RID: 10603 RVA: 0x000F2D76 File Offset: 0x000F1176
			public void Delete()
			{
				if (this.item != null)
				{
					UnityEngine.Object.Destroy(this.item);
					this.item = null;
				}
			}

			// Token: 0x0600296C RID: 10604 RVA: 0x000F2D9C File Offset: 0x000F119C
			public void LoadObject(string bundle, string asset, Transform root, bool worldPositionStays = false, string manifest = null)
			{
				this.Delete();
				GameObject asset2 = AssetBundleManager.LoadAsset(bundle, asset, typeof(GameObject), manifest).GetAsset<GameObject>();
				this.item = UnityEngine.Object.Instantiate<GameObject>(asset2);
				AssetBundleManager.UnloadAssetBundle(bundle, false, manifest, false);
				this.item.name = asset2.name;
				this.item.transform.SetParent(root, worldPositionStays);
			}

			// Token: 0x0600296D RID: 10605 RVA: 0x000F2E04 File Offset: 0x000F1204
			public void LoadAnimator(string bundle, string asset, string state)
			{
				Animator orAddComponent = this.item.GetOrAddComponent<Animator>();
				if (this.motion == null)
				{
					this.motion = new Illusion.Game.Elements.EasyLoader.Motion(bundle, asset, state);
				}
				if (this.motion.Setting(orAddComponent, bundle, asset, state, true))
				{
					this.motion.Play(orAddComponent);
				}
			}

			// Token: 0x04002A86 RID: 10886
			private Illusion.Game.Elements.EasyLoader.Motion motion;
		}
	}
}
