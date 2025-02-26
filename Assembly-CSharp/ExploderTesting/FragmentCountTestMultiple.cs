using System;
using System.Collections;
using System.Collections.Generic;
using Exploder;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003D1 RID: 977
	internal class FragmentCountTestMultiple : TestCase
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00065E2C File Offset: 0x0006422C
		public FragmentCountTestMultiple(int target, int count)
		{
			this.target = target;
			this.count = count;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00065E44 File Offset: 0x00064244
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" ",
				this.target,
				" ",
				this.count
			});
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00065E94 File Offset: 0x00064294
		protected override IEnumerator RunTest()
		{
			base.Exploder.TargetFragments = this.target;
			GameObject obj = base.Tester.testObjects[0];
			GameObject[] objs = new GameObject[this.count];
			for (int i = 0; i < this.count; i++)
			{
				objs[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				objs[i].transform.position = obj.transform.position + UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(1f, 10f);
			}
			bool finished = false;
			base.Exploder.ExplodeObjects(delegate(float ms, ExploderObject.ExplosionState state)
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
			}, objs);
			yield return new WaitUntil(() => finished);
			yield break;
		}

		// Token: 0x0400132A RID: 4906
		private int count;

		// Token: 0x0400132B RID: 4907
		private int target;
	}
}
