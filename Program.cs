using APBD_Cw1_s28760.Enums;
using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services;

namespace APBD_Cw1_s28760;

internal class Program

{
    private static void Main(string[] args)
    {
        var equipmentRepo = new InMemoryEquipmentRepository();
        var userRepo = new InMemoryUserRepository();
        var rentalRepo = new InMemoryRentalRepository();
        var limitPolicy = new SimpleUserLimitPolicy();
        var penaltyPolicy = new SimplePenaltyPolicy(dailyRate: 10m);

        var rentalService = new RentalService(
            equipmentRepo, userRepo, rentalRepo, limitPolicy, penaltyPolicy);
        
        var laptop1 = new Laptop("Dell XPS", "i7", 16);
        var projector1 = new Projector("Epson Pro", 3000, "1920x1080");
        var camera1 = new Camera("Sony A7", "FullFrame", true);

        equipmentRepo.Add(laptop1);
        equipmentRepo.Add(projector1);
        equipmentRepo.Add(camera1);
        
        var student = new Student("Jan", "Kowalski", "s12345");
        var employee = new Employee("Anna", "Nowak", "IT");

        userRepo.Add(student);
        userRepo.Add(employee);

        Console.WriteLine("=== Sprzęt w systemie ===");
        foreach (var e in equipmentRepo.GetAll())
            Console.WriteLine(e);

        Console.WriteLine("\n=== Użytkownicy ===");
        foreach (var u in userRepo.GetAll())
            Console.WriteLine(u);
        
        Console.WriteLine("\n=== Poprawne wypożyczenie ===");
        var rental1 = rentalService.Rent(student.Id, laptop1.Id, days: 7);
        Console.WriteLine($"Wypożyczono: {rental1}");
        
        Console.WriteLine("\n=== Próba wypożyczenia niedostępnego sprzętu ===");
        try
        {
            rentalService.Rent(employee.Id, laptop1.Id, days: 3);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oczekiwany błąd: {ex.Message}");
        }
        
        Console.WriteLine("\n=== Zwrot w terminie ===");
        rentalService.Return(rental1.Id, rental1.DueDate);
        Console.WriteLine($"Po zwrocie: {rental1}");
        
        Console.WriteLine("\n=== Wypożyczenie i opóźniony zwrot ===");
        var rental2 = rentalService.Rent(student.Id, projector1.Id, days: 3);
        Console.WriteLine($"Wypożyczono: {rental2}");

        var lateReturnDate = rental2.DueDate.AddDays(2);
        rentalService.Return(rental2.Id, lateReturnDate);
        Console.WriteLine($"Po zwrocie: {rental2}");
        
        Console.WriteLine("\n=== Cały sprzęt ze statusem ===");
        foreach (var e in equipmentRepo.GetAll())
            Console.WriteLine(e);
        
        Console.WriteLine("\n=== Sprzęt dostępny ===");
        foreach (var e in equipmentRepo.GetAll().Where(x => x.Status == EquipmentStatus.Available))
            Console.WriteLine(e);
        
        Console.WriteLine("\n=== Oznaczenie kamery jako niedostępnej ===");
        camera1.MarkUnavailable();
        Console.WriteLine(camera1);
        
        Console.WriteLine("\n=== Aktywne wypożyczenia studenta ===");
        foreach (var r in rentalService.GetActiveRentalsForUser(student.Id))
            Console.WriteLine(r);
        
        Console.WriteLine("\n=== Przeterminowane wypożyczenia ===");
        foreach (var r in rentalService.GetOverdueRentals())
            Console.WriteLine(r);
        
        Console.WriteLine("\n=== Raport końcowy ===");
        var allRentals = rentalRepo.GetAll().ToList();
        var active = allRentals.Count(r => !r.IsReturned);
        var closed = allRentals.Count(r => r.IsReturned);
        var totalPenalty = allRentals.Sum(r => r.Penalty);

        Console.WriteLine($"Liczba sprzętów: {equipmentRepo.GetAll().Count()}");
        Console.WriteLine($"Liczba użytkowników: {userRepo.GetAll().Count()}");
        Console.WriteLine($"Aktywne wypożyczenia: {active}");
        Console.WriteLine($"Zamknięte wypożyczenia: {closed}");
        Console.WriteLine($"Suma naliczonych kar: {totalPenalty:C}");

        Console.WriteLine("\nKoniec scenariusza. Enter, aby zakończyć.");
        Console.ReadLine();
    }
}