using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using UnityEngine;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x0200079F RID: 1951
	[Serializable]
	public class Item
	{
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x001056E6 File Offset: 0x00103AE6
		public List<GameObject> itemObjectList
		{
			get
			{
				return this._itemObjectList;
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x001056F0 File Offset: 0x00103AF0
		public void Visible(bool visible)
		{
			this._itemObjectList.ForEach(delegate(GameObject item)
			{
				item.SetActive(visible);
			});
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x00105724 File Offset: 0x00103B24
		public void Setting(ChaControl chaCtrl, bool isItemClear = true)
		{
			if (isItemClear)
			{
				this._itemObjectList.ForEach(delegate(GameObject item)
				{
					UnityEngine.Object.Destroy(item);
				});
				this._itemObjectList.Clear();
			}
			this._itemObjectList.AddRange(from item in this.data
			select item.Load(chaCtrl));
		}

		// Token: 0x04002D21 RID: 11553
		public Item.Data[] data = new Item.Data[0];

		// Token: 0x04002D22 RID: 11554
		[SerializeField]
		private List<GameObject> _itemObjectList = new List<GameObject>();

		// Token: 0x020007A0 RID: 1952
		[Serializable]
		public class Data : AssetBundleData
		{
			// Token: 0x06002E42 RID: 11842 RVA: 0x00105DA4 File Offset: 0x001041A4
			public static Transform GetParent(Item.Data.Type type, ChaControl chaCtrl)
			{
				switch (type)
				{
				case Item.Data.Type.Head:
					return chaCtrl.cmpBoneBody.targetEtc.trfHeadParent;
				case Item.Data.Type.Neck:
					return chaCtrl.GetAccessoryParentTransform(15).transform;
				case Item.Data.Type.LeftHand:
					return chaCtrl.GetAccessoryParentTransform(44).transform;
				case Item.Data.Type.RightHand:
					return chaCtrl.GetAccessoryParentTransform(48).transform;
				case Item.Data.Type.LeftFoot:
					return chaCtrl.GetAccessoryParentTransform(29).transform;
				case Item.Data.Type.RightFoot:
					return chaCtrl.GetAccessoryParentTransform(33).transform;
				case Item.Data.Type.a_n_headside:
					return chaCtrl.GetAccessoryParentTransform(8).transform;
				case Item.Data.Type.k_f_handL_00:
					return chaCtrl.GetReferenceInfo(ChaReference.RefObjKey.k_f_handL_00).transform;
				case Item.Data.Type.k_f_handR_00:
					return chaCtrl.GetReferenceInfo(ChaReference.RefObjKey.k_f_handR_00).transform;
				case Item.Data.Type.chara:
					return chaCtrl.animBody.transform;
				case Item.Data.Type.k_f_shoulderL_00:
					return chaCtrl.GetReferenceInfo(ChaReference.RefObjKey.k_f_shoulderL_00).transform;
				case Item.Data.Type.k_f_shoulderR_00:
					return chaCtrl.GetReferenceInfo(ChaReference.RefObjKey.k_f_shoulderR_00).transform;
				default:
					return null;
				}
			}

			// Token: 0x06002E43 RID: 11843 RVA: 0x00105E94 File Offset: 0x00104294
			public GameObject Load(ChaControl chaCtrl)
			{
				GameObject gameObject = this.LoadModel(chaCtrl);
				Animator component = gameObject.GetComponent<Animator>();
				if (component != null)
				{
					this.motion.LoadAnimator(component);
				}
				return gameObject;
			}

			// Token: 0x06002E44 RID: 11844 RVA: 0x00105ECC File Offset: 0x001042CC
			private GameObject LoadModel(ChaControl chaCtrl)
			{
				Transform transform = Item.Data.GetParent(this.type, chaCtrl);
				if (transform == null)
				{
					transform = chaCtrl.transform.root;
				}
				GameObject asset = this.GetAsset<GameObject>();
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(asset, transform, false);
				gameObject.transform.localPosition += this.offsetPos;
				gameObject.transform.localEulerAngles += this.offsetAngle;
				gameObject.name = asset.name;
				this.UnloadBundle(false, false);
				return gameObject;
			}

			// Token: 0x04002D24 RID: 11556
			public Item.Data.Type type;

			// Token: 0x04002D25 RID: 11557
			public Vector3 offsetPos = Vector3.zero;

			// Token: 0x04002D26 RID: 11558
			public Vector3 offsetAngle = Vector3.zero;

			// Token: 0x04002D27 RID: 11559
			public Motion motion = new Motion();

			// Token: 0x020007A1 RID: 1953
			public enum Type
			{
				// Token: 0x04002D29 RID: 11561
				None,
				// Token: 0x04002D2A RID: 11562
				Head,
				// Token: 0x04002D2B RID: 11563
				Neck,
				// Token: 0x04002D2C RID: 11564
				LeftHand,
				// Token: 0x04002D2D RID: 11565
				RightHand,
				// Token: 0x04002D2E RID: 11566
				LeftFoot,
				// Token: 0x04002D2F RID: 11567
				RightFoot,
				// Token: 0x04002D30 RID: 11568
				a_n_headside,
				// Token: 0x04002D31 RID: 11569
				k_f_handL_00,
				// Token: 0x04002D32 RID: 11570
				k_f_handR_00,
				// Token: 0x04002D33 RID: 11571
				chara,
				// Token: 0x04002D34 RID: 11572
				k_f_shoulderL_00,
				// Token: 0x04002D35 RID: 11573
				k_f_shoulderR_00
			}
		}
	}
}
