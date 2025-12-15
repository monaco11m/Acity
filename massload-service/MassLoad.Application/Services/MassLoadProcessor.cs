using MassLoad.Application.Dtos;
using MassLoad.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class MassLoadProcessor : IMassLoadProcessor
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IConfiguration _config;
    private readonly IRabbitPublisher _publisher;

    public MassLoadProcessor(
        IHttpClientFactory httpFactory,
        IConfiguration config,
        IRabbitPublisher publisher
    )
    {
        _httpFactory = httpFactory;
        _config = config;
        _publisher = publisher;
    }

    public async Task ProcessAsync(CargaMessage msg)
    {
        var http = _httpFactory.CreateClient();

        var fileUrl = $"{_config["SeaweedFS:VolumeUrl"]}/{msg.FileId}";
        var fileBytes = await http.GetByteArrayAsync(fileUrl);

        await Task.Delay(2000);

        var callbackUrl = $"{_config["ControlService:BaseUrl"]}/api/cargaarchivo/actualizar";

        await http.PostAsJsonAsync(callbackUrl, new
        {
            id = msg.CargaId,
            estado = 2
        });

        await _publisher.PublishCargaFinalizadaAsync(new
        {
            CargaId = msg.CargaId,
            Usuario = msg.Usuario,
            FileId = msg.FileId
        });
    }
}
