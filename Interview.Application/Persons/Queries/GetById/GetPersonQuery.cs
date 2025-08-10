using Interview.Application.Persons.Queries.GetById;
using MediatR;

namespace Interview.Application.Persons.Queries.GetById;

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

