using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BA3 RID: 2979
	public class FlyingPetAnimal : MovingPetAnimal
	{
		// Token: 0x06005908 RID: 22792 RVA: 0x00264CAC File Offset: 0x002630AC
		protected override void Initialize()
		{
			if (this.bodyObject != null)
			{
				float y = (!base.ChaseActor) ? this._standbyHeight : this._movingHeight;
				this.bodyObject.transform.localPosition = new Vector3(0f, y, 0f);
			}
			this.StartMoveHeightOffset();
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x00264D10 File Offset: 0x00263110
		private void StartMoveHeightOffset()
		{
			IEnumerator coroutine = this.MoveHeightOffsetCoroutine();
			if (this._moveHeightDisposable != null)
			{
				this._moveHeightDisposable.Dispose();
			}
			this._moveHeightDisposable = Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>();
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00264D6C File Offset: 0x0026316C
		private IEnumerator MoveHeightOffsetCoroutine()
		{
			for (;;)
			{
				if (this.bodyObject != null)
				{
					float num = (!base.ChaseActor) ? this._standbyHeight : this._movingHeight;
					Vector3 localPosition = this.bodyObject.transform.localPosition;
					float f = num - localPosition.y;
					float num2 = Mathf.Abs(f);
					float num3 = this._heightMoveSpeed * Time.deltaTime;
					if (num2 < num3)
					{
						num3 = num2;
					}
					localPosition.y += num3 * Mathf.Sign(f);
					this.bodyObject.transform.localPosition = localPosition;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00264D87 File Offset: 0x00263187
		private void StopMoveHeightOffset()
		{
			if (this._moveHeightDisposable != null)
			{
				this._moveHeightDisposable.Dispose();
				this._moveHeightDisposable = null;
			}
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x00264DA8 File Offset: 0x002631A8
		protected override void AnimationLovelyFollow()
		{
			float num = (!Mathf.Approximately(base.Agent.velocity.magnitude, 0f)) ? 1f : 0f;
			if (!Mathf.Approximately(num, this._animParam))
			{
				float f = num - this._animParam;
				float num2 = Mathf.Sign(f);
				float num3 = this._animParam + this._animLerpValue * Time.deltaTime * num2;
				if (0f < num2)
				{
					if (num < num3)
					{
						num3 = num;
					}
				}
				else if (num3 < num)
				{
					num3 = num;
				}
				this._animParam = num3;
			}
			base.SetFloat(this._locomotionParamName, this._animParam);
		}

		// Token: 0x0400518B RID: 20875
		[SerializeField]
		private float _standbyHeight = 3f;

		// Token: 0x0400518C RID: 20876
		[SerializeField]
		private float _standbyRadius = 2f;

		// Token: 0x0400518D RID: 20877
		[SerializeField]
		private float _movingHeight = 7f;

		// Token: 0x0400518E RID: 20878
		[SerializeField]
		private float _heightMoveSpeed = 1f;

		// Token: 0x0400518F RID: 20879
		private PlayerActor _player;

		// Token: 0x04005190 RID: 20880
		private IDisposable _moveHeightDisposable;
	}
}
