// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Validation;

public sealed class RflUpdateRequestValidator : AbstractValidator<RflUpdateRequest>
{
    public RflUpdateRequestValidator()
    {
        RuleFor(m => m.ExchangedDocument)
            .NotNull()
            .ChildRules(ExchangeDocumentChildRules);
        RuleFor(m => m.OperatorResponsibleForConsignment)
            .NotNull()
            .ForEach(static x => x
                .NotNull()
                .ChildRules(OperatorResponsibleForConsignmentChildRules));
    }

    private static void ContentIsNullOr<T>(InlineValidator<T> validator, params string[] values)
        where T : TextContainer
    {
        ValueIsNullOr(validator, x => x.Content, values, RegexOptions.IgnoreCase);
    }

    private static void ContentNotNull<T>(InlineValidator<T> validator)
            where T : TextContainer
    {
        validator.RuleFor(m => m.Content)
            .NotNull();
    }

    private static void EnglishText(InlineValidator<TextType> validator)
    {
        ContentNotNull(validator);
        ValueIsNullOr(validator, x => x.LanguageId, ["en", "EN"]);
    }

    private static void ExchangedDocumentIdChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        validator.RuleFor(m => m.SchemeId)
            .NotNull();

        SchemeIdIsNullOr(validator, "DAERA.CHIP.RFL.MessageId");
    }

    private static void ExchangedDocumentIssueDateTimeChildRules(InlineValidator<FormattedDateTime> validator)
    {
        ContentNotNull(validator);
        validator.RuleFor(m => m.Format)
            .NotNull();
        ValueIsNullOr(validator, x => x.Format, ["205"]);
    }

    private static void ExchangedDocumentIssuerChildRules(InlineValidator<Issuer> validator)
    {
        validator.RuleFor(m => m.Id)
            .ChildRules(ExchangedDocumentIssuerIdChildRules);
        validator.RuleFor(m => m.Name)
            .NotNull()
            .ChildRules(EnglishText);
        validator.RuleFor(m => m.RoleCode)
            .ChildRules(ExchangedDocumentIssuerRoleCodeChildRules);
    }

    private static void ExchangedDocumentIssuerIdChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        validator.RuleFor(m => m.SchemeId)
            .NotNull();

        SchemeIdIsNullOr(validator, "CA", "Defra.Customer", "FBO", "RMS");
    }

    private static void ExchangedDocumentIssuerRoleCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        ContentIsNullOr(validator, "ZA", "AG", "AM", "DGP", "FZ", "GA", "WS");
        ListAgencyIdIsNullOr(validator, "6");
    }

    private static void ExchangedDocumentTypeCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        validator.RuleFor(m => m.ListAgencyId)
            .NotNull();

        ContentIsNullOr(validator, "1004");
        ListAgencyIdIsNullOr(validator, "6");
    }

    private static void ExchangeDocumentChildRules(InlineValidator<RflExchangeDocument> validator)
    {
        validator.RuleFor(m => m.Id)
            .NotNull()
            .ChildRules(ExchangedDocumentIdChildRules);
        validator.RuleFor(m => m.TypeCode)
            .NotNull()
            .ChildRules(ExchangedDocumentTypeCodeChildRules);
        validator.RuleFor(m => m.IssueDateTime)
            .NotNull()
            .ChildRules(ExchangedDocumentIssueDateTimeChildRules);
        validator.RuleFor(m => m.Issuer)
            .NotNull()
            .ChildRules(ExchangedDocumentIssuerChildRules);
    }

    private static void ListAgencyIdIsNullOr(InlineValidator<IdType> validator, params string[] values)
    {
        ValueIsNullOr(validator, x => x.ListAgencyId, values, RegexOptions.IgnoreCase);
    }

    private static void OperatorResponsibleForConsignmentChildRules(InlineValidator<RflOperatorUpdate> validator)
    {
        validator.RuleFor(m => m.Id)
            .NotNull()
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.Name)
            .NotNull()
            .ChildRules(EnglishText);
        validator.RuleFor(m => m.ClassificationCode)
            .ChildRules(OperatorResponsibleForConsignmentClassificationCodeChildRules);
        validator.RuleFor(m => m.PostalAddress)
            .NotNull()
            .ChildRules(OperatorResponsibleForConsignmentPostalAddressChildRules);
    }

    private static void OperatorResponsibleForConsignmentClassificationCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        ContentIsNullOr(validator, "C");
    }

    private static void OperatorResponsibleForConsignmentPostalAddressChildRules(InlineValidator<RflOperatorAddressUpdate> validator)
    {
        validator.RuleFor(m => m.Postcode)
            .NotNull()
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.LineOne)
            .NotNull()
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.LineTwo)
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.LineThree)
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.LineFour)
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.LineFive)
            .ChildRules(ContentNotNull);
        validator.RuleFor(m => m.CityName)
            .ChildRules(ContentNotNull)
            .NotNull();
        validator.RuleFor(m => m.CountryCode)
            .NotNull()
            .ChildRules(OperatorResponsibleForConsignmentPostalAddressCountryCodeChildRules);
        validator.RuleFor(m => m.CountryName)
            .ChildRules(EnglishText);
        validator.RuleFor(m => m.CountrySubDivisionCode)
            .ChildRules(OperatorResponsibleForConsignmentPostalAddressCountrySubDivisionCodeChildRules);
        validator.RuleFor(m => m.CountrySubDivisionName)
            .ChildRules(EnglishText);
        validator.RuleFor(m => m.TypeCode)
            .ChildRules(OperatorResponsibleForConsignmentPostalAddressCountryTypeCodeChildRules);
    }

    private static void OperatorResponsibleForConsignmentPostalAddressCountryCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        ContentIsNullOr(validator,
            "AD", "AE", "AF", "AG", "AI", "AL", "AM", "AO", "AQ", "AR", "AS", "AT", "AU", "AW", "AX", "AZ",
            "BA", "BB", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BL", "BM", "BN", "BO", "BQ", "BR", "BS", "BT", "BV", "BW", "BY", "BZ",
            "CA", "CC", "CD", "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN", "CO", "CR", "CU", "CV", "CW", "CX", "CY", "CZ",
            "DE", "DJ", "DK", "DM", "DO", "DZ",
            "EC", "EE", "EG", "EH", "ER", "ES", "ET",
            "FI", "FJ", "FK", "FM", "FO", "FR",
            "GA", "GB", "GD", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GP", "GQ", "GR", "GS", "GT", "GU", "GW", "GY",
            "HK", "HM", "HN", "HR", "HT", "HU",
            "ID", "IE", "IL", "IM", "IN", "IO", "IQ", "IR", "IS", "IT",
            "JE", "JM", "JO", "JP",
            "KE", "KG", "KH", "KI", "KM", "KN", "KP", "KR", "KW", "KY", "KZ",
            "LA", "LB", "LC", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY",
            "MA", "MC", "MD", "ME", "MF", "MG", "MH", "MK", "ML", "MM", "MN", "MO", "MP", "MQ", "MR", "MS", "MT", "MU", "MV", "MW", "MX", "MY", "MZ",
            "NA", "NC", "NE", "NF", "NG", "NI", "NL", "NO", "NP", "NR", "NU", "NZ",
            "OM",
            "PA", "PE", "PF", "PG", "PH", "PK", "PL", "PM", "PN", "PR", "PS", "PT", "PW", "PY",
            "QA",
            "RE", "RO", "RS", "RU", "RW",
            "SA", "SB", "SC", "SD", "SE", "SG", "SH", "SI", "SJ", "SK", "SL", "SM", "SN", "SO", "SR", "SS", "ST", "SV", "SX", "SY", "SZ",
            "TC", "TD", "TF", "TG", "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TR", "TT", "TV", "TW", "TZ",
            "UA", "UG", "UM", "US", "UY", "UZ",
            "VA", "VC", "VE", "VG", "VI", "VN", "VU",
            "WF", "WS",
            "YE", "YT",
            "ZA", "ZM", "ZW",
            "XI"
        );
    }

    private static void OperatorResponsibleForConsignmentPostalAddressCountrySubDivisionCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        ValueIsNullOr(validator, x => x.SchemeAgencyId, ["5"]);
    }

    private static void OperatorResponsibleForConsignmentPostalAddressCountryTypeCodeChildRules(InlineValidator<IdType> validator)
    {
        ContentNotNull(validator);
        validator.RuleFor(m => m.ListAgencyId)
            .NotNull();

        ContentIsNullOr(validator, "1", "2", "3", "4", "5", "6", "7", "8");
        ListAgencyIdIsNullOr(validator, "6");
    }

    private static void SchemeIdIsNullOr(InlineValidator<IdType> validator, params string[] values)
    {
        ValueIsNullOr(validator, x => x.SchemeId, values, RegexOptions.IgnoreCase);
    }

    private static void ValueIsNullOr<T>(InlineValidator<T> validator, Expression<Func<T, string>> property, string[] values, RegexOptions options = 0)
    {
        var getter = property.Compile();
        switch (values.Length)
        {
            case 0:
                break;

            case 1:
                string value = values[0];
                validator.When(m => getter(m) is not null, () =>
                {
                    validator.RuleFor(property)
                        .Equal(value);
                });
                break;

            default:
                var regex = new Regex($@"^(?:{string.Join('|', values.Select(Regex.Escape))})$", options | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
                validator.When(m => getter(m) is not null, () =>
                {
                    validator.RuleFor(property)
                        .Matches(regex)
                        .WithMessage($"'{{PropertyName}}' must be equal to one of '{string.Join("', '", values)}'.");
                });
                break;
        }
    }
}
