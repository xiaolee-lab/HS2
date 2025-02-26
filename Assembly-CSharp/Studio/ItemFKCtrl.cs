using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001260 RID: 4704
	public class ItemFKCtrl : MonoBehaviour
	{
		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06009B7D RID: 39805 RVA: 0x003FB2BE File Offset: 0x003F96BE
		// (set) Token: 0x06009B7E RID: 39806 RVA: 0x003FB2C6 File Offset: 0x003F96C6
		private int count { get; set; }

		// Token: 0x06009B7F RID: 39807 RVA: 0x003FB2D0 File Offset: 0x003F96D0
		public void InitBone(OCIItem _ociItem, Info.ItemLoadInfo _loadInfo, bool _isNew)
		{
			Transform transform = _ociItem.objectItem.transform;
			_ociItem.listBones = new List<OCIChar.BoneInfo>();
			foreach (string text in _loadInfo.bones)
			{
				GameObject gameObject = transform.FindLoop(text);
				if (!(gameObject == null))
				{
					OIBoneInfo oiboneInfo = null;
					if (!_ociItem.itemInfo.bones.TryGetValue(text, out oiboneInfo))
					{
						oiboneInfo = new OIBoneInfo(Studio.GetNewIndex());
						_ociItem.itemInfo.bones.Add(text, oiboneInfo);
					}
					GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, oiboneInfo.dicKey);
					guideObject.enablePos = false;
					guideObject.enableScale = false;
					guideObject.enableMaluti = false;
					guideObject.calcScale = false;
					guideObject.scaleRate = 0.5f;
					guideObject.scaleRot = 0.025f;
					guideObject.scaleSelect = 0.05f;
					guideObject.parentGuide = _ociItem.guideObject;
					_ociItem.listBones.Add(new OCIChar.BoneInfo(guideObject, oiboneInfo, -1));
					guideObject.SetActive(false, true);
					this.listBones.Add(new ItemFKCtrl.TargetInfo(gameObject, oiboneInfo.changeAmount, _isNew));
				}
			}
			this.count = this.listBones.Count;
		}

		// Token: 0x06009B80 RID: 39808 RVA: 0x003FB450 File Offset: 0x003F9850
		private void OnDisable()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.listBones[i].CopyBase();
			}
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x003FB488 File Offset: 0x003F9888
		private void LateUpdate()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.listBones[i].Update();
			}
		}

		// Token: 0x04007C00 RID: 31744
		private List<ItemFKCtrl.TargetInfo> listBones = new List<ItemFKCtrl.TargetInfo>();

		// Token: 0x02001261 RID: 4705
		private class TargetInfo
		{
			// Token: 0x06009B82 RID: 39810 RVA: 0x003FB4C0 File Offset: 0x003F98C0
			public TargetInfo(GameObject _gameObject, ChangeAmount _changeAmount, bool _new)
			{
				this.gameObject = _gameObject;
				this.changeAmount = _changeAmount;
				if (_new)
				{
					this.CopyBone();
				}
				this.changeAmount.defRot = this.transform.localEulerAngles;
				this.baseRot = this.transform.localEulerAngles;
			}

			// Token: 0x17002137 RID: 8503
			// (get) Token: 0x06009B83 RID: 39811 RVA: 0x003FB51F File Offset: 0x003F991F
			public Transform transform
			{
				get
				{
					if (this.m_Transform == null)
					{
						this.m_Transform = this.gameObject.transform;
					}
					return this.m_Transform;
				}
			}

			// Token: 0x06009B84 RID: 39812 RVA: 0x003FB549 File Offset: 0x003F9949
			public void CopyBone()
			{
				this.changeAmount.rot = this.transform.localEulerAngles;
			}

			// Token: 0x06009B85 RID: 39813 RVA: 0x003FB561 File Offset: 0x003F9961
			public void CopyBase()
			{
				this.transform.localEulerAngles = this.baseRot;
			}

			// Token: 0x06009B86 RID: 39814 RVA: 0x003FB574 File Offset: 0x003F9974
			public void Update()
			{
				this.transform.localRotation = Quaternion.Euler(this.changeAmount.rot);
			}

			// Token: 0x04007C02 RID: 31746
			public GameObject gameObject;

			// Token: 0x04007C03 RID: 31747
			private Transform m_Transform;

			// Token: 0x04007C04 RID: 31748
			public ChangeAmount changeAmount;

			// Token: 0x04007C05 RID: 31749
			private Vector3 baseRot = Vector3.zero;
		}
	}
}
