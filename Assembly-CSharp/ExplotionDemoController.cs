using System;
using SpriteToParticlesAsset;
using UnityEngine;

// Token: 0x020005A1 RID: 1441
public class ExplotionDemoController : MonoBehaviour
{
	// Token: 0x06002163 RID: 8547 RVA: 0x000B84FF File Offset: 0x000B68FF
	private void Start()
	{
		this.cursor.Show(true);
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x000B8510 File Offset: 0x000B6910
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.SpawnRanger(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.SpawnRanger(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.SpawnRanger(2);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.Explode();
		}
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x000B8568 File Offset: 0x000B6968
	public void SpawnRanger(int index)
	{
		base.CancelInvoke("SpawnRanger");
		if (index >= 0)
		{
			this.lastRangerIndex = index;
		}
		if (this.currentRanger && !this.currentRanger.exploded)
		{
			UnityEngine.Object.DestroyImmediate(this.currentRanger.gameObject);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rangerPrefabs[this.lastRangerIndex], this.spawnPosition.position, Quaternion.identity);
		this.currentRanger = gameObject.GetComponent<EffectorExplode>();
		this.cursor.Show(true);
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x000B85F9 File Offset: 0x000B69F9
	public void SpawnRanger()
	{
		this.SpawnRanger(-1);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x000B8604 File Offset: 0x000B6A04
	public void Explode()
	{
		if (this.currentRanger)
		{
			this.currentRanger.ExplodeAt(this.cursor.transform.position, this.cursor.radius, this.cursor.angle, this.cursor.rotationAngle, this.cursor.strenght);
			this.currentRanger.GetComponent<BoxCollider2D>().enabled = false;
		}
		base.Invoke("SpawnRanger", 1f);
		this.cursor.Show(false);
	}

	// Token: 0x040020E8 RID: 8424
	public GameObject[] rangerPrefabs;

	// Token: 0x040020E9 RID: 8425
	public EffectorExplode currentRanger;

	// Token: 0x040020EA RID: 8426
	public RadialFillCursor cursor;

	// Token: 0x040020EB RID: 8427
	public Transform spawnPosition;

	// Token: 0x040020EC RID: 8428
	private int lastRangerIndex;
}
