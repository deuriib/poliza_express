namespace PolizaExpress.SharedKernel.Helpers;

public static class PolizaHelper
{
    public static string GenerarNumeroPoliza()
    {
        string formato = "{0}-{1}";
        string fechaActual = DateTime.Now.ToString("yyyyMMdd");
        string numeroAleatorio = GenerarNumeroAleatorio();

        return string.Format(formato, fechaActual, numeroAleatorio);
    }

    private static string GenerarNumeroAleatorio()
    {
        Random random = new Random();
        int numero = random.Next(1000, 999999);

        return numero.ToString();
    }
}