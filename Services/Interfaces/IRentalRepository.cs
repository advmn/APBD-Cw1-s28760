using APBD_Cw1_s28760.Models;

namespace APBD_Cw1_s28760.Services.Interfaces;

public interface IRentalRepository
{
    void Add(Rental rental);
    IEnumerable<Rental> GetAll();
    Rental? GetById(Guid id);
}