using System;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BDE RID: 3038
	[RequireComponent(typeof(DoorPoint))]
	public class HousingDoorAnimation : DoorAnimation
	{
		// Token: 0x06005CFC RID: 23804 RVA: 0x00275B9C File Offset: 0x00273F9C
		protected override void OnStart()
		{
			if (this._animator != null)
			{
				this._animator.runtimeAnimatorController = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(this._animatorID);
				string doorDefaultState = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.DoorDefaultState;
				this._animator.Play(doorDefaultState, 0, 0f);
			}
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x00275C04 File Offset: 0x00274004
		public override void PlayMoveSE(bool open)
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Camera cameraComponent = Map.GetCameraComponent();
			Transform transform = (cameraComponent != null) ? cameraComponent.transform : null;
			if (transform == null)
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			SoundPack.DoorSEIDInfo doorSEIDInfo;
			if (!soundPack.DoorIDTable.TryGetValue(this._doorMatType, out doorSEIDInfo))
			{
				return;
			}
			int clipID = (!open) ? doorSEIDInfo.CloseID : doorSEIDInfo.OpenID;
			SoundPack.Data3D data3D;
			if (!soundPack.TryGetActionSEData(clipID, out data3D))
			{
				return;
			}
			Vector3 position = base.transform.position;
			float num = Mathf.Pow(data3D.MaxDistance + soundPack.Game3DInfo.MarginMaxDistance, 2f);
			float sqrMagnitude = (position - transform.position).sqrMagnitude;
			if (num < sqrMagnitude)
			{
				return;
			}
			AudioSource audioSource = soundPack.Play(data3D, Sound.Type.GameSE3D, 0f);
			if (audioSource == null)
			{
				return;
			}
			audioSource.Stop();
			audioSource.transform.position = position;
			audioSource.Play();
		}

		// Token: 0x04005373 RID: 21363
		[SerializeField]
		private DoorMatType _doorMatType;
	}
}
