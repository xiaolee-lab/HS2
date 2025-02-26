using System;
using System.Collections;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E05 RID: 3589
	public class Normal : PlayerStateBase
	{
		// Token: 0x06006F0B RID: 28427 RVA: 0x002F8ACC File Offset: 0x002F6ECC
		protected override void OnAwake(PlayerActor player)
		{
			player.Animation.RefsActAnimInfo = true;
			player.ActivateTransfer();
			if (player.HandsHolder.EnabledHolding)
			{
				player.HandsHolder.EnabledHolding = false;
				player.HandsHolder.EnabledHolding = true;
			}
			player.SetActiveOnEquipedItem(true);
			player.ResetCoolTime();
			if (player.CameraControl.Mode == CameraMode.ActionNotMove || player.CameraControl.Mode == CameraMode.ActionFreeLook)
			{
				player.CameraControl.Mode = CameraMode.Normal;
				player.CameraControl.RecoverShotType();
			}
			if (Singleton<Map>.IsInstance())
			{
				Singleton<Map>.Instance.CheckTutorialState(player);
			}
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x002F8B6E File Offset: 0x002F6F6E
		public override void Release(Actor actor, EventType type)
		{
			if (Singleton<GameCursor>.IsInstance())
			{
				Singleton<GameCursor>.Instance.SetCursorLock(false);
			}
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x002F8B85 File Offset: 0x002F6F85
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x002F8B94 File Offset: 0x002F6F94
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			Singleton<Map>.Instance.CheckStoryProgress();
			if (actor == null)
			{
				return;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			if (instance.State == Manager.Input.ValidType.Action)
			{
				Transform transform = actor.CameraControl.transform;
				Vector2 moveAxis = instance.MoveAxis;
				Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
				Vector3 move = rotation * Vector3.ClampMagnitude(new Vector3(moveAxis.x, 0f, moveAxis.y), 1f);
				info.move = move;
				info.lookPos = actor.transform.position + transform.forward * 100f;
				StuffItem equipedLampItem = actor.PlayerData.EquipedLampItem;
				if (Mathf.Approximately(moveAxis.sqrMagnitude, 0f) && (equipedLampItem == null || !Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.ContainsLightItem(equipedLampItem)))
				{
					this._elapsedTimeOnLeft += Time.deltaTime;
					if (this._elapsedTimeOnLeft > Singleton<Manager.Resources>.Instance.LocomotionProfile.TimeToLeftState)
					{
						actor.Controller.ChangeState("Houchi");
					}
				}
				else
				{
					this._elapsedTimeOnLeft = 0f;
				}
			}
			else
			{
				info.move = Vector3.zero;
				Transform transform2 = actor.CameraControl.transform;
				info.lookPos = actor.transform.position + transform2.forward * 100f;
			}
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x002F8D30 File Offset: 0x002F7130
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005BD9 RID: 23513
		private float _elapsedTimeOnLeft;
	}
}
