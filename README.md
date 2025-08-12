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
├── Interview.Tests/            # Test Projects
│   ├── Interview.Tests.Unit/   # Unit Tests
│   └── Interview.Tests.Integration/ # Integration Tests
└── Documentations/             # Project Documentation
    ├── DetailedDocumentation.md # Detailed documentation
    ├── ArchitectureReadme.md  # Detailed architecture
    ├── DictionaryManualReadme.md # Dictionary manual
    └── DictionarySeedData.sql # Database seed data
```

## 📚 დოკუმენტაცია

პროექტის სრული დოკუმენტაცია მოთავსებულია **Documentations** ფოლდერში:

- **[Documentations/DetailedDocumentation.md](Documentations/DetailedDocumentation.md)** - დეტალური დოკუმენტაცია (ფუნქციონალი, API endpoints, ინსტალაცია)
- **[Documentations/ArchitectureReadme.md](Documentations/ArchitectureReadme.md)** - პროექტის სქემა და დეტალური არქიტექტურული აღწერა
- **[Documentations/DictionaryManualReadme.md](Documentations/DictionaryManualReadme.md)** - ცნობარების მართვის სახელმძღვანელო
- **[Documentations/DictionarySeedData.sql](Documentations/DictionarySeedData.sql)** - ბაზის საწყისი მონაცემები

## 🚀 პროექტის გაშვებისთვის საჭირო საფეხურები

### **წინაპირობები**
- .NET 8 SDK
- SQL Server (ან LocalDB)
- Visual Studio 2022 / VS Code

### **ინსტალაცია**
```bash
# 1. პროექტის კლონირება
git clone https://github.com/nmakharadze/InterviewAPI.git
cd Interview

# 2. პაკეტების აღდგენა
dotnet restore

# 3. მიგრაციების გაშვება
dotnet ef database update --project Interview.Infrastructure --startup-project Interview.Api

# 4. ბაზის seed მონაცემების შევსება
# SQL Server-ში Documentations/DictionarySeedData.sql ფაილის გაშვება

# 5. აპლიკაციის გაშვება
dotnet run --project Interview.Api
```

დეტალური ინსტრუქციებისთვის იხილეთ **[Documentations/DetailedDocumentation.md](Documentations/DetailedDocumentation.md)**.

## 👨‍💻 ავტორი

**ნინო მახარაძე**
- Email: nmakharadze13@gmail.com
- GitHub: [@nmakharadze](https://github.com/nmakharadze)

---
