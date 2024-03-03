namespace Wms.Tests.Base;

using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

public enum QueryStrings
{
    withCookies,
}

/// <summary>
/// Basic helper class for building Uri with QueryStrings.
/// In case more generic helper methods are needed, helpful link: https://makolyte.com/csharp-sending-query-strings-with-httpclient/
/// </summary>
public static class UriExtensions
{
    public static string MakeUriWithCookiesQueryString(string uri, bool withCookies)
    {
        var queryString = new Dictionary<string, string>()
        {
            [nameof(QueryStrings.withCookies)] = withCookies.ToString()
        };

        return QueryHelpers.AddQueryString(uri, queryString!);
    }
}
