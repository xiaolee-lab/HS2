using System;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.SaveData;
using Cinemachine;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000BF0 RID: 3056
	public class CheckEventPointArea : MonoBehaviour
	{
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x00277533 File Offset: 0x00275933
		// (set) Token: 0x06005D6C RID: 23916 RVA: 0x0027753B File Offset: 0x0027593B
		public EventPoint EventPoint
		{
			get
			{
				return this._eventPoint;
			}
			set
			{
				this._eventPoint = value;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x00277544 File Offset: 0x00275944
		public CollisionState CollisionState
		{
			[CompilerGenerated]
			get
			{
				return this._collisionState;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x0027754C File Offset: 0x0027594C
		private OpenData openData { get; } = new OpenData();

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x00277554 File Offset: 0x00275954
		// (set) Token: 0x06005D70 RID: 23920 RVA: 0x0027755C File Offset: 0x0027595C
		private CheckEventPointArea.PackData packData { get; set; }

		// Token: 0x06005D71 RID: 23921 RVA: 0x00277565 File Offset: 0x00275965
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00277598 File Offset: 0x00275998
		private void OnUpdate()
		{
			if (this._eventPoint == null)
			{
				return;
			}
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			PlayerActor player = Manager.Map.GetPlayer();
			CommandArea commandArea = Manager.Map.GetCommandArea(player);
			if (commandArea == null)
			{
				return;
			}
			bool flag = this._eventPoint.IsNeutralCommand;
			if (flag)
			{
				IState state = player.PlayerController.State;
				flag &= (state is Normal || state is Houchi || state is Onbu);
			}
			if (flag)
			{
				float num = (!this._eventPoint.EnableRangeCheck) ? commandArea.AllAroundRadius : this._eventPoint.CheckRadius;
				Vector3 commandCenter = this._eventPoint.CommandCenter;
				Vector3 vector = player.Position;
				Vector3 b = player.Rotation * commandArea.Offset;
				vector += b;
				flag &= (this.DistanceNonHeight(commandCenter, vector) <= num);
				if (flag)
				{
					float num2 = Mathf.Abs(commandCenter.y - vector.y);
					flag &= (num2 < commandArea.Height);
				}
				if (flag)
				{
					NavMeshHit navMeshHit;
					flag &= (player.NavMeshAgent != null && player.NavMeshAgent.isActiveAndEnabled && !player.NavMeshAgent.Raycast(commandCenter, out navMeshHit));
				}
			}
			if (flag)
			{
				switch (this._collisionState)
				{
				case CollisionState.None:
				case CollisionState.Exit:
					this._collisionState = CollisionState.Enter;
					this.OnHitEnter();
					this.OnHitStay();
					break;
				case CollisionState.Enter:
				case CollisionState.Stay:
					this._collisionState = CollisionState.Stay;
					this.OnHitStay();
					break;
				}
			}
			else
			{
				switch (this._collisionState)
				{
				case CollisionState.None:
				case CollisionState.Exit:
					this._collisionState = CollisionState.None;
					break;
				case CollisionState.Enter:
				case CollisionState.Stay:
					this._collisionState = CollisionState.Exit;
					this.OnHitExit();
					break;
				}
			}
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x00277798 File Offset: 0x00275B98
		private float DistanceNonHeight(Vector3 p0, Vector3 p1)
		{
			p0.y = (p1.y = 0f);
			return Vector3.Distance(p0, p1);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x002777C4 File Offset: 0x00275BC4
		private void OnHitEnter()
		{
			if (this._eventPoint.GroupID != 2)
			{
				return;
			}
			int pointID = this._eventPoint.PointID;
			if (pointID != 0)
			{
				if (pointID == 1)
				{
					this._eventPoint.StartMerchantADV(4);
				}
			}
			else if (Manager.Map.TutorialMode)
			{
				this.StartMerchantStory0();
			}
			else if (this._eventPoint.GetDedicatedNumber() == 0)
			{
				this._eventPoint.SetDedicatedNumber(1);
			}
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x00277849 File Offset: 0x00275C49
		private void OnHitStay()
		{
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0027784B File Offset: 0x00275C4B
		private void OnHitExit()
		{
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0027784D File Offset: 0x00275C4D
		private void EndTutorial()
		{
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x00277850 File Offset: 0x00275C50
		private void StartMerchantStory0()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			this.openData.FindLoad("0", -90, 1);
			this.packData = new CheckEventPointArea.PackData();
			this.packData.Init();
			this.packData.SetParam(new IParams[]
			{
				instance.Merchant.MerchantData,
				instance.Player.PlayerData
			});
			this.packData.onComplete = delegate()
			{
				this.EndMerchantStory0();
				this.packData.Release();
				this.packData = null;
			};
			instance.Player.PlayerController.ChangeState("Idle");
			instance.Merchant.ChangeBehavior(Merchant.ActionType.TalkWithPlayer);
			MapUIContainer.SetVisibleHUD(false);
			MapUIContainer.FadeCanvas.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 2f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				if (!Singleton<Manager.Map>.IsInstance())
				{
					return;
				}
				MapUIContainer.SetVisibleHUD(false);
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				MerchantActor merchant = Singleton<Manager.Map>.Instance.Merchant;
				StoryPoint storyPoint = Manager.Map.GetStoryPoint(3);
				Transform transform = (!(storyPoint != null)) ? null : storyPoint.transform;
				if (transform != null)
				{
					GameObject gameObject = transform.FindLoop("player_point");
					if (gameObject != null)
					{
						transform = gameObject.transform;
					}
				}
				if (transform == null)
				{
					transform = Singleton<Manager.Map>.Instance.PlayerStartPoint;
				}
				Transform transform2 = transform;
				if (transform2 != null)
				{
					if (player.NavMeshAgent.enabled)
					{
						player.NavMeshAgent.Warp(transform2.position);
					}
					else
					{
						player.Position = transform2.position;
					}
					player.Rotation = transform2.rotation;
				}
				AgentActor tutorialAgent = Singleton<Manager.Map>.Instance.TutorialAgent;
				if (tutorialAgent != null)
				{
					if (tutorialAgent.NavMeshAgent.enabled)
					{
						tutorialAgent.NavMeshAgent.Warp(storyPoint.Position);
					}
					else
					{
						tutorialAgent.Position = storyPoint.Position;
					}
					tutorialAgent.Rotation = storyPoint.Rotation;
					if (tutorialAgent.TutorialType != AIProject.Definitions.Tutorial.ActionType.WaitAtAgit)
					{
						tutorialAgent.TargetStoryPoint = storyPoint;
						tutorialAgent.ChangeTutorialBehavior(AIProject.Definitions.Tutorial.ActionType.WaitAtAgit);
					}
				}
				Transform transform3 = merchant.Locomotor.transform;
				transform3.LookAt(player.Position);
				Vector3 eulerAngles = transform3.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				transform3.eulerAngles = eulerAngles;
				player.CommCompanion = merchant;
				player.PlayerController.ChangeState("Communication");
				if (player.CameraControl.ShotType == ShotType.PointOfView)
				{
					player.CameraControl.XAxisValue = player.Rotation.eulerAngles.y;
					player.CameraControl.YAxisValue = 0.5f;
				}
				else
				{
					player.CameraControl.XAxisValue = player.Rotation.eulerAngles.y - 30f;
					player.CameraControl.YAxisValue = 0.6f;
				}
				CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
				player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
				ADV.ChangeADVCamera(merchant);
				Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
				{
					player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
				});
				Transform transform4 = player.CameraControl.CameraComponent.transform;
				merchant.SetLookPtn(1, 3);
				merchant.SetLookTarget(1, 0, transform4);
				Observable.EveryUpdate().TakeUntilDestroy(player).Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).Subscribe(delegate(long _)
				{
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				});
			});
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x00277944 File Offset: 0x00275D44
		private void EndMerchantStory0()
		{
			this._eventPoint.SetDedicatedNumber(1);
			MapUIContainer.SetVisibleHUD(true);
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			instance.SetActiveMapEffect(true);
			MerchantActor merchant = instance.Merchant;
			MerchantData merchantData = merchant.MerchantData;
			if (merchant != null)
			{
				if (merchantData != null)
				{
					merchantData.Unlock = true;
				}
				merchant.SetLookPtn(0, 3);
				merchant.SetLookTarget(0, 0, null);
				merchant.ChangeBehavior(merchant.LastNormalMode);
			}
			AgentActor tutorialAgent = instance.TutorialAgent;
			if (tutorialAgent != null)
			{
				tutorialAgent.ChangeFirstNormalBehavior();
				instance.TutorialAgent = null;
				Manager.Map.SetTutorialProgressAndUIUpdate(16);
			}
			PlayerActor player = instance.Player;
			if (player != null)
			{
				if (Config.GraphicData.CharasEntry[0])
				{
					player.CameraControl.Mode = CameraMode.Normal;
					player.PlayerController.ChangeState("Idle");
					Observable.EveryUpdate().SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending).Take(1).Subscribe(delegate(long _)
					{
						player.AddTutorialUI(Popup.Tutorial.Type.Girl, false);
						player.PlayerController.ChangeState("Normal");
					});
				}
				else
				{
					player.PlayerController.ChangeState("Idle");
					Singleton<Manager.Map>.Instance.ApplyConfig(delegate
					{
						CinemachineBlendDefinition.Style prevStyle = player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style;
						player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
						player.CameraControl.Mode = CameraMode.Normal;
						Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
						{
							player.CameraControl.CinemachineBrain.m_DefaultBlend.m_Style = prevStyle;
						});
					}, delegate
					{
						player.AddTutorialUI(Popup.Tutorial.Type.Girl, false);
						player.PlayerController.ChangeState("Normal");
					});
				}
			}
			Singleton<Manager.Map>.Instance.SetBaseOpenState(-1, true);
			instance.Simulator.EnabledTimeProgression = true;
		}

		// Token: 0x040053B0 RID: 21424
		private EventPoint _eventPoint;

		// Token: 0x040053B1 RID: 21425
		private CollisionState _collisionState;

		// Token: 0x02000BF1 RID: 3057
		private class PackData : CharaPackData
		{
		}
	}
}
