using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011D9 RID: 4569
	public class IKCtrl : MonoBehaviour
	{
		// Token: 0x17001FCB RID: 8139
		// (set) Token: 0x06009621 RID: 38433 RVA: 0x003DFD60 File Offset: 0x003DE160
		public OCIChar.IKInfo addIKInfo
		{
			set
			{
				this.listIKInfo.Add(value);
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x06009622 RID: 38434 RVA: 0x003DFD6E File Offset: 0x003DE16E
		public int count
		{
			get
			{
				return this.listIKInfo.Count;
			}
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x003DFD7B File Offset: 0x003DE17B
		public void InitTarget()
		{
			base.StartCoroutine("InitTargetCoroutine");
		}

		// Token: 0x06009624 RID: 38436 RVA: 0x003DFD8C File Offset: 0x003DE18C
		public void CopyBone(OIBoneInfo.BoneGroup _target)
		{
			foreach (OCIChar.IKInfo ikinfo in from l in this.listIKInfo
			where (l.boneGroup & _target) != (OIBoneInfo.BoneGroup)0
			select l)
			{
				ikinfo.CopyBone();
			}
		}

		// Token: 0x06009625 RID: 38437 RVA: 0x003DFE04 File Offset: 0x003DE204
		public void CopyBoneRotation(OIBoneInfo.BoneGroup _target)
		{
			foreach (OCIChar.IKInfo ikinfo in from l in this.listIKInfo
			where (l.boneGroup & _target) != (OIBoneInfo.BoneGroup)0
			select l)
			{
				ikinfo.CopyBoneRotation();
			}
		}

		// Token: 0x06009626 RID: 38438 RVA: 0x003DFE7C File Offset: 0x003DE27C
		private IEnumerator InitTargetCoroutine()
		{
			yield return null;
			yield return new WaitForEndOfFrame();
			foreach (OCIChar.IKInfo ikinfo in this.listIKInfo)
			{
				ikinfo.CopyBone();
			}
			yield break;
		}

		// Token: 0x06009627 RID: 38439 RVA: 0x003DFE97 File Offset: 0x003DE297
		private void Awake()
		{
			this.listIKInfo = new List<OCIChar.IKInfo>();
		}

		// Token: 0x040078C8 RID: 30920
		private List<OCIChar.IKInfo> listIKInfo;
	}
}
