using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.SaveData;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE6 RID: 3558
	public class CatchAnimal : PlayerStateBase
	{
		// Token: 0x06006DFE RID: 28158 RVA: 0x002EFD40 File Offset: 0x002EE140
		protected override void OnAwake(PlayerActor player)
		{
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			this.isEnd = false;
			this.isWait = null;
			this.isNextEnabled = false;
			if (this.disposable != null)
			{
				this.disposable.Clear();
			}
			this.disposable = new CompositeDisposable();
			if ((this.hasAnimal = player.Animal) == null)
			{
				this.isEnd = true;
				this.ToErrorEnd(player);
				return;
			}
			this.animalName = this.hasAnimal.Name;
			this.isCameraBlend = true;
			this.getPercent = 30f;
			if (this.hasAnimal is WildGround)
			{
				this.getPercent = (this.hasAnimal as WildGround).GetPercent;
			}
			float num = UnityEngine.Random.Range(0f, 100f);
			this.getAnimalFlag = (num <= this.getPercent);
			if (this.getAnimalFlag && Singleton<Manager.Resources>.IsInstance() && Singleton<Map>.IsInstance())
			{
				PlayerActor player2 = Singleton<Map>.Instance.Player;
				List<StuffItem> list;
				if (player2 == null)
				{
					list = null;
				}
				else
				{
					PlayerData playerData = player2.PlayerData;
					list = ((playerData != null) ? playerData.ItemList : null);
				}
				List<StuffItem> list2 = list;
				if (list2 != null)
				{
					this.getIteminfo = this.hasAnimal.ItemInfo;
					if (this.getIteminfo != null)
					{
						this.addItem = new StuffItem(this.getIteminfo.CategoryID, this.getIteminfo.ID, 1);
						list2.AddItem(this.addItem);
					}
				}
			}
			if (Singleton<Map>.IsInstance())
			{
				PlayerActor player3 = Singleton<Map>.Instance.Player;
				List<StuffItem> list3;
				if (player3 == null)
				{
					list3 = null;
				}
				else
				{
					PlayerData playerData2 = player3.PlayerData;
					list3 = ((playerData2 != null) ? playerData2.ItemList : null);
				}
				List<StuffItem> list4 = list3;
				if (list4 != null && this.hasAnimal is WildGround)
				{
					WildGround wildGround = this.hasAnimal as WildGround;
					ItemIDKeyPair getItemID = wildGround.GetItemID;
					list4.RemoveItem(new StuffItem(getItemID.categoryID, getItemID.itemID, 1));
				}
			}
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			if (this.hasAnimal.AnimalType == AnimalTypes.Cat || this.hasAnimal.AnimalType == AnimalTypes.Chicken)
			{
				this.Initialize(player);
				return;
			}
			this.ToErrorEnd(player);
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x002EFF80 File Offset: 0x002EE380
		private void ToErrorEnd(PlayerActor player)
		{
			Observable.NextFrame(FrameCountType.Update).TakeUntilDestroy(player).Subscribe(delegate(Unit _)
			{
				player.PlayerController.ChangeState("Normal");
			}).AddTo(this.disposable);
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x002EFFC8 File Offset: 0x002EE3C8
		private void ToEnd(PlayerActor player)
		{
			Observable.FromCoroutine(() => this.End(player), false).TakeUntilDestroy(player).Subscribe<Unit>().AddTo(this.disposable);
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x002F0018 File Offset: 0x002EE418
		private void Initialize(PlayerActor player)
		{
			this.hasAnimal.SetState(AnimalState.WithPlayer, null);
			player.CameraControl.CrossFade.FadeStart(-1f);
			Vector3 eulerAngles = Quaternion.LookRotation(this.hasAnimal.Position - player.Position, Vector3.up).eulerAngles;
			eulerAngles.x = (eulerAngles.z = 0f);
			player.Rotation = Quaternion.Euler(eulerAngles);
			this.hasAnimal.SetWithActorGetPoint(player);
			this.hasAnimal.PlayInAnim(AnimationCategoryID.Idle, 0, null);
			int poseID = (!this.getAnimalFlag) ? 1 : 0;
			this.SetPlayerAnimationState(player, poseID);
			this.onEndGetAnimal = new Subject<Unit>();
			this.onEndGetAnimal.TakeUntilDestroy(player).Take(1).Subscribe(delegate(Unit _)
			{
				this.hasAnimal.Destroy();
				this.ToEnd(player);
			}).AddTo(this.disposable);
			this.onEndInAnimation = new Subject<Unit>();
			this.onEndInAnimation.TakeUntilDestroy(player).Take(1).Subscribe(delegate(Unit _)
			{
				if (this.getAnimalFlag)
				{
					this.hasAnimal.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
					this.hasAnimal.SetFloat(Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo.LocomotionParamName, 0.5f);
					this.isWait = ((PlayerActor actor) => !<Initialize>c__AnonStorey.isNextEnabled);
					this.onEndAction = this.onEndGetAnimal;
					IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(1f, true).FrameTimeInterval(false).Publish<TimeInterval<float>>();
					connectableObservable.Connect();
					Vector3 _start = this.hasAnimal.Position;
					Vector3 _end = _start + this.hasAnimal.Forward * 1f;
					connectableObservable.TakeUntilDestroy(player).Subscribe(delegate(TimeInterval<float> x)
					{
						<Initialize>c__AnonStorey.hasAnimal.Position = Vector3.Lerp(_start, _end, x.Value);
					}).AddTo(this.disposable);
					Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
					{
						connectableObservable
					}).TakeUntilDestroy(player).Subscribe(delegate(TimeInterval<float>[] __)
					{
						<Initialize>c__AnonStorey.isNextEnabled = true;
					}).AddTo(this.disposable);
					if (Singleton<Manager.Resources>.IsInstance())
					{
						SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
						AnimalDefinePack.SoundIDInfo soundID = Singleton<Manager.Resources>.Instance.AnimalDefinePack.SoundID;
						AudioSource audioSource = null;
						AnimalTypes animalType = this.hasAnimal.AnimalType;
						if (animalType != AnimalTypes.Cat)
						{
							if (animalType == AnimalTypes.Chicken)
							{
								audioSource = soundPack.Play(soundID.GetChicken, Sound.Type.GameSE3D, 0f);
							}
						}
						else
						{
							audioSource = soundPack.Play(soundID.GetCat, Sound.Type.GameSE3D, 0f);
						}
						if (audioSource != null)
						{
							audioSource.Stop();
							audioSource.transform.SetPositionAndRotation(this.hasAnimal.Position, this.hasAnimal.Rotation);
							audioSource.Play();
						}
					}
				}
				else
				{
					if (this.hasAnimal is WildGround)
					{
						(this.hasAnimal as WildGround).StartAvoid(player.Position, null);
					}
					else
					{
						this.hasAnimal.BadMood = true;
						this.hasAnimal.PlayOutAnim(delegate()
						{
							this.hasAnimal.SetState(AnimalState.Locomotion, null);
						});
					}
					this.isWait = ((PlayerActor actor) => true);
					this.ToEnd(player);
				}
			}).AddTo(this.disposable);
			this.onStart = new Subject<Unit>();
			this.onStart.TakeUntilDestroy(player).Take(1).Subscribe(delegate(Unit _)
			{
				player.Animation.PlayInAnimation(this.playerPlayState.MainStateInfo.InStateInfo.EnableFade, this.playerPlayState.MainStateInfo.InStateInfo.FadeSecond, this.playerPlayState.MainStateInfo.FadeOutTime, this.playerPlayState.Layer);
				this.isWait = ((PlayerActor actor) => actor.Animation.PlayingInAnimation);
				this.onEndAction = this.onEndInAnimation;
			}).AddTo(this.disposable);
			this.onEndAction = this.onStart;
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x002F01C0 File Offset: 0x002EE5C0
		private void SetPlayerAnimationState(PlayerActor _player, int _poseID)
		{
			this.playerPlayState = null;
			Dictionary<int, Dictionary<int, PoseKeyPair>> playerCatchAnimalPoseTable = Singleton<Manager.Resources>.Instance.AnimalTable.PlayerCatchAnimalPoseTable;
			Dictionary<int, PoseKeyPair> dictionary;
			PoseKeyPair poseKeyPair;
			if (playerCatchAnimalPoseTable.TryGetValue((int)_player.ChaControl.sex, out dictionary) && dictionary.TryGetValue(_poseID, out poseKeyPair))
			{
				PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)_player.ChaControl.sex][poseKeyPair.postureID][poseKeyPair.poseID];
				_player.Animation.InitializeStates(this.playerPlayState = playState);
				_player.Animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
				_player.LoadEventItems(playState);
			}
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x002F027C File Offset: 0x002EE67C
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			info.move = Vector3.zero;
			if (this.isEnd)
			{
				return;
			}
			if (this.isCameraBlend)
			{
				this.isCameraBlend = actor.CameraControl.CinemachineBrain.IsBlending;
				if (this.isCameraBlend)
				{
					return;
				}
			}
			bool? flag = (this.isWait != null) ? new bool?(this.isWait(actor)) : null;
			if (flag != null && flag.Value)
			{
				return;
			}
			if (this.onEndAction != null)
			{
				this.onEndAction.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006E04 RID: 28164 RVA: 0x002F0334 File Offset: 0x002EE734
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			this.isEnd = true;
			player.Animation.PlayOutAnimation(this.playerPlayState.MainStateInfo.OutStateInfo.EnableFade, this.playerPlayState.MainStateInfo.OutStateInfo.FadeSecond, this.playerPlayState.Layer);
			while (player.Animation.PlayingOutAnimation)
			{
				yield return null;
			}
			this.OnEndSet(player);
			yield break;
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x002F0358 File Offset: 0x002EE758
		private void OnEndSet(PlayerActor player)
		{
			if (this.getAnimalFlag)
			{
				if (this.getIteminfo != null && this.addItem != null)
				{
					MapUIContainer.AddSystemItemLog(this.getIteminfo, this.addItem.Count, true);
				}
				else
				{
					MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
				}
			}
			player.ClearItems();
			player.Animal = null;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			player.PlayerController.ChangeState("Normal");
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x002F03CF File Offset: 0x002EE7CF
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x002F03DE File Offset: 0x002EE7DE
		protected override void OnRelease(PlayerActor player)
		{
			if (this.disposable != null)
			{
				this.disposable.Clear();
			}
			this.disposable = null;
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x002F0400 File Offset: 0x002EE800
		~CatchAnimal()
		{
			if (this.disposable != null)
			{
				this.disposable.Clear();
			}
			this.disposable = null;
		}

		// Token: 0x04005B61 RID: 23393
		private bool isEnd;

		// Token: 0x04005B62 RID: 23394
		private AnimalBase hasAnimal;

		// Token: 0x04005B63 RID: 23395
		private float getPercent;

		// Token: 0x04005B64 RID: 23396
		private bool getAnimalFlag;

		// Token: 0x04005B65 RID: 23397
		private StuffItemInfo getIteminfo;

		// Token: 0x04005B66 RID: 23398
		private StuffItem addItem;

		// Token: 0x04005B67 RID: 23399
		private string animalName = string.Empty;

		// Token: 0x04005B68 RID: 23400
		private bool isCameraBlend;

		// Token: 0x04005B69 RID: 23401
		private PlayState playerPlayState;

		// Token: 0x04005B6A RID: 23402
		private Subject<Unit> onStart;

		// Token: 0x04005B6B RID: 23403
		private Subject<Unit> onEndInAnimation;

		// Token: 0x04005B6C RID: 23404
		private Subject<Unit> onEndGetAnimal;

		// Token: 0x04005B6D RID: 23405
		private Subject<Unit> onEndAction;

		// Token: 0x04005B6E RID: 23406
		private CompositeDisposable disposable;

		// Token: 0x04005B6F RID: 23407
		private Func<PlayerActor, bool> isWait;

		// Token: 0x04005B70 RID: 23408
		private bool isNextEnabled;
	}
}
