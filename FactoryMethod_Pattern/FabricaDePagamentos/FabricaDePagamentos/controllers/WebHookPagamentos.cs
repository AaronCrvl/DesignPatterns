using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/webhooks")]
public class WebHookPagamentos : ControllerBase
{
    private PagamentoFactory _factory;
    private ILogger<WebHookPagamentos> _logger;

    public WebHookPagamentos(PagamentoFactory factory, ILogger<WebHookPagamentos> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    [HttpPost("receberPagamento")]
    public IActionResult ReceberPagamento([FromBody] object objetoPagamento)
    {
        try
        {
            var res = _factory.ConcretizarPagamento(objetoPagamento);
            if (res == 200)
                return Ok("Pagamento Recebido.");

            // lógica de retry e novos enfileiramentos + consulta na plataforma externa...
            return BadRequest("Errp ap receber pagamento.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao receber pagamento.");
            return BadRequest();
        }
    }
}