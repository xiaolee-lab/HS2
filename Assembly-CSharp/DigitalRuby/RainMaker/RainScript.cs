using System;
using UnityEngine;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E5 RID: 1253
	public class RainScript : BaseRainScript
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x00092750 File Offset: 0x00090B50
		private void UpdateRain()
		{
			if (this.RainFallParticleSystem != null)
			{
				if (this.FollowCamera)
				{
					this.RainFallParticleSystem.shape.shapeType = ParticleSystemShapeType.ConeVolume;
					this.RainFallParticleSystem.transform.position = this.Camera.transform.position;
					this.RainFallParticleSystem.transform.Translate(0f, this.RainHeight, this.RainForwardOffset);
					this.RainFallParticleSystem.transform.rotation = Quaternion.Euler(0f, this.Camera.transform.rotation.eulerAngles.y, 0f);
					if (this.RainMistParticleSystem != null)
					{
						this.RainMistParticleSystem.shape.shapeType = ParticleSystemShapeType.Hemisphere;
						Vector3 position = this.Camera.transform.position;
						position.y += this.RainMistHeight;
						this.RainMistParticleSystem.transform.position = position;
					}
				}
				else
				{
					this.RainFallParticleSystem.shape.shapeType = ParticleSystemShapeType.Box;
					if (this.RainMistParticleSystem != null)
					{
						this.RainMistParticleSystem.shape.shapeType = ParticleSystemShapeType.Box;
						Vector3 position2 = this.RainFallParticleSystem.transform.position;
						position2.y += this.RainMistHeight;
						position2.y -= this.RainHeight;
						this.RainMistParticleSystem.transform.position = position2;
					}
				}
			}
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000928F6 File Offset: 0x00090CF6
		protected override void Start()
		{
			base.Start();
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000928FE File Offset: 0x00090CFE
		protected override void Update()
		{
			base.Update();
			this.UpdateRain();
		}

		// Token: 0x04001AA6 RID: 6822
		[Tooltip("The height above the camera that the rain will start falling from")]
		public float RainHeight = 25f;

		// Token: 0x04001AA7 RID: 6823
		[Tooltip("How far the rain particle system is ahead of the player")]
		public float RainForwardOffset = -7f;

		// Token: 0x04001AA8 RID: 6824
		[Tooltip("The top y value of the mist particles")]
		public float RainMistHeight = 3f;
	}
}
