using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ReMotion
{
	// Token: 0x020004FA RID: 1274
	public abstract class Tween<TObject, TProperty> : ITween where TObject : class
	{
		// Token: 0x060017DD RID: 6109 RVA: 0x00094554 File Offset: 0x00092954
		public Tween(TweenSettings settings, TObject target, TweenGetter<TObject, TProperty> getter, TweenSetter<TObject, TProperty> setter, EasingFunction easingFunction, float duration, TProperty to, bool isRelativeTo)
		{
			this.Settings = settings;
			this.target = target;
			this.getter = getter;
			this.setter = setter;
			this.duration = duration;
			this.easingFunction = easingFunction;
			this.originalTo = to;
			this.isRelativeTo = isRelativeTo;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000945A4 File Offset: 0x000929A4
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x000945AC File Offset: 0x000929AC
		public TweenSettings Settings { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000945B5 File Offset: 0x000929B5
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x000945BD File Offset: 0x000929BD
		public TweenStatus Status { get; private set; }

		// Token: 0x060017E2 RID: 6114 RVA: 0x000945C8 File Offset: 0x000929C8
		public void Reset()
		{
			this.from = this.originalFrom;
			if (this.isRelativeTo)
			{
				this.to = this.AddOperator(this.from, this.originalTo);
			}
			else
			{
				this.to = this.originalTo;
			}
			this.difference = this.GetDifference(this.from, this.to);
			this.currentTime = 0f;
			this.repeatCount = 0;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0009463F File Offset: 0x00092A3F
		public Tween<TObject, TProperty> Start()
		{
			this.originalFrom = this.getter(this.target);
			this.delayTime = 0f;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0009466A File Offset: 0x00092A6A
		public Tween<TObject, TProperty> Start(TProperty from, float delay)
		{
			this.originalFrom = from;
			if (delay <= 0f)
			{
				delay = 0f;
			}
			this.delayTime = delay;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00094694 File Offset: 0x00092A94
		public Tween<TObject, TProperty> Start(TProperty from, float delay, bool isRelativeFrom)
		{
			this.originalFrom = ((!isRelativeFrom) ? from : this.AddOperator(this.getter(this.target), from));
			if (delay <= 0f)
			{
				delay = 0f;
			}
			this.delayTime = delay;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000946EB File Offset: 0x00092AEB
		public Tween<TObject, TProperty> StartFrom(TProperty from)
		{
			this.originalFrom = from;
			this.delayTime = 0f;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00094706 File Offset: 0x00092B06
		public Tween<TObject, TProperty> StartFromRelative(TProperty from)
		{
			this.originalFrom = this.AddOperator(this.getter(this.target), from);
			this.delayTime = 0f;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00094738 File Offset: 0x00092B38
		public Tween<TObject, TProperty> StartAfter(float delay)
		{
			this.originalFrom = this.getter(this.target);
			if (delay <= 0f)
			{
				delay = 0f;
			}
			this.delayTime = delay;
			this.StartCore();
			return this;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00094774 File Offset: 0x00092B74
		private void StartCore()
		{
			this.Reset();
			switch (this.Status)
			{
			case TweenStatus.Stopped:
				this.Status = TweenStatus.Running;
				TweenEngine.Instance.Add(this);
				return;
			}
			this.Status = TweenStatus.Running;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000947CC File Offset: 0x00092BCC
		public void Stop()
		{
			switch (this.Status)
			{
			case TweenStatus.Stopped:
				return;
			}
			this.Status = TweenStatus.WaitingToStop;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0009480C File Offset: 0x00092C0C
		public void Pause()
		{
			switch (this.Status)
			{
			case TweenStatus.Running:
			case TweenStatus.WaitingToStop:
				this.Status = TweenStatus.Pausing;
				break;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0009484C File Offset: 0x00092C4C
		public void Resume()
		{
			switch (this.Status)
			{
			case TweenStatus.Pausing:
				this.Status = TweenStatus.Running;
				break;
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0009488C File Offset: 0x00092C8C
		public void PauseOrResume()
		{
			switch (this.Status)
			{
			case TweenStatus.Running:
				this.Status = TweenStatus.Pausing;
				break;
			case TweenStatus.Pausing:
				this.Status = TweenStatus.Running;
				break;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000948D8 File Offset: 0x00092CD8
		public IObservable<Unit> ToObservable(bool stopWhenDisposed = true)
		{
			if (this.completedEvent == null)
			{
				this.completedEvent = new Subject<Unit>();
			}
			if (this.Status == TweenStatus.Running)
			{
				IObservable<Unit> observable = this.completedEvent.FirstOrDefault<Unit>();
				return (!stopWhenDisposed) ? observable : observable.DoOnCancel(delegate
				{
					this.Stop();
				});
			}
			return Observable.Defer<Unit>(delegate
			{
				if (this.Status == TweenStatus.Stopped)
				{
					this.Start();
				}
				IObservable<Unit> observable2 = this.completedEvent.FirstOrDefault<Unit>();
				return (!stopWhenDisposed) ? observable2 : observable2.DoOnCancel(delegate
				{
					this.Stop();
				});
			});
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0009495C File Offset: 0x00092D5C
		public void AttachSafe(GameObject gameObject)
		{
			gameObject.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				this.Stop();
			});
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00094976 File Offset: 0x00092D76
		public void AttachSafe(Component component)
		{
			component.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				this.Stop();
			});
		}

		// Token: 0x060017F1 RID: 6129
		protected abstract TProperty AddOperator(TProperty left, TProperty right);

		// Token: 0x060017F2 RID: 6130
		protected abstract TProperty GetDifference(TProperty from, TProperty to);

		// Token: 0x060017F3 RID: 6131
		protected abstract void CreateValue(ref TProperty from, ref TProperty difference, ref float ratio, out TProperty value);

		// Token: 0x060017F4 RID: 6132 RVA: 0x00094990 File Offset: 0x00092D90
		public bool MoveNext(ref float deltaTime, ref float unscaledDeltaTime)
		{
			switch (this.Status)
			{
			case TweenStatus.Running:
			{
				if (this.delayTime != 0f)
				{
					this.delayTime -= ((!this.Settings.IsIgnoreTimeScale) ? deltaTime : unscaledDeltaTime);
					if (this.delayTime > 0f)
					{
						return true;
					}
					this.delayTime = 0f;
				}
				float num = (!this.Settings.IsIgnoreTimeScale) ? (this.currentTime += deltaTime) : (this.currentTime += unscaledDeltaTime);
				bool flag = false;
				if (num >= this.duration)
				{
					num = this.duration;
					flag = true;
				}
				float num2 = this.easingFunction(num, this.duration);
				TProperty tproperty;
				this.CreateValue(ref this.from, ref this.difference, ref num2, out tproperty);
				this.setter(this.target, ref tproperty);
				if (flag)
				{
					this.repeatCount++;
					switch (this.Settings.LoopType)
					{
					case LoopType.None:
						goto IL_1C8;
					case LoopType.Restart:
						this.from = this.originalFrom;
						this.currentTime = 0f;
						return true;
					case LoopType.Cycle:
						break;
					case LoopType.CycleOnce:
						if (this.repeatCount == 2)
						{
							return false;
						}
						break;
					default:
						goto IL_1C8;
					}
					TProperty tproperty2 = this.from;
					this.from = this.to;
					this.to = tproperty2;
					this.difference = this.GetDifference(this.from, this.to);
					this.currentTime = 0f;
					return true;
					IL_1C8:
					if (this.completedEvent != null)
					{
						this.completedEvent.OnNext(Unit.Default);
					}
					return false;
				}
				return true;
			}
			case TweenStatus.Pausing:
				return true;
			}
			this.Status = TweenStatus.Stopped;
			return false;
		}

		// Token: 0x04001B08 RID: 6920
		private readonly TObject target;

		// Token: 0x04001B09 RID: 6921
		private readonly TweenGetter<TObject, TProperty> getter;

		// Token: 0x04001B0A RID: 6922
		private readonly TweenSetter<TObject, TProperty> setter;

		// Token: 0x04001B0B RID: 6923
		private readonly EasingFunction easingFunction;

		// Token: 0x04001B0C RID: 6924
		private readonly float duration;

		// Token: 0x04001B0D RID: 6925
		private readonly bool isRelativeTo;

		// Token: 0x04001B0E RID: 6926
		private TProperty from;

		// Token: 0x04001B0F RID: 6927
		private TProperty to;

		// Token: 0x04001B10 RID: 6928
		private TProperty difference;

		// Token: 0x04001B11 RID: 6929
		private TProperty originalFrom;

		// Token: 0x04001B12 RID: 6930
		private TProperty originalTo;

		// Token: 0x04001B13 RID: 6931
		private Subject<Unit> completedEvent;

		// Token: 0x04001B14 RID: 6932
		private float delayTime;

		// Token: 0x04001B15 RID: 6933
		private float currentTime;

		// Token: 0x04001B16 RID: 6934
		private int repeatCount;
	}
}
