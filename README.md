# LibrarySystem

Bibliotekssystem byggt i C# och .NET med fokus på objektorientering, Entity Framework Core och Blazor.

Projektet är uppdelat i flera delar:

- `LibrarySystem.Core` innehåller modeller och domänlogik
- `LibrarySystem.Data` innehåller `LibraryContext`, migrationer och dataåtkomst
- `LibrarySystem.Web` är Blazor-webbgränssnittet
- `LibrarySystem.App` är konsolprojektet från tidigare del
- `LibrarySystem.Tests` innehåller tester för Del 1
- `LibrarySystem.Data.Tests` innehåller tester för Del 2

## Funktioner

Webbapplikationen innehåller bland annat:

- startsida med snabbstatistik
- boklista med sökning, sortering och kortvy
- bokdetaljer med lånehistorik
- skapa och ta bort bok
- medlemslista och registrering av ny medlem
- skapa lån och returnera lån
- datalagring med SQLite via Entity Framework Core

## Projektstruktur

```text
LibrarySystem/
├── LibrarySystem.Core/
├── LibrarySystem.Data/
├── LibrarySystem.Web/
├── LibrarySystem.App/
├── LibrarySystem.Tests/
└── LibrarySystem.Data.Tests/
```

## Databasmodell

Databasen hanteras av Entity Framework Core med SQLite.

### Book

- `Id`
- `ISBN`
- `Title`
- `Author`
- `PublishedYear`
- `IsAvailable`

En bok kan ha flera lån via relationen till `Loan`.

### Member

- `Id`
- `MemberId`
- `Name`
- `Email`
- `MemberSince`

En medlem kan ha flera lån via relationen till `Loan`.

### Loan

- `Id`
- `BookId`
- `MemberId`
- `LoanDate`
- `DueDate`
- `ReturnDate`

Varje lån kopplas till exakt en bok och exakt en medlem.

### Relationer

- `Book 1 -> many Loan`
- `Member 1 -> many Loan`
- `Loan many -> 1 Book`
- `Loan many -> 1 Member`

## Databasschema

Tabellstrukturen i databasen består av:

- `Books`
- `Members`
- `Loans`
- `__EFMigrationsHistory`

Förhållandet mellan tabellerna:

```text
Books (Id) ----< Loans (BookId)
Members (Id) --< Loans (MemberId)
```

## Så kör du projektet

Utgå från lösningens rotmapp.

### 1. Bygg lösningen

```bash
dotnet build
```

### 2. Uppdatera databasen med migrationer

```bash
dotnet ef database update --project LibrarySystem.Data --startup-project LibrarySystem.Web
```

### 3. Starta webbappen

```bash
dotnet run --project LibrarySystem.Web
```

### 4. Öppna i webbläsaren

Standardadress är normalt:

```text
https://localhost:xxxx
```

eller

```text
http://localhost:xxxx
```

### 5. Kör tester

Alla tester:

```bash
dotnet test
```

Endast Del 2-tester:

```bash
dotnet test LibrarySystem.Data.Tests
```

## Del 2-tester

Del 2-testerna finns i projektet `LibrarySystem.Data.Tests`.

De testar bland annat:

- `BookRepository`
- CRUD-operationer mot datalagret
- sökning och hämtning via repository
- integrationsflöden i `LoanService`
- utlåning och returnering av böcker

Projektet innehåller minst 10 nya tester för Del 2 och passerar lokalt.

## Blazor-sidor

Följande sidor finns i webbprojektet:

- `/` startsida med statistik
- `/books` boklista
- `/books/{id}` bokdetaljer
- `/members` medlemslista
- `/loans` hantering av lån

## Screenshots

Lägg in screenshots här innan inlämning:

- Startsida
- Boklista
- Bokdetaljer
- Medlemmar
- Utlåning

Exempel:

```text
/screenshots/home.png
/screenshots/books.png
/screenshots/book-details.png
/screenshots/members.png
/screenshots/loans.png
```

## Teknisk sammanfattning

- .NET 9
- Blazor Server via .NET Blazor Web App
- Entity Framework Core
- SQLite
- xUnit för enhetstester

## Kommentar

Konsolprojektet från tidigare del finns kvar i lösningen, men huvudfokus i Del 2 är datalagret och webbgränssnittet.
