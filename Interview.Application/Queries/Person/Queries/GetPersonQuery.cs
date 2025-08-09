using Interview.Application.DTOs.Person;
using MediatR;

namespace Interview.Application.Queries.Person;

/// <summary>
/// ერთი ფიზიკური პირის მიღების Query ID-ით
/// გამოიყენება CQRS პატერნში ფიზიკური პირის მიღებისთვის
/// </summary>
public class GetPersonQuery : IRequest<PersonDto>
{
    /// <summary>
    /// ფიზიკური პირის ID
    /// </summary>
    public int Id { get; set; }
}

