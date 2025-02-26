using System;
using System.Collections.Generic;
using System.Linq;
using Correct.Process;
using UnityEngine;

namespace Correct
{
	// Token: 0x02000B39 RID: 2873
	public class FrameCorrect : BaseCorrect
	{
		// Token: 0x0600543A RID: 21562 RVA: 0x002521CB File Offset: 0x002505CB
		private void Start()
		{
			FrameCorrect.Init(this);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x002521D4 File Offset: 0x002505D4
		public static void Init(FrameCorrect frameCorrect)
		{
			List<BaseCorrect.Info> list = new List<BaseCorrect.Info>();
			for (int i = 0; i < FrameCorrect.FrameNames.Length; i++)
			{
				for (int j = 0; j < frameCorrect.list.Count; j++)
				{
					if (!(FrameCorrect.FrameNames[i] != frameCorrect.list[j].data.transform.name))
					{
						list.Add(frameCorrect.list[j]);
						if (list[list.Count - 1].data.bone == null)
						{
							list[list.Count - 1].data.bone = list[list.Count - 1].data.transform;
						}
						break;
					}
				}
			}
			frameCorrect.list = new List<BaseCorrect.Info>(list);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x002522C4 File Offset: 0x002506C4
		public static void AddFrameCorrect(GameObject gameObject)
		{
			FrameCorrect frameCorrect = gameObject.GetComponent<FrameCorrect>();
			if (frameCorrect == null)
			{
				frameCorrect = gameObject.AddComponent<FrameCorrect>();
				frameCorrect.list.Clear();
				Transform[] componentsInChildren = frameCorrect.GetComponentsInChildren<Transform>(true);
				IEnumerable<Transform> frames = frameCorrect.GetFrames(componentsInChildren, frameCorrect);
				List<BaseCorrect.Info> list = new List<BaseCorrect.Info>();
				BaseCorrect.Info info = null;
				bool flag = false;
				foreach (Transform transform in frames)
				{
					info = new BaseCorrect.Info(transform);
					foreach (string a in FrameCorrect.bodyNames)
					{
						if (!(a != transform.name))
						{
							flag = true;
						}
					}
					info.type = ((!flag) ? BaseCorrect.Info.ProcOrderType.Last : BaseCorrect.Info.ProcOrderType.First);
					info.bone = transform;
					list.Add(info);
				}
				frameCorrect.list.AddRange(list);
				foreach (BaseCorrect.Info info2 in frameCorrect.list)
				{
					info2.process.type = BaseProcess.Type.Target;
				}
				return;
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00252430 File Offset: 0x00250830
		private IEnumerable<Transform> GetFrames(Transform[] t, FrameCorrect correct)
		{
			List<string> frameNames = null;
			Transform parent = correct.transform.parent.parent;
			TestChara component = parent.GetComponent<TestChara>();
			if (component != null)
			{
				frameNames = correct.GetFrameNames(FrameCorrect.FrameNames);
				return (from frame in t
				where frameNames.Contains(frame.name)
				select frame).OrderBy(delegate(Transform frame)
				{
					for (int i = 0; i < frameNames.Count; i++)
					{
						if (FrameCorrect.FrameNames[i] == frame.name)
						{
							return i;
						}
					}
					return -1;
				});
			}
			return null;
		}

		// Token: 0x04004F0F RID: 20239
		public static string[] FrameNames = new string[]
		{
			"cf_J_Hips",
			"cf_J_Spine01",
			"cf_J_Spine02",
			"cf_J_Spine03",
			"cf_J_Neck",
			"cf_J_Head",
			"cf_J_Kosi01",
			"cf_J_Kosi02",
			"cf_J_Shoulder_L",
			"cf_J_Shoulder_R",
			"cf_J_Toes01_L",
			"cf_J_Toes01_R",
			"cf_J_Hand_Thumb01_L",
			"cf_J_Hand_Thumb02_L",
			"cf_J_Hand_Thumb03_L",
			"cf_J_Hand_Index01_L",
			"cf_J_Hand_Index02_L",
			"cf_J_Hand_Index03_L",
			"cf_J_Hand_Middle01_L",
			"cf_J_Hand_Middle02_L",
			"cf_J_Hand_Middle03_L",
			"cf_J_Hand_Ring01_L",
			"cf_J_Hand_Ring02_L",
			"cf_J_Hand_Ring03_L",
			"cf_J_Hand_Little01_L",
			"cf_J_Hand_Little02_L",
			"cf_J_Hand_Little03_L",
			"cf_J_Hand_Thumb01_R",
			"cf_J_Hand_Thumb02_R",
			"cf_J_Hand_Thumb03_R",
			"cf_J_Hand_Index01_R",
			"cf_J_Hand_Index02_R",
			"cf_J_Hand_Index03_R",
			"cf_J_Hand_Middle01_R",
			"cf_J_Hand_Middle02_R",
			"cf_J_Hand_Middle03_R",
			"cf_J_Hand_Ring01_R",
			"cf_J_Hand_Ring02_R",
			"cf_J_Hand_Ring03_R",
			"cf_J_Hand_Little01_R",
			"cf_J_Hand_Little02_R",
			"cf_J_Hand_Little03_R",
			"cf_J_Mune00_L",
			"cf_J_Mune01_L",
			"cf_J_Mune02_L",
			"cf_J_Mune03_L",
			"cf_J_Mune00_R",
			"cf_J_Mune01_R",
			"cf_J_Mune02_R",
			"cf_J_Mune03_R"
		};

		// Token: 0x04004F10 RID: 20240
		private static string[] bodyNames = new string[]
		{
			"cf_J_Hips",
			"cf_J_Spine01",
			"cf_J_Spine02",
			"cf_J_Spine03",
			"cf_J_Neck",
			"cf_J_Head",
			"cf_J_Kosi01",
			"cf_J_Kosi02",
			"cf_J_Shoulder_L",
			"cf_J_Foot01_L",
			"cf_J_Foot02_L",
			"cf_J_Shoulder_R",
			"cf_J_Foot01_R",
			"cf_J_Foot02_R",
			"cf_J_Mune00_L",
			"cf_J_Mune01_L",
			"cf_J_Mune02_L",
			"cf_J_Mune03_L",
			"cf_J_Mune00_R",
			"cf_J_Mune01_R",
			"cf_J_Mune02_R",
			"cf_J_Mune03_R"
		};

		// Token: 0x04004F11 RID: 20241
		public static string[] RestoreBodyNames = new string[]
		{
			"cf_J_Hips",
			"cf_J_Spine01",
			"cf_J_Spine02",
			"cf_J_Spine03",
			"cf_J_Neck",
			"cf_J_Head",
			"cf_J_Kosi01",
			"cf_J_Kosi02"
		};
	}
}
