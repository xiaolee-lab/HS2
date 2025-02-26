using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ReMotion
{
	// Token: 0x020004FB RID: 1275
	internal class TweenEngine
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x00095024 File Offset: 0x00093424
		private TweenEngine()
		{
			this.unhandledExceptionCallback = delegate(Exception ex)
			{
			};
			MainThreadDispatcher.StartUpdateMicroCoroutine(this.RunEveryFrame());
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00095093 File Offset: 0x00093493
		public static void AddTween(ITween tween)
		{
			TweenEngine.Instance.Add(tween);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000950A0 File Offset: 0x000934A0
		private IEnumerator RunEveryFrame()
		{
			for (;;)
			{
				yield return null;
				TweenEngine.Instance.Run(Time.deltaTime, Time.unscaledDeltaTime);
			}
			yield break;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000950B4 File Offset: 0x000934B4
		public void Add(ITween tween)
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				if (this.running)
				{
					this.waitQueue.Enqueue(tween);
					return;
				}
			}
			object obj2 = this.arrayLock;
			lock (obj2)
			{
				if (this.tweens.Length == this.tail)
				{
					Array.Resize<ITween>(ref this.tweens, checked(this.tail * 2));
				}
				this.tweens[this.tail++] = tween;
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0009517C File Offset: 0x0009357C
		public void Run(float deltaTime, float unscaledDeltaTime)
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				this.running = true;
			}
			object obj2 = this.arrayLock;
			lock (obj2)
			{
				int num = this.tail - 1;
				int i = 0;
				while (i < this.tweens.Length)
				{
					ITween tween = this.tweens[i];
					if (tween != null)
					{
						try
						{
							if (tween.MoveNext(ref deltaTime, ref unscaledDeltaTime))
							{
								goto IL_15A;
							}
							this.tweens[i] = null;
						}
						catch (Exception obj3)
						{
							this.tweens[i] = null;
							try
							{
								this.unhandledExceptionCallback(obj3);
							}
							catch
							{
							}
						}
						goto IL_AE;
					}
					goto IL_AE;
					IL_15A:
					i++;
					continue;
					IL_AE:
					while (i < num)
					{
						ITween tween2 = this.tweens[num];
						if (tween2 != null)
						{
							try
							{
								if (!tween2.MoveNext(ref deltaTime, ref unscaledDeltaTime))
								{
									this.tweens[num] = null;
									num--;
									continue;
								}
								this.tweens[i] = tween2;
								this.tweens[num] = null;
								num--;
								goto IL_155;
							}
							catch (Exception obj4)
							{
								this.tweens[num] = null;
								num--;
								try
								{
									this.unhandledExceptionCallback(obj4);
								}
								catch
								{
								}
								continue;
							}
							goto IL_139;
							IL_155:
							goto IL_15A;
						}
						IL_139:
						num--;
					}
					this.tail = i;
					break;
				}
				object obj5 = this.runningAndQueueLock;
				lock (obj5)
				{
					this.running = false;
					while (this.waitQueue.Count != 0)
					{
						if (this.tweens.Length == this.tail)
						{
							Array.Resize<ITween>(ref this.tweens, checked(this.tail * 2));
						}
						this.tweens[this.tail++] = this.waitQueue.Dequeue();
					}
				}
			}
		}

		// Token: 0x04001B17 RID: 6935
		internal static TweenEngine Instance = new TweenEngine();

		// Token: 0x04001B18 RID: 6936
		private const int InitialSize = 16;

		// Token: 0x04001B19 RID: 6937
		private readonly object runningAndQueueLock = new object();

		// Token: 0x04001B1A RID: 6938
		private readonly object arrayLock = new object();

		// Token: 0x04001B1B RID: 6939
		private readonly Action<Exception> unhandledExceptionCallback;

		// Token: 0x04001B1C RID: 6940
		private int tail;

		// Token: 0x04001B1D RID: 6941
		private bool running;

		// Token: 0x04001B1E RID: 6942
		private ITween[] tweens = new ITween[16];

		// Token: 0x04001B1F RID: 6943
		private Queue<ITween> waitQueue = new Queue<ITween>();
	}
}
