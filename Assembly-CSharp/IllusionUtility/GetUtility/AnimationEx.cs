using System;
using System.Collections;
using UnityEngine;

namespace IllusionUtility.GetUtility
{
	// Token: 0x02001187 RID: 4487
	public static class AnimationEx
	{
		// Token: 0x060093F5 RID: 37877 RVA: 0x003D2904 File Offset: 0x003D0D04
		public static AnimationClip GetPlayingClip(this Animation animation)
		{
			AnimationClip result = null;
			float num = 0f;
			IEnumerator enumerator = animation.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (animationState.enabled)
					{
						if (num < animationState.weight)
						{
							num = animationState.weight;
							result = animationState.clip;
						}
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return result;
		}
	}
}
