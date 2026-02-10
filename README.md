# Library System – Objektorienterad programmering i C#

## Beskrivning

Detta projekt är ett **konsolbaserat bibliotekssystem** utvecklat i C# som en del av kursen **Programmering Fördjupning**.

Syftet är att demonstrera förståelse för:

- Klasser och objekt  
- Inkapsling och properties  
- Komposition  
- Polymorfism och interfaces  
- Grundläggande algoritmer  
- Enhetstestning med xUnit  

Systemet hanterar:

- Böcker  
- Medlemmar  
- Utlåning  
- Sökning, sortering och statistik  

---

## Projektstruktur

Lösningen består av tre projekt:

### **LibrarySystem.Core**
Innehåller all **domänlogik**.

**Models**
- Book  
- Member  
- Loan  

**Services**
- Library  
- BookCatalog  
- MemberRegistry  
- LoanManager  

**Abstractions**
- ISearchable  

---

### **LibrarySystem.Tests**
xUnit-testprojekt som verifierar:

- Domänlogik  
- Sökfunktionalitet  
- Statistik  
- Lånelogik  

✔ Innehåller **fler än 10 tester** (uppfyller minimikravet)

---

### **LibrarySystem.App**
Konsolapplikation som fungerar som **startpunkt** för systemet.

---

## Uppfyllda krav enligt uppgift

### Del 1 – Klasser och inkapsling

**Book**
- ISBN kan endast sättas vid skapande  
- Tillgänglighet hanteras via metoder  
- `GetInfo()` returnerar formaterad bokinformation  

**Member**
- Inkapslad lista över lånade böcker  
- `GetInfo()` visar medlemsinformation  

**Loan**
- Beräknade properties:
  - `IsReturned`
  - `IsOverdue`

---

### Del 2 – Komposition

Vald lösning: **Alternativ B – Komposition**

`Library` innehåller:

- BookCatalog  
- MemberRegistry  
- LoanManager  

---

### Del 3 – Interface och polymorfism

```csharp
public interface ISearchable
{
    bool Matches(string searchTerm);
}
```

Implementeras av:

- Book

Ger en enhetlig sökfunktion i systemet.

---

## Del 4 – Algoritmer

### Sökning
- Sök efter titel, författare eller ISBN.

### Sortering
- Alfabetisk sortering av titel.
- Sortering efter utgivningsår.

### Statistik
- Totalt antal böcker.
- Antal utlånade böcker.
- Mest aktiva låntagaren.

---

## Del 5 – Enhetstester (xUnit)

Projektet innehåller tester för:

- Book
- Loan
- ISearchable
- Statistik och algoritmer

✔ Minst **10 tester**  
✔ Följer **AAA-mönstret (Arrange, Act, Assert)**  
✔ Täcker både **happy path** och **edge cases**

---

## Så kör du projektet

### 1. Klona repository

```bash
git clone <repo-länk>
cd LibrarySystem
```

### 2. Bygg lösningen

```bash
dotnet build
```

### 3. Kör tester

```bash
dotnet test
```

### 4. Starta konsolapplikationen

```bash
dotnet run --project LibrarySystem.App
```

---

## Kodkvalitet

Projektet följer:

- Objektorienterade principer  
- Inkapsling och separation av ansvar  
- Clean Code-namngivning  
- Testdriven utveckling (TDD)  
- Read-only exponering av interna samlingar  

---

## Vidareutveckling (Del 2)

Planerad fortsättning:

- Entity Framework Core  
- Databaslagring  
- Blazor-gränssnitt  

