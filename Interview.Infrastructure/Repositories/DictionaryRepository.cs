using Interview.Domain.Entities.Dictionary;
using Interview.Domain.Entities.Base;
using Interview.Application.Repositories;
using Interview.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;
using System.Dynamic;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Interview.Infrastructure.Repositories;

/// <summary>
/// Dictionary-specific operations implementation
/// გამოიყენება dictionary სქემის ცხრილების დინამიური ოპერაციებისთვის
/// </summary>
public class DictionaryRepository : IDictionaryRepository
{
    private readonly InterviewDbContext _context;

    public DictionaryRepository(InterviewDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// შეამოწმებს ცხრილის ვალიდურობას და გადააგდებს ერორს თუ არასწორია
    /// </summary>
    public async Task ValidateDictionaryTableAsync(string tableName)
    {
        var isValid = await _context.DictionaryNames
            .AnyAsync(d => d.TableName == tableName);
        if (!isValid) throw new ArgumentException($"არასწორი ცხრილი: {tableName}");
    }

    /// <summary>
    /// იღებს ყველა ხელმისაწვდომ dictionary ცხრილის სახელს
    /// </summary>
    public async Task<IEnumerable<string>> GetAvailableDictionaryTablesAsync()
    {
        return await _context.DictionaryNames
            .Select(d => d.TableName)
            .ToListAsync();
    }

    /// <summary>
    /// იღებს EntityTypeName-ს tableName-ის მიხედვით
    /// </summary>
    public async Task<string> GetDictionaryEntityTypeNameAsync(string tableName)
    {
        var dictionaryTableName = await _context.DictionaryNames
            .FirstOrDefaultAsync(d => d.TableName == tableName);
        
        if (dictionaryTableName == null)
        {
            throw new ArgumentException($"მსგავსი დასახელებით {tableName} ცხრილის კონფიგურაცია ვერ მოიძებნა");
        }
        
        if (string.IsNullOrEmpty(dictionaryTableName.EntityTypeName))
        {
            throw new InvalidOperationException($"EntityTypeName არ არის მითითებული {tableName} ცხრილისთვის");
        }
        
        return dictionaryTableName.EntityTypeName;
    }

    /// <summary>
    /// Entity type-ის მიღება DictionaryTableName-ის მიხედვით
    /// </summary>
    public async Task<Type> GetEntityTypeAsync(string tableName)
    {
        var entityTypeName = await GetDictionaryEntityTypeNameAsync(tableName);
        return GetEntityTypeByName(entityTypeName, tableName);
    }
    
    /// <summary>
    /// ყველა dictionary სქემის ცხრილიდან ჩანაწერის მიღება
    /// </summary>
    public async Task<IEnumerable<DictionaryBase>> GetAllDictionariesAsync(string tableName)
    {
        await ValidateDictionaryTableAsync(tableName);
        
        // Get DbSet using reflection
        var dbSet = GetDbSet(tableName);
        
        // Cast to non-generic IEnumerable and use ToList()
        var enumerable = (System.Collections.IEnumerable)dbSet;
        var items = enumerable.Cast<object>().ToList();
        
        return items.Cast<DictionaryBase>();
    }
    
    /// <summary>
    /// კონკრეტული dictionary სქემის ცხრილიდან ჩანაწერის მიღება
    /// </summary>
    public async Task<DictionaryBase?> GetDictionaryByIdAsync(string tableName, int id)
    {
        await ValidateDictionaryTableAsync(tableName);
        
        // Get DbSet using reflection
        var dbSet = GetDbSet(tableName);
        
        // Cast to non-generic IEnumerable and filter by ID
        var enumerable = (System.Collections.IEnumerable)dbSet;
        var items = enumerable.Cast<object>().ToList();
        
        // Filter by ID in memory
        return items.Cast<DictionaryBase>().FirstOrDefault(x => x.Id == id);
    }
    
    /// <summary>
    /// Dictionary სქემის ცხრილში ჩანაწერის შექმნა
    /// </summary>
    public async Task<DictionaryBase> CreateDictionaryAsync(string tableName, string name)
    {
        await ValidateDictionaryTableAsync(tableName);
        
        // Check for duplicate names
        var allItems = await GetAllDictionariesAsync(tableName);
        if (allItems.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Dictionary ჩანაწერი სახელით '{name}' უკვე არსებობს {tableName}-ში");
        }
        
        // Get entity type and create entity
        var entityType = GetEntityTypeByName(tableName.TrimEnd('s'), tableName);
        
        // Create entity using reflection with specific type
        var entity = CreateEntityOfType(entityType, name);
        
        // Cast to DictionaryBase
        var dictionaryEntity = (DictionaryBase)entity;
        
        // Get DbSet using reflection
        var dbSet = GetDbSet(tableName);
        
        // Use reflection to call Add method with proper type
        var addMethod = dbSet.GetType().GetMethod("Add");
        if (addMethod == null)
        {
            throw new InvalidOperationException($"Add მეთოდი ვერ მოიძებნა DbSet-ში");
        }
        
        // Cast entity to proper type and add
        var typedEntity = Convert.ChangeType(entity, entityType);
        addMethod.Invoke(dbSet, new object[] { typedEntity });
        
        return dictionaryEntity;
    }
    
    /// <summary>
    /// Dictionary სქემის ცხრილში არსებული ჩანაწერის განახლება
    /// </summary>
    public async Task<DictionaryBase> UpdateDictionaryAsync(string tableName, int id, string name)
    {
        await ValidateDictionaryTableAsync(tableName);
        
        var entity = await GetDictionaryByIdAsync(tableName, id);
        if (entity == null)
        {
            throw new ArgumentException($"Dictionary ჩანაწერი ID-ით {id} ვერ მოიძებნა {tableName}-ში");
        }
        
        // Check for duplicate names (excluding current entity)
        var allItems = await GetAllDictionariesAsync(tableName);
        if (allItems.Any(e => e.Id != id && e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Dictionary ჩანაწერი სახელით '{name}' უკვე არსებობს {tableName}-ში");
        }
        
        entity.Name = name;
        
        return entity;
    }
    
    /// <summary>
    /// Dictionary სქემის ცხრილიდან ჩანაწერის წაშლა
    /// </summary>
    public async Task<bool> DeleteDictionaryAsync(string tableName, int id)
    {
        await ValidateDictionaryTableAsync(tableName);
        
        var entity = await GetDictionaryByIdAsync(tableName, id);
        if (entity == null)
        {
            return false;
        }
        
        // Get DbSet using reflection like in CreateDictionaryAsync
        var dbSet = GetDbSet(tableName);
        
        // Use reflection to call Remove method with proper type
        var removeMethod = dbSet.GetType().GetMethod("Remove");
        if (removeMethod == null)
        {
            throw new InvalidOperationException($"Remove მეთოდი ვერ მოიძებნა DbSet-ში");
        }
        
        // Cast entity to proper type and remove
        var entityType = GetEntityTypeByName(tableName.TrimEnd('s'), tableName);
        var typedEntity = Convert.ChangeType(entity, entityType);
        removeMethod.Invoke(dbSet, new object[] { typedEntity });
        
        return true;
    }
    
    /// <summary>
    /// Entity type-ის მიღება სახელის მიხედვით
    /// </summary>
    private Type GetEntityTypeByName(string entityTypeName, string tableName)
    {
        // Search in current assembly and referenced assemblies
        var assemblies = new[] { typeof(DictionaryRepository).Assembly, typeof(DictionaryBase).Assembly };
        
        foreach (var assembly in assemblies)
        {
            var type = assembly.GetTypes()
                .FirstOrDefault(t => t.Name.Equals(entityTypeName, StringComparison.OrdinalIgnoreCase) && 
                                   typeof(DictionaryBase).IsAssignableFrom(t));
            
            if (type != null)
            {
                return type;
            }
        }
        
        throw new ArgumentException($"Entity ტიპი ვერ მოიძებნა {tableName} ცხრილისთვის");
    }
    
    /// <summary>
    /// Entity-ის შექმნა DictionaryTableName-ის მიხედვით
    /// </summary>
    private DictionaryBase CreateEntity(string tableName, string name)
    {
        var entityType = GetEntityTypeByName(tableName.TrimEnd('s'), tableName);
        return (DictionaryBase)CreateEntityOfType(entityType, name);
    }
    
    /// <summary>
    /// Entity-ის შექმნა კონკრეტული ტიპის მიხედვით
    /// </summary>
    private object CreateEntityOfType(Type entityType, string name)
    {
        // Try to find constructor with string parameter
        var constructor = entityType.GetConstructor(new[] { typeof(string) });
        if (constructor != null)
        {
            return constructor.Invoke(new object[] { name });
        }
        
        // Try parameterless constructor and set Name property
        var parameterlessConstructor = entityType.GetConstructor(Type.EmptyTypes);
        if (parameterlessConstructor != null)
        {
            var entity = parameterlessConstructor.Invoke(null);
            // Set Name property using reflection
            var nameProperty = entityType.GetProperty("Name");
            if (nameProperty != null)
            {
                nameProperty.SetValue(entity, name);
            }
            return entity;
        }
        
        throw new InvalidOperationException($"საჭირო კონსტრუქტორი ვერ მოიძებნა {entityType.Name}-ისთვის");
    }
    
    /// <summary>
    /// DbSet-ის მიღება DictionaryTableName-ის მიხედვით
    /// </summary>
    private object GetDbSet(string tableName)
    {
        var propertyName = tableName; // "Cities"
        var property = typeof(InterviewDbContext).GetProperty(propertyName);
        
        if (property == null)
        {
            throw new ArgumentException($"DbSet პროპერტი '{propertyName}' ვერ მოიძებნა InterviewDbContext-ში");
        }
        
        var dbSet = property.GetValue(_context);
        if (dbSet == null)
        {
            throw new InvalidOperationException($"DbSet '{propertyName}' არის null");
        }
        
        return dbSet;
    }
}
