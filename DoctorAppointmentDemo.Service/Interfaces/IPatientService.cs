using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces
{
    public interface IPatientService
    {
        Patient Create(Patient doctor);

        IEnumerable<Patient> GetAll();

        Patient? Get(int id);

        bool Delete(int id);

        Patient Update(int id, Patient doctor);
    }
}
