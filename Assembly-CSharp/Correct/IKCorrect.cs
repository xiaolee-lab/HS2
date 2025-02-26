using System;
using System.Collections.Generic;
using System.Linq;
using Correct.Process;
using UnityEngine;

namespace Correct
{
	// Token: 0x02000B3A RID: 2874
	public class IKCorrect : BaseCorrect
	{
		// Token: 0x06005440 RID: 21568 RVA: 0x002527FC File Offset: 0x00250BFC
		public static void AddIKCorrect(GameObject gameObject)
		{
			IKCorrect ikcorrect = gameObject.GetComponent<IKCorrect>();
			if (ikcorrect == null)
			{
				ikcorrect = gameObject.AddComponent<IKCorrect>();
				Transform parent = ikcorrect.transform.parent.parent;
				ikcorrect.list.Clear();
				Transform[] componentsInChildren = ikcorrect.GetComponentsInChildren<Transform>(true);
				IEnumerable<Transform> ikframes = ikcorrect.GetIKFrames(componentsInChildren, parent, ikcorrect);
				List<BaseCorrect.Info> list = new List<BaseCorrect.Info>();
				foreach (Transform component in ikframes)
				{
					list.Add(new BaseCorrect.Info(component)
					{
						type = BaseCorrect.Info.ProcOrderType.Second,
						bone = null
					});
				}
				ikcorrect.list.AddRange(list);
				foreach (BaseCorrect.Info info in ikcorrect.list)
				{
					info.process.type = BaseProcess.Type.Sync;
				}
				return;
			}
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0025292C File Offset: 0x00250D2C
		private IEnumerable<Transform> GetIKFrames(Transform[] t, Transform correctRoot, IKCorrect correct)
		{
			List<string> frameNames = null;
			TestChara component = correctRoot.GetComponent<TestChara>();
			if (component != null)
			{
				frameNames = correct.GetFrameNames(IKCorrect.FrameNames);
				return (from frame in t
				where frameNames.Contains(frame.name)
				select frame).OrderBy(delegate(Transform frame)
				{
					for (int i = 0; i < frameNames.Count; i++)
					{
						if (frameNames[i] == frame.name)
						{
							return i;
						}
					}
					return -1;
				});
			}
			return null;
		}

		// Token: 0x04004F12 RID: 20242
		public static string[] FrameNames = new string[]
		{
			"f_t_hips",
			"f_t_thigh_L",
			"f_t_thigh_R",
			"f_t_shoulder_L",
			"f_t_shoulder_R",
			"f_t_arm_L",
			"f_t_arm_R",
			"f_t_elbo_L",
			"f_t_elbo_R",
			"f_t_knee_L",
			"f_t_knee_R",
			"f_t_leg_L",
			"f_t_leg_R"
		};
	}
}
