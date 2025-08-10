using Interview.Application.Persons.Queries.GetAll;
using MediatR;

namespace Interview.Application.Persons.Queries.GetAll;

/// <summary>
/// ყველა ფიზიკური პირის სიის მიღების Query
/// მოიცავს paging ფუნქციონალს
/// გამოიყენება CQRS პატერნში ფიზიკური პირების სიის მიღებისთვის
/// </summary>
public class GetAllPersonsQuery : IRequest<IEnumerable<PersonListDto>>
{
    /// <summary>
    /// გვერდის ნომერი (1-დან იწყება)
    /// </summary>
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// გვერდზე ჩანაწერების რაოდენობა
    /// </summary>
    public int PageSize { get; set; } = 10;
}

