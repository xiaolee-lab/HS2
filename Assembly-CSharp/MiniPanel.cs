using System;
using System.Collections.Generic;
using System.Linq;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020005A2 RID: 1442
public class MiniPanel : MonoBehaviour
{
	// Token: 0x06002169 RID: 8553 RVA: 0x000B86A8 File Offset: 0x000B6AA8
	private void Start()
	{
		if (this.PlayableFXs == null || this.PlayableFXs.Count <= 0)
		{
			this.PlayableFXs = UnityEngine.Object.FindObjectsOfType<SpriteToParticles>().ToList<SpriteToParticles>();
		}
		if (this.PlayableFXs == null || this.PlayableFXs.Count <= 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (!this.wind)
		{
			this.wind = UnityEngine.Object.FindObjectOfType<WindZone>();
		}
		if (!this.wind)
		{
			this.WindButton.gameObject.SetActive(false);
		}
		foreach (SpriteToParticles spriteToParticles in this.PlayableFXs)
		{
			if (spriteToParticles)
			{
				spriteToParticles.OnAvailableToPlay += this.BecameAvailableToPlay;
			}
		}
		this.RefreshButtons();
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x000B87B0 File Offset: 0x000B6BB0
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x000B87D0 File Offset: 0x000B6BD0
	public void TogglePlay()
	{
		bool flag = this.PlayableFXs.TrueForAll((SpriteToParticles x) => x.IsPlaying());
		if (flag)
		{
			foreach (SpriteToParticles spriteToParticles in this.PlayableFXs)
			{
				spriteToParticles.Pause();
			}
		}
		else
		{
			foreach (SpriteToParticles spriteToParticles2 in this.PlayableFXs)
			{
				spriteToParticles2.Play();
			}
		}
		this.RefreshButtons();
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x000B88B0 File Offset: 0x000B6CB0
	public void Stop()
	{
		foreach (SpriteToParticles spriteToParticles in this.PlayableFXs)
		{
			spriteToParticles.Stop();
		}
		this.RefreshButtons();
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000B8914 File Offset: 0x000B6D14
	public void BecameAvailableToPlay()
	{
		this.RefreshButtons();
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000B891C File Offset: 0x000B6D1C
	public void RefreshButtons()
	{
		bool flag = this.PlayableFXs.TrueForAll((SpriteToParticles x) => x.IsPlaying());
		this.PlayButton.gameObject.SetActive(!flag);
		this.PauseButton.gameObject.SetActive(flag);
		bool interactable = this.PlayableFXs.TrueForAll((SpriteToParticles x) => x.IsAvailableToPlay());
		this.PlayButton.interactable = interactable;
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000B89AC File Offset: 0x000B6DAC
	public void ToggleWind()
	{
		if (this.wind)
		{
			this.wind.gameObject.SetActive(!this.wind.gameObject.activeInHierarchy);
		}
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000B89E4 File Offset: 0x000B6DE4
	public void NextScene()
	{
		this.currentScene = SceneManager.GetActiveScene().buildIndex;
		this.currentScene = (this.currentScene + 1) % this.SceneCount;
		this.UnloadCurrentScene();
		base.Invoke("LoadNextScene", 0.1f);
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x000B8A30 File Offset: 0x000B6E30
	public void PreviousScene()
	{
		this.currentScene = SceneManager.GetActiveScene().buildIndex;
		this.currentScene = (this.currentScene - 1 + this.SceneCount) % this.SceneCount;
		this.UnloadCurrentScene();
		base.Invoke("LoadNextScene", 0.1f);
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000B8A84 File Offset: 0x000B6E84
	private void UnloadCurrentScene()
	{
		foreach (SpriteToParticles spriteToParticles in this.PlayableFXs)
		{
			UnityEngine.Object.DestroyImmediate(spriteToParticles.gameObject);
		}
		GC.Collect();
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000B8AF4 File Offset: 0x000B6EF4
	private void LoadNextScene()
	{
		GC.Collect();
		SceneManager.LoadScene(this.currentScene);
	}

	// Token: 0x040020ED RID: 8429
	public List<SpriteToParticles> PlayableFXs;

	// Token: 0x040020EE RID: 8430
	public Button PlayButton;

	// Token: 0x040020EF RID: 8431
	public Button PauseButton;

	// Token: 0x040020F0 RID: 8432
	public Toggle WindButton;

	// Token: 0x040020F1 RID: 8433
	private int SceneCount = 11;

	// Token: 0x040020F2 RID: 8434
	public WindZone wind;

	// Token: 0x040020F3 RID: 8435
	private int currentScene;
}
