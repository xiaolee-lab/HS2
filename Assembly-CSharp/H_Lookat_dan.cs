using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

// Token: 0x02000A90 RID: 2704
public class H_Lookat_dan : MonoBehaviour
{
	// Token: 0x06004FBF RID: 20415 RVA: 0x001EB084 File Offset: 0x001E9484
	public void DankonInit(ChaControl _male, ChaControl[] _females)
	{
		this.females = _females;
		this.male = _male;
		if (this.male.objBodyBone == null)
		{
			return;
		}
		Transform transform = this.male.objBodyBone.transform;
		if (transform != null)
		{
			GameObject gameObject = transform.FindLoop("cm_J_dan101_00");
			this.DanBase = new UnityEx.ValueTuple<GameObject, Transform>(gameObject, gameObject.transform);
			this.DanTop = transform.FindLoop("cm_J_dan109_00");
			this.DanBaseR = this.DanBase.Item2.parent.gameObject;
			this.dan_Info.SetUpAxisTransform(this.DanBaseR.transform);
		}
		this.dan_Info.SetLookAtTransform(this.DanBase.Item2);
	}

	// Token: 0x06004FC0 RID: 20416 RVA: 0x001EB14C File Offset: 0x001E954C
	public bool LoadList(string _pathFile, int motionId = -1)
	{
		this.Release();
		if (_pathFile == string.Empty)
		{
			return false;
		}
		this.assetName.Append(_pathFile);
		this.assetName.Replace("_m_", "_");
		List<H_Lookat_dan.MotionLookAtList> collection;
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.DicLstLookAtDan.TryGetValue(this.assetName.ToString(), out collection))
		{
			collection = new List<H_Lookat_dan.MotionLookAtList>();
		}
		this.lstLookAt = new List<H_Lookat_dan.MotionLookAtList>(collection);
		if (this.lstLookAt.Count != 0)
		{
			this.setInfo(this.lstLookAt[0]);
		}
		return true;
	}

	// Token: 0x06004FC1 RID: 20417 RVA: 0x001EB1F0 File Offset: 0x001E95F0
	public void Release()
	{
		this.lstLookAt.Clear();
		this.assetName.Clear();
		this.strPlayMotion = string.Empty;
		this.transLookAtNull = null;
		this.bTopStick = false;
		this.dan_Info.SetTargetTransform(null);
	}

	// Token: 0x06004FC2 RID: 20418 RVA: 0x001EB230 File Offset: 0x001E9630
	private void LateUpdate()
	{
		if (this.male == null || this.females == null)
		{
			return;
		}
		if (this.male.objBodyBone == null || this.females[0].objBodyBone == null)
		{
			return;
		}
		this.setLookAt();
		if (this.lstShape != null && this.transLookAtNull != null)
		{
			Vector3 vector = this.transLookAtNull.position;
			for (int i = 0; i < this.lstShape.Length; i++)
			{
				if (this.lstShape[i].bUse)
				{
					float shapeBodyValue = this.females[this.numFemale].GetShapeBodyValue(this.lstShape[i].shape);
					Vector3 vector2 = (shapeBodyValue < 0.5f) ? Vector3.Lerp(this.lstShape[i].minPos, this.lstShape[i].middlePos, Mathf.InverseLerp(0f, 0.5f, shapeBodyValue)) : Vector3.Lerp(this.lstShape[i].middlePos, this.lstShape[i].maxPos, Mathf.InverseLerp(0.5f, 1f, shapeBodyValue));
					vector2 = this.transLookAtNull.TransformDirection(vector2);
					vector += vector2;
				}
			}
			this.transLookAtNull.position = vector;
		}
		H_Lookat_dan.LookAtProc(this);
	}

	// Token: 0x06004FC3 RID: 20419 RVA: 0x001EB3B8 File Offset: 0x001E97B8
	private bool setLookAt()
	{
		AnimatorStateInfo animatorStateInfo = this.females[0].getAnimatorStateInfo(0);
		if (animatorStateInfo.IsName(this.strPlayMotion))
		{
			return true;
		}
		foreach (H_Lookat_dan.MotionLookAtList motionLookAtList in this.lstLookAt)
		{
			if (animatorStateInfo.IsName(motionLookAtList.strMotion))
			{
				this.setInfo(motionLookAtList);
				break;
			}
		}
		return true;
	}

	// Token: 0x06004FC4 RID: 20420 RVA: 0x001EB458 File Offset: 0x001E9858
	private bool setInfo(H_Lookat_dan.MotionLookAtList _list)
	{
		if (_list == null)
		{
			return false;
		}
		if (this.females[_list.numFemale].objBodyBone == null)
		{
			this.transLookAtNull = null;
			this.lstShape = null;
			return false;
		}
		this.strPlayMotion = _list.strMotion;
		this.numFemale = _list.numFemale;
		if (_list.strLookAtNull == string.Empty)
		{
			this.transLookAtNull = null;
			this.lstShape = null;
		}
		else
		{
			GameObject gameObject = this.females[_list.numFemale].objBodyBone.transform.FindLoop(_list.strLookAtNull);
			this.transLookAtNull = ((!(gameObject != null)) ? null : gameObject.transform);
			this.lstShape = _list.lstShape;
		}
		this.bTopStick = _list.bTopStick;
		this.bManual = _list.bManual;
		Transform item = this.DanBase.Item2;
		this.dan_Info.SetTargetTransform(this.transLookAtNull);
		this.dan_Info.SetOldRotation((!(item != null)) ? Quaternion.identity : item.rotation);
		return true;
	}

	// Token: 0x06004FC5 RID: 20421 RVA: 0x001EB584 File Offset: 0x001E9984
	public static bool LookAtProc(H_Lookat_dan h_Lookat_Dan)
	{
		if (h_Lookat_Dan.DanBase.Item1 == null)
		{
			return false;
		}
		if (h_Lookat_Dan.transLookAtNull == null)
		{
			return false;
		}
		if (!h_Lookat_Dan.bManual)
		{
			h_Lookat_Dan.DanBase.Item2.LookAt(h_Lookat_Dan.transLookAtNull);
		}
		else
		{
			h_Lookat_Dan.dan_Info.ManualCalc();
		}
		if (h_Lookat_Dan.bTopStick && h_Lookat_Dan.DanTop != null)
		{
			h_Lookat_Dan.DanTop.transform.position = h_Lookat_Dan.transLookAtNull.position;
		}
		return true;
	}

	// Token: 0x040048A6 RID: 18598
	private ChaControl[] females;

	// Token: 0x040048A7 RID: 18599
	private ChaControl male;

	// Token: 0x040048A8 RID: 18600
	private StringBuilder assetName = new StringBuilder();

	// Token: 0x040048A9 RID: 18601
	public List<H_Lookat_dan.MotionLookAtList> lstLookAt = new List<H_Lookat_dan.MotionLookAtList>();

	// Token: 0x040048AA RID: 18602
	public string strPlayMotion = string.Empty;

	// Token: 0x040048AB RID: 18603
	public Transform transLookAtNull;

	// Token: 0x040048AC RID: 18604
	public bool bTopStick;

	// Token: 0x040048AD RID: 18605
	public bool bManual;

	// Token: 0x040048AE RID: 18606
	public H_Lookat_dan.ShapeInfo[] lstShape = new H_Lookat_dan.ShapeInfo[10];

	// Token: 0x040048AF RID: 18607
	public int numFemale;

	// Token: 0x040048B0 RID: 18608
	[SerializeField]
	public H_LookAtDan_Info dan_Info = new H_LookAtDan_Info();

	// Token: 0x040048B1 RID: 18609
	public UnityEx.ValueTuple<GameObject, Transform> DanBase;

	// Token: 0x040048B2 RID: 18610
	public GameObject DanTop;

	// Token: 0x040048B3 RID: 18611
	public GameObject DanBaseR;

	// Token: 0x02000A91 RID: 2705
	[Serializable]
	public struct ShapeInfo
	{
		// Token: 0x040048B4 RID: 18612
		public int shape;

		// Token: 0x040048B5 RID: 18613
		public Vector3 minPos;

		// Token: 0x040048B6 RID: 18614
		public Vector3 middlePos;

		// Token: 0x040048B7 RID: 18615
		public Vector3 maxPos;

		// Token: 0x040048B8 RID: 18616
		public bool bUse;
	}

	// Token: 0x02000A92 RID: 2706
	[Serializable]
	public class MotionLookAtList
	{
		// Token: 0x06004FC6 RID: 20422 RVA: 0x001EB628 File Offset: 0x001E9A28
		public MotionLookAtList()
		{
			for (int i = 0; i < this.lstShape.Length; i++)
			{
				this.lstShape[i].bUse = false;
			}
		}

		// Token: 0x040048B9 RID: 18617
		public string strMotion;

		// Token: 0x040048BA RID: 18618
		public int numFemale;

		// Token: 0x040048BB RID: 18619
		public string strLookAtNull;

		// Token: 0x040048BC RID: 18620
		public bool bTopStick;

		// Token: 0x040048BD RID: 18621
		public bool bManual;

		// Token: 0x040048BE RID: 18622
		public H_Lookat_dan.ShapeInfo[] lstShape = new H_Lookat_dan.ShapeInfo[10];
	}
}
