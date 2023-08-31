using System.Net;

namespace ExcelData.Domain.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public string TitleMessage { get; protected set; } = string.Empty;
}

