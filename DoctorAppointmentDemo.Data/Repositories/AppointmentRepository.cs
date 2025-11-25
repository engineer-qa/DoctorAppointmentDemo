using DoctorAppointmentDemo.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public AppointmentRepository()
        {
            dynamic result = ReadFromAppSettings();

            Path = result.Database.Appointments.Path;
            LastId = result.Database.Appointments.LastId;
        }

        public override void ShowInfo(Appointment appointment)
        {
            string[] info =
            {
                $"Appointment patient:{appointment.Patient}\n",
                $"Appointment doctor:{appointment.Doctor}\n",
                $"Appointment description:{appointment.Description}\n"
            };
            var message = string.Concat(info);
            Console.WriteLine(message);
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Appointments.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsJsonPath, result.ToString());
        }
    }
}
