using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C1 RID: 1985
	public class ChaReference : BaseLoader
	{
		// Token: 0x0600314E RID: 12622 RVA: 0x001091BC File Offset: 0x001075BC
		public void Log_ReferenceObjectNull()
		{
			if (this.dictRefObj == null)
			{
				return;
			}
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.dictRefObj)
			{
				if (!(null != keyValuePair.Value))
				{
					string text = "There is no " + keyValuePair.Key.ToString() + ".";
				}
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x0010925C File Offset: 0x0010765C
		public void CreateReferenceInfo(ulong flags, GameObject objRef)
		{
			this.ReleaseRefObject(flags);
			if (null == objRef)
			{
				return;
			}
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(objRef.transform);
			if (flags >= 7UL && flags <= 10UL)
			{
				switch ((int)(flags - 7UL))
				{
				case 0:
					this.dictRefObj[5] = findAssist.GetObjectFromName("o_bra_a");
					this.dictRefObj[6] = findAssist.GetObjectFromName("o_bra_b");
					this.dictRefObj[7] = findAssist.GetObjectFromName("o_shorts_a");
					return;
				case 1:
					this.dictRefObj[8] = findAssist.GetObjectFromName("o_shorts_a");
					return;
				case 3:
					this.dictRefObj[9] = findAssist.GetObjectFromName("o_panst_a");
					return;
				}
			}
			if (flags != 1UL)
			{
				if (flags != 2UL)
				{
				}
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0010935C File Offset: 0x0010775C
		public void ReleaseRefObject(ulong flags)
		{
			if (flags >= 7UL && flags <= 10UL)
			{
				switch ((int)(flags - 7UL))
				{
				case 0:
					this.dictRefObj.Remove(5);
					this.dictRefObj.Remove(6);
					this.dictRefObj.Remove(7);
					return;
				case 1:
					this.dictRefObj.Remove(8);
					return;
				case 3:
					this.dictRefObj.Remove(9);
					return;
				}
			}
			if (flags != 1UL)
			{
				if (flags != 2UL)
				{
				}
			}
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00109404 File Offset: 0x00107804
		public void ReleaseRefAll()
		{
			this.dictRefObj.Clear();
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00109414 File Offset: 0x00107814
		public GameObject GetReferenceInfo(ChaReference.RefObjKey key)
		{
			ChaControl chaControl = this as ChaControl;
			if (null != chaControl)
			{
				switch (key)
				{
				case ChaReference.RefObjKey.HeadParent:
					if (null == chaControl.cmpBoneBody)
					{
						return null;
					}
					if (null == chaControl.cmpBoneBody.targetEtc.trfHeadParent)
					{
						return null;
					}
					return chaControl.cmpBoneBody.targetEtc.trfHeadParent.gameObject;
				case ChaReference.RefObjKey.k_f_shoulderL_00:
					if (null == chaControl.cmpBoneBody)
					{
						return null;
					}
					if (null == chaControl.cmpBoneBody.targetEtc.trf_k_shoulderL_00)
					{
						return null;
					}
					return chaControl.cmpBoneBody.targetEtc.trf_k_shoulderL_00.gameObject;
				case ChaReference.RefObjKey.k_f_shoulderR_00:
					if (null == chaControl.cmpBoneBody)
					{
						return null;
					}
					if (null == chaControl.cmpBoneBody.targetEtc.trf_k_shoulderR_00)
					{
						return null;
					}
					return chaControl.cmpBoneBody.targetEtc.trf_k_shoulderR_00.gameObject;
				case ChaReference.RefObjKey.k_f_handL_00:
					if (null == chaControl.cmpBoneBody)
					{
						return null;
					}
					if (null == chaControl.cmpBoneBody.targetEtc.trf_k_handL_00)
					{
						return null;
					}
					return chaControl.cmpBoneBody.targetEtc.trf_k_handL_00.gameObject;
				case ChaReference.RefObjKey.k_f_handR_00:
					if (null == chaControl.cmpBoneBody)
					{
						return null;
					}
					if (null == chaControl.cmpBoneBody.targetEtc.trf_k_handR_00)
					{
						return null;
					}
					return chaControl.cmpBoneBody.targetEtc.trf_k_handR_00.gameObject;
				}
			}
			GameObject result = null;
			this.dictRefObj.TryGetValue((int)key, out result);
			return result;
		}

		// Token: 0x04002E8A RID: 11914
		public const ulong FbxTypeBodyBone = 1UL;

		// Token: 0x04002E8B RID: 11915
		public const ulong FbxTypeHeadBone = 2UL;

		// Token: 0x04002E8C RID: 11916
		public const ulong FbxTypeInnerT = 7UL;

		// Token: 0x04002E8D RID: 11917
		public const ulong FbxTypeInnerB = 8UL;

		// Token: 0x04002E8E RID: 11918
		public const ulong FbxTypePanst = 10UL;

		// Token: 0x04002E8F RID: 11919
		private Dictionary<int, GameObject> dictRefObj = new Dictionary<int, GameObject>();

		// Token: 0x020007C2 RID: 1986
		public enum RefObjKey
		{
			// Token: 0x04002E91 RID: 11921
			HeadParent,
			// Token: 0x04002E92 RID: 11922
			k_f_shoulderL_00,
			// Token: 0x04002E93 RID: 11923
			k_f_shoulderR_00,
			// Token: 0x04002E94 RID: 11924
			k_f_handL_00,
			// Token: 0x04002E95 RID: 11925
			k_f_handR_00,
			// Token: 0x04002E96 RID: 11926
			mask_braA,
			// Token: 0x04002E97 RID: 11927
			mask_braB,
			// Token: 0x04002E98 RID: 11928
			mask_innerTB,
			// Token: 0x04002E99 RID: 11929
			mask_innerB,
			// Token: 0x04002E9A RID: 11930
			mask_panst
		}
	}
}
