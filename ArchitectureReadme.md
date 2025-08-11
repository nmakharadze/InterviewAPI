# Interview Project Architecture

## Project Structure

```
Interview/
├── Interview.Api/                    # Web API Layer
│   ├── Controllers/                  # API Controllers
│   │   ├── DictionaryController.cs   # Dictionary CRUD operations
│   │   ├── HomeController.cs         # Health check and basic info
│   │   ├── PersonController.cs       # Person CRUD operations
│   │   ├── PhoneNumberController.cs  # Phone number operations
│   │   └── RelationController.cs     # Person relation operations
│   ├── Middleware/                   # Custom middleware
│   │   └── GlobalExceptionHandlerMiddleware.cs
│   ├── Program.cs                    # Application entry point
│   ├── appsettings.json             # Configuration files
│   └── appsettings.Development.json
│
├── Interview.Application/            # Application Layer (CQRS)
│   ├── Dictionaries/                 # Dictionary domain operations
│   │   ├── Commands/                 # Write operations
│   │   │   ├── Create/               # Create Dictionary
│   │   │   │   ├── CreateDictionaryCommand.cs
│   │   │   │   ├── CreateDictionaryCommandHandler.cs
│   │   │   │   ├── CreateDictionaryDto.cs
│   │   │   │   └── CreateDictionaryResultDto.cs
│   │   │   ├── Delete/               # Delete Dictionary
│   │   │   │   ├── DeleteDictionaryCommand.cs
│   │   │   │   └── DeleteDictionaryCommandHandler.cs
│   │   │   └── Update/               # Update Dictionary
│   │   │       ├── UpdateDictionaryCommand.cs
│   │   │       ├── UpdateDictionaryCommandHandler.cs
│   │   │       ├── UpdateDictionaryDto.cs
│   │   │       └── UpdateDictionaryResultDto.cs
│   │   └── Queries/                  # Read operations
│   │       ├── GetAll/               # Get all dictionaries
│   │       │   ├── GetAllDictionariesQuery.cs
│   │       │   ├── GetAllDictionariesQueryHandler.cs
│   │       │   └── DictionaryListDto.cs
│   │       ├── GetDetail/            # Get dictionary detail
│   │       │   ├── GetDictionaryQuery.cs
│   │       │   ├── GetDictionaryQueryHandler.cs
│   │       │   └── DictionaryDetailDto.cs
│   │       └── GetDictionaryTable/   # Get dictionary table data
│   │           ├── GetDictionaryTableQuery.cs
│   │           ├── GetDictionaryTableQueryHandler.cs
│   │           └── DictionaryTableDto.cs
│   │
│   ├── Persons/                      # Person domain operations
│   │   ├── Commands/                 # Write operations
│   │   │   ├── Create/               # Create Person
│   │   │   │   ├── CreatePersonCommand.cs
│   │   │   │   ├── CreatePersonCommandHandler.cs
│   │   │   │   ├── CreatePersonDto.cs
│   │   │   │   └── CreatePersonResultDto.cs
│   │   │   ├── Delete/               # Delete Person
│   │   │   │   ├── DeletePersonCommand.cs
│   │   │   │   └── DeletePersonCommandHandler.cs
│   │   │   └── Update/               # Update Person
│   │   │       ├── UpdatePersonCommand.cs
│   │   │       ├── UpdatePersonCommandHandler.cs
│   │   │       ├── UpdatePersonDto.cs
│   │   │       └── UpdatePersonResultDto.cs
│   │   └── Queries/                  # Read operations
│   │       ├── GetAll/               # Get all persons
│   │       │   ├── GetAllPersonsQuery.cs
│   │       │   ├── GetAllPersonsQueryHandler.cs
│   │       │   └── PersonListDto.cs
│   │       ├── GetById/              # Get person by ID
│   │       │   ├── GetPersonQuery.cs
│   │       │   ├── GetPersonQueryHandler.cs
│   │       │   └── PersonDto.cs
│   │       ├── GetDetail/            # Get person with details
│   │       │   ├── GetPersonDetailQuery.cs
│   │       │   ├── GetPersonDetailQueryHandler.cs
│   │       │   ├── PersonDetailDto.cs
│   │       │   ├── PhoneNumberDto.cs
│   │       │   └── RelationDto.cs
│   │       └── SearchPersons/        # Search persons
│   │           ├── SearchPersonsQuery.cs
│   │           ├── SearchPersonsQueryHandler.cs
│   │           └── SearchResultDto.cs
│   │
│   ├── PersonPhoneNumber/            # Phone number operations
│   │   └── Commands/                 # Write operations
│   │       ├── Create/               # Create phone number
│   │       │   ├── CreatePhoneNumberCommand.cs
│   │       │   ├── CreatePhoneNumberCommandHandler.cs
│   │       │   └── CreatePhoneNumberDto.cs
│   │       ├── Update/               # Update phone number
│   │       │   ├── UpdatePhoneNumberCommand.cs
│   │       │   ├── UpdatePhoneNumberCommandHandler.cs
│   │       │   └── UpdatePhoneNumberDto.cs
│   │       └── Delete/               # Delete phone number
│   │           ├── DeletePhoneNumberCommand.cs
│   │           └── DeletePhoneNumberCommandHandler.cs
│   │
│   ├── PersonRelation/               # Person relation operations
│   │   └── Commands/                 # Write operations
│   │       ├── Create/               # Create relation
│   │       │   ├── CreateRelationCommand.cs
│   │       │   ├── CreateRelationCommandHandler.cs
│   │       │   └── CreateRelationDto.cs
│   │       ├── Update/               # Update relation
│   │       │   ├── UpdateRelationCommand.cs
│   │       │   ├── UpdateRelationCommandHandler.cs
│   │       │   └── UpdateRelationDto.cs
│   │       └── Delete/               # Delete relation
│   │           ├── DeleteRelationCommand.cs
│   │           └── DeleteRelationCommandHandler.cs
│   │
│   └── Repositories/                 # Repository interfaces
│       ├── IDictionaryRepository.cs
│       ├── IGenericRepository.cs
│       ├── IPersonRepository.cs
│       └── IUnitOfWork.cs
│
├── Interview.Domain/                 # Domain Layer
│   ├── Entities/                     # Domain entities
│   │   ├── Base/                     # Base entity classes
│   │   │   ├── BaseEntity.cs         # Base entity with common properties
│   │   │   └── DictionaryBase.cs     # Base class for dictionary entities
│   │   ├── Dictionary/               # Dictionary entities
│   │   │   ├── City.cs               # City entity
│   │   │   ├── DictionaryName.cs     # Dictionary name entity
│   │   │   ├── Gender.cs             # Gender entity
│   │   │   ├── PhoneType.cs          # Phone type entity
│   │   │   └── RelationType.cs       # Relation type entity
│   │   └── Person/                   # Person entities
│   │       ├── Person.cs             # Main person entity
│   │       ├── PersonPhoneNumber.cs  # Person phone number entity
│   │       └── PersonRelation.cs     # Person relation entity
│   ├── ValueObjects/                 # Value objects
│   │   ├── Age.cs                    # Age value object
│   │   ├── BirthDate.cs              # Birth date value object
│   │   ├── ImagePath.cs              # Image path value object
│   │   ├── Name.cs                   # Name value object
│   │   ├── PersonalNumber.cs         # Personal number value object
│   │   ├── PhoneNumber.cs            # Phone number value object
│   │   └── ValueObject.cs            # Base value object class
│   ├── Constants/                    # Domain constants
│   ├── Events/                       # Domain events
│   └── Exceptions/                   # Domain exceptions
│
└── Interview.Infrastructure/         # Infrastructure Layer
    ├── Data/                         # Data access
    │   ├── InterviewDbContext.cs     # Entity Framework DbContext
    │   ├── UnitOfWork.cs             # Unit of Work implementation
    │   └── Configurations/           # Entity configurations
    │       ├── Base/                 # Base configurations
    │       │   ├── BaseEntityConfiguration.cs
    │       │   └── DictionaryBaseConfiguration.cs
    │       ├── Dictionary/           # Dictionary configurations
    │       │   ├── CityConfiguration.cs
    │       │   ├── DictionaryNameConfiguration.cs
    │       │   ├── GenderConfiguration.cs
    │       │   ├── PhoneTypeConfiguration.cs
    │       │   └── RelationTypeConfiguration.cs
    │       └── Person/               # Person configurations
    │           ├── PersonConfiguration.cs
    │           ├── PersonPhoneNumberConfiguration.cs
    │           └── PersonRelationConfiguration.cs
    ├── Repositories/                 # Repository implementations
    │   ├── DictionaryRepository.cs   # Dictionary repository
    │   ├── GenericRepository.cs      # Generic repository
    │   └── PersonRepository.cs       # Person repository
    └── Migrations/                   # Entity Framework migrations
        ├── 20250807145859_InitialCreate.cs
        ├── 20250807145859_InitialCreate.Designer.cs
        ├── 20250808110818_AddDictionaryNamesTable.cs
        ├── 20250808110818_AddDictionaryNamesTable.Designer.cs
        └── InterviewDbContextModelSnapshot.cs
```

## Architecture Overview

### Clean Architecture Pattern
პროექტი იყენებს Clean Architecture-ის პრინციპებს:

1. **Domain Layer** - ბიზნეს ლოგიკა და ენტიტები
2. **Application Layer** - CQRS პატერნით ორგანიზებული ბიზნეს ოპერაციები
3. **Infrastructure Layer** - მონაცემთა ბაზის ფაილი და რეპოზიტორიები
4. **API Layer** - HTTP API კონტროლერები

### CQRS Pattern
Application Layer იყენებს CQRS (Command Query Responsibility Segregation) პატერნს:

- **Commands** - მონაცემთა ცვლილების ოპერაციები (Create, Update, Delete)
- **Queries** - მონაცემთა წაკითხვის ოპერაციები (Get, Search, List)

### DTO Structure
ყველა Command-ს აქვს შესაბამისი DTO კლასები:
- **Input DTOs** - მონაცემთა შეყვანისთვის (CreateXxxDto, UpdateXxxDto)
- **Result DTOs** - ოპერაციის შედეგისთვის (CreateXxxResultDto, UpdateXxxResultDto)

### Repository Pattern
მონაცემთა ფაილისთვის გამოიყენება Repository პატერნი:
- **IGenericRepository<T>** - ზოგადი ოპერაციები
- **IPersonRepository** - პიროვნების სპეციფიური ოპერაციები
- **IDictionaryRepository** - ლექსიკონის სპეციფიური ოპერაციები
- **IUnitOfWork** - ტრანზაქციების მართვა

### Value Objects
Domain Layer-ში გამოიყენება Value Objects მონაცემთა უფრო უსაფრთხო და მნიშვნელოვანი ტიპებისთვის:
- Age, BirthDate, Name, PersonalNumber, PhoneNumber, ImagePath

### Entity Framework
Infrastructure Layer იყენებს Entity Framework Core-ს:
- **InterviewDbContext** - მთავარი DbContext
- **Configurations** - ენტიტების კონფიგურაციები
- **Migrations** - მონაცემთა ბაზის სქემის ცვლილებები
