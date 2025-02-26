using System;
using System.Collections;
using System.Collections.Generic;
using Exploder;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003D2 RID: 978
	internal class FragmentCrackCount : TestCase
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x000660E4 File Offset: 0x000644E4
		public FragmentCrackCount(int count)
		{
			this.target = count;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000660F3 File Offset: 0x000644F3
		public override string ToString()
		{
			return base.ToString() + " " + this.target;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00066110 File Offset: 0x00064510
		protected override IEnumerator RunTest()
		{
			base.Exploder.TargetFragments = this.target;
			bool finished = false;
			GameObject obj = base.Tester.testObjects[0];
			ExploderUtils.SetActiveRecursively(obj, true);
			base.Exploder.CrackObject(obj, delegate(float ms, ExploderObject.ExplosionState state)
			{
				if (state == ExploderObject.ExplosionState.ObjectCracked)
				{
					this.Exploder.ExplodeCracked(obj, delegate(float timeMs, ExploderObject.ExplosionState explosionState)
					{
						if (explosionState == ExploderObject.ExplosionState.ExplosionFinished)
						{
							List<Fragment> activeFragments = FragmentPool.Instance.GetActiveFragments();
							if (activeFragments.Count == this.target)
							{
								this.OnTestSuccess();
							}
							else
							{
								this.OnTestFailed(string.Format("Invalid number of result fragmens: {0}, expected: {1}", activeFragments.Count, this.target));
							}
							finished = true;
						}
					});
				}
			});
			yield return new WaitUntil(() => finished);
			yield break;
		}

		// Token: 0x0400132C RID: 4908
		private int target;
	}
}
