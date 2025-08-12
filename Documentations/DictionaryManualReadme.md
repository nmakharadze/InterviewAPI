# Dictionary API Manual

## 📋 Overview

Dictionary კონტროლერი გამოიყენება ცნობარების სამართავად.
ის საშუალლებას იძლევა ერთი წერტილიდან მართო Dictionary სქემის ქვეშ შექმილი ცხრილები ვინაიდან მათ აქვთ ერთი სტრუქტურა.
სისტემა დინამიურად მუშაობს ყველა dictionary ცხრილთან ერთნაირად.

## 🚀 Available Endpoints

### Base URL
```
https://localhost:7166/api/dictionary
```

### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/dictionary/{tableName}` | ყველა ჩანაწერის მიღება |
| GET | `/api/dictionary/{tableName}/{id}` | კონკრეტული ჩანაწერის მიღება |
| POST | `/api/dictionary/{tableName}` | ახალი ჩანაწერის შექმნა |
| PUT | `/api/dictionary/{tableName}/{id}` | ჩანაწერის განახლება |
| DELETE | `/api/dictionary/{tableName}/{id}` | ჩანაწერის წაშლა |
| GET | `/api/dictionary/types` | ყველა ცნობარის ცხრილის დასახელება(თუ დაამატებ ახალ ცნოარს ის ბაზაშიც უნდა დააკონფიგრირო) |

## 📊 Available Dictionary Types

- **cities** - ქალაქები
- **genders** - სქესი 
- **phonetypes** - ტელეფონის ნომრების ტიპები
- **relationtypes** - ურთიერთობის ტიპები

## 🔧 Usage Examples

### 1. ყველა ქალაქის მიღება
```bash
GET /api/dictionary/cities
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "თბილისი",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z"
  },
  {
    "id": 2,
    "name": "ბათუმი",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z"
  }
]
```

### 2. ახალი ქალაქის დამატება
```bash
POST /api/dictionary/cities
Content-Type: application/json

{
  "name": "ქუთაისი"
}
```

**Response:**
```json
{
  "id": 3,
  "name": "ქუთაისი",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

### 3. ქალაქის განახლება
```bash
PUT /api/dictionary/cities/1
Content-Type: application/json

{
  "name": "თბილისი - დედაქალაქი"
}
```

### 4. ქალაქის წაშლა
```bash
DELETE /api/dictionary/cities/1
```

**Response:**
```json
true
```

### 5. ყველა dictionary სქემის ცხრილის წამოღება ბაზიდან
```bash
GET /api/dictionary/types
```

**Response:**
```json
[
  {
    "tableName": "cities",
    "entityTypeName": "City"
  },
  {
    "tableName": "genders", 
    "entityTypeName": "Gender"
  }
]
```

## 📝 Request/Response Formats

### Create/Update Request
```json
{
  "name": "string"
}
```

### Dictionary Response
```json
{
  "id": "number",
  "name": "string", 
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## ⚠️ Error Handling

### 400 Bad Request
```json
{
  "error": "არასწორი ცხრილი: invalidTable",
  "details": null,
  "timestamp": "2024-01-01T00:00:00Z"
}
```

### 400 Bad Request (Duplicate)
```json
{
  "error": "Dictionary ჩანაწერი სახელით 'თბილისი' უკვე არსებობს cities-ში",
  "details": null,
  "timestamp": "2024-01-01T00:00:00Z"
}
```

### 400 Bad Request (Not Found)
```json
{
  "error": "Dictionary ჩანაწერი ID-ით 999 ვერ მოიძებნა cities-ში",
  "details": null,
  "timestamp": "2024-01-01T00:00:00Z"
}
```

## 🔄 Testing with Swagger

1. გაუშვით პროექტი: `dotnet run --project Interview.Api`
2. გადადით: `https://localhost:7166/swagger`
3. იპოვეთ `DictionaryController` სექცია
4. ტესტირება შეგიძლიათ პირდაპირ Swagger UI-დან

## 🛠️ Technical Implementation

### ახალი Dictionary ტიპის დამატება

თუ საჭირო გახდება ახალი dictionary ტიპის დამატება, შეასრულეთ შემდეგი ნაბიჯები:

#### 1. Entity შექმნა
```csharp
// Interview.Domain/Entities/Dictionary/NewType.cs
public class NewType : DictionaryBase
{
    // დამატებითი properties თუ საჭიროა
}
```

#### 2. DbSet დამატება
```csharp
// Interview.Infrastructure/Data/InterviewDbContext.cs
public DbSet<NewType> NewTypes { get; set; }
```

#### 3. Configuration შექმნა
```csharp
// Interview.Infrastructure/Data/Configurations/Dictionary/NewTypeConfiguration.cs
public class NewTypeConfiguration : DictionaryBaseConfiguration<NewType>
{
    public override void Configure(EntityTypeBuilder<NewType> builder)
    {
        base.Configure(builder);
        builder.ToTable("NewTypes", "Dictionary");
    }
}
```

#### 4. Migration შექმნა
```bash
dotnet ef migrations add AddNewTypes
dotnet ef database update
```

#### 5. Database-ში ჩანაწერის დამატება
```sql
INSERT INTO Dictionary.DictionaryNames (TableName, EntityTypeName, CreatedAt, UpdatedAt)
VALUES ('newtypes', 'NewType', GETUTCDATE(), GETUTCDATE())
```

### ავტომატური ფუნქციონალი

ახალი dictionary ტიპის დამატების შემდეგ, API ავტომატურად:
- ✅ პოულობს entity type-ს reflection-ით
- ✅ პოულობს DbSet property-ს reflection-ით  
- ✅ მუშაობს ყველა CRUD ოპერაციასთან
- ✅ ვალიდაციას ახორციელებს
- ✅ error handling-ს უზრუნველყოფს

### API Endpoints ავტომატურად ხელმისაწვდომი გახდება

```
GET    /api/dictionary/newtypes
GET    /api/dictionary/newtypes/{id}
POST   /api/dictionary/newtypes
PUT    /api/dictionary/newtypes/{id}
DELETE /api/dictionary/newtypes/{id}
```

## 📋 Summary

Dictionary API უზრუნველყოფს:
- ✅ ერთი კონტროლერი ყველა dictionary ტიპისთვის
- ✅ დინამიური reflection-based სისტემა
- ✅ ყველა CRUD ოპერაცია
- ✅ ვალიდაცია და error handling
- ✅ ახალი ტიპების მარტივი დამატება
- ✅ Swagger დოკუმენტაცია

---

**ვერსია**: 1.0.0  
**ბოლო განახლება**: 2024
