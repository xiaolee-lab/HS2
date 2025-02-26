using System;
using UnityEngine;
using UnityEngine.UI;

namespace EpicToonFX
{
	// Token: 0x02000419 RID: 1049
	public class ETFXButtonScript : MonoBehaviour
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x000759C8 File Offset: 0x00073DC8
		private void Start()
		{
			this.effectScript = GameObject.Find("ETFXFireProjectile").GetComponent<ETFXFireProjectile>();
			this.getProjectileNames();
			this.MyButtonText = this.Button.transform.Find("Text").GetComponent<Text>();
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00075A21 File Offset: 0x00073E21
		private void Update()
		{
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00075A34 File Offset: 0x00073E34
		public void getProjectileNames()
		{
			this.projectileScript = this.effectScript.projectiles[this.effectScript.currentProjectile].GetComponent<ETFXProjectileScript>();
			this.projectileParticleName = this.projectileScript.projectileParticle.name;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00075A70 File Offset: 0x00073E70
		public bool overButton()
		{
			Rect rect = new Rect(this.buttonsX, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			Rect rect2 = new Rect(this.buttonsX + this.buttonsDistance, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			return rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || rect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		}

		// Token: 0x0400154B RID: 5451
		public GameObject Button;

		// Token: 0x0400154C RID: 5452
		private Text MyButtonText;

		// Token: 0x0400154D RID: 5453
		private string projectileParticleName;

		// Token: 0x0400154E RID: 5454
		private ETFXFireProjectile effectScript;

		// Token: 0x0400154F RID: 5455
		private ETFXProjectileScript projectileScript;

		// Token: 0x04001550 RID: 5456
		public float buttonsX;

		// Token: 0x04001551 RID: 5457
		public float buttonsY;

		// Token: 0x04001552 RID: 5458
		public float buttonsSizeX;

		// Token: 0x04001553 RID: 5459
		public float buttonsSizeY;

		// Token: 0x04001554 RID: 5460
		public float buttonsDistance;
	}
}
