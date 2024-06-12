namespace Lanpuda.ERP;

public static class ERPDbProperties
{
    public static string DbTablePrefix { get; set; } = "ERP_";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "ERP";
}
