using Interview.Application.Persons.Queries.GetDetail;
using MediatR;

namespace Interview.Application.Persons.Queries.GetDetail;

/// <summary>
/// ფიზიკური პირის სრული ინფორმაციის მიღების Query ID-ით
/// მოიცავს ტელეფონის ნომრებს და კავშირებს
/// გამოიყენება CQRS პატერნში ფიზიკური პირის დეტალების მიღებისთვის
/// </summary>
public class GetPersonDetailQuery : IRequest<PersonDetailDto>
{
    /// <summary>
    /// ფიზიკური პირის ID
    /// </summary>
    public int Id { get; set; }
}

