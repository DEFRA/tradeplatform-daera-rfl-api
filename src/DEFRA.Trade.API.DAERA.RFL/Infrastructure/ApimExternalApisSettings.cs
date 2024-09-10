// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.Infrastructure;

public class ApimExternalApisSettings
{
    public const string OptionsName = "DaeraRflApiSettings";

    public string BaseUrl { get; set; }

    public string DaeraRflApiPathV1 { get; set; }

    public string DaeraRflApiUrlV1 => BaseUrl + DaeraRflApiPathV1;

    public string TestApiUri { get; set; } // Added it to test multi region API failover
}
