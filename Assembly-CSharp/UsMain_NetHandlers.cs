using System;
using System.Collections.Generic;
using usmooth;

// Token: 0x02000490 RID: 1168
public class UsMain_NetHandlers
{
	// Token: 0x06001587 RID: 5511 RVA: 0x00085130 File Offset: 0x00083530
	public UsMain_NetHandlers(UsCmdParsing exec)
	{
		exec.RegisterHandler(eNetCmd.CL_Handshake, new UsCmdHandler(this.NetHandle_Handshake));
		exec.RegisterHandler(eNetCmd.CL_KeepAlive, new UsCmdHandler(this.NetHandle_KeepAlive));
		exec.RegisterHandler(eNetCmd.CL_ExecCommand, new UsCmdHandler(this.NetHandle_ExecCommand));
		exec.RegisterHandler(eNetCmd.CL_RequestFrameData, new UsCmdHandler(this.NetHandle_RequestFrameData));
		exec.RegisterHandler(eNetCmd.CL_FrameV2_RequestMeshes, new UsCmdHandler(this.NetHandle_FrameV2_RequestMeshes));
		exec.RegisterHandler(eNetCmd.CL_FrameV2_RequestNames, new UsCmdHandler(this.NetHandle_FrameV2_RequestNames));
		exec.RegisterHandler(eNetCmd.CL_QuerySwitches, new UsCmdHandler(this.NetHandle_QuerySwitches));
		exec.RegisterHandler(eNetCmd.CL_QuerySliders, new UsCmdHandler(this.NetHandle_QuerySliders));
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x00085204 File Offset: 0x00083604
	private bool NetHandle_Handshake(eNetCmd cmd, UsCmd c)
	{
		if (!string.IsNullOrEmpty(LogService.LastLogFile))
		{
		}
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_HandshakeResponse);
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x00085240 File Offset: 0x00083640
	private bool NetHandle_KeepAlive(eNetCmd cmd, UsCmd c)
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_KeepAliveResponse);
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x0008526C File Offset: 0x0008366C
	private bool NetHandle_ExecCommand(eNetCmd cmd, UsCmd c)
	{
		string fullcmd = c.ReadString();
		bool flag = UsvConsole.Instance.ExecuteCommand(fullcmd);
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_ExecCommandResponse);
		usCmd.WriteInt32((!flag) ? 0 : 1);
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000852BC File Offset: 0x000836BC
	private bool NetHandle_RequestFrameData(eNetCmd cmd, UsCmd c)
	{
		if (DataCollector.Instance == null)
		{
			return true;
		}
		FrameData frameData = DataCollector.Instance.CollectFrameData();
		UsNet.Instance.SendCommand(frameData.CreatePacket());
		UsNet.Instance.SendCommand(DataCollector.Instance.CreateMaterialCmd());
		UsNet.Instance.SendCommand(DataCollector.Instance.CreateTextureCmd());
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_FrameDataEnd);
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x00085338 File Offset: 0x00083738
	private bool NetHandle_FrameV2_RequestMeshes(eNetCmd cmd, UsCmd c)
	{
		if (DataCollector.Instance != null)
		{
			List<int> objList = UsCmdUtil.ReadIntList(c);
			foreach (List<int> list in UsGeneric.Slice<int>(objList, this.SLICE_COUNT))
			{
				UsCmd usCmd = new UsCmd();
				usCmd.WriteNetCmd(eNetCmd.SV_FrameDataV2_Meshes);
				usCmd.WriteInt32(list.Count);
				foreach (int instID in list)
				{
					DataCollector.Instance.MeshTable.WriteMesh(instID, usCmd);
				}
				UsNet.Instance.SendCommand(usCmd);
			}
		}
		return true;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x00085420 File Offset: 0x00083820
	private bool NetHandle_FrameV2_RequestNames(eNetCmd cmd, UsCmd c)
	{
		if (DataCollector.Instance != null)
		{
			List<int> objList = UsCmdUtil.ReadIntList(c);
			foreach (List<int> list in UsGeneric.Slice<int>(objList, this.SLICE_COUNT))
			{
				UsCmd usCmd = new UsCmd();
				usCmd.WriteNetCmd(eNetCmd.SV_FrameDataV2_Names);
				usCmd.WriteInt32(list.Count);
				foreach (int instID in list)
				{
					DataCollector.Instance.WriteName(instID, usCmd);
				}
				UsNet.Instance.SendCommand(usCmd);
			}
		}
		return true;
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00085500 File Offset: 0x00083900
	private bool NetHandle_QuerySwitches(eNetCmd cmd, UsCmd c)
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_QuerySwitchesResponse);
		usCmd.WriteInt32(GameInterface.ObjectNames.Count);
		foreach (KeyValuePair<string, string> keyValuePair in GameInterface.ObjectNames)
		{
			usCmd.WriteString(keyValuePair.Key);
			usCmd.WriteString(keyValuePair.Value);
			usCmd.WriteInt16(1);
		}
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000855A4 File Offset: 0x000839A4
	private bool NetHandle_QuerySliders(eNetCmd cmd, UsCmd c)
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_QuerySlidersResponse);
		usCmd.WriteInt32(GameInterface.VisiblePercentages.Count);
		foreach (KeyValuePair<string, double> keyValuePair in GameInterface.VisiblePercentages)
		{
			usCmd.WriteString(keyValuePair.Key);
			usCmd.WriteFloat(0f);
			usCmd.WriteFloat(100f);
			usCmd.WriteFloat((float)keyValuePair.Value);
		}
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x0400189D RID: 6301
	public static UsMain_NetHandlers Instance;

	// Token: 0x0400189E RID: 6302
	private int SLICE_COUNT = 50;
}
