using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200041E RID: 1054
public class ETFXSceneManager : MonoBehaviour
{
	// Token: 0x06001338 RID: 4920 RVA: 0x00076353 File Offset: 0x00074753
	public void LoadScene1()
	{
		SceneManager.LoadScene("etfx_explosions");
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x0007635F File Offset: 0x0007475F
	public void LoadScene2()
	{
		SceneManager.LoadScene("etfx_explosions2");
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0007636B File Offset: 0x0007476B
	public void LoadScene3()
	{
		SceneManager.LoadScene("etfx_portals");
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x00076377 File Offset: 0x00074777
	public void LoadScene4()
	{
		SceneManager.LoadScene("etfx_magic");
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00076383 File Offset: 0x00074783
	public void LoadScene5()
	{
		SceneManager.LoadScene("etfx_emojis");
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x0007638F File Offset: 0x0007478F
	public void LoadScene6()
	{
		SceneManager.LoadScene("etfx_sparkles");
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x0007639B File Offset: 0x0007479B
	public void LoadScene7()
	{
		SceneManager.LoadScene("etfx_fireworks");
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000763A7 File Offset: 0x000747A7
	public void LoadScene8()
	{
		SceneManager.LoadScene("etfx_powerups");
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000763B3 File Offset: 0x000747B3
	public void LoadScene9()
	{
		SceneManager.LoadScene("etfx_swordcombat");
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000763BF File Offset: 0x000747BF
	public void LoadScene10()
	{
		SceneManager.LoadScene("etfx_maindemo");
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000763CB File Offset: 0x000747CB
	public void LoadScene11()
	{
		SceneManager.LoadScene("etfx_combat");
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000763D7 File Offset: 0x000747D7
	public void LoadScene12()
	{
		SceneManager.LoadScene("etfx_2ddemo");
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000763E3 File Offset: 0x000747E3
	public void LoadScene13()
	{
		SceneManager.LoadScene("etfx_missiles");
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000763F0 File Offset: 0x000747F0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			this.GUIHide = !this.GUIHide;
			if (this.GUIHide)
			{
				GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = false;
			}
			else
			{
				GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.J))
		{
			this.GUIHide2 = !this.GUIHide2;
			if (this.GUIHide2)
			{
				GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
			}
			else
			{
				GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			this.GUIHide3 = !this.GUIHide3;
			if (this.GUIHide3)
			{
				GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas>().enabled = false;
			}
			else
			{
				GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas>().enabled = true;
			}
		}
	}

	// Token: 0x04001572 RID: 5490
	public bool GUIHide;

	// Token: 0x04001573 RID: 5491
	public bool GUIHide2;

	// Token: 0x04001574 RID: 5492
	public bool GUIHide3;
}
