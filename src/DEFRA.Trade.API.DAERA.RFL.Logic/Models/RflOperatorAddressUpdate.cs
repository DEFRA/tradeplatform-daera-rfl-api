// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class RflOperatorAddressUpdate
{
    [JsonPropertyName("CityName")]
    public TextType CityName { get; set; }

    [JsonPropertyName("CountryCode")]
    public IdType CountryCode { get; set; }

    [JsonPropertyName("CountryName")]
    public TextType CountryName { get; set; }

    [JsonPropertyName("CountrySubDivisionCode")]
    public IdType CountrySubDivisionCode { get; set; }

    [JsonPropertyName("CountrySubDivisionName")]
    public TextType CountrySubDivisionName { get; set; }

    [JsonPropertyName("LineOne")]
    public TextType LineOne { get; set; }

    [JsonPropertyName("LineTwo")]
    public TextType LineTwo { get; set; }

    [JsonPropertyName("LineThree")]
    public TextType LineThree { get; set; }

    [JsonPropertyName("LineFour")]
    public TextType LineFour { get; set; }

    [JsonPropertyName("LineFive")]
    public TextType LineFive { get; set; }

    [JsonPropertyName("Postcode")]
    public TextType Postcode { get; set; }

    [JsonPropertyName("TypeCode")]
    public IdType TypeCode { get; set; }
}