using System;
using System.Runtime.CompilerServices;
using DigitalRuby.RainMaker;
using UniRx;
using UnityEngine;

// Token: 0x02000F02 RID: 3842
public class RainControl : BaseRainScript
{
	// Token: 0x170018AC RID: 6316
	// (get) Token: 0x06007DA0 RID: 32160 RVA: 0x0035ABCA File Offset: 0x00358FCA
	public Vector3 RainOffset
	{
		[CompilerGenerated]
		get
		{
			return this._rainOffset;
		}
	}

	// Token: 0x170018AD RID: 6317
	// (get) Token: 0x06007DA1 RID: 32161 RVA: 0x0035ABD2 File Offset: 0x00358FD2
	public Vector3 MistOffset
	{
		[CompilerGenerated]
		get
		{
			return this._mistOffset;
		}
	}

	// Token: 0x06007DA2 RID: 32162 RVA: 0x0035ABDA File Offset: 0x00358FDA
	protected override void Start()
	{
		base.Start();
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
	}

	// Token: 0x06007DA3 RID: 32163 RVA: 0x0035AC15 File Offset: 0x00359015
	private void OnUpdate()
	{
		base.Update();
		this.UpdateRain();
	}

	// Token: 0x06007DA4 RID: 32164 RVA: 0x0035AC23 File Offset: 0x00359023
	protected override void Update()
	{
	}

	// Token: 0x06007DA5 RID: 32165 RVA: 0x0035AC28 File Offset: 0x00359028
	private void UpdateRain()
	{
		if (this.FollowCamera)
		{
			if (this.RainFallParticleSystem != null)
			{
				this.RainFallParticleSystem.shape.shapeType = ParticleSystemShapeType.ConeVolume;
				this.RainFallParticleSystem.transform.position = this.Camera.transform.position;
				this.RainFallParticleSystem.transform.Translate(this._rainOffset);
				this.RainFallParticleSystem.transform.rotation = Quaternion.Euler(0f, this.Camera.transform.eulerAngles.y, 0f);
			}
			if (this.RainMistParticleSystem != null)
			{
				this.RainMistParticleSystem.shape.shapeType = ParticleSystemShapeType.Hemisphere;
				Vector3 position = this.Camera.transform.position;
				position.y += this._mistOffset.y;
				this.RainMistParticleSystem.transform.position = position;
			}
		}
		else
		{
			if (this.RainFallParticleSystem != null)
			{
				this.RainFallParticleSystem.shape.shapeType = ParticleSystemShapeType.Box;
			}
			if (this.RainMistParticleSystem != null)
			{
				this.RainMistParticleSystem.shape.shapeType = ParticleSystemShapeType.Box;
				Vector3 vector = this.RainFallParticleSystem.transform.position;
				vector += this._mistOffset;
				vector.y -= this._rainOffset.y;
				this.RainMistParticleSystem.transform.position = vector;
			}
		}
	}

	// Token: 0x0400658A RID: 25994
	[SerializeField]
	private Vector3 _rainOffset = new Vector3(0f, 25f, -7f);

	// Token: 0x0400658B RID: 25995
	[SerializeField]
	private Vector3 _mistOffset = new Vector3(0f, 3f, 0f);
}
