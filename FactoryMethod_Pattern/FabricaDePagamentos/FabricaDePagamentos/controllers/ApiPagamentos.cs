using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1")]
public class ApiPagamentos : ControllerBase
{
    private PagamentoFactory _factory;
    private ILogger<ApiPagamentos> _logger;

    public ApiPagamentos(PagamentoFactory factory, ILogger<ApiPagamentos> logger)
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
            switch (res)
            {
                case 200:
                    return new ObjectResult(new
                    {
                        status = 200,
                        message = "Pagamento efetivado na plataforma."
                    });

                case 500:
                    return new ObjectResult(new
                    {
                        status = 500,
                        message = "Falha ao efetivar pagamento na plataforma."
                    });

                default:
                    return new ObjectResult(new
                    {
                        status = 0,
                        message = "Pagamento não efetivado, contate a plataforma."
                    }); ;
            }
        }
        catch (Exception e)
        {
            throw new InvalidDataException(e.Message);
            // log...
        }
    }
}