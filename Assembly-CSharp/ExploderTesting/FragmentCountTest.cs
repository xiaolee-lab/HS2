using System;
using System.Collections;
using System.Collections.Generic;
using Exploder;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003D0 RID: 976
	internal class FragmentCountTest : TestCase
	{
		// Token: 0x06001153 RID: 4435 RVA: 0x00065C23 File Offset: 0x00064023
		public FragmentCountTest(int count)
		{
			this.target = count;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00065C32 File Offset: 0x00064032
		public override string ToString()
		{
			return base.ToString() + " " + this.target;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00065C50 File Offset: 0x00064050
		protected override IEnumerator RunTest()
		{
			base.Exploder.TargetFragments = this.target;
			bool finished = false;
			GameObject obj = base.Tester.testObjects[0];
			ExploderUtils.SetActiveRecursively(obj, true);
			base.Exploder.ExplodeObject(obj, delegate(float ms, ExploderObject.ExplosionState state)
			{
				if (state == ExploderObject.ExplosionState.ExplosionFinished)
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
			yield return new WaitUntil(() => finished);
			yield break;
		}

		// Token: 0x04001329 RID: 4905
		private int target;
	}
}
