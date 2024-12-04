using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelationRenderPass : ScriptableRenderPass{

    #region Campos

    ProfilingSampler profilingSampler = new ProfilingSampler("ColorBlit");
    private Material material;
    private RTHandle cameraTexture;
    private PixelationSettings settings;

    #endregion

    #region Metodos Herdados

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData){
        ConfigureTarget(cameraTexture);
    }

    // Executado todo frame, contém funcionalidade de renderização customizada
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData){
        var cameraData = renderingData.cameraData;

        if(cameraData.camera.cameraType != CameraType.Game) return;
        if(material == null) return;

        CommandBuffer cmd = CommandBufferPool.Get();
        using ( new ProfilingScope(cmd, profilingSampler)){
            material.SetFloat("_PixelRange", settings.pixelRange);
            Blitter.BlitCameraTexture(cmd, cameraTexture, cameraTexture, material, 0);
        }
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();

        CommandBufferPool.Release(cmd);
    }

    #endregion

    #region Metodos

    public void SetTarget(RTHandle colorHandle, PixelationSettings settings){
        cameraTexture = colorHandle;
        this.settings = settings;
    }

    // Construtor
    public PixelationRenderPass(Material material){
        this.material = material;
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    #endregion
}
