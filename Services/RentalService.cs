using APBD_Cw1_s28760.Enums;
using APBD_Cw1_s28760.Exceptions;
using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class RentalService
{
    private readonly IEquipmentRepository _equipmentRepo;
    private readonly IUserRepository _userRepo;
    private readonly IRentalRepository _rentalRepo;
    private readonly IUserLimitPolicy _limitPolicy;
    private readonly IPenaltyPolicy _penaltyPolicy;

    public RentalService(
        IEquipmentRepository equipmentRepo,
        IUserRepository userRepo,
        IRentalRepository rentalRepo,
        IUserLimitPolicy limitPolicy,
        IPenaltyPolicy penaltyPolicy)
    {
        _equipmentRepo = equipmentRepo;
        _userRepo = userRepo;
        _rentalRepo = rentalRepo;
        _limitPolicy = limitPolicy;
        _penaltyPolicy = penaltyPolicy;
    }

    public Rental Rent(Guid userId, Guid equipmentId, int days)
    {
        var user = _userRepo.GetById(userId) ?? throw new NotFoundException("User not found");
        var equipment = _equipmentRepo.GetById(equipmentId) ?? throw new NotFoundException("Equipment not found");

        if (equipment.Status != EquipmentStatus.Available)
            throw new BusinessRuleException("Equipment is not available");

        var activeCount = _rentalRepo.GetAll()
            .Count(r => r.User.Id == user.Id && !r.IsReturned);

        var maxAllowed = _limitPolicy.GetMaxActiveRentals(user);
        if (activeCount >= maxAllowed)
            throw new BusinessRuleException("User exceeded active rentals limit");

        var start = DateTime.Now;
        var due = start.AddDays(days);

        var rental = new Rental(user, equipment, start, due);
        _rentalRepo.Add(rental);
        equipment.MarkRented();

        return rental;
    }

    public void Return(Guid rentalId, DateTime returnDate)
    {
        var rental = _rentalRepo.GetById(rentalId) ?? throw new NotFoundException("Rental not found");
        if (rental.IsReturned)
            throw new BusinessRuleException("Rental already closed");

        var penalty = _penaltyPolicy.CalculatePenalty(rental, returnDate);
        rental.Close(returnDate, penalty);
        rental.Equipment.MarkAvailable();
    }

    public IEnumerable<Rental> GetActiveRentalsForUser(Guid userId)
        => _rentalRepo.GetAll().Where(r => r.User.Id == userId && !r.IsReturned);

    public IEnumerable<Rental> GetOverdueRentals()
        => _rentalRepo.GetAll().Where(r => r.IsOverdue);
}