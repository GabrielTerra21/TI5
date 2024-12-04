using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelationRenderFeature : ScriptableRendererFeature
{
    #region Campos

    [SerializeField] private PixelationSettings settings;
    private PixelationRenderPass pixelationRenderPass;
    [SerializeField] private Shader pixelationShader;
    private Material material;

    #endregion

    #region Metodos Herdados

    // Chamado uma vez ao carregar, ao ser habilitado ou desabilitado e ao alterar informações no inspetor
    public override void Create(){
        if(pixelationShader == null) return;

        material = CoreUtils.CreateEngineMaterial(pixelationShader);
        pixelationRenderPass = new PixelationRenderPass(material);

        pixelationRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    // Chamado todo frame, para cada camera
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData){
        if(renderingData.cameraData.cameraType == CameraType.Game){
            renderer.EnqueuePass(pixelationRenderPass);
        }
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        if(renderingData.cameraData.cameraType == CameraType.Game){
            pixelationRenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
            pixelationRenderPass.SetTarget(renderer.cameraColorTargetHandle, settings);
        }
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(material);
    }

    #endregion    
}

[Serializable] public class PixelationSettings{
    [Range(1, 256)] public int pixelRange;
}
