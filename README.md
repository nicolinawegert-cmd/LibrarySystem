# LibrarySystem

Bibliotekssystem byggt i C# och .NET med Entity Framework Core, SQLite och Blazor Server.

Lösningen vidareutvecklar bibliotekssystemet från Del 1 med databashantering, webbgränssnitt och tester för datalagret.

## Projektöversikt

- `LibrarySystem.Core` innehåller modeller och domänlogik
- `LibrarySystem.Data` innehåller `LibraryContext`, migrationer, repositories och tjänster för dataåtkomst
- `LibrarySystem.Web` innehåller Blazor-webbgränssnittet
- `LibrarySystem.App` innehåller konsolapplikationen från tidigare del
- `LibrarySystem.Tests` innehåller tester för grundfunktioner och Blazor-komponenten `BookCard`
- `LibrarySystem.Data.Tests` innehåller tester för repository och integrationsflöden i datalagret

## Funktioner

Webbapplikationen innehåller följande funktioner:

- startsida med snabbstatistik för böcker, medlemmar och aktiva lån
- boklista med sökning, sortering, kortvy och tabellvy
- skapa, redigera och ta bort böcker
- bokdetaljer med lånehistorik samt låna och returnera bok
- medlemslista med antal aktiva lån per medlem
- skapa, redigera och ta bort medlemmar
- medlemsdetaljer med aktuella lån
- formulär för att skapa lån
- lista över aktiva lån med markering av försenade lån
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

```text
Book    1 ----< many Loan
Member  1 ----< many Loan
Loan many >---- 1 Book
Loan many >---- 1 Member
```

## Databasschema

Tabeller i databasen:

- `Books`
- `Members`
- `Loans`
- `__EFMigrationsHistory`

Förenklad struktur:

```text
Books   (Id, ISBN, Title, Author, PublishedYear, IsAvailable)
Members (Id, MemberId, Name, Email, MemberSince)
Loans   (Id, BookId, MemberId, LoanDate, DueDate, ReturnDate)

Books.Id   ----< Loans.BookId
Members.Id ----< Loans.MemberId
```

## Blazor-sidor

Webbprojektet innehåller följande sidor:

- `/` startsida
- `/books` boklista
- `/books/{id}` bokdetaljer
- `/members` medlemslista
- `/members/{id}` medlemsdetaljer
- `/loans` utlåning

## Så kör du projektet

Utgå från lösningens rotmapp.

### 1. Bygg lösningen

```bash
dotnet build LibrarySystem.sln
```

### 2. Uppdatera databasen

```bash
dotnet ef database update --project LibrarySystem.Data --startup-project LibrarySystem.Web
```

`LibrarySystem.Web` är konfigurerat att använda en gemensam SQLite-databas i `LibrarySystem.App/library.db`.

### 3. Starta webbappen

```bash
dotnet run --project LibrarySystem.Web
```

### 4. Starta konsolappen

```bash
dotnet run --project LibrarySystem.App
```

Konsolappen är valfri för Del 2 men finns kvar i lösningen.

### 5. Kör tester

Alla tester:

```bash
dotnet test LibrarySystem.sln
```

Endast datalager-tester:

```bash
dotnet test LibrarySystem.Data.Tests/LibrarySystem.Data.Tests.csproj
```

Endast grundtester och komponenttest:

```bash
dotnet test LibrarySystem.Tests/LibrarySystem.Tests.csproj
```

## Tester

Lösningen innehåller två testprojekt:

- `LibrarySystem.Tests` för domänlogik och Blazor-komponenten `BookCard`
- `LibrarySystem.Data.Tests` för `BookRepository`, CRUD-operationer och integrationsflöden i `LoanService`

Testerna täcker bland annat:

- skapa, hämta, söka, uppdatera och ta bort böcker
- utlåning och återlämning
- felhantering när bok, medlem eller lån saknas
- case-insensitive sökning i bokrepository
- rendering av `BookCard` med bUnit

## Teknik

- .NET 9
- Blazor Server via .NET Blazor Web App
- Entity Framework Core 9
- SQLite
- xUnit
- bUnit

## Screenshots

Inför inlämning bör README kompletteras med screenshots från:

- startsidan
- boklistan
- bokdetaljer
- medlemmar
- utlåning

Exempel på struktur:

```text
screenshots/
├── home.png
├── books.png
├── book-details.png
├── members.png
└── loans.png
```

## Kommentar

Huvudfokus i Del 2 är integrationen mellan Entity Framework Core, datalagret och Blazor-gränssnittet. Konsolprojektet från Del 1 finns kvar som separat körbar del i lösningen.
