using System;
using System.Collections;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF6 RID: 3574
	public class EditPlayer : PlayerStateBase
	{
		// Token: 0x06006E6F RID: 28271 RVA: 0x002F416C File Offset: 0x002F256C
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActivePlayerChangeUI(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			PlayerData playerData = Singleton<Game>.Instance.WorldData.PlayerData;
			this._prevSex = playerData.Sex;
			this._prevChaFileName = playerData.CharaFileName;
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				if (this.CheckChange(player))
				{
					this.StartChange(player);
				}
				else
				{
					player.Controller.ChangeState("DeviceMenu");
				}
			});
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x002F420B File Offset: 0x002F260B
		protected override void OnRelease(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006E71 RID: 28273 RVA: 0x002F4220 File Offset: 0x002F2620
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.PlayerChangeUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E72 RID: 28274 RVA: 0x002F4266 File Offset: 0x002F2666
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E73 RID: 28275 RVA: 0x002F4278 File Offset: 0x002F2678
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006E74 RID: 28276 RVA: 0x002F428C File Offset: 0x002F268C
		private bool CheckChange(PlayerActor player)
		{
			return this._prevSex != player.PlayerData.Sex || this._prevChaFileName != player.PlayerData.CharaFileName;
		}

		// Token: 0x06006E75 RID: 28277 RVA: 0x002F42C4 File Offset: 0x002F26C4
		private void StartChange(PlayerActor player)
		{
			this._onEndFadeIn.Take(1).Subscribe(delegate(Unit _)
			{
				this.Refresh(player);
				this._onEndFadeOut.Take(1).Subscribe(delegate(Unit __)
				{
					player.CurrentDevicePoint = null;
					MapUIContainer.SetVisibleHUDExceptStoryUI(true);
					MapUIContainer.StorySupportUI.Open();
					player.Controller.ChangeState("Normal");
					Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
					Singleton<Manager.Input>.Instance.SetupState();
					player.SetScheduledInteractionState(true);
					player.ReleaseInteraction();
				});
				Observable.Timer(TimeSpan.FromMilliseconds(100.0)).Subscribe(delegate(long __)
				{
					MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, false).Subscribe(delegate(Unit ___)
					{
					}, delegate()
					{
						this._onEndFadeOut.OnNext(Unit.Default);
					});
				});
			});
			IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true);
			source.Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this._onEndFadeIn.OnNext(Unit.Default);
			});
		}

		// Token: 0x06006E76 RID: 28278 RVA: 0x002F4341 File Offset: 0x002F2741
		private void Refresh(PlayerActor player)
		{
			player.ReloadChara();
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x04005B9D RID: 23453
		private Subject<Unit> _onEndMenu = new Subject<Unit>();

		// Token: 0x04005B9E RID: 23454
		private byte _prevSex;

		// Token: 0x04005B9F RID: 23455
		private string _prevChaFileName = string.Empty;

		// Token: 0x04005BA0 RID: 23456
		private Subject<Unit> _onEndFadeIn = new Subject<Unit>();

		// Token: 0x04005BA1 RID: 23457
		private Subject<Unit> _onEndFadeOut = new Subject<Unit>();
	}
}
