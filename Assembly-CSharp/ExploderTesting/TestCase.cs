using System;
using System.Collections;
using System.Diagnostics;
using Exploder;
using Exploder.Utils;
using UnityEngine;

namespace ExploderTesting
{
	// Token: 0x020003D3 RID: 979
	internal abstract class TestCase
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x000656A0 File Offset: 0x00063AA0
		protected TestCase()
		{
			this.watch = new Stopwatch();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000656B4 File Offset: 0x00063AB4
		public virtual IEnumerator Run()
		{
			this.watch.Start();
			yield return this.RunTest();
			this.watch.Stop();
			yield break;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000656CF File Offset: 0x00063ACF
		protected ExploderObject Exploder
		{
			get
			{
				return ExploderSingleton.Instance;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000656D6 File Offset: 0x00063AD6
		protected ExploderTester Tester
		{
			get
			{
				return ExploderTester.Instance;
			}
		}

		// Token: 0x06001160 RID: 4448
		protected abstract IEnumerator RunTest();

		// Token: 0x06001161 RID: 4449 RVA: 0x000656DD File Offset: 0x00063ADD
		protected void OnTestSuccess()
		{
			UnityEngine.Debug.LogFormat("Test success {0}", new object[]
			{
				this.ToString()
			});
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000656F8 File Offset: 0x00063AF8
		protected void OnTestFailed(string reason)
		{
			UnityEngine.Debug.LogErrorFormat("Test failed {0}, reason: {1}", new object[]
			{
				this.ToString(),
				reason
			});
		}

		// Token: 0x0400132D RID: 4909
		private Stopwatch watch;
	}
}
