# ფიზიკური პირების ცნობარი - API პროექტი

## პროექტის აღწერა
ეს არის ფიზიკური პირების ცნობარის API, რომელიც შექმნილია ASP.NET Core Web API-ს გამოყენებით Clean Architecture პრინციპების დაცვით.

## ტექნოლოგიები
- **პროგრამირების ენა**: C#
- **API Framework**: ASP.NET Core Web API
- **Database Framework**: Entity Framework Core
- **Database**: Microsoft SQL Server
- **Architecture**: Clean Architecture + Repository Pattern + Unit of Work

## პროექტის სტრუქტურა (Clean Architecture)

```
InterviewApi/
├── src/
│   ├── InterviewApi.API/                 # Presentation Layer
│   ├── InterviewApi.Application/          # Application Layer
│   ├── InterviewApi.Domain/              # Domain Layer
│   └── InterviewApi.Infrastructure/      # Infrastructure Layer
├── tests/
│   ├── InterviewApi.UnitTests/           # Unit Tests
│   └── InterviewApi.IntegrationTests/    # Integration Tests
└── docs/                                 # დოკუმენტაცია
```

## ბაზის სქემების განაწილება

### 1. Base ცხრილები

#### BaseEntity (ყველა ცხრილის ბაზა)
```sql
-- ყველა ცხრილს ექნება ეს ველები
Id INT IDENTITY(1,1) PRIMARY KEY,
CreatedAt DATETIME2 DEFAULT GETDATE(),
UpdatedAt DATETIME2 DEFAULT GETDATE()
```

### 2. Dictionary სქემა (ცნობარები)

#### DictionaryBase (ცნობარების ბაზა)
```sql
-- ყველა ცნობარს ექნება ეს ველები
Name NVARCHAR(100) NOT NULL,
IsActive BIT DEFAULT 1
```

#### Genders (სქესები)
```sql
CREATE TABLE Dictionary.Genders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
)
```

#### Cities (ქალაქები)
```sql
CREATE TABLE Dictionary.Cities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
)
```

#### PhoneTypes (ტელეფონის ნომრების ტიპები)
```sql
CREATE TABLE Dictionary.PhoneTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
)
```

#### RelationTypes (ურთიერთობის ტიპები)
```sql
CREATE TABLE Dictionary.RelationTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
)
```

### 3. Person სქემა

#### Persons (ფიზიკური პირები)
```sql
CREATE TABLE Person.Persons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    GenderId INT NOT NULL,
    PersonalNumber NVARCHAR(11) NOT NULL UNIQUE,
    BirthDate DATE NOT NULL,
    CityId INT NOT NULL,
    ImagePath NVARCHAR(500) NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (GenderId) REFERENCES Dictionary.Genders(Id),
    FOREIGN KEY (CityId) REFERENCES Dictionary.Cities(Id)
)
```

#### PersonPhoneNumbers (ტელეფონის ნომრები)
```sql
CREATE TABLE Person.PersonPhoneNumbers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PersonId INT NOT NULL,
    PhoneTypeId INT NOT NULL,
    Number NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PersonId) REFERENCES Person.Persons(Id) ON DELETE CASCADE,
    FOREIGN KEY (PhoneTypeId) REFERENCES Dictionary.PhoneTypes(Id)
)
```

#### PersonRelations (დაკავშირებული პირები)
```sql
CREATE TABLE Person.PersonRelations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PersonId INT NOT NULL,
    RelatedPersonId INT NOT NULL,
    RelationTypeId INT NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PersonId) REFERENCES Person.Persons(Id) ON DELETE CASCADE,
    FOREIGN KEY (RelatedPersonId) REFERENCES Person.Persons(Id) ON DELETE CASCADE,
    FOREIGN KEY (RelationTypeId) REFERENCES Dictionary.RelationTypes(Id),
    UNIQUE (PersonId, RelatedPersonId)
)
```

### 4. ფაილების მენეჯმენტი

ფაილები შეინახება ფაილურ სისტემაში, ბაზაში მხოლოდ ფაილის relative path ინახება ImagePath ველში.

**ფაილების სტრუქტურა:**
```
wwwroot/
├── images/
│   ├── persons/
│   │   ├── {personId}/
│   │   │   ├── profile.jpg
│   │   │   └── thumbnail.jpg
│   │   └── ...
│   └── temp/
└── uploads/
```

**ფაილების ვალიდაცია:**
- მხოლოდ JPG, PNG, GIF ფორმატები
- მაქსიმუმ 5MB ზომა
- ავტომატური thumbnail შექმნა
- ფაილების compression

## სრული გეგმა საფეხურებად

### ფაზა 1: პროექტის სტრუქტურის შექმნა (1-2 დღე)

**დღე 1:**
- [ ] Solution-ის შექმნა
- [ ] Clean Architecture პროექტების შექმნა:
  - InterviewApi.API
  - InterviewApi.Application
  - InterviewApi.Domain
  - InterviewApi.Infrastructure
- [ ] NuGet პაკეტების დამატება:
  - Entity Framework Core
  - AutoMapper
  - FluentValidation
  - Serilog (ლოგირებისთვის)

**დღე 2:**
- [ ] Domain Layer-ის შექმნა (Base Entities)
- [ ] Database სქემების დიზაინი
- [ ] პროექტის სტრუქტურის ტესტირება

#### 1.1 პროექტის სტრუქტურის შექმნა
- [ ] Solution-ის შექმნა
- [ ] Clean Architecture პროექტების შექმნა:
  - InterviewApi.API
  - InterviewApi.Application
  - InterviewApi.Domain
  - InterviewApi.Infrastructure
- [ ] Test პროექტების შექმნა
- [ ] NuGet პაკეტების დამატება

#### 1.2 Domain Layer შექმნა
- [ ] Base Entities შექმნა:
  - BaseEntity (Id, CreatedAt, UpdatedAt)
  - DictionaryBase (Name, IsActive) - BaseEntity-ს შვილობილი
- [ ] Dictionary Entities შექმნა:
  - Gender (DictionaryBase-ს შვილობილი)
  - City (DictionaryBase-ს შვილობილი)
  - PhoneType (DictionaryBase-ს შვილობილი)
  - RelationType (DictionaryBase-ს შვილობილი)
- [ ] Person Entities შექმნა:
  - Person (BaseEntity-ს შვილობილი)
  - PersonPhoneNumber (BaseEntity-ს შვილობილი)
  - PersonRelation (BaseEntity-ს შვილობილი)
- [ ] Value Objects შექმნა:
  - PersonalNumber (11 ციფრი, უნიკალური)
  - Name (ქართული/ლათინური ვალიდაციით, 2-50 სიმბოლო)
  - Age (18+ ვალიდაციით)
  - PhoneNumber (4-50 სიმბოლო)
- [ ] Domain Events შექმნა:
  - PersonCreatedEvent
  - PersonUpdatedEvent
  - PersonDeletedEvent
  - ImageUploadedEvent
  - DictionaryItemCreatedEvent
  - DictionaryItemUpdatedEvent
- [ ] Domain Exceptions შექმნა:
  - InvalidNameException
  - InvalidPersonalNumberException
  - InvalidAgeException
  - PersonNotFoundException
  - DictionaryItemNotFoundException
- [ ] Domain Services შექმნა:
  - NameValidationService (ქართული/ლათინური ვალიდაცია)
  - AgeCalculationService (ასაკის გამოთვლა)
  - PersonalNumberValidationService (უნიკალურობის შემოწმება)

### ფაზა 2: Infrastructure Layer (2-3 დღე)

**დღე 1:**
- [ ] DbContext შექმნა და კონფიგურაცია
- [ ] Base Entity კონფიგურაციები
- [ ] Dictionary Entity კონფიგურაციები

**დღე 2:**
- [ ] Person Entity კონფიგურაციები
- [ ] Repository Pattern იმპლემენტაცია
- [ ] Unit of Work Pattern იმპლემენტაცია

**დღე 3:**
- [ ] Database Migrations შექმნა
- [ ] Seed Data შექმნა
- [ ] ფაილური სისტემის სერვისი

#### 2.1 Entity Framework კონფიგურაცია
- [ ] DbContext შექმნა:
  - ApplicationDbContext (ყველა ცხრილისთვის)
  - სქემების კონფიგურაცია (Dictionary, Person)
- [ ] Entity კონფიგურაციები (Fluent API):
  - BaseEntityConfiguration
  - DictionaryBaseConfiguration
  - PersonConfiguration
  - PersonPhoneNumberConfiguration
  - PersonRelationConfiguration
  - GenderConfiguration
  - CityConfiguration
  - PhoneTypeConfiguration
  - RelationTypeConfiguration
- [ ] Database Migrations შექმნა:
  - InitialCreate (ყველა ცხრილის შექმნა)
  - SeedData (საწყისი მონაცემების დამატება)
- [ ] Repository Pattern იმპლემენტაცია:
  - IRepository<T> ინტერფეისი (generic)
  - GenericRepository<T> იმპლემენტაცია
  - IDictionaryRepository<T> ინტერფეისი (dictionary entities-სთვის)
  - DictionaryRepository<T> იმპლემენტაცია
  - IPersonRepository (specific person operations)
  - PersonRepository იმპლემენტაცია
- [ ] Unit of Work Pattern იმპლემენტაცია:
  - IUnitOfWork ინტერფეისი
  - UnitOfWork იმპლემენტაცია (transaction management)

#### 2.2 ფაილური სისტემის სერვისი
- [ ] IFileService ინტერფეისი
- [ ] LocalFileService იმპლემენტაცია
- [ ] სურათების ატვირთვის ლოგიკა:
  - სურათის ზომის შემოწმება
  - ფაილის ფორმატის ვალიდაცია
  - ფაილის შენახვა ფაილურ სისტემაში
  - ფაილის წაშლა
- [ ] ImageProcessingService (სურათების resize, compression)

### ფაზა 3: Application Layer (3-4 დღე)

**დღე 1:**
- [ ] Base DTOs შექმნა
- [ ] Dictionary DTOs შექმნა
- [ ] AutoMapper Base პროფილები

**დღე 2:**
- [ ] Person DTOs შექმნა
- [ ] Request/Response DTOs
- [ ] AutoMapper Person პროფილები

**დღე 3:**
- [ ] Base Services იმპლემენტაცია
- [ ] Dictionary Services იმპლემენტაცია
- [ ] Person Services იმპლემენტაცია

**დღე 4:**
- [ ] ვალიდაციის იმპლემენტაცია
- [ ] Custom Validators
- [ ] Report Services

#### 3.1 DTOs შექმნა
- [ ] Base DTOs:
  - BaseResponse (Id, CreatedAt, UpdatedAt)
  - DictionaryResponse (Name, IsActive) - BaseResponse-ს შვილობილი
- [ ] Request DTOs:
  - CreatePersonRequest
  - UpdatePersonRequest
  - AddPersonPhoneNumberRequest
  - AddPersonRelationRequest
  - PersonSearchRequest
  - UploadImageRequest
  - CreateDictionaryItemRequest (generic)
  - UpdateDictionaryItemRequest (generic)
- [ ] Response DTOs:
  - PersonResponse (BaseResponse-ს შვილობილი)
  - PersonDetailResponse (PersonResponse + relations + phones)
  - PersonListResponse (paging + total count)
  - DictionaryResponse (generic)
  - ReportResponse
- [ ] AutoMapper პროფილები:
  - BaseMappingProfile
  - DictionaryMappingProfile
  - PersonMappingProfile
  - PersonPhoneNumberMappingProfile
  - PersonRelationMappingProfile

#### 3.2 სერვისების შექმნა
- [ ] Base Services:
  - IBaseService<T> ინტერფეისი (generic CRUD operations)
  - BaseService<T> იმპლემენტაცია
- [ ] Dictionary Services:
  - IDictionaryService<T> ინტერფეისი (dictionary entities)
  - DictionaryService<T> იმპლემენტაცია
  - IGenderService, GenderService
  - ICityService, CityService
  - IPhoneTypeService, PhoneTypeService
  - IRelationTypeService, RelationTypeService
- [ ] Person Services:
  - IPersonService ინტერფეისი
  - PersonService იმპლემენტაცია (CRUD + search + relations)
  - IPersonPhoneNumberService ინტერფეისი
  - PersonPhoneNumberService იმპლემენტაცია
  - IPersonRelationService ინტერფეისი
  - PersonRelationService იმპლემენტაცია
- [ ] File Services:
  - IFileService ინტერფეისი
  - FileService იმპლემენტაცია (image upload/delete)
- [ ] Report Services:
  - IReportService ინტერფეისი
  - ReportService იმპლემენტაცია (statistics + relations report)

#### 3.3 ვალიდაცია
- [ ] FluentValidation იმპლემენტაცია
- [ ] Base Validators:
  - BaseValidator<T> (generic validation)
  - DictionaryValidator<T> (dictionary entities)
- [ ] Custom validators შექმნა:
  - NameValidator (ქართული/ლათინური, 2-50 სიმბოლო)
  - PersonalNumberValidator (11 ციფრი, უნიკალური)
  - AgeValidator (18+)
  - PhoneNumberValidator (4-50 სიმბოლო)
  - BirthDateValidator (მინიმუმ 18 წლის)
  - DictionaryNameValidator (უნიკალური სახელი)
- [ ] Custom validation attributes შექმნა:
  - GeorgianOrLatinLettersAttribute
  - MinimumAgeAttribute
  - UniquePersonalNumberAttribute
  - UniqueDictionaryNameAttribute

### ფაზა 4: API Layer (2-3 დღე)

**დღე 1:**
- [ ] Base Controllers შექმნა
- [ ] Dictionary Controllers შექმნა
- [ ] Middleware შექმნა

**დღე 2:**
- [ ] Person Controllers შექმნა
- [ ] Report Controllers შექმნა
- [ ] Images Controller შექმნა

**დღე 3:**
- [ ] Dependency Injection კონფიგურაცია
- [ ] კონფიგურაციების დასრულება
- [ ] API ტესტირება

#### 4.1 Controllers შექმნა
- [ ] Base Controllers:
  - BaseController<T> (generic CRUD operations)
  - DictionaryController<T> (dictionary CRUD operations)
- [ ] Specific Controllers:
  - PersonsController (CRUD, ძებნა, paging, relations)
  - PersonPhoneNumbersController (phone numbers management)
  - PersonRelationsController (relations management)
  - GendersController (CRUD)
  - CitiesController (CRUD)
  - PhoneTypesController (CRUD)
  - RelationTypesController (CRUD)
  - ReportsController (დაკავშირებული პირების რეპორტი)
  - ImagesController (სურათების მენეჯმენტი)

#### 4.2 Middleware შექმნა
- [ ] Exception Handling Middleware
- [ ] Localization Middleware
- [ ] Logging Middleware

#### 4.3 კონფიგურაცია
- [ ] Dependency Injection კონფიგურაცია
- [ ] AutoMapper კონფიგურაცია
- [ ] FluentValidation კონფიგურაცია
- [ ] Localization კონფიგურაცია

### ფაზა 5: ფუნქციონალური მოთხოვნები (4-5 დღე)

**დღე 1:**
- [ ] Dictionary CRUD ოპერაციები
- [ ] Person CRUD ოპერაციები
- [ ] ვალიდაციის ტესტირება

**დღე 2:**
- [ ] სურათების მენეჯმენტი
- [ ] ფაილური სისტემის ტესტირება
- [ ] ურთიერთობების მენეჯმენტი

**დღე 3:**
- [ ] ძებნა და ფილტრაცია
- [ ] Paging და Sorting
- [ ] ძებნის ფუნქციების ტესტირება

**დღე 4:**
- [ ] რეპორტების შექმნა
- [ ] სტატისტიკური რეპორტები
- [ ] API endpoints ტესტირება

**დღე 5:**
- [ ] ლოკალიზაციის დასრულება
- [ ] შეცდომების დამუშავება
- [ ] ფინალური ტესტირება

#### 5.1 CRUD ოპერაციები
- [ ] ფიზიკური პირის დამატება
- [ ] ფიზიკური პირის რედაქტირება
- [ ] ფიზიკური პირის წაშლა
- [ ] ფიზიკური პირის დეტალების მიღება

#### 5.2 სურათების მენეჯმენტი
- [ ] სურათის ატვირთვა
- [ ] სურათის განახლება
- [ ] სურათის წაშლა

#### 5.3 ურთიერთობების მენეჯმენტი
- [ ] დაკავშირებული პირის დამატება
- [ ] დაკავშირებული პირის წაშლა
- [ ] ურთიერთობის ტიპის ცვლილება
- [ ] ურთიერთობების სიის მიღება

#### 5.4 ძებნა და ფილტრაცია
- [ ] სწრაფი ძებნა (SQL LIKE):
  - სახელი/გვარი
  - პირადი ნომერი
- [ ] დეტალური ძებნა:
  - ქალაქის მიხედვით
  - სქესის მიხედვით
  - ასაკის დიაპაზონით
  - დაბადების თარიღით
- [ ] Paging იმპლემენტაცია:
  - PageNumber, PageSize პარამეტრები
  - TotalCount, TotalPages მეტამონაცემები
- [ ] Sorting იმპლემენტაცია:
  - სახელი/გვარი
  - დაბადების თარიღი
  - შექმნის თარიღი

#### 5.5 რეპორტები
- [ ] დაკავშირებული პირების რაოდენობის რეპორტი:
  - თითოეული პირისთვის დაკავშირებული პირების რაოდენობა
  - კავშირის ტიპების მიხედვით დაჯგუფება
  - ქალაქების მიხედვით სტატისტიკა
- [ ] სტატისტიკური რეპორტები:
  - ასაკის დიაპაზონების მიხედვით
  - სქესის მიხედვით
  - ქალაქების მიხედვით

### ფაზა 6: დოკუმენტაცია და დეპლოიმენტი (1-2 დღე)

**დღე 1:**
- [ ] Swagger/OpenAPI კონფიგურაცია
- [ ] API endpoints დოკუმენტაცია
- [ ] Request/Response მაგალითები

**დღე 2:**
- [ ] Docker კონფიგურაცია
- [ ] Environment კონფიგურაციები
- [ ] Database migration scripts
- [ ] დეპლოიმენტის ტესტირება

#### 6.1 API დოკუმენტაცია
- [ ] Swagger/OpenAPI კონფიგურაცია
- [ ] API endpoints დოკუმენტაცია
- [ ] Request/Response მაგალითები

#### 6.2 დეპლოიმენტი
- [ ] Docker კონფიგურაცია
- [ ] Environment კონფიგურაციები
- [ ] Database migration scripts
- [ ] Production კონფიგურაციები

## API Endpoints

### Persons
- `POST /api/persons` - ფიზიკური პირის დამატება
- `GET /api/persons/{id}` - ფიზიკური პირის დეტალები
- `PUT /api/persons/{id}` - ფიზიკური პირის განახლება
- `DELETE /api/persons/{id}` - ფიზიკური პირის წაშლა
- `GET /api/persons` - ფიზიკური პირების სია (ძებნით და paging-ით)

### Images
- `POST /api/persons/{id}/image` - სურათის ატვირთვა
- `PUT /api/persons/{id}/image` - სურათის განახლება
- `DELETE /api/persons/{id}/image` - სურათის წაშლა

### Relations
- `POST /api/persons/{id}/relations` - დაკავშირებული პირის დამატება
- `DELETE /api/persons/{id}/relations/{relationId}` - დაკავშირებული პირის წაშლა

### Dictionary (ცნობარები)
- `GET /api/genders` - სქესების სია
- `POST /api/genders` - ახალი სქესის დამატება
- `PUT /api/genders/{id}` - სქესის განახლება
- `DELETE /api/genders/{id}` - სქესის წაშლა

- `GET /api/cities` - ქალაქების სია
- `POST /api/cities` - ახალი ქალაქის დამატება
- `PUT /api/cities/{id}` - ქალაქის განახლება
- `DELETE /api/cities/{id}` - ქალაქის წაშლა

- `GET /api/phone-types` - ტელეფონის ნომრების ტიპები
- `POST /api/phone-types` - ახალი ტიპის დამატება
- `PUT /api/phone-types/{id}` - ტიპის განახლება
- `DELETE /api/phone-types/{id}` - ტიპის წაშლა

- `GET /api/relation-types` - ურთიერთობის ტიპები
- `POST /api/relation-types` - ახალი ტიპის დამატება
- `PUT /api/relation-types/{id}` - ტიპის განახლება
- `DELETE /api/relation-types/{id}` - ტიპის წაშლა

### Reports
- `GET /api/reports/person-relations` - დაკავშირებული პირების რეპორტი

## ვალიდაციის წესები

### სახელი/გვარი
- სავალდებულო
- 2-50 სიმბოლო
- მხოლოდ ქართული ან ლათინური ასოები
- არ შეიძლება შეიცავდეს ორივე ანბანის ასოებს

### პირადი ნომერი
- სავალდებულო
- ზუსტად 11 ციფრი
- უნიკალური

### დაბადების თარიღი
- სავალდებულო
- მინიმუმ 18 წლის

### ტელეფონის ნომერი
- 4-50 სიმბოლო
- ტიპი: მობილური, ოფისის, სახლის

## ლოკალიზაცია

API მხარს უჭერს ქართულ და ინგლისურ ენებს Accept-Language header-ის მეშვეობით.

## შეცდომების დამუშავება

- ყველა შეცდომა ლოგირებულია
- მომხმარებელს ბრუნდება მარტივი შეცდომის მესიჯები
- შეცდომების კოდები სტანდარტულია (400, 404, 500, etc.)

## გაშვება

1. Database-ის შექმნა და migration-ების გაშვება
2. appsettings.json-ში connection string-ის კონფიგურაცია
3. `dotnet run` ბრძანების გაშვება

## ტესტირება

```bash
# Unit Tests
dotnet test tests/InterviewApi.UnitTests

# Integration Tests
dotnet test tests/InterviewApi.IntegrationTests
```

ეს არის სრული გეგმა დავალების შესასრულებლად Clean Architecture პრინციპების დაცვით.

## ტესტირების გეგმა (შემდგომი ფაზა)

### Unit Tests
- [ ] Domain Entities ტესტები
- [ ] Value Objects ტესტები
- [ ] Application Services ტესტები
- [ ] Validators ტესტები
- [ ] Repository ტესტები

### Integration Tests
- [ ] API Controllers ტესტები
- [ ] Database Integration ტესტები
- [ ] File Upload ტესტები

### Performance Tests
- [ ] ძებნის ფუნქციების ტესტები
- [ ] დიდი მონაცემების ტესტები
- [ ] Concurrent access ტესტები 