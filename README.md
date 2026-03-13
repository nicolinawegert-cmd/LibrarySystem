# LibrarySystem

Bibliotekssystem byggt i C# och .NET med fokus pa objektorientering, Entity Framework Core och Blazor.

Projektet ar uppdelat i flera delar:

- `LibrarySystem.Core` innehaller modeller och domanlogik
- `LibrarySystem.Data` innehaller `LibraryContext`, migrationer och dataatkomst
- `LibrarySystem.Web` ar Blazor-webbgranssnittet
- `LibrarySystem.App` ar konsolprojektet fran tidigare del
- `LibrarySystem.Tests` innehaller tester for Del 1
- `LibrarySystem.Data.Tests` innehaller tester for Del 2

## Funktioner

Webbapplikationen innehaller bland annat:

- startsida med snabbstatistik
- boklista med sokning, sortering och kortvy
- bokdetaljer med lanehistorik
- skapa och ta bort bok
- medlemslista och registrering av ny medlem
- skapa lan och returnera lan
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

En bok kan ha flera lan via relationen till `Loan`.

### Member

- `Id`
- `MemberId`
- `Name`
- `Email`
- `MemberSince`

En medlem kan ha flera lan via relationen till `Loan`.

### Loan

- `Id`
- `BookId`
- `MemberId`
- `LoanDate`
- `DueDate`
- `ReturnDate`

Varje lan kopplas till exakt en bok och exakt en medlem.

### Relationer

- `Book 1 -> many Loan`
- `Member 1 -> many Loan`
- `Loan many -> 1 Book`
- `Loan many -> 1 Member`

## Databasschema

Tabellstrukturen i databasen bestar av:

- `Books`
- `Members`
- `Loans`
- `__EFMigrationsHistory`

Forhallandet mellan tabellerna:

```text
Books (Id) ----< Loans (BookId)
Members (Id) --< Loans (MemberId)
```

## Sa kor du projektet

Utga fran losningens rotmapp.

### 1. Bygg losningen

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

### 4. Oppna i webblasaren

Standardadress ar normalt:

```text
https://localhost:xxxx
```

eller

```text
http://localhost:xxxx
```

### 5. Kor tester

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
- sokning och hamtning via repository
- integrationsfloden i `LoanService`
- utlanning och returnering av bocker

Projektet innehaller minst 10 nya tester for Del 2 och passerar lokalt.

## Blazor-sidor

Foljande sidor finns i webbprojektet:

- `/` startsida med statistik
- `/books` boklista
- `/books/{id}` bokdetaljer
- `/members` medlemslista
- `/loans` hantering av lan

## Screenshots

Lagg in screenshots har innan inlamning:

- Startsida
- Boklista
- Bokdetaljer
- Medlemmar
- Utlanning

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
- xUnit for enhetstester

## Kommentar

Konsolprojektet fran tidigare del finns kvar i losningen, men huvudfokus i Del 2 ar datalagret och webbgranssnittet.
