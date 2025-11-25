using DoctorAppointmentDemo.Service.Interfaces;
using DoctorAppointmentDemo.Service.Services;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("Выберите пункт меню:\n");

                foreach (Domain.Enums.MenuTypes value in Enum.GetValues(typeof(Domain.Enums.MenuTypes)))
                {
                    Console.WriteLine($"{(int)value} — {value}");
                }

                var input = Console.ReadLine();

                if (!TryParseMenuSelection<Domain.Enums.MenuTypes>(input, out int selectedOperation, out string error))
                {
                    Console.Clear();
                    Console.WriteLine(error);
                    continue;
                }

                if (selectedOperation == 1)
                {
                    while (true)
                    {
                        Console.WriteLine("Выберите действие с докторами:\n");

                        foreach (Domain.Enums.DoctorMenuTypes value in Enum.GetValues(typeof(Domain.Enums.DoctorMenuTypes)))
                        {
                            Console.WriteLine($"{(int)value} — {value}");
                        }

                        var docInput = Console.ReadLine();

                        if (!TryParseMenuSelection<Domain.Enums.DoctorMenuTypes>(docInput,
                                out int selectedDocOperation,
                                out string docError))
                        {
                            Console.Clear();
                            Console.WriteLine(docError);
                            continue;
                        }


                        switch (selectedDocOperation)
                        {
                            case (int)Domain.Enums.DoctorMenuTypes.CreateDoctor:
                                HandleCreateDoctor();
                                break;
                            case (int)Domain.Enums.DoctorMenuTypes.GetDoctor:
                                HandleGetDoctor();
                                break;
                            case (int)Domain.Enums.DoctorMenuTypes.GetAllDoctors:
                                HandleListDoctors();
                                break;
                            case (int)Domain.Enums.DoctorMenuTypes.UpdateDoctor:
                                HandleUpdateDoctor();
                                break;
                            case (int)Domain.Enums.DoctorMenuTypes.DeleteDoctor:
                                HandleDeleteDoctor();
                                break;
                            default:
                                Console.WriteLine("Неизвестная операция с докторами.");
                                break;
                        }

                        break;
                    }
                }
                if (selectedOperation == 2)
                {
                    while (true)
                    {
                        Console.WriteLine("Выберите действие с пациентами:\n");

                        foreach (Domain.Enums.PatientMenuTypes value in Enum.GetValues(typeof(Domain.Enums.PatientMenuTypes)))
                        {
                            Console.WriteLine($"{(int)value} — {value}");
                        }

                        var patInput = Console.ReadLine();

                        if (!TryParseMenuSelection<Domain.Enums.PatientMenuTypes>(patInput,
                                out int selectedPatOperation,
                                out string patError))
                        {
                            Console.Clear();
                            Console.WriteLine(patError);
                            continue;
                        }

                        switch (selectedPatOperation)
                        {
                            case (int)Domain.Enums.PatientMenuTypes.CreatePatient:
                                HandleCreatePatient();
                                break;
                            case (int)Domain.Enums.PatientMenuTypes.GetPatient:
                                HandleGetPatient();
                                break;
                            case (int)Domain.Enums.PatientMenuTypes.GetAllPatients:
                                HandleListPatients();
                                break;
                            case (int)Domain.Enums.PatientMenuTypes.UpdatePatient:
                                HandleUpdatePatient();
                                break;
                            case (int)Domain.Enums.PatientMenuTypes.DeletePatient:
                                HandleDeletePatient();
                                break;
                            default:
                                Console.WriteLine("Неизвестная операция с пациентами.");
                                break;
                        }

                        break;
                    }
                }
                if (selectedOperation == 3)
                {
                    while (true)
                    {
                        Console.WriteLine("Выберите действие с записями приёма:\n");

                        foreach (Domain.Enums.AppointmentMenuTypes value in Enum.GetValues(typeof(Domain.Enums.AppointmentMenuTypes)))
                        {
                            Console.WriteLine($"{(int)value} — {value}");
                        }

                        var appInput = Console.ReadLine();

                        if (!TryParseMenuSelection<Domain.Enums.AppointmentMenuTypes>(appInput,
                                out int selectedAppOperation,
                                out string appError))
                        {
                            Console.Clear();
                            Console.WriteLine(appError);
                            continue;
                        }

                        switch (selectedAppOperation)
                        {
                            case (int)Domain.Enums.AppointmentMenuTypes.CreateAppointment:
                                HandleCreateAppointment();
                                break;
                            case (int)Domain.Enums.AppointmentMenuTypes.GetAppointment:
                                HandleGetAppointment();
                                break;
                            case (int)Domain.Enums.AppointmentMenuTypes.GetAllAppointments:
                                HandleListAppointments();
                                break;
                            case (int)Domain.Enums.AppointmentMenuTypes.UpdateAppointment:
                                HandleUpdateAppointment();
                                break;
                            case (int)Domain.Enums.AppointmentMenuTypes.DeleteAppointment:
                                HandleDeleteAppointment();
                                break;
                            default:
                                Console.WriteLine("Неизвестная операция с записями приёма.");
                                break;
                        }

                        break;
                    }
                }
            }
        }



        private bool TryParseMenuSelection<TEnum>(string? input, out int selected, out string errorMessage)
    where TEnum : Enum
        {
            selected = 0;
            errorMessage = string.Empty;

            if (!int.TryParse(input, out var number))
            {
                errorMessage = $"Вводить можно только цифры. Вы ввели: {input}";
                return false;
            }

            int min = 1;
            int max = Enum.GetValues(typeof(TEnum)).Length;

            if (number < min || number > max)
            {
                errorMessage = $"Вводить можно только цифры от {min} до {max}";
                return false;
            }

            selected = number;
            return true;
        }

        // Extracted doctor handlers
        private void HandleCreateDoctor()
        {
            Console.WriteLine("Для добавления доктора, введите имя:\n");
            var name = Console.ReadLine();

            Console.WriteLine("Введите фамилию:\n");
            var surname = Console.ReadLine();

            Console.WriteLine("Введите опыт работы (в годах):\n");
            var experienceInput = Console.ReadLine();
            if (!byte.TryParse(experienceInput, out var experience))
            {
                Console.Clear();
                Console.WriteLine($"Вводить можно только цифры. Вы ввели: {experienceInput}");
                return;
            }

            Console.WriteLine($"Введите специализацию:\n");
            foreach (Domain.Enums.DoctorTypes value in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)value} — {value}");
            }
            var docTypeInput = Console.ReadLine();
            int maxDocTypeValue = Enum.GetValues(typeof(Domain.Enums.DoctorTypes)).Length;
            if (!int.TryParse(docTypeInput, out var docTypeNumber) ||
                docTypeNumber < 1 || docTypeNumber > maxDocTypeValue)
            {
                Console.Clear();
                Console.WriteLine($"Вводить можно только цифры от 1 до {maxDocTypeValue}. Вы ввели: {docTypeInput}");
                return;
            }

            var newDoctor = new Doctor
            {
                Name = name,
                Surname = surname,
                Experience = experience,
                DoctorType = (Domain.Enums.DoctorTypes)docTypeNumber
            };
            _doctorService.Create(newDoctor);
            Console.WriteLine($"Доктор {name} {surname} успешно создан");
        }

        private void HandleGetDoctor()
        {
            Console.WriteLine("Введите ID доктора для получения информации:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var doctorId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var doctor = _doctorService.Get(doctorId);
            if (doctor == null)
            {
                Console.Clear();
                Console.WriteLine($"Доктор с ID {doctorId} не найден.");
                return;
            }
            Console.WriteLine($"Информация о докторе:\nID: {doctor.Id}\nИмя: {doctor.Name}\nФамилия: {doctor.Surname}\nСпециализация: {doctor.DoctorType}\nОпыт: {doctor.Experience} лет\n");
        }

        private void HandleListDoctors()
        {
            var doctors = _doctorService.GetAll();
            Console.WriteLine("Список всех докторов:\n");
            foreach (var doc in doctors)
            {
                Console.WriteLine($"{doc.Id}: {doc.Name} {doc.Surname}, Специализация: {doc.DoctorType}, Опыт: {doc.Experience} лет");
            }
        }

        private void HandleUpdateDoctor()
        {
            Console.WriteLine("Введите ID доктора для обновления информации:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var doctorId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var doctor = _doctorService.Get(doctorId);
            if (doctor == null)
            {
                Console.Clear();
                Console.WriteLine($"Доктор с ID {doctorId} не найден.");
                return;
            }
            Console.WriteLine("Введите новое имя (оставьте пустым для пропуска):\n");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                doctor.Name = name;
            }
            Console.WriteLine("Введите новую фамилию (оставьте пустым для пропуска):\n");
            var surname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname))
            {
                doctor.Surname = surname;
            }
            _doctorService.Update(doctorId, doctor);
            Console.WriteLine($"Информация о докторе с ID {doctorId} успешно обновлена.");
        }

        private void HandleDeleteDoctor()
        {
            Console.WriteLine("Введите ID доктора для удаления:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var doctorId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var doctor = _doctorService.Get(doctorId);
            if (doctor == null)
            {
                Console.Clear();
                Console.WriteLine($"Доктор с ID {doctorId} не найден.");
                return;
            }
            _doctorService.Delete(doctorId);
            Console.WriteLine($"Доктор с ID {doctorId} успешно удалён.");
        }

        // Patient handlers
        private void HandleCreatePatient()
        {
            Console.WriteLine("Для добавления пациента, введите имя:\n");
            var name = Console.ReadLine();

            Console.WriteLine("Введите фамилию:\n");
            var surname = Console.ReadLine();

            Console.WriteLine($"Выберите тип болезни:\n");
            foreach (Domain.Enums.IllnessTypes value in Enum.GetValues(typeof(Domain.Enums.IllnessTypes)))
            {
                Console.WriteLine($"{(int)value} — {value}");
            }
            var illnessInput = Console.ReadLine();
            int maxIllnessValue = Enum.GetValues(typeof(Domain.Enums.IllnessTypes)).Length;
            if (!int.TryParse(illnessInput, out var illnessNumber) ||
                illnessNumber < 1 || illnessNumber > maxIllnessValue)
            {
                Console.Clear();
                Console.WriteLine($"Вводить можно только цифры от 1 до {maxIllnessValue}. Вы ввели: {illnessInput}");
                return;
            }

            Console.WriteLine("Введите дополнительную информацию (опционально):\n");
            var additionalInfo = Console.ReadLine();

            Console.WriteLine("Введите адрес (опционально):\n");
            var address = Console.ReadLine();

            var newPatient = new Patient
            {
                Name = name,
                Surname = surname,
                IllnessType = (Domain.Enums.IllnessTypes)illnessNumber,
                AdditionalInfo = additionalInfo,
                Address = address
            };

            _patientService.Create(newPatient);
            Console.WriteLine($"Пациент {name} {surname} успешно создан.");
        }

        private void HandleGetPatient()
        {
            Console.WriteLine("Введите ID пациента для получения информации:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var patientId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var patient = _patientService.Get(patientId);
            if (patient == null)
            {
                Console.Clear();
                Console.WriteLine($"Пациент с ID {patientId} не найден.");
                return;
            }
            Console.WriteLine($"Информация о пациенте:\nID: {patient.Id}\nИмя: {patient.Name}\nФамилия: {patient.Surname}\nТип болезни: {patient.IllnessType}\nДополнительно: {patient.AdditionalInfo}\nАдрес: {patient.Address}\n");
        }

        private void HandleListPatients()
        {
            var patients = _patientService.GetAll();
            Console.WriteLine("Список всех пациентов:\n");
            foreach (var p in patients)
            {
                Console.WriteLine($"{p.Id}: {p.Name} {p.Surname}, Болезнь: {p.IllnessType}, Адрес: {p.Address}");
            }
        }

        private void HandleUpdatePatient()
        {
            Console.WriteLine("Введите ID пациента для обновления информации:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var patientId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var patient = _patientService.Get(patientId);
            if (patient == null)
            {
                Console.Clear();
                Console.WriteLine($"Пациент с ID {patientId} не найден.");
                return;
            }

            Console.WriteLine("Введите новое имя (оставьте пустым для пропуска):\n");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                patient.Name = name;
            }

            Console.WriteLine("Введите новую фамилию (оставьте пустым для пропуска):\n");
            var surname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname))
            {
                patient.Surname = surname;
            }

            Console.WriteLine($"Выберите новый тип болезни (оставьте пустым для пропуска):\n");
            foreach (Domain.Enums.IllnessTypes value in Enum.GetValues(typeof(Domain.Enums.IllnessTypes)))
            {
                Console.WriteLine($"{(int)value} — {value}");
            }
            var illnessInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(illnessInput))
            {
                int maxIllnessValue = Enum.GetValues(typeof(Domain.Enums.IllnessTypes)).Length;
                if (!int.TryParse(illnessInput, out var illnessNumber) ||
                    illnessNumber < 1 || illnessNumber > maxIllnessValue)
                {
                    Console.Clear();
                    Console.WriteLine($"Вводить можно только цифры от 1 до {maxIllnessValue}. Вы ввели: {illnessInput}");
                    return;
                }
                patient.IllnessType = (Domain.Enums.IllnessTypes)illnessNumber;
            }

            Console.WriteLine("Введите новую дополнительную информацию (оставьте пустым для пропуска):\n");
            var additionalInfo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(additionalInfo))
            {
                patient.AdditionalInfo = additionalInfo;
            }

            Console.WriteLine("Введите новый адрес (оставьте пустым для пропуска):\n");
            var address = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(address))
            {
                patient.Address = address;
            }

            _patientService.Update(patientId, patient);
            Console.WriteLine($"Информация о пациенте с ID {patientId} успешно обновлена.");
        }

        private void HandleDeletePatient()
        {
            Console.WriteLine("Введите ID пациента для удаления:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var patientId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var patient = _patientService.Get(patientId);
            if (patient == null)
            {
                Console.Clear();
                Console.WriteLine($"Пациент с ID {patientId} не найден.");
                return;
            }
            _patientService.Delete(patientId);
            Console.WriteLine($"Пациент с ID {patientId} успешно удалён.");
        }

        // Appointment handlers
        private void HandleCreateAppointment()
        {
            Console.WriteLine("Введите ID доктора для записи:\n");
            var docIdInput = Console.ReadLine();
            if (!int.TryParse(docIdInput, out var docId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {docIdInput}");
                return;
            }
            var doctor = _doctorService.Get(docId);
            if (doctor == null)
            {
                Console.Clear();
                Console.WriteLine($"Доктор с ID {docId} не найден.");
                return;
            }

            Console.WriteLine("Введите ID пациента для записи:\n");
            var patIdInput = Console.ReadLine();
            if (!int.TryParse(patIdInput, out var patId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {patIdInput}");
                return;
            }
            var patient = _patientService.Get(patId);
            if (patient == null)
            {
                Console.Clear();
                Console.WriteLine($"Пациент с ID {patId} не найден.");
                return;
            }

            Console.WriteLine("Введите дату и время начала приёма (например: 2025-11-17 14:30):\n");
            var fromInput = Console.ReadLine();
            if (!DateTime.TryParse(fromInput, out var dateFrom))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат даты/времени. Вы ввели: {fromInput}");
                return;
            }

            Console.WriteLine("Введите дату и время окончания приёма (например: 2025-11-17 15:00):\n");
            var toInput = Console.ReadLine();
            if (!DateTime.TryParse(toInput, out var dateTo))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат даты/времени. Вы ввели: {toInput}");
                return;
            }

            Console.WriteLine("Введите описание (опционально):\n");
            var description = Console.ReadLine();

            var newAppointment = new Appointment
            {
                Doctor = doctor,
                Patient = patient,
                DateTimeFrom = dateFrom,
                DateTimeTo = dateTo,
                Description = description
            };

            _appointmentService.Create(newAppointment);
            Console.WriteLine("Запись приёма успешно создана.");
        }

        private void HandleGetAppointment()
        {
            Console.WriteLine("Введите ID записи для получения информации:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var appointmentId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var appointment = _appointmentService.Get(appointmentId);
            if (appointment == null)
            {
                Console.Clear();
                Console.WriteLine($"Запись с ID {appointmentId} не найдена.");
                return;
            }
            Console.WriteLine($"Информация о записи:\nID: {appointment.Id}\nДоктор: {appointment.Doctor?.Name} {appointment.Doctor?.Surname}\nПациент: {appointment.Patient?.Name} {appointment.Patient?.Surname}\nС: {appointment.DateTimeFrom}\nПо: {appointment.DateTimeTo}\nОписание: {appointment.Description}\n");
        }

        private void HandleListAppointments()
        {
            var appointments = _appointmentService.GetAll();
            Console.WriteLine("Список всех записей приёма:\n");
            foreach (var a in appointments)
            {
                Console.WriteLine($"{a.Id}: Доктор: {a.Doctor?.Name} {a.Doctor?.Surname}, Пациент: {a.Patient?.Name} {a.Patient?.Surname}, С: {a.DateTimeFrom}, По: {a.DateTimeTo}");
            }
        }

        private void HandleUpdateAppointment()
        {
            Console.WriteLine("Введите ID записи для обновления:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var appointmentId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var appointment = _appointmentService.Get(appointmentId);
            if (appointment == null)
            {
                Console.Clear();
                Console.WriteLine($"Запись с ID {appointmentId} не найдена.");
                return;
            }

            Console.WriteLine("Введите новый ID доктора (оставьте пустым для пропуска):\n");
            var docIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(docIdInput))
            {
                if (!int.TryParse(docIdInput, out var docId))
                {
                    Console.Clear();
                    Console.WriteLine($"Некорректный формат ID. Вы ввели: {docIdInput}");
                    return;
                }
                var doctor = _doctorService.Get(docId);
                if (doctor == null)
                {
                    Console.Clear();
                    Console.WriteLine($"Доктор с ID {docId} не найден.");
                    return;
                }
                appointment.Doctor = doctor;
            }

            Console.WriteLine("Введите новый ID пациента (оставьте пустым для пропуска):\n");
            var patIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(patIdInput))
            {
                if (!int.TryParse(patIdInput, out var patId))
                {
                    Console.Clear();
                    Console.WriteLine($"Некорректный формат ID. Вы ввели: {patIdInput}");
                    return;
                }
                var patient = _patientService.Get(patId);
                if (patient == null)
                {
                    Console.Clear();
                    Console.WriteLine($"Пациент с ID {patId} не найден.");
                    return;
                }
                appointment.Patient = patient;
            }

            Console.WriteLine("Введите новую дату и время начала приёма (оставьте пустым для пропуска):\n");
            var fromInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fromInput))
            {
                if (!DateTime.TryParse(fromInput, out var dateFrom))
                {
                    Console.Clear();
                    Console.WriteLine($"Некорректный формат даты/времени. Вы ввели: {fromInput}");
                    return;
                }
                appointment.DateTimeFrom = dateFrom;
            }

            Console.WriteLine("Введите новую дату и время окончания приёма (оставьте пустым для пропуска):\n");
            var toInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(toInput))
            {
                if (!DateTime.TryParse(toInput, out var dateTo))
                {
                    Console.Clear();
                    Console.WriteLine($"Некорректный формат даты/времени. Вы ввели: {toInput}");
                    return;
                }
                appointment.DateTimeTo = dateTo;
            }

            Console.WriteLine("Введите новое описание (оставьте пустым для пропуска):\n");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                appointment.Description = description;
            }

            _appointmentService.Update(appointmentId, appointment);
            Console.WriteLine($"Информация о записи с ID {appointmentId} успешно обновлена.");
        }

        private void HandleDeleteAppointment()
        {
            Console.WriteLine("Введите ID записи для удаления:\n");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out var appointmentId))
            {
                Console.Clear();
                Console.WriteLine($"Некорректный формат ID. Вы ввели: {idInput}");
                return;
            }
            var appointment = _appointmentService.Get(appointmentId);
            if (appointment == null)
            {
                Console.Clear();
                Console.WriteLine($"Запись с ID {appointmentId} не найдена.");
                return;
            }
            _appointmentService.Delete(appointmentId);
            Console.WriteLine($"Запись с ID {appointmentId} успешно удалена.");
        }
    }



    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}