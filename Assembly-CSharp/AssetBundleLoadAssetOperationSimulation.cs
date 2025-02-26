using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200112A RID: 4394
public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
{
	// Token: 0x0600916A RID: 37226 RVA: 0x003C7515 File Offset: 0x003C5915
	public AssetBundleLoadAssetOperationSimulation(UnityEngine.Object simulatedObject)
	{
		this.m_SimulatedObjects = new UnityEngine.Object[]
		{
			simulatedObject
		};
	}

	// Token: 0x0600916B RID: 37227 RVA: 0x003C752D File Offset: 0x003C592D
	public AssetBundleLoadAssetOperationSimulation(UnityEngine.Object[] simulatedObjects)
	{
		this.m_SimulatedObjects = simulatedObjects;
	}

	// Token: 0x0600916C RID: 37228 RVA: 0x003C753C File Offset: 0x003C593C
	public override bool IsEmpty()
	{
		return this.m_SimulatedObjects == null || this.m_SimulatedObjects.Length == 0 || this.m_SimulatedObjects[0] == null;
	}

	// Token: 0x0600916D RID: 37229 RVA: 0x003C756A File Offset: 0x003C596A
	public override T GetAsset<T>()
	{
		return this.m_SimulatedObjects[0] as T;
	}

	// Token: 0x0600916E RID: 37230 RVA: 0x003C757E File Offset: 0x003C597E
	public override T[] GetAllAssets<T>()
	{
		return this.m_SimulatedObjects.OfType<T>().ToArray<T>();
	}

	// Token: 0x0600916F RID: 37231 RVA: 0x003C7590 File Offset: 0x003C5990
	public override bool Update()
	{
		return false;
	}

	// Token: 0x06009170 RID: 37232 RVA: 0x003C7593 File Offset: 0x003C5993
	public override bool IsDone()
	{
		return true;
	}

	// Token: 0x040075D5 RID: 30165
	private UnityEngine.Object[] m_SimulatedObjects;
}
