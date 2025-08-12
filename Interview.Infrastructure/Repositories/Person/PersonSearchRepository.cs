using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories.Person;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data;
using Interview.Application.Persons.Queries.AdvancedSearch;
using PersonEntity = Interview.Domain.Entities.Person.Person;

namespace Interview.Infrastructure.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ძებნის Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonSearchRepository : IPersonSearchRepository
{
    private readonly InterviewDbContext _context;

    public PersonSearchRepository(InterviewDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// მარტივი ძებნა ფიზიკური პირებისთვის
    /// </summary>
    public async Task<IEnumerable<PersonEntity>> SearchAsync(string? searchTerm, int? cityId, int? genderId, int page, int pageSize)
    {
        // Raw SQL Query - ყველაზე მარტივი და ეფექტური გადაწყვეტა Value Objects-თან
        var sql = @"
            SELECT p.* 
            FROM [Person].[Persons] p
            WHERE 1=1";
        
        var parameters = new List<object>();
        var parameterIndex = 0;
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            sql += $" AND (LOWER(p.FirstName) LIKE {{{parameterIndex}}} OR LOWER(p.LastName) LIKE {{{parameterIndex}}} OR p.PersonalNumber LIKE {{{parameterIndex}}})";
            parameters.Add($"%{searchTerm.ToLower()}%");
            parameterIndex++;
        }
        
        if (cityId.HasValue)
        {
            sql += $" AND p.CityId = {{{parameterIndex}}}";
            parameters.Add(cityId.Value);
            parameterIndex++;
        }
        
        if (genderId.HasValue)
        {
            sql += $" AND p.GenderId = {{{parameterIndex}}}";
            parameters.Add(genderId.Value);
            parameterIndex++;
        }
        
        sql += " ORDER BY p.FirstName, p.LastName";
        sql += $" OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        
        return await _context.Persons
            .FromSqlRaw(sql, parameters.ToArray())
            .Include(p => p.City)
            .ToListAsync();
    }

    /// <summary>
    /// დეტალური ძებნა ფიზიკური პირებისთვის
    /// </summary>
    public async Task<IEnumerable<PersonEntity>> AdvancedSearchAsync(AdvancedSearchFiltersDto filters)
    {
        // Raw SQL Query დეტალური ძებნისთვის
        var sql = @"
            SELECT p.* 
            FROM [Person].[Persons] p
            WHERE 1=1";
        
        var parameters = new List<object>();
        var parameterIndex = 0;
        
        // ძებნის ტერმინი (სახელი, გვარი, პირადი ნომერი)
        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
        {
            sql += $" AND (LOWER(p.FirstName) LIKE {{{parameterIndex}}} OR LOWER(p.LastName) LIKE {{{parameterIndex}}} OR p.PersonalNumber LIKE {{{parameterIndex}}})";
            parameters.Add($"%{filters.SearchTerm.ToLower()}%");
            parameterIndex++;
        }
        
        // სახელი
        if (!string.IsNullOrWhiteSpace(filters.FirstName))
        {
            sql += $" AND LOWER(p.FirstName) LIKE {{{parameterIndex}}}";
            parameters.Add($"%{filters.FirstName.ToLower()}%");
            parameterIndex++;
        }
        
        // გვარი
        if (!string.IsNullOrWhiteSpace(filters.LastName))
        {
            sql += $" AND LOWER(p.LastName) LIKE {{{parameterIndex}}}";
            parameters.Add($"%{filters.LastName.ToLower()}%");
            parameterIndex++;
        }
        
        // პირადი ნომერი
        if (!string.IsNullOrWhiteSpace(filters.PersonalNumber))
        {
            sql += $" AND p.PersonalNumber LIKE {{{parameterIndex}}}";
            parameters.Add($"%{filters.PersonalNumber}%");
            parameterIndex++;
        }
        
        // დაბადების თარიღი (დიაპაზონი)
        if (filters.BirthDateFrom.HasValue)
        {
            sql += $" AND p.BirthDate >= {{{parameterIndex}}}";
            parameters.Add(filters.BirthDateFrom.Value);
            parameterIndex++;
        }
        
        if (filters.BirthDateTo.HasValue)
        {
            sql += $" AND p.BirthDate <= {{{parameterIndex}}}";
            parameters.Add(filters.BirthDateTo.Value);
            parameterIndex++;
        }
        
        // ქალაქი
        if (filters.CityId.HasValue)
        {
            sql += $" AND p.CityId = {{{parameterIndex}}}";
            parameters.Add(filters.CityId.Value);
            parameterIndex++;
        }
        
        // სქესი
        if (filters.GenderId.HasValue)
        {
            sql += $" AND p.GenderId = {{{parameterIndex}}}";
            parameters.Add(filters.GenderId.Value);
            parameterIndex++;
        }
        
        // სურათის ფილტრი
        if (filters.HasImage.HasValue)
        {
            if (filters.HasImage.Value)
                sql += $" AND p.ImagePath IS NOT NULL AND p.ImagePath != ''";
            else
                sql += $" AND (p.ImagePath IS NULL OR p.ImagePath = '')";
        }
        
        // ტელეფონის ნომრის ფილტრი
        if (!string.IsNullOrWhiteSpace(filters.PhoneNumber))
        {
            sql += $" AND EXISTS (SELECT 1 FROM [Person].[PersonPhoneNumbers] pn WHERE pn.PersonId = p.Id AND pn.Number LIKE {{{parameterIndex}}})";
            parameters.Add($"%{filters.PhoneNumber}%");
            parameterIndex++;
        }
        
        // ტელეფონის ტიპის ფილტრი
        if (filters.PhoneTypeId.HasValue)
        {
            sql += $" AND EXISTS (SELECT 1 FROM [Person].[PersonPhoneNumbers] pn WHERE pn.PersonId = p.Id AND pn.PhoneTypeId = {{{parameterIndex}}})";
            parameters.Add(filters.PhoneTypeId.Value);
            parameterIndex++;
        }
        
        // ურთიერთობის ტიპის ფილტრი
        if (filters.RelationTypeId.HasValue)
        {
            sql += $" AND EXISTS (SELECT 1 FROM [Person].[PersonRelations] pr WHERE pr.PersonId = p.Id AND pr.RelationTypeId = {{{parameterIndex}}})";
            parameters.Add(filters.RelationTypeId.Value);
            parameterIndex++;
        }
        
        // შექმნის თარიღი (დიაპაზონი)
        if (filters.CreatedFrom.HasValue)
        {
            sql += $" AND p.CreatedAt >= {{{parameterIndex}}}";
            parameters.Add(filters.CreatedFrom.Value);
            parameterIndex++;
        }
        
        if (filters.CreatedTo.HasValue)
        {
            sql += $" AND p.CreatedAt <= {{{parameterIndex}}}";
            parameters.Add(filters.CreatedTo.Value);
            parameterIndex++;
        }
        
        // განახლების თარიღი (დიაპაზონი)
        if (filters.UpdatedFrom.HasValue)
        {
            sql += $" AND p.UpdatedAt >= {{{parameterIndex}}}";
            parameters.Add(filters.UpdatedFrom.Value);
            parameterIndex++;
        }
        
        if (filters.UpdatedTo.HasValue)
        {
            sql += $" AND p.UpdatedAt <= {{{parameterIndex}}}";
            parameters.Add(filters.UpdatedTo.Value);
            parameterIndex++;
        }
        
        sql += " ORDER BY p.FirstName, p.LastName";
        sql += $" OFFSET {(filters.Page - 1) * filters.PageSize} ROWS FETCH NEXT {filters.PageSize} ROWS ONLY";
        
        return await _context.Persons
            .FromSqlRaw(sql, parameters.ToArray())
            .Include(p => p.City)
            .ToListAsync();
    }
}
