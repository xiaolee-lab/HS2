using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using Correct;
using Correct.Process;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000B29 RID: 2857
public class MotionIK
{
	// Token: 0x060053C6 RID: 21446 RVA: 0x0024EA80 File Offset: 0x0024CE80
	public MotionIK(ChaControl info, bool isHscene = false, MotionIKData data = null)
	{
		this.info = info;
		this.data = (data ?? new MotionIKData());
		Animator animBody = info.animBody;
		this.ik = animBody.GetComponent<FullBodyBipedIK>();
		if (this.ik != null)
		{
			this.frameCorrect = animBody.GetComponent<FrameCorrect>();
			this.ikCorrect = animBody.GetComponent<IKCorrect>();
		}
		this.SetPartners(Array.Empty<MotionIK>());
		this.Reset();
		if (!isHscene)
		{
			this.tmpBones = new List<GameObject>();
			animBody.transform.FindLoopPrefix(this.tmpBones, "f_pv_");
			this.DefBone[0] = this.ListFindBone(this.tmpBones, "f_pv_arm_L");
			this.DefBone[1] = this.ListFindBone(this.tmpBones, "f_pv_elbo_L");
			this.DefBone[2] = this.ListFindBone(this.tmpBones, "f_pv_arm_R");
			this.DefBone[3] = this.ListFindBone(this.tmpBones, "f_pv_elbo_R");
			this.DefBone[4] = this.ListFindBone(this.tmpBones, "f_pv_leg_L");
			this.DefBone[5] = this.ListFindBone(this.tmpBones, "f_pv_knee_L");
			this.DefBone[6] = this.ListFindBone(this.tmpBones, "f_pv_leg_R");
			this.DefBone[7] = this.ListFindBone(this.tmpBones, "f_pv_knee_R");
		}
	}

	// Token: 0x060053C7 RID: 21447 RVA: 0x0024EC54 File Offset: 0x0024D054
	public static List<MotionIK> Setup(List<ChaControl> infos)
	{
		List<MotionIK> list = new List<MotionIK>();
		for (int i = 0; i < infos.Count; i++)
		{
			int index = i;
			list.Add(new MotionIK(infos[index], false, null));
		}
		for (int j = 0; j < list.Count; j++)
		{
			list[j].SetPartners(list);
		}
		return list;
	}

	// Token: 0x060053C8 RID: 21448 RVA: 0x0024ECBC File Offset: 0x0024D0BC
	public static Vector3 GetShapeLerpPositionValue(float shape, Vector3 min, Vector3 med, Vector3 max, bool MapIK = false)
	{
		Vector3 result;
		if (MapIK)
		{
			result = ((shape < 0.5f) ? Vector3.Lerp(min, Vector3.zero, Mathf.InverseLerp(0f, 0.5f, shape)) : Vector3.Lerp(Vector3.zero, max, Mathf.InverseLerp(0.5f, 1f, shape)));
		}
		else
		{
			result = ((shape < 0.5f) ? Vector3.Lerp(min, med, Mathf.InverseLerp(0f, 0.5f, shape)) : Vector3.Lerp(med, max, Mathf.InverseLerp(0.5f, 1f, shape)));
		}
		return result;
	}

	// Token: 0x060053C9 RID: 21449 RVA: 0x0024ED5C File Offset: 0x0024D15C
	public static Vector3 GetShapeLerpAngleValue(float shape, Vector3 min, Vector3 med, Vector3 max, bool MapIK = false)
	{
		Vector3 zero = Vector3.zero;
		if (shape >= 0.5f)
		{
			float t = Mathf.InverseLerp(0.5f, 1f, shape);
			for (int i = 0; i < 3; i++)
			{
				if (MapIK)
				{
					zero[i] = Mathf.LerpAngle(0f, max[i], t);
				}
				else
				{
					zero[i] = Mathf.LerpAngle(med[i], max[i], t);
				}
			}
		}
		else
		{
			float t2 = Mathf.InverseLerp(0f, 0.5f, shape);
			for (int j = 0; j < 3; j++)
			{
				if (MapIK)
				{
					zero[j] = Mathf.LerpAngle(min[j], 0f, t2);
				}
				else
				{
					zero[j] = Mathf.LerpAngle(min[j], med[j], t2);
				}
			}
		}
		return zero;
	}

	// Token: 0x060053CA RID: 21450 RVA: 0x0024EE5C File Offset: 0x0024D25C
	public void SetPartners(params MotionIK[] partners)
	{
		this.partners = new MotionIK[(partners.Length != 0) ? partners.Length : 1];
		this.partners[0] = this;
		int[] array = new int[2];
		array[0] = 1;
		int[] array2 = array;
		for (int i = 0; i < partners.Length; i++)
		{
			array2[1] = i;
			if (this.partners[0] != partners[array2[1]])
			{
				this.partners[array2[0]] = partners[array2[1]];
				array2[0]++;
			}
		}
	}

	// Token: 0x060053CB RID: 21451 RVA: 0x0024EEE4 File Offset: 0x0024D2E4
	public void SetPartners(List<MotionIK> partners)
	{
		MotionIK[] array = new MotionIK[partners.Count];
		for (int i = 0; i < array.Length; i++)
		{
			int num = i;
			array[num] = partners[num];
		}
		this.SetPartners(array);
	}

	// Token: 0x060053CC RID: 21452 RVA: 0x0024EF28 File Offset: 0x0024D328
	public void SetPartners(List<Tuple<int, int, MotionIK>> partners)
	{
		MotionIK[] array = new MotionIK[partners.Count];
		for (int i = 0; i < array.Length; i++)
		{
			int num = i;
			array[num] = partners[num].Item3;
		}
		this.SetPartners(array);
	}

	// Token: 0x060053CD RID: 21453 RVA: 0x0024EF70 File Offset: 0x0024D370
	public void SetItems(MotionIK motionIK, GameObject[] items)
	{
		for (int i = 0; i < this.partners.Length; i++)
		{
			for (int j = 0; j < items.Length; j++)
			{
				if (this.partners[i].items == null)
				{
					this.partners[i].items = new List<GameObject>();
				}
				if (!this.partners[i].items.Contains(items[j]))
				{
					this.partners[i].items.Add(items[j]);
				}
			}
		}
	}

	// Token: 0x060053CE RID: 21454 RVA: 0x0024EFFD File Offset: 0x0024D3FD
	public void Reset()
	{
		this.InitFrameCalc();
		this.enabled = false;
	}

	// Token: 0x060053CF RID: 21455 RVA: 0x0024F00C File Offset: 0x0024D40C
	public void Release()
	{
		this.data.Release();
	}

	// Token: 0x060053D0 RID: 21456 RVA: 0x0024F019 File Offset: 0x0024D419
	public void LoadData(string abName, string assetName, bool add = false)
	{
		if (this.data == null)
		{
			this.data = new MotionIKData();
		}
		this.data.AIRead(abName, assetName, add);
	}

	// Token: 0x17000F02 RID: 3842
	// (get) Token: 0x060053D1 RID: 21457 RVA: 0x0024F03F File Offset: 0x0024D43F
	// (set) Token: 0x060053D2 RID: 21458 RVA: 0x0024F047 File Offset: 0x0024D447
	public ChaControl info { get; private set; }

	// Token: 0x17000F03 RID: 3843
	// (get) Token: 0x060053D3 RID: 21459 RVA: 0x0024F050 File Offset: 0x0024D450
	// (set) Token: 0x060053D4 RID: 21460 RVA: 0x0024F058 File Offset: 0x0024D458
	public List<GameObject> items { get; private set; }

	// Token: 0x17000F04 RID: 3844
	// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0024F061 File Offset: 0x0024D461
	// (set) Token: 0x060053D6 RID: 21462 RVA: 0x0024F069 File Offset: 0x0024D469
	public FullBodyBipedIK ik { get; private set; }

	// Token: 0x17000F05 RID: 3845
	// (get) Token: 0x060053D7 RID: 21463 RVA: 0x0024F072 File Offset: 0x0024D472
	// (set) Token: 0x060053D8 RID: 21464 RVA: 0x0024F07A File Offset: 0x0024D47A
	public MotionIK[] partners { get; private set; }

	// Token: 0x17000F06 RID: 3846
	// (get) Token: 0x060053D9 RID: 21465 RVA: 0x0024F083 File Offset: 0x0024D483
	public MotionIK.IKTargetPair[] ikTargetPairs
	{
		get
		{
			return (!(this.ik == null)) ? MotionIK.IKTargetPair.GetPairs(this.ik.solver) : null;
		}
	}

	// Token: 0x17000F07 RID: 3847
	// (get) Token: 0x060053DA RID: 21466 RVA: 0x0024F0AC File Offset: 0x0024D4AC
	// (set) Token: 0x060053DB RID: 21467 RVA: 0x0024F0B4 File Offset: 0x0024D4B4
	public MotionIKData data { get; private set; }

	// Token: 0x17000F08 RID: 3848
	// (get) Token: 0x060053DC RID: 21468 RVA: 0x0024F0BD File Offset: 0x0024D4BD
	// (set) Token: 0x060053DD RID: 21469 RVA: 0x0024F0C5 File Offset: 0x0024D4C5
	public FrameCorrect frameCorrect { get; private set; }

	// Token: 0x17000F09 RID: 3849
	// (get) Token: 0x060053DE RID: 21470 RVA: 0x0024F0CE File Offset: 0x0024D4CE
	// (set) Token: 0x060053DF RID: 21471 RVA: 0x0024F0D6 File Offset: 0x0024D4D6
	public IKCorrect ikCorrect { get; private set; }

	// Token: 0x17000F0A RID: 3850
	// (get) Token: 0x060053E0 RID: 21472 RVA: 0x0024F0DF File Offset: 0x0024D4DF
	// (set) Token: 0x060053E1 RID: 21473 RVA: 0x0024F100 File Offset: 0x0024D500
	public bool enabled
	{
		get
		{
			return this.ik != null && this.ik.enabled;
		}
		set
		{
			if (this.ik == null)
			{
				return;
			}
			this.ik.enabled = value;
			this.ikCorrect.isEnabled = value;
		}
	}

	// Token: 0x17000F0B RID: 3851
	// (get) Token: 0x060053E2 RID: 21474 RVA: 0x0024F12C File Offset: 0x0024D52C
	public string[] stateNames
	{
		get
		{
			string[] array = new string[this.data.states.Length];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[num] = this.data.states[num].name;
			}
			return (this.data.states != null) ? array : new string[0];
		}
	}

	// Token: 0x060053E3 RID: 21475 RVA: 0x0024F194 File Offset: 0x0024D594
	public void InitFrameCalc()
	{
		if (this.frameCorrect != null)
		{
			foreach (BaseCorrect.Info info in this.frameCorrect.list)
			{
				info.enabled = false;
				info.pos = Vector3.zero;
				info.ang = Vector3.zero;
			}
		}
		if (this.ikCorrect != null)
		{
			foreach (BaseCorrect.Info info2 in this.ikCorrect.list)
			{
				info2.enabled = false;
				info2.pos = Vector3.zero;
				info2.ang = Vector3.zero;
				info2.bone = null;
			}
		}
	}

	// Token: 0x060053E4 RID: 21476 RVA: 0x0024F29C File Offset: 0x0024D69C
	public MotionIKData.State InitState(string stateName, int sex)
	{
		return this.data.InitState(stateName, sex);
	}

	// Token: 0x060053E5 RID: 21477 RVA: 0x0024F2AC File Offset: 0x0024D6AC
	public MotionIKData.State GetNowState(string stateName)
	{
		if (this.data.states == null)
		{
			return null;
		}
		int num = -1;
		for (int i = 0; i < this.data.states.Length; i++)
		{
			if (!(this.data.states[i].name != stateName))
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return null;
		}
		return this.data.states[num];
	}

	// Token: 0x060053E6 RID: 21478 RVA: 0x0024F32C File Offset: 0x0024D72C
	public MotionIKData.State GetNowState(int hashName)
	{
		if (this.data == null || this.data.states == null)
		{
			return null;
		}
		int num = -1;
		for (int i = 0; i < this.data.states.Length; i++)
		{
			if (Animator.StringToHash(this.data.states[i].name) == hashName)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return null;
		}
		return this.data.states[num];
	}

	// Token: 0x060053E7 RID: 21479 RVA: 0x0024F3B8 File Offset: 0x0024D7B8
	public MotionIKData.Frame[] GetNowFrames(string stateName)
	{
		MotionIKData.State nowState = this.GetNowState(stateName);
		return (nowState != null) ? nowState.frames : null;
	}

	// Token: 0x060053E8 RID: 21480 RVA: 0x0024F3E0 File Offset: 0x0024D7E0
	public void SetMapIK(string AnimatorName)
	{
		if (!Singleton<Manager.Resources>.Instance.MapIKData.TryGetValue(AnimatorName, out this.tmpMapData))
		{
			this.tmpMapData = null;
		}
		this.data = ((this.tmpMapData != null) ? this.tmpMapData.Copy() : null);
	}

	// Token: 0x060053E9 RID: 21481 RVA: 0x0024F434 File Offset: 0x0024D834
	public void Calc(string stateName)
	{
		if (this.frameCorrect == null)
		{
			return;
		}
		this.InitFrameCalc();
		MotionIKData.State nowState = this.GetNowState(stateName);
		if (nowState != null)
		{
			int iktargetLength = MotionIK.IKTargetPair.IKTargetLength;
			foreach (MotionIKData.Frame frame in nowState.frames)
			{
				int num = frame.frameNo - iktargetLength;
				if (num >= 0)
				{
					BaseCorrect.Info info = this.frameCorrect.list[num];
					info.enabled = true;
					Vector3[] correctShapeValues = this.GetCorrectShapeValues(this.partners[frame.editNo].info, frame.shapes);
					info.pos = correctShapeValues[0];
					info.ang = correctShapeValues[1];
				}
			}
		}
		this.enabled = (nowState != null);
		for (int j = 0; j < this.ikTargetPairs.Length; j++)
		{
			int num2 = j;
			this.LinkIK(num2, nowState, this.ikTargetPairs[num2]);
		}
	}

	// Token: 0x060053EA RID: 21482 RVA: 0x0024F55C File Offset: 0x0024D95C
	public void Calc(int hashName)
	{
		if (this.frameCorrect == null)
		{
			return;
		}
		this.InitFrameCalc();
		MotionIKData.State nowState = this.GetNowState(hashName);
		if (nowState != null)
		{
			int iktargetLength = MotionIK.IKTargetPair.IKTargetLength;
			foreach (MotionIKData.Frame frame in nowState.frames)
			{
				int num = frame.frameNo - iktargetLength;
				if (num >= 0)
				{
					BaseCorrect.Info info = this.frameCorrect.list[num];
					info.enabled = true;
					Vector3[] correctShapeValues = this.GetCorrectShapeValues(this.partners[frame.editNo].info, frame.shapes);
					info.pos = correctShapeValues[0];
					info.ang = correctShapeValues[1];
				}
			}
		}
		if (!this.MapIK)
		{
			this.enabled = (nowState != null);
		}
		else
		{
			this.enabled = true;
		}
		if (!this.MapIK)
		{
			for (int j = 0; j < this.ikTargetPairs.Length; j++)
			{
				int num2 = j;
				this.LinkIK(num2, nowState, this.ikTargetPairs[num2]);
			}
		}
		else if (nowState != null)
		{
			for (int k = 0; k < this.ikTargetPairs.Length; k++)
			{
				int num2 = k;
				this.LinkIK(num2, nowState, this.ikTargetPairs[num2]);
			}
		}
		else
		{
			for (int l = 0; l < this.ikTargetPairs.Length; l++)
			{
				int num2 = l;
				this.tmpData = this.ikTargetPairs[num2].effector.target.GetComponent<BaseData>();
				if (this.tmpData.bone == null)
				{
					this.tmpData.bone = this.DefBone[num2 * 2];
					this.tmpData.GetComponent<BaseProcess>().enabled = true;
					this.ikTargetPairs[num2].effector.positionWeight = 1f;
					this.ikTargetPairs[num2].effector.rotationWeight = 1f;
				}
				this.tmpData = this.ikTargetPairs[num2].bend.bendGoal.GetComponent<BaseData>();
				if (this.tmpData.bone == null)
				{
					this.tmpData.GetComponent<BaseProcess>().enabled = true;
					this.tmpData.bone = this.DefBone[num2 * 2 + 1];
					this.ikTargetPairs[num2].bend.weight = 1f;
				}
			}
		}
	}

	// Token: 0x060053EB RID: 21483 RVA: 0x0024F808 File Offset: 0x0024DC08
	private Vector3[] GetCorrectShapeValues(ChaControl chara, MotionIKData.Shape[] shapes)
	{
		Vector3[] array = new Vector3[]
		{
			Vector3.zero,
			Vector3.zero
		};
		foreach (MotionIKData.Shape shape in shapes)
		{
			float shapeBodyValue = chara.GetShapeBodyValue(shape.shapeNo);
			for (int j = 0; j < array.Length; j++)
			{
				if (j == 0)
				{
					array[j] += MotionIK.GetShapeLerpPositionValue(shapeBodyValue, shape.small[j], shape.mediam[j], shape.large[j], this.MapIK);
				}
				else
				{
					array[j] += MotionIK.GetShapeLerpAngleValue(shapeBodyValue, shape.small[j], shape.mediam[j], shape.large[j], this.MapIK);
				}
			}
		}
		return array;
	}

	// Token: 0x060053EC RID: 21484 RVA: 0x0024F934 File Offset: 0x0024DD34
	private Vector3[] GetCorrectShapeValues(ChaControl chara, float nowKeyFrame, Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>> calcBlend)
	{
		Vector3[] array = new Vector3[]
		{
			Vector3.zero,
			Vector3.zero
		};
		for (int i = 0; i < 2; i++)
		{
			foreach (KeyValuePair<int, List<MotionIKData.BlendWeightInfo>> keyValuePair in calcBlend[i])
			{
				int num = -1;
				for (int j = 0; j < keyValuePair.Value.Count; j++)
				{
					int index = j;
					if (keyValuePair.Value[index].StartKey <= nowKeyFrame)
					{
						num = j;
					}
				}
				if (num < 0)
				{
					num = 0;
				}
				MotionIKData.BlendWeightInfo blendWeightInfo = keyValuePair.Value[num];
				float shapeBodyValue = chara.GetShapeBodyValue(blendWeightInfo.shape.shapeNo);
				float t = Mathf.InverseLerp(blendWeightInfo.StartKey, blendWeightInfo.EndKey, nowKeyFrame);
				if (i == 0)
				{
					Vector3 vector = MotionIK.GetShapeLerpPositionValue(shapeBodyValue, blendWeightInfo.shape.small[i], blendWeightInfo.shape.mediam[i], blendWeightInfo.shape.large[i], this.MapIK);
					if (blendWeightInfo.pattern == 1)
					{
						vector = Vector3.Lerp(Vector3.zero, vector, t);
					}
					else if (blendWeightInfo.pattern == 2)
					{
						vector = Vector3.Lerp(vector, Vector3.zero, t);
					}
					array[i] += vector;
				}
				else
				{
					Vector3 vector = MotionIK.GetShapeLerpAngleValue(shapeBodyValue, blendWeightInfo.shape.small[i], blendWeightInfo.shape.mediam[i], blendWeightInfo.shape.large[i], this.MapIK);
					if (blendWeightInfo.pattern == 1)
					{
						vector = Vector3.Lerp(Vector3.zero, vector, t);
					}
					else if (blendWeightInfo.pattern == 2)
					{
						vector = Vector3.Lerp(vector, Vector3.zero, t);
					}
					array[i] += vector;
				}
			}
		}
		return array;
	}

	// Token: 0x060053ED RID: 21485 RVA: 0x0024FBB0 File Offset: 0x0024DFB0
	private Vector3[] GetCorrectShapeValues(ChaControl chara, float nowKeyFrame, Dictionary<int, List<MotionIKData.BlendWeightInfo>> calcBlend)
	{
		Vector3[] array = new Vector3[]
		{
			Vector3.zero,
			Vector3.zero
		};
		foreach (KeyValuePair<int, List<MotionIKData.BlendWeightInfo>> keyValuePair in calcBlend)
		{
			int num = -1;
			for (int i = 0; i < keyValuePair.Value.Count; i++)
			{
				int index = i;
				if (keyValuePair.Value[index].StartKey <= nowKeyFrame)
				{
					num = i;
				}
			}
			if (num < 0)
			{
				num = 0;
			}
			MotionIKData.BlendWeightInfo blendWeightInfo = keyValuePair.Value[num];
			float shapeBodyValue = chara.GetShapeBodyValue(blendWeightInfo.shape.shapeNo);
			float t = Mathf.InverseLerp(blendWeightInfo.StartKey, blendWeightInfo.EndKey, nowKeyFrame);
			Vector3 vector = MotionIK.GetShapeLerpPositionValue(shapeBodyValue, blendWeightInfo.shape.small[0], blendWeightInfo.shape.mediam[0], blendWeightInfo.shape.large[0], this.MapIK);
			if (blendWeightInfo.pattern == 1)
			{
				vector = Vector3.Lerp(Vector3.zero, vector, t);
			}
			else if (blendWeightInfo.pattern == 2)
			{
				vector = Vector3.Lerp(vector, Vector3.zero, t);
			}
			array[0] += vector;
			vector = MotionIK.GetShapeLerpAngleValue(shapeBodyValue, blendWeightInfo.shape.small[1], blendWeightInfo.shape.mediam[1], blendWeightInfo.shape.large[1], this.MapIK);
			if (blendWeightInfo.pattern == 1)
			{
				vector = Vector3.Lerp(Vector3.zero, vector, t);
			}
			else if (blendWeightInfo.pattern == 2)
			{
				vector = Vector3.Lerp(vector, Vector3.zero, t);
			}
			array[1] += vector;
		}
		return array;
	}

	// Token: 0x060053EE RID: 21486 RVA: 0x0024FDFC File Offset: 0x0024E1FC
	private void LinkIK(int index, MotionIKData.State state, MotionIK.IKTargetPair pair)
	{
		this.nowKeyFrame = 0f;
		MotionIKData.Parts parts = (state != null) ? state[index] : null;
		Transform x = null;
		MotionIKData.Param2 param = (parts != null) ? parts.param2 : null;
		IKEffector effector = pair.effector;
		this.tmpData = effector.target.GetComponent<BaseData>();
		if (this.tmpData != null)
		{
			this.tmpData.bone = ((param != null) ? this.getTarget(param.sex, param.target) : null);
			x = this.tmpData.bone;
			this.tmpProcess = this.tmpData.GetComponent<BaseProcess>();
			if (this.tmpProcess != null)
			{
				this.tmpProcess.enabled = (this.tmpData.bone != null);
			}
			MotionIKData.Frame frame = this.FindFrame(state, index * 2);
			if (frame.frameNo == -1)
			{
				this.tmpData.pos = Vector3.zero;
				this.tmpData.rot = Quaternion.identity;
				return;
			}
			Vector3[] array = new Vector3[2];
			if (!this.MapIK)
			{
				array = this.GetCorrectShapeValues(this.partners[frame.editNo].info, frame.shapes);
			}
			else
			{
				array[0] = this.tmpData.pos;
				array[1] = this.tmpData.rot.eulerAngles;
				this.CalcBlendSet(ref this.calcBlend[index], parts.param2.blendInfos);
				array = this.GetCorrectShapeValues(this.partners[0].info, this.nowKeyFrame, this.calcBlend[index]);
			}
			this.tmpData.pos = array[0];
			this.tmpData.rot = Quaternion.Euler(array[1]);
		}
		if (x == null || param == null)
		{
			effector.positionWeight = 0f;
			effector.rotationWeight = 0f;
		}
		else
		{
			effector.positionWeight = param.weightPos;
			effector.rotationWeight = param.weightAng;
		}
		x = null;
		MotionIKData.Param3 param2 = (parts != null) ? parts.param3 : null;
		IKConstraintBend bend = pair.bend;
		this.tmpData = bend.bendGoal.GetComponent<BaseData>();
		if (this.tmpData != null)
		{
			this.tmpData.bone = ((param2 != null) ? this.getTarget(0, param2.chein) : null);
			x = this.tmpData.bone;
			this.tmpProcess = this.tmpData.GetComponent<BaseProcess>();
			if (this.tmpProcess != null)
			{
				this.tmpProcess.enabled = (this.tmpData.bone != null);
			}
			MotionIKData.Frame frame2 = this.FindFrame(state, index * 2 + 1);
			if (frame2.frameNo == -1)
			{
				this.tmpData.pos = Vector3.zero;
				this.tmpData.rot = Quaternion.identity;
				return;
			}
			Vector3[] array2 = new Vector3[2];
			if (!this.MapIK)
			{
				array2 = this.GetCorrectShapeValues(this.partners[frame2.editNo].info, frame2.shapes);
			}
			else
			{
				array2[0] = this.tmpData.pos;
				array2[1] = this.tmpData.rot.eulerAngles;
				this.CalcBlendBendSet(ref this.calcBlendBend[index], new List<MotionIKData.BlendWeightInfo>[]
				{
					parts.param3.blendInfos
				});
				array2 = this.GetCorrectShapeValues(this.partners[0].info, this.nowKeyFrame, this.calcBlendBend[index]);
			}
			this.tmpData.pos = array2[0];
			this.tmpData.rot = Quaternion.Euler(array2[1]);
		}
		if (x == null || param2 == null)
		{
			bend.weight = 0f;
		}
		else
		{
			bend.weight = param2.weight;
		}
	}

	// Token: 0x060053EF RID: 21487 RVA: 0x00250250 File Offset: 0x0024E650
	private Transform getTarget(int sex, string frameName)
	{
		if (frameName.IsNullOrEmpty())
		{
			return null;
		}
		Transform result = null;
		Transform[] componentsInChildren;
		if (sex < this.partners.Length)
		{
			componentsInChildren = this.partners[sex].info.GetComponentsInChildren<Transform>();
		}
		else
		{
			int index = sex - this.partners.Length;
			componentsInChildren = this.partners[0].items[index].GetComponentsInChildren<Transform>();
		}
		if (componentsInChildren == null)
		{
			return null;
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			int num = i;
			if (!(componentsInChildren[num].name != frameName))
			{
				result = componentsInChildren[num];
				break;
			}
		}
		return result;
	}

	// Token: 0x060053F0 RID: 21488 RVA: 0x00250300 File Offset: 0x0024E700
	private MotionIKData.Frame FindFrame(MotionIKData.State state, int no)
	{
		MotionIKData.Frame result = default(MotionIKData.Frame);
		result.frameNo = -1;
		if (state == null)
		{
			return result;
		}
		for (int i = 0; i < state.frames.Length; i++)
		{
			int num = i;
			if (state.frames[num].frameNo == no)
			{
				result = state.frames[num];
			}
		}
		return result;
	}

	// Token: 0x060053F1 RID: 21489 RVA: 0x00250370 File Offset: 0x0024E770
	private Transform ListFindBone(List<GameObject> list, string name)
	{
		Transform result = null;
		foreach (GameObject gameObject in list)
		{
			if (!(gameObject.name != name))
			{
				result = gameObject.transform;
				break;
			}
		}
		return result;
	}

	// Token: 0x060053F2 RID: 21490 RVA: 0x002503E8 File Offset: 0x0024E7E8
	public void ChangeWeight(int nameHash, AnimatorStateInfo info)
	{
		if (this.data == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < this.data.states.Length; i++)
		{
			if (Animator.StringToHash(this.data.states[i].name) == nameHash)
			{
				num = i;
				break;
			}
		}
		if (num < 0)
		{
			return;
		}
		if (info.loop)
		{
			this.nowKeyFrame = info.normalizedTime % 1f;
		}
		else
		{
			this.nowKeyFrame = Mathf.Clamp01(info.normalizedTime);
		}
		for (int j = 0; j < 4; j++)
		{
			this.tmpData = this.ikTargetPairs[j].effector.target.GetComponent<BaseData>();
			Vector3[] array = new Vector3[]
			{
				this.tmpData.pos,
				this.tmpData.rot.eulerAngles
			};
			Vector3[] correctShapeValues = this.GetCorrectShapeValues(this.partners[0].info, this.nowKeyFrame, this.calcBlend[j]);
			this.tmpData.pos = correctShapeValues[0];
			this.tmpData.rot = Quaternion.Euler(correctShapeValues[1]);
			this.tmpData = this.ikTargetPairs[j].bend.bendGoal.GetComponent<BaseData>();
			Vector3[] array2 = new Vector3[]
			{
				this.tmpData.pos,
				this.tmpData.rot.eulerAngles
			};
			correctShapeValues = this.GetCorrectShapeValues(this.partners[0].info, this.nowKeyFrame, this.calcBlendBend[j]);
			this.tmpData.pos = correctShapeValues[0];
			this.tmpData.rot = Quaternion.Euler(correctShapeValues[1]);
		}
	}

	// Token: 0x060053F3 RID: 21491 RVA: 0x002505FC File Offset: 0x0024E9FC
	public void CalcBlendSet(ref Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>> calcBlend, params List<MotionIKData.BlendWeightInfo>[] binfo)
	{
		calcBlend[0] = new Dictionary<int, List<MotionIKData.BlendWeightInfo>>();
		calcBlend[1] = new Dictionary<int, List<MotionIKData.BlendWeightInfo>>();
		for (int i = 0; i < binfo.Length; i++)
		{
			foreach (MotionIKData.BlendWeightInfo item in binfo[i])
			{
				if (!calcBlend[i].ContainsKey(item.shape.shapeNo))
				{
					calcBlend[i].Add(item.shape.shapeNo, new List<MotionIKData.BlendWeightInfo>());
				}
				calcBlend[i][item.shape.shapeNo].Add(item);
				List<MotionIKData.BlendWeightInfo> list = calcBlend[i][item.shape.shapeNo];
				if (MotionIK.<>f__mg$cache0 == null)
				{
					MotionIK.<>f__mg$cache0 = new Comparison<MotionIKData.BlendWeightInfo>(MotionIK.Compare);
				}
				list.Sort(MotionIK.<>f__mg$cache0);
			}
		}
	}

	// Token: 0x060053F4 RID: 21492 RVA: 0x00250714 File Offset: 0x0024EB14
	public void CalcBlendBendSet(ref Dictionary<int, List<MotionIKData.BlendWeightInfo>> calcBlendBend, params List<MotionIKData.BlendWeightInfo>[] binfo)
	{
		calcBlendBend = new Dictionary<int, List<MotionIKData.BlendWeightInfo>>();
		for (int i = 0; i < binfo.Length; i++)
		{
			foreach (MotionIKData.BlendWeightInfo item in binfo[i])
			{
				if (!calcBlendBend.ContainsKey(item.shape.shapeNo))
				{
					calcBlendBend.Add(item.shape.shapeNo, new List<MotionIKData.BlendWeightInfo>());
				}
				calcBlendBend[item.shape.shapeNo].Add(item);
				List<MotionIKData.BlendWeightInfo> list = calcBlendBend[item.shape.shapeNo];
				if (MotionIK.<>f__mg$cache1 == null)
				{
					MotionIK.<>f__mg$cache1 = new Comparison<MotionIKData.BlendWeightInfo>(MotionIK.Compare);
				}
				list.Sort(MotionIK.<>f__mg$cache1);
			}
		}
	}

	// Token: 0x060053F5 RID: 21493 RVA: 0x00250800 File Offset: 0x0024EC00
	private static int Compare(MotionIKData.BlendWeightInfo a, MotionIKData.BlendWeightInfo b)
	{
		if (a.StartKey - b.StartKey > 0f)
		{
			return 1;
		}
		if (a.StartKey - b.StartKey < 0f)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x04004EBF RID: 20159
	private BaseData tmpData;

	// Token: 0x04004EC0 RID: 20160
	private BaseProcess tmpProcess;

	// Token: 0x04004EC1 RID: 20161
	public bool MapIK = true;

	// Token: 0x04004EC2 RID: 20162
	private Transform[] DefBone = new Transform[8];

	// Token: 0x04004EC3 RID: 20163
	private List<GameObject> tmpBones;

	// Token: 0x04004EC4 RID: 20164
	private float nowKeyFrame;

	// Token: 0x04004ECC RID: 20172
	public int layerNo;

	// Token: 0x04004ECD RID: 20173
	private MotionIKData tmpMapData;

	// Token: 0x04004ECE RID: 20174
	private Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>[] calcBlend = new Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>[]
	{
		new Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>(),
		new Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>(),
		new Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>(),
		new Dictionary<int, Dictionary<int, List<MotionIKData.BlendWeightInfo>>>()
	};

	// Token: 0x04004ECF RID: 20175
	private Dictionary<int, List<MotionIKData.BlendWeightInfo>>[] calcBlendBend = new Dictionary<int, List<MotionIKData.BlendWeightInfo>>[]
	{
		new Dictionary<int, List<MotionIKData.BlendWeightInfo>>(),
		new Dictionary<int, List<MotionIKData.BlendWeightInfo>>(),
		new Dictionary<int, List<MotionIKData.BlendWeightInfo>>(),
		new Dictionary<int, List<MotionIKData.BlendWeightInfo>>()
	};

	// Token: 0x04004ED0 RID: 20176
	[CompilerGenerated]
	private static Comparison<MotionIKData.BlendWeightInfo> <>f__mg$cache0;

	// Token: 0x04004ED1 RID: 20177
	[CompilerGenerated]
	private static Comparison<MotionIKData.BlendWeightInfo> <>f__mg$cache1;

	// Token: 0x02000B2A RID: 2858
	public class IKTargetPair
	{
		// Token: 0x060053F6 RID: 21494 RVA: 0x0025083C File Offset: 0x0024EC3C
		public IKTargetPair(MotionIK.IKTargetPair.IKTarget target, IKSolverFullBodyBiped solver)
		{
			switch (target)
			{
			case MotionIK.IKTargetPair.IKTarget.LeftHand:
				this.effector = solver.leftHandEffector;
				this.bend = solver.leftArmChain.bendConstraint;
				break;
			case MotionIK.IKTargetPair.IKTarget.RightHand:
				this.effector = solver.rightHandEffector;
				this.bend = solver.rightArmChain.bendConstraint;
				break;
			case MotionIK.IKTargetPair.IKTarget.LeftFoot:
				this.effector = solver.leftFootEffector;
				this.bend = solver.leftLegChain.bendConstraint;
				break;
			case MotionIK.IKTargetPair.IKTarget.RightFoot:
				this.effector = solver.rightFootEffector;
				this.bend = solver.rightLegChain.bendConstraint;
				break;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x002508F2 File Offset: 0x0024ECF2
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x002508FA File Offset: 0x0024ECFA
		public IKEffector effector { get; private set; }

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x00250903 File Offset: 0x0024ED03
		// (set) Token: 0x060053FA RID: 21498 RVA: 0x0025090B File Offset: 0x0024ED0B
		public IKConstraintBend bend { get; private set; }

		// Token: 0x060053FB RID: 21499 RVA: 0x00250914 File Offset: 0x0024ED14
		public static MotionIK.IKTargetPair[] GetPairs(IKSolverFullBodyBiped solver)
		{
			for (int i = 0; i < MotionIK.IKTargetPair.ret.Length; i++)
			{
				int num = i;
				MotionIK.IKTargetPair.ret[num] = new MotionIK.IKTargetPair((MotionIK.IKTargetPair.IKTarget)num, solver);
			}
			return MotionIK.IKTargetPair.ret;
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x00250950 File Offset: 0x0024ED50
		public static int IKTargetLength
		{
			get
			{
				return MotionIK.IKTargetPair.IKTargetNum * 2;
			}
		}

		// Token: 0x04004ED4 RID: 20180
		private static int IKTargetNum = 4;

		// Token: 0x04004ED5 RID: 20181
		private static MotionIK.IKTargetPair[] ret = new MotionIK.IKTargetPair[MotionIK.IKTargetPair.IKTargetNum];

		// Token: 0x02000B2B RID: 2859
		public enum IKTarget
		{
			// Token: 0x04004ED7 RID: 20183
			LeftHand,
			// Token: 0x04004ED8 RID: 20184
			RightHand,
			// Token: 0x04004ED9 RID: 20185
			LeftFoot,
			// Token: 0x04004EDA RID: 20186
			RightFoot,
			// Token: 0x04004EDB RID: 20187
			TotalNum
		}
	}
}
