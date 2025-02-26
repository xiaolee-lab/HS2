using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Correct;
using Manager;
using UnityEngine;

// Token: 0x02000B2C RID: 2860
public class MotionIKData
{
	// Token: 0x060053FF RID: 21503 RVA: 0x00250984 File Offset: 0x0024ED84
	public MotionIKData Copy()
	{
		MotionIKData motionIKData = new MotionIKData();
		int num = this.states.Length;
		motionIKData.states = new MotionIKData.State[num];
		for (int i = 0; i < num; i++)
		{
			MotionIKData.State state = new MotionIKData.State();
			MotionIKData.State state2 = this.states[i];
			state.name = state2.name;
			int num2 = state.parts.Length;
			for (int j = 0; j < num2; j++)
			{
				MotionIKData.Parts parts = state.parts[j];
				MotionIKData.Parts parts2 = state2.parts[j];
				parts.param2.sex = parts2.param2.sex;
				parts.param2.target = parts2.param2.target;
				parts.param2.weightPos = parts2.param2.weightPos;
				parts.param2.weightAng = parts2.param2.weightAng;
				parts.param2.blendInfos[0] = new List<MotionIKData.BlendWeightInfo>(parts2.param2.blendInfos[0]);
				parts.param2.blendInfos[1] = new List<MotionIKData.BlendWeightInfo>(parts2.param2.blendInfos[1]);
				parts.param3.chein = parts2.param3.chein;
				parts.param3.weight = parts2.param3.weight;
				parts.param3.blendInfos = new List<MotionIKData.BlendWeightInfo>(parts2.param3.blendInfos);
			}
			int num3 = state2.frames.Length;
			state.frames = new MotionIKData.Frame[num3];
			for (int k = 0; k < num3; k++)
			{
				MotionIKData.Frame frame = default(MotionIKData.Frame);
				MotionIKData.Frame frame2 = state2.frames[k];
				frame.frameNo = frame2.frameNo;
				frame.editNo = frame2.editNo;
				int num4 = frame2.shapes.Length;
				frame.shapes = new MotionIKData.Shape[num4];
				for (int l = 0; l < num4; l++)
				{
					MotionIKData.Shape shape = default(MotionIKData.Shape);
					MotionIKData.Shape shape2 = frame2.shapes[l];
					shape.shapeNo = shape2.shapeNo;
					for (int m = 0; m < 3; m++)
					{
						shape[m] = new MotionIKData.PosAng?(default(MotionIKData.PosAng));
						MotionIKData.PosAng value = shape[m].Value;
						for (int n = 0; n < 3; n++)
						{
							value.pos[n] = shape2[m].Value.pos[n];
						}
						for (int num5 = 0; num5 < 3; num5++)
						{
							value.ang[num5] = shape2[m].Value.ang[num5];
						}
						shape[m] = new MotionIKData.PosAng?(value);
					}
					frame.shapes[l] = shape;
				}
				state.frames[k] = frame;
			}
			motionIKData.states[i] = state;
		}
		return motionIKData;
	}

	// Token: 0x06005400 RID: 21504 RVA: 0x00250CD8 File Offset: 0x0024F0D8
	public MotionIKData.State InitState(string stateName, int sex)
	{
		int num = -1;
		if (this.states != null)
		{
			int num2 = -1;
			while (++num2 < this.states.Length && !(this.states[num2].name == stateName))
			{
			}
			num = ((num2 < this.states.Length) ? num2 : -1);
		}
		if (num == -1)
		{
			MotionIKData.State[] array = new MotionIKData.State[]
			{
				new MotionIKData.State
				{
					name = stateName
				}
			};
			if (this.states == null)
			{
				this.states = array;
			}
			else
			{
				this.states = this.states.Concat(array).ToArray<MotionIKData.State>();
			}
			num = this.states.Length - 1;
		}
		MotionIKData.State state = this.states[num];
		MotionIKData.InitFrame(state, sex);
		return state;
	}

	// Token: 0x06005401 RID: 21505 RVA: 0x00250DA5 File Offset: 0x0024F1A5
	public void Release()
	{
		this.states = null;
	}

	// Token: 0x06005402 RID: 21506 RVA: 0x00250DB0 File Offset: 0x0024F1B0
	public static void InitFrame(MotionIKData.State state, int sex)
	{
		int ikLen = MotionIK.IKTargetPair.IKTargetLength;
		IEnumerable<MotionIKData.Frame> first = from i in Enumerable.Range(0, ikLen)
		select new MotionIKData.Frame
		{
			frameNo = i
		};
		IEnumerable<MotionIKData.Frame> second = from i in Enumerable.Range(0, FrameCorrect.FrameNames.Length)
		select new MotionIKData.Frame
		{
			frameNo = i + ikLen
		};
		IEnumerable<MotionIKData.Frame> source = first.Concat(second);
		state.frames = source.ToArray<MotionIKData.Frame>();
		for (int j = 0; j < state.frames.Length; j++)
		{
			MotionIKData.InitShape(ref state.frames[j]);
		}
	}

	// Token: 0x06005403 RID: 21507 RVA: 0x00250E60 File Offset: 0x0024F260
	public static void InitShape(ref MotionIKData.Frame frame)
	{
		frame.shapes = (from i in Enumerable.Range(0, ChaFileDefine.cf_bodyshapename.Length)
		select new MotionIKData.Shape
		{
			shapeNo = i
		}).ToArray<MotionIKData.Shape>();
		for (int j = 0; j < frame.shapes.Length; j++)
		{
			MotionIKData.Shape shape = frame.shapes[j];
			shape.small = default(MotionIKData.PosAng);
			shape.mediam = default(MotionIKData.PosAng);
			shape.large = default(MotionIKData.PosAng);
		}
	}

	// Token: 0x17000F0F RID: 3855
	// (get) Token: 0x06005404 RID: 21508 RVA: 0x00250F03 File Offset: 0x0024F303
	// (set) Token: 0x06005405 RID: 21509 RVA: 0x00250F0B File Offset: 0x0024F30B
	public MotionIKData.State[] states
	{
		get
		{
			return this._states;
		}
		set
		{
			this._states = value;
		}
	}

	// Token: 0x06005406 RID: 21510 RVA: 0x00250F14 File Offset: 0x0024F314
	public void AIRead(string abName, string assetName, bool add = false)
	{
		if (!GlobalMethod.AssetFileExist(abName, assetName, string.Empty))
		{
			return;
		}
		this.data = null;
		this.data = CommonLib.LoadAsset<ExcelData>(abName, assetName, false, string.Empty);
		Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(abName);
		if (this.data == null)
		{
			return;
		}
		int i = 1;
		int num = this.data.list[i].list.Count - 4;
		int num2 = 0;
		int maxCell = this.data.MaxCell;
		MotionIKData.State[] array;
		if (!add)
		{
			array = new MotionIKData.State[num];
		}
		else
		{
			num2 = this.states.Length;
			array = new MotionIKData.State[num + num2];
			for (int j = 0; j < num2; j++)
			{
				array[j] = new MotionIKData.State(this.states[j]);
			}
		}
		string a = string.Empty;
		string text = string.Empty;
		string text2 = string.Empty;
		for (int k = 0; k < num; k++)
		{
			int num3 = k;
			int num4 = num3;
			if (add)
			{
				num4 += num2;
			}
			array[num4] = new MotionIKData.State();
			array[num4].frames = new MotionIKData.Frame[FrameCorrect.FrameNames.Length + 8];
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			bool flag = true;
			i = 1;
			while (i < maxCell)
			{
				this.row = this.data.list[i++].list;
				int count = this.row.Count;
				int num8 = 4 + num3;
				if (num8 < count)
				{
					text2 = this.row[num8];
				}
				else
				{
					text2 = null;
				}
				bool flag2 = text2.IsNullOrEmpty();
				if (i < 28)
				{
					if (num8 < count)
					{
						text = this.row[3];
					}
					else
					{
						text = null;
					}
					switch (text)
					{
					case "anime name":
						array[num4].name = text2;
						break;
					case "sex":
						num5++;
						if (num8 < count)
						{
							a = this.row[0];
						}
						else
						{
							a = null;
						}
						if (a != string.Empty)
						{
							int.TryParse(text2, out array[num4].parts[num5].param2.sex);
						}
						break;
					case "target":
						array[num4].parts[num5].param2.target = text2;
						break;
					case "chain":
						array[num4].parts[num5].param3.chein = text2;
						break;
					case "weight pos":
						float.TryParse(text2, out array[num4].parts[num5].param2.weightPos);
						break;
					case "weight rot":
						float.TryParse(text2, out array[num4].parts[num5].param2.weightAng);
						break;
					case "weight":
						float.TryParse(text2, out array[num4].parts[num5].param3.weight);
						break;
					}
				}
				else if (i < 86)
				{
					num6++;
					if (!flag2)
					{
						int.TryParse(text2, out array[num4].frames[num6].editNo);
					}
					array[num4].frames[num6].frameNo = num6;
					array[num4].frames[num6].shapes = new MotionIKData.Shape[ChaFileDefine.cf_bodyshapename.Length];
					num7 = -1;
				}
				else
				{
					if (count > 3)
					{
						text = this.row[3];
					}
					else
					{
						text = null;
					}
					switch (text)
					{
					case "Sx":
						if (flag)
						{
							if (count > 0)
							{
								a = this.row[0];
							}
							else
							{
								a = null;
							}
							if (a != string.Empty)
							{
								num6++;
								num7 = -1;
							}
							num7++;
							array[num4].frames[num6].shapes[num7].shapeNo = num7;
						}
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.pos.x);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.ang.x);
							}
						}
						continue;
					case "Sy":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.pos.y);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.ang.y);
							}
						}
						continue;
					case "Sz":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.pos.z);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].small.ang.z);
							}
						}
						continue;
					case "Mx":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.pos.x);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.ang.x);
							}
						}
						continue;
					case "My":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.pos.y);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.ang.y);
							}
						}
						continue;
					case "Mz":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.pos.z);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].mediam.ang.z);
							}
						}
						continue;
					case "Lx":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.pos.x);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.ang.x);
							}
						}
						continue;
					case "Ly":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.pos.y);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.ang.y);
							}
						}
						continue;
					case "Lz":
						if (!flag2)
						{
							if (flag)
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.pos.z);
							}
							else
							{
								float.TryParse(text2, out array[num4].frames[num6].shapes[num7].large.ang.z);
							}
						}
						flag ^= true;
						continue;
					}
					if (count > 0)
					{
						a = this.row[0];
					}
					else
					{
						a = null;
					}
					if (a == "フレーム")
					{
						num6 = -1;
					}
				}
			}
		}
		this.states = new MotionIKData.State[array.Length];
		for (int l = 0; l < array.Length; l++)
		{
			this.states[l] = array[l];
		}
	}

	// Token: 0x04004EDC RID: 20188
	private ExcelData data;

	// Token: 0x04004EDD RID: 20189
	private List<string> row = new List<string>();

	// Token: 0x04004EDE RID: 20190
	private MotionIKData.State[] _states;

	// Token: 0x02000B2D RID: 2861
	public class State
	{
		// Token: 0x06005409 RID: 21513 RVA: 0x002519DA File Offset: 0x0024FDDA
		public State()
		{
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00251A1C File Offset: 0x0024FE1C
		public State(MotionIKData.State src)
		{
			this.name = src.name;
			this.leftHand = src.leftHand;
			this.rightHand = src.rightHand;
			this.leftFoot = src.leftFoot;
			this.rightFoot = src.rightFoot;
			this.frames = src.frames;
		}

		// Token: 0x17000F10 RID: 3856
		public MotionIKData.Parts this[int index]
		{
			get
			{
				return this.parts[index];
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x00251AB8 File Offset: 0x0024FEB8
		public MotionIKData.Parts[] parts
		{
			get
			{
				return new MotionIKData.Parts[]
				{
					this.leftHand,
					this.rightHand,
					this.leftFoot,
					this.rightFoot
				};
			}
		}

		// Token: 0x04004EE3 RID: 20195
		public string name = string.Empty;

		// Token: 0x04004EE4 RID: 20196
		public MotionIKData.Parts leftHand = new MotionIKData.Parts();

		// Token: 0x04004EE5 RID: 20197
		public MotionIKData.Parts rightHand = new MotionIKData.Parts();

		// Token: 0x04004EE6 RID: 20198
		public MotionIKData.Parts leftFoot = new MotionIKData.Parts();

		// Token: 0x04004EE7 RID: 20199
		public MotionIKData.Parts rightFoot = new MotionIKData.Parts();

		// Token: 0x04004EE8 RID: 20200
		public MotionIKData.Frame[] frames;
	}

	// Token: 0x02000B2E RID: 2862
	public class Parts
	{
		// Token: 0x04004EE9 RID: 20201
		public MotionIKData.Param2 param2 = new MotionIKData.Param2();

		// Token: 0x04004EEA RID: 20202
		public MotionIKData.Param3 param3 = new MotionIKData.Param3();
	}

	// Token: 0x02000B2F RID: 2863
	public class Param2
	{
		// Token: 0x17000F12 RID: 3858
		public object this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.sex;
				case 1:
					return this.target;
				case 2:
					return this.weightPos;
				case 3:
					return this.weightAng;
				default:
					return null;
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.sex = ((!(value is string)) ? ((int)value) : int.Parse((string)value));
					break;
				case 1:
					this.target = (string)value;
					break;
				case 2:
					this.weightPos = ((!(value is string)) ? ((float)value) : float.Parse((string)value));
					break;
				case 3:
					this.weightAng = ((!(value is string)) ? ((float)value) : float.Parse((string)value));
					break;
				}
			}
		}

		// Token: 0x04004EEB RID: 20203
		public int sex;

		// Token: 0x04004EEC RID: 20204
		public string target = string.Empty;

		// Token: 0x04004EED RID: 20205
		public float weightPos;

		// Token: 0x04004EEE RID: 20206
		public float weightAng;

		// Token: 0x04004EEF RID: 20207
		public List<MotionIKData.BlendWeightInfo>[] blendInfos = new List<MotionIKData.BlendWeightInfo>[]
		{
			new List<MotionIKData.BlendWeightInfo>(),
			new List<MotionIKData.BlendWeightInfo>()
		};
	}

	// Token: 0x02000B30 RID: 2864
	public class Param3
	{
		// Token: 0x17000F13 RID: 3859
		public object this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.chein;
				}
				if (index != 1)
				{
					return null;
				}
				return this.weight;
			}
			set
			{
				if (index != 0)
				{
					if (index == 1)
					{
						this.weight = ((!(value is string)) ? ((float)value) : float.Parse((string)value));
					}
				}
				else
				{
					this.chein = (string)value;
				}
			}
		}

		// Token: 0x04004EF0 RID: 20208
		public string chein = string.Empty;

		// Token: 0x04004EF1 RID: 20209
		public float weight;

		// Token: 0x04004EF2 RID: 20210
		public List<MotionIKData.BlendWeightInfo> blendInfos = new List<MotionIKData.BlendWeightInfo>();
	}

	// Token: 0x02000B31 RID: 2865
	public struct Frame
	{
		// Token: 0x04004EF3 RID: 20211
		public int frameNo;

		// Token: 0x04004EF4 RID: 20212
		public int editNo;

		// Token: 0x04004EF5 RID: 20213
		public MotionIKData.Shape[] shapes;
	}

	// Token: 0x02000B32 RID: 2866
	public struct Shape
	{
		// Token: 0x17000F14 RID: 3860
		public MotionIKData.PosAng? this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return new MotionIKData.PosAng?(this.small);
				case 1:
					return new MotionIKData.PosAng?(this.mediam);
				case 2:
					return new MotionIKData.PosAng?(this.large);
				default:
					return null;
				}
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (index != 0)
				{
					if (index != 1)
					{
						if (index == 2)
						{
							this.large = value.Value;
						}
					}
					else
					{
						this.mediam = value.Value;
					}
				}
				else
				{
					this.small = value.Value;
				}
			}
		}

		// Token: 0x04004EF6 RID: 20214
		public int shapeNo;

		// Token: 0x04004EF7 RID: 20215
		public MotionIKData.PosAng small;

		// Token: 0x04004EF8 RID: 20216
		public MotionIKData.PosAng mediam;

		// Token: 0x04004EF9 RID: 20217
		public MotionIKData.PosAng large;
	}

	// Token: 0x02000B33 RID: 2867
	public struct PosAng
	{
		// Token: 0x17000F15 RID: 3861
		public Vector3 this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.pos;
				}
				if (index != 1)
				{
					return Vector3.zero;
				}
				return this.ang;
			}
			set
			{
				if (index != 0)
				{
					if (index == 1)
					{
						this.ang = value;
					}
				}
				else
				{
					this.pos = value;
				}
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x00251DFB File Offset: 0x002501FB
		public float[] posArray
		{
			get
			{
				return new float[]
				{
					this.pos.x,
					this.pos.y,
					this.pos.z
				};
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x00251E2D File Offset: 0x0025022D
		public float[] angArray
		{
			get
			{
				return new float[]
				{
					this.ang.x,
					this.ang.y,
					this.ang.z
				};
			}
		}

		// Token: 0x04004EFA RID: 20218
		public Vector3 pos;

		// Token: 0x04004EFB RID: 20219
		public Vector3 ang;
	}

	// Token: 0x02000B34 RID: 2868
	public struct BlendWeightInfo
	{
		// Token: 0x04004EFC RID: 20220
		public int pattern;

		// Token: 0x04004EFD RID: 20221
		public float StartKey;

		// Token: 0x04004EFE RID: 20222
		public float EndKey;

		// Token: 0x04004EFF RID: 20223
		public MotionIKData.Shape shape;
	}
}
