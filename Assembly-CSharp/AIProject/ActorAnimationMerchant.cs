using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C5F RID: 3167
	public class ActorAnimationMerchant : ActorAnimation
	{
		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x002B7470 File Offset: 0x002B5870
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x002B7478 File Offset: 0x002B5878
		public FullBodyBipedIK IK
		{
			get
			{
				return this.ik;
			}
			set
			{
				this.ik = value;
				if (this.ik != null)
				{
					IKSolverFullBodyBiped solver = this.ik.solver;
					solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
					this.ik.enabled = true;
				}
			}
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x002B74D5 File Offset: 0x002B58D5
		public override Vector3 GetPivotPoint()
		{
			return base.transform.position;
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x002B74E4 File Offset: 0x002B58E4
		protected override void Start()
		{
			base.Start();
			if (this.ik == null)
			{
				this.ik = base.GetComponentInChildren<FullBodyBipedIK>(true);
				if (this.ik != null)
				{
					this.ik.enabled = true;
					IKSolverFullBodyBiped solver = this.ik.solver;
					solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
				}
			}
			if (this.Animator == null)
			{
				this.Animator = base.GetComponentInChildren<Animator>(true);
			}
			(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x002B75B0 File Offset: 0x002B59B0
		protected void OnAnimatorMove()
		{
			base.Character.transform.localPosition += this.Animator.deltaPosition;
			base.Character.transform.localRotation *= this.Animator.deltaRotation;
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x002B760C File Offset: 0x002B5A0C
		protected override void LoadMatchTargetInfo(string _stateName)
		{
			int key = Animator.StringToHash(_stateName);
			List<AnimeMoveInfo> list;
			if (Singleton<Manager.Resources>.Instance.Animation.MerchantMoveInfoTable.TryGetValue(key, out list))
			{
				foreach (AnimeMoveInfo animeMoveInfo in list)
				{
					GameObject gameObject = base.Actor.CurrentPoint.transform.FindLoop(animeMoveInfo.movePoint);
					ProceduralTargetParameter proceduralTargetParameter = new ProceduralTargetParameter
					{
						Start = animeMoveInfo.start,
						End = animeMoveInfo.end
					};
					if (gameObject != null)
					{
						proceduralTargetParameter.Target = gameObject.transform;
					}
					base.Targets.Add(proceduralTargetParameter);
				}
			}
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x002B76EC File Offset: 0x002B5AEC
		private void AfterFBBIK()
		{
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x002B76EE File Offset: 0x002B5AEE
		public override void LoadEventKeyTable(int eventID, int poseID)
		{
			this.LoadActionEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x002B76F8 File Offset: 0x002B5AF8
		public void LoadActionEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> merchantCommonItemEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonItemEventKeyTable;
			if (!merchantCommonItemEventKeyTable.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>())
			{
				this.LoadEventKeyTable(eventID, poseID, merchantCommonItemEventKeyTable);
			}
			this.LoadActionSEEventKeyTable(eventID, poseID);
			this.LoadActionParticleEventKeyTable(eventID, poseID);
			this.LoadActionOnceVoiceEventKeyTable(eventID, poseID);
			this.LoadActionLoopVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x002B774C File Offset: 0x002B5B4C
		public void LoadMerchantEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> merchantOnlyItemEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyItemEventKeyTable;
			if (!merchantOnlyItemEventKeyTable.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>())
			{
				this.LoadEventKeyTable(eventID, poseID, merchantOnlyItemEventKeyTable);
			}
			this.LoadMerchantSEEventKeyTable(eventID, poseID);
			this.LoadMerchantParticleEventKeyTable(eventID, poseID);
			this.LoadMerchantOnceVoiceEventKeyTable(eventID, poseID);
			this.LoadMerchantLoopVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x002B77A0 File Offset: 0x002B5BA0
		private void LoadEventKeyTable(int eventID, int poseID, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> tableGroup)
		{
			Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary;
			Dictionary<int, List<AnimeEventInfo>> itemEventKeyTable;
			if (tableGroup.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out itemEventKeyTable))
			{
				base.ItemEventKeyTable = itemEventKeyTable;
				return;
			}
			base.ItemEventKeyTable = null;
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x002B77D8 File Offset: 0x002B5BD8
		public override void LoadSEEventKeyTable(int eventID, int poseID)
		{
			this.LoadActionSEEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x002B77E4 File Offset: 0x002B5BE4
		public void LoadActionSEEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> merchantCommonSEEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonSEEventKeyTable;
			this.LoadSEEventKeyTable(eventID, poseID, merchantCommonSEEventKeyTable);
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x002B780C File Offset: 0x002B5C0C
		public void LoadMerchantSEEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> merchantOnlySEEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlySEEventKeyTable;
			this.LoadSEEventKeyTable(eventID, poseID, merchantOnlySEEventKeyTable);
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x002B7834 File Offset: 0x002B5C34
		private void LoadSEEventKeyTable(int eventID, int poseID, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> tableGroup)
		{
			Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>> dictionary;
			Dictionary<int, List<AnimeSEEventInfo>> seeventKeyTable;
			if (!tableGroup.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>() && tableGroup.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out seeventKeyTable))
			{
				base.SEEventKeyTable = seeventKeyTable;
				return;
			}
			base.SEEventKeyTable = null;
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x002B7877 File Offset: 0x002B5C77
		public override void LoadParticleEventKeyTable(int eventID, int poseID)
		{
			this.LoadActionParticleEventKeyTable(eventID, poseID);
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x002B7884 File Offset: 0x002B5C84
		public void LoadActionParticleEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> merchantCommonParticleEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonParticleEventKeyTable;
			this.LoadParticleEventKeyTable(eventID, poseID, merchantCommonParticleEventKeyTable);
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x002B78AC File Offset: 0x002B5CAC
		public void LoadMerchantParticleEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> merchantOnlyParticleEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyParticleEventKeyTable;
			this.LoadParticleEventKeyTable(eventID, poseID, merchantOnlyParticleEventKeyTable);
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x002B78D4 File Offset: 0x002B5CD4
		private void LoadParticleEventKeyTable(int eventID, int poseID, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> tableGroup)
		{
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
			Dictionary<int, List<AnimeParticleEventInfo>> particleEventKeyTable;
			if (!tableGroup.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>() && tableGroup.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out particleEventKeyTable))
			{
				base.ParticleEventKeyTable = particleEventKeyTable;
				return;
			}
			base.ParticleEventKeyTable = null;
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x002B7917 File Offset: 0x002B5D17
		public override void LoadOnceVoiceEventKeyTable(int eventID, int poseID)
		{
			this.LoadActionOnceVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x002B7924 File Offset: 0x002B5D24
		public void LoadActionOnceVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> merchantCommonOnceVoiceEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonOnceVoiceEventKeyTable;
			this.LoadOnceVoiceEventKeyTable(eventID, poseID, merchantCommonOnceVoiceEventKeyTable);
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x002B794C File Offset: 0x002B5D4C
		public void LoadMerchantOnceVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> merchantOnlyOnceVoiceEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyOnceVoiceEventKeyTable;
			this.LoadOnceVoiceEventKeyTable(eventID, poseID, merchantOnlyOnceVoiceEventKeyTable);
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x002B7974 File Offset: 0x002B5D74
		private void LoadOnceVoiceEventKeyTable(int eventID, int poseID, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> tableGroup)
		{
			Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>> dictionary;
			Dictionary<int, List<AnimeOnceVoiceEventInfo>> onceVoiceEventKeyTable;
			if (!tableGroup.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>>() && tableGroup.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out onceVoiceEventKeyTable))
			{
				base.OnceVoiceEventKeyTable = onceVoiceEventKeyTable;
				return;
			}
			base.OnceVoiceEventKeyTable = null;
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x002B79B7 File Offset: 0x002B5DB7
		public override void LoadLoopVoiceEventKeyTable(int eventID, int poseID)
		{
			this.LoadActionLoopVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x002B79C4 File Offset: 0x002B5DC4
		public void LoadActionLoopVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> merchantCommonLoopVoiceEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonLoopVoiceEventKeyTable;
			this.LoadLoopVoiceEventKeyTable(eventID, poseID, merchantCommonLoopVoiceEventKeyTable);
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x002B79EC File Offset: 0x002B5DEC
		public void LoadMerchantLoopVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> merchantOnlyLoopVoiceEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyLoopVoiceEventKeyTable;
			this.LoadLoopVoiceEventKeyTable(eventID, poseID, merchantOnlyLoopVoiceEventKeyTable);
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x002B7A14 File Offset: 0x002B5E14
		private void LoadLoopVoiceEventKeyTable(int eventID, int poseID, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> tableGroup)
		{
			Dictionary<int, Dictionary<int, List<int>>> dictionary;
			Dictionary<int, List<int>> loopVoiceEventKeyTable;
			if (!tableGroup.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<int>>>>() && tableGroup.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out loopVoiceEventKeyTable))
			{
				base.LoopVoiceEventKeyTable = loopVoiceEventKeyTable;
				return;
			}
			base.LoopVoiceEventKeyTable = null;
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x002B7A58 File Offset: 0x002B5E58
		protected override void PlayEventOnceVoice(int voiceID)
		{
			AssetBundleInfo assetBundleInfo;
			if (!Singleton<Manager.Resources>.Instance.Sound.TryGetMapActionVoiceInfo(-90, voiceID, out assetBundleInfo))
			{
				return;
			}
			Transform transform = base.Actor.Locomotor.transform;
			Voice instance = Singleton<Voice>.Instance;
			int no = -90;
			string assetbundle = assetBundleInfo.assetbundle;
			string asset = assetBundleInfo.asset;
			Transform voiceTrans = transform;
			Transform transform2 = instance.OnecePlayChara(no, assetbundle, asset, 1f, 0f, 0f, true, voiceTrans, Voice.Type.PCM, -1, true, true, false);
			base.Actor.ChaControl.SetVoiceTransform(transform2);
			base.OnceActionVoice = transform2.GetComponent<AudioSource>();
			if (base.OnceActionVoice != null)
			{
				base.OnceActionVoice.OnDestroyAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(Unit _)
				{
					if (!this.PlayEventLoopVoice())
					{
						this.StopLoopActionVoice();
					}
				});
			}
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x002B7B28 File Offset: 0x002B5F28
		protected override bool PlayEventLoopVoice()
		{
			if (!base.PlayEventLoopVoice())
			{
				return false;
			}
			int num = base.StateLoopVoiceEvents[UnityEngine.Random.Range(0, base.StateLoopVoiceEvents.Count)];
			if (this._loopActionVoice.Item1 == num && this._loopActionVoice.Item2 != null)
			{
				return false;
			}
			int num2 = -90;
			AssetBundleInfo assetBundleInfo;
			if (!Singleton<Manager.Resources>.Instance.Sound.TryGetMapActionVoiceInfo(num2, num, out assetBundleInfo))
			{
				return false;
			}
			Transform transform = base.Actor.Locomotor.transform;
			Voice instance = Singleton<Voice>.Instance;
			int no = num2;
			string assetbundle = assetBundleInfo.assetbundle;
			string asset = assetBundleInfo.asset;
			Transform voiceTrans = transform;
			Transform transform2 = instance.OnecePlayChara(no, assetbundle, asset, 1f, 0f, 0f, true, voiceTrans, Voice.Type.PCM, -1, false, true, false);
			base.Actor.ChaControl.SetVoiceTransform(transform2);
			this._loopActionVoice.Item1 = num;
			this._loopActionVoice.Item2 = transform2.GetComponent<AudioSource>();
			this._loopActionVoice.Item3 = transform;
			this._loopActionVoice.Item4 = num2;
			return this._loopActionVoice.Item2 != null;
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x002B7C51 File Offset: 0x002B6051
		protected override void LoadFootStepEventKeyTable()
		{
			this._footStepEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.MerchantFootStepEventKeyTable;
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x002B7C68 File Offset: 0x002B6068
		public void UpdateState(ActorLocomotion.AnimationState state)
		{
			if (Mathf.Approximately(Time.deltaTime, 0f))
			{
				return;
			}
			if (this.Animator == null || !this.Animator.isActiveAndEnabled)
			{
				return;
			}
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string directionParameterName = definePack.AnimatorParameter.DirectionParameterName;
			string forwardMove = definePack.AnimatorParameter.ForwardMove;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			float b2;
			if (state.setMediumOnWalk)
			{
				if (state.moveDirection.z <= state.medVelocity || Mathf.Approximately(state.moveDirection.z, state.medVelocity))
				{
					float b = Mathf.InverseLerp(0f, state.maxVelocity, state.medVelocity);
					float value = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
					float t = Mathf.InverseLerp(0f, b, value);
					b2 = Mathf.Lerp(0f, 0.5f, t);
				}
				else
				{
					b2 = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
				}
			}
			else
			{
				b2 = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
			}
			foreach (AnimatorControllerParameter animatorControllerParameter in this.Animator.parameters)
			{
				if (!base.Actor.IsSlave && animatorControllerParameter.name == forwardMove && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float @float = this.Animator.GetFloat(forwardMove);
					float value2 = Mathf.Lerp(@float, b2, Time.deltaTime * Singleton<Manager.Resources>.Instance.LocomotionProfile.LerpSpeed);
					this.Animator.SetFloat(forwardMove, value2);
				}
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float shapeBodyValue = base.Actor.ChaControl.GetShapeBodyValue(0);
					this.Animator.SetFloat(heightParameterName, shapeBodyValue);
				}
			}
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x002B7E9F File Offset: 0x002B629F
		private void OnLateUpdate()
		{
			base.Follow();
			if (Mathf.Approximately(Vector3.Angle(base.transform.up, Vector3.up), 0f))
			{
				return;
			}
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x002B7ECC File Offset: 0x002B62CC
		public ActorAnimationMerchant CloneComponent(GameObject target)
		{
			ActorAnimationMerchant actorAnimationMerchant = target.AddComponent<ActorAnimationMerchant>();
			actorAnimationMerchant._character = this._character;
			actorAnimationMerchant.Animator = this.Animator;
			actorAnimationMerchant.EnabledPoser = base.EnabledPoser;
			actorAnimationMerchant.ArmAnimator = base.ArmAnimator;
			actorAnimationMerchant.Poser = base.Poser;
			actorAnimationMerchant.IsLocomotionState = base.IsLocomotionState;
			actorAnimationMerchant.ik = this.ik;
			return actorAnimationMerchant;
		}

		// Token: 0x04005847 RID: 22599
		[SerializeField]
		private FullBodyBipedIK ik;
	}
}
