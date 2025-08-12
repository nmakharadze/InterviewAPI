# Interview Project Architecture

## Project Structure

```
Interview/
├── Interview.Api/                    # Web API Layer
│   ├── Controllers/                  # API Controllers
│   │   ├── DictionaryController.cs   # Dictionary CRUD operations
│   │   ├── HomeController.cs         # Health check and basic info
│   │   ├── ImageController.cs        # Image upload/download operations
│   │   ├── LocalizationTestController.cs # Localization testing
│   │   ├── PersonController.cs       # Person CRUD operations
│   │   ├── PersonReportController.cs # Person reporting operations
│   │   ├── PhoneNumberController.cs  # Phone number operations
│   │   └── RelationController.cs     # Person relation operations
│   ├── Extensions/                   # Extension methods
│   │   └── LocalizationExtensions.cs # Localization extensions
│   ├── Localization/                 # Localization resources
│   │   └── Resources/                # Resource files
│   │       ├── SharedResource.cs     # Shared resource class
│   │       ├── SharedResource.en-US.resx # English resources
│   │       └── SharedResource.ka-GE.resx # Georgian resources
│   ├── Middleware/                   # Custom middleware
│   │   ├── GlobalExceptionHandlerMiddleware.cs # Global exception handling
│   │   └── LocalizationMiddleware.cs # Localization middleware
│   ├── wwwroot/                      # Static files
│   │   └── images/                   # Image storage
│   │       └── persons/              # Person images
│   ├── Program.cs                    # Application entry point
│   ├── appsettings.json             # Configuration files
│   ├── appsettings.Development.json
│   └── Interview.Api.http           # HTTP client tests
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
│   │   ├── Image/                    # Image operations
│   │   │   ├── Commands/             # Write operations
│   │   │   │   ├── DeleteImage/      # Delete image
│   │   │   │   │   ├── DeleteImageCommand.cs
│   │   │   │   │   └── DeleteImageCommandHandler.cs
│   │   │   │   ├── UpdateImage/      # Update image
│   │   │   │   └── UploadImage/      # Upload image
│   │   │   │       ├── UploadOrUpdateImageCommand.cs
│   │   │   │       ├── UploadOrUpdateImageCommandHandler.cs
│   │   │   │       └── UploadOrUpdateImageDto.cs
│   │   │   └── Queries/              # Read operations
│   │   │       └── GetImage/         # Get image
│   │   ├── PhoneNumber/              # Phone number operations
│   │   │   └── Commands/             # Write operations
│   │   │       ├── Create/           # Create phone number
│   │   │       │   ├── CreatePhoneNumberCommand.cs
│   │   │       │   ├── CreatePhoneNumberCommandHandler.cs
│   │   │       │   └── CreatePhoneNumberDto.cs
│   │   │       ├── Update/           # Update phone number
│   │   │       │   ├── UpdatePhoneNumberCommand.cs
│   │   │       │   ├── UpdatePhoneNumberCommandHandler.cs
│   │   │       │   └── UpdatePhoneNumberDto.cs
│   │   │       └── Delete/           # Delete phone number
│   │   │           ├── DeletePhoneNumberCommand.cs
│   │   │           └── DeletePhoneNumberCommandHandler.cs
│   │   ├── Relation/                 # Person relation operations
│   │   │   └── Commands/             # Write operations
│   │   │       ├── Create/           # Create relation
│   │   │       │   ├── CreateRelationCommand.cs
│   │   │       │   ├── CreateRelationCommandHandler.cs
│   │   │       │   └── CreateRelationDto.cs
│   │   │       ├── Update/           # Update relation
│   │   │       │   ├── UpdateRelationCommand.cs
│   │   │       │   ├── UpdateRelationCommandHandler.cs
│   │   │       │   └── UpdateRelationDto.cs
│   │   │       └── Delete/           # Delete relation
│   │   │           ├── DeleteRelationCommand.cs
│   │   │           └── DeleteRelationCommandHandler.cs
│   │   └── Queries/                  # Read operations
│   │       ├── AdvancedSearch/       # Advanced search
│   │       │   ├── AdvancedSearchFiltersDto.cs
│   │       │   ├── AdvancedSearchPersonsQuery.cs
│   │       │   ├── AdvancedSearchPersonsQueryHandler.cs
│   │       │   └── AdvancedSearchResultDto.cs
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
│   ├── Reports/                      # Reporting operations
│   │   └── Person/                   # Person reports
│   │       └── Queries/              # Read operations
│   │           └── GetPersonRelationsReport/
│   │               ├── GetPersonRelationsReportQuery.cs
│   │               ├── GetPersonRelationsReportQueryHandler.cs
│   │               └── PersonRelationsReportDto.cs
│   │
│   ├── Services/                     # Application services
│   │   ├── IFileStorageService.cs    # File storage service interface
│   │   └── ILocalizationService.cs   # Localization service interface
│   │
│   ├── Helpers/                      # Helper classes
│   │   └── FileExtensionHelper.cs    # File extension helper
│   │
│   ├── Repositories/                 # Repository interfaces
│   │   ├── IDictionaryRepository.cs
│   │   ├── IGenericRepository.cs
│   │   ├── IUnitOfWork.cs
│   │   └── Person/                   # Person-specific repositories
│   │       ├── IPersonPhoneRepository.cs
│   │       ├── IPersonRelationRepository.cs
│   │       ├── IPersonReportRepository.cs
│   │       ├── IPersonRepository.cs
│   │       └── IPersonSearchRepository.cs
│   │
│   └── Specifications/               # Specification pattern
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
│   ├── Exceptions/                   # Domain exceptions
│   ├── Repositories/                 # Repository interfaces (Domain layer)
│   └── Services/                     # Domain services
│
├── Interview.Infrastructure/         # Infrastructure Layer
│   ├── Data/                         # Data access
│   │   ├── InterviewDbContext.cs     # Entity Framework DbContext
│   │   ├── UnitOfWork.cs             # Unit of Work implementation
│   │   └── Configurations/           # Entity configurations
│   │       ├── Base/                 # Base configurations
│   │       │   ├── BaseEntityConfiguration.cs
│   │       │   └── DictionaryBaseConfiguration.cs
│   │       ├── Dictionary/           # Dictionary configurations
│   │       │   ├── CityConfiguration.cs
│   │       │   ├── DictionaryNameConfiguration.cs
│   │       │   ├── GenderConfiguration.cs
│   │       │   ├── PhoneTypeConfiguration.cs
│   │       │   └── RelationTypeConfiguration.cs
│   │       └── Person/               # Person configurations
│   │           ├── PersonConfiguration.cs
│   │           ├── PersonPhoneNumberConfiguration.cs
│   │           └── PersonRelationConfiguration.cs
│   ├── Repositories/                 # Repository implementations
│   │   ├── DictionaryRepository.cs   # Dictionary repository
│   │   ├── GenericRepository.cs      # Generic repository
│   │   └── Person/                   # Person-specific repositories
│   │       ├── PersonPhoneRepository.cs
│   │       ├── PersonRelationRepository.cs
│   │       ├── PersonReportRepository.cs
│   │       ├── PersonRepository.cs
│   │       └── PersonSearchRepository.cs
│   ├── Services/                     # Infrastructure services
│   │   ├── FileStorageService.cs     # File storage implementation
│   │   └── LocalizationService.cs    # Localization implementation
│   ├── Localization/                 # Localization resources
│   │   └── Resources/                # Resource files
│   │       ├── SharedResource.en-US.resx # English resources
│   │       └── SharedResource.ka-GE.resx # Georgian resources
│   ├── Specifications/               # Specification implementations
│   └── Migrations/                   # Entity Framework migrations
│       ├── 20250807145859_InitialCreate.cs
│       ├── 20250807145859_InitialCreate.Designer.cs
│       ├── 20250808110818_AddDictionaryNamesTable.cs
│       ├── 20250808110818_AddDictionaryNamesTable.Designer.cs
│       ├── 20250811143635_MakeImagePathNullable.cs
│       ├── 20250811143635_MakeImagePathNullable.Designer.cs
│       └── InterviewDbContextModelSnapshot.cs
│
├── Interview.Tests.Unit/             # Unit Tests
│   └── Domain/                       # Domain layer tests
│       └── ValueObjects/             # Value object tests
│           ├── AgeTests.cs           # Age value object tests
│           ├── BirthDateTests.cs     # Birth date value object tests
│           ├── ImagePathTests.cs     # Image path value object tests
│           ├── NameTests.cs          # Name value object tests
│           ├── PersonalNumberTests.cs # Personal number value object tests
│           └── PhoneNumberTests.cs   # Phone number value object tests
│
├── Interview.Tests.Integration/      # Integration Tests
│   └── Controllers/                  # Controller tests
│       └── PersonControllerTests.cs  # Person controller integration tests
│
├── .github/                          # GitHub configuration
├── .vs/                              # Visual Studio configuration
├── Documentations/                   # Project documentation
│   ├── DetailedDocumentation.md     # Detailed project documentation
│   ├── ArchitectureReadme.md        # Detailed architecture description
│   ├── DictionaryManualReadme.md    # Dictionary usage manual
│   └── DictionarySeedData.sql       # Database seed data
├── Interview.sln                     # Solution file
└── .gitignore                        # Git ignore rules
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

#### **Generic Repositories**
- **IGenericRepository<T>** - ზოგადი CRUD ოპერაციები ყველა ენტიტისთვის
- **IUnitOfWork** - ტრანზაქციების მართვა და ცვლილებების შენახვა

#### **Person-Specific Repositories**
- **IPersonRepository** - პიროვნების ძირითადი ოპერაციები (CRUD)
- **IPersonSearchRepository** - პიროვნებების ძიების ოპერაციები
- **IPersonPhoneRepository** - პიროვნების ტელეფონის ნომრების მართვა
- **IPersonRelationRepository** - პიროვნებების ურთიერთობების მართვა
- **IPersonReportRepository** - პიროვნებების ანგარიშების გენერაცია

#### **Dictionary Repositories**
- **IDictionaryRepository** - ლექსიკონის სპეციფიური ოპერაციები (Cities, Genders, PhoneTypes, RelationTypes)

#### **Repository Implementations**
ყველა რეპოზიტორი იმპლემენტირებულია Infrastructure Layer-ში:
- **GenericRepository<T>** - ზოგადი რეპოზიტორის იმპლემენტაცია
- **PersonRepository** - პიროვნების რეპოზიტორის იმპლემენტაცია
- **PersonSearchRepository** - ძიების რეპოზიტორის იმპლემენტაცია
- **PersonPhoneRepository** - ტელეფონის ნომრების რეპოზიტორის იმპლემენტაცია
- **PersonRelationRepository** - ურთიერთობების რეპოზიტორის იმპლემენტაცია
- **PersonReportRepository** - ანგარიშების რეპოზიტორის იმპლემენტაცია
- **DictionaryRepository** - ლექსიკონის რეპოზიტორის იმპლემენტაცია

### Value Objects
Domain Layer-ში გამოიყენება Value Objects მონაცემთა უფრო უსაფრთხო და მნიშვნელოვანი ტიპებისთვის:
- **Age** - ასაკის ვალიდაცია (18+ წელი)
- **BirthDate** - დაბადების თარიღის ვალიდაცია (18+ წელი)
- **Name** - სახელის ვალიდაცია (ქართული/ლათინური ასოები)
- **PersonalNumber** - პირადი ნომრის ვალიდაცია (11 ციფრი)
- **PhoneNumber** - ტელეფონის ნომრის ვალიდაცია (ქართული ფორმატი)
- **ImagePath** - სურათის მისამართის ვალიდაცია (ფაილის გაფართოებები)

### Entity Framework
Infrastructure Layer იყენებს Entity Framework Core-ს:
- **InterviewDbContext** - მთავარი DbContext
- **Configurations** - ენტიტების კონფიგურაციები
- **Migrations** - მონაცემთა ბაზის სქემის ცვლილებები

### Localization
პროექტი მხარს უჭერს მრავალენოვნებას:
- **Resource Files** - .resx ფაილები ინგლისური და ქართული ენებისთვის
- **LocalizationService** - ლოკალიზაციის სერვისი
- **LocalizationMiddleware** - Accept-Language ჰედერის დამუშავება

### File Storage
სურათების მართვისთვის:
- **FileStorageService** - ფაილების შენახვა ფაილურ სისტემაში
- **ImageController** - სურათების ატვირთვა/წაშლა/მიღება
- **ImagePath Value Object** - სურათის მისამართის ვალიდაცია

### Testing
პროექტი მოიცავს ორივე ტიპის ტესტებს:
- **Unit Tests** - Value Objects-ების ტესტირება
- **Integration Tests** - API კონტროლერების ტესტირება

### Advanced Features
- **Advanced Search** - რთული ძიების ფუნქციონალი
- **Reporting** - პიროვნებების ანგარიშები
- **Specification Pattern** - რთული ფილტრებისთვის
- **Global Exception Handling** - გლობალური შეცდომების მართვა
