# Interview Project - პერსონების მართვის სისტემა

## 📋 პროექტის აღწერა

ეს არის **პერსონების მართვის სისტემა**, რომელიც შექმნილია Clean Architecture პრინციპების გამოყენებით. სისტემა საშუალებას იძლევა ფიზიკური პირების დამატება, რედაქტირება, წაშლა და ძებნა.

## 🏗️ არქიტექტურა

პროექტი აგებულია **Clean Architecture** პრინციპებზე და შედგება შემდეგი ფენებისგან:

```
Interview/
├── Interview.Api/              # API Layer (Controllers, Middleware)
├── Interview.Application/      # Application Layer (Commands, Queries, Handlers)
├── Interview.Domain/           # Domain Layer (Entities, Value Objects)
├── Interview.Infrastructure/   # Infrastructure Layer (Database, Repositories)
└── Interview.Tests/            # Test Projects
    ├── Interview.Tests.Unit/   # Unit Tests
    └── Interview.Tests.Integration/ # Integration Tests
```

დეტალური არქიტექტურული აღწერა შეგიძლიათ იხილოთ ArchitectureReadme.md ფაილში

### 📁 ფენების აღწერა

#### **Interview.Api** - API ფენა
- **Controllers**: HTTP endpoints
- **Middleware**: Global exception handling, Localization
- **Extensions**: Service configuration

#### **Interview.Application** - აპლიკაციის ფენა
- **Commands**: CQRS Commands (Create, Update, Delete)
- **Queries**: CQRS Queries (Get, Search, Reports)
- **Handlers**: Command/Query handlers
- **DTOs**: Data Transfer Objects

#### **Interview.Domain** - დომენის ფენა
- **Entities**: Person, PersonPhoneNumber, PersonRelation
- **Value Objects**: Name, PersonalNumber, BirthDate, Age
- **Dictionaries**: City, Gender, PhoneType, RelationType

#### **Interview.Infrastructure** - ინფრასტრუქტურის ფენა
- **Data**: Entity Framework DbContext, Migrations
- **Repositories**: Data access implementations
- **Services**: File storage, Localization

## 🚀 ფუნქციონალი

### 👤 პერსონების მართვა
- ✅ **შექმნა**: ახალი პერსონის დამატება
- ✅ **რედაქტირება**: არსებული პერსონის განახლება
- ✅ **წაშლა**: პერსონის წაშლა
- ✅ **ძებნა**: პერსონების ძებნა და ფილტრაცია
- ✅ **დეტალები**: პერსონის სრული ინფორმაციის ნახვა

### 📞 ტელეფონის ნომრები
- ✅ **დამატება**: მრავალი ტელეფონის ნომრის დამატება
- ✅ **რედაქტირება**: ნომრების განახლება
- ✅ **წაშლა**: ნომრების წაშლა

### 👥 დაკავშირებული პირები
- ✅ **დამატება**: პირების დაკავშირება
- ✅ **რედაქტირება**: ურთიერთობის განახლება
- ✅ **წაშლა**: ურთიერთობის წაშლა

### 🖼️ სურათების მართვა
- ✅ **ატვირთვა**: პროფილის სურათის ატვირთვა
- ✅ **მიღება**: სურათის ნახვა
- ✅ **წაშლა**: სურათის წაშლა

### 📊 რეპორტები
- ✅ **ურთიერთობების რეპორტი**: პერსონის ყველა ურთიერთობის ნახვა

### 🌍 ლოკალიზაცია
- ✅ **ქართული**: ka-GE
- ✅ **ინგლისური**: en-US
- ✅ **Accept-Language** header-ის მხარდაჭერა

## 🛠️ ტექნოლოგიები

### **Backend**
- **.NET 8**: ძირითადი ფრეიმვორკი
- **ASP.NET Core**: Web API
- **Entity Framework Core**: ORM
- **SQL Server**: მონაცემთა ბაზა
- **CQRS Pattern**: Commands/Queries separation
- **MediatR**: Command/Query handling

### **ტესტირება**
- **xUnit**: ტესტირების ფრეიმვორკი
- **FluentAssertions**: Assertions
- **Moq**: Mocking
- **AutoFixture**: Test data generation
- **In-Memory Database**: Integration tests

## 📦 ინსტალაცია და გაშვება

### **წინაპირობები**
- .NET 8 SDK
- SQL Server (ან LocalDB)
- Visual Studio 2022 / VS Code

### **1. პროექტის კლონირება**
```bash
git clone https://github.com/nmakharadze/InterviewAPI.git
cd Interview
```

### **2. დამოკიდებულებების აღდგენა**
```bash
dotnet restore
```

### **3. მიგრაციების გაშვება**
```bash
dotnet ef database update --project Interview.Infrastructure --startup-project Interview.Api
```

### **4. ბაზის seed მონაცემების შევსება**
```bash
# SQL Server-ში Documentations/DictionarySeedData.sql ფაილის გაშვება
# ან Visual Studio-ში ფაილის გახსნა და Execute
```
**შენიშვნა**: Documentations/DictionarySeedData.sql ფაილი შეიცავს საწყის მონაცემებს (ქალაქები, სქესი, ტელეფონის ტიპები, ურთიერთობის ტიპები), რომლებიც საჭიროა სისტემის სწორად მუშაობისთვის.

### **5. აპლიკაციის გაშვება**
```bash
dotnet run --project Interview.Api
```

### **6. ტესტების გაშვება**
```bash
# ყველა ტესტი
dotnet test

# მხოლოდ Unit Tests
dotnet test Interview.Tests.Unit

# მხოლოდ Integration Tests
dotnet test Interview.Tests.Integration
```

## 🌐 API Endpoints

### **Persons**
- `GET /api/persons` - ყველა პერსონის სია
- `GET /api/persons/{id}` - პერსონის დეტალები
- `POST /api/persons` - ახალი პერსონის შექმნა
- `PUT /api/persons/{id}` - პერსონის განახლება
- `DELETE /api/persons/{id}` - პერსონის წაშლა
- `GET /api/persons/search` - პერსონების ძებნა
- `POST /api/persons/advanced-search` - მოწინავე ძებნა

### **Phone Numbers**
- `POST /api/persons/{id}/phone-numbers` - ტელეფონის ნომრის დამატება
- `PUT /api/persons/{id}/phone-numbers/{phoneId}` - ნომრის განახლება
- `DELETE /api/persons/{id}/phone-numbers/{phoneId}` - ნომრის წაშლა

### **Relations**
- `POST /api/persons/{id}/relations` - ურთიერთობის დამატება
- `PUT /api/persons/{id}/relations/{relationId}` - ურთიერთობის განახლება
- `DELETE /api/persons/{id}/relations/{relationId}` - ურთიერთობის წაშლა

### **Images**
- `POST /api/persons/{id}/image` - სურათის ატვირთვა
- `GET /api/persons/{id}/image` - სურათის მიღება
- `DELETE /api/persons/{id}/image` - სურათის წაშლა

### **Reports**
- `GET /api/persons/{id}/relations-report` - ურთიერთობების რეპორტი

### **Dictionaries**
   
   დეტალური აღწერა იხილეთ DctionaryManualReadme.md ფაილში



## 🧪 ტესტირება

პროექტი შეიცავს ნაწილობრივ ტესტირებას:

### **Unit Tests** (15 ტესტი)
- **Value Objects**: Name, PersonalNumber, BirthDate ვალიდაცია

### **Integration Tests** (7 ტესტი)
- **API Controllers**: PersonController endpoints
- **Database Integration**: In-memory database ტესტირება

### **ტესტების გაშვება**
```bash
# ყველა ტესტი
dotnet test

# კონკრეტული პროექტი
dotnet test Interview.Tests.Unit
dotnet test Interview.Tests.Integration

# კონკრეტული კლასი
dotnet test --filter "FullyQualifiedName~NameTests"

# კონკრეტული ტესტი
dotnet test --filter "FullyQualifiedName~Should_Create_Valid_Name"
```

## 📚 დოკუმენტაცია

- [ArchitectureReadme.md](ArchitectureReadme.md) - დეტალური არქიტექტურის აღწერა
- [DictionaryManualReadme.md](DictionaryManualReadme.md) - ცნობარების სრული აღწერა
- [DictionarySeedData.sql](DictionarySeedData.sql) - ბაზის საწყისი მონაცემები

