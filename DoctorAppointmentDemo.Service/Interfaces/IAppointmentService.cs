using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces
{
    public interface IAppointmentService
    {
        Appointment Create(Appointment doctor);

        IEnumerable<Appointment> GetAll();

        Appointment? Get(int id);

        bool Delete(int id);

        Appointment Update(int id, Appointment doctor);
    }
}
