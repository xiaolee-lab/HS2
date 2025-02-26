using System;
using System.Collections;
using Exploder;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003CE RID: 974
	internal class DestroyedObject_Test : TestCase
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x000657CC File Offset: 0x00063BCC
		protected override IEnumerator RunTest()
		{
			bool finished = false;
			GameObject testObj = base.Tester.testObjects[0];
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.transform.position = testObj.transform.position;
			ExploderUtils.SetActiveRecursively(obj, true);
			UnityEngine.Object.DestroyImmediate(obj);
			base.Exploder.ExplodeObject(obj, delegate(float ms, ExploderObject.ExplosionState state)
			{
				if (state == ExploderObject.ExplosionState.ExplosionFinished)
				{
					this.OnTestSuccess();
					finished = true;
				}
			});
			yield return new WaitUntil(() => finished);
			yield break;
		}
	}
}
