using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Player
{
	// Token: 0x02000E06 RID: 3590
	public class Onbu : PlayerStateBase
	{
		// Token: 0x06006F11 RID: 28433 RVA: 0x002F8DB4 File Offset: 0x002F71B4
		protected override void OnAwake(PlayerActor player)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Sprite icon;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(instance.PlayerProfile.CommonActionIconID, out icon);
			this._cancelCommandInfo = new CommandLabel.CommandInfo
			{
				Text = "下ろす",
				Icon = icon,
				TargetSpriteInfo = null,
				Transform = null,
				Event = delegate
				{
					this.ExitState();
				}
			};
			this._uncancelableCommandInfo = new CommandLabel.CommandInfo
			{
				Text = null,
				Icon = null,
				TargetSpriteInfo = null,
				Transform = null,
				Condition = null,
				Event = delegate
				{
					this.OutputWarningMessage();
				}
			};
			player.Mode = Desire.ActionType.Onbu;
			player.CameraControl.CrossFade.FadeStart(-1f);
			AgentActor agentPartner = player.AgentPartner;
			agentPartner.Partner = player;
			agentPartner.IsSlave = true;
			agentPartner.Animation.StopAllAnimCoroutine();
			agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Onbu);
			agentPartner.NavMeshAgent.enabled = false;
			player.PlayerController.CommandArea.RefreshCommands();
			if (player.PlayerController.PrevStateName == "Normal" || player.PlayerController.PrevStateName == "Houchi")
			{
				this.ActivateTransfer(player, agentPartner);
			}
			else
			{
				this.ActivateTransferImmediate(player, agentPartner);
			}
			this._onEndInAnimationDisposable = this._onEndInAnimation.Take(1).Subscribe(delegate(Unit _)
			{
				this._activeSubjectCommandDisposable = this._activeSubjectCommand.DistinctUntilChanged<bool>().Subscribe(delegate(bool isOn)
				{
					if (isOn)
					{
						if (MapUIContainer.CommandLabel.SubjectCommand != this._cancelCommandInfo)
						{
							MapUIContainer.CommandLabel.SubjectCommand = this._cancelCommandInfo;
							player.PlayerController.CommandArea.RefreshCommands();
						}
					}
					else if (MapUIContainer.CommandLabel.SubjectCommand != this._uncancelableCommandInfo)
					{
						MapUIContainer.CommandLabel.SubjectCommand = this._uncancelableCommandInfo;
						player.PlayerController.CommandArea.RefreshCommands();
					}
				});
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			});
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this._layer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HLayer;
			this._tag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.OnbuMeshTag;
			if (Singleton<Manager.Map>.IsInstance())
			{
				Singleton<Manager.Map>.Instance.CheckTutorialState(player);
			}
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x002F8FD8 File Offset: 0x002F73D8
		private void ActivateTransfer(PlayerActor player, Actor partner)
		{
			EquipEventItemInfo itemInfo = null;
			PlayState playState;
			this.LoadLocomotionAnimation(player, out playState, ref itemInfo);
			player.ResetEquipEventItem(itemInfo);
			partner.Position = player.Position;
			partner.Rotation = player.Rotation;
			int onbuStateID = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.OnbuStateID;
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.PlayerLocomotionStateTable[(int)player.ChaControl.sex][onbuStateID];
			ActorAnimation animation = player.Animation;
			ActorAnimation animation2 = partner.Animation;
			AnimatorStateInfo currentAnimatorStateInfo = animation.Animator.GetCurrentAnimatorStateInfo(0);
			if (playState != null)
			{
				animation.InitializeStates(playState);
				animation2.InitializeStates(playState2);
				bool flag = false;
				foreach (PlayState.Info info in playState.MainStateInfo.InStateInfo.StateInfos)
				{
					if (currentAnimatorStateInfo.shortNameHash == info.ShortNameStateHash)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (playState.MaskStateInfo.layer > 0)
					{
						float layerWeight = animation.Animator.GetLayerWeight(playState.MaskStateInfo.layer);
						if (layerWeight == 0f)
						{
							flag = false;
						}
					}
					else
					{
						for (int j = 1; j < animation.Animator.layerCount; j++)
						{
							float layerWeight2 = animation.Animator.GetLayerWeight(j);
							if (layerWeight2 > 0f)
							{
								flag = false;
								break;
							}
						}
					}
				}
				if (flag)
				{
					animation.InStates.Clear();
					animation.OutStates.Clear();
					animation.ActionStates.Clear();
					animation2.InStates.Clear();
					animation2.OutStates.Clear();
					animation2.ActionStates.Clear();
				}
				else
				{
					int layer = playState.Layer;
					if (animation.RefsActAnimInfo)
					{
						animation.StopAllAnimCoroutine();
						animation.PlayInLocoAnimation(animation.AnimInfo.outEnableBlend, animation.AnimInfo.outBlendSec, layer);
						animation2.StopAllAnimCoroutine();
						animation2.PlayInLocoAnimation(animation.AnimInfo.outEnableBlend, animation.AnimInfo.outBlendSec, layer);
						animation.RefsActAnimInfo = false;
					}
					else
					{
						bool enableFade = playState.MainStateInfo.InStateInfo.EnableFade;
						float fadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
						animation.StopAllAnimCoroutine();
						animation.PlayInLocoAnimation(enableFade, fadeSecond, layer);
						animation2.StopAllAnimCoroutine();
						animation2.PlayInLocoAnimation(enableFade, fadeSecond, layer);
					}
				}
			}
			else
			{
				for (int k = 1; k < animation.Animator.layerCount; k++)
				{
					animation.Animator.SetLayerWeight(k, 0f);
				}
				animation.InitializeStates(playState2);
				animation2.InitializeStates(playState2);
				int layer2 = playState2.Layer;
				if (animation.RefsActAnimInfo)
				{
					animation.StopAllAnimCoroutine();
					animation.PlayInLocoAnimation(animation.AnimInfo.endEnableBlend, animation.AnimInfo.endBlendSec, layer2);
					animation2.StopAllAnimCoroutine();
					animation2.PlayInLocoAnimation(animation.AnimInfo.inEnableBlend, animation.AnimInfo.inBlendSec, layer2);
					animation.RefsActAnimInfo = false;
				}
				else
				{
					ActorAnimInfo actorAnimInfo = new ActorAnimInfo
					{
						layer = playState2.Layer,
						inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
						inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond
					};
					player.Animation.AnimInfo = actorAnimInfo;
					ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
					bool inEnableBlend = actorAnimInfo2.inEnableBlend;
					float inBlendSec = actorAnimInfo2.inBlendSec;
					animation.StopAllAnimCoroutine();
					animation.PlayInLocoAnimation(inEnableBlend, inBlendSec, layer2);
					animation2.StopAllAnimCoroutine();
					animation2.PlayInLocoAnimation(inEnableBlend, inBlendSec, layer2);
				}
			}
			if (player.NavMeshAgent.isStopped)
			{
				player.NavMeshAgent.isStopped = false;
			}
			if (player.IsKinematic)
			{
				player.IsKinematic = false;
			}
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x002F9418 File Offset: 0x002F7818
		private void ActivateTransferImmediate(PlayerActor player, Actor partner)
		{
			EquipEventItemInfo itemInfo = null;
			PlayState playState;
			this.LoadLocomotionAnimation(player, out playState, ref itemInfo);
			player.ResetEquipEventItem(itemInfo);
			partner.Position = player.Position;
			partner.Rotation = player.Rotation;
			int onbuStateID = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.OnbuStateID;
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.PlayerLocomotionStateTable[(int)player.ChaControl.sex][onbuStateID];
			ActorAnimation animation = player.Animation;
			ActorAnimation animation2 = partner.Animation;
			animation.InStates.Clear();
			animation.OutStates.Clear();
			animation.ActionStates.Clear();
			partner.Animation.InStates.Clear();
			partner.Animation.OutStates.Clear();
			partner.Animation.ActionStates.Clear();
			PlayState.Info element = playState2.MainStateInfo.InStateInfo.StateInfos.GetElement(playState2.MainStateInfo.InStateInfo.StateInfos.Length - 1);
			if (playState != null)
			{
				PlayState.Info element2 = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1);
				player.Animation.InStates.Enqueue(element2);
				if (!playState.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
				{
					foreach (PlayState.Info item in playState.MainStateInfo.OutStateInfo.StateInfos)
					{
						player.Animation.OutStates.Enqueue(item);
					}
				}
				partner.Animation.InStates.Enqueue(element);
				if (!playState2.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
				{
					foreach (PlayState.Info item2 in playState2.MainStateInfo.OutStateInfo.StateInfos)
					{
						partner.Animation.OutStates.Enqueue(item2);
					}
				}
				ActorAnimInfo animInfo = player.Animation.AnimInfo;
				int layer = playState.Layer;
				player.Animation.StopAllAnimCoroutine();
				player.Animation.PlayInLocoAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, layer);
				partner.Animation.StopAllAnimCoroutine();
				partner.Animation.PlayInLocoAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, layer);
			}
			else
			{
				for (int k = 1; k < animation.Animator.layerCount; k++)
				{
					animation.Animator.SetLayerWeight(k, 0f);
				}
				player.Animation.InStates.Enqueue(element);
				if (!playState2.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
				{
					foreach (PlayState.Info item3 in playState2.MainStateInfo.OutStateInfo.StateInfos)
					{
						player.Animation.OutStates.Enqueue(item3);
					}
				}
				partner.Animation.InStates.Enqueue(element);
				if (!playState2.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
				{
					foreach (PlayState.Info item4 in playState2.MainStateInfo.OutStateInfo.StateInfos)
					{
						partner.Animation.OutStates.Enqueue(item4);
					}
				}
				ActorAnimInfo animInfo2 = player.Animation.AnimInfo;
				int layer2 = playState2.Layer;
				player.Animation.StopAllAnimCoroutine();
				player.Animation.PlayInLocoAnimation(animInfo2.inEnableBlend, animInfo2.inBlendSec, layer2);
				partner.Animation.StopAllAnimCoroutine();
				partner.Animation.PlayInLocoAnimation(animInfo2.inEnableBlend, animInfo2.inBlendSec, layer2);
			}
			if (player.NavMeshAgent.isStopped)
			{
				player.NavMeshAgent.isStopped = false;
			}
			if (player.IsKinematic)
			{
				player.IsKinematic = false;
			}
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x002F985C File Offset: 0x002F7C5C
		private void LoadLocomotionAnimation(PlayerActor player, out PlayState info, ref EquipEventItemInfo itemInfo)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			LocomotionProfile locomotionProfile = instance.LocomotionProfile;
			PlayerProfile playerProfile = instance.PlayerProfile;
			StuffItem equipedLampItem = player.PlayerData.EquipedLampItem;
			CommonDefine.ItemIDDefines itemIDDefine = instance.CommonDefine.ItemIDDefine;
			if (equipedLampItem != null)
			{
				ItemIDKeyPair torchID = itemIDDefine.TorchID;
				ItemIDKeyPair flashlightID = itemIDDefine.FlashlightID;
				ItemIDKeyPair maleLampID = itemIDDefine.MaleLampID;
				if (equipedLampItem.CategoryID == torchID.categoryID && equipedLampItem.ID == torchID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)player.ChaControl.sex][playerProfile.PoseIDData.TorchOnbuLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[torchID.categoryID][torchID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
				if (equipedLampItem.CategoryID == maleLampID.categoryID && equipedLampItem.ID == maleLampID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)player.ChaControl.sex][playerProfile.PoseIDData.LampOnbuLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[maleLampID.categoryID][maleLampID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
				if (equipedLampItem.CategoryID == flashlightID.categoryID && equipedLampItem.ID == flashlightID.itemID)
				{
					info = instance.Animation.PlayerLocomotionStateTable[(int)player.ChaControl.sex][playerProfile.PoseIDData.TorchOnbuLocoID];
					itemInfo = instance.GameInfo.CommonEquipEventItemTable[flashlightID.categoryID][flashlightID.itemID];
					itemInfo.ParentName = instance.LocomotionProfile.PlayerLocoItemParentName;
					return;
				}
			}
			info = null;
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x002F9A58 File Offset: 0x002F7E58
		private void ExitState()
		{
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Observable.FromCoroutine(delegate()
			{
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				return player.Controller.State.End(player);
			}, false).Take(1).Subscribe(delegate(Unit __)
			{
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				player.CameraControl.CrossFade.FadeStart(-1f);
				player.PlayerController.ChangeState("Normal");
				player.CameraControl.Mode = CameraMode.Normal;
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
				AgentActor agentPartner = player.AgentPartner;
				agentPartner.ChangeBehavior(agentPartner.PrevMode);
				agentPartner.Partner = null;
				agentPartner.IsSlave = false;
				agentPartner.NavMeshAgent.enabled = true;
				Vector3 point = player.Position + player.Back * 10f;
				LayerMask hlayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HLayer;
				string onbuMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.OnbuMeshTag;
				GameObject[] array = GameObject.FindGameObjectsWithTag(onbuMeshTag);
				List<MeshCollider> list = ListPool<MeshCollider>.Get();
				foreach (GameObject gameObject in array)
				{
					MeshCollider[] componentsInChildren = gameObject.GetComponentsInChildren<MeshCollider>();
					list.AddRange(componentsInChildren);
				}
				float num = float.PositiveInfinity;
				Vector3 vector = Vector3.zero;
				foreach (MeshCollider collider in list)
				{
					Vector3 vector2 = collider.NearestVertexTo(point);
					float num2 = Vector3.Distance(player.Position, vector2);
					if (num2 < num)
					{
						num = num2;
						vector = vector2;
					}
				}
				ListPool<MeshCollider>.Release(list);
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(vector, out navMeshHit, 100f, -1))
				{
					vector = navMeshHit.position;
				}
				player.Partner.NavMeshAgent.Warp(vector);
				Vector3 vector3 = vector + player.Partner.Forward * 10f;
				if (NavMesh.SamplePosition(vector3, out navMeshHit, 100f, -1))
				{
					vector3 = navMeshHit.position;
				}
				player.NavMeshAgent.Warp(vector3);
				player.Partner = null;
				agentPartner.IsVisible = true;
			});
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x002F9AB7 File Offset: 0x002F7EB7
		private void OutputWarningMessage()
		{
			MapUIContainer.PushMessageUI("ここに女の子を下ろすことはできません", 2, 0, null);
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x002F9AC6 File Offset: 0x002F7EC6
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x002F9AD8 File Offset: 0x002F7ED8
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			Singleton<Manager.Map>.Instance.CheckStoryProgress();
			if (actor.Animation.PlayingInLocoAnimation)
			{
				return;
			}
			this._onEndInAnimation.OnNext(Unit.Default);
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			if (instance.State == Manager.Input.ValidType.Action)
			{
				Transform transform = actor.CameraControl.transform;
				Vector2 moveAxis = instance.MoveAxis;
				Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
				Vector3 move = rotation * Vector3.ClampMagnitude(new Vector3(moveAxis.x, 0f, moveAxis.y), 1f);
				info.move = move;
				if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse2) || UnityEngine.Input.GetKeyDown(KeyCode.F1))
				{
					actor.PlayerController.ChangeState("Menu");
				}
				else if (UnityEngine.Input.GetKeyDown(KeyCode.M))
				{
					if (Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode == 0)
					{
						actor.Controller.ChangeState("WMap");
					}
					else if (Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode == 2)
					{
						bool miniMap = Config.GameData.MiniMap;
						if (miniMap)
						{
							Singleton<MapUIContainer>.Instance.MinimapUI.OpenMiniMap();
						}
						else
						{
							actor.Controller.ChangeState("WMap");
						}
					}
				}
			}
			else
			{
				info.move = Vector3.zero;
				Transform transform2 = actor.CameraControl.transform;
				info.lookPos = actor.transform.position + transform2.forward * 100f;
			}
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x002F9C8C File Offset: 0x002F808C
		protected override void OnFixedUpdate(PlayerActor player, Actor.InputInfo info)
		{
			if (player.Animation.PlayingInLocoAnimation)
			{
				return;
			}
			this._layer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HLayer;
			this._tag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.OnbuMeshTag;
			Vector3 position = player.Position;
			position.y += 15f;
			int num = Physics.SphereCastNonAlloc(position, 7.5f, Vector3.down, Onbu._raycastHits, 25f, this._layer);
			bool value = false;
			for (int i = 0; i < num; i++)
			{
				if (Onbu._raycastHits[i].collider.tag == this._tag)
				{
					value = true;
					break;
				}
			}
			this._activeSubjectCommand.OnNext(value);
			for (int j = 0; j < num; j++)
			{
				Onbu._raycastHits[j] = default(RaycastHit);
			}
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x002F9D9C File Offset: 0x002F819C
		protected override void OnRelease(PlayerActor player)
		{
			MapUIContainer.CommandLabel.SubjectCommand = null;
			MapUIContainer.CommandLabel.RefreshCommands();
			if (this._onEndInAnimationDisposable != null)
			{
				this._onEndInAnimationDisposable.Dispose();
			}
			if (this._activeSubjectCommandDisposable != null)
			{
				this._activeSubjectCommandDisposable.Dispose();
			}
			player.Mode = Desire.ActionType.Normal;
		}

		// Token: 0x04005BDA RID: 23514
		private Subject<Unit> _onEndInAnimation = new Subject<Unit>();

		// Token: 0x04005BDB RID: 23515
		private Subject<bool> _activeSubjectCommand = new Subject<bool>();

		// Token: 0x04005BDC RID: 23516
		private IDisposable _onEndInAnimationDisposable;

		// Token: 0x04005BDD RID: 23517
		private IDisposable _activeSubjectCommandDisposable;

		// Token: 0x04005BDE RID: 23518
		private CommandLabel.CommandInfo _cancelCommandInfo;

		// Token: 0x04005BDF RID: 23519
		private CommandLabel.CommandInfo _uncancelableCommandInfo;

		// Token: 0x04005BE0 RID: 23520
		private static readonly RaycastHit[] _raycastHits = new RaycastHit[100];

		// Token: 0x04005BE1 RID: 23521
		private LayerMask _layer = 0;

		// Token: 0x04005BE2 RID: 23522
		private string _tag = string.Empty;
	}
}
