using System.Text;
using NFSe;

public interface iNFSeBuilder
{
    string Cabecalho();

    string DPS(NFSeNacional nota);

    string Prestador(NFSeNacional nota);

    string Tomador(NFSeNacional nota);

    string Servico(NFSeNacional nota);

    string IBSCBS(NFSeNacional nota);

    string Deducao();

    string DocumentosReferenciados();    
}