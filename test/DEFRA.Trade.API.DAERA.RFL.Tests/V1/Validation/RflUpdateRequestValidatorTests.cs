// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.V1.Dtos;
using DEFRA.Trade.API.DAERA.RFL.V1.Validation;
using FluentValidation.TestHelper;

namespace DEFRA.Trade.API.DAERA.RFL.Tests.V1.Validation;

public class RflUpdateRequestValidatorTests
{
    private readonly RflUpdateRequestValidator _sut = new();

    public static TheoryData<TestCase> Validate_CorrectlyChecks_TheoryData()
    {
        return
        [
            new(
                new(),
                [
                    new("ExchangedDocument", "'Exchanged Document' must not be empty."),
                    new("OperatorResponsibleForConsignment", "'Operator Responsible For Consignment' must not be empty."),
                ]),
            new(
                new()
                {
                    ExchangedDocument = new(),
                    OperatorResponsibleForConsignment = []
                },
                [
                    new("ExchangedDocument.Id", "'Id' must not be empty."),
                    new("ExchangedDocument.TypeCode", "'Type Code' must not be empty."),
                    new("ExchangedDocument.IssueDateTime", "'Issue Date Time' must not be empty."),
                    new("ExchangedDocument.Issuer", "'Issuer' must not be empty."),
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new(),
                        TypeCode = new(),
                        IssueDateTime = new(),
                        Issuer = new()
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        null
                    }
                },
                [
                    new("ExchangedDocument.Id.Content", "'Content' must not be empty."),
                    new("ExchangedDocument.Id.SchemeId", "'Scheme Id' must not be empty."),
                    new("ExchangedDocument.TypeCode.Content", "'Content' must not be empty."),
                    new("ExchangedDocument.TypeCode.ListAgencyId", "'List Agency Id' must not be empty."),
                    new("ExchangedDocument.IssueDateTime.Content", "'Content' must not be empty."),
                    new("ExchangedDocument.IssueDateTime.Format", "'Format' must not be empty."),
                    new("ExchangedDocument.Issuer.Name", "'Name' must not be empty."),
                    new("OperatorResponsibleForConsignment[0]", "'Operator Responsible For Consignment' must not be empty."),
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new()
                        {
                            Content = "abc",
                            ListAgencyId = "abc",
                            SchemeAgencyId = "abc",
                            SchemeId = "abc",
                            SchemeName = "abc"
                        },
                        TypeCode = new()
                        {
                            Content = "abc",
                            ListAgencyId = "abc",
                            SchemeAgencyId = "abc",
                            SchemeId = "abc",
                            SchemeName = "abc"
                        },
                        IssueDateTime = new()
                        {
                            Content = "abc",
                            Format = "abc"
                        },
                        Issuer = new()
                        {
                            Id = new(),
                            Name = new(),
                            RoleCode = new()
                        }
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        new()
                    }
                },
                [
                    new("ExchangedDocument.Id.SchemeId", "'Scheme Id' must be equal to 'DAERA.CHIP.RFL.MessageId'."),
                    new("ExchangedDocument.TypeCode.Content", "'Content' must be equal to '1004'."),
                    new("ExchangedDocument.TypeCode.ListAgencyId", "'List Agency Id' must be equal to '6'."),
                    new("ExchangedDocument.IssueDateTime.Format", "'Format' must be equal to '205'."),
                    new("ExchangedDocument.Issuer.Id.Content", "'Content' must not be empty."),
                    new("ExchangedDocument.Issuer.Id.SchemeId", "'Scheme Id' must not be empty."),
                    new("ExchangedDocument.Issuer.Name.Content", "'Content' must not be empty."),
                    new("ExchangedDocument.Issuer.RoleCode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].Id", "'Id' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].Name", "'Name' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress", "'Postal Address' must not be empty."),
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new()
                        {
                            Content = "abc",
                            SchemeId = "DAERA.CHIP.RFL.MessageId",
                        },
                        TypeCode = new()
                        {
                            Content = "1004",
                            ListAgencyId = "6"
                        },
                        IssueDateTime = new()
                        {
                            Content = "abc",
                            Format = "205"
                        },
                        Issuer = new()
                        {
                            Id = new()
                            {
                                Content = "abc",
                                ListAgencyId = "abc",
                                SchemeAgencyId = "abc",
                                SchemeId = "abc",
                                SchemeName = "abc"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "abc"
                            },
                            RoleCode = new()
                            {
                                Content = "abc",
                                ListAgencyId = "abc",
                                SchemeAgencyId = "abc",
                                SchemeId = "abc",
                                SchemeName = "abc"
                            }
                        }
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        new()
                        {
                            Id = new(),
                            ClassificationCode = new(),
                            Name = new(),
                            PostalAddress = new()
                        }
                    }
                },
                [
                    new("ExchangedDocument.Issuer.Id.SchemeId", "'Scheme Id' must be equal to one of 'CA', 'Defra.Customer', 'FBO', 'RMS'."),
                    new("ExchangedDocument.Issuer.Name.LanguageId", "'Language Id' must be equal to one of 'en', 'EN'."),
                    new("ExchangedDocument.Issuer.RoleCode.Content", "'Content' must be equal to one of 'ZA', 'AG', 'AM', 'DGP', 'FZ', 'GA', 'WS'."),
                    new("ExchangedDocument.Issuer.RoleCode.ListAgencyId", "'List Agency Id' must be equal to '6'."),
                    new("OperatorResponsibleForConsignment[0].Id.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].Name.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].ClassificationCode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.Postcode", "'Postcode' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineOne", "'Line One' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CityName", "'City Name' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountryCode", "'Country Code' must not be empty.")
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new()
                        {
                            Content = "abc",
                            SchemeId = "DAERA.CHIP.RFL.MessageId",
                        },
                        TypeCode = new()
                        {
                            Content = "1004",
                            ListAgencyId = "6"
                        },
                        IssueDateTime = new()
                        {
                            Content = "abc",
                            Format = "205"
                        },
                        Issuer = new()
                        {
                            Id = new()
                            {
                                Content = "abc",
                                SchemeId = "RMS"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "en"
                            },
                            RoleCode = new()
                            {
                                Content = "GA",
                                ListAgencyId = "6"
                            }
                        }
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        new()
                        {
                            Id = new()
                            {
                                Content = "abc",
                                ListAgencyId = "abc",
                                SchemeAgencyId = "abc",
                                SchemeId = "abc",
                                SchemeName = "abc"
                            },
                            ClassificationCode = new()
                            {
                                Content = "abc",
                                ListAgencyId = "abc",
                                SchemeAgencyId = "abc",
                                SchemeId = "abc",
                                SchemeName = "abc"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "abc"
                            },
                            PostalAddress = new()
                            {
                                CityName = new(),
                                CountryCode = new(),
                                CountryName = new(),
                                CountrySubDivisionCode = new(),
                                CountrySubDivisionName = new(),
                                LineFive = new(),
                                LineFour = new(),
                                LineOne = new(),
                                LineThree = new(),
                                LineTwo = new(),
                                Postcode = new(),
                                TypeCode = new()
                            }
                        }
                    }
                },
                [
                    new("OperatorResponsibleForConsignment[0].Name.LanguageId", "'Language Id' must be equal to one of 'en', 'EN'."),
                    new("OperatorResponsibleForConsignment[0].ClassificationCode.Content", "'Content' must be equal to 'C'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.Postcode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountryCode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineOne.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineTwo.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineThree.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineFour.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.LineFive.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CityName.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountryName.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountrySubDivisionCode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountrySubDivisionName.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.TypeCode.Content", "'Content' must not be empty."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.TypeCode.ListAgencyId", "'List Agency Id' must not be empty.")
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new()
                        {
                            Content = "abc",
                            SchemeId = "DAERA.CHIP.RFL.MessageId",
                        },
                        TypeCode = new()
                        {
                            Content = "1004",
                            ListAgencyId = "6"
                        },
                        IssueDateTime = new()
                        {
                            Content = "abc",
                            Format = "205"
                        },
                        Issuer = new()
                        {
                            Id = new()
                            {
                                Content = "abc",
                                SchemeId = "RMS"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "en"
                            },
                            RoleCode = new()
                            {
                                Content = "GA",
                                ListAgencyId = "6"
                            }
                        }
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        new()
                        {
                            Id = new()
                            {
                                Content = "abc"
                            },
                            ClassificationCode = new()
                            {
                                Content = "C"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "en"
                            },
                            PostalAddress = new()
                            {
                                CityName = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                CountryCode = new()
                                {
                                    Content = "abc",
                                    ListAgencyId = "abc",
                                    SchemeAgencyId = "abc",
                                    SchemeId = "abc",
                                    SchemeName = "abc"
                                },
                                CountryName = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                CountrySubDivisionCode = new()
                                {
                                    Content = "abc",
                                    ListAgencyId = "abc",
                                    SchemeAgencyId = "abc",
                                    SchemeId = "abc",
                                    SchemeName = "abc"
                                },
                                CountrySubDivisionName = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                LineFive = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                LineFour = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                LineOne = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                LineThree = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                LineTwo = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                Postcode = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                TypeCode = new()
                                {
                                    Content = "abc",
                                    ListAgencyId = "abc",
                                    SchemeAgencyId = "abc",
                                    SchemeId = "abc",
                                    SchemeName = "abc"
                                }
                            }
                        }
                    }
                },
                [
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountryCode.Content", "'Content' must be equal to one of 'AD', 'AE', 'AF', 'AG', 'AI', 'AL', 'AM', 'AO', 'AQ', 'AR', 'AS', 'AT', 'AU', 'AW', 'AX', 'AZ', 'BA', 'BB', 'BD', 'BE', 'BF', 'BG', 'BH', 'BI', 'BJ', 'BL', 'BM', 'BN', 'BO', 'BQ', 'BR', 'BS', 'BT', 'BV', 'BW', 'BY', 'BZ', 'CA', 'CC', 'CD', 'CF', 'CG', 'CH', 'CI', 'CK', 'CL', 'CM', 'CN', 'CO', 'CR', 'CU', 'CV', 'CW', 'CX', 'CY', 'CZ', 'DE', 'DJ', 'DK', 'DM', 'DO', 'DZ', 'EC', 'EE', 'EG', 'EH', 'ER', 'ES', 'ET', 'FI', 'FJ', 'FK', 'FM', 'FO', 'FR', 'GA', 'GB', 'GD', 'GE', 'GF', 'GG', 'GH', 'GI', 'GL', 'GM', 'GN', 'GP', 'GQ', 'GR', 'GS', 'GT', 'GU', 'GW', 'GY', 'HK', 'HM', 'HN', 'HR', 'HT', 'HU', 'ID', 'IE', 'IL', 'IM', 'IN', 'IO', 'IQ', 'IR', 'IS', 'IT', 'JE', 'JM', 'JO', 'JP', 'KE', 'KG', 'KH', 'KI', 'KM', 'KN', 'KP', 'KR', 'KW', 'KY', 'KZ', 'LA', 'LB', 'LC', 'LI', 'LK', 'LR', 'LS', 'LT', 'LU', 'LV', 'LY', 'MA', 'MC', 'MD', 'ME', 'MF', 'MG', 'MH', 'MK', 'ML', 'MM', 'MN', 'MO', 'MP', 'MQ', 'MR', 'MS', 'MT', 'MU', 'MV', 'MW', 'MX', 'MY', 'MZ', 'NA', 'NC', 'NE', 'NF', 'NG', 'NI', 'NL', 'NO', 'NP', 'NR', 'NU', 'NZ', 'OM', 'PA', 'PE', 'PF', 'PG', 'PH', 'PK', 'PL', 'PM', 'PN', 'PR', 'PS', 'PT', 'PW', 'PY', 'QA', 'RE', 'RO', 'RS', 'RU', 'RW', 'SA', 'SB', 'SC', 'SD', 'SE', 'SG', 'SH', 'SI', 'SJ', 'SK', 'SL', 'SM', 'SN', 'SO', 'SR', 'SS', 'ST', 'SV', 'SX', 'SY', 'SZ', 'TC', 'TD', 'TF', 'TG', 'TH', 'TJ', 'TK', 'TL', 'TM', 'TN', 'TO', 'TR', 'TT', 'TV', 'TW', 'TZ', 'UA', 'UG', 'UM', 'US', 'UY', 'UZ', 'VA', 'VC', 'VE', 'VG', 'VI', 'VN', 'VU', 'WF', 'WS', 'YE', 'YT', 'ZA', 'ZM', 'ZW', 'XI'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountryName.LanguageId", "'Language Id' must be equal to one of 'en', 'EN'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountrySubDivisionCode.SchemeAgencyId", "'Scheme Agency Id' must be equal to '5'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.CountrySubDivisionName.LanguageId", "'Language Id' must be equal to one of 'en', 'EN'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.TypeCode.Content", "'Content' must be equal to one of '1', '2', '3', '4', '5', '6', '7', '8'."),
                    new("OperatorResponsibleForConsignment[0].PostalAddress.TypeCode.ListAgencyId", "'List Agency Id' must be equal to '6'.")
                ]),
            new(
                new()
                {
                    ExchangedDocument = new()
                    {
                        Id = new()
                        {
                            Content = "abc",
                            SchemeId = "DAERA.CHIP.RFL.MessageId",
                        },
                        TypeCode = new()
                        {
                            Content = "1004",
                            ListAgencyId = "6"
                        },
                        IssueDateTime = new()
                        {
                            Content = "abc",
                            Format = "205"
                        },
                        Issuer = new()
                        {
                            Id = new()
                            {
                                Content = "abc",
                                SchemeId = "RMS"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "en"
                            },
                            RoleCode = new()
                            {
                                Content = "GA",
                                ListAgencyId = "6"
                            }
                        }
                    },
                    OperatorResponsibleForConsignment = new RflOperatorUpdate[]
                    {
                        new()
                        {
                            Id = new()
                            {
                                Content = "abc"
                            },
                            ClassificationCode = new()
                            {
                                Content = "C"
                            },
                            Name = new()
                            {
                                Content = "abc",
                                LanguageId = "en"
                            },
                            PostalAddress = new()
                            {
                                CityName = new()
                                {
                                    Content = "abc"
                                },
                                CountryCode = new()
                                {
                                    Content = "XI"
                                },
                                CountryName = new()
                                {
                                    Content = "abc",
                                    LanguageId = "en"
                                },
                                CountrySubDivisionCode = new()
                                {
                                    Content = "abc",
                                    SchemeAgencyId = "5",
                                },
                                CountrySubDivisionName = new()
                                {
                                    Content = "abc",
                                    LanguageId = "en"
                                },
                                LineOne = new()
                                {
                                    Content = "abc"
                                },
                                Postcode = new()
                                {
                                    Content = "abc",
                                    LanguageId = "abc"
                                },
                                TypeCode = new()
                                {
                                    Content = "5",
                                    ListAgencyId = "6"
                                }
                            }
                        }
                    }
                },
                [])
        ];
    }

    [Theory]
    [MemberData(nameof(Validate_CorrectlyChecks_TheoryData))]
    public void Validate_CorrectlyChecks(TestCase testCase)
    {
        // arrange

        // act
        var result = _sut.TestValidate(testCase.Model);

        // assert
        result.Errors.Should().HaveCount(testCase.Errors.Length);
        foreach (var error in testCase.Errors)
        {
            result.ShouldHaveValidationErrorFor(error.Property)
                .WithErrorMessage(error.Message);
        }
    }

    public sealed record ExpectedError(string Property, string Message);

    public sealed record TestCase(RflUpdateRequest Model, ExpectedError[] Errors);
}
