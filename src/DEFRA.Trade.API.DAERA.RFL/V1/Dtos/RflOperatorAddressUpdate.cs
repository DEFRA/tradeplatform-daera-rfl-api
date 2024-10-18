// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// The address details of the operator being updated
/// </summary>
public sealed class RflOperatorAddressUpdate
{
    /// <summary>
    /// The name of the city
    /// </summary>
    public TextType CityName { get; set; }

    /// <summary>
    /// The 2 digit ISO country code
    /// </summary>
    public IdType CountryCode { get; set; }

    /// <summary>
    /// The name of the country
    /// </summary>
    public TextType CountryName { get; set; }

    /// <summary>
    /// The ISO country sub-division code
    /// </summary>
    public IdType CountrySubDivisionCode { get; set; }

    /// <summary>
    /// The name of the country sub-division
    /// </summary>
    public TextType CountrySubDivisionName { get; set; }

    /// <summary>
    /// The fifth line of the address
    /// </summary>
    public TextType LineFive { get; set; }

    /// <summary>
    /// The fourth line of the address
    /// </summary>
    public TextType LineFour { get; set; }

    /// <summary>
    /// The first line of the address
    /// </summary>
    public TextType LineOne { get; set; }

    /// <summary>
    /// The third line of the address
    /// </summary>
    public TextType LineThree { get; set; }

    /// <summary>
    /// The second line of the address
    /// </summary>
    public TextType LineTwo { get; set; }

    /// <summary>
    /// The postcode of the address
    /// </summary>
    public TextType Postcode { get; set; }

    /// <summary>
    /// The type of the address
    /// </summary>
    public IdType TypeCode { get; set; }
}
