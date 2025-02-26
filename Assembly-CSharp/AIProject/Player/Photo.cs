using System;
using Cinemachine;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E0B RID: 3595
	public class Photo : PlayerStateBase
	{
		// Token: 0x06006F43 RID: 28483 RVA: 0x002FB754 File Offset: 0x002F9B54
		protected override void OnAwake(PlayerActor player)
		{
			this._actorCamera = player.CameraControl;
			this._camera = this._actorCamera.CameraComponent;
			this._prevShotType = this._actorCamera.ShotType;
			if (this._prevShotType != ShotType.PointOfView)
			{
				this._actorCamera.ShotType = ShotType.PointOfView;
			}
			this._prevCameraChangeable = this._actorCamera.IsChangeable;
			if (this._prevCameraChangeable)
			{
				this._actorCamera.IsChangeable = false;
			}
			this._prevFieldOfView = this._actorCamera.LensSetting.FieldOfView;
			this._prevDutch = this._actorCamera.LensSetting.Dutch;
			CinemachineFreeLook activeFreeLookCamera = this._actorCamera.ActiveFreeLookCamera;
			this._followRoot = ((activeFreeLookCamera != null) ? activeFreeLookCamera.Follow : null);
			if (this._followRoot != null)
			{
				this._prevCameraLocalPosition = this._followRoot.localPosition;
				this._prevCameraLocalRotation = this._followRoot.localRotation;
			}
			this._cameraLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.MapLayer;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			MapUIContainer.SetVisibleHUD(false);
			if (Singleton<Manager.Resources>.IsInstance())
			{
				LocomotionProfile.LensSettings defaultLensSetting = Singleton<Manager.Resources>.Instance.LocomotionProfile.DefaultLensSetting;
				LocomotionProfile.PhotoShotSetting photoShot = Singleton<Manager.Resources>.Instance.LocomotionProfile.PhotoShot;
				float zoomValue = Mathf.InverseLerp(defaultLensSetting.MinFOV, defaultLensSetting.MaxFOV, this._prevFieldOfView);
				MapUIContainer.PhotoShotUI.SetZoomValue(zoomValue);
			}
			else
			{
				MapUIContainer.PhotoShotUI.SetZoomValue(0.5f);
			}
			MapUIContainer.SetActivePhotoShotUI(true);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				this._updatable = false;
				player.PlayerController.ChangeState("Normal");
			});
			this._updatable = true;
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x002FB928 File Offset: 0x002F9D28
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			if (!this._updatable)
			{
				return;
			}
			if (!Singleton<Manager.Input>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			if (Singleton<Game>.Instance.MapShortcutUI != null)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			LocomotionProfile.PhotoShotSetting photoShot = locomotionProfile.PhotoShot;
			LocomotionProfile.LensSettings defaultLensSetting = locomotionProfile.DefaultLensSetting;
			float num = instance.ScrollValue();
			float num2 = (num + this._prevScrollValue) / 2f;
			this._prevScrollValue = num;
			Transform transform = this._actorCamera.transform;
			Vector2 moveAxis = instance.MoveAxis;
			Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
			Vector3 move = rotation * Vector3.ClampMagnitude(new Vector3(moveAxis.x, 0f, moveAxis.y), 1f);
			info.move = move;
			info.lookPos = player.transform.position + transform.forward * 100f;
			if (instance.IsPressedKey(KeyCode.Mouse1))
			{
				this._onEndAction.OnNext(Unit.Default);
				return;
			}
			if (instance.IsPressedKey(KeyCode.Mouse2))
			{
				this.PlaySE(SoundPack.SystemSE.Photo);
				this._actorCamera.ScreenShot.Capture(string.Empty);
			}
			if (!Mathf.Approximately(num2, 0f))
			{
				float num3 = num2 * photoShot.mouseZoomScale * deltaTime;
				this._offset.z = this._offset.z + num3 * (float)((!this._reversi) ? 1 : -1);
			}
			if (this._followRoot != null && this._camera != null)
			{
				Vector3 offsetMoveValue = photoShot.offsetMoveValue;
				Vector3 maxOffset = photoShot.maxOffset;
				Vector3 minOffset = photoShot.minOffset;
				if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
				{
					this._offset.y = this._offset.y + offsetMoveValue.y * deltaTime;
				}
				if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
				{
					this._offset.y = this._offset.y - offsetMoveValue.y * deltaTime;
				}
				if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
				{
					this._offset.x = this._offset.x + offsetMoveValue.x * deltaTime;
				}
				if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
				{
					this._offset.x = this._offset.x - offsetMoveValue.x * deltaTime;
				}
				this._offset.x = Mathf.Clamp(this._offset.x, minOffset.x, maxOffset.x);
				this._offset.y = Mathf.Clamp(this._offset.y, minOffset.y, maxOffset.y);
				this._offset.z = Mathf.Clamp(this._offset.z, minOffset.z, maxOffset.z);
				this._followRoot.localPosition = this._prevCameraLocalPosition;
				Vector3 vector = this._followRoot.position;
				Vector3 vector2 = vector;
				vector2.y += this._offset.y;
				vector2 += this._camera.transform.right * this._offset.x;
				vector2 = (vector = this.CheckNextPosition(vector, vector2, defaultLensSetting.NearClipPlane, this._cameraLayer));
				vector2 += this._camera.transform.forward * this._offset.z;
				vector = this.CheckNextPosition(vector, vector2, defaultLensSetting.NearClipPlane, this._cameraLayer);
				this._followRoot.position = vector;
			}
			MapUIContainer.PhotoShotUI.SetZoomValue(Mathf.InverseLerp(photoShot.maxOffset.z, photoShot.minOffset.z, this._offset.z));
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x002FBD4E File Offset: 0x002FA14E
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x002FBD60 File Offset: 0x002FA160
		protected override void OnRelease(PlayerActor player)
		{
			if (this._followRoot != null)
			{
				this._followRoot.localPosition = this._prevCameraLocalPosition;
				this._followRoot.localRotation = this._prevCameraLocalRotation;
			}
			if (this._actorCamera != null)
			{
				LensSettings lensSetting = this._actorCamera.LensSetting;
				lensSetting.FieldOfView = this._prevFieldOfView;
				lensSetting.Dutch = this._prevDutch;
				this._actorCamera.LensSetting = lensSetting;
				if (this._prevCameraChangeable)
				{
					this._actorCamera.IsChangeable = true;
				}
				if (this._prevShotType != this._actorCamera.ShotType)
				{
					this._actorCamera.ShotType = this._prevShotType;
				}
			}
			MapUIContainer.SetActivePhotoShotUI(false);
			MapUIContainer.SetVisibleHUD(true);
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x002FBE30 File Offset: 0x002FA230
		private Vector3 CheckNextPosition(Vector3 current, Vector3 next, float nearClip, LayerMask layer)
		{
			Vector3 vector = next - current;
			Vector3 normalized = vector.normalized;
			RaycastHit raycastHit;
			if (Physics.Raycast(current, normalized, out raycastHit, vector.magnitude + nearClip, this._cameraLayer))
			{
				current = raycastHit.point - normalized * nearClip;
			}
			else
			{
				current = next;
			}
			return current;
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x002FBE90 File Offset: 0x002FA290
		private void PlaySE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack == null)
			{
				return;
			}
			soundPack.Play(se);
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x002FBECC File Offset: 0x002FA2CC
		~Photo()
		{
		}

		// Token: 0x04005BFB RID: 23547
		private float _prevFieldOfView = 60f;

		// Token: 0x04005BFC RID: 23548
		private float _prevDutch;

		// Token: 0x04005BFD RID: 23549
		private ShotType _prevShotType;

		// Token: 0x04005BFE RID: 23550
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005BFF RID: 23551
		private bool _updatable;

		// Token: 0x04005C00 RID: 23552
		private bool _reversi;

		// Token: 0x04005C01 RID: 23553
		private float _prevScrollValue;

		// Token: 0x04005C02 RID: 23554
		private bool _prevCameraChangeable = true;

		// Token: 0x04005C03 RID: 23555
		private Vector3 _prevCameraLocalPosition = Vector3.zero;

		// Token: 0x04005C04 RID: 23556
		private Quaternion _prevCameraLocalRotation = Quaternion.identity;

		// Token: 0x04005C05 RID: 23557
		private Transform _followRoot;

		// Token: 0x04005C06 RID: 23558
		private Camera _camera;

		// Token: 0x04005C07 RID: 23559
		private Vector3 _offset = Vector3.zero;

		// Token: 0x04005C08 RID: 23560
		private ActorCameraControl _actorCamera;

		// Token: 0x04005C09 RID: 23561
		private LayerMask _cameraLayer = 0;
	}
}
