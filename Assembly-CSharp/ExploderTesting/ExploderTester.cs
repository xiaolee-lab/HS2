using System;
using System.Collections;
using System.Collections.Generic;
using Exploder;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003CF RID: 975
	public class ExploderTester : MonoBehaviour
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x0006596C File Offset: 0x00063D6C
		private void Start()
		{
			ExploderTester.Instance = this;
			int[] array = new int[]
			{
				0,
				1,
				2,
				5,
				10,
				50,
				100,
				200
			};
			this.cases.Add(new DestroyedObject_Test());
			foreach (int count in array)
			{
				this.cases.Add(new FragmentCountTest(count));
			}
			foreach (int num in array)
			{
				this.cases.Add(new FragmentCountTestMultiple(num, UnityEngine.Random.Range(1, num)));
			}
			foreach (int count2 in array)
			{
				this.cases.Add(new FragmentCrackCount(count2));
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00065A3E File Offset: 0x00063E3E
		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 50f), "Start Test"))
			{
				base.StartCoroutine(this.StartTesting());
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00065A78 File Offset: 0x00063E78
		private IEnumerator StartTesting()
		{
			foreach (TestCase testCase in this.cases)
			{
				yield return testCase.Run();
				yield return new WaitForSeconds(0.3f);
				FragmentPool.Instance.DeactivateFragments();
			}
			yield break;
		}

		// Token: 0x04001326 RID: 4902
		public GameObject[] testObjects;

		// Token: 0x04001327 RID: 4903
		private List<TestCase> cases = new List<TestCase>(255);

		// Token: 0x04001328 RID: 4904
		public static ExploderTester Instance;
	}
}
