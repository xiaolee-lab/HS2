using System;
using System.Collections;
using AIChara;
using Illusion.Extensions;
using IllusionUtility.SetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009FC RID: 2556
	public class CvsC_CreateCoordinateFile : MonoBehaviour
	{
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06004B89 RID: 19337 RVA: 0x001CDB8F File Offset: 0x001CBF8F
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x001CDB96 File Offset: 0x001CBF96
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x001CDBA3 File Offset: 0x001CBFA3
		public void CreateCoordinateFile(string _savePath, string _coordinateName, bool _overwrite)
		{
			this.saveCoordinateName = _coordinateName;
			this.saveCoordinateFileName = _savePath;
			this.saveOverwrite = _overwrite;
			base.StartCoroutine(this.CreateCoordinateFileCoroutine());
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x001CDBC8 File Offset: 0x001CBFC8
		public void CreateCoordinateFileBefore()
		{
			if (this.customBase.customCtrl.draw3D)
			{
				this.custom3DRender.update = false;
				RenderTexture renderTexture = this.custom3DRender.GetRenderTexture();
				this.imgDummy.texture = renderTexture;
			}
			else
			{
				this.custom2DRender.update = false;
				RenderTexture renderTexture2 = this.custom2DRender.GetRenderTexture();
				this.imgDummy.texture = renderTexture2;
			}
			this.imgDummy.transform.parent.gameObject.SetActiveIfDifferent(true);
			this.chaCtrl.ChangeSettingMannequin(true);
			this.customDrawMenu.ChangeClothesStateForCapture(true);
			Texture2D tex = PngAssist.LoadTexture2DFromAssetBundle("custom/custom_etc.unity3d", "coordinate_front");
			this.customBase.saveFrameAssist.ChangeSaveFrameTexture(1, tex);
			this.backPoseNo = this.customBase.poseNo;
			if (null != this.chaCtrl.animBody)
			{
				this.backPosePos = this.chaCtrl.getAnimatorStateInfo(0).normalizedTime;
				this.customBase.animationPos = 0f;
				this.customBase.ChangeAnimationNo(0, true);
				this.chaCtrl.resetDynamicBoneAll = true;
			}
			this.customBase.updateCustomUI = true;
			for (int i = 0; i < 20; i++)
			{
				this.customBase.ChangeAcsSlotName(i);
			}
			this.customBase.SetUpdateToggleSetting();
			this.customBase.forceUpdateAcsList = true;
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x001CDD40 File Offset: 0x001CC140
		public IEnumerator CreateCoordinateFileCoroutine()
		{
			this.CreateCoordinateFileBefore();
			yield return new WaitForEndOfFrame();
			ObservableYieldInstruction<byte[]> ret = Observable.FromCoroutine<byte[]>((IObserver<byte[]> res) => this.CreateCoordinatePng(res)).ToYieldInstruction(false);
			yield return ret;
			if (this.saveOverwrite)
			{
				this.chaCtrl.chaFile.coordinate.pngData = ret.Result;
				this.chaCtrl.chaFile.coordinate.coordinateName = this.saveCoordinateName;
				this.chaCtrl.chaFile.coordinate.SaveFile(this.saveCoordinateFileName, (int)Singleton<GameSystem>.Instance.language);
			}
			else
			{
				string str = string.Empty;
				if (this.chaCtrl.sex == 0)
				{
					str = "AISCoordeM_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
				}
				else
				{
					str = "AISCoordeF_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
				}
				string path = string.Empty;
				path = UserData.Path + ((this.chaCtrl.sex != 0) ? "coordinate/female/" : "coordinate/male/") + str + ".png";
				this.chaCtrl.chaFile.coordinate.pngData = ret.Result;
				this.chaCtrl.chaFile.coordinate.coordinateName = this.saveCoordinateName;
				this.chaCtrl.chaFile.coordinate.SaveFile(path, (int)Singleton<GameSystem>.Instance.language);
			}
			this.customBase.updateCvsClothesSaveDelete = true;
			this.customBase.updateCvsClothesLoad = true;
			this.customBase.updateCustomUI = true;
			for (int i = 0; i < 20; i++)
			{
				this.customBase.ChangeAcsSlotName(i);
			}
			this.customBase.SetUpdateToggleSetting();
			this.customBase.forceUpdateAcsList = true;
			yield return null;
			this.objMap3D.SetActiveIfDifferent(this.customBase.customCtrl.draw3D);
			this.objBackCamera.SetActiveIfDifferent(!this.customBase.customCtrl.draw3D);
			this.imgDummy.transform.parent.gameObject.SetActiveIfDifferent(false);
			if (this.customBase.customCtrl.draw3D)
			{
				this.custom3DRender.update = true;
			}
			else
			{
				this.custom2DRender.update = true;
			}
			yield break;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x001CDD5C File Offset: 0x001CC15C
		public IEnumerator CreateCoordinatePng(IObserver<byte[]> observer)
		{
			this.mainCamera.gameObject.SetActiveIfDifferent(false);
			this.objMap3D.SetActiveIfDifferent(false);
			this.objBackCamera.SetActiveIfDifferent(false);
			if (this.coordinateCamera)
			{
				this.coordinateCamera.enabled = true;
			}
			if (this.coordinateCamera)
			{
				this.coordinateCamera.gameObject.SetActiveIfDifferent(true);
				if (this.chaCtrl.sex == 0)
				{
					this.coordinateCamera.transform.SetPosition(0f, 10.5f, 46.9f);
					this.coordinateCamera.transform.SetRotation(2.62f, -180f, 0f);
				}
				else
				{
					this.coordinateCamera.transform.SetPosition(0f, 10.2f, 45.6f);
					this.coordinateCamera.transform.SetRotation(2.62f, -180f, 0f);
				}
			}
			this.customBase.drawSaveFrameTop = true;
			bool drawSaveFrameFront = this.customBase.drawSaveFrameFront;
			this.customBase.drawSaveFrameFront = true;
			byte[] value = this.customBase.customCtrl.customCap.CapCoordinateCard(false, this.customBase.saveFrameAssist, this.coordinateCamera);
			this.customBase.saveFrameAssist.ChangeSaveFrameFront(2, true);
			this.customBase.drawSaveFrameFront = drawSaveFrameFront;
			this.customBase.drawSaveFrameTop = false;
			if (this.coordinateCamera)
			{
				this.coordinateCamera.enabled = false;
			}
			if (this.coordinateCamera)
			{
				this.coordinateCamera.gameObject.SetActiveIfDifferent(false);
			}
			this.mainCamera.gameObject.SetActiveIfDifferent(true);
			this.chaCtrl.ChangeSettingMannequin(false);
			this.customDrawMenu.ChangeClothesStateForCapture(false);
			if (null != this.chaCtrl.animBody)
			{
				this.customBase.animationPos = this.backPosePos;
				this.customBase.ChangeAnimationNo(this.backPoseNo, false);
				this.chaCtrl.resetDynamicBoneAll = true;
			}
			observer.OnNext(value);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x0400456C RID: 17772
		[SerializeField]
		private RawImage imgDummy;

		// Token: 0x0400456D RID: 17773
		[SerializeField]
		private CustomRender custom2DRender;

		// Token: 0x0400456E RID: 17774
		[SerializeField]
		private CustomRender custom3DRender;

		// Token: 0x0400456F RID: 17775
		[SerializeField]
		private CustomDrawMenu customDrawMenu;

		// Token: 0x04004570 RID: 17776
		[SerializeField]
		private GameObject objMap3D;

		// Token: 0x04004571 RID: 17777
		[SerializeField]
		private GameObject objBackCamera;

		// Token: 0x04004572 RID: 17778
		[SerializeField]
		private Camera mainCamera;

		// Token: 0x04004573 RID: 17779
		[SerializeField]
		private Camera coordinateCamera;

		// Token: 0x04004574 RID: 17780
		private string saveCoordinateName = string.Empty;

		// Token: 0x04004575 RID: 17781
		private string saveCoordinateFileName = string.Empty;

		// Token: 0x04004576 RID: 17782
		private bool saveOverwrite;

		// Token: 0x04004577 RID: 17783
		private int backPoseNo;

		// Token: 0x04004578 RID: 17784
		private float backPosePos;
	}
}
