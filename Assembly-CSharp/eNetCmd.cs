using System;

// Token: 0x02000488 RID: 1160
public enum eNetCmd
{
	// Token: 0x04001865 RID: 6245
	None,
	// Token: 0x04001866 RID: 6246
	CL_CmdBegin = 1000,
	// Token: 0x04001867 RID: 6247
	CL_Handshake,
	// Token: 0x04001868 RID: 6248
	CL_KeepAlive,
	// Token: 0x04001869 RID: 6249
	CL_ExecCommand,
	// Token: 0x0400186A RID: 6250
	CL_RequestFrameData,
	// Token: 0x0400186B RID: 6251
	CL_FrameV2_RequestMeshes,
	// Token: 0x0400186C RID: 6252
	CL_FrameV2_RequestNames,
	// Token: 0x0400186D RID: 6253
	CL_QuerySwitches,
	// Token: 0x0400186E RID: 6254
	CL_QuerySliders,
	// Token: 0x0400186F RID: 6255
	CL_RequestStackSummary,
	// Token: 0x04001870 RID: 6256
	CL_StartAnalysePixels,
	// Token: 0x04001871 RID: 6257
	CL_RequestStackData,
	// Token: 0x04001872 RID: 6258
	CL_CmdEnd,
	// Token: 0x04001873 RID: 6259
	SV_CmdBegin = 2000,
	// Token: 0x04001874 RID: 6260
	SV_HandshakeResponse,
	// Token: 0x04001875 RID: 6261
	SV_KeepAliveResponse,
	// Token: 0x04001876 RID: 6262
	SV_ExecCommandResponse,
	// Token: 0x04001877 RID: 6263
	SV_FrameDataV2,
	// Token: 0x04001878 RID: 6264
	SV_FrameDataV2_Meshes,
	// Token: 0x04001879 RID: 6265
	SV_FrameDataV2_Names,
	// Token: 0x0400187A RID: 6266
	SV_FrameData_Material,
	// Token: 0x0400187B RID: 6267
	SV_FrameData_Texture,
	// Token: 0x0400187C RID: 6268
	SV_FrameDataEnd,
	// Token: 0x0400187D RID: 6269
	SV_App_Logging,
	// Token: 0x0400187E RID: 6270
	SV_QuerySwitchesResponse,
	// Token: 0x0400187F RID: 6271
	SV_QuerySlidersResponse,
	// Token: 0x04001880 RID: 6272
	SV_QueryStacksResponse,
	// Token: 0x04001881 RID: 6273
	SV_VarTracerJsonParameter,
	// Token: 0x04001882 RID: 6274
	SV_StressTestNames,
	// Token: 0x04001883 RID: 6275
	SV_StressTestResult,
	// Token: 0x04001884 RID: 6276
	SV_StartAnalysePixels,
	// Token: 0x04001885 RID: 6277
	SV_CmdEnd,
	// Token: 0x04001886 RID: 6278
	SV_SendLuaProfilerMsg,
	// Token: 0x04001887 RID: 6279
	SV_StartLuaProfilerMsg
}
